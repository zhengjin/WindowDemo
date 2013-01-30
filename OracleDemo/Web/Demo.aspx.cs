using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using Dosoft.DAL;
using System.Data;
namespace Web
{
    public partial class Demo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strSql = "select * from xxpoo_customer_details_v t";
            DataSet ds = new DataSet();

            ds= OracleHelper.Query(strSql);
            GridView1.DataSource = ds;
            GridView1.DataBind();            
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
        }

    }
}