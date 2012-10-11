using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.CustomAttribute;
using HHSoft.FieldProtect.DataEntity.CustomInterface;

namespace HHSoft.FieldProtect.DataEntity.ItemManage
{
    [Serializable]
    public class Xm_Ss_Ztb_Sght : IHasFile
    {
        /// <summary>
        /// 记录编号。
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// 合同标段。
        /// </summary>
        public string HTBD { get; set; }

        /// <summary>
        /// 合同编号。
        /// </summary>
        [ViewInDataView(DisplayName = "合同编号", Index = 1)]
        public string HTBH { get; set; }

        /// <summary>
        /// 发包方。
        /// </summary>
        [ViewInDataView(DisplayName = "发包方", Index = 2)]
        public string FBF { get; set; }

        /// <summary>
        /// 承包方。
        /// </summary>
        [ViewInDataView(DisplayName = "承包方", Index = 3)]
        public string CBF { get; set; }

        /// <summary>
        /// 签订时间。
        /// </summary>
        [ViewInDataView(DisplayName = "签订时间", Index = 5)]
        public DateTime? QDSJ { get; set; }

        /// <summary>
        /// 合同工期（天数）。
        /// </summary>
        public int? HTGQ { get; set; }

        /// <summary>
        /// 合同文件名称。
        /// </summary>
        public string HTWJMC { get; set; }

        /// <summary>
        /// 合同金额。
        /// </summary>
        [ViewInDataView(DisplayName = "合同金额(万元)", Index = 4)]
        public double? HTJE { get; set; }

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
                return "HTWJMC";
            }
        }

        #endregion
    }
}
