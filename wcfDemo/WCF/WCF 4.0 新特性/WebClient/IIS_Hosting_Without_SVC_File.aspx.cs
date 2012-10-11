using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClient
{
    public partial class IIS_Hosting_Without_SVC_File : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IIS_Hosting_Without_SVC_File_ServiceReference.DemoClient client = new IIS_Hosting_Without_SVC_File_ServiceReference.DemoClient();
            Response.Write(client.Hello("webabcd"));
        }
    }
}