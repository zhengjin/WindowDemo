using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Net;

namespace WebClient
{
    public partial class RestDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Net.WebClient client = new System.Net.WebClient();

            var jsonResult = client.DownloadString("http://localhost:14802/RestDemo.svc/Hello/webabcd");

            Response.Write(jsonResult);
        }
    }
}