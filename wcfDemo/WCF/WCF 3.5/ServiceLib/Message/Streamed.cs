using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.IO;

namespace WCF.ServiceLib.Message
{
    /// <summary>
    /// IStreamed类
    /// </summary>
    public class Streamed : IStreamed
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileWrapper">WCF.ServiceLib.Message.FileWrapper</param>
        public void UploadFile(FileWrapper fileWrapper)
        {
            var sourceStream = fileWrapper.FileData;

            var targetStream = new FileStream(fileWrapper.FilePath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None);

            var buffer = new byte[4096];
            var count = 0;

            while ((count = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                targetStream.Write(buffer, 0, count);
            }

            targetStream.Close();
            sourceStream.Close();
        }
    }
}
