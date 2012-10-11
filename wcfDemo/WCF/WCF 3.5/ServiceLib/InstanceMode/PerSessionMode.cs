using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace WCF.ServiceLib.InstanceMode
{
    /// <summary>
    /// 演示实例模型的接口（PerSession）
    /// </summary>
    [ServiceContract()]
    public interface IPerSessionMode
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
    /// InstanceContextMode.PerSession - 为每个会话创建一个新的 System.ServiceModel.InstanceContext 对象。
    /// InstanceContextMode 的默认值为 InstanceContextMode.PerSession
    /// </remarks>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class PerSessionMode : IPerSessionMode
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
