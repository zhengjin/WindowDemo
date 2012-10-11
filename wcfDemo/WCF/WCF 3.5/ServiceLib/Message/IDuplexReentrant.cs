using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace WCF.ServiceLib.Message
{
    /// <summary>
    /// IDuplexReentrant接口（演示ConcurrencyMode.Reentrant）
    /// </summary>
    /// <remarks>
    /// IDuplexReentrantCallback - 回调接口
    /// </remarks>
    [ServiceContract(CallbackContract = typeof(IDuplexReentrantCallback))]
    public interface IDuplexReentrant
    {
        /// <summary>
        /// Hello
        /// </summary>
        /// <param name="name">名字</param>
        [OperationContract]
        void HelloDuplexReentrant(string name);
    }

    /// <summary>
    /// IDuplexReentrant回调接口
    /// </summary>
    public interface IDuplexReentrantCallback
    {
        /// <summary>
        /// Hello
        /// </summary>
        /// <param name="name"></param>
        [OperationContract]
        void HelloDuplexReentrantCallback(string name);
    }
}
