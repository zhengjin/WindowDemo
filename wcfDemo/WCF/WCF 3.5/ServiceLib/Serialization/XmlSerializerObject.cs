using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WCF.ServiceLib.Serialization
{
    /// <summary>
    /// 用于演示XML序列化的实体类
    /// </summary>
    // Namespace - XML 根元素的命名空间
    [XmlRoot(Namespace = "http://webabcd.cnblogs.com/")]
    public class XmlSerializerObject
    {
        // ElementName - 生成的 XML 元素的名称
        // Order - 序列化和反序列化的顺序
        [XmlElement(ElementName = "UniqueID", Order = 0)]
        public Guid ID { get; set; }

        [XmlElement(Order = 1)]
        public string Name { get; set; }

        [XmlElement(Order = 2)]
        public int Age { get; set; }

        [XmlElement(Order = 3)]
        public DateTime Time { get; set; }
    }
}
