using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.CustomInterface;
using HHSoft.FieldProtect.DataEntity.CustomAttribute;

namespace HHSoft.FieldProtect.DataEntity.ItemManage
{
    [Serializable]
    public class Xm_Ss_Gcjl_Jlht : IHasFile
    {
        /// <summary>
        /// 项目编号。
        /// </summary>
        public string ITEMCODE { get; set; }

        /// <summary>
        /// 合同甲方。
        /// </summary>
        [ViewInDataView(DisplayName = "甲方", Index = 2)]
        public string HTJF { get; set; }

        /// <summary>
        /// 合同乙方。
        /// </summary>
        [ViewInDataView(DisplayName = "乙方", Index = 3)]
        public string HTYF { get; set; }

        /// <summary>
        /// 签订时间。
        /// </summary>
        [ViewInDataView(DisplayName = "签订时间", Index = 5)]
        public DateTime? QDSJ { get; set; }

        /// <summary>
        /// 合同文件名称。
        /// </summary>
        public string HTWJMC { get; set; }

        /// <summary>
        /// 合同金额。
        /// </summary>
        [ViewInDataView(DisplayName = "合同金额(万元)", Index = 4)]
        public double? HTJE { get; set; }

        /// <summary>
        /// 合同编号。
        /// </summary>
        [ViewInDataView(DisplayName = "合同编号", Index = 1)]
        public string HTBH { get; set; }

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
