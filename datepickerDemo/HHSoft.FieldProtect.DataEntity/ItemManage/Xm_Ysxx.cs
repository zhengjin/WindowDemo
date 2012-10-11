using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.CustomInterface;
using HHSoft.FieldProtect.DataEntity.CustomAttribute;

namespace HHSoft.FieldProtect.DataEntity.ItemManage
{
    [Serializable]
    public class Xm_Ysxx : IHasFile
    {
        /// <summary>
        /// 项目编号。
        /// </summary>
        [Pk]
        public string ITEMCODE { get; set; }

        /// <summary>
        /// 验收申请时间。
        /// </summary>
        public DateTime? YSSQSJ { get; set; }

        /// <summary>
        /// 验收申请单位。
        /// </summary>
        public string YSSQDW { get; set; }

        /// <summary>
        /// 初验单位。
        /// </summary>
        public string CYDW { get; set; }

        /// <summary>
        /// 初验时间。
        /// </summary>
        public DateTime? CYSJ { get; set; }

        /// <summary>
        /// 初验意见。
        /// </summary>
        [IsDbColumn(false)]
        public string CYYJ { get; set; }

        /// <summary>
        /// 终验单位。
        /// </summary>
        public string ZYDW { get; set; }

        /// <summary>
        /// 验收时间。
        /// </summary>
        public DateTime? ZYSJ { get; set; }

        /// <summary>
        /// 终验意见。
        /// </summary>
        [IsDbColumn(false)]
        public string ZYYJ { get; set; }

        /// <summary>
        /// 验收文号。
        /// </summary>
        public string YSWH { get; set; }

        /// <summary>
        /// 技术复核单位
        /// </summary>
        public string JSFHDW { get; set; }

        /// <summary>
        /// 技术复核时间。
        /// </summary>
        public DateTime? JSFHSJ { get; set; }

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
