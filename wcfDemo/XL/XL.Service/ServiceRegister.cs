using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Activation;
using System.Web.Routing;
using System.Reflection;
using System.ServiceModel;

namespace XL.Service
{
    /// <summary>
    /// 为了进一步拓展，才定义了自己的ServiceHostFactory
    /// </summary>
    public class MyServiceHostFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var result = base.CreateServiceHost(serviceType, baseAddresses);
            return result;
        }
    }
    public static class ServiceRegister
    {
        private static ServiceHostFactory wshf;
        /// <summary>
        /// 静态构造函数只执行一次
        /// </summary>
        static ServiceRegister()
        {
            wshf = new MyServiceHostFactory();
        }
        /// <summary>
        /// 通过反射注册服务
        /// </summary>
        public static void RegisterAllService()
        {
            
            var ass = (typeof(ServiceRegister)).Assembly;
            var ts = ass.GetTypes();
            foreach (var t in ts)
            {
                //约定:类型名以Service结尾的为WCF服务类型
                if (t.Name.EndsWith("Service"))
                {
                    var serviceName = t.FullName.Substring("XL.Service.".Length);
                    serviceName = serviceName.Replace(".", "-");
                    var sr = new ServiceRoute(serviceName, wshf, t);
                    RouteTable.Routes.Add(sr);
                }
            }
        }
    }
}