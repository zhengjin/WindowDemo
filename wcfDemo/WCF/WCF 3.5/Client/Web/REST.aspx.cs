using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Net;

public partial class Web_REST : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var client = new WebClient();

        var create = client.UploadString("http://localhost:3502/ServiceHost/Web/REST.svc/User/webabcd/1980-2-14", "PUT", string.Empty);
        var read = client.DownloadString("http://localhost:3502/ServiceHost/Web/REST.svc/User/webabcd");
        var update = client.UploadString("http://localhost:3502/ServiceHost/Web/REST.svc/User/webabcd/1980-2-14", "POST", string.Empty);
        var delete = client.UploadString("http://localhost:3502/ServiceHost/Web/REST.svc/User/webabcd", "DELETE", string.Empty);

        lblMsg.Text = string.Format("{0}<br />{1}<br />{2}<br />{3}",
            create,
            read,
            update,
            delete);
    }
}
