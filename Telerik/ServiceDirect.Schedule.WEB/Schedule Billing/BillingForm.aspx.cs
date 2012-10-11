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

/**
*Description:所有的Cycle数据集都是根据Cycle字段进行排序，UTP也是如此,SD保存的是Cycle
*/

public partial class BillingForm : System.Web.UI.Page 
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

    vUTPCustomerBLL BLL_vUTPCustomer;
    ScheduleTasksBLL BLL_ScheduleTasks;//声明对象的控制类

    ObjectQuery<vUTPCompany> vUTPCompanyObjs;
    ObjectQuery<vUTPCycle> vUTPCycleObjs;
    ObjectQuery<vUTPCustomerState> vUTPCustomerStateObjs;

    tblScheduler SchedulerObj;//声明对象

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pnlMessage.Visible = false;
            pnlRight.Visible = false;
            pnlError.Visible = false;
            
            AllComboBoxBind();//查询数据，给所有ComboBox绑定数据
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

    #region 给所有页面控件赋值
    protected void FindValue()
    {
        vUTPCycle startingCycle;//起始区域
        vUTPCycle endingCycle;//终止区域

        Boolean startCycle=false;//是否是起始区域
        Boolean endCycle=false;//是否是结束区域
        RadComboBoxItem endingCycleComboBox;
        RadComboBoxItem startingCycleComboBox;

        string[] cycle;

        if (this.KeyId != string.Empty)
        {
            if (SchedulerObj.Company != null)
            {
                rctxtCompany.SelectedValue = SchedulerObj.Company.Trim();
            }
            if (SchedulerObj.Cycle!=null)
            {
                cycle = SchedulerObj.Cycle.Split(',');
                if (SchedulerObj.Cycle != null)
                {
                    cycle = SchedulerObj.Cycle.Split(',');
                    if (cycle!=null)
                    {
                        int i=0;
                        foreach (var item in cycle)
                        {
                            if(i == 0)
                            {
                                if (item.Equals("N/A"))
                                {
                                    //清空起始区域
                                    rctxtStartingCycle.Items.Clear();

                                    startingCycleComboBox = new RadComboBoxItem("N/A", "N/A");
                                    rctxtStartingCycle.Items.Add(startingCycleComboBox);
                                }
                                else
                                {
                                    rctxtStartingCycle.SelectedValue = item.Trim();
                                }
                                i++;
                                //是否数据集的first
                                if (!item.Equals("N/A"))
                                {
                                    this.OrderBy = " it.Cycle asc";
                                    BLL_vUTPCustomer = new vUTPCustomerBLL();
                                    startingCycle = BLL_vUTPCustomer.FindFirstCycleByCompanyId(rctxtCompany.SelectedValue, this.OrderBy);
                                    if (item.Equals(startingCycle.Cycle.ToString()))
                                    {
                                        startCycle = true;
                                    }
                                }
                            }else if(i == 1)
                            {
                                if (item.Equals("N/A"))
                                {
                                    //清空终止区域
                                    rctxtEndingCycle.Items.Clear();

                                    endingCycleComboBox = new RadComboBoxItem("N/A", "N/A");
                                    rctxtEndingCycle.Items.Add(endingCycleComboBox);
                                }
                                else
                                {
                                    rctxtEndingCycle.SelectedValue = item.Trim();
                                }
                                //是否数据的Last
                                if (!item.Equals("N/A"))
                                {
                                    endingCycle = BLL_vUTPCustomer.FindLastCycleByCompanyId(rctxtCompany.SelectedValue, this.OrderBy);
                                    if (item.Equals(endingCycle.Cycle.ToString()))
                                    {
                                        endCycle = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (SchedulerObj.Copy!=null&&SchedulerObj.Copy.Equals("True"))
            {
                chkCopy.Checked=true;
            }
            if (SchedulerObj.Calc != null && SchedulerObj.Calc.Equals("True"))
            {
                chkCalc.Checked = true;
            }
            if (SchedulerObj.Status != null)
            {
                if (SchedulerObj.Status.Trim().Equals("All"))
                {
                    rctxtStatusCode.SelectedValue = "All";
                }else
                {
                    rctxtStatusCode.SelectedValue = SchedulerObj.Status.Trim();
                }
            }

            //如果起始区域和结束区域是数据的frist和last，则AllCycle为true
            if (startCycle == true && endCycle==true)
            {
                chkAllCycles.Checked = true;
                //区域控件置灰
                rctxtEndingCycle.Enabled = false;
                rctxtStartingCycle.Enabled = false;
            }
        }
    }
    #endregion

    #region 确认保存到Cookie
    protected void rbtnOK_Click(object sender, EventArgs e)
    {
        System.Guid KeyIdGuid;
        string successFlag;//标记数据库操作是否成功

        int startCycleIndex=0;
        int endCycleIndex = 0;
        startCycleIndex = rctxtStartingCycle.SelectedIndex;
        endCycleIndex = rctxtStartingCycle.FindItemIndexByValue(rctxtEndingCycle.SelectedValue);

        if (rctxtCompany.SelectedValue != "N/A" && rctxtStartingCycle.SelectedValue != "N/A" && rctxtEndingCycle.SelectedValue != "N/A")
        {
            if (startCycleIndex < endCycleIndex || startCycleIndex == endCycleIndex)
            {
                if (this.KeyId == string.Empty)//新增
                {
                    //将数据保存到客户端cookie
                    HttpCookie myCookie = new HttpCookie("Billing");

                    if (rctxtCompany.SelectedItem != null)
                    {
                        myCookie.Values.Add("Company", rctxtCompany.SelectedItem.Value);
                    }

                    if (rctxtStartingCycle.SelectedItem != null)
                    {
                        myCookie.Values.Add("StartingCycle", rctxtStartingCycle.SelectedItem.Value);
                    }

                    if (rctxtEndingCycle.SelectedItem != null)
                    {
                        myCookie.Values.Add("EndingCycle", rctxtEndingCycle.SelectedItem.Value);
                    }

                    if (rctxtStatusCode.SelectedItem != null)
                    {
                        myCookie.Values.Add("StatusCode", rctxtStatusCode.SelectedItem.Value);
                    }

                    myCookie.Values.Add("AllCycles", chkAllCycles.Checked.ToString());
                    myCookie.Values.Add("Copy", chkCopy.Checked.ToString());
                    myCookie.Values.Add("Calc", chkCalc.Checked.ToString());

                    myCookie.Expires = System.DateTime.Now.AddDays(1);
                    Response.AppendCookie(myCookie);
                    Response.Redirect("~/Schedule Billing/TaskDetailForm.aspx");
                }
                else//更新
                {
                    SchedulerObj = new tblScheduler();
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
                    if (rctxtCompany.SelectedItem != null)
                    {
                        SchedulerObj.Company = rctxtCompany.SelectedItem.Value;
                    }

                    if (rctxtStartingCycle.SelectedItem != null && rctxtEndingCycle.SelectedItem != null)
                    {
                        SchedulerObj.Cycle = rctxtStartingCycle.SelectedItem.Value + "," + rctxtEndingCycle.SelectedItem.Value;
                    }

                    if (rctxtStatusCode.SelectedItem != null)
                    {
                        SchedulerObj.Status = rctxtStatusCode.SelectedItem.Value;
                    }

                    SchedulerObj.Copy = chkCopy.Checked.ToString();
                    SchedulerObj.Calc = chkCalc.Checked.ToString();

                    KeyIdGuid = new Guid(this.KeyId);//转换成Guid类型

                    SchedulerObj.ScheduleID = KeyIdGuid;

                    BLL_ScheduleTasks = new ScheduleTasksBLL();
                    successFlag = BLL_ScheduleTasks.UpdateInBillingPage(SchedulerObj);
                    if (!successFlag.Equals("InsertError"))
                    {
                        //将数据保存到客户端cookie
                        HttpCookie myCookie = new HttpCookie("Billing");

                        myCookie.Values.Add("billingEdit", "billingEdit");

                        myCookie.Expires = System.DateTime.Now.AddDays(1);
                        Response.AppendCookie(myCookie);

                        Response.Redirect("~/Schedule Billing/TaskDetailForm.aspx?KeyGuid=" + this.KeyId);
                    }
                    else
                    {
                        //错误提示信息
                        MessageBox(false, false, true,
                                GetGlobalResourceObject("WebResource", "BillingForm_SaveMessage_ErrorMessage").ToString());
                    }
                }
            }
            else//EndCycle<startCycle
            {
                MessageBox(false, true, false,
                                GetGlobalResourceObject("WebResource", "BillingForm_SelectCycleMessage_Message").ToString());
            }
        }
        else//Cycle的数据无效
        {
            MessageBox(false, true, false,
                                GetGlobalResourceObject("WebResource", "BillingForm_ConpanyCycle_Message").ToString());
        }
    }
    #endregion

    #region 取消返回条件选择页面
    protected void rbtnCancel_Click(object sender, EventArgs e)
    {
        if (this.KeyId!=null)
        {
            Response.Redirect("~/Schedule Billing/TaskDetailForm.aspx?KeyGuid=" + this.KeyId);
        }
        else
        {
            Response.Redirect("~/Schedule Billing/TaskDetailForm.aspx");
        }
    }
    #endregion

    #region 给Company、StatusCode、StateCode的ComboBox控件赋值
    protected void AllComboBoxBind()
    {
        RadComboBoxItem Status;//Telerik的状态控件

        vUTPCompany vUTPCompanyObj;

        this.OrderBy = " it.CompName desc ";
        string whereStr;

        whereStr = "";
        BLL_vUTPCustomer = new vUTPCustomerBLL();
        vUTPCompanyObjs = BLL_vUTPCustomer.GetUTPCompanys(whereStr, this.OrderBy);

        if (vUTPCompanyObjs.Count() > 0)
        {
            rctxtCompany.DataSource = vUTPCompanyObjs;
            rctxtCompany.DataValueField = "CompanyID";
            rctxtCompany.DataTextField = "Comp";
            rctxtCompany.DataBind();
        }
        vUTPCompanyObj = vUTPCompanyObjs.ToList().First();

        CycleComboBoxBind(vUTPCompanyObj.CompanyID);

        whereStr = " and it.Billable=true";
        this.OrderBy = " it.StatType desc ";
        vUTPCustomerStateObjs = BLL_vUTPCustomer.GetUTPCustomerStates(whereStr, this.OrderBy);

        if (vUTPCustomerStateObjs.Count() > 0)
        {
            try
            {
                rctxtStatusCode.DataSource = vUTPCustomerStateObjs;
                rctxtStatusCode.DataValueField = "CustomerStateID";
                rctxtStatusCode.DataTextField = "StatCode";
                rctxtStatusCode.DataBind();

                Status = new RadComboBoxItem("All", "All");
                rctxtStatusCode.Items.Add(Status);
                rctxtStatusCode.SelectedValue = "All";
            }
            catch (Exception)
            {
                rctxtStatusCode.Items.Clear();

                Status = new RadComboBoxItem("All", "All");
                rctxtStatusCode.Items.Add(Status);
                rctxtStatusCode.SelectedValue = "All";
            }
        }
    }
    #endregion

    #region 给StartCycle和EndCycle的ComboBox控件赋值,根据company的id
    protected void CycleComboBoxBind(string UTPCompanyId)
    {
        RadComboBoxItem startingCycle;//起始区域Telerik控件
        RadComboBoxItem endingCycle;//结束区域Telerik控件
        string whereStr;

        whereStr = " and it.CompanyID='" + UTPCompanyId + "'";
        this.OrderBy = " it.Cycle asc ";

        try
        {
            vUTPCycleObjs = BLL_vUTPCustomer.GetUTPCycles(whereStr, this.OrderBy);
        }
        catch (NullReferenceException)
        {
            BLL_vUTPCustomer = new vUTPCustomerBLL();
            vUTPCycleObjs = BLL_vUTPCustomer.GetUTPCycles(whereStr, this.OrderBy);
        }

        if (vUTPCycleObjs.Count() > 0)
        {
            try
            {
                //起始区域
                rctxtStartingCycle.DataSource = vUTPCycleObjs;
                rctxtStartingCycle.DataValueField = "Cycle";
                rctxtStartingCycle.DataTextField = "Cycle";
                rctxtStartingCycle.DataBind();

                //终止区域
                rctxtEndingCycle.DataSource = vUTPCycleObjs;
                rctxtEndingCycle.DataValueField = "Cycle";
                rctxtEndingCycle.DataTextField = "Cycle";
                rctxtEndingCycle.DataBind();
            }
            catch (Exception)
            {
                //清空终止区域
                rctxtEndingCycle.Items.Clear();

                endingCycle = new RadComboBoxItem("N/A", "N/A");
                rctxtEndingCycle.Items.Add(endingCycle);

                //清空起始区域
                rctxtStartingCycle.Items.Clear();

                startingCycle = new RadComboBoxItem("N/A", "N/A");
                rctxtStartingCycle.Items.Add(startingCycle);
            }
        }
        else
        {
            //清空终止区域
            rctxtEndingCycle.Items.Clear();

            endingCycle = new RadComboBoxItem("N/A", "N/A");
            rctxtEndingCycle.Items.Add(endingCycle);

            //清空起始区域
            rctxtStartingCycle.Items.Clear();

            startingCycle = new RadComboBoxItem("N/A", "N/A");
            rctxtStartingCycle.Items.Add(startingCycle);

            //无数据chkAllCycles置灰
            chkAllCycles.Checked = false;
            chkAllCycles.Enabled = false;
        }
    }
    #endregion

    #region 从cookie取数据,获取cookie保存的当前页数据
    protected void GetDataReturnFormOtherPageFromCookie()
    {
        HttpCookie BillingData = Request.Cookies.Get("Billing");

        if (BillingData != null)
        {
            if (BillingData.Values["Company"] != null)
            {
                rctxtCompany.SelectedValue = BillingData.Values["Company"].Trim();
            }
            if (BillingData.Values["StartingCycle"] != null)
            {
                rctxtStartingCycle.SelectedValue = BillingData.Values["StartingCycle"].Trim();
            }

            if (BillingData.Values["EndingCycle"] != null)
            {
                rctxtEndingCycle.SelectedValue = BillingData.Values["EndingCycle"].Trim();
            }

            if (BillingData.Values["StatusCode"] != null)
            {
                rctxtStatusCode.SelectedValue = BillingData.Values["StatusCode"].Trim();
            }
            if (BillingData.Values["AllCycles"] != null)
            {
                chkAllCycles.Checked = Convert.ToBoolean(BillingData.Values["AllCycles"]);
                if (chkAllCycles.Checked==true)
                {
                    //区域控件置灰
                    rctxtEndingCycle.Enabled = false;
                    rctxtStartingCycle.Enabled = false;
                }
            }
            if (BillingData.Values["Copy"] != null)
            {
                chkCopy.Checked = Convert.ToBoolean(BillingData.Values["Copy"]);
            }
            if (BillingData.Values["Calc"] != null)
            {
                chkCalc.Checked = Convert.ToBoolean(BillingData.Values["Calc"]);
            }
        }
    }
    #endregion

    #region 根据Company的选择值，设置Cycle的下拉数据集
    protected void rctxtCompany_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (rctxtCompany.SelectedValue != "N/A")
        {
            CycleComboBoxBind(rctxtCompany.SelectedValue);
        }
    }
    #endregion

    #region 选择所有地区的时候
    protected void chkAllCycles_CheckedChanged(object sender, EventArgs e)
    {
        this.OrderBy = " it.Cycle asc";

        vUTPCycle startingCycle;//起始区域
        vUTPCycle endingCycle;//终止区域
        
        string vUTPCompanyId;

        if (chkAllCycles.Checked==true)
        {
            vUTPCompanyId=rctxtCompany.SelectedValue;
            if (vUTPCompanyId != "N/A" && !rctxtEndingCycle.SelectedValue.Equals("N/A")
                                            && !rctxtStartingCycle.SelectedValue.Equals("N/A"))
            {
                BLL_vUTPCustomer = new vUTPCustomerBLL();
                startingCycle = BLL_vUTPCustomer.FindFirstCycleByCompanyId(vUTPCompanyId, this.OrderBy);
                endingCycle = BLL_vUTPCustomer.FindLastCycleByCompanyId(vUTPCompanyId, this.OrderBy);

                //给空间赋值
                rctxtStartingCycle.SelectedValue = startingCycle.Cycle.ToString().Trim();

                rctxtEndingCycle.SelectedValue = endingCycle.Cycle.ToString().Trim();

                //区域控件置灰
                rctxtEndingCycle.Enabled = false;
                rctxtStartingCycle.Enabled = false;
            }
        }
        else
        {
            rctxtEndingCycle.Enabled = true;
            rctxtStartingCycle.Enabled = true;
        }
    }
    #endregion

    #region 选择开始区域，自动加载结束区域数据集
    protected void rctxtStartingCycle_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string whereStr;

        vUTPCycle endingCycle;//终止区域

        if (rctxtCompany.SelectedValue != "N/A")
        {
            whereStr = "  and it.CompanyID='" + rctxtCompany.SelectedValue + "'";
            this.OrderBy = " it.Cycle asc ";

            BLL_vUTPCustomer = new vUTPCustomerBLL();
            endingCycle = BLL_vUTPCustomer.FindLastCycleByCompanyId(rctxtCompany.SelectedValue, this.OrderBy);

            whereStr = " and it.CompanyID='" + rctxtCompany.SelectedValue + "' and it.Cycle >= " +
                                                    rctxtStartingCycle.SelectedValue + " and it.Cycle <= " + endingCycle.Cycle + "";

            vUTPCycleObjs = BLL_vUTPCustomer.GetUTPCycles(whereStr, this.OrderBy);

            if (vUTPCycleObjs.Count()>0)
            {
                rctxtEndingCycle.DataSource = vUTPCycleObjs;
                rctxtEndingCycle.DataValueField = "Cycle";
                rctxtEndingCycle.DataTextField = "Cycle";
                rctxtEndingCycle.DataBind();
            }
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
