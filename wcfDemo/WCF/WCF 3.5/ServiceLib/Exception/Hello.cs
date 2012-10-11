using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace WCF.ServiceLib.Exception
{
    /// <summary>
    /// Hello类
    /// </summary>
    public class Hello : IHello, IDisposable, IServiceBehavior 
    {
        /// <summary>
        /// 抛出Exception异常
        /// </summary>
        public void HelloException()
        {
            throw new System.Exception("抛出Exception异常");
        }

        /// <summary>
        /// 抛出FaultException异常
        /// </summary>
        public void HelloFaultException()
        {
            throw new FaultException("抛出FaultException异常", new FaultCode("服务"));
        }

        /// <summary>
        /// 抛出FaultException泛型T异常
        /// </summary>
        public void HelloFaultExceptionGeneric()
        {
            throw new FaultException<FaultMessage>(new FaultMessage { Message = "抛出FaultException<T>异常", ErrorCode = -1 }, "为了测试FaultException<T>用的");
        }

        /// <summary>
        /// IErrorHandler处理异常
        /// </summary>
        public void HelloIErrorHandler()
        {
            throw new System.IO.IOException("抛出异常，用IErrorHandler处理");
        }

        /// <summary>
        /// 实现IDisposable接口的Dispose()方法
        /// </summary>
        public void Dispose()
        {

        }

        /// <summary>
        /// 为契约增加自定义绑定参数
        /// </summary>
        /// <param name="serviceDescription">服务描述</param>
        /// <param name="serviceHostBase">服务宿主</param>
        /// <param name="endpoints">服务端点</param>
        /// <param name="bindingParameters">需要增加的自定义绑定参数</param>
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {

        }

        /// <summary>
        /// runtime时修改属性值或增加自定义扩展对象
        /// </summary>
        /// <param name="serviceDescription">服务描述</param>
        /// <param name="serviceHostBase">服务宿主</param>
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            IErrorHandler handler = new FaultErrorHandler();

            foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
            {
                // 增加错误处理器
                dispatcher.ErrorHandlers.Add(handler);
            }
        }

        /// <summary>
        /// 检查服务描述和服务宿主，以确认服务可以成功运行
        /// </summary>
        /// <param name="serviceDescription">服务描述</param>
        /// <param name="serviceHostBase">服务宿主</param>
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {

        }
    }
}
