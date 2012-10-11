using System;
using System.Collections.Generic;
using SOAContracts;

namespace SOASerivces
{
	public class LogServices : ILog
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
				new LogEntity{EventName = "Record",ID = 1,Level = "Error",Time = new DateTime(2011,10,20)}
        	};
			return logEntities;
		}
	}
}
