using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ProjectCore;
using ProjectDAL;
using Telerik.Web.UI;

public partial class SetPasswordForm : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// 设置密码事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rbtnSaveUser_Click(object sender, EventArgs e)
    {
        if (Request["UserID"] != null)
        {
            string strUserID = Request.QueryString["UserID"];
            if (!string.IsNullOrEmpty(strUserID))
            {
                tblUser tblUserObj;
                UserBLL UserBLLs = new UserBLL();
                tblUserObj = UserBLLs.GetByID(strUserID);
                if (tblUserObj != null)
                {
                    tblUserObj.LoginPwd = rtxtConfirmPassword.Text;
                    UserBLLs.Update(tblUserObj);
                    ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CancelEdit();", true);
                }
            }
        }
    }
}
