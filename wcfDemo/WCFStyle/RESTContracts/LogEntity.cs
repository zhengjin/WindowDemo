using System;
using System.Runtime.Serialization;

namespace RESTContracts
{
	[DataContract]
	public class LogEntity
	{
		[DataMember]
		public int ID { get; set; }

		[DataMember]
		public string EventName { get; set; }

		[DataMember]
		public string Level { get; set; }

		[DataMember]
		public DateTime Time { get; set; }

	}
}
