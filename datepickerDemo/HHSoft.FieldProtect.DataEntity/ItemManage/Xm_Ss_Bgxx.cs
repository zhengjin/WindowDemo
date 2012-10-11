using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.CustomAttribute;

namespace HHSoft.FieldProtect.DataEntity.ItemManage
{
    [Serializable]
    public class Xm_Ss_Bgxx
    {
        /// <summary>
        /// 项目编号。
        /// </summary>
        [Pk]
        public string ITEMCODE { get; set; }

        /// <summary>
        /// 申请时间。
        /// </summary>
        [ViewInDataView(DisplayName = "变更申请时间", Index = 1)]
        public DateTime? SQSJ { get; set; }

        /// <summary>
        /// 申请单位。
        /// </summary>
        [ViewInDataView(DisplayName = "变更申请单位", Index = 2)]
        public string SQDW { get; set; }

        /// <summary>
        /// 批复单位。
        /// </summary>
        [ViewInDataView(DisplayName = "变更批复单位", Index = 3)]
        public string PFDW { get; set; }

        /// <summary>
        /// 变更原因。
        /// </summary>
        [ViewInDataView(DisplayName = "变更原因", Index = 4)]
        public string BGYY { get; set; }

        /// <summary>
        /// 批复情况。
        /// </summary>
        [ViewInDataView(DisplayName = "批复情况", Index = 5)]
        public string PFQK { get; set; }

        /// <summary>
        /// 变更内容：包括建设位置、建设规模、支出预算、工程量1010表示建设位置和支出预算变更。
        /// </summary>
        public string BGNR { get; set; }

        /// <summary>
        /// 是否完成：包括建设位置、建设规模、支出预算、工程量1010表示建设位置和支出预算变更完成。
        /// </summary>
        public string COMPLETED { get; set; }

        /// <summary>
        /// 序号（每个项目独立排序）。
        /// </summary>
        [Pk]
        public int XH { get; set; }
    }
}
