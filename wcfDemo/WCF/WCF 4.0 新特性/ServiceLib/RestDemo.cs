/*
 * 注意：
 * 如果需要引用 System.ServiceModel.Web 程序集的话，需要把程序的目标框架设置为 .NET Framework 4 而不是默认的 .NET Framework 4 Client Profile（精简版）
 * 引用了目标框架为 .NET Framework 4 的项目的程序的目标框架也应该是 .NET Framework 4
 */

/*
 * 要在 REST 服务上实现 HTTP 缓存，需要做的配置如下
 * 1、在 web.config 中的 system.web/caching 节点上为 REST 服务提供一个缓存配置
 * 2、在方法上通过类似 [AspNetCacheProfile("Cache30S")] 的声明指定方法所使用的缓存配置
 * 3、在 web.config 中的 system.serviceModel/serviceHostingEnvironment 节点上增加一个属性 aspNetCompatibilityEnabled="true" ，以启用 asp.net 兼容模式
 * 4、在方法上使用如下声明，[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)] ，以启用 asp.net 兼容模式
 */ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;

namespace ServiceLib
{
    [ServiceContract]
    public interface IRestDemo
    {
        [OperationContract]
        [WebGet(UriTemplate = "Hello/{name}", ResponseFormat = WebMessageFormat.Json)]
        // [AspNetCacheProfile("Cache30S")]
        string Hello(string name);
    }

    // [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class RestDemo : IRestDemo
    {
        public string Hello(string name)
        {
            return "Hello: " + name + " - " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}