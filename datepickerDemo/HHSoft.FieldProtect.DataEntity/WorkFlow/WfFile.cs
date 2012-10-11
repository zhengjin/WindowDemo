using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.Framework.Control;

namespace HHSoft.FieldProtect.DataEntity.SysManage
{
    [Serializable]
    public class WfFile : IDoubleDropDownSource
    {
        private string filecode;
        private string filename;
        private string filetype;       
        

        public WfFile() { }

        /// <summary>
        /// 文件编码
        /// </summary>
        public virtual string FileCode
        {
            get { return filecode; }
            set { filecode = value; }
        }
        /// <summary>
        /// 文件名称
        /// </summary>
        public virtual string FileName
        {
            get { return filename; }
            set { filename = value; }
        }
        /// <summary>
        /// 后缀类型
        /// </summary>
        public virtual string FileType
        {
            get { return filetype; }
            set { filetype = value; }
        }

        #region 重写Equals和HashCode
        /// <summary>
        /// 用唯一值实现Equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if ((obj == null) || (obj.GetType() != GetType())) return false;
            WfFile castObj = (WfFile)obj;
            return (castObj != null) &&
                (filecode == castObj.FileCode);
        }

        /// <summary>
        /// 用唯一值实现GetHashCode
        /// </summary>
        public override int GetHashCode()
        {
            int hash = 57;
            hash = 27 * hash * filecode.GetHashCode();
            return hash;
        }
        #endregion

        #region IDoubleDropDownSource 成员

        public string DisplayValue
        {
            get
            {
                return this.filecode;
            }
            set
            {
                this.filecode = value;
            }
        }

        public string DisplayName
        {
            get
            {
                return this.filename;
            }
            set
            {
                this.filename = value;
            }
        }

        public bool AllowMove
        {
            get
            {
                return true;
            }
        }
        #endregion
    }
}
