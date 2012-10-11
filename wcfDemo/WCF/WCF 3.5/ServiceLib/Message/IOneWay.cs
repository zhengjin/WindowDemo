using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace WCF.ServiceLib.Message
{
    /// <summary>
    /// IOneWay接口
    /// </summary>
    [ServiceContract]
    public interface IOneWay
    {
        /// <summary>
        /// 不使用OneWay(同步调用)
        /// </summary>
        [OperationContract]
        void WithoutOneWay();

        /// <summary>
        /// 使用OneWay(异步调用)
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void WithOneWay();
    }
}
