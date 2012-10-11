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
using System.Data.Objects;
using System.Linq;
using System.Collections.Generic;

public partial class TaskDetailForm : System.Web.UI.Page 
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
    BackupBLL BLL_Backup;//声明对象的控制类

    tblScheduler SchedulerObj;//声明对象
    ObjectQuery<vBackupAndEmailSetting> vBackupAndEmailSettingObj;

    string successFlag;//操作数据是否成功的标记

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            //ckxlROOTD.Enabled = false;//默认CheckBoxList为不可用
            rcbAction.Enabled = false;
            rcbPostAction.Enabled = false;
            pnlMessage.Visible = false;
            pnlRight.Visible = false;
            pnlError.Visible = false;

            chkBDBB.Checked = true;

            if (Request.QueryString["KeyGuid"] != null && Request.QueryString["KeyGuid"] != string.Empty)//编辑数据
            {
                rtxtTaskName.Enabled = false;

                this.KeyId = Request.QueryString["KeyGuid"].ToString();
                GetData();//根据Guid查询数据
                FindValue();//给控件赋值
                GetDataReturnFormOtherPageFromCookie();//从cookies读取数据
            }
            else
            {
                rdStartTime.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy")).AddHours(18);//开始时间
                rdEndTime.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy")).AddHours(21);//结束时间
                GetDataReturnFormOtherPageFromCookie();//从cookies读取数据
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

    #region 确认保存到数据库
    protected void btnOK_Click(object sender, EventArgs e)
    {
        System.Guid KeyIdGuid;
        Boolean flagCookie=false;
        Boolean flagTaskName=false;
        Boolean flagQequest;
        Boolean flagRunTime=false;

        if (this.KeyId == string.Empty)
        {
            flagQequest=CheckRequired(sender, e);//校验必填字段是否不为空
            if (flagQequest==true)
            {
                flagCookie = CheckCookiesInfo(sender, e);//校验Cookies是否为空
                if (flagCookie==true)
                {
                    flagTaskName = rbtnCheck_Click(sender, e);//判断Tast名是否重复
                    if (flagTaskName==true)
                    {
                        flagRunTime = JudgeRunTime(rdStartTime.SelectedDate.ToString(), rdEndTime.SelectedDate.ToString());
                    }
                }
            }

            if (flagCookie == true && flagTaskName == true && flagQequest == true && flagRunTime==true)
            {
                //新增
                EncapsulationData();//封装数据

                GetLoginUserInfo();//获取当前登陆的账号和密码

                BLL_ScheduleTasks = new ScheduleTasksBLL();
                successFlag = BLL_ScheduleTasks.Insert(SchedulerObj);
                if (!successFlag.Equals("InsertError"))
                {
                    PageSet(successFlag);//跳View
                }
                else
                {
                    //错误提示信息
                    MessageBox(false, false, true,
                        GetGlobalResourceObject("WebResource", "TaskDetailForm_SaveTasksMessage_ErrorMessage").ToString());
                }
            }
        }
        else//编辑更新
        {
            //billing前不进行备份
            if (chkBDBB.Checked == false)
            {
                flagQequest = CheckRequired(sender, e);//校验必填字段是否为空
                if (flagQequest == true)
                {
                    flagTaskName = rbtnCheck_Click(sender, e);//判断Tast名是否重复
                    if (flagTaskName == true)
                    {
                        flagRunTime = JudgeRunTime(rdStartTime.SelectedDate.ToString(), rdEndTime.SelectedDate.ToString());
                    }
                }
                if (flagTaskName == true && flagQequest == true && flagRunTime == true)
                {
                    EncapsulationData();//封装数据
                    GetLoginUserInfo();//获取当前登陆的账号和密码

                    SchedulerObj.BackupID = null;
                    KeyIdGuid = new Guid(this.KeyId);//转换成Guid类型
                    SchedulerObj.ScheduleID = KeyIdGuid;
                    successFlag = BLL_ScheduleTasks.UpdateInTaskDetailForm(SchedulerObj);
                    if (!successFlag.Equals("InsertError"))
                    {
                        PageSet(this.KeyId);//跳View
                    }
                    else
                    {
                        //错误提示信息
                        MessageBox(false, false, true,
                            GetGlobalResourceObject("WebResource", "TaskDetailForm_UpdateTasksMessage_ErrorMessage").ToString());
                    }
                }
            }
            else
            {//billing前进行备份
                flagQequest = CheckRequired(sender, e);//校验必填字段是否为空
                if (flagQequest == true)
                {
                    flagTaskName = rbtnCheck_Click(sender, e);//判断Tast名是否重复
                    if (flagTaskName == true)
                    {
                        flagCookie = JudgeBackupAndEmailSetting();
                        if (flagCookie == true)//Cookie中是否有值
                        {
                            flagRunTime = JudgeRunTime(rdStartTime.SelectedDate.ToString(), rdEndTime.SelectedDate.ToString());//新的Tast的StartTime、EndTime不在其他的RunOnlyStart和RunOnlyEnd之间
                        }
                    }
                }
                if (flagTaskName == true && flagQequest == true && flagRunTime == true && flagCookie == true)
                {
                    EncapsulationData();//封装数据
                    GetLoginUserInfo();//获取当前登陆的账号和密码

                    if (chkBDBB.Checked == true)//系统备份
                    {
                        KeyIdGuid = new Guid(this.KeyId);//转换成Guid类型
                        SchedulerObj.ScheduleID = KeyIdGuid;
                        successFlag = BLL_ScheduleTasks.UpdateInTaskDetailForm(SchedulerObj);
                    }
                    if (!successFlag.Equals("InsertError"))
                    {
                        PageSet(this.KeyId);//跳View
                    }
                    else
                    {
                        //错误提示信息
                        MessageBox(false, false, true,
                            GetGlobalResourceObject("WebResource", "TaskDetailForm_UpdateTasksMessage_ErrorMessage").ToString());
                    }
                }
                else
                {
                    if (flagCookie==false)
                    {
                        MessageBox(false, true, false,
                        GetGlobalResourceObject("WebResource", "TaskDetailForm_CookiesBackup_ErrorMessage").ToString());
                    }
                }
            }
        }
    }
    #endregion

    #region 获得当前登陆用户的登陆账号和密码
    protected void GetLoginUserInfo()
    {
        HttpCookie UserData = Request.Cookies.Get("UserIdCookies");
        if (UserData != null)
        {
            SchedulerObj.UTPUser = UserData.Value;
        }
        HttpCookie UserPasswordData = Request.Cookies.Get("UserPasswordCookies");
        if (UserPasswordData != null)
        {
            SchedulerObj.UTPPwd = UserPasswordData.Value;
        }
    }
    #endregion

    #region 取消返回View页面
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (this.KeyId!=string.Empty)
        {
            Response.Redirect("~/Schedule Billing/TaskDetailFormView.aspx?KeyGuid=" + this.KeyId);//返回到View页面
        }else
        {
            Response.Redirect("~/Schedule Billing/ScheduleTasksForm.aspx");//返回到GridView页面
        }
    }
    #endregion

    #region 封装数据
    public void EncapsulationData()
    {
        //string selectDate;//选择的时间
        string whereStr;
        string orderBy;

        if (this.KeyId == string.Empty)//Tasks的billing相关信息，在billing页面完成
        {
            GetDataFromCookie();//从cookies读取数据
        }
        else
        {
            SchedulerObj = new tblScheduler();
            //HttpCookie BuckupSettingData = Request.Cookies.Get("BuckupSetting");
            //if (BuckupSettingData != null)
            //{
            //    System.Guid KeyIdGuid = new Guid(BuckupSettingData.Values["BackupId"]);
            //    SchedulerObj.BackupID = KeyIdGuid;
            //}
        }

        whereStr = "";
        orderBy = "it.EmailFrom desc";
        BLL_ScheduleTasks = new ScheduleTasksBLL();
        vBackupAndEmailSettingObj = BLL_ScheduleTasks.GetBackupAndEmailSetting(whereStr, orderBy);
        if (vBackupAndEmailSettingObj.Count() > 0)
        {
            foreach (var item in vBackupAndEmailSettingObj)
            {
                if (chkBDBB.Checked==true)
                {
                    SchedulerObj.BackupID = item.BackupID;
                }
                SchedulerObj.EmailID = item.EmailID;
            }
        }

        SchedulerObj.TaskName = rtxtTaskName.Text.Trim()+"-CreateSD";
        if(rcbScheduleType.SelectedItem!=null)
        {
            SchedulerObj.ScheduleType = rcbScheduleType.SelectedItem.Value.Trim();
        }
        SchedulerObj.StartTime = rdStartTime.SelectedDate;

        //if (ckxlROOTD.Items.Count>0)
        //{
        //    selectDate = "#";
        //    for (int i = 0; i < ckxlROOTD.Items.Count; i++)
        //    {
        //        if (ckxlROOTD.Items[i].Selected == true)
        //        {
        //            selectDate += ckxlROOTD.Items[i].Value + "#";
        //        }
        //    }
        //    SchedulerObj.RunOnly = selectDate;
        //}
        SchedulerObj.RunOnlyStart = rdStartTime.SelectedDate;//运行开始时间
        SchedulerObj.RunOnlyEnd = rdEndTime.SelectedDate;//运行结束时间

        if (rcbAction.SelectedItem!=null)
        {
            SchedulerObj.Action = rcbAction.SelectedItem.Value.Trim();
        }
        if (rcbPostAction.SelectedItem != null)
        {
            SchedulerObj.PostAction = rcbPostAction.SelectedItem.Value.Trim();
        }
    }
    #endregion

    #region 从cookie取数据,获取Billing、BuckupSetting、EmailSetting其他界面的数据
    protected void GetDataFromCookie()
    {
        HttpCookie billingData = Request.Cookies.Get("Billing");
        HttpCookie EmailSettingData = Request.Cookies.Get("EmailSetting");

        SchedulerObj = new tblScheduler();

        if (billingData!=null)
        {
            SchedulerObj.Company = billingData.Values["Company"];
            SchedulerObj.Cycle = billingData.Values["StartingCycle"] + "," + billingData.Values["EndingCycle"];
            SchedulerObj.Status = billingData.Values["StatusCode"];
            SchedulerObj.Copy = billingData.Values["Copy"];
            SchedulerObj.Calc = billingData.Values["Calc"];
        }
        if (EmailSettingData != null)
        {
            SchedulerObj.EmailTo = EmailSettingData.Values["EmailTo"];
        }
        
    }
    #endregion

    #region 从cookie取数据,获取cookie保存的当前页数据
    protected void GetDataReturnFormOtherPageFromCookie()
    {
        //string checkValue;
        //string[] KeyIdChecks;

        HttpCookie TeskDetailData = Request.Cookies.Get("TeskDetail");

        if (TeskDetailData != null)
        {
            rtxtTaskName.Text = TeskDetailData.Values["TaskName"];
            if (TeskDetailData.Values["ScheduleType"] != null)
            {
                rcbScheduleType.SelectedIndex = rcbScheduleType.Items.IndexOf(
                                                            rcbScheduleType.Items.FindItemByValue(
                                                            TeskDetailData.Values["ScheduleType"].Trim()));
            }

            //if (TeskDetailData.Values["StartTime"] != null)
            //{
            //    rdtimepStartTime.SelectedDate = Convert.ToDateTime(TeskDetailData.Values["StartTime"]);
            //}

            //if (TeskDetailData.Values["RunOnly"] != null)
            //{
            //    KeyIdChecks = TeskDetailData.Values["RunOnly"].Split('#');
            //    if (KeyIdChecks != null)
            //    {
            //        for (int i = 0; i < KeyIdChecks.Length; i++)
            //        {
            //            checkValue = KeyIdChecks[i].ToString();
            //            if (checkValue!=string.Empty)
            //            {
            //                for (int j = 0; j < ckxlROOTD.Items.Count; j++)
            //                {
            //                    if (ckxlROOTD.Items[j].Value == checkValue)
            //                    {
            //                        ckxlROOTD.Items[j].Selected = true;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            if (TeskDetailData.Values["RunOnlyStart"] != null)
            {
                rdStartTime.SelectedDate = Convert.ToDateTime(TeskDetailData.Values["RunOnlyStart"]);
            }
            if (TeskDetailData.Values["RunOnlyEnd"] != null)
            {
                rdEndTime.SelectedDate = Convert.ToDateTime(TeskDetailData.Values["RunOnlyEnd"]);
            }
            if (TeskDetailData.Values["BoolBackup"] != null)
            {
                chkBDBB.Checked = Convert.ToBoolean(TeskDetailData.Values["BoolBackup"]);
            }
            if (TeskDetailData.Values["Action"] != null)
            {
                rcbAction.SelectedIndex = rcbAction.Items.IndexOf(rcbAction.Items.FindItemByValue(TeskDetailData.Values["Action"].Trim()));
            }
            if (TeskDetailData.Values["PostAction"] != null)
            {
                rcbPostAction.SelectedIndex = rcbPostAction.Items.IndexOf(rcbPostAction.Items.FindItemByValue(TeskDetailData.Values["PostAction"].Trim()));
            }
        }
    }
    #endregion

    #region Billing设置
    protected void rbtnBillingSetting_Click(object sender, EventArgs e)
    {
        WriteDataForCookie();//将本页数据存入cookie跳页
        if (this.KeyId == string.Empty)
        {
            Response.Redirect("~/Schedule Billing/BillingForm.aspx");
        }
        else
        {
            Response.Redirect("~/Schedule Billing/BillingForm.aspx?KeyGuid="+this.KeyId);
        }
    }
    #endregion

    #region Email设置
    protected void rbtnMailSetting_Click(object sender, EventArgs e)
    {
        WriteDataForCookie();//将本页数据存入cookie跳页
        if (this.KeyId == string.Empty)
        {
            Response.Redirect("~/Schedule Setting/EmailSettingForm.aspx");
        }
        else
        {
            Response.Redirect("~/Schedule Setting/EmailSettingForm.aspx?KeyGuid=" + this.KeyId);
        }
    }
    #endregion

    #region 将当前数据写入cookie
    protected void WriteDataForCookie()
    {
        //string selectDate;//选择的时间
        //将数据保存到客户端cookie
        HttpCookie myCookie = new HttpCookie("TeskDetail");

        if (rtxtTaskName.Text != string.Empty)
        {
            myCookie.Values.Add("TaskName", rtxtTaskName.Text.Trim());
        }

        if (rcbScheduleType.SelectedItem != null)
        {
            myCookie.Values.Add("ScheduleType", rcbScheduleType.SelectedItem.Value);
        }

        //if (rdtimepStartTime.SelectedDate != null)
        //{
        //    myCookie.Values.Add("StartTime", rdtimepStartTime.SelectedDate.ToString());
        //}

        //if (ckxlROOTD.Items.Count > 0)
        //{
        //    selectDate = "#";
        //    for (int i = 0; i < ckxlROOTD.Items.Count; i++)
        //    {
        //        if (ckxlROOTD.Items[i].Selected == true)
        //        {
        //            selectDate += ckxlROOTD.Items[i].Value + "#";
        //        }
        //    }
        //    myCookie.Values.Add("RunOnly", selectDate);
        //}

        if (rdStartTime.SelectedDate != null)
        {
            myCookie.Values.Add("RunOnlyStart", rdStartTime.SelectedDate.ToString());
        }
        if (rdEndTime.SelectedDate != null)
        {
            myCookie.Values.Add("RunOnlyEnd", rdEndTime.SelectedDate.ToString());
        }
        myCookie.Values.Add("BoolBackup", chkBDBB.Checked.ToString());
        if (rcbAction.SelectedItem != null)
        {
            myCookie.Values.Add("Action", rcbAction.SelectedItem.Value);
        }

        if (rcbPostAction.SelectedItem != null)
        {
            myCookie.Values.Add("PostAction", rcbPostAction.SelectedItem.Value);
        }

        myCookie.Expires = System.DateTime.Now.AddDays(1);
        Response.AppendCookie(myCookie);
    }
    #endregion

    #region 保存数据成功的处理方法,清空cookie
    protected void PageSet(string strScheduleID)
    {
        ClearCookie();
        Response.Redirect("~/Schedule Billing/TaskDetailFormView.aspx?KeyGuid=" + strScheduleID);
    }
    #endregion

    #region 给控件赋值
    protected void FindValue()
    {
        //string checkValue;
        //string[] KeyIdChecks;

        if (this.KeyId != string.Empty)
        {
            rtxtTaskName.Text = SchedulerObj.TaskName.Substring(0, SchedulerObj.TaskName.LastIndexOf("-"));//截取-CreateSD;
            if (SchedulerObj.ScheduleType != null)
            {
                rcbScheduleType.SelectedIndex = rcbScheduleType.Items.IndexOf(rcbScheduleType.Items.FindItemByValue(SchedulerObj.ScheduleType.Trim()));
                //if (!SchedulerObj.ScheduleType.Equals("Weekly"))
                //{
                //    ckxlROOTD.Enabled = false;
                //}
                //else
                //{
                //    ckxlROOTD.Enabled = true;
                //}
            }
            //if (SchedulerObj.StartTime.ToString() != null)
            //{
            //    rdtimepStartTime.SelectedDate = SchedulerObj.StartTime;
            //}

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

            if (SchedulerObj.RunOnlyStart.ToString() != string.Empty)
            {
                rdStartTime.SelectedDate = SchedulerObj.RunOnlyStart;
            }
            if (SchedulerObj.RunOnlyEnd.ToString() != string.Empty)
            {
                rdEndTime.SelectedDate = SchedulerObj.RunOnlyEnd;
            }
            if (SchedulerObj.Action != null)
            {
                rcbAction.SelectedIndex = rcbAction.Items.IndexOf(rcbAction.Items.FindItemByValue(SchedulerObj.Action.Trim()));
            }
            if (SchedulerObj.PostAction != null)
            {
                rcbPostAction.SelectedIndex = rcbPostAction.Items.IndexOf(rcbPostAction.Items.FindItemByValue(SchedulerObj.PostAction.Trim()));
            }

            if (SchedulerObj.BackupID.ToString() != string.Empty)
            {
                BLL_Backup = new BackupBLL();
                chkBDBB.Checked = BLL_Backup.ExistFindBackupById(SchedulerObj.BackupID.ToString());//查看是否有数据备份设置
            }
            else
            {
                chkBDBB.Checked = false;
                //编辑新增加了Backup信息
                HttpCookie BuckupSettingData = Request.Cookies.Get("BuckupSetting");
                if (BuckupSettingData != null)
                {
                    if (BuckupSettingData.Values["BackupId"]!=string.Empty)
                    {
                        chkBDBB.Checked = true;
                    }
                }
            }
        }
    }
    #endregion

    #region 选择Schedule的类型不为Weekly时，将CheckBoxList置灰
    protected void rcbScheduleType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        //if (rcbScheduleType.SelectedValue.Equals("Weekly"))
        //{
        //    ckxlROOTD.Enabled = true;
        //}
        //else
        //{
        //    ckxlROOTD.Enabled = false;
        //    //所有按钮都不选
        //    if (ckxlROOTD.Items.Count > 0)
        //    {
        //        for (int i = 0; i < ckxlROOTD.Items.Count; i++)
        //        {
        //            ckxlROOTD.Items[i].Selected = false;
        //        }
        //    }
        //}
    }
    #endregion

    #region 校验TaskName是否重复
    protected Boolean rbtnCheck_Click(object sender, EventArgs e)
    {
        ObjectQuery<tblScheduler> SchedulerObjs;

        string strWhere;
        string orderBy;
        string taskName;

        taskName = rtxtTaskName.Text.Trim() + "-CreateSD";
        if (taskName != null && taskName != string.Empty)
        {
            strWhere = " and it.TaskName='" + taskName + "'";
            orderBy = "it.ScheduleID desc";

            BLL_ScheduleTasks = new ScheduleTasksBLL();
            SchedulerObjs = BLL_ScheduleTasks.GetSchedulers(strWhere, orderBy);

            if (this.KeyId!=string.Empty)
            {
                if (SchedulerObjs.Count() > 1)//编辑的时候包括自己
                {
                    MessageBox(false, true, false,
                        GetGlobalResourceObject("WebResource", "TaskDetailForm_lnkMessage_ErrorMessage").ToString());

                    return false;
                }
                else
                {
                    pnlMessage.Visible = false;
                }
            }
            else
            {
                if (SchedulerObjs.Count() > 0)
                {
                    MessageBox(false, true, false,
                        GetGlobalResourceObject("WebResource", "TaskDetailForm_lnkMessage_ErrorMessage").ToString());

                    return false;
                }
                else
                {
                    pnlMessage.Visible = false;
                }
            }
        }

        return true;
    }
    #endregion

    #region 校验Billing、BuckupSetting、EmailSetting等信息是否为空
    protected Boolean CheckCookiesInfo(object sender, EventArgs e)
    {
        Boolean judgeBackup;
        HttpCookie billingData = Request.Cookies.Get("Billing");
        HttpCookie emailSettingData = Request.Cookies.Get("EmailSetting");

        judgeBackup = JudgeBackupAndEmailSetting();
        if (chkBDBB.Checked==false)
        {
            if (billingData == null || emailSettingData == null)
            {
                MessageBox(false, true, false,
                        GetGlobalResourceObject("WebResource", "TaskDetailForm_CookiesMessage_ErrorMessage").ToString());

                return false;
            }
            else
            {
                pnlMessage.Visible = false;
            }
        }
        else
        {
            if (billingData == null || judgeBackup == false || emailSettingData == null)
            {
                MessageBox(false, true, false,
                        GetGlobalResourceObject("WebResource", "TaskDetailForm_CookiesMessage_ErrorMessage").ToString());

                return false;
            }
            else
            {
                pnlMessage.Visible = false;
            }
        }

        return true;
    }
    #endregion

    #region 校验TaskName字段是否为空,以及是否已经存在
    protected void rtxtTaskName_TextChanged(object sender, EventArgs e)
    {
        //tasks名称必填
        string taskName=rtxtTaskName.Text.Trim();
        if (taskName != string.Empty)
        {
            pnlMessage.Visible = false;
            rbtnCheck_Click(sender, e);
        }
        else
        {
            MessageBox(false, true, false,
                        GetGlobalResourceObject("WebResource", "TaskDetailForm_rfvRequiredFrom_ErrorMessage").ToString());
            rtxtTaskName.Focus();
        }
    }
    #endregion

    #region 校验必填字段相应控件是否为空
    protected Boolean CheckRequired(object sender, EventArgs e)
    {
        //tasks名称必填
        string taskName = rtxtTaskName.Text.Trim();
        if (taskName != string.Empty)
        {
            pnlMessage.Visible = false;
        }
        else
        {
            pnlMessage.Visible = true;
            lnkMessage.Text = GetGlobalResourceObject("WebResource", "TaskDetailForm_rfvRequiredFrom_ErrorMessage").ToString();
            rtxtTaskName.Focus();

            return false;
        }

        ////开始时间必填
        //if (rdtimepStartTime.SelectedDate != null)
        //{
        //    pnlMessage.Visible = false;
        //}
        //else
        //{
        //    pnlMessage.Visible = true;
        //    lnkMessage.Text = GetGlobalResourceObject("WebResource", "TaskDetailForm_rfvRequiredFrom_ErrorMessage").ToString();
        //    rdtimepStartTime.Focus();

        //    return false;
        //}

        //时间段必填
        if (rdStartTime.SelectedDate != null)
        {
            pnlMessage.Visible = false;
        }
        else
        {
            pnlMessage.Visible = true;
            lnkMessage.Text = GetGlobalResourceObject("WebResource", "TaskDetailForm_rfvRequiredFrom_ErrorMessage").ToString();
            rdStartTime.Focus();

            return false;
        }
        if (rdEndTime.SelectedDate != null)
        {
            pnlMessage.Visible = false;
        }
        else
        {
            pnlMessage.Visible = true;
            lnkMessage.Text = GetGlobalResourceObject("WebResource", "TaskDetailForm_rfvRequiredFrom_ErrorMessage").ToString();
            rdEndTime.Focus();

            return false;
        }

        return true;
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
            if (message.Length>100)
            {
                pnlMessage.Height=80;
            }
        }
    }
    #endregion

    #region 新的Tast的StartTime、EndTime不在其他的RunOnlyStart和RunOnlyEnd之间
    protected Boolean JudgeRunTime(string startDate, string endDate)
    {
        string strWhere;
        string orderBy;

        ObjectQuery<tblScheduler> SchedulerObjs;

        BLL_ScheduleTasks = new ScheduleTasksBLL();
        //检测新建Task的起始时间段内，不包含旧的Task时间
        strWhere = " and it.RunOnlyStart>=cast('" + startDate + "' as System.DateTime) and it.RunOnlyStart<=cast('" + endDate + "' as System.DateTime)";
        orderBy = "it.TaskName desc";
        SchedulerObjs = BLL_ScheduleTasks.GetSchedulers(strWhere, orderBy);

        if (this.KeyId == string.Empty)//插入验证
        {
            if (SchedulerObjs.Count() > 0)
            {
                MessageBox(false, true, false,
                            GetGlobalResourceObject("WebResource", "TaskDetailForm_rdEndTime_Message").ToString());
                return false;
            }
        }
        else
        {
            if (SchedulerObjs.Count() > 1)//更新验证
            {
                MessageBox(false, true, false,
                            GetGlobalResourceObject("WebResource", "TaskDetailForm_rdEndTime_Message").ToString());
                return false;
            }
        }

        strWhere = " and it.RunOnlyEnd>=cast('" + startDate + "' as System.DateTime) and it.RunOnlyEnd<=cast('" + endDate + "' as System.DateTime)";
        orderBy = "it.TaskName desc";
        SchedulerObjs = BLL_ScheduleTasks.GetSchedulers(strWhere, orderBy);

        if (this.KeyId == string.Empty)//插入验证
        {
            if (SchedulerObjs.Count() > 0)
            {
                MessageBox(false, true, false,
                            GetGlobalResourceObject("WebResource", "TaskDetailForm_rdEndTime_Message").ToString());
                return false;
            }
        }
        else
        {
            if (SchedulerObjs.Count() > 1)//更新验证
            {
                MessageBox(false, true, false,
                            GetGlobalResourceObject("WebResource", "TaskDetailForm_rdEndTime_Message").ToString());
                return false;
            }
        }

        //检测旧Task的起始时间段内，不包含的新建Task时间
        strWhere = " and it.RunOnlyStart<=cast('" + startDate + "' as System.DateTime) and it.RunOnlyEnd>=cast('" + startDate + "' as System.DateTime)";
        orderBy = "it.TaskName desc";
        SchedulerObjs = BLL_ScheduleTasks.GetSchedulers(strWhere, orderBy);

        if (this.KeyId == string.Empty)//插入验证
        {
            if (SchedulerObjs.Count() > 0)
            {
                MessageBox(false, true, false,
                            GetGlobalResourceObject("WebResource", "TaskDetailForm_rdEndTime_Message").ToString());
                return false;
            }
        }
        else
        {
            if (SchedulerObjs.Count() > 1)//更新验证
            {
                MessageBox(false, true, false,
                            GetGlobalResourceObject("WebResource", "TaskDetailForm_rdEndTime_Message").ToString());
                return false;
            }
        }

        strWhere = " and it.RunOnlyStart<=cast('" + endDate + "' as System.DateTime) and it.RunOnlyEnd>=cast('" + endDate + "' as System.DateTime)";
        orderBy = "it.TaskName desc";
        SchedulerObjs = BLL_ScheduleTasks.GetSchedulers(strWhere, orderBy);

        if (this.KeyId == string.Empty)//插入验证
        {
            if (SchedulerObjs.Count() > 0)
            {
                MessageBox(false, true, false,
                            GetGlobalResourceObject("WebResource", "TaskDetailForm_rdEndTime_Message").ToString());
                return false;
            }
        }
        else
        {
            if (SchedulerObjs.Count() > 1)//更新验证
            {
                MessageBox(false, true, false,
                            GetGlobalResourceObject("WebResource", "TaskDetailForm_rdEndTime_Message").ToString());
                return false;
            }
        }

        return true;
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

    #region 判断backup和Email是否已经设置
    protected Boolean JudgeBackupAndEmailSetting()
    {
        string whereStr;
        string orderBy;

        whereStr = "";
        orderBy = "it.EmailFrom desc";
        BLL_ScheduleTasks = new ScheduleTasksBLL();
        vBackupAndEmailSettingObj = BLL_ScheduleTasks.GetBackupAndEmailSetting(whereStr, orderBy);
        if (vBackupAndEmailSettingObj.Count() > 0)
        {
            foreach (var item in vBackupAndEmailSettingObj)
            {
                return true;
            }
        }
        return false;
    }
    #endregion
}
