using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace ServiceLib
{
    [ServiceContract(Namespace = "WCF")]
    public interface IAjaxDemo
    {
        [OperationContract]
        string Hello(string name);
    }

    public class AjaxDemo : IAjaxDemo
    {
        public string Hello(string name)
        {
            return "Hello: " + name;
        }
    }
}
