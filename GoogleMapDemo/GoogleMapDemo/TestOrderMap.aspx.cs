using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WEB.Business_Logic
{
    public partial class TestOrderMap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["Adress"] != null && Request.QueryString["Adress"] != string.Empty)
                {
                    HiddenField_testOrderAdress.Value = Request.QueryString["Adress"].ToString();
                }
                if (Request["Longitude"] != null && Request.QueryString["Longitude"] != string.Empty)
                {
                    HiddenField_Longitude.Value = Request.QueryString["Longitude"].ToString();
                }
                if (Request["Latitude"] != null && Request.QueryString["Latitude"] != string.Empty)
                {
                    HiddenField_Latitude.Value = Request.QueryString["Latitude"].ToString();
                }
            }
        }
    }
}