using System;

using System.Data;
using System.Collections.Generic;
using System.Drawing;
using Telerik.Charting;

namespace ChartDemo
{
    public partial class _Default : System.Web.UI.Page
    {
        private DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                const string skin = "Telerik";

                //调用绑定函数
                this.TransactionChartDataBinding();
                this.CustomerStatusChartDataBinding();
                this.PaymentChartDataBinding();
                this.UsageChartDataBinding();
                this.BillingChartDataBinding();

                //皮肤
                RChartCDProTransaction.Skin = skin;
                //RChartCDProUsage.Skin = skin;
                RChartCDProCustomer.Skin = skin;

                RChartCDProBilling.Skin = skin;
                RChartCDProPayment.Skin = skin;
            }

            this.BindingGridView();
        }

        #region 线图绑定数据
        /// <summary>
        /// 每月交易数据线图绑定
        /// </summary>
        protected void TransactionChartDataBinding()
        {
            int[] array = { 111, 21, 116, 234, 118, 223, 114, 310, 225, 243, 1252, 102, 234 };
            //获取当前年的月底日期
            string firstMothDateTimeStr =
                DateTime.Parse(DateTime.Now.ToString("01/01/yyyy")).AddMonths(1).AddDays(-1).ToShortDateString();
            DateTime firstMothDate = DateTime.Parse(firstMothDateTimeStr);

            //生成Chart的X轴角标
            dt = new DataTable();
            DataColumn transaction = new DataColumn("Total");
            transaction.DataType = typeof(int);
            dt.Columns.Add(transaction);
            transaction = new DataColumn("month");
            transaction.DataType = typeof(string);
            dt.Columns.Add(transaction);

            //统计当前月份的前N个月的数据
            for (int i = DateTime.Now.Month - 7; i < DateTime.Now.Month - 1; i++)
            {
                DateTime currentDate = firstMothDate.AddMonths(i);

                dt.Rows.Add(new object[] { array[i], currentDate.ToString("MM/yyyy") });
            }

            RChartCDProTransaction.DataSource = dt;
            RChartCDProTransaction.DataBind();
        }

        /// <summary>
        /// 每月Account的状态变化情况
        /// </summary>
        protected void CustomerStatusChartDataBinding()
        {
            //acount的状态
            int[] actb = { 11, 22, 216, 134, 218, 213, 214, 110, 425, 143, 452, 202, 334 };
            int[] woff = { 101, 212, 116, 164, 318, 513, 204, 310, 25, 343, 52, 222, 234 };
            int[] actn = { 110, 242, 316, 114, 418, 113, 114, 10, 325, 143, 252, 302, 134 };

            //获取当前年的月底日期
            string firstMothDateTimeStr =
                DateTime.Parse(DateTime.Now.ToString("01/01/yyyy")).AddMonths(1).AddDays(-1).ToShortDateString();
            DateTime firstMothDate = DateTime.Parse(firstMothDateTimeStr);

            //生成Chart的X轴角标
            dt = new DataTable();
            DataColumn customerACTB = new DataColumn("ACTB");
            customerACTB.DataType = typeof(int);
            dt.Columns.Add(customerACTB);

            DataColumn customerWOFF = new DataColumn("WOFF");
            customerWOFF.DataType = typeof(int);
            dt.Columns.Add(customerWOFF);

            DataColumn customerACTN = new DataColumn("ACTN");
            customerACTN.DataType = typeof(int);
            dt.Columns.Add(customerACTN);

            customerACTB = new DataColumn("month");
            customerACTB.DataType = typeof(string);
            dt.Columns.Add(customerACTB);

            //统计当前月份的前N个月的数据
            for (int i = DateTime.Now.Month - 7; i < DateTime.Now.Month - 1; i++)
            {
                DateTime currentDate = firstMothDate.AddMonths(i);

                dt.Rows.Add(new object[] { actb[i], woff[i], actn[i], currentDate.ToString("MM/yyyy") });
            }
            RChartCDProCustomer.DataSource = dt;
            RChartCDProCustomer.DataBind();
        }

        protected void PaymentChartDataBinding()
        {
            int[] array = { 211, 121, 116, 34, 418, 423, 114, 510, 425, 243, 1252, 102, 134 };
            //获取当前年的月底日期
            string firstMothDateTimeStr =
                DateTime.Parse(DateTime.Now.ToString("01/01/yyyy")).AddMonths(1).AddDays(-1).ToShortDateString();
            DateTime firstMothDate = DateTime.Parse(firstMothDateTimeStr);

            //生成Chart的X轴角标
            dt = new DataTable();
            DataColumn Payment = new DataColumn("Amount");
            Payment.DataType = typeof(int);
            dt.Columns.Add(Payment);
            Payment = new DataColumn("QuarterDate");
            Payment.DataType = typeof(string);
            dt.Columns.Add(Payment);

            //统计当前月份的前N个月的数据
            for (int i = DateTime.Now.Month - 7; i < DateTime.Now.Month - 1; i++)
            {
                DateTime currentDate = firstMothDate.AddMonths(i);

                dt.Rows.Add(new object[] { array[i], currentDate.ToString("MM/yyyy") });
            }
            RChartCDProPayment.DataSource = dt;
            RChartCDProPayment.DataBind();
        }

        protected void RChartCDProPayment_ItemDataBound(object sender, Telerik.Charting.ChartItemDataBoundEventArgs e)
        {
            Random r = new Random();
            e.SeriesItem.ActiveRegion.Tooltip += "Pay the total number: " + r.Next(40) + ". <br/>Total amount: "
                + String.Format("{0:C}", e.SeriesItem.YValue);
        }

        protected void UsageChartDataBinding()
        {
            Telerik.Charting.ChartSeries s = new Telerik.Charting.ChartSeries();

            RChartCDProUsage.Series.Add(s);

            List<UsageItem> us = new List<UsageItem>();

            DataView dv = (DataView)SqlDataSource1.Select(System.Web.UI.DataSourceSelectArguments.Empty);

            if (dv != null)
            {
                foreach (DataRowView dr in dv)
                {
                    ChartSeriesItem item = new ChartSeriesItem();

                    item.Name=dr["BillDt"].ToString();
                    item.YValue=(double)dr["UsageSummary"];
                    item.Label.TextBlock.Text = (item.YValue/1000).ToString() + "K";

                    s.Items.Add(item);
                }

                RChartCDProUsage.Series[0].Appearance.LegendDisplayMode =
                    Telerik.Charting.ChartSeriesLegendDisplayMode.ItemLabels;//生成Item模式
                RChartCDProUsage.Series[0].Type = Telerik.Charting.ChartSeriesType.Pie;
                RChartCDProUsage.Series[0].Appearance.ShowLabelConnectors = true;
                RChartCDProUsage.Series[0].Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.ComplexGradient;
            }
        }

        protected void BillingChartDataBinding()
        {
            //获取当前年的月底日期
            string firstMothDateTimeStr =
                DateTime.Parse(DateTime.Now.ToString("01/01/yyyy")).AddMonths(1).AddDays(-1).ToShortDateString();
            DateTime firstMothDate = DateTime.Parse(firstMothDateTimeStr);

            //生成Chart的X轴角标
            dt = new DataTable();
            DataColumn transaction = new DataColumn("Amount");
            transaction.DataType = typeof(int);
            dt.Columns.Add(transaction);
            transaction = new DataColumn("month");
            transaction.DataType = typeof(string);
            dt.Columns.Add(transaction);

            GetInfo bllGetInfo = new GetInfo();

            //decimal usageAmount = bllGetInfo.GetBillingInfo();
            //统计当前月份的前N个月的数据
            for (int i = DateTime.Now.Month - 7; i < DateTime.Now.Month - 1; i++)
            {
                DateTime currentDate = firstMothDate.AddMonths(i);

                dt.Rows.Add(new object[] { bllGetInfo.GetBillingInfo(currentDate), currentDate.ToString("MM/yyyy") });
            }
            RChartCDProBilling.DataSource = dt;
            RChartCDProBilling.DataBind();
        }
        #endregion

        #region chart的单击事件
        protected void RChartCDProUsage_Click(object sender, Telerik.Charting.ChartClickEventArgs args)
        {
            Response.Redirect("~/ChartCDProUsage.aspx");
        }

        protected void RChartCDProBilling_Click(object sender, Telerik.Charting.ChartClickEventArgs args)
        {
            Response.Redirect("~/ChartCDProBilling.aspx");
        }

        protected void RadButton4_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ChartCDProBilling.aspx");
        }

        protected void RadButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ChartCDProUsage.aspx");
        }
        #endregion

        protected void Page_PreRender(object sender, EventArgs e)
        {
            const int lineWidth = 5;//线图宽度

            //柱状图阴影
            RChartCDProTransaction.Series.GetByName("Total").Appearance.BarWidthPercent = 40;
            RChartCDProTransaction.Series.GetByName("Total").Appearance.Shadow.Distance = 3;

            RChartCDProPayment.Series.GetByName("Amount").Appearance.BarWidthPercent = 40;
            RChartCDProPayment.Series.GetByName("Amount").Appearance.Shadow.Distance = 3;

            RChartCDProBilling.Series.GetByName("Amount").Appearance.BarWidthPercent = 40;
            
            //设置线图的粗细，颜色
            RChartCDProCustomer.Series.GetByName("ACTB").Appearance.FillStyle.MainColor = Color.FromArgb(238, 180, 180);
            RChartCDProCustomer.Series.GetByName("ACTB").Appearance.LineSeriesAppearance.Width = lineWidth;
            RChartCDProCustomer.Series.GetByName("WOFF").Appearance.FillStyle.MainColor = Color.FromArgb(158, 238, 180);
            RChartCDProCustomer.Series.GetByName("WOFF").Appearance.LineSeriesAppearance.Width = lineWidth;
            RChartCDProCustomer.Series.GetByName("ACTN").Appearance.FillStyle.MainColor = Color.FromArgb(138, 180, 180);
            RChartCDProCustomer.Series.GetByName("ACTN").Appearance.LineSeriesAppearance.Width = lineWidth;
        }

        protected void BindingGridView()
        {
            GetInfo bllGetInfo = new GetInfo();
            List<ViewTopCustomer> viewTopCustomerList=bllGetInfo.GetTopCustomer();

            rGridCustomer.DataSource = viewTopCustomerList;
            rGridCustomer.DataBind();
        }

        protected void RChartCDProUsage_BeforeLayout(object sender, EventArgs e)
        {
            //获取当前年的月底日期
            //string firstMothDateTimeStr =
            //    DateTime.Parse(DateTime.Now.ToString("01/01/yyyy")).AddMonths(1).AddDays(-1).ToShortDateString();
            //DateTime firstMothDate = DateTime.Parse(firstMothDateTimeStr);
            
            //for (int i = DateTime.Now.Month - 7; i < DateTime.Now.Month - 1; i++)
            //{
            //    DateTime currentDate = firstMothDate.AddMonths(i);
            //    int itemIndex=i-(DateTime.Now.Month - 7);
            //    RChartCDProUsage.Legend.Items[itemIndex].TextBlock.Text = 
            //        currentDate.ToString("MM/yyyy");
            //}
            
            //RChartCDProUsage.Series[0].Items[0].Appearance.FillStyle.MainColor = Color.Red;
        }

        protected void RChartCDProUsage_ItemDataBound(object sender, ChartItemDataBoundEventArgs e)
        {
            e.SeriesItem.Appearance.FillStyle.MainColor = Color.SeaGreen;
            e.SeriesItem.Appearance.FillStyle.SecondColor = Color.Red;
        }
    }
}
