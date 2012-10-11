using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.CustomAttribute;

namespace HHSoft.FieldProtect.DataEntity.PersonalManage
{
    /// <summary>
    /// 系统信息接收结构体。
    /// </summary>
    [Serializable]
    public class XTXXJS
    {
        /// <summary>
        /// 信息编号
        /// </summary>
        [Pk]
        public string JSXXBH { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        [Pk]
        public string JSR { get; set; }

        /// <summary>
        /// 接收人行政代码。
        /// </summary>
        public string JSRXZDM { get; set; }

        /// <summary>
        /// 阅读状态：0未阅读 1阅读
        /// </summary>
        public string YDZT { get; set; }

        /// <summary>
        /// 接收人删除状态。
        /// </summary>
        public string JSRSC { get; set; }
    }
}
