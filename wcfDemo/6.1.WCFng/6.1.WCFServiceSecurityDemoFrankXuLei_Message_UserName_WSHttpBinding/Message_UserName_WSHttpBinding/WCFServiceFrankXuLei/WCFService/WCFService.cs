using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
//ServiceContract 属性以及 Indigo 使用的所有其他属性均在 System.ServiceModel 命名空间中定义，
//因此本例开头使用 using 语句来引用该命名空间。
namespace WCFService
{
    //1.服务契约
    [ServiceContract(Namespace = "http://www.cnblogs.com/frank_xl/")]
    public interface IWCFService
    {
        //操作契约
        [OperationContract]
        string SayHello(string name);

    }
    //2.服务类，继承接口。实现服务契约定义的操作
    public class WCFService : IWCFService
    {
        //实现接口定义的方法
        public string SayHello(string name)
        {
            Console.WriteLine("Hello! {0},Calling at {1} ...", name,DateTime.Now.ToLongTimeString());
            return "Hello! " + name;
        }
    }
}
