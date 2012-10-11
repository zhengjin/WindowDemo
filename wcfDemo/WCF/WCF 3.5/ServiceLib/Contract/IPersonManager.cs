using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Runtime.Serialization;

namespace WCF.ServiceLib.Contract
{
    /// <summary>
    /// 人员管理接口
    /// </summary>
    // Namespace - 服务契约的命名空间
    // Name - 服务契约的名称（会对应到相关的wsdl，默认情况下本例为接口名“IPersonManager”）
    // ConfigurationName - 服务契约在宿主中所配置的服务名称（默认情况下本例为类的全名“WCF.ServiceLib.Contract.IPersonManager”）
    [ServiceContract(Namespace = "http://webabcd.cnblogs.com", Name = "IPersonManager", ConfigurationName = "ConfigurationNameTest")]
    // 服务已知类型 - Student（数据契约）继承自Person（数据契约），要指定Student为已知类型，其才会被序列化
    [ServiceKnownType(typeof(Student))]
    public interface IPersonManager
    {
        /// <summary>
        /// 获取某人的姓名
        /// </summary>
        /// <param name="p">Person对象</param>
        /// <returns></returns>
        // Name - 操作契约的名称（会对应到相关的wsdl，默认情况下本例为方法名“GetName”）
        [OperationContract(Name="GetPersonName")]
        string GetName([MessageParameter(Name = "person")] Person p);
    }
}
