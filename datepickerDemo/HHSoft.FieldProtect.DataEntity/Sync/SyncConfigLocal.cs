using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHSoft.FieldProtect.DataEntity.Sync
{
    /// <summary>
    /// 同步本地配置类。
    /// </summary>
    public class SyncConfigLocal
    {
        /// <summary>
        /// 行政区编号。
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 行政区名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数据对象
        /// </summary>
        public DbConfig DbConfig { get; set; }

        /// <summary>
        /// Ftp对象
        /// </summary>
        public FtpConfig FtpConfig { get; set; }
    }
}
