using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.CustomAttribute;

namespace HHSoft.FieldProtect.DataEntity.ItemManage
{
    [Serializable]
    public class Xm_Jsxx
    {
        /// <summary>
        /// 项目编号。
        /// </summary>
        [Pk]
        public string ITEMCODE { get; set; }

        /// <summary>
        /// 实际总投资（万元）。
        /// </summary>
        public double? SJZTZ { get; set; }

        /// <summary>
        /// 实际建设规模
        /// </summary>
        public double? SJGM { get; set; }

        /// <summary>
        /// 亩均投资（万元）。
        /// </summary>
        public double? MJTZ { get; set; }

        /// <summary>
        /// 决算单位（决算书编制单位）。
        /// </summary>
        public string JSDW { get; set; }

        /// <summary>
        /// 决算审查单位。
        /// </summary>
        public string SCDW { get; set; }

        /// <summary>
        /// 决算审查定案通知书下发时间。
        /// </summary>
        public DateTime? SCDASJ { get; set; }

        /// <summary>
        /// 决算审查定案通知书文号。
        /// </summary>
        public string SCDAWH { get; set; }
    }
}
