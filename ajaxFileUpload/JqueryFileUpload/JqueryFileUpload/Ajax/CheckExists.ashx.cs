using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JqueryFileUpload.Ajax
{
    /// <summary>
    /// CheckExists 的摘要说明
    /// </summary>
    public class CheckExists : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";

            HttpPostedFile file = context.Request.Files["Filedata"];
            string uploadPath =
                HttpContext.Current.Server.MapPath(@"~/Upload") + "\\";

            if (file != null)
            {
                context.Response.Write("0");
            }
            else
            {
                context.Response.Write("1");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}