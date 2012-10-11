using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace WCF.ServiceLib.Message
{
    /// <summary>
    /// IDuplex接口
    /// </summary>
    /// <remarks>
    /// CallbackContract - 回调接口
    /// </remarks>
    [ServiceContract(CallbackContract = typeof(IDuplexCallback))]
    public interface IDuplex
    {
        /// <summary>
        /// Hello
        /// </summary>
        /// <param name="name">名字</param>
        [OperationContract(IsOneWay = true)]
        void HelloDuplex(string name);
    }

    /// <summary>
    /// IDuplex回调接口
    /// </summary>
    public interface IDuplexCallback
    {
        /// <summary>
        /// Hello
        /// </summary>
        /// <param name="name"></param>
        [OperationContract(IsOneWay = true)]
        void HelloDuplexCallback(string name);
    }
}
