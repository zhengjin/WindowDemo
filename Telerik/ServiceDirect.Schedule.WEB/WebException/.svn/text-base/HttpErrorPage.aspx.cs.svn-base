using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiceDirect.Schedule.WEB.WebException
{
    public partial class HttpErrorPage : System.Web.UI.Page
    {
        protected HttpException ex = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            ex = (HttpException)Server.GetLastError();
            int httpCode = ex.GetHttpCode();

            // Filter for Error Codes and set text
            if (httpCode >= 400 && httpCode < 500)
                ex = new HttpException
                    (httpCode, "Safe message for 4xx HTTP codes.", ex);
            else if (httpCode > 499)
                ex = new HttpException
                    (ex.ErrorCode, "Safe message for 5xx HTTP codes.", ex);
            else
                ex = new HttpException
                    (httpCode, "Safe message for unexpected HTTP codes.", ex);

            // Log the exception and notify system operators
            ExceptionUtility.LogException(ex, "HttpErrorPage");
            ExceptionUtility.NotifySystemOps(ex);

            // Fill the page fields
            exMessage.Text = ex.Message;
            exTrace.Text = ex.StackTrace;

            // Show Inner Exception fields for local access
            if (ex.InnerException != null)
            {
                innerTrace.Text = ex.InnerException.StackTrace;
                InnerErrorPanel.Visible = Request.IsLocal;
                innerMessage.Text = string.Format("HTTP {0}: {1}",
                  httpCode, ex.InnerException.Message);
            }
            // Show Trace for local access
            exTrace.Visible = Request.IsLocal;

            // Clear the error from the server
            Server.ClearError();

        }
    }
}