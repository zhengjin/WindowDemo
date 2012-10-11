using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Runtime.Serialization;

namespace WCF.ServiceLib.Exception
{
    /// <summary>
    /// 错误信息实体类（用于错误契约FaultContract）
    /// </summary>
    [DataContract]
    public class FaultMessage
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        [DataMember]
        public int ErrorCode { get; set; }
    }
}
