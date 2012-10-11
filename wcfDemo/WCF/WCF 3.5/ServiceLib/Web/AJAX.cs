using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Activation;

namespace WCF.ServiceLib.Web
{
    /// <summary>
    /// 演示AJAX的类
    /// </summary>
    /// <remarks>
    /// ASP.NET 兼容性模型：
    /// 如果在负载平衡或者甚至 Web 园的环境中承载 WCF 服务，并且在该环境中后续的会话请求可以被此环境内的不同宿主或进程处理，则需要对会话状态进行进程外持久存储。最新的 WCF 不支持会话状态的持久存储。相反，WCF 将它的所有会话状态存储在内存中。如果在 IIS 中承载 WCF 服务，最后可以使用回收方案。
    /// WCF 依赖于会话状态的 ASP.NET 实现，而不是为会话全部再次建立持久存储。此方式有一个严重的限制：使服务仅限于 HTTP
    /// ASP.NET 会话状态不是受 ASP.NET 兼容性模式支持的唯一功能。它还支持诸如 HttpContext、globalization 和模拟等功能，就像用于 ASP.NET Web 服务 (ASMX) 一样
    /// </remarks>
    /// [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AJAX : IAJAX
    {
        public User GetUser(string name)
        {
            return new User { Name = name, DayOfbirth = new DateTime(1986, 2, 14) };
        }
    }
}
