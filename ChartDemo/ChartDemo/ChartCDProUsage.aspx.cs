using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Charting;
using System.Data;

namespace ChartDemo
{
    public partial class ChartCDProUsage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.UsageChartDataBinding();

                LoadCompany();
            }

            List<UsageItem> us = new List<UsageItem>();

            DataView dv = (DataView)SqlDataSource4.Select(System.Web.UI.DataSourceSelectArguments.Empty);

            if (dv != null)
            {
                foreach (DataRowView dr in dv)
                {
                    UsageItem v = new UsageItem();


                    v.BillDt = dr["BillDt"].ToString();
                    v.UsageSummary = (double)dr["UsageSummary"];

                    us.Add(v);
                }

                RadChart1.DataSource = us;
                RadChart1.Series[0].DataYColumn = "UsageSummary";
                RadChart1.PlotArea.XAxis.DataLabelsColumn = "BillDt";

                RadChart1.DataBind();
            }
        }

        protected void SubtypeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void UsageChartDataBinding()
        {

        }

        //装入公司信息
        private void LoadCompany()
        {
            using (MECEntities objDB = new MECEntities())
            {
                List<tblCompany> CompList = objDB.tblCompany.ToList<tblCompany>();

                cboCompany.Items.Clear();

                //设置空值
                Telerik.Web.UI.RadComboBoxItem item = new Telerik.Web.UI.RadComboBoxItem();

                item.Text = "N/A";
                item.Value = string.Empty;

                cboCompany.Items.Add(item);

                //装入公司数据
                foreach (tblCompany cp in CompList)
                {
                    item = new Telerik.Web.UI.RadComboBoxItem();

                    item.Value = cp.CompanyID;
                    item.Text = cp.CompName;

                    cboCompany.Items.Add(item);
                }
            }
        }

        protected void cboCompany_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadCycle(e.Value);
        }

        //装入对应公司的Cycle信息
        private void LoadCycle(string CompID)
        {
            using (MECEntities objDB = new MECEntities())
            {
                List<tblCycle> CycleList = objDB.tblCycle.Where("it.CompanyID=='" + CompID+"'").OrderBy("it.Cycle").ToList<tblCycle>();

                cboCycle.Items.Clear();

                Telerik.Web.UI.RadComboBoxItem item = new Telerik.Web.UI.RadComboBoxItem();

                item.Value = string.Empty;
                item.Text = "N/A";

                cboCycle.Items.Add(item);

                foreach (tblCycle cl in CycleList)
                {
                    item = new Telerik.Web.UI.RadComboBoxItem();

                    item.Value = cl.CycleID;
                    item.Text = cl.Cycle.ToString();

                    cboCycle.Items.Add(item);
                }
            }
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            if (cboCompany.SelectedItem.Value != string.Empty &&
                cboCycle.SelectedItem.Value != string.Empty &&
                !string.IsNullOrEmpty(txtAcctNum.Text) &&
                !string.IsNullOrWhiteSpace(txtAcctNum.Text))
            {
                //use viewUsageSummarybyAccount view
                List<UsageItem> us = new List<UsageItem>();

                DataView dv = (DataView)SqlDataSource3.Select(System.Web.UI.DataSourceSelectArguments.Empty);

                if (dv != null)
                {
                    foreach (DataRowView dr in dv)
                    {
                        UsageItem v = new UsageItem();


                        v.BillDt = dr["BillDt"].ToString();
                        v.UsageSummary = (double)dr["UsageSummary"];

                        us.Add(v);
                    }

                    RadChart1.DataSource = us;
                    RadChart1.Series[0].DataYColumn = "UsageSummary";
                    RadChart1.PlotArea.XAxis.DataLabelsColumn = "BillDt";

                    RadChart1.DataBind();
                }

            }
            else
            {
                if (cboCompany.SelectedItem.Value != string.Empty &&
                cboCycle.SelectedItem.Value != string.Empty)
                {
                    //use viewUsageSummaryByCycle view
                    List<UsageItem> us = new List<UsageItem>();

                    DataView dv = (DataView)SqlDataSource2.Select(System.Web.UI.DataSourceSelectArguments.Empty);

                    if (dv != null)
                    {
                        foreach (DataRowView dr in dv)
                        {
                            UsageItem v = new UsageItem();


                            v.BillDt = dr["BillDt"].ToString();
                            v.UsageSummary = (double)dr["UsageSummary"];

                            us.Add(v);
                        }

                        RadChart1.DataSource = us;
                        RadChart1.Series[0].DataYColumn = "UsageSummary";
                        RadChart1.PlotArea.XAxis.DataLabelsColumn = "BillDt";

                        RadChart1.DataBind();
                    }
                }
                else
                {
                    if (cboCompany.SelectedItem.Value != string.Empty)
                    {
                        List<UsageItem> us = new List<UsageItem>();

                        DataView dv = (DataView)SqlDataSource1.Select(System.Web.UI.DataSourceSelectArguments.Empty);

                        if (dv != null)
                        {
                            foreach (DataRowView dr in dv)
                            {
                                UsageItem v = new UsageItem();


                                v.BillDt = dr["BillDt"].ToString();
                                v.UsageSummary = (double)dr["UsageSummary"];

                                us.Add(v);
                            }

                            RadChart1.DataSource = us;
                            RadChart1.Series[0].DataYColumn = "UsageSummary";
                            RadChart1.PlotArea.XAxis.DataLabelsColumn = "BillDt";

                            RadChart1.DataBind();
                        }
                    }
                    else
                    {
                        //use all data
                        List<UsageItem> us = new List<UsageItem>();

                        DataView dv = (DataView)SqlDataSource4.Select(System.Web.UI.DataSourceSelectArguments.Empty);

                        if (dv != null)
                        {
                            foreach (DataRowView dr in dv)
                            {
                                UsageItem v = new UsageItem();


                                v.BillDt = dr["BillDt"].ToString();
                                v.UsageSummary = (double)dr["UsageSummary"];

                                us.Add(v);
                            }

                            RadChart1.DataSource = us;
                            RadChart1.Series[0].DataYColumn = "UsageSummary";
                            RadChart1.PlotArea.XAxis.DataLabelsColumn = "BillDt";

                            RadChart1.DataBind();
                        }
                    }
                }
            }
        }

        protected void rbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}