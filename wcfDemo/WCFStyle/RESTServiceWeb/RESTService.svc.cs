using System.Collections.Generic;

using RESTContracts;
using RESTServices;

namespace RESTServiceWeb
{
	public class RESTService
	{
		public List<LogEntity> GetAll()
		{
			LogServices services = new LogServices();
			return services.GetAll();
		}

		public List<LogEntity> GetMonthLog(string year, string month)
		{
			LogServices services = new LogServices();
			return services.GetMonthLog(year, month);
		}
	}
}
