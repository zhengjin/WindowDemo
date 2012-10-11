using System;
using System.Web.Security;

using ProjectCore;

public partial class Logon : System.Web.UI.Page 
{
    private UserBLL bllUser;

    protected void Page_Load(object sender, EventArgs e)
    {
        form1.DefaultButton = "rbtnLogin";//提交默认按钮
        if (!IsPostBack)
        {
            rtxtUserName.Focus();
        }
        else
        {
            rtxtPassword.Focus();
        }
    }

    //登陆事件
    protected void rbtnLogin_Click(object sender, EventArgs e)
    {
        string userName;//登陆账号
        string passWord;//登陆密码
        string userId;//用户的GUID

        userName = rtxtUserName.Text.Trim();
        passWord = rtxtPassword.Text.Trim();

        if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(passWord))
        {
            bllUser = new UserBLL();
            userId = bllUser.Verify(userName, passWord);
            if (string.IsNullOrEmpty(userId))
            {
                lblLoginMessage.Visible = true;
                lblLoginMessage.Text =
                    GetGlobalResourceObject("en_US", "Logon_LoginError_Text").ToString();
                return;
            }

            userName = bllUser.GetUserNameById(new Guid(userId));
            if (!string.IsNullOrEmpty(userId))
            {
                bllUser.ResponseCookies(userName);

                FormsAuthentication.RedirectFromLoginPage(userId, false);
            }
        }
        else
        {
            lblLoginMessage.Visible = true;
            lblLoginMessage.Text =
                GetGlobalResourceObject("en_US", "Logon_txtEmpty_Text").ToString();
        }
    }
}
