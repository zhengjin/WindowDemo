using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace Client2.Message
{
    /// <summary>
    /// 实现回调接口
    /// </summary>
    /// <remarks>
    /// CallbackBehavior - 在客户端应用程序中配置回调服务实现
    /// UseSynchronizationContext - 如果对服务的所有调用都必须在 System.Threading.SynchronizationContext 指定的线程上运行，则为 true；否则为false。默认值为 true。
    /// </remarks>
    [System.ServiceModel.CallbackBehavior(UseSynchronizationContext = false)]
    public class ReentrantCallbackType : MessageSvc.DuplexReentrant.IDuplexReentrantCallback
    {
        /// <summary>
        /// Hello
        /// </summary>
        /// <param name="name">名字</param>
        public void HelloDuplexReentrantCallback(string name)
        {
            MessageBox.Show("Hello: " + name);
        }
    }
}
