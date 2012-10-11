using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.CustomAttribute;

namespace HHSoft.FieldProtect.DataEntity.ItemManage
{
    [Serializable]
    public class Item_File
    {
        public Item_File() { }

        public Item_File(string _fileName)
        {
            this.FileName = _fileName;
        }

        /// <summary>
        /// 项目编号
        /// </summary>
        [Pk]
        public virtual string ItemCode { get; set; }

        /// <summary>
        /// 文件类别编码
        /// </summary>
        public virtual string FileCode { get; set; }      

        /// <summary>
        /// 文件类别名称
        /// </summary>
        [IsDbColumn(false)]
        public virtual string FileCodeName { get; set; }
       
        /// <summary>
        /// 文件名称
        /// </summary>
        public virtual string FileName { get; set; }

        /// <summary>
        /// 上传人员ID
        /// </summary>
        public virtual string UserId { get; set; }      

        /// <summary>
        /// 上传人员名称
        /// </summary>
        public virtual string UserName { get; set; }

        /// <summary>
        /// 上传日期
        /// </summary>
        public virtual DateTime? CreateDate { get; set; }

        /// <summary>
        /// 上传阶段
        /// </summary>
        public virtual ItemStage Stage { get; set; }   

        /// <summary>
        /// 流程环节
        /// </summary>
        [Pk]
        public virtual WorkFlowNode NodeId { get; set; }
      
        /// <summary>
        /// 序号
        /// </summary>
        [Pk]
        public virtual string XH { get; set; }

        /// <summary>
        /// 只读属性
        /// </summary>
        [IsDbColumn(false)]
        public virtual bool ReadOnly { get; set; }
       
        #region 重写Equals和HashCode
        /// <summary>
        /// 用唯一值实现Equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if ((obj == null) || (obj.GetType() != GetType())) return false;
            Item_File castObj = (Item_File)obj;
            return (castObj != null) &&
                (this.FileName == castObj.FileName);
        }

        /// <summary>
        /// 用唯一值实现GetHashCode
        /// </summary>
        public override int GetHashCode()
        {
            int hash = 57;
            hash = 27 * hash * this.FileName.GetHashCode();
            return hash;
        }
        #endregion

    }
}
