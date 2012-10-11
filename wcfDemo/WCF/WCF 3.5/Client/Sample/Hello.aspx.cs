﻿using System;
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

public partial class Sample_Hello : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSayHello_Click(object sender, EventArgs e)
    {
        Sample.HelloClient proxy = new Sample.HelloClient();

        Page.ClientScript.RegisterStartupScript(
            this.GetType(),
            "js",
            string.Format("alert('{0}')", proxy.SayHello(txtName.Text)),
            true);

        proxy.Close();
    }
}
