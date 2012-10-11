using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.ServiceModel;

namespace RESTContracts
{
	[ServiceContract]
	public interface Ilog
	{
		[OperationContract]
		[WebGet(UriTemplate = "/")]
		List<LogEntity> GetAll();

		[OperationContract]
		[WebGet(UriTemplate = "Get/{year}/{month}")]
		List<LogEntity> GetMonthLog(string year, string month);

	}
}
