using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.CustomAttribute;

namespace HHSoft.FieldProtect.DataEntity.ItemManage
{
    [Serializable]
    public class Xm_Ss_Gcjl_Jlry
    {
        /// <summary>
        /// 项目编号。
        /// </summary>
        public string ITEMCODE { get; set; }

        /// <summary>
        /// 人员名称。
        /// </summary>
        [ViewInDataView(DisplayName = "姓名", Index = 1, MaxLength = 6)]
        public string RYMC { get; set; }

        /// <summary>
        /// 性别。
        /// </summary>
        [ViewInDataView(DisplayName = "性别", Index = 2)]
        public string XB { get; set; }

        /// <summary>
        /// 职称。
        /// </summary>
        [ViewInDataView(DisplayName = "职称", Index = 4, MaxLength = 18)]
        public string ZC { get; set; }

        /// <summary>
        /// 专业。
        /// </summary>
        [ViewInDataView(DisplayName = "专业", Index = 3, MaxLength = 18)]
        public string ZY { get; set; }

        /// <summary>
        /// 职务。
        /// </summary>
        [ViewInDataView(DisplayName = "职务", Index = 6, MaxLength = 18)]
        public string ZW { get; set; }

        /// <summary>
        /// 执业资格。
        /// </summary>
        [ViewInDataView(DisplayName = "执业资格", Index = 7, MaxLength = 18)]
        public string ZYZG { get; set; }

        /// <summary>
        /// 联系电话。
        /// </summary>
        [ViewInDataView(DisplayName = "联系电话", Index = 5, MaxLength = 8)]
        public string LXDH { get; set; }
    }
}
