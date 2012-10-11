using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace HHSoft.FieldProtect.FrameWork.Utility
{
    public class FtpHelper
    {
        private const int bufferSize = 2048;

        private string ip;
        private int port;
        private string username;
        private string password;

        private FileHelper fileHelper;

        private string rootUrl;

        private FtpWebRequest ftpWebRequest;

        public FtpHelper(string ip, int port, string username, string password)
        {
            this.ip = ip;
            this.port = port;
            this.username = username;
            this.password = password;

            fileHelper = new FileHelper();

            rootUrl = "ftp://" + ip + ":" + port + "/";
        }


        private string GetParentDirectory(string path)
        {
            string result = string.Empty;
            List<string> items = path.Split(new string[] { "//", "/", @"\\", @"\" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (items.Count > 1)
            {
                items.RemoveAt(items.Count - 1);
                result = string.Join("/", items.ToArray());
            }
            return result;
        }

        private string GetCurrentLevelName(string path)
        {
            string result = string.Empty;
            List<string> items = path.Split(new string[] { "//", "/", @"\\", @"\" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (items.Count > 0)
            {
                result = items[items.Count - 1];
            }
            return result;
        }

        private void Connection(string path)
        {
            ftpWebRequest = (FtpWebRequest)FtpWebRequest.Create(rootUrl + path);
            ftpWebRequest.Credentials = new NetworkCredential(username, password);
        }

        /// <summary>
        /// 测试FTP连接
        /// </summary>
        /// <returns></returns>
        public bool TestConn()
        {
            try
            {
                this.GetFileList(string.Empty, WebRequestMethods.Ftp.ListDirectory);
                return true;
            }
            catch
            {
                return false;
            } 
        }

        public List<string> GetFileList(string path, string method)
        {
            List<string> result = new List<string>();
            Connection(path);
            ftpWebRequest.Method = method;
            WebResponse response = ftpWebRequest.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default);
            while (!reader.EndOfStream)
            {
                result.Add(reader.ReadLine());
            }
            reader.Close();
            response.Close();
            return result;
        }

        public Stream GetRequestStream(string ftpPath, string method)
        {
            Connection(ftpPath);
            ftpWebRequest.Method = method;
            return ftpWebRequest.GetRequestStream();
        }

        public Stream GetResponseStream(string ftpPath, string method)
        {
            Connection(ftpPath);
            ftpWebRequest.Method = method;
            FtpWebResponse response = (FtpWebResponse)ftpWebRequest.GetResponse();            
            return response.GetResponseStream();
        }

        public bool CheckHaveDirectory(string path)
        {
            string parentPath = GetParentDirectory(path);
            string currentName = GetCurrentLevelName(path);
            if (string.IsNullOrEmpty(parentPath) || CheckHaveDirectory(parentPath))
            {
                List<string> items = GetFileList(parentPath, WebRequestMethods.Ftp.ListDirectory);
                foreach (var item in items)
                {
                    if (item == currentName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void DeleteDirectory(string path)
        {
            List<string> fileNames = GetFileList(path, WebRequestMethods.Ftp.ListDirectory);
            foreach (var fileName in fileNames)
            {
                DeleteFile(path + "/" + fileName);
            }
            Connection(path);
            ftpWebRequest.Method = WebRequestMethods.Ftp.RemoveDirectory;
            FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
            ftpWebResponse.Close();
        }

        public void CreateDirectory(string path)
        {
            string parentDirectory = GetParentDirectory(path);
            if (!string.IsNullOrEmpty(parentDirectory))
            {
                if (!CheckHaveDirectory(parentDirectory))
                {
                    CreateDirectory(parentDirectory);
                }
            }
            Connection(path);
            ftpWebRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
            FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
            ftpWebResponse.Close();
        }

        public void CopyFile(string localFtpPath, FtpHelper targetFtpOperation, string targetFtpPath)
        {
            if (CheckHaveDirectory(localFtpPath))
            {
                string targetDirectory = GetParentDirectory(targetFtpPath);
                if (!targetFtpOperation.CheckHaveDirectory(targetDirectory))
                {
                    targetFtpOperation.CreateDirectory(targetDirectory);
                }
                Stream downloadStream = GetResponseStream(localFtpPath, WebRequestMethods.Ftp.DownloadFile);
                Stream uploadStream = targetFtpOperation.GetRequestStream(targetFtpPath, WebRequestMethods.Ftp.UploadFile);
                byte[] buff = new byte[bufferSize];
                int length;
                while ((length = downloadStream.Read(buff, 0, bufferSize)) != 0)
                {
                    uploadStream.Write(buff, 0, length);
                    if (length < bufferSize)
                    {
                        break;
                    }
                }
                downloadStream.Close();
                uploadStream.Close();
            }
        }

        public void DeleteFile(string path)
        {
            Connection(path);
            ftpWebRequest.Method = WebRequestMethods.Ftp.DeleteFile;
            WebResponse response = ftpWebRequest.GetResponse();
            response.Close();
        }

        public void CopyDirectory(string localParentPath, string localDirectoryName, 
            FtpHelper targetFtpOperation, string targetParentPath, string targetDirectoryName,Action<string,int> CopyAction)
        {
            string localFullPath = localParentPath + localDirectoryName;
            string targetFullPath = targetParentPath + targetDirectoryName;
            if (CheckHaveDirectory(localFullPath))
            {
                if (targetFtpOperation.CheckHaveDirectory(targetFullPath))
                {
                    targetFtpOperation.DeleteDirectory(targetFullPath);
                }
                targetFtpOperation.CreateDirectory(targetFullPath);
                List<string> fileNames = GetFileList(localFullPath, WebRequestMethods.Ftp.ListDirectory);
                foreach (var fileName in fileNames)
                {
                    CopyFile(localFullPath + "/" + fileName, targetFtpOperation, targetFullPath + "/" + fileName);
                }

                if (CopyAction != null)
                {
                    CopyAction(localDirectoryName, fileNames.Count);
                }
            }
        }

        public void ClearDirectory(string itemCode,string path, List<string> exceptions, Action<string,string> deleteAction)
        {
            List<string> fileNames = GetFileList(path, WebRequestMethods.Ftp.ListDirectory);
            foreach (var fileName in fileNames)
            {
                if (exceptions == null || !exceptions.Contains(fileName))
                {
                    DeleteFile(path + "/" + fileName);
                    if (deleteAction != null)
                    {
                        deleteAction(itemCode, fileName);
                    }
                }
            }
        }
    }
}
