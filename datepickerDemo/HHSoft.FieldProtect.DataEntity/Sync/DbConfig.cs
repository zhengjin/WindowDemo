using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHSoft.FieldProtect.DataEntity.Sync
{
    /// <summary>
    /// 数据库配置类
    /// </summary>
    public class DbConfig
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
                if (!string.IsNullOrEmpty(Name))
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
        /// Ip
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// Net名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnStr 
        {
            get
            {
                return "Data Source=" + (string.IsNullOrEmpty(Ip) ? string.Empty : Ip + "/") + Name + ";User ID=" + Username + ";Password=" + Password + ";";
            }
        }

        public override string ToString()
        {
            return "IP：" + Ip + "，名称：" + Name + "，用户名：" + Username + "，密码：" + Password + "。";
        }
    }
}
