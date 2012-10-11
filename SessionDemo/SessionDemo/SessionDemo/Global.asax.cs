using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace SessionDemo
{
    public class Global : System.Web.HttpApplication
    {
        public string strs="";
        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            if (Session["01"] != null)
            {
                string str = Session["01"].ToString();
            }
            else
            {
                strs = "01用户退出系统！！！"; 
            }
            if (Session["02"] != null)
            {
                string str2 = Session["02"].ToString();
            }
            else
            {
                strs = "02用户退出系统！！！";
            }

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}