using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace WCF.ServiceLib.Message
{
    /// <summary>
    /// 演示MSMQ的类
    /// </summary>
    public class MSMQ : IMSMQ
    {
        /// <summary>
        /// 将字符串写入文本文件
        /// </summary>
        /// <param name="str">需要写入文本文件的字符串</param>
        public void Write(string str)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(@"C:\WCF_Log_MSMQ.txt", true);
            sw.Write(str);
            sw.WriteLine();
            sw.Close();
        }
    }
}
