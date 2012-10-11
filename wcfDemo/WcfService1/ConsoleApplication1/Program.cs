using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new WebClient();
            string str = client.DownloadString("http://localhost:1733/Service1.svc/sample/json");
            Console.WriteLine("XML：");
            Console.WriteLine(str);
            str = client.DownloadString("http://localhost:1733/Service1.svc/sample/xml");
            Console.WriteLine("Json：");
            Console.WriteLine(str);
        }
    }
}
