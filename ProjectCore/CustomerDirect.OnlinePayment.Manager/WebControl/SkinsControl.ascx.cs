using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.WebControl
{
    public partial class SkinsControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //设置微软的控件皮肤
                rfdCDPro.Skin =
                    GetGlobalResourceObject("en_US", "Site_DefaultSkin_Value").ToString();
                //设置telerik控件皮肤
                rsmSDPro.Skin =
                    GetGlobalResourceObject("en_US", "Site_DefaultSkin_Value").ToString();
            }
        }
    }
}