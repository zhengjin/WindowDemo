using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Runtime.Serialization;
namespace WCFHost
{
    //采用自托管方式，也可以是IIS、WAS,Windows服务等用户自定义程序托管服务
   public class WCFHost
    {
        static void Main(string[] args)
        {

            //反射方式创建服务实例，
            //Using方式生命实例，可以在对象生命周期结束时候，释放非托管资源
            using (ServiceHost host = new ServiceHost(typeof(WCFService.WCFService)))
            {
                
                ////判断是否以及打开连接，如果尚未打开，就打开侦听端口
                if (host.State !=CommunicationState.Opening)
                host.Open();
                //显示运行状态
                Console.WriteLine("Host is runing! and state is {0}",host.State);

                  //print endpoint information
                foreach (ServiceEndpoint se in host.Description.Endpoints)
                {
                    Console.WriteLine("Host is listening at {0}", se.Address.Uri.ToString());

                }
                  
                 
                  
                //等待输入即停止服务
                Console.Read();
            
            
            }
        }
    }
}
