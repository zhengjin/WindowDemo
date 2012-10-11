using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;


namespace SessionDemo
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["01"] = "123";
                Session["02"] = "456";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //删除会话状态集合中的项。
            Session.Remove("01");
            //取消当前会话
            Session.Abandon();
        }
        public string aa()
        {
            string str = "";
            return str;
        }
        [WebMethod]
        public static string SessionExit(string ABC)
        {
            //删除会话状态集合中的项。
            HttpContext.Current.Session.Remove("01");
            //取消当前会话
            HttpContext.Current.Session.Abandon();
            Global g = new Global();
            
            return g.strs;
        }
    }
}