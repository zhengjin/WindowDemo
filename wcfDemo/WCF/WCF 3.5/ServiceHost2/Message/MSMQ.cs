using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace WCF.ServiceHost2.Message
{
    /// <summary>
    /// host WCF.ServiceLib.Message.MSMQ的类
    /// </summary>
    public class MSMQ
    {
        /// <summary>
        /// 启动WCF.ServiceLib.Message.MSMQ服务
        /// </summary>
        public void Launch()
        {
            // 队列名
            // 只能使用.\private$\YourPrivateMSMQName来访问本机的私有MSMQ队列
            //string queueName = @".\private$\SampleMSMQ";

            //// 没有queueName队列，则创建queueName队列
            //if (!System.Messaging.MessageQueue.Exists(queueName))
            //{
            //    // 第二个参数为是否创建事务性队列
            //    System.Messaging.MessageQueue.Create(queueName, true);
            //}

            ////using (ServiceHost host = new ServiceHost(typeof(WCF.ServiceLib.Message.MSMQ)))
            ////{
            ////    host.Open();

            ////    Console.WriteLine("服务已启动(WCF.ServiceLib.Message.MSMQ)");
            ////    Console.WriteLine("按<ENTER>停止服务");
            ////    Console.ReadLine();

            ////}

            //ServiceHost host = new ServiceHost(typeof(WCF.ServiceLib.Message.MSMQ));
            //host.Open();

            //Console.WriteLine("服务已启动(WCF.ServiceLib.Message.MSMQ)");
        }
    }
}
