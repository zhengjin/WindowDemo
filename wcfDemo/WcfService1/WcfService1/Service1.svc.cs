using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    //在使用地址后面加上sample/xml或者sample/json访问
    //e.g) http://localhost:1733/Service1.svc/sample/xml
    public class Service1 : IService1
    {

        public List<CompositeType> GetJson()
        {
            return BuildData();  
        }

        public List<CompositeType> GetXml()
        {
            return BuildData();  
        }
        private static List<CompositeType> BuildData()
        {
            return new List<CompositeType>  
            {  
                new CompositeType { ID=Guid.NewGuid().ToString(), Name="alex", Age=24 },                 
                new CompositeType { ID=Guid.NewGuid().ToString(), Name="ryan", Age=24 },                 
                new CompositeType { ID=Guid.NewGuid().ToString(), Name="kail", Age=24 },                
                new CompositeType { ID=Guid.NewGuid().ToString(), Name="ulee", Age=24 }
            };
        }  
    }
}
