using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Windows.Forms;

namespace Client2.Message
{
    /// <summary>
    /// 演示Message.DuplexReentrant的类
    /// </summary>
    public class DuplexReentrant
    {
        /// <summary>
        /// Hello
        /// </summary>
        /// <param name="name">名字</param>
        public void HelloDulexReentrant(string name)
        {
            var ct = new Client2.Message.ReentrantCallbackType();
            var ctx = new InstanceContext(ct);

            var proxy = new MessageSvc.DuplexReentrant.DuplexReentrantClient(ctx);

            proxy.HelloDuplexReentrant(name);
        }
    }
}
