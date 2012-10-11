using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel.Dispatcher;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WCF.ServiceLib.Exception
{
    /// <summary>
    /// 自定义错误处理器（继承自System.ServiceModel.Dispatcher.IErrorHandler）
    /// </summary>
    public class FaultErrorHandler : IErrorHandler
    {
        /// <summary>
        /// 在异常返回给客户端之后被调用
        /// </summary>
        /// <param name="error">异常</param>
        /// <returns></returns>
        public bool HandleError(System.Exception error)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(@"C:\WCF_Log.txt", true);
            sw.Write("IErrorHandler - HandleError测试。错误类型：{0}；错误信息：{1}", error.GetType().ToString(), error.Message);
            sw.WriteLine();
            sw.Close();

            // true - 已处理
            return true;
        }

        /// <summary>
        /// 在异常发生后，异常信息返回前被调用
        /// </summary>
        /// <param name="error">异常</param>
        /// <param name="version">SOAP版本</param>
        /// <param name="fault">返回给客户端的错误信息</param>
        public void ProvideFault(System.Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {
            if (error is System.IO.IOException)
            {
                FaultException ex = new FaultException("IErrorHandler - ProvideFault测试");

                MessageFault mf = ex.CreateMessageFault();

                fault = System.ServiceModel.Channels.Message.CreateMessage(version, mf, ex.Action);

                // InvalidOperationException error = new InvalidOperationException("An invalid operation has occurred.");
                // MessageFault mfault = MessageFault.CreateFault(new FaultCode("Server", new FaultCode(String.Format("Server.{0}", error.GetType().Name))), new FaultReason(error.Message), error);
                // FaultException fe = FaultException.CreateFault(mfault, typeof(InvalidOperationException));
            }
        }
    }
}
