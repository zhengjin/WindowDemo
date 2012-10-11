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
using System.Text;

public partial class ScheduleTasksForm : System.Web.UI.Page 
{
    #region 属性
    /// <summary>
    /// 排序
    /// </summary>
    private string OrderBy
    {
        get
        {
            object o = ViewState["OrderBy"];
            return o == null ? string.Empty : o.ToString();
        }
        set { ViewState["OrderBy"] = value; }
    }
    #endregion

    string successFlag;//操作数据是否成功的标记

    ObjectQuery<tblScheduler> SchedulerObjList;
    ObjectQuery<vSchedulerTasks> vSysJobsObjs;
    ObjectQuery<vOnlySysJobs> vOnlyJobsObjs;

    List<TasksTemp> TasksTempList;

    TasksTemp TasksTempObj;
    tblScheduler SchedulerObj;

    vSysJobsBLL BLL_vSysJobs;
    ScheduleTasksBLL BLL_ScheduleTasks;
    JOB BLL_JOB;
    vScheduleLogBLL BLL_ScheduleLog;

    int count;//删除影响的job个数
    string SuccessFlag;//新建job成功标记

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            pnlMessage.Visible = false;
            pnlRight.Visible = false;
            pnlError.Visible = false;

            //提示创建job成功
            if (Request.QueryString["SuccessFlag"] != null && Request.QueryString["SuccessFlag"] != string.Empty)
            {
                SuccessFlag = Request.QueryString["SuccessFlag"].ToString();
                if (Convert.ToBoolean(SuccessFlag))
                {
                    MessageBox(true, false, false,
                        GetGlobalResourceObject("WebResource", "ScheduleTasksForm_CreateJob_Message").ToString());
                }
            }
        }
        this.GridViewDataBinding();
    }

    #region GridView绑定数据
    public void GridViewDataBinding()
    {
        Boolean isExtit;
        StringBuilder jobExpetion;
        this.OrderBy = " it.StartTime desc ";
        string whereStr;

        whereStr = "";
        BLL_ScheduleTasks=new ScheduleTasksBLL();
        SchedulerObjList = BLL_ScheduleTasks.GetSchedulers(whereStr, OrderBy);//查询SD的job信息

        BLL_vSysJobs = new vSysJobsBLL();
        if (SchedulerObjList.Count()>0)
        {
            BLL_ScheduleLog = new vScheduleLogBLL();
            TasksTempList = new List<TasksTemp>();

            foreach (tblScheduler item in SchedulerObjList)
            {
                this.OrderBy = " it.name desc";
                whereStr = " and it.name='" + item.TaskName + "'";
                
                vSysJobsObjs = BLL_vSysJobs.GetVSysJobs(whereStr, this.OrderBy);

                if (vSysJobsObjs.Count() > 0)
                {//job存在
                    foreach (vSchedulerTasks Temp in vSysJobsObjs)
                    {
                        TasksTempObj = new TasksTemp();
                        TasksTempObj.ScheduleID = item.ScheduleID;
                        TasksTempObj.TaskName = item.TaskName.Substring(0, item.TaskName.LastIndexOf("-"));//截取-CreateSD;
                        TasksTempObj.ScheduleType = item.ScheduleType;
                        TasksTempObj.Enable=Temp.enabled;
                        if (TasksTempObj.Enable.Equals(1))
                        {//job运行
                            TasksTempObj.StatusImageURL = GetGlobalResourceObject("WebResource", "ScheduleTasksForm_CustomerMessage_Message").ToString();
                            TasksTempObj.Status = "Enable";
                        }
                        else
                        {//job停止运行
                            TasksTempObj.StatusImageURL = GetGlobalResourceObject("WebResource", "ScheduleTasksForm_CustomerMessage_Message").ToString();
                            TasksTempObj.Status = "Disable";
                        }
                        TasksTempObj.Next_run_date = Temp.RunOnlyEnd.ToString();
                        TasksTempObj.Last_run_date = Temp.RunOnlyStart.ToString();

                        TasksTempObj.LogResult = BLL_ScheduleLog.FindJobResultByScheduleId(
                                                                            item.ScheduleID.ToString(), 
                                                                                        item.RunOnlyStart.ToString(), 
                                                                                                            item.RunOnlyEnd.ToString());
                    }
                    TasksTempList.Add(TasksTempObj);
                }
                else
                {//job不存在
                    TasksTempObj = new TasksTemp();
                    TasksTempObj.ScheduleID = item.ScheduleID;
                    TasksTempObj.TaskName = item.TaskName.Substring(0, item.TaskName.LastIndexOf("-"));//截取-CreateSD;;
                    TasksTempObj.ScheduleType = item.ScheduleType;
                    TasksTempObj.StatusImageURL = GetGlobalResourceObject("WebResource", "ScheduleTasksForm_CustomerMessage_Message").ToString();
                    TasksTempObj.Status = "Disable";
                    TasksTempList.Add(TasksTempObj);
                }
            }
        }

        this.OrderBy = " it.name desc";
        whereStr = "";
        vOnlyJobsObjs = BLL_vSysJobs.GetVOnlyJobs(whereStr, this.OrderBy);//查询job的系统信息

        jobExpetion = new StringBuilder();
        foreach (vOnlySysJobs job in vOnlyJobsObjs)
        {
            isExtit=SchedulerObjList.Any(it => it.TaskName == job.name);//在SD中是否存在相应的Taskname
            if (!isExtit)//不存在
            {
                if (job.name.IndexOf("-CreateSD") > 0)//是否SD创建
                {
                    pnlMessage.Visible = true;//显示提示信息
                    //在系统中存在为-CreateSD结尾的job但是在SD数据库中不存在
                    if (jobExpetion.ToString() ==string.Empty )
                    {
                        jobExpetion.Append("Job Excepetion:"+" ");
                        jobExpetion.Append("\""+job.name+"\"");
                    }
                    else
                    {
                        jobExpetion.Append(",\""+job.name+"\"");
                    }
                }
            }
        }
        if (pnlMessage.Visible == true && SuccessFlag == null)//显示异常job信息
        {
            jobExpetion.Append(" ");
            jobExpetion.Append(GetGlobalResourceObject("WebResource", "ScheduleTasksForm_JobExcepeciton_Message").ToString());
            MessageBox(false, true, false,
                        jobExpetion.ToString());
        }
        else
        {
            pnlMessage.Visible = false;
        }

        RadGrid.DataSource = TasksTempList;
    }
    #endregion

    #region GridView删除事件
    protected void RadGrid_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        string strWhere;
        string getScheduleID;//获取的绑定MasterTableView的Guid值

        getScheduleID = (e.Item as GridDataItem).GetDataKeyValue("ScheduleID").ToString();

        BLL_ScheduleTasks = new ScheduleTasksBLL();
        SchedulerObj = BLL_ScheduleTasks.FindSchedulerById(getScheduleID);

        this.OrderBy = " it.name desc ";
        strWhere = " and it.name='" + SchedulerObj.TaskName + "'";
        BLL_vSysJobs = new vSysJobsBLL();
        vSysJobsObjs = BLL_vSysJobs.GetVSysJobs(strWhere, this.OrderBy);

        if (vSysJobsObjs.Count() > 0)
        {
            BLL_JOB = new JOB();
            count = BLL_JOB.DeleteJob(SchedulerObj.JobID.ToString());//删除job

            if (count == 1)//删除作业成功
            {
                successFlag = BLL_ScheduleTasks.LogicDelete(getScheduleID);
                if (!successFlag.Equals("InsertError"))//删除ScheduleTasks内的数据成功
                {
                    GridViewDataBinding();

                    //删除ScheduleTask数据成功
                    MessageBox(true, false, false,
                        GetGlobalResourceObject("WebResource", "ScheduleTasksForm_DeleteTasksMessage_RightMessage").ToString());
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "reload", "<script>window.location=window.self.location;</script>");//强制刷页
                }
                else
                {
                    //删除ScheduleTask数据失败
                    MessageBox(false, false, true,
                        GetGlobalResourceObject("WebResource", "ScheduleTasksForm_DeleteTasksMessage_ErrorMessage").ToString());
                }
            }
            else
            {
                //删除job失败
                MessageBox(false, false, true,
                    GetGlobalResourceObject("WebResource", "ScheduleTasksForm_DeleteJobMessage_ErrorMessage").ToString());
            }
        }
        else
        {
            successFlag = BLL_ScheduleTasks.LogicDelete(getScheduleID);
            if (!successFlag.Equals("InsertError"))//删除ScheduleTasks内的数据成功
            {
                GridViewDataBinding();

                //删除ScheduleTask数据成功
                MessageBox(true, false, false,
                    GetGlobalResourceObject("WebResource", "ScheduleTasksForm_DeleteTasksMessage_RightMessage").ToString());
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "reload", "<script>window.location=window.self.location;</script>");//强制刷页
            }
            else
            {
                //删除ScheduleTask数据失败
                MessageBox(false, false, true,
                    GetGlobalResourceObject("WebResource", "ScheduleTasksForm_DeleteTasksMessage_ErrorMessage").ToString());
            }
        }
    }
    #endregion

    #region GridView更新事件
    protected void RadGrid_UpdateCommand(object sender, GridCommandEventArgs e)
    {
        string getScheduleID;//获取
        //获取Grid的主键值
        getScheduleID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ScheduleID"].ToString();
        if (getScheduleID!=string.Empty)
        {
            ClearCookie();
            Response.Redirect("~/Schedule Billing/TaskDetailFormView.aspx?KeyGuid=" + getScheduleID);
        }
    }
    #endregion

    #region GridView事件分类处理
    protected void RadGrid_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.DeleteCommandName)
        {
            RadGrid_DeleteCommand(sender, e);//删除记录
        }

        if (e.CommandName == RadGrid.EditCommandName)
        {
            RadGrid_UpdateCommand(sender, e);//编辑记录
        }

        if (e.CommandName == RadGrid.UpdateCommandName)
        {
            RadGrid_EnableCommand(sender, e);//修改job属性
        }
        if (e.CommandName == "LogResult")
        {
            string scheduleID;//获取
            //获取Grid的主键值
            scheduleID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ScheduleID"].ToString();
            FindScheduleLog(scheduleID);
        }
    }
    #endregion

    #region 置job是否可以运行-enable/disable属性，以及在job和tblScheduler数据不一致时，以tblScheduler表的数据为准
    protected void RadGrid_EnableCommand(object sender, GridCommandEventArgs e)
    {
        //this.OrderBy = " it.StartTime desc ";
        //string flagSuccess;//修改job是否成功标记

        //vSchedulerTasks vSchedulerObj;

        //string getScheduleID;//获取
        ////获取Grid的主键值
        //getScheduleID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ScheduleID"].ToString();

        //BLL_vSysJobs = new vSysJobsBLL();
        //vSchedulerObj = BLL_vSysJobs.FindTaskSchedulerByTaskId(getScheduleID);

        //if (vSchedulerObj != null)
        //{
        //    //1为运行,0为停止
        //    if (vSchedulerObj.enabled == 1)
        //    {
        //        //修改job为停止
        //        BLL_JOB = new JOB();
        //        flagSuccess = BLL_JOB.Create(vSchedulerObj.ScheduleID.ToString(), "disable");
        //    }
        //    else
        //    {
        //        //修改job为运行
        //        BLL_JOB = new JOB();
        //        flagSuccess = BLL_JOB.Create(vSchedulerObj.ScheduleID.ToString(), "enable");
        //    }

        //    if (flagSuccess != string.Empty)
        //    {
        //        BLL_ScheduleTasks = new ScheduleTasksBLL();
        //        SchedulerObj=BLL_ScheduleTasks.FindSchedulerById(vSchedulerObj.ScheduleID.ToString());
        //        System.Guid KeyIdGuid = new Guid(flagSuccess);
        //        SchedulerObj.JobID = KeyIdGuid;
        //        BLL_ScheduleTasks.UpdateInTaskView(SchedulerObj);
                
        //        //成功绑定数据，刷新页面
        //        GridViewDataBinding();

        //        //提示修改job状态成功
        //        MessageBox(true, false, false,
        //            GetGlobalResourceObject("WebResource", "ScheduleTasksForm_EbleDisableTasksMessage_RightMessage").ToString());
        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "reload", "<script>window.location=window.self.location;</script>");//强制刷页
        //    }
        //    else//修改job失败
        //    {
        //        MessageBox(false, false, true,
        //            GetGlobalResourceObject("WebResource", "ScheduleTasksForm_UpdateFailed_ErrorMessage").ToString());
        //    }
        //}
        //else//Job不存在
        //{
        //    MessageBox(false, false, true, 
        //            GetGlobalResourceObject("WebResource", "ScheduleTasksForm_JobMessage_ErrorMessage").ToString());
        //}
    }
    #endregion

    #region 插入一条记录,如果cookies不为空，进行清空处理
    protected void rbtnInsert_Click(object sender, EventArgs e)
    {
        //清空cookies
        ClearCookie();
        //清除Cookies跳回GridView页面
        Response.Redirect("~/Schedule Billing/TaskDetailForm.aspx");
    }
    #endregion

    #region 格式化系统int32时间类型，Next_run_date、Last_run_date
    protected string FormatDateTime(int Date, int time)
    {
        string dateTime;
        string temp;

        string year="0000";
        string months="00";
        string date="00";

        string hours="00";
        string minutes="00";

        //格式化年月日 （MM/dd/yyyy）
        if (Date>0)
        {
            year=Date.ToString().Substring(0, 4);
            months = Date.ToString().Substring(4, 2);
            date = Date.ToString().Substring(6, 2);
        }
        //格式化化时间  （HH:mm）
        if (time>0)
        {
            if (time.ToString().Length < 6 )//不满足（HH:mm:ss）的用0左前补充
            {
                temp = time.ToString().PadLeft(6, '0');
                hours = temp.ToString().Substring(0, 2);
                minutes = temp.ToString().Substring(2, 2);
            }
            else
            {
                hours = time.ToString().Substring(0, 2);
                minutes = time.ToString().Substring(2, 2);
            }
            
        }
        dateTime = months + "/" + date + "/" + year + " " + hours + ":" + minutes;
        return dateTime;
    }
    #endregion

    #region 显示提示信息
    protected void MessageBox(Boolean correct, Boolean Warning, Boolean incorrect, string message)
    {
        pnlError.Visible = incorrect;//错误提示信息
        pnlMessage.Visible = Warning;//警告
        pnlRight.Visible = correct;//正确

        if (incorrect==true)
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

    #region 清空cookie
    protected void ClearCookie()
    {
        //清空cookies
        HttpCookie BillingCookie;
        HttpCookie TeskDetailCookie;
        HttpCookie EmailSettingCookie;
        HttpCookie backupSettingCookie;

        BillingCookie = Request.Cookies["Billing"];
        if (BillingCookie != null)
        {
            BillingCookie.Expires = DateTime.Now.AddDays(-31);
            Response.Cookies.Add(BillingCookie);
        }

        EmailSettingCookie = Request.Cookies["EmailSetting"];
        if (EmailSettingCookie != null)
        {
            EmailSettingCookie.Expires = DateTime.Now.AddDays(-31);
            Response.Cookies.Add(EmailSettingCookie);
        }

        backupSettingCookie = Request.Cookies["BuckupSetting"];
        if (backupSettingCookie != null)
        {
            backupSettingCookie.Expires = DateTime.Now.AddDays(-31);
            Response.Cookies.Add(backupSettingCookie);
        }

        TeskDetailCookie = Request.Cookies["TeskDetail"];
        if (TeskDetailCookie != null)
        {
            TeskDetailCookie.Expires = DateTime.Now.AddDays(-31);
            Response.Cookies.Add(TeskDetailCookie);
        }
    }
    #endregion

    #region 跳转到Log页面
    protected void FindScheduleLog(string scheduleID)
    {
        Response.Redirect("~/Schedule Billing/ScheduleLogForm.aspx?KeyGuid=" + scheduleID);
    }
    #endregion

    #region 系统设置
    protected void rbtnBackupSetting_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Schedule Setting/BackupSettingForm.aspx");
    }
    #endregion
}
