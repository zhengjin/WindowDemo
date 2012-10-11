using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using XL.Models;

namespace XL.ServiceAPI.Staff
{
    [ServiceContract(SessionMode = SessionMode.Required)]    
    public interface ILogin
    {
        [OperationContract]
        UserModel Login(string UserName,string PassWord);
    }
}
