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

public partial class Sample_Security : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSayHello_Click(object sender, EventArgs e)
    {
        using (var proxy = new SecuritySvc.HelloClient())
        {
            try
            {
                // proxy.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.PeerTrust;

                proxy.ClientCredentials.UserName.UserName = txtUserName.Text;
                proxy.ClientCredentials.UserName.Password = txtPassword.Text;

                lblMsg.Text = proxy.SayHello(txtName.Text);
            }
            catch (TimeoutException ex)
            {
                lblMsg.Text = ex.ToString();
                proxy.Abort();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.ToString();
                proxy.Abort();
            }
        }
    }
}
