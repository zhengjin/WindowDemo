using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace WCF.ServiceLib.Security
{
    /// <summary>
    /// 自定义的用户名/密码验证类
    /// </summary>
    public class CustomNamePasswordValidator : System.IdentityModel.Selectors.UserNamePasswordValidator
    {
        /// <summary>
        /// 验证指定的用户名和密码
        /// </summary>
        /// <param name="userName">要验证的用户名</param>
        /// <param name="password">要验证的密码</param>
        public override void Validate(string userName, string password)
        {
            if (!(userName == "webabcd" && password == "webabcd"))
            {
                throw new FaultException("用户名或密码不正确");
            }
        }
    }
}
