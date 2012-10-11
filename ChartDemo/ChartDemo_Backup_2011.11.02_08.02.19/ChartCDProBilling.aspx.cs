using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using Telerik.Charting;

namespace ChartDemo
{
    public partial class ChartCDProBilling : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                this.BillingChartDataBinding();
            }
        }

        protected void SubtypeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void BillingChartDataBinding()
        {
            
        }
    }
}