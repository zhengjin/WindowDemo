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

public partial class InstanceMode_Hello : System.Web.UI.Page
{
    SessionManagemenSvc.HelloClient _proxy = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["proxy"] == null)
            Session["proxy"] = new SessionManagemenSvc.HelloClient();

        _proxy = Session["proxy"] as SessionManagemenSvc.HelloClient;
    }

    protected void btnStartSession_Click(object sender, EventArgs e)
    {
        _proxy.StartSession();
    }

    protected void btnCounter_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(
            this.GetType(),
            "js",
            string.Format("alert('计数器：{0}')", _proxy.Counter()),
            true);
    }

    protected void btnGetSessionId_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(
           this.GetType(),
           "js",
           string.Format("alert('SessionId：{0}')", _proxy.GetSessionId()),
           true);
    }

    protected void btnStopSession_Click(object sender, EventArgs e)
    {
        _proxy.StopSession();
    }
}
