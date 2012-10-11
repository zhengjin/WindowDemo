using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GoogleMapDemo
{
    public partial class _Default : System.Web.UI.Page
    {
        public string strDate;
        protected void Page_Load(object sender, EventArgs e)
        {
            //GetNameCookie();
        }

        public string GetNameCookie()
        {
                    return strDate=DateTime.Now.ToString("MM/dd/yyyy");
        }
    }
}
