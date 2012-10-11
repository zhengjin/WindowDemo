using System;
using System.Collections.Generic;
using RESTContracts;

namespace RESTServices
{
	public class LogServices : Ilog
	{
		#region Ilog 成员

		public List<LogEntity> GetAll()
		{
			return GetLogEntityList();
		}

		public List<LogEntity> GetMonthLog(string year, string month)
		{
			List<LogEntity> logEntities = GetLogEntityList();
			List<LogEntity> logList = new List<LogEntity>();
			logEntities.ForEach(log =>
			{
				if (log.Time.Year.ToString() == year && log.Time.Month.ToString() == month)
				{
					logList.Add(log);
				}
			});
			return logList;

		}

		#endregion

		public List<LogEntity> GetLogEntityList()
		{
			List<LogEntity> logEntities = new List<LogEntity>
        	{
        		new LogEntity{EventName = "Record",ID = 1,Level = "Warning",Time = new DateTime(2011,10,25)},
				new LogEntity{EventName = "Record",ID = 1,Level = "Error",Time = new DateTime(2011,10,23)},
				new LogEntity{EventName = "Record",ID = 1,Level = "Warning",Time = new DateTime(2011,8,15)},
				new LogEntity{EventName = "Record",ID = 1,Level = "Error",Time = new DateTime(2011,10,15)},
				new LogEntity{EventName = "Record",ID = 1,Level = "Warning",Time = new DateTime(2011,9,26)},
				new LogEntity{EventName = "Record",ID = 1,Level = "Error",Time = new DateTime(2011,10,20)}
        	};
			return logEntities;
		}
	}
}
