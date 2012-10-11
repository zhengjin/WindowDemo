using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace WCF.ServiceLib.Exception
{
    /// <summary>
    /// IHello接口
    /// </summary>
    [ServiceContract]
    public interface IHello
    {
        /// <summary>
        /// 抛出Exception异常
        /// </summary>
        [OperationContract]
        void HelloException();

        /// <summary>
        /// 抛出FaultException异常
        /// </summary>
        [OperationContract]
        void HelloFaultException();

        /// <summary>
        /// 抛出FaultException泛型T异常
        /// </summary>
        [OperationContract]
        [FaultContract(typeof(FaultMessage))]
        void HelloFaultExceptionGeneric();

        /// <summary>
        /// IErrorHandler处理异常
        /// </summary>
        [OperationContract]
        void HelloIErrorHandler();
    }
}
