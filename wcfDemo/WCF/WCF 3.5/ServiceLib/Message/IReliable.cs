using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace WCF.ServiceLib.Message
{
    /// <summary>
    /// 演示可靠性消息的接口
    /// </summary>
    /// <remarks>
    /// DeliveryRequirements - 指定绑定必须提供给服务或客户端实现的功能要求
    /// RequireOrderedDelivery - 如果指示 WCF 确认绑定必须支持排序消息，则为 true；否则为 false。默认值为 false。如果设置为了 true，那么也需要在配置的时候将order设置为 true
    /// </remarks>
    [ServiceContract]
    [DeliveryRequirements(RequireOrderedDelivery = true)]
    public interface IReliable
    {
        /// <summary>
        /// 将字符串写入文本文件
        /// </summary>
        /// <param name="str">需要写入文本文件的字符串</param>
        [OperationContract(IsOneWay = true)]
        void Write(string str);
    }
}
