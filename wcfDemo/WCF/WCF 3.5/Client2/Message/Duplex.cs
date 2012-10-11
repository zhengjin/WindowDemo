using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Windows.Forms;

namespace Client2.Message
{
    /// <summary>
    /// 演示Message.Duplex的类
    /// </summary>
    public class Duplex
    {
        /// <summary>
        /// Hello
        /// </summary>
        /// <param name="name">名字</param>
        public void HelloDulex(string name)
        {
            var ct = new Client2.Message.CallbackType();
            var ctx = new InstanceContext(ct);

            var proxy = new MessageSvc.Duplex.DuplexClient(ctx);

            proxy.HelloDuplex(name);
        }
    }
}
