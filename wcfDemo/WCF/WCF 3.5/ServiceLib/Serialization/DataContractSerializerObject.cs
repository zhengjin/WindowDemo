using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace WCF.ServiceLib.Serialization
{
    /// <summary>
    /// 用于演示DataContract序列化的实体类
    /// </summary>
    // Namespace - 数据契约的命名空间
    [DataContract(Namespace = "http://webabcd.cnblogs.com/")]
    public class DataContractSerializerObject
    {
        // Name - 数据成员的名称
        // Order - 数据成员的序列化和反序列化的顺序
        [DataMember(Name = "UniqueID", Order = 0)]
        public Guid ID { get; set; }

        [DataMember(Order = 1)]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        public int Age { get; set; }

        [DataMember(Order = 3)]
        public DateTime Time { get; set; }
    }
}
