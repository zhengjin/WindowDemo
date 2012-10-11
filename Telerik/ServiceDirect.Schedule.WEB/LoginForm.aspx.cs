using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ServiceDirect.Schedule.DAL;
using ServiceDirect.Schedule.Billing.BLL;
using ServiceDirect.Membership;
using System.Data.Objects;
using ServiceDirect.Membership.DAL;

namespace WEB
{
    /// <summary>
    /// 登录画面-LoginForm.aspx
    /// Param：用户名
    /// Param:口令
    /// 作者：崔扬
    /// 创建日期：2010-12-28
    /// </summary>
    public partial class LoginForm : System.Web.UI.Page
    {
        UTPUser BLL_UTPUser;

        protected void lgiSystem_Authenticate(object sender, AuthenticateEventArgs e)
        {
            vUTPUserWithRole objUTPUser;
            string userName;//用户名
            string passWord;//密码
            string strRightCode;//角色代码
            string userId;//用户ID
            Boolean LoginSuccess;
            string tempMD5;

            userName=lgiSystem.UserName;
            passWord=lgiSystem.Password;
            if (userName != string.Empty && passWord!=string.Empty)
            {
                BLL_UTPUser = new UTPUser();

                objUTPUser = BLL_UTPUser.GetUTPUserBLL(userName);
                userId = objUTPUser.UserID;
                //roleId = objUTPUser.RoleID;//"586487590170100231" 
                strRightCode = "Schedule Billing (SD)";//"586487590170100231" 
                tempMD5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(userId + passWord, "MD5");

                LoginSuccess = BLL_UTPUser.UTPUserAuthenticate(userId, tempMD5, strRightCode);

                if (LoginSuccess)
                {
                    e.Authenticated = true;//验证通过
                    Server.Transfer("~/Schedule Billing/ScheduleTasksForm.aspx");
                }
                else
                {
                    e.Authenticated = false;
                }
            }
            else
            {
                e.Authenticated = false;
            }
        }

        protected void labVersion_Load(object sender, EventArgs e)
        {
            labVersion.Text = "RC Ver." + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}