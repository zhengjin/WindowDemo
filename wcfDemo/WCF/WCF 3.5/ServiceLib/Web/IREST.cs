using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Web;

namespace WCF.ServiceLib.Web
{
    /// <summary>
    /// 演示REST(Representational State Transfer)的接口
    /// </summary>
    /// <remarks>
    /// HTTP方法中：
    /// PUT相当于Create
    /// GET相当于Read
    /// POST相当于Update
    /// DELETE相当于Delete
    /// </remarks>
    [ServiceContract]
    public interface IREST
    {
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="dayOfbirth">生日</param>
        /// <remarks>
        /// WebInvoke - 指示服务操作在逻辑上就是调用操作，而且可由 Web 编程模型调用
        /// UriTemplate - 用于服务操作的统一资源标识符 (URI) 模板。URI模板可以将一个 URI 或一组 URI 映射到服务操作。有关 URI 模板的更多信息，请参见 UriTemplate 和 UriTemplateTable
        /// Method - 与操作关联的协议方法，默认为 POST
        /// ResponseFormat - 指定从服务操作发出的响应的格式。Xml 或 Json
        /// </remarks>
        [OperationContract]
        [WebInvoke(
            UriTemplate = "User/{name}/{dayOfbirth}",
            Method = "PUT",
            ResponseFormat = WebMessageFormat.Json)]
        User CreateUser(string name, string dayOfbirth);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="name">用户名</param>
        /// <remarks>
        /// WebGet - 指示服务操作在逻辑上就是检索操作，而且可由 Web 编程模型调用
        /// </remarks>
        [OperationContract]
        [WebGet(
            UriTemplate = "User/{name}",
            ResponseFormat = WebMessageFormat.Json)]
        User GetUser(string name);

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="dayOfbirth">生日</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "User/{name}/{dayOfbirth}",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json)]
        bool UpdateUser(string name, string dayOfbirth);

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="name">用户名</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(
            UriTemplate = "User/{name}",
            Method = "DELETE",
            ResponseFormat = WebMessageFormat.Json)]
        bool DeleteUser(string name);
    }
}
