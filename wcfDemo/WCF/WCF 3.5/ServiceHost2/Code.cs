using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Description;

namespace WCF.ServiceHost2
{
    /// <summary>
    /// 用编码的方式代替配置的方式
    /// </summary>
    public class Code
    {
        public void Launch()
        {
            Uri baseUri = new Uri("http://localhost:12345/Sample/Hello/");

            using (ServiceHost host = new ServiceHost(typeof(WCF.ServiceLib.Sample.Hello), baseUri))
            {
                host.AddServiceEndpoint(typeof(WCF.ServiceLib.Sample.IHello), new BasicHttpBinding(), string.Empty);

                ServiceMetadataBehavior behavior = host.Description.Behaviors.Find<ServiceMetadataBehavior>();

                if (behavior == null)
                {
                    behavior = new ServiceMetadataBehavior();

                    behavior.HttpGetEnabled = true;
                    behavior.HttpGetUrl = baseUri;

                    host.Description.Behaviors.Add(behavior);
                }
                else
                {
                    behavior.HttpGetEnabled = true;
                    behavior.HttpGetUrl = baseUri;
                }

                host.Open();

                Console.WriteLine("服务已启动(WCF.ServiceLib.Sample.Hello)");
            }
        }
    }
}
