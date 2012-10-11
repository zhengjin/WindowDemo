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

using System.Threading;

public partial class Transaction_Sample : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        var proxyHello = new TransactionSvc.Hello.HelloClient();
        var proxyHi = new TransactionSvc.Hi.HiClient();
        var proxyResult = new TransactionSvc.Result.ResultClient();

        System.Transactions.TransactionOptions to = new System.Transactions.TransactionOptions();
        // 设置事务的超时时间
        to.Timeout = new TimeSpan(0, 0, 30);
        // 设置事务的隔离级别
        to.IsolationLevel = System.Transactions.IsolationLevel.Serializable;

        using (var ts = new System.Transactions.TransactionScope())
        {
            try
            {
                proxyHello.WriteHello("webabcd");
                proxyHello.Close();

                proxyHi.WriteHi("webabcd");
                proxyHi.Close();

                ts.Complete();

                lblErr.Text = "OK";
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.ToString();
            }
        }

        GridView1.DataSource = proxyResult.GetResult();
        GridView1.DataBind();
        proxyHello.Close();
    }
}
