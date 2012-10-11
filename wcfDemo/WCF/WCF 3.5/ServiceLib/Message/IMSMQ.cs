using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace WCF.ServiceLib.Message
{
    /// <summary>
    /// 演示MSMQ的接口
    /// </summary>
    /// <remarks>
    /// DeliveryRequirements - 指定绑定必须提供给服务或客户端实现的功能要求
    /// QueuedDeliveryRequirements - 指定服务的绑定是否必须支持排队协定
    /// QueuedDeliveryRequirementsMode.Allowed - 允许排队传送
    /// QueuedDeliveryRequirementsMode.Required - 要求排队传送
    /// QueuedDeliveryRequirementsMode.NotAllowed - 不允许排队传送
    /// </remarks>
    [ServiceContract]
    [DeliveryRequirements(QueuedDeliveryRequirements = QueuedDeliveryRequirementsMode.Required)]
    public interface IMSMQ
    {
        /// <summary>
        /// 将字符串写入文本文件
        /// </summary>
        /// <param name="str">需要写入文本文件的字符串</param>
        /// <remarks>
        /// 如果要使用 MSMQ 的话，则必须配置IsOneWay = true
        /// </remarks>
        [OperationContract(IsOneWay = true)]
        void Write(string str);
    }
}
