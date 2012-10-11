using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        [WebGet(UriTemplate = "Sample/Json", ResponseFormat = WebMessageFormat.Json)]
        List<CompositeType> GetJson();

        [OperationContract]
        [WebGet(UriTemplate = "Sample/Xml", ResponseFormat = WebMessageFormat.Xml)]
        List<CompositeType> GetXml();  

        // TODO: 在此添加您的服务操作
    }


    // 使用下面示例中说明的数据约定将复合类型添加到服务操作。
    [DataContract]
    public class CompositeType
    {
        [DataMember]
        public string ID { get; set; }

        [DataMember]
        public string Name { get; set; }


        [DataMember]
        public int Age { get; set; }
    }
}
