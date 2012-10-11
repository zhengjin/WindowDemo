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
using ServiceDirect.Schedule.Billing.JOB;

public partial class TaskDetailFormView : System.Web.UI.Page 
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

    tblScheduler SchedulerObj;

    ScheduleTasksBLL BLL_ScheduleTasks;//声明对象的控制类
    BackupBLL BLL_Backup;//声明对象的控制类
    JOB BLL_JOB;

    string flagSuccess;//创建job是否成功标记
    Boolean updateSuccess;//更新task成功标志

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pnlMessage.Visible = false;
            pnlRight.Visible = false;
            pnlError.Visible = false;

            rcbScheduleType.Enabled = false;
            rdtimepStartTime.Enabled = false;

            RadTimePicker2.Enabled = false;
            chkBDBB.Enabled = false;
            rcbAction.Enabled = false;
            rcbPostAction.Enabled = false;

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
        if (this.KeyId!=string.Empty)
        {
            BLL_ScheduleTasks = new ScheduleTasksBLL();
            SchedulerObj=BLL_ScheduleTasks.FindSchedulerById(this.KeyId);
        }
    }
    #endregion

    #region 给控件赋值
    protected void FindValue()
    {
        //string checkValue;
        //string[] KeyIdChecks;

        if (this.KeyId != string.Empty)
        {
            lblTeskName.Text = SchedulerObj.TaskName.Substring(0, SchedulerObj.TaskName.LastIndexOf("-"));//截取-CreateSD
            if (SchedulerObj.ScheduleType!=null)
            {
                rcbScheduleType.Text = SchedulerObj.ScheduleType.Trim();
            }
            if (SchedulerObj.StartTime.ToString() != null)
            {
                rdtimepStartTime.Text = Convert.ToDateTime(SchedulerObj.StartTime).ToString("MM/dd/yyyy h:mm tt");
            }

            //KeyIdChecks = SchedulerObj.RunOnly.Split('#');
            //if (KeyIdChecks != null)
            //{
            //    for (int i = 0; i < KeyIdChecks.Length; i++)
            //    {
            //        checkValue = KeyIdChecks[i].ToString();
            //        for (int j = 0; j < ckxlROOTD.Items.Count; j++)
            //        {
            //            if (ckxlROOTD.Items[j].Value == checkValue)
            //            {
            //                ckxlROOTD.Items[j].Selected = true;
            //            }
            //        }
            //    }
            //}

            
            if (SchedulerObj.RunOnlyEnd.ToString()!=string.Empty)
            {
                RadTimePicker2.Text = Convert.ToDateTime(SchedulerObj.RunOnlyEnd).ToString("MM/dd/yyyy h:mm tt");
            }
            if (SchedulerObj.Action != null)
            {
                rcbAction.Text = SchedulerObj.Action.Trim();
            }
            if (SchedulerObj.PostAction != null)
            {
                rcbPostAction.Text = SchedulerObj.PostAction.Trim();
            }

            if (SchedulerObj.BackupID.ToString()!=string.Empty)
            {
                BLL_Backup = new BackupBLL();
                chkBDBB.Checked = BLL_Backup.ExistFindBackupById(SchedulerObj.BackupID.ToString());//查看是否有数据备份设置
            }
        }
    }
    #endregion

    #region 编辑数据的处理方法
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Schedule Billing/TaskDetailForm.aspx?KeyGuid=" + this.KeyId);
    }
    #endregion

    #region 确认提交Tast,生成job
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        DateTime timeNow;
        DateTime StartTime;

        timeNow=System.DateTime.Now;
        StartTime=Convert.ToDateTime(rdtimepStartTime.Text);
        long i = StartTime.Ticks - timeNow.Ticks;
        if (StartTime.Ticks - timeNow.Ticks > 0 || StartTime.Ticks - timeNow.Ticks == 0)
        {
            //创建job
            BLL_JOB = new JOB();
            flagSuccess = BLL_JOB.Create(this.KeyId, "Insert");

            if (flagSuccess != string.Empty)//成功更新Task的jobId字段
            {
                GetData();//查询数据
                System.Guid KeyIdGuid = new Guid(flagSuccess);
                SchedulerObj.JobID = KeyIdGuid;
                updateSuccess = BLL_ScheduleTasks.UpdateInTaskView(SchedulerObj);

                if (updateSuccess == true)
                {
                    Response.Redirect("~/Schedule Billing/ScheduleTasksForm.aspx?SuccessFlag=" + updateSuccess);
                }
                else
                {
                    //更新task失败
                    MessageBox(false, false, true,
                            GetGlobalResourceObject("WebResource", "TaskDetailFormView_UpdateTask_ErrorMessage").ToString());
                }
            }
            else
            {
                //生成job失败
                MessageBox(false, false, true,
                            GetGlobalResourceObject("WebResource", "TaskDetailFormView_ConfirmMessage_ErrorMessage").ToString());
            }
        }
        else
        {
            //StartTime小于系统时间，不允许保存
            MessageBox(false, false, true,
                            GetGlobalResourceObject("WebResource", "TaskDetailFormView_StartTime_ErrorMessage").ToString());
        }
    }
    #endregion

    #region 取消按钮的处理方法
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Schedule Billing/ScheduleTasksForm.aspx");
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
