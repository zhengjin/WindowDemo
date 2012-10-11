using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace HHSoft.FieldProtect.FrameWork.Utility
{
    public class SerializeOperation
    {
        public static string Serialize(object item)
        {
            Type type = item.GetType();
            XmlSerializer xs = new XmlSerializer(type);
            MemoryStream ms = new MemoryStream();
            xs.Serialize(ms, item);
            return Encoding.UTF8.GetString(ms.ToArray());
        }

        public static T Deserialize<T>(string content)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            return (T)xs.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(content)));
        }
    }
}
