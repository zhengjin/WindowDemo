using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Dispatcher;

namespace RobinLib
{
    public class MessageInspector : IClientMessageInspector,IDispatchMessageInspector
    {
        #region IClientMessageInspector 成员

        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            Console.WriteLine(reply.ToString());
        }

        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            Console.WriteLine(request.ToString());
            return null;
        }

        #endregion

        #region IDispatchMessageInspector 成员

        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel, System.ServiceModel.InstanceContext instanceContext)
        {
            Console.WriteLine(request.ToString());
            return null;
        }

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            Console.WriteLine(reply.ToString());
        }

        #endregion
    }
}
