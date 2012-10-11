using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace WCF.ServiceLib.SessionManagement
{
    /// <summary>
    /// 演示会话状态的接口
    /// </summary>
    /// <remarks>
    /// InstanceContextMode - 获取或设置指示新服务对象何时创建的值。
    /// InstanceContextMode.PerSession - 为每个会话创建一个新的 System.ServiceModel.InstanceContext 对象。
    /// InstanceContextMode 的默认值为 InstanceContextMode.PerSession
    /// </remarks>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class Hello : IHello
    {
        private int _counter;

        /// <summary>
        /// 初始化Session
        /// </summary>
        public void StartSession()
        {
            _counter = 0;
        }

        /// <summary>
        /// 结束Session
        /// </summary>
        public void StopSession()
        {
            _counter = 0;
        }

        /// <summary>
        /// 获取计数器结果
        /// </summary>
        /// <returns></returns>
        public int Counter()
        {
            _counter++;

            return _counter;
        }

        /// <summary>
        /// 获取SessionId
        /// </summary>
        /// <returns></returns>
        public string GetSessionId()
        {
            return OperationContext.Current.SessionId;
        }
    }
}
