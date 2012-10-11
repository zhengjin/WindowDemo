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

using System.ServiceModel;

public partial class Exception_Hello : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnHelloException_Click(object sender, EventArgs e)
    {
        ExceptionService.HelloClient proxy = new ExceptionService.HelloClient();
        try
        {
            proxy.HelloException();
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
        finally
        {
            try
            {
                proxy.Close();
            }
            catch (Exception ex)
            {
                lblMsg.Text += "<br />" + ex.Message;
            }
        }
    }

    protected void btnHelloFaultException_Click(object sender, EventArgs e)
    {
        ExceptionService.HelloClient proxy = new ExceptionService.HelloClient();
        try
        {
            proxy.HelloFaultException();
        }
        catch (FaultException ex)
        {
            lblMsg.Text = string.Format("错误编码：{0}；错误原因：{1}",
                ex.Code.Name,
                ex.Reason.ToString());
        }
        finally
        {
            proxy.Close();
        }
    }

    protected void btnHelloFaultExceptionGeneric_Click(object sender, EventArgs e)
    {
        ExceptionService.HelloClient proxy = new ExceptionService.HelloClient();
        try
        {
            proxy.HelloFaultExceptionGeneric();
        }
        catch (System.ServiceModel.FaultException<ExceptionService.FaultMessage> ex)
        {
            lblMsg.Text = string.Format("错误代码：{0}；错误信息：{1}；错误原因：{2}",
                ex.Detail.ErrorCode.ToString(),
                ex.Detail.Message,
                ex.Reason.ToString());
        }
        finally
        {
            proxy.Close();
        }
    }

    protected void btnHelloIErrorHandler_Click(object sender, EventArgs e)
    {
        ExceptionService.HelloClient proxy = new ExceptionService.HelloClient();
        try
        {
            proxy.HelloIErrorHandler();
        }
        catch (Exception ex)
        {
            System.ServiceModel.FaultException faultException = ex as System.ServiceModel.FaultException;

            if (faultException != null)
            {
                lblMsg.Text = string.Format("错误信息：{0}", faultException.Message);
            }
            else
            {
                lblMsg.Text = ex.ToString();
            }
        }
        finally
        {
            proxy.Close();
        }
    }
}
