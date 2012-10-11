using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;
using System.Threading;

namespace HHSoft.FieldProtect.Framework.Utility
{
    /// <summary>
    /// 压缩帮助类
    /// </summary>
    public class ZipHelper
    {        

        /// <summary>
        /// 构造函数
        /// </summary>        
        public ZipHelper()
        {
            
        }

        public void Compress(string zipPath, string zipFile, List<string> fileList)
        {           
            ////压缩包名称不为空
            if (zipFile != string.Empty)
            {
                ////新建压缩文件流ZipOutputStream 
                ZipOutputStream u = new ZipOutputStream(File.Create(zipPath + "\\" + zipFile));

                foreach (string s in fileList)
                {
                    if (!string.IsNullOrEmpty(s)) this.AddZipEntry(zipPath, s, u, out u);
                }
                ////结束压缩   
                u.Finish();
                u.Close();
            }
        }


        /// <summary>
        /// 解压缩文件
        /// </summary>
        /// <param name="GzipFile">压缩包文件名</param>
        /// <param name="targetPath">解压缩目标路径</param>       
        public static void Decompress(string GzipFile, string targetPath)
        {
            //string directoryName = Path.GetDirectoryName(targetPath + "\\") + "\\";
            string directoryName = targetPath;
            if (!Directory.Exists(directoryName)) Directory.CreateDirectory(directoryName);//生成解压目录
            string CurrentDirectory = directoryName;

            byte[] data = new byte[2048];
            int size = 2048;

            ZipEntry theEntry = null;
            using (ZipInputStream s = new ZipInputStream(File.OpenRead(GzipFile)))
            {
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    if (theEntry.IsDirectory)
                    {// 该结点是目录
                        if (!Directory.Exists(CurrentDirectory + theEntry.Name)) Directory.CreateDirectory(CurrentDirectory + theEntry.Name);
                    }
                    else
                    {
                        if (theEntry.Name != String.Empty)
                        {
                            //解压文件到指定的目录
                            using (FileStream streamWriter = File.Create(CurrentDirectory + theEntry.Name))
                            {
                                while (true)
                                {
                                    size = s.Read(data, 0, data.Length);
                                    if (size <= 0) break;

                                    streamWriter.Write(data, 0, size);
                                }
                                streamWriter.Close();
                            }
                        }
                    }
                }
                s.Close();
            }
        }

        /// <summary>
        /// 加入压缩文件
        /// </summary>
        /// <param name="p">文件名</param>
        /// <param name="u">输入流</param>
        /// <param name="j">输出流</param>
        public void AddZipEntry(string zipPath, string p, ZipOutputStream u, out ZipOutputStream j)
        {
            string s = zipPath + "\\" + p;
            if (Directory.Exists(s))
            {
                ////文件夹的处理 
                DirectoryInfo di = new DirectoryInfo(s);
                ////没有子目录   
                if (di.GetDirectories().Length <= 0)
                {
                    ////末尾“\\”用于文件夹的标记
                    ZipEntry z = new ZipEntry(p + "\\");
                    u.PutNextEntry(z);
                }
                ////获取子目录
                foreach (DirectoryInfo tem in di.GetDirectories())
                {
                    ////末尾“\\”用于文件夹的标记
                    ZipEntry z = new ZipEntry(this.ShortDir(zipPath,tem.FullName) + "\\");
                    ////空目录添加  
                    u.PutNextEntry(z);
                    s = this.ShortDir(zipPath,tem.FullName);
                    ////递归
                    this.AddZipEntry(zipPath,s, u, out u);
                }
                ////获取此目录的文件   
                foreach (FileInfo temp in di.GetFiles())
                {
                    s = this.ShortDir(zipPath,temp.FullName);
                    ////递归
                    this.AddZipEntry(zipPath, s, u, out u);
                }
            }
            else if (File.Exists(s))
            {
                ////文件的处理 
                ////压缩等级   
                u.SetLevel(9);
                FileStream f = File.OpenRead(s);
                byte[] b = new byte[f.Length];
                ////将文件流加入缓冲字节中 
                f.Read(b, 0, b.Length);
                ZipEntry z = new ZipEntry(this.ShortDir(zipPath,s));
                ////为压缩文件流提供一个容器
                u.PutNextEntry(z);
                ////写入字节   
                u.Write(b, 0, b.Length);
                f.Close();
            }
            ////返回已添加数据的ZipOutputStream
            j = u;
        }

        /// <summary>
        /// 绝对路径转为相对路径
        /// </summary>
        /// <param name="s">绝对路径</param>
        /// <returns>相对路径</returns>
        public string ShortDir(string zipPath,string s)
        {
            ////将文件的绝对路径转为相对路径   
            string d = s.Replace(zipPath, string.Empty);
            return d;
        }
    }
}
