using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Activation;
using XL.ServiceAPI.Staff;
using XL.Models;
using XL.DataAccess.Staff;
using System.ServiceModel;

namespace XL.Service.Staff
{
    public class LoginService :ServiceBase, ILogin
    {
        UserDA DA = new UserDA();

        protected override void CheckLogin()
        {

        }

        public UserModel Login(string UserName,string PassWord)
        {            
            var result = DA.GetModel(UserName, PassWord);
            if (result != null)
            {
                CacheStrategy.AddObject(result.Id, result);
            }
            return result;
        }
    }
}