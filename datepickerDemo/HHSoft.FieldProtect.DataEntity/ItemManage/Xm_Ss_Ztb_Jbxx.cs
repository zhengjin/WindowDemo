using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.CustomAttribute;

namespace HHSoft.FieldProtect.DataEntity.ItemManage
{
    /// <summary>
    /// 实施——招投标——基本信息。
    /// </summary>
    [Serializable]
    public class Xm_Ss_Ztb_Jbxx
    {
        /// <summary>
        /// 项目编号。
        /// </summary>
        [Pk]
        public string ItemCode { get; set; }

        /// <summary>
        /// 开标时间。
        /// </summary>
        public DateTime? KBSJ { get; set; }

        /// <summary>
        /// 公告时间。
        /// </summary>
        public DateTime? GGSJ { get; set; }

        /// <summary>
        /// 开工时间。
        /// </summary>
        public DateTime? KGSJ { get; set; }

        /// <summary>
        /// 完工时间。
        /// </summary>
        public DateTime? JGSJ { get; set; }

        /// <summary>
        /// 招标人员。
        /// </summary>
        public string ZBRY { get; set; }

        /// <summary>
        /// 监标人员。
        /// </summary>
        public string JBRY { get; set; }
    }
}
