using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.ServiceModel;
using System.IO;

namespace Client2.Message
{
    /// <summary>
    /// 演示Message.Streamed的类
    /// </summary>
    public class Streamed
    {
        /// <summary>
        /// 流数据上传文件
        /// </summary>
        /// <param name="source">源文件地址</param>
        /// <param name="destination">目标路径</param>
        public void HelloStreamed(string source, string destination)
        {
            try
            {
                var proxy = new MessageSvc.Streamed.StreamedClient();

                var sr = new System.IO.FileStream(
                    source, System.IO.FileMode.Open);

                proxy.UploadFile(destination + Path.GetFileName(source), sr);

                sr.Close();
                proxy.Close();

                MessageBox.Show("上传成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
