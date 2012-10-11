using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;
using System.Xml;

namespace RobinLib
{
    public class MyBodyWriter : BodyWriter
    {
        string body = "";
        XmlDocument doc;
        public MyBodyWriter(string body)
            : base(true)
        {
            this.body = body;
        }

        public MyBodyWriter(XmlDocument doc)
            : base(true)
        {
            this.doc = doc;
        }

        protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
        {
            XmlWriterSettings setting = new XmlWriterSettings();
            setting.NewLineHandling = NewLineHandling.Entitize;
            setting.CheckCharacters = false;
            if (!string.IsNullOrEmpty(body))
            {
                writer.WriteRaw(body);
            }
            if (doc != null)
            {
                doc.WriteContentTo(writer);
                writer.Flush();
            }
        }
    }
}
