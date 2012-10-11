using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHSoft.FieldProtect.DataEntity.Sync
{
    /// <summary>
    /// 同步目标配置类。
    /// </summary>
    public class SyncConfigTarget : SyncConfigLocal
    {
        /// <summary>
        /// 同步间隔。
        /// </summary>
        public string Jg { get; set; }

        /// <summary>
        /// 同步时间。
        /// </summary>
        public string Time { get; set; }
    }
}
