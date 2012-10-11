using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Data.Objects;
using System.Linq;

using ServiceDirect.Schedule.DAL;
using ServiceDirect.Schedule.Billing.BLL;
using ServiceDirect.Schedule.Billing.JOB;

public partial class BackupSettingForm : System.Web.UI.Page 
{
    #region 属性
    /// <summary>
    /// BackupSetting主键
    /// </summary>
    private string TempKeyId
    {
        get
        {
            object o = ViewState["TempKeyId"];
            return o == null ? string.Empty : o.ToString();
        }
        set { ViewState["TempKeyId"] = value; }
    }
    #endregion

    ScheduleTasksBLL BLL_ScheduleTasks;//声明对象的控制类
    BackupBLL BLL_Backup;//声明Backup的控制类
    EmailBLL BLL_Email;//声明Email的控制类
    Email Job_Email;

    ObjectQuery<vBackupAndEmailSetting> vBackupAndEmailSettingObj;

    tblBackup BackupObj;//声明对象
    tblEmail EmailObj;//声明对象

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            //初始化状态
            pnlMessage.Visible = false;
            pnlRight.Visible = false;
            pnlError.Visible = false;
            txtBackupFolder.Text = "D:\\BackupFolder";
            rntxtPort.Text = "25";//默认邮件端口号

            GetDataReturnFormOtherPageFromCookie();

            GetData();
            FindValue();
        }
    }

    #region 根据ID查询数据
    protected void GetData()
    {
        string whereStr;
        string orderBy;

        whereStr = "";
        orderBy = "it.EmailFrom desc";
        BLL_ScheduleTasks = new ScheduleTasksBLL();
        vBackupAndEmailSettingObj = BLL_ScheduleTasks.GetBackupAndEmailSetting(whereStr, orderBy);
        if (vBackupAndEmailSettingObj.Count()>0)
        {
            foreach (var item in vBackupAndEmailSettingObj)
            {
                this.TempKeyId = item.BackupID.ToString();
            }
        }
    }
    #endregion

    #region 给控件赋值
    protected void FindValue()
    {
        if (this.TempKeyId != null)
        {
            foreach (var item in vBackupAndEmailSettingObj)
            {
                txtServer.Text = item.BackupServer;
                txtDatabase.Text = item.BackupDatabase;
                txtBackupFolder.Text = item.BackupFloder;

                rtxtServerName.Text = item.SMTPHost;
                rtxtFrom.Text=item.EmailFrom;
                rntxtPort.Text=item.EmailPort.ToString();
                rtxtName.Text=item.SMTPUsername;
                rtxtPwd.Text=item.SMTPPassword;
                ckbSSL.Checked = Convert.ToBoolean(item.EmailSSL);
            }
        }
    }
    #endregion

    #region 封装数据
    public void EncapsulationData()
    {
        EmailObj = new tblEmail();
        BackupObj = new tblBackup();

        BackupObj.BackupFloder = txtBackupFolder.Text.Trim();
        BackupObj.BackupDatabase = txtDatabase.Text.Trim();
        BackupObj.BackupServer = txtServer.Text.Trim();
        BackupObj.BackupID = System.Guid.NewGuid();

        EmailObj.EmailID = BackupObj.BackupID;
        EmailObj.SMTPHost = rtxtServerName.Text.Trim();
        EmailObj.EmailFrom = rtxtFrom.Text.Trim();
        EmailObj.EmailPort = Convert.ToInt16(rntxtPort.Text.Trim());
        EmailObj.SMTPUsername = rtxtName.Text.Trim();
        EmailObj.SMTPPassword = rtxtPwd.Text.Trim();
        EmailObj.EmailSSL = ckbSSL.Checked;

        HttpCookie UserData = Request.Cookies.Get("UserIdCookies");
        if (UserData != null)
        {
            BackupObj.BackupUser = UserData.Value;
        }
        HttpCookie UserPasswordData = Request.Cookies.Get("UserPasswordCookies");
        if (UserPasswordData != null)
        {
            BackupObj.BackupPassword = UserPasswordData.Value;
        }
    }
    #endregion

    #region 确认保存到数据库
    protected void btnOK_Click(object sender, EventArgs e)
    {
        string backupFlag;//backup保存返回的插入信息
        string emailFlag;//backup保存返回的插入信息

        System.Guid KeyIdGuid;

        this.EncapsulationData();

        if (this.TempKeyId == string.Empty)//插入
        {
            //插入Backup数据
            BLL_Backup = new BackupBLL();
            backupFlag = BLL_Backup.Insert(BackupObj);
            if (!backupFlag.Equals("InsertError"))
            {
                //将数据保存到客户端cookie
                HttpCookie myCookie = new HttpCookie("BuckupSetting");

                myCookie.Values.Add("BackupId", backupFlag);
                myCookie.Expires = System.DateTime.Now.AddDays(1);
                Response.AppendCookie(myCookie);
            }
            else
            {
                //错误提示信息
                MessageBox(false, false, true,
                        GetGlobalResourceObject("WebResource", "BackupSettingForm_SaveBackupMessage_ErrorMessage").ToString());
            }

            //插入Email数据
            BLL_Email = new EmailBLL();
            emailFlag = BLL_Email.Insert(EmailObj);
            if (!emailFlag.Equals("InsertError"))
            {
                //将数据保存到客户端cookie
                HttpCookie myCookie = new HttpCookie("EmailSetting");

                myCookie.Values.Add("EmailId", emailFlag);
                myCookie.Expires = System.DateTime.Now.AddDays(1);
                Response.AppendCookie(myCookie);
            }
            else
            {
                //错误提示信息
                MessageBox(false, false, true,
                        GetGlobalResourceObject("WebResource", "EmailSettingForm_SaveEmailSetting_ErrorMessage").ToString());
            }

            if (!emailFlag.Equals("InsertError") && !backupFlag.Equals("InsertError"))
            {//backup和Email的保存都成功
                Job_Email = new Email();
                Job_Email.CreateEmail(emailFlag);
                Response.Redirect("~/Schedule Billing/ScheduleTasksForm.aspx");
            }
        }
        else//编辑
        {
            KeyIdGuid = new Guid(this.TempKeyId);//转换成Guid类型
            BLL_Backup = new BackupBLL();

            BackupObj.BackupID = KeyIdGuid;
            backupFlag = BLL_Backup.Update(BackupObj);//更新错误，则进行数据插入
            if (!backupFlag.Equals("InsertError"))
            {
                //将数据保存到客户端cookie
                HttpCookie myCookie = new HttpCookie("BuckupSetting");

                myCookie.Values.Add("BackupId", this.TempKeyId);
                myCookie.Expires = System.DateTime.Now.AddDays(1);
                Response.AppendCookie(myCookie);
            }
            else
            {
                //错误提示信息
                MessageBox(false, false, true,
                        GetGlobalResourceObject("WebResource", "BackupSettingForm_UpdateBackupMessage_ErrorMessage").ToString());
            }

            BLL_Email = new EmailBLL();
            EmailObj.EmailID = KeyIdGuid;
            emailFlag = BLL_Email.Update(EmailObj);
            if (!emailFlag.Equals("InsertError"))
            {
                //将数据保存到客户端cookie
                HttpCookie myCookie = new HttpCookie("EmailSetting");

                myCookie.Values.Add("EmailId", this.TempKeyId);
                myCookie.Expires = System.DateTime.Now.AddDays(1);
                Response.AppendCookie(myCookie);
            }
            else
            {
                //错误提示信息
                MessageBox(false, false, true,
                        GetGlobalResourceObject("WebResource", "EmailSettingForm_UpdateEmailSetting_ErrorMessage").ToString());
            }

            if (!emailFlag.Equals("InsertError") && !backupFlag.Equals("InsertError"))
            {//backup和Email的保存都成功
                Job_Email = new Email();
                Job_Email.CreateEmail(KeyIdGuid.ToString());
                Response.Redirect("~/Schedule Billing/ScheduleTasksForm.aspx");
            }
        }
    }
    #endregion

    #region 取消返回条件选择页面
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Schedule Billing/ScheduleTasksForm.aspx");
    }
    #endregion

    #region 从cookie取数据,获取cookie保存的当前页数据
    protected void GetDataReturnFormOtherPageFromCookie()
    {
        GetData();
        FindValue();
    }
    #endregion

    #region 显示提示信息
    protected void MessageBox(Boolean correct, Boolean Warning, Boolean incorrect, string message)
    {
        pnlError.Visible = incorrect;//错误提示信息
        pnlMessage.Visible = Warning;//警告
        pnlRight.Visible = correct;//正确

        if (incorrect == true)
        {
            lnkError.Text = message;//显示错误信息
        }

        if (correct == true)
        {
            lnkRight.Text = message;//显示正确信息
        }

        if (Warning == true)
        {
            lnkMessage.Text = message;//显示提示信息
        }
    }
    #endregion

    #region 修改端口默认值
    protected void ckbSSL_CheckedChanged(object sender, EventArgs e)
    {
        if (ckbSSL.Checked == true)
        {
            rntxtPort.Text = "465";
        }
        else
        {
            rntxtPort.Text = "25";
        }
    }
    #endregion
}
