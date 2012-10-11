using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.ServiceModel;

namespace Client2.Message
{
    /// <summary>
    /// 演示Message.OneWay的类
    /// </summary>
    public class OneWay
    {
        /// <summary>
        /// 调用IsOneWay=true的操作契约（异步操作）
        /// </summary>
        public void HelloWithOneWay()
        {
            try
            {
                var proxy = new MessageSvc.OneWay.OneWayClient();

                proxy.WithOneWay();

                proxy.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 调用IsOneWay=false的操作契约（同步操作）
        /// </summary>
        public void HelloWithoutOneWay()
        {
            try
            {
                var proxy = new MessageSvc.OneWay.OneWayClient();

                proxy.WithoutOneWay();

                proxy.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
