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

using ServiceDirect.Schedule.Billing;
using ServiceDirect.Schedule.Billing.BLL;
using ServiceDirect.Schedule.DAL;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using ServiceDirect.Schedule.Billing.JOB;

public partial class ScheduleLogForm : System.Web.UI.Page 
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
    vScheduleLogBLL BLL_ScheduleLog;
    ScheduleTasksBLL BLL_ScheduleTasks;

    protected void Page_Load(object sender, EventArgs e)
    {
        vUTPScheduleMessager vScheduleLogObj;
        tblScheduler SchedulerObj;

        if (!IsPostBack)
        {
            if (Request.QueryString["KeyGuid"] != null && Request.QueryString["KeyGuid"] != string.Empty)
            {
                KeyId = Request.QueryString["KeyGuid"].ToString();
                BLL_ScheduleLog = new vScheduleLogBLL();
                //显示UserName信息
                vScheduleLogObj = BLL_ScheduleLog.FindUserNameByScheduleId(KeyId);
                if (vScheduleLogObj!=null)
                {
                    lbl_UserName.Text = vScheduleLogObj.UserName;
                }
                //显示TaskName
                BLL_ScheduleTasks = new ScheduleTasksBLL();
                SchedulerObj = BLL_ScheduleTasks.FindSchedulerById(KeyId);
                lbl_TaskName.Text = SchedulerObj.TaskName.Substring(0, SchedulerObj.TaskName.LastIndexOf("-"));
            }
        }
        GridViewDataBinding(KeyId);
    }

    #region GridView绑定数据
    public void GridViewDataBinding(string shceduleId)
    {
        if (shceduleId == string.Empty)
        {
            string OrderBy = " it.GDT desc ";
            string whereStr;

            ObjectQuery<vUTPScheduleMessager> vScheduleLogObjs;

            whereStr = "";
            BLL_ScheduleLog = new vScheduleLogBLL();
            vScheduleLogObjs = BLL_ScheduleLog.FindJobResult(whereStr, OrderBy);

            RadGrid.DataSource = vScheduleLogObjs;
        }
        else
        {
            string OrderBy = " it.GDT desc ";
            string whereStr;

            ObjectQuery<vUTPScheduleMessager> vScheduleLogObjs;

            whereStr = " and it.SID='" + shceduleId + "'";
            BLL_ScheduleLog = new vScheduleLogBLL();
            vScheduleLogObjs = BLL_ScheduleLog.FindJobResult(whereStr, OrderBy);

            RadGrid.DataSource = vScheduleLogObjs;
        }
    }
    #endregion

    protected void rbtn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Schedule Billing/ScheduleTasksForm.aspx");
    }
}
