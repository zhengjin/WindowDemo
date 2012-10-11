using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace WCF.ServiceLib.InstanceMode
{
    /// <summary>
    /// 演示实例模型的接口（Single）
    /// </summary>
    [ServiceContract]
    public interface ISingleMode
    {
        /// <summary>
        /// 获取计数器结果
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        int Counter();
    }

    /// <summary>
    /// 演示实例模型的接口（Single）
    /// </summary>
    /// <remarks>
    /// InstanceContextMode - 获取或设置指示新服务对象何时创建的值。
    /// InstanceContextMode.Single - 只有一个 System.ServiceModel.InstanceContext 对象用于所有传入呼叫，并且在调用后不回收。如果服务对象不存在，则创建一个。
    /// </remarks>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SingleMode : ISingleMode
    {
        private int _counter;

        /// <summary>
        /// 获取计数器结果
        /// </summary>
        /// <returns></returns>
        public int Counter()
        {
            _counter++;

            return _counter;
        }
    }
}
