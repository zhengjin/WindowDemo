using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.IO;

using API = WCF.ServiceLib.Serialization;

public partial class Serialization_Sample : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // 演示DataContractSerializer的序列化和反序列化
        ShowDataContractSerializer();

        // 演示XmlSerializer的序列化和反序列化
        ShowXmlSerializer();

        // 演示SoapFormatter的序列化和反序列化
        ShowSoapFormatter();

        // 演示BinaryFormatter的序列化和反序列化
        ShowBinaryFormatter();

        // 演示DataContractJsonSerializer的序列化和反序列化
        ShowDataContractJsonSerializer();
    }

    /// <summary>
    /// 演示DataContractSerializer的序列化和反序列化
    /// </summary>
    void ShowDataContractSerializer()
    {
        var dataContractSerializerObject = new API.DataContractSerializerObject { ID = Guid.NewGuid(), Name = "DataContractSerializer", Age = 28, Time = DateTime.Now };

        var serializer = new System.Runtime.Serialization.DataContractSerializer(typeof(API.DataContractSerializerObject));

        // 序列化
        var ms = new MemoryStream();

        serializer.WriteObject(ms, dataContractSerializerObject);

        ms.Position = 0;
        var sr = new StreamReader(ms);
        var str = sr.ReadToEnd();
        txtDataContractSerializer.Text = str;


        // 反序列化
        var buffer = System.Text.Encoding.UTF8.GetBytes(str);
        var ms2 = new MemoryStream(buffer);
        var dataContractSerializerObject2 = serializer.ReadObject(ms2) as API.DataContractSerializerObject;
        lblDataContractSerializer.Text = dataContractSerializerObject2.Name;
    }

    /// <summary>
    /// 演示XmlSerializer的序列化和反序列化
    /// </summary>
    void ShowXmlSerializer()
    {
        var xmlSerializerObject = new API.XmlSerializerObject { ID = Guid.NewGuid(), Name = "XmlSerializer", Age = 28, Time = DateTime.Now };

        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(API.XmlSerializerObject));

        // 序列化
        var ms = new MemoryStream();

        serializer.Serialize(ms, xmlSerializerObject);

        ms.Position = 0;
        var sr = new StreamReader(ms);
        var str = sr.ReadToEnd();
        txtXmlSerializer.Text = str;


        // 反序列化
        var buffer = System.Text.Encoding.UTF8.GetBytes(str);
        var ms2 = new MemoryStream(buffer);
        var xmlSerializerObject2 = serializer.Deserialize(ms2) as API.XmlSerializerObject;
        lblXmlSerializer.Text = xmlSerializerObject2.Name;
    }

    /// <summary>
    /// 演示SoapFormatter的序列化和反序列化
    /// </summary>
    void ShowSoapFormatter()
    {
        var soapFormatterOjbect = new API.SoapFormatterOjbect { ID = Guid.NewGuid(), Name = "ShowSoapFormatter", Age = 28, Time = DateTime.Now };

        var formatter = new System.Runtime.Serialization.Formatters.Soap.SoapFormatter();

        // 序列化
        var ms = new MemoryStream();

        formatter.Serialize(ms, soapFormatterOjbect);

        ms.Position = 0;
        var str = System.Text.Encoding.UTF8.GetString(ms.GetBuffer());
        txtSoapFormatter.Text = str;


        // 反序列化
        var buffer = System.Text.Encoding.UTF8.GetBytes(str);
        var ms2 = new MemoryStream(buffer);
        var soapFormatterOjbect2 = formatter.Deserialize(ms2) as API.SoapFormatterOjbect;
        lblSoapFormatter.Text = soapFormatterOjbect2.Name;
    }

    /// <summary>
    /// 演示BinaryFormatter的序列化和反序列化
    /// </summary>
    void ShowBinaryFormatter()
    {
        var binaryFormatterObject = new API.BinaryFormatterObject { ID = Guid.NewGuid(), Name = "BinaryFormatter", Age = 28, Time = DateTime.Now };

        var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

        // 序列化
        var ms = new MemoryStream();

        formatter.Serialize(ms, binaryFormatterObject);

        ms.Position = 0;
        var buffer = ms.GetBuffer();
        var str = System.Text.Encoding.UTF8.GetString(buffer);
        txtBinaryFormatter.Text = str;


        // 反序列化
        var ms2 = new MemoryStream(buffer);
        var binaryFormatterObject2 = formatter.Deserialize(ms2) as API.BinaryFormatterObject;
        lblBinaryFormatter.Text = binaryFormatterObject2.Name;
    }

    /// <summary>
    /// 演示DataContractJsonSerializer的序列化和反序列化
    /// </summary>
    void ShowDataContractJsonSerializer()
    {
        var dataContractJsonSerializerObject = new API.DataContractJsonSerializerObject { ID = Guid.NewGuid(), Name = "DataContractJsonSerializer", Age = 28, Time = DateTime.Now };

        var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(API.DataContractJsonSerializerObject));

        // 序列化
        var ms = new MemoryStream();

        serializer.WriteObject(ms, dataContractJsonSerializerObject);

        ms.Position = 0;
        var sr = new StreamReader(ms);
        var str = sr.ReadToEnd();
        txtDataContractJsonSerializer.Text = str;


        // 反序列化
        var buffer = System.Text.Encoding.UTF8.GetBytes(str);
        var ms2 = new MemoryStream(buffer);
        var dataContractJsonSerializerObject2 = serializer.ReadObject(ms2) as API.DataContractJsonSerializerObject;
        lblDataContractJsonSerializer.Text = dataContractJsonSerializerObject2.Name;
    }
}
