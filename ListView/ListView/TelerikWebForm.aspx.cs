using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;

public partial class TelerikWebForm : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        EntityDataSource1.EntitySetName = "CardIssuer";
        EntityDataSource1.DefaultContainerName = "TestDataEntities";
        EntityDataSource1.ConnectionString = "name=TestDataEntities";

        EntityDataSource1.Where = "it.ImgId is not null";


        string tem = Request.Params["RadioGroup1"];
    }
}
