using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.IO;

namespace WCF.ServiceLib.Message
{
    /// <summary>
    /// 消息契约（定义与 SOAP 消息相对应的强类型类）
    /// </summary>
    [MessageContract]
    public class FileWrapper
    {
        /// <summary>
        /// 指定数据成员为 SOAP 消息头
        /// </summary>
        [MessageHeader]
        public string FilePath;

        /// <summary>
        /// 指定将成员序列化为 SOAP 正文中的元素
        /// </summary>
        [MessageBodyMember]
        public Stream FileData;
    }

    /// <summary>
    /// IStreamed接口
    /// </summary>
    [ServiceContract]
    public interface IStreamed
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <remarks>
        /// 1、支持数据流传输的绑定有：BasicHttpBinding、NetTcpBinding 和 NetNamedPipeBinding
        /// 2、流数据类型必须是可序列化的 Stream 或 MemoryStream
        // /3、传递时消息体(Message Body)中不能包含其他数据，即参数中只能有一个System.ServiceModel.MessageBodyMember
        /// </remarks>
        /// <param name="fileWrapper">WCF.ServiceLib.Message.FileWrapper</param>
        [OperationContract]
        void UploadFile(FileWrapper fileWrapper);
    }
}
