using System.Collections.Generic;
using System.ServiceModel;


namespace SOAContracts
{

	[ServiceContract]
	public interface ILog
	{
		[OperationContract]
		List<LogEntity> GetAll();

		[OperationContract]
		List<LogEntity> GetMonthLog(string year, string month);

	}
}
