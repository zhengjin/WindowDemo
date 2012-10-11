using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace ServiceLib
{
    [ServiceContract]
    public interface IDemo
    {
        [OperationContract]
        string Hello(string name);
    }

    public class Demo : IDemo
    {
        public string Hello(string name)
        {
            return "Hello: " + name;
        }
    }
}
