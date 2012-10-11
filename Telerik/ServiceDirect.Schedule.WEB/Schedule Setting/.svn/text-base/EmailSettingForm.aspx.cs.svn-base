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

using ServiceDirect.Schedule.DAL;
using ServiceDirect.Schedule.Billing.BLL;

public partial class EmailSettingForm : System.Web.UI.Page 
{
    #region 属性
    /// <summary>
    /// 主键
    /// </summary>
    private string KeyId
    {
        get
        {
            object o = ViewState["KeyId"];
            return o == null ? string.Empty : o.ToString();
        }
        set { ViewState["KeyId"] = value; }
    }
    #endregion

    ScheduleTasksBLL BLL_ScheduleTasks;//声明对象的控制类

    tblScheduler SchedulerObj;

    string successFlag;//返回的插入信息

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //初始化状态
            pnlMessage.Visible = false;
            pnlRight.Visible = false;
            pnlError.Visible = false;

            GetDataReturnFormOtherPageFromCookie();
            if (Request.QueryString["KeyGuid"] != null && Request.QueryString["KeyGuid"] != string.Empty)
            {
                this.KeyId = Request.QueryString["KeyGuid"].ToString();
                GetData();
                FindValue();
            }
        }
    }

    #region 根据ID查询数据
    protected void GetData()
    {
        if (this.KeyId != string.Empty)
        {
            BLL_ScheduleTasks = new ScheduleTasksBLL();
            SchedulerObj = BLL_ScheduleTasks.FindSchedulerById(this.KeyId);
        }
    }
    #endregion

    #region 给控件赋值
    protected void FindValue()
    {
        if (this.KeyId != string.Empty)
        {
            if (SchedulerObj != null)
            {
                rtxtTo.Text = SchedulerObj.EmailTo;   
            }
        }
    }
    #endregion

    #region 取消返回条件选择页面
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (this.KeyId == string.Empty)
        {
            Response.Redirect("~/Schedule Billing/TaskDetailForm.aspx");
        }else
        {
            Response.Redirect("~/Schedule Billing/TaskDetailForm.aspx?KeyGuid=" + this.KeyId);
        }
    }
    #endregion

    #region 确认保存到数据库
    protected void btnOK_Click(object sender, EventArgs e)
    {
        Guid KeyIdGuid;
        if (this.KeyId == string.Empty)
        {
            //将数据保存到客户端cookie
            HttpCookie myCookie = new HttpCookie("EmailSetting");

            myCookie.Values.Add("EmailTo", rtxtTo.Text.Trim());
            myCookie.Expires = System.DateTime.Now.AddDays(1);
            Response.AppendCookie(myCookie);

            Response.Redirect("~/Schedule Billing/TaskDetailForm.aspx");
        }
        else
        {
            KeyIdGuid = new Guid(this.KeyId);//转换成Guid类型
            SchedulerObj = new tblScheduler();
            SchedulerObj.EmailTo = rtxtTo.Text.Trim();
            SchedulerObj.ScheduleID = KeyIdGuid;
            BLL_ScheduleTasks = new ScheduleTasksBLL();
            successFlag = BLL_ScheduleTasks.UpdateInEmailForm(SchedulerObj);
            if (!successFlag.Equals("InsertError"))
            {
                Response.Redirect("~/Schedule Billing/TaskDetailForm.aspx?KeyGuid=" + this.KeyId);
            }
            else
            {
                //错误提示信息
                MessageBox(false, false, true,
                        GetGlobalResourceObject("WebResource", "EmailSettingForm_UpdateEmailSetting_ErrorMessage").ToString());
            }
        }
    }
    #endregion

    #region 从cookie取数据,获取cookie保存的当前页数据
    protected void GetDataReturnFormOtherPageFromCookie()
    {
        HttpCookie TeskDetailData = Request.Cookies.Get("EmailSetting");

        if (TeskDetailData!=null)
        {
            rtxtTo.Text = TeskDetailData.Values["EmailTo"];
        }
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
}
