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
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnPerCallMode_Click(object sender, EventArgs e)
    {

        var proxy = new InstanceModeSvc.PerCallMode.PerCallModeClient();

        Page.ClientScript.RegisterStartupScript(
            this.GetType(),
            "js",
            string.Format("alert('计数器：{0}')", proxy.Counter()),
            true);

        proxy.Close();
    }

    protected void btnPerSessionMode_Click(object sender, EventArgs e)
    {
        if (Session["proxy"] == null)
            Session["proxy"] = new InstanceModeSvc.PerSessionMode.PerSessionModeClient();

        Page.ClientScript.RegisterStartupScript(
            this.GetType(),
            "js",
            string.Format("alert('计数器：{0}')", (Session["proxy"] as InstanceModeSvc.PerSessionMode.PerSessionModeClient).Counter()),
            true);
    }

    protected void btnSingleMode_Click(object sender, EventArgs e)
    {
        var proxy = new InstanceModeSvc.SingleMode.SingleModeClient();

        Page.ClientScript.RegisterStartupScript(
            this.GetType(),
            "js",
            string.Format("alert('计数器：{0}')", proxy.Counter()),
            true);

        proxy.Close();
    }
}
