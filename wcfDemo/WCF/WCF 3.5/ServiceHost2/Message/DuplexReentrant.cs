using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace WCF.ServiceHost2.Message
{
    /// <summary>
    /// host WCF.ServiceLib.Message.DuplexReentrant的类
    /// </summary>
    public class DuplexReentrant
    {
        /// <summary>
        /// 启动WCF.ServiceLib.Message.DuplexReentrant服务
        /// </summary>
        public void Launch()
        {
            //using (ServiceHost host = new ServiceHost(typeof(WCF.ServiceLib.Message.DuplexReentrant)))
            //{
            //    host.Open();

            //    Console.WriteLine("服务已启动(WCF.ServiceLib.Message.DuplexReentrant)");
            //    Console.WriteLine("按<ENTER>停止服务");
            //    Console.ReadLine();

            //}

            ServiceHost host = new ServiceHost(typeof(WCF.ServiceLib.Message.DuplexReentrant));
            host.Open();

            Console.WriteLine("服务已启动(WCF.ServiceLib.Message.DuplexReentrant)");
        }
    }
}
