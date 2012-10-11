using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RobinLib;

namespace Robin_Wcf_SvcLib
{
    // 注意: 如果更改此处的类名“IService1”，也必须更新 App.config 中对“IService1”的引用。
    [ServiceBehavior(IncludeExceptionDetailInFaults=true)]
    public class Service1 : IService1
    { 

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
 
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
