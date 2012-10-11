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

public partial class TelerikWebForm : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // Populate the chart
            DataTable tbl = new DataTable();
            DataColumn col = new DataColumn("Temp");
            col.DataType = typeof(int);
            tbl.Columns.Add(col);
            col = new DataColumn("Measurement");
            col.DataType = typeof(string);
            tbl.Columns.Add(col);

            int size = 20;
            int maxLen = size.ToString().Length;
            Random r = new Random();
            for (int i = 1; i <= size; i++)
            {
                tbl.Rows.Add(new object[] { r.Next(40), "Measurement " + i.ToString("D" + maxLen) });
            }
            RadChart1.DataSource = tbl;
            RadChart1.DataBind();
        }
    }
    protected void RadChart1_ItemDataBound(object sender, Telerik.Charting.ChartItemDataBoundEventArgs e)
    {
        if (e.SeriesItem.YValue > 30)
        {
            e.SeriesItem.ActiveRegion.Tooltip = "Attention! Temperature too high! " + '\n';
        }
        else if (e.SeriesItem.YValue < 10)
        {
            e.SeriesItem.ActiveRegion.Tooltip = "Attention! Temperature too low! " + '\n';
        }
        e.SeriesItem.ActiveRegion.Tooltip += ((DataRowView)e.DataItem)["Measurement"].ToString() + ": Temperature: " + e.SeriesItem.YValue;
    }
}
