using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using CDPro.BLL;

namespace cssControl
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string hostAdress = HttpContext.Current.Request.Url.Host;
            //XmlNode section = (XmlNode)ConfigurationSettings.GetConfig("SkinSetion");

            //List<SkinsObject> skinOjbList = SkinSectionHandler.GetSkinList(section);
            Label1.Text = hostAdress;
            if (hostAdress.StartsWith("www."))//域名以www.test.com开头的截取，为test.com
                hostAdress = hostAdress.Substring(4);

            Label2.Text = hostAdress;
            switch (hostAdress)
            {
                case "test.com":
                    Response.Redirect("~/LoginFolder/LoginPage.aspx");
                    break;
                case "test1.com":
                    Response.Redirect("~/LoginFolder/LoginPage1.aspx");
                    break;
                default:
                    break;
            }
        }
    }
}