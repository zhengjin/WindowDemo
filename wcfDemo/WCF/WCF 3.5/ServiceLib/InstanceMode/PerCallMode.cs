using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace WCF.ServiceLib.InstanceMode
{
    /// <summary>
    /// 演示实例模型的接口（PerCall）
    /// </summary>
    [ServiceContract]
    public interface IPerCallMode
    {
        /// <summary>
        /// 获取计数器结果
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        int Counter();
    }

    /// <summary>
    /// 演示实例模型的类（PerCall）
    /// </summary>
    /// <remarks>
    /// InstanceContextMode - 获取或设置指示新服务对象何时创建的值。
    /// InstanceContextMode.PerCall - 新的 System.ServiceModel.InstanceContext 对象在每次调用前创建，在调用后回收。
    /// </remarks>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class PerCallMode : IPerCallMode
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
