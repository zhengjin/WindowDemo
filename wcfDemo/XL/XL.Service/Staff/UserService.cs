using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Activation;
using XL.ServiceAPI.Staff;
using XL.Models;
using XL.DataAccess.Staff;

namespace XL.Service.Staff
{
    public class UserService :ServiceBase, IUser
    {
        UserDA DA = new UserDA();
        public UserModel GetOneUser(string UserName,string PassWord)
        {
            var result = DA.GetModel(UserName, PassWord);
            if (result != null)
            {
                HttpContext.Current.Session["IsLogin"] = result;
            }
            return result;
        }
    }
}