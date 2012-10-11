using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.ServiceModel;
using SOAContracts;

namespace ClientConsole
{
	class Program
	{
		static void Main()
		{
			Console.WriteLine("调用SOA 服务....");
			InvoleSOAService();
			Console.WriteLine();
			Console.WriteLine("调用REST服务....");
			InvokeRESTService();
			Console.ReadLine();
		}

		static  void InvoleSOAService()
		{
			using (ChannelFactory<ILog> factory = new ChannelFactory<ILog>("SOAService"))
			{
				ILog log = factory.CreateChannel();
				List<LogEntity> listAll = log.GetAll();
				Console.WriteLine(string.Format("SOA Service中 GetAll 方法获取到日志记录有{0}条", listAll.Count));
				Console.WriteLine();
				const string year = "2011";
				const string month = "10";
				List<LogEntity> list = log.GetMonthLog(year, month);
				Console.WriteLine(string.Format("SOA Service中 GetMonthLog 方法获取到{0}年{1}月日志记录有{2}条", year, month, list.Count));			
			}			
		}

		static void InvokeRESTService()
		{
			HttpWebRequest request = WebRequest.Create("http://172.17.2.178:8081/RESTService.svc/") as HttpWebRequest;
			request.Method = "GET";
			HttpWebResponse response = request.GetResponse() as HttpWebResponse;
			using (StreamReader reader=new StreamReader(response.GetResponseStream()))
			{
				if (response.StatusCode==HttpStatusCode.OK)
				{
					Console.WriteLine(string.Format("REST Service 中GetAll 方法读取到的数据为：{0}", reader.ReadToEnd()));
				}
			}

		}

	}
}
