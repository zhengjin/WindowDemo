using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace Client2.Message
{
    /// <summary>
    /// 实现回调接口
    /// </summary>
    public class CallbackType : MessageSvc.Duplex.IDuplexCallback
    {
        /// <summary>
        /// Hello
        /// </summary>
        /// <param name="name">名字</param>
        public void HelloDuplexCallback(string name)
        {
            MessageBox.Show("Hello: " + name);
        }
    }
}
