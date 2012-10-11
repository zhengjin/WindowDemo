using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.CustomAttribute;
using HHSoft.FieldProtect.DataEntity.CustomInterface;

namespace HHSoft.FieldProtect.DataEntity.ItemManage
{
    /// <summary>
    /// 实施——招投标——中标情况表。
    /// </summary>
    [Serializable]
    public class Xm_Ss_Ztb_Zbqk : IHasFile
    {
        /// <summary>
        /// 中标标段。
        /// </summary>
        [ViewInDataView(DisplayName = "中标标段", Index = 1)]
        public string ZBBD { get; set; }

        /// <summary>
        /// 中标单位。
        /// </summary>
        [ViewInDataView(DisplayName = "中标单位", Index = 2)]
        public string ZBDW { get; set; }

        /// <summary>
        /// 中标价格 万元。
        /// </summary>
        [ViewInDataView(DisplayName = "中标价格(万元)", Index = 3)]
        public double? ZBJG { get; set; }

        /// <summary>
        /// 中标通知书文件名称。
        /// </summary>
        [FileNameUrl, ViewInDataView(DisplayName = "中标通知书", Index = 4)]
        public string ZBTZS { get; set; }

        /// <summary>
        /// 记录编号。
        /// </summary>
        [Pk]
        public string ItemCode { get; set; }

        #region IHasFile 成员

        /// <summary>
        /// 文件对象列表。（非数据库字段）
        /// </summary>
        [IsDbColumn(false)]
        public List<Item_File> Files { get; set; }

        /// <summary>
        /// 被删除的文件对象列表。（非数据库字段）
        /// </summary>
        [IsDbColumn(false)]
        public List<string> DelFiles { get; set; }

        /// <summary>
        /// 文件名称列名。
        /// </summary>
        [IsDbColumn(false)]
        public string FileNameColumnName
        {
            get
            {
                return "ZBTZS";
            }
        }

        #endregion
    }
}
