using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HHSoft.FieldProtect.FrameWork.Utility
{
    public class FileHelper
    {
        /// <summary>
        /// 递归创建文件夹。
        /// </summary>
        /// <param name="directoryInfo">文件夹信息对象。</param>
        public void CreateDirectory(DirectoryInfo directoryInfo)
        {
            if (!directoryInfo.Parent.Exists)
            {
                CreateDirectory(directoryInfo.Parent);
            }
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
        }

        public void ClearDirectory(DirectoryInfo directoryInfo)
        {
            foreach (var fileInfo in directoryInfo.GetFiles())
            {
                fileInfo.Delete();
            }
            foreach (var childDirectoryInfo in directoryInfo.GetDirectories())
            {
                childDirectoryInfo.Delete(true);
            }
        }

        public void CreateFile(string path, string content)
        {
            StreamWriter sw = new StreamWriter(path);
            sw.Write(content);
            sw.Flush();
            sw.Close();
        }

        public void DeleteFile(string path)
        {
            File.Delete(path);
        }

        public string GetTextContent(string path)
        {
            StreamReader sr = new StreamReader(path);
            string content = sr.ReadToEnd();
            sr.Close();
            return content;
        }

        public string GetFileNameWithoutExtension(string fileName)
        {
            int index = fileName.LastIndexOf('.');
            return fileName.Remove(index);
        }
    }
}
