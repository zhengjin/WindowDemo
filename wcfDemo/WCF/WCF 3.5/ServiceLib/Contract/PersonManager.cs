using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Runtime.Serialization;

namespace WCF.ServiceLib.Contract
{
    /// <summary>
    /// 人员管理类
    /// </summary>
    public class PersonManager : IPersonManager
    {
        /// <summary>
        /// 获取某人的姓名
        /// </summary>
        /// <param name="p">Person对象</param>
        /// <returns></returns>
        public string GetName(Person p)
        {
 	        return "Name: " + p.Name;
        }
    }
}
