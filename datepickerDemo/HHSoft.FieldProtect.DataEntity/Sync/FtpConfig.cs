using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHSoft.FieldProtect.DataEntity.Sync
{
    /// <summary>
    /// Ftp配置类
    /// </summary>
    public class FtpConfig
    {
        /// <summary>
        /// 判断是否为空
        /// </summary>
        public bool IsNull
        {
            get
            {
                if (!string.IsNullOrEmpty(Ip))
                {
                    return false;
                }
                if (Port != 0)
                {
                    return false;
                }
                if (!string.IsNullOrEmpty(Username))
                {
                    return false;
                }
                if (!string.IsNullOrEmpty(Password))
                {
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// IP
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        public override string ToString()
        {
            return "IP：" + Ip + "，端口：" + Port + "，用户名：" + Username + "，密码：" + Password + "。";
        }
    }
}
