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

public partial class ConcurrencyLock_Hello : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btn_Command(object sender, CommandEventArgs e)
    {
        // 线程1
        var thread1 = new Thread(new ParameterizedThreadStart(Do));
        thread1.Start(e.CommandName);

        // 线程2
        var thread2 = new Thread(new ParameterizedThreadStart(Do));
        thread2.Start(e.CommandName);

        for (int i = 0; i < 100; i++)
        {
            Thread.Sleep(100);

            if (thread1.ThreadState == ThreadState.Stopped && thread2.ThreadState == ThreadState.Stopped)
            {
                // 返回服务端的技术器的调用结果
                var proxy = new ConcurrencyLockSvc.HelloClient();

                txtResult.Text = proxy.GetResult();

                proxy.Close();

                break;
            }
        }
    }

    private void Do(object commandName)
    {
        ConcurrencyLockSvc.LockType lockType = (ConcurrencyLockSvc.LockType)Enum.Parse(typeof(ConcurrencyLockSvc.LockType), (string)commandName);

        // 调用服务端技术器
        using (var proxy = new ConcurrencyLockSvc.HelloClient())
        {
            proxy.Counter(lockType);
        }
    }

    protected void btnCleanResult_Click(object sender, EventArgs e)
    {
        // 清空计数器和结果
        using (var proxy = new ConcurrencyLockSvc.HelloClient())
        {
            proxy.CleanResult();
        }

        txtResult.Text = "";
    }
}
