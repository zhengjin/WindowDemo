using System;

using System.Data;

namespace ChartDemo
{
    public partial class _Default : System.Web.UI.Page
    {
        private DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //调用绑定函数
                this.TransactionChartDataBinding();
                this.CustomerStatusChartDataBinding();
                this.PaymentChartDataBinding();
                this.UsageChartDataBinding();

                //皮肤
                RChartCDProTransaction.Skin = "Telerik";
                RChartCDProUsage.Skin = "Telerik";
                RChartCDProCustomer.Skin = "Telerik";

                RChartCDProBilling.Skin = "Telerik";
                RChartCDProPayment.Skin = "Telerik";
            }
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
                DateTime.Parse(DateTime.Now.ToString("01/dd/yyyy")).AddMonths(1).AddDays(-1).ToShortDateString();
            DateTime firstMothDate = DateTime.Parse(firstMothDateTimeStr);

            //生成Chart的X轴角标
            dt = new DataTable();
            DataColumn transaction = new DataColumn("Transaction total");
            transaction.DataType = typeof(int);
            dt.Columns.Add(transaction);
            transaction = new DataColumn("month");
            transaction.DataType = typeof(string);
            dt.Columns.Add(transaction);

            //统计当前月份的前N个月的数据
            for (int i = 0; i < DateTime.Now.Month; i++)
            {
                DateTime currentDate = firstMothDate.AddMonths(i);

                dt.Rows.Add(new object[] { array[i], currentDate.ToString("MM/dd/yyyy") });
            }

            RChartCDProTransaction.DataSource = dt;
            RChartCDProTransaction.DataBind();



            RChartCDProBilling.DataSource = dt;
            RChartCDProBilling.DataBind();
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
                DateTime.Parse(DateTime.Now.ToString("01/dd/yyyy")).AddMonths(1).AddDays(-1).ToShortDateString();
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
            for (int i = 0; i < DateTime.Now.Month; i++)
            {
                DateTime currentDate = firstMothDate.AddMonths(i);

                dt.Rows.Add(new object[] { actb[i], woff[i], actn[i], currentDate.ToString("MM/dd/yyyy") });
            }
            RChartCDProCustomer.DataSource = dt;
            RChartCDProCustomer.DataBind();
        }

        protected void PaymentChartDataBinding()
        {
            int[] array = { 211, 121, 116, 34, 418, 423, 114, 510, 425, 243, 1252, 102, 134 };
            //获取当前年的月底日期
            string firstMothDateTimeStr =
                DateTime.Parse(DateTime.Now.ToString("01/dd/yyyy")).AddMonths(1).AddDays(-1).ToShortDateString();
            DateTime firstMothDate = DateTime.Parse(firstMothDateTimeStr);

            //生成Chart的X轴角标
            dt = new DataTable();
            DataColumn Payment = new DataColumn("Payment amount");
            Payment.DataType = typeof(int);
            dt.Columns.Add(Payment);
            Payment = new DataColumn("QuarterDate");
            Payment.DataType = typeof(string);
            dt.Columns.Add(Payment);

            //统计当前月份的前N个月的数据
            for (int i = 0; i < DateTime.Now.Month; i++)
            {
                DateTime currentDate = firstMothDate.AddMonths(i);

                dt.Rows.Add(new object[] { array[i], currentDate.ToString("MM/dd/yyyy") });
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
            //获取当前年的月底日期
            string firstMothDateTimeStr =
                DateTime.Parse(DateTime.Now.ToString("01/dd/yyyy")).AddMonths(1).AddDays(-1).ToShortDateString();
            DateTime firstMothDate = DateTime.Parse(firstMothDateTimeStr);

            //生成Chart的X轴角标
            dt = new DataTable();
            DataColumn transaction = new DataColumn("Transaction total");
            transaction.DataType = typeof(int);
            dt.Columns.Add(transaction);
            transaction = new DataColumn("month");
            transaction.DataType = typeof(string);
            dt.Columns.Add(transaction);

            GetInfo bllGetInfo = new GetInfo();

            //decimal usageAmount = bllGetInfo.GetBillingInfo();
            //统计当前月份的前N个月的数据
            for (int i = 0; i < DateTime.Now.Month; i++)
            {
                DateTime currentDate = firstMothDate.AddMonths(i);

                dt.Rows.Add(new object[] { bllGetInfo.GetBillingInfo(currentDate), currentDate.ToString("MM/dd/yyyy") });
            }
            RChartCDProUsage.DataSource = dt;
            RChartCDProUsage.DataBind();
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
        #endregion
    }
}
