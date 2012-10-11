using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace WCF.ServiceHost2.Message
{
    /// <summary>
    /// host WCF.ServiceLib.Message.Streamed的类
    /// </summary>
    public class Streamed
    {
        /// <summary>
        /// 启动WCF.ServiceLib.Message.Streamed服务
        /// </summary>
        public void Launch()
        {
            //using (ServiceHost host = new ServiceHost(typeof(WCF.ServiceLib.Message.Streamed)))
            //{
            //    host.Open();

            //    Console.WriteLine("服务已启动(WCF.ServiceLib.Message.Streamed)");
            //    Console.WriteLine("按<ENTER>停止服务");
            //    Console.ReadLine();

            //}

            ServiceHost host = new ServiceHost(typeof(WCF.ServiceLib.Message.Streamed));
            host.Open();

            Console.WriteLine("服务已启动(WCF.ServiceLib.Message.Streamed)");
        }
    }
}
