using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace WCF.ServiceLib.SessionManagement
{
    /// <summary>
    /// 演示会话状态的接口
    /// </summary>NotAllowed
    /// <remarks>
    /// SessionMode - 获取或设置是否允许、不允许或要求会话
    /// SessionMode.Allowed - 指定当传入绑定支持会话时，协定也支持会话（默认值）
    /// SessionMode.Required -  指定协定需要会话绑定。如果绑定并未配置为支持会话，则将引发异常
    /// SessionMode.NotAllowed - 指定协定永不支持启动会话的绑定
    /// </remarks>
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IHello
    {
        /// <summary>
        /// 初始化Session
        /// </summary>
        /// <remarks>
        /// IsInitiating - 获取或设置一个值，该值指示方法是否实现可在服务器上启动会话（如果存在会话）的操作。
        /// IsTerminating - 获取或设置一个值，该值指示服务操作在发送答复消息（如果存在）后，是否会导致服务器关闭会话。
        /// </remarks>
        [OperationContract(IsInitiating = true, IsTerminating = false)]
        void StartSession();

        /// <summary>
        /// 结束Session
        /// </summary>
        [OperationContract(IsInitiating = false, IsTerminating = true)]
        void StopSession();

        /// <summary>
        /// 获取计数器结果
        /// </summary>
        /// <returns></returns>
        [OperationContract(IsInitiating = false, IsTerminating = false)]
        int Counter();

        /// <summary>
        /// 获取SessionId
        /// </summary>
        /// <returns></returns>
        [OperationContract(IsInitiating = false, IsTerminating = false)]
        string GetSessionId();
    }
}
