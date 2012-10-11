using System;
using System.ServiceModel;

namespace SOAHost
{
	class Program
	{
		static void Main()
		{
			SOAServiceStart();
		}

		static void SOAServiceStart()
		{
			using (ServiceHost host = new ServiceHost(typeof(SOASerivces.LogServices)))
			{
				Console.Write("SOA服务开启");
				host.Open();
				Console.ReadLine();
			}
		}
	}
}
