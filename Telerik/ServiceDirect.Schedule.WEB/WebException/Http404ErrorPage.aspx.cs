using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiceDirect.Schedule.WEB.WebException
{
    public partial class Http404ErrorPage : System.Web.UI.Page
    {
        protected HttpException ex = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Log the exception and notify system operators
            ex = new HttpException("HTTP 404");
            ExceptionUtility.LogException(ex, "Caught in Http404ErrorPage");
            ExceptionUtility.NotifySystemOps(ex);
        }
    }
}