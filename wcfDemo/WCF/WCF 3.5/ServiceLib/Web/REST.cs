using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace WCF.ServiceLib.Web
{
    /// <summary>
    /// 演示REST(Representational State Transfer)的类
    /// </summary>
    public class REST : IREST
    {
        public User CreateUser(string name, string dayOfbirth)
        {
            return new User { Name = name, DayOfbirth = DateTime.Parse(dayOfbirth) };
        }

        public User GetUser(string name)
        {
            return new User { Name = name, DayOfbirth = new DateTime(1980, 2, 14) };
        }

        public bool UpdateUser(string name, string dayOfbirth)
        {
            return true;
        }

        public bool DeleteUser(string name)
        {
            return true;
        }
    }
}
