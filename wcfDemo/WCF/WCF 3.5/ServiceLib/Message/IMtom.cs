using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.IO;

namespace WCF.ServiceLib.Message
{
    /// <summary>
    /// IMtom接口
    /// </summary>
    [ServiceContract]
    public interface IMtom
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="path">文件目标路径</param>
        /// <param name="fileData">文件字节数组</param>
        [OperationContract]
        void UploadFile(string path, byte[] fileData);
    }
}
