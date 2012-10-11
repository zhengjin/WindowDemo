using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoftTreeView;

namespace HHSoft.FieldProtect.DataEntity.SysManage
{
    [Serializable]
    public class Company : ITreeNode
    {   
        private string ccode;
        private string shortccode;
        private string parentCode;
        private string companytype;
        private string name;
        private string namejc;
        private string description;
        private int orderno;
        private string nodeurl;
        private bool nodecheckbox;
        private ActionEnum action;


        public Company() { }
        /// <summary>
        /// 命令类型
        /// </summary>
        public virtual ActionEnum Action
        {
            get { return action; }
            set { action = value; }
        }

        /// <summary>
        /// 组织机构编码
        /// </summary>
        public virtual string CompanyCode
        {
            get { return ccode; }
            set { ccode = value; }
        }

        /// <summary>
        /// 拓扑编码
        /// </summary>
        public virtual string ShortCcode
        {
            get 
            {
                return this.shortccode;
            }
            set { shortccode = value; }
        }

        /// <summary>
        /// 父级拓扑编码
        /// </summary>
        public virtual string ParentCode
        {
            get { return parentCode; }
            set { parentCode = value; }
        }

        /// <summary>
        /// 单位类型
        /// </summary>
        public virtual string CompanyType
        {
            get { return companytype; }
            set { companytype = value; }
        }

        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// 单位简称
        /// </summary>
        public virtual string NameJc
        {
            get { return namejc; }
            set { namejc = value; }
        }

        /// <summary>
        /// 单位描述
        /// </summary>
        public virtual string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// 排序字段
        /// </summary>
        public virtual int OrderNo
        {
            get { return orderno; }
            set { orderno = value; }
        }


        #region 重写Equals和HashCode
        /// <summary>
        /// 用唯一值实现Equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if ((obj == null) || (obj.GetType() != GetType())) return false;
            Company castObj = (Company)obj;
            return (castObj != null) &&
                (shortccode == castObj.ShortCcode);
        }

        /// <summary>
        /// 用唯一值实现GetHashCode
        /// </summary>
        public override int GetHashCode()
        {
            int hash = 57;
            hash = 27 * hash * this.shortccode.GetHashCode();
            return hash;
        }
        #endregion

        #region ITreeNode 成员

        public string NodeCode
        {
            get
            {
                return this.shortccode;
            }
            set
            {
                this.shortccode = value;
            }
        }

        public string NodeName
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public string NodeUrl
        {
            get
            {
                return this.nodeurl;
            }
            set
            {
                this.nodeurl = value;
            }
        }

        public string NodeUrlTarget
        {
            get
            {
                return string.Empty;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int NodeOrderNo
        {
            get
            {
                return this.orderno;
            }
            set
            {
                this.orderno = value;
            }
        }

        public bool NodeCheckBox
        {
            get
            {
                return this.nodecheckbox;
            }
            set
            {
                this.nodecheckbox = value;
            }
        }

        #endregion
    }
}
