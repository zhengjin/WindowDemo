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
using System.ServiceModel.Channels;
using System.IO;

public partial class Message_Reliable : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void btnReliable_Click(object sender, EventArgs e)
    {
        using (var proxy = new MessageSvc.Reliable.ReliableClient())
        {
            proxy.Write("1");
            System.Threading.Thread.Sleep(3000);
            proxy.Write("2");
        }
    }
}
