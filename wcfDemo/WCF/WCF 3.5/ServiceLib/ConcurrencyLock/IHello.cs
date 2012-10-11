using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace WCF.ServiceLib.ConcurrencyLock
{
    /// <summary>
    /// 演示并发控制(锁)的接口
    /// </summary>
    [ServiceContract]
    public interface IHello
    {
        /// <summary>
        /// 计数器
        /// </summary>
        /// <param name="lockType">锁的类型</param>
        [OperationContract]
        void Counter(LockType lockType);

        /// <summary>
        /// 获取计数器被调用的结果
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetResult();

        /// <summary>
        /// 清空计数器和结果
        /// </summary>
        [OperationContract]
        void CleanResult();
    }
}
