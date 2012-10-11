using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robin_Wcf_ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(3300);
            Robin_Wcf_Formatter_Svc.IService1 svc = new Robin_Wcf_Formatter_Svc.Service1Client();
            string res =  svc.GetData(10);
            Console.WriteLine(res);
            Robin_Wcf_Formatter_Svc.CompositeType ct = new Robin_Wcf_ClientApp.Robin_Wcf_Formatter_Svc.CompositeType();
            ct.BoolValue = false;
            ct.StringValue = "RobinZhang";
            Robin_Wcf_Formatter_Svc.CompositeType returnObj =  svc.GetDataUsingDataContract(ct);
            Console.WriteLine(returnObj.StringValue+","+returnObj.BoolValue);
            Console.Read();
        }
    }
}
