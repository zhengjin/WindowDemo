using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.CustomAttribute;

namespace HHSoft.FieldProtect.DataEntity.ItemManage
{
    [Serializable]
    public class Xm_Gdxx
    {
        /// <summary>
        /// 项目编号。
        /// </summary>
        [Pk]
        public string ITEMCODE { get; set; }

        /// <summary>
        /// 归档单位。
        /// </summary>
        public string GDDW { get; set; }

        /// <summary>
        /// 归档时间。
        /// </summary>
        public DateTime? GDSJ { get; set; }

        /// <summary>
        /// 归档人员。
        /// </summary>
        public string GDRY { get; set; }

        /// <summary>
        /// 备注。
        /// </summary>
        public string BZ { get; set; }
    }
}
