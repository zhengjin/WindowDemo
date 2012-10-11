using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace WCF.ServiceHost2.Message
{
    /// <summary>
    /// host WCF.ServiceLib.Message.OneWay的类
    /// </summary>
    public class OneWay
    {
        /// <summary>
        /// 启动WCF.ServiceLib.Message.OneWay服务
        /// </summary>
        public void Launch()
        {
            //using (ServiceHost host = new ServiceHost(typeof(WCF.ServiceLib.Message.OneWay)))
            //{
            //    host.Open();

            //    Console.WriteLine("服务已启动(WCF.ServiceLib.Message.OneWay)");
            //    Console.WriteLine("按<ENTER>停止服务");
            //    Console.ReadLine();

            //}

            ServiceHost host = new ServiceHost(typeof(WCF.ServiceLib.Message.OneWay));
            host.Open();

            Console.WriteLine("服务已启动(WCF.ServiceLib.Message.OneWay)");
        }
    }
}
