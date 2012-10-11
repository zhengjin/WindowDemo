using System;
using System.Collections.Generic;
using System.Data;

using System.Drawing;
using System.Data.Common;
using System.Data.Objects;

namespace ChartDemo
{
    public partial class ChartCDProBilling : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                this.BillingChartDataBinding();
                this.BindingCombox();
            }
        }

        protected void BindingCombox()
        {
            List<tblCompany> companyObjList;
            GetInfo bllGetInfo = new GetInfo();
            companyObjList=bllGetInfo.GetCompany();
            if (companyObjList != null && companyObjList.Count>0)
            {
                tblCompany companyObj = new tblCompany();
                companyObj.CompName = "N/A";
                companyObj.CompanyID = "N/A";
                companyObjList.Add(companyObj);
                rcomCompany.DataSource = companyObjList;
                rcomCompany.DataTextField = "CompName";
                rcomCompany.DataValueField = "CompanyID";
                rcomCompany.DataBind();

                rcomCompany.SelectedValue = "N/A";
            }
        }

        protected void BillingChartDataBinding(string company = null, string cycle = null, string accNum = null)
        {
            //获取当前年的月底日期
            string firstMothDateTimeStr =
                DateTime.Parse(DateTime.Now.ToString("01/01/yyyy")).AddMonths(1).AddDays(-1).ToShortDateString();
            DateTime firstMothDate = DateTime.Parse(firstMothDateTimeStr);

            //生成Chart的X轴角标
            DataTable dt = new DataTable();
            DataColumn transaction = new DataColumn("Amount");
            transaction.DataType = typeof(int);
            dt.Columns.Add(transaction);
            transaction = new DataColumn("month");
            transaction.DataType = typeof(string);
            dt.Columns.Add(transaction);

            GetInfo bllGetInfo = new GetInfo();

            //decimal usageAmount = bllGetInfo.GetBillingInfo();
            //统计当前月份的前N个月的数据
            if (string.IsNullOrEmpty(company) && string.IsNullOrEmpty(cycle) && string.IsNullOrEmpty(accNum))
            {
                //默认的查询所有的数据
                for (int i = DateTime.Now.Month-7; i < DateTime.Now.Month-1; i++)
                {
                    DateTime currentDate = firstMothDate.AddMonths(i);

                    dt.Rows.Add(new object[] { bllGetInfo.GetBillingInfo(currentDate), currentDate.ToString("MM/dd/yyyy") });
                }
            }
            else if (!string.IsNullOrEmpty(company) && string.IsNullOrEmpty(cycle) && string.IsNullOrEmpty(accNum))
            {
                //查询公司的
                for (int i = DateTime.Now.Month - 7; i < DateTime.Now.Month-1; i++)
                {
                    DateTime currentDate = firstMothDate.AddMonths(i);

                    dt.Rows.Add(new object[] { bllGetInfo.GetBillingInfo(currentDate, company), currentDate.ToString("MM/dd/yyyy") });
                }
            }
            else if (!string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(cycle) && string.IsNullOrEmpty(accNum))
            {
                //查询公司和相应区域
                for (int i = DateTime.Now.Month - 7; i < DateTime.Now.Month-1; i++)
                {
                    DateTime currentDate = firstMothDate.AddMonths(i);

                    dt.Rows.Add(new object[] { bllGetInfo.GetBillingInfo(currentDate, company, cycle), currentDate.ToString("MM/dd/yyyy") });
                }
            }
            else if (!string.IsNullOrEmpty(company) && !string.IsNullOrEmpty(cycle) && !string.IsNullOrEmpty(accNum))
            {
                //查询公司和相应区域
                for (int i = DateTime.Now.Month - 7; i < DateTime.Now.Month-1; i++)
                {
                    DateTime currentDate = firstMothDate.AddMonths(i);

                    dt.Rows.Add(new object[] { bllGetInfo.GetBillingInfo(currentDate, company, cycle, accNum), currentDate.ToString("MM/dd/yyyy") });
                }
            }
            RChartCDProBilling.DataSource = dt;
            RChartCDProBilling.DataBind();
        }

        protected void rcomCompany_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rcomCycle.Enabled = true;
            List<tblCycle> cycleObjList;
            GetInfo bllGetInfo = new GetInfo();

            cycleObjList = bllGetInfo.GetCycleByCompanyId(rcomCompany.SelectedItem.Value);
            if (cycleObjList != null && cycleObjList.Count>0)
            {
                rcomCycle.DataSource = cycleObjList;
                rcomCycle.DataTextField = "Cycle";
                rcomCycle.DataValueField = "Cycle";
                rcomCycle.DataBind();
            }
            else
            {
                tblCycle cycleObj = new tblCycle();
                cycleObj.Description = "N/A";
                cycleObjList.Add(cycleObj);
                rcomCycle.DataSource = cycleObjList;
                rcomCycle.DataTextField = "Description";
                rcomCycle.DataValueField = "Description";
                rcomCycle.DataBind();
                rcomCycle.Enabled = false;
            }
        }

        protected void rbtnApply_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(rcomCompany.SelectedItem.Value)
                && string.IsNullOrEmpty(rtxtAccNum.Text.Trim())
                && rcomCycle.SelectedItem.Value=="N/A")
            {
                //统计某公司
                this.BillingChartDataBinding(rcomCompany.SelectedItem.Value);
            }
            else if (!string.IsNullOrEmpty(rtxtAccNum.Text.Trim()) 
                && !string.IsNullOrEmpty(rcomCompany.SelectedItem.Value) 
                && !string.IsNullOrEmpty(rcomCycle.SelectedItem.Value)
                && rcomCycle.SelectedItem.Value != "N/A")
            {
                //统计某公司的下属机构，以及对应的账户
                this.BillingChartDataBinding(rcomCompany.SelectedItem.Value, 
                    rcomCycle.SelectedItem.Value, rtxtAccNum.Text.Trim());
            }
            else if (!string.IsNullOrEmpty(rcomCompany.SelectedItem.Value)
                && !string.IsNullOrEmpty(rcomCycle.SelectedItem.Value)
                && rcomCycle.SelectedItem.Value != "N/A")
            {
                //统计某公司的下属机构
                this.BillingChartDataBinding(rcomCompany.SelectedItem.Value, rcomCycle.SelectedItem.Value);
            }
        }

        protected void RChartCDProBilling_PreRender(object sender, EventArgs e)
        {
            //RChartCDProBilling.Series.GetByName("Amount").Appearance.FillStyle.MainColor = Color.FromArgb(238, 180, 180);
            //RChartCDProBilling.Series.GetByName("Amount").Appearance.LineSeriesAppearance.Width = 5;
            RChartCDProBilling.Series.GetByName("Amount").Appearance.LabelAppearance.RotationAngle = 45;
            //RChartCDProBilling.Series.GetByName("Amount").Appearance.LabelAppearance. = 45;
            RChartCDProBilling.Series.GetByName("Amount").Appearance.BarWidthPercent = 40;
            RChartCDProBilling.Series.GetByName("Amount").Appearance.Shadow.Distance = 3;
        }

        protected void rbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}