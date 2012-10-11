using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WCF.ServiceLib.Serialization
{
    /// <summary>
    /// 用于演示Binary序列化的实体类
    /// </summary>
    [Serializable]
    public class BinaryFormatterObject
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Time { get; set; }
    }
}
