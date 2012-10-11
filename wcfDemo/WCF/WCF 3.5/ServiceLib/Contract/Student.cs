using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Runtime.Serialization;

namespace WCF.ServiceLib.Contract
{
    /// <summary>
    /// Student的实体类
    /// </summary>
    // Name - 数据契约的名称（会对应到相关的wsdl，默认情况下本例为类名“Student”）
    [DataContract(Name = "StudentModel")]
    public class Student : Person
    {
        /// <summary>
        /// Student的实体类的School属性
        /// </summary>
        // Name - 数据成员的名称（会对应到相关的wsdl，默认情况下本例为属性名“School”）
        // IsRequired - 该值指示序列化引擎该成员在读取或反序列化时必须存在
        // Order - 数据成员在相关的wsdl中的顺序
        // EmitDefaultValue - 如果应该在序列化流中生成成员的默认值，则为 true，否则为 false，默认值为 true
        [DataMember(Name = "School", IsRequired = false, Order = 0)]
        public string School { get; set; }
    }
}
