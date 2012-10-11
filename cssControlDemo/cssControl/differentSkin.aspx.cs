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
    public partial class differentSkin : System.Web.UI.Page
    {
        private string skinId;

        private List<SkinsObject> skinOjbList;

        protected void Page_Load(object sender, EventArgs e)
        {
            string hostAdress = HttpContext.Current.Request.Url.Host;
            Label1.Text = hostAdress;
            if (hostAdress.StartsWith("www."))//域名以www.test.com开头的截取，为test.com
                hostAdress = hostAdress.Substring(4);
            XmlNode section = (XmlNode)ConfigurationSettings.GetConfig("SkinSetion");
            
            skinOjbList=SkinSectionHandler.GetSkinList(section);
            if (!string.IsNullOrEmpty(hostAdress))
            {
                foreach (var item in skinOjbList)
                {
                    if (item.HostAddress == hostAdress)
                    {
                        skinId = item.SkinId;
                        break;
                    }
                }
            }
            ChangeSkins();
        }

        protected void SkinsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadFormDecorator1.Skin = ((DropDownList)sender).SelectedValue;
        }

        protected void ChangeSkins()
        {
            if (!string.IsNullOrEmpty(skinId))
            {
                RadFormDecorator1.Skin = skinId;
                SkinsList.SelectedValue = skinId;
                RadButton1.Skin = skinId;
            }
            else
            {
                RadFormDecorator1.Skin = "WebBlue";
                SkinsList.SelectedValue = "WebBlue";
            }

        }

        protected void DecoratedControlsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedControlToDecorate = Int32.Parse(((RadioButtonList)sender).SelectedValue);
            RadFormDecorator1.DecoratedControls =
                (Telerik.Web.UI.FormDecoratorDecoratedControls)selectedControlToDecorate;
        }
    }
}