using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.CustomAttribute;

namespace HHSoft.FieldProtect.DataEntity.Sync
{
    /// <summary>
    /// 同步数据日志
    /// </summary>
    [Serializable]
    public class SyncDataLog
    {
        /// <summary>
        /// 日志编号
        /// </summary>
        [Pk]
        public string LogId { get; set; }

        /// <summary>
        /// 行政区编码
        /// </summary>
        public string CCode { get; set; }

        /// <summary>
        /// 行政区名称
        /// </summary>
        public string CName { get; set; }

        /// <summary>
        /// 同步类型
        /// </summary>
        public SyncDataType Synctype { get; set; }

        /// <summary>
        /// 同步时间
        /// </summary>
        public DateTime SyncDate { get; set; }

        /// <summary>
        /// 同步文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 成功标识(0　成功　1 失败)
        /// </summary>
        public int Succ { get; set; }
    }

    /// <summary>
    /// 同步方式。
    /// </summary>
    public enum SyncDataType : int
    {
        /// <summary>
        /// 系统
        /// </summary>
        System = 1,
        /// <summary>
        /// 手动
        /// </summary>
        Manual = 2
    }
}
