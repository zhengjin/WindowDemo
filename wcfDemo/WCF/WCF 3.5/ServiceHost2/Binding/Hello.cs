using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace WCF.ServiceHost2.Binding
{
    /// <summary>
    /// host WCF.ServiceLib.Binding.Hello的类
    /// </summary>
    public class Hello
    {
        /// <summary>
        /// 启动WCF.ServiceLib.Binding.Hello服务
        /// </summary>
        public void Launch()
        {
            //using (ServiceHost host = new ServiceHost(typeof(WCF.ServiceLib.Binding.Hello)))
            //{
            //    // 写代码的方式做host
            //    // host.AddServiceEndpoint(typeof(WCF.ServiceLib.Binding.IHello), new NetTcpBinding(), "net.tcp://localhost:54321/Binding/Hello");
            //    host.Open();

            //    Console.WriteLine("服务已启动(WCF.ServiceLib.Binding.Hello)");
            //    Console.WriteLine("按<ENTER>停止服务");
            //    Console.ReadLine();
            //}

            ServiceHost host = new ServiceHost(typeof(WCF.ServiceLib.Binding.Hello));
            host.Open();

            Console.WriteLine("服务已启动(WCF.ServiceLib.Binding.Hello)");
        }
    }
}
