using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Robin_Wcf_Host_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //服务地址
            Uri baseAddress = new Uri("net.tcp://127.0.0.1:8081/Robin_Wcf_Formatter"); 
            ServiceHost host = new ServiceHost(typeof(Robin_Wcf_SvcLib.Service1), new Uri[] { baseAddress });
            //服务绑定
            NetTcpBinding bind = new NetTcpBinding();
            host.AddServiceEndpoint(typeof(Robin_Wcf_SvcLib.IService1), bind, "");
            if (host.Description.Behaviors.Find<System.ServiceModel.Description.ServiceMetadataBehavior>() == null)
            {
                System.ServiceModel.Description.ServiceMetadataBehavior svcMetaBehavior = new System.ServiceModel.Description.ServiceMetadataBehavior();
                svcMetaBehavior.HttpGetEnabled = true;
                svcMetaBehavior.HttpGetUrl = new Uri("http://127.0.0.1:8001/Mex");
                host.Description.Behaviors.Add(svcMetaBehavior);
            }
            host.Opened+=new EventHandler(delegate(object obj,EventArgs e){
                Console.WriteLine("服务已经启动！");
            });
            foreach (var sep in host.Description.Endpoints)
            {
                sep.Behaviors.Add(new RobinLib.OutputMessageBehavior());
                foreach (var op in sep.Contract.Operations)
                { 
                    op.Behaviors.Add(new RobinLib.Base64BodyBehavior()); 
                }                 
            } 
            host.Open();
            Console.Read();
        }
    }
}

