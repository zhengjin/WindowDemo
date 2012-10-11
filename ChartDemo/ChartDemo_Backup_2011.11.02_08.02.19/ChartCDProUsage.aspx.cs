using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Charting;

namespace ChartDemo
{
    public partial class ChartCDProUsage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.UsageChartDataBinding();
            }
        }

        protected void SubtypeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void UsageChartDataBinding()
        {

        }
    }
}