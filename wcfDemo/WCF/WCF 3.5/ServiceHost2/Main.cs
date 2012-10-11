using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCF.ServiceHost2
{
    class Hello
    {
        static void Main(string[] args)
        {
            // host WCF.ServiceLib.Binding.Hello
            new WCF.ServiceHost2.Binding.Hello().Launch();

            // host WCF.ServiceLib.Message.OneWay
            new WCF.ServiceHost2.Message.OneWay().Launch();

            // host WCF.ServiceLib.Message.Duplex
            new WCF.ServiceHost2.Message.Duplex().Launch();

            // host WCF.ServiceLib.Message.Streamed
            new WCF.ServiceHost2.Message.Streamed().Launch();

            // host WCF.ServiceLib.Message.DuplexReentrant
            new WCF.ServiceHost2.Message.DuplexReentrant().Launch();

            // host WCF.ServiceLib.Message.MSMQ
            new WCF.ServiceHost2.Message.MSMQ().Launch();


            Console.WriteLine("按<ENTER>停止服务");
            Console.ReadLine();
        }
    }
}
