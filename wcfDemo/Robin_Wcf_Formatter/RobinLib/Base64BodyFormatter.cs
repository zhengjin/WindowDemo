using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.IO;
using System.Xml;

namespace RobinLib
{
    /// <summary>
    /// 这个Formatter能将消息的正文转换成base64字符串,这样做的好处是能混淆正文内容。对消息安全有好处
    /// </summary>
    public class Base64BodyFormatter : IClientMessageFormatter, IDispatchMessageFormatter
    {
        private IClientMessageFormatter clientFormatter;
        private IDispatchMessageFormatter dispatchFormatter;

private Message Base64BodyMessage(Message ora_msg)
{
    MemoryStream ms = new MemoryStream();
    XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(ms, Encoding.UTF8);
    ora_msg.WriteBodyContents(writer);
    writer.Flush();
    string body = System.Text.Encoding.UTF8.GetString(ms.GetBuffer());
    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(body);
    body = Convert.ToBase64String(buffer);
    Message msg = Message.CreateMessage(ora_msg.Version, ora_msg.Headers.Action, new MyBodyWriter(body));
    ora_msg.Close();
    return msg;
}

private Message RestoreMessageFormBase64Message(Message message)
{
    Message msg = null;
    MemoryStream ms = new MemoryStream();
    XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(ms, Encoding.UTF8);
    message.WriteBody(writer);
    writer.Flush();
    string body = System.Text.Encoding.UTF8.GetString(ms.GetBuffer());
    int index = body.IndexOf(">");
    int index2 = body.IndexOf("</");
    body = body.Substring(index + 1, index2 - index - 1);
    byte[] buffer2 = Convert.FromBase64String(body);
    body = System.Text.Encoding.UTF8.GetString(buffer2);
    XmlDocument doc = new XmlDocument();
    doc.LoadXml(body);
    msg = Message.CreateMessage(message.Version, message.Headers.Action, new MyBodyWriter(doc));
    msg.Headers.FaultTo = message.Headers.FaultTo;
    msg.Headers.From = message.Headers.From;
    msg.Headers.MessageId = message.Headers.MessageId;
    msg.Headers.RelatesTo = message.Headers.RelatesTo;
    msg.Headers.ReplyTo = message.Headers.ReplyTo;
    msg.Headers.To = message.Headers.To;
    foreach (var mh in message.Headers)
    {
        if (mh is MessageHeader)
        {
            if (mh.Name != "Action" && mh.Name != "FaultTo" && mh.Name != "From" && mh.Name != "MessageID" && mh.Name != "RelatesTo" && mh.Name != "ReplyTo" && mh.Name != "To")
            {
                msg.Headers.Add(mh as MessageHeader);
            }
        }
    }
    msg.Properties.Clear();
    foreach (var p in message.Properties)
    {
        msg.Properties[p.Key] = p.Value;
    }
    return msg;
}

        public Base64BodyFormatter(IClientMessageFormatter ora_Formatter)
        {
            this.clientFormatter = ora_Formatter;
        }
        public Base64BodyFormatter(IDispatchMessageFormatter ora_Formatter)
        {
            this.dispatchFormatter = ora_Formatter;
        }

        #region 实现IClientMessageFormatter成员，格式化客户端中的消息

        public object DeserializeReply(System.ServiceModel.Channels.Message message, object[] parameters)
        {
            Message msg = RestoreMessageFormBase64Message(message); 
            message = msg;
            return clientFormatter.DeserializeReply(msg, parameters);
        }

        public System.ServiceModel.Channels.Message SerializeRequest(System.ServiceModel.Channels.MessageVersion messageVersion, object[] parameters)
        {
            Message ora_msg = clientFormatter.SerializeRequest(messageVersion, parameters);
            return Base64BodyMessage(ora_msg);
        }

        #endregion

        #region 实现IDispatchMessageFormatter成员,格式化服务端消息

        public void DeserializeRequest(System.ServiceModel.Channels.Message message, object[] parameters)
        {
            Message msg = RestoreMessageFormBase64Message(message);
            dispatchFormatter.DeserializeRequest(msg, parameters);
            message = msg;
        }

        public System.ServiceModel.Channels.Message SerializeReply(System.ServiceModel.Channels.MessageVersion messageVersion, object[] parameters, object result)
        {
            Message ora_msg = dispatchFormatter.SerializeReply(messageVersion, parameters, result);
            return Base64BodyMessage(ora_msg);
        }

        #endregion
    }
}
