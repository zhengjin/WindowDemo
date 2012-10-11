using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClient
{
    public partial class SimplifiedConfiguration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SimplifiedConfiguration_ServiceReference.DemoClient client = new SimplifiedConfiguration_ServiceReference.DemoClient();
            Response.Write(client.Hello("webabcd"));
        }
    }
}