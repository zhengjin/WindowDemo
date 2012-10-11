using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;

namespace RobinLib
{
    /// <summary>
    /// 将消息主题序列为base64的OperationBehavior
    /// </summary>
    public class Base64BodyBehavior : IOperationBehavior
    {
        #region IOperationBehavior 成员

        public void AddBindingParameters(OperationDescription operationDescription, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
           
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, System.ServiceModel.Dispatcher.ClientOperation clientOperation)
        {
            clientOperation.SerializeRequest = true;
            clientOperation.DeserializeReply = true;
            clientOperation.Formatter = new Base64BodyFormatter(clientOperation.Formatter);   
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, System.ServiceModel.Dispatcher.DispatchOperation dispatchOperation)
        {
            dispatchOperation.DeserializeRequest = true;
            dispatchOperation.SerializeReply = true;
            dispatchOperation.Formatter = new Base64BodyFormatter(dispatchOperation.Formatter);
        }

        public void Validate(OperationDescription operationDescription)
        {

        }

        #endregion
    }
}
