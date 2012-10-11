using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace WCF.ServiceLib.Web
{
    /// <summary>
    /// 演示AJAX的接口
    /// </summary>
    [ServiceContract(Namespace = "WCF")]
    public interface IAJAX
    {
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="name">用户名</param>
        /// <returns></returns>
        [OperationContract]
        User GetUser(string name);
    }
}
