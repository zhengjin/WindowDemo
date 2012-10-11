using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.CustomAttribute;

namespace HHSoft.FieldProtect.DataEntity.ItemManage
{
    [Serializable]
    public class Xm_Ss_Zjbf
    {
        /// <summary>
        /// 项目编号。
        /// </summary>
        [Pk]
        public string ITEMCODE { get; set; }

        /// <summary>
        /// 拨付期次。
        /// </summary>
        [Pk, ViewInDataView(DisplayName = "拨付期", Index = 2)]
        public int? QC { get; set; }

        /// <summary>
        /// 拨付单位。
        /// </summary>
        [ViewInDataView(DisplayName = "拨付单位", Index = 1)]
        public string BFDW { get; set; }

        /// <summary>
        /// 拨付时间。
        /// </summary>
        [ViewInDataView(DisplayName = "拨付时间", Index = 3)]
        public DateTime? BFSJ { get; set; }

        /// <summary>
        /// 拨付金额。
        /// </summary>
        [ViewInDataView(DisplayName = "拨付金额(万元)", Index = 4)]
        public double? BFJE { get; set; }
    }
}
