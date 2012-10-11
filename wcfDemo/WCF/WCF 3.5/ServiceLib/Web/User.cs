using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Runtime.Serialization;

namespace WCF.ServiceLib.Web
{
    /// <summary>
    /// User实体类
    /// </summary>
    [DataContract]
    public class User
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [DataMember(Order = 0)]
        public string Name { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [DataMember(Order = 1)]
        public DateTime DayOfbirth { get; set; }
    }

}
