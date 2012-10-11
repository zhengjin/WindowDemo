using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.ServiceModel;

namespace Client2.Binding
{
    /// <summary>
    /// 演示Binding.Hello的类
    /// </summary>
    public class Hello
    {
        /// <summary>
        /// Hello
        /// </summary>
        /// <param name="name">名字</param>
        public void SayHello(string name)
        {
            // 写代码的方式做client
            // IHello proxy = ChannelFactory<IHello>.CreateChannel(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:54321/Binding/Hello"));

            var proxy = new BindingSvc.Hello.HelloClient();

            MessageBox.Show(proxy.SayHello(name));

            proxy.Close();
        }
    }
}
