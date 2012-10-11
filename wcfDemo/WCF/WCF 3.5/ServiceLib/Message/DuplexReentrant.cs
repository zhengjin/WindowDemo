using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace WCF.ServiceLib.Message
{
    /// <summary>
    /// DuplexReentrant类 （演示ConcurrencyMode.Reentrant）
    /// </summary>
    /// <remarks>
    /// ConcurrencyMode - 获取或设置一个值，该值指示服务是支持单线程、多线程还是支持可重入调用。默认值为 System.ServiceModel.ConcurrencyMode.Single。
    /// Single - 服务实例是单线程的，且不接受可重入调用。
    /// Reentrant - 服务实例是单线程的，且接受可重入调用。
    /// Multiple - 服务实例是多线程的。
    /// </remarks>
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class DuplexReentrant : IDuplexReentrant
    {
        /// <summary>
        /// Hello
        /// </summary>
        /// <param name="name">名字</param>
        public void HelloDuplexReentrant(string name)
        {
            // 声明回调接口
            IDuplexReentrantCallback callback = OperationContext.Current.GetCallbackChannel<IDuplexReentrantCallback>();

            // 调用回调接口中的方法
            callback.HelloDuplexReentrantCallback(name);
        }
    }
}
