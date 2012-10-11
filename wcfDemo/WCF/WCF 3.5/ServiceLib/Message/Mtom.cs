using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.IO;

namespace WCF.ServiceLib.Message
{
    /// <summary>
    /// Mtom类
    /// </summary>
    public class Mtom : IMtom
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="path">文件目标路径</param>
        /// <param name="fileData">文件字节数组</param>
        public void UploadFile(string path, byte[] fileData)
        {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            fs.Write(fileData, 0, fileData.Length);
            fs.Flush();
            fs.Close();
        }
    }
}
