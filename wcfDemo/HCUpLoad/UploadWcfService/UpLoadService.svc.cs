using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;

namespace UploadWcfService
{
    // 注意: 如果更改此处的类名 "UpLoadService"，也必须更新 Web.config 中对 "UpLoadService" 的引用。
    public class UpLoadService : IUpLoadService
    {
        public void UploadFile(FileUploadMessage request)
        {
            string uploadFolder = @"C:\kkk\";
            string savaPath = request.SavePath;
            string dateString = DateTime.Now.ToShortDateString() + @"\";
            string fileName = request.FileName;
            Stream sourceStream = request.FileData;
            FileStream targetStream = null;
           
            if (!sourceStream.CanRead)
            {
                throw new Exception("数据流不可读!");
            }
            if (savaPath == null) savaPath = @"Photo\";
            if (!savaPath.EndsWith("\\")) savaPath += "\\";

            uploadFolder = uploadFolder + savaPath + dateString;
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            string filePath = Path.Combine(uploadFolder, fileName);
            using (targetStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                //read from the input stream in 4K chunks
                //and save to output stream
                const int bufferLen = 4096;
                byte[] buffer = new byte[bufferLen];
                int count = 0;
                while ((count = sourceStream.Read(buffer, 0, bufferLen)) > 0)
                {
                    targetStream.Write(buffer, 0, count);
                }
                targetStream.Close();
                sourceStream.Close();
            }
        }

    }
}
