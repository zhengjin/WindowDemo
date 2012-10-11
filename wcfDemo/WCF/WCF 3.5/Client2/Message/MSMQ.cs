using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.ServiceModel;

namespace Client2.Message
{
    /// <summary>
    /// 演示Message.MSMQ的类
    /// </summary>
    public class MSMQ
    {
        /// <summary>
        /// 用于测试 MSMQ 的客户端
        /// </summary>
        /// <param name="str">需要写入文本文件的字符串</param>
        public void HelloMSMQ(string str)
        {
            using (var proxy = new MessageSvc.MSMQ.MSMQClient())
            {
                proxy.Write(str);
            }
        }      
    }
}
