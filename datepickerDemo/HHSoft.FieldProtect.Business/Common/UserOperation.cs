using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.Business.SysManage;
using HHSoft.FieldProtect.DataEntity.SysManage;

namespace HHSoft.FieldProtect.Business.Common
{
    public class UserOperation
    {
        public string GetFullName(string userId)
        {
            BusiUserService user = new BusiUserService();
            StringBuilder sbJsr = new StringBuilder();
            if (userId.Equals("0"))
            {
                sbJsr.Append("系统管理员");
            }
            else
            {
                sbJsr.Append(user.CompanyName(userId) + " - " + user.DeptName(userId) + " - " + user.RealName(userId));
            }
            return sbJsr.ToString();
        }

        public string GetCurrentUserDepartmentFullName(LoginUser loginUser)
        {
            return loginUser.CompanyName + loginUser.DepartName;
        }
    }
}
