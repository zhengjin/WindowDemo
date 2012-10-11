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

public partial class Contract_PersonManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnGetName_Click(object sender, EventArgs e)
    {
        // Contract.IPersonManager pm = new Contract.PersonManagerClient();

        Contract.PersonManagerClient proxy = new Contract.PersonManagerClient();

        Contract.StudentModel sm = new Contract.StudentModel() { PersonName = txtName.Text };

        Page.ClientScript.RegisterStartupScript(
            this.GetType(),
            "js",
            string.Format("alert('{0}')", proxy.GetPersonName(sm)),
            true);

        proxy.Close();
    }
}
