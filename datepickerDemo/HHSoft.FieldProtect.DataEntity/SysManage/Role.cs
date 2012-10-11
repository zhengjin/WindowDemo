using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.Framework.Control;

namespace HHSoft.FieldProtect.DataEntity.SysManage
{
    [Serializable]
    public class Role : IDoubleDropDownSource
    {
        private ActionEnum action;
        private string roleid;
        private string rolename;
        private string rolekey;
        private string description;
        private string filterRoleKey;
        private string roletype;
        private string rolelevel;
        private string fullname;
        private string companycode;

        public Role() { }
        /// <summary>
        /// 命令类型
        /// </summary>
        public virtual ActionEnum Action
        {
            get { return action; }
            set { action = value; }
        }
        /// <summary>
        /// 角色ID
        /// </summary>
        public virtual string RoleId
        {
            get { return roleid; }
            set { roleid = value; }
        }
        /// <summary>
        /// 角色名称
        /// </summary>
        public virtual string RoleName
        {
            get { return rolename; }
            set { rolename = value; }
        }
        /// <summary>
        /// 角色标示
        /// </summary>
        public virtual string RoleKey
        {
            get { return rolekey; }
            set { rolekey = value; }
        }

        /// <summary>
        /// 角色标示过滤
        /// </summary>
        public virtual string FilterRoleKey
        {
            get { return filterRoleKey; }
            set { filterRoleKey = value; }
        }
        /// <summary>
        /// 角色描述
        /// </summary>
        public virtual string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// 角色类型(1 为系统默认的角色 0为用户自定义的角色)
        /// </summary>
        public virtual string RoleType
        {
            get { return this.roletype; }
            set { this.roletype = value; }
        }

        /// <summary>
        /// 单位名称 + 角色名称
        /// </summary>
        public virtual string FullName
        {
            get { return fullname; }
            set { fullname = value; }
        }

        /// <summary>
        /// 角色级别(1省 2市 3 县)
        /// </summary>
        public virtual string RoleLevel
        {
            get { return this.rolelevel; }
            set { this.rolelevel = value; }
        }

        /// <summary>
        /// 组织机构编码
        /// </summary>
        public virtual string CompanyCode
        {
            get { return this.companycode; }
            set { this.companycode = value; }
        }

        #region 重写Equals和HashCode
        /// <summary>
        /// 用唯一值实现Equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if ((obj == null) || (obj.GetType() != GetType())) return false;
            Role castObj = (Role)obj;
            return (castObj != null) &&
                (roleid == castObj.RoleId);
        }

        /// <summary>
        /// 用唯一值实现GetHashCode
        /// </summary>
        public override int GetHashCode()
        {
            int hash = 57;
            hash = 27 * hash * roleid.GetHashCode();
            return hash;
        }
        #endregion

        #region IDoubleDropDownSource 成员

        public string DisplayValue
        {
            get
            {
                return this.roleid;
            }
            set
            {
                this.roleid = value;
            }
        }

        public string DisplayName
        {
            get
            {
                return this.fullname;
            }
            set
            {
                this.fullname = value;
            }
        }

        private bool allowmove = true;
        public bool AllowMove
        {
            get
            {
                return this.allowmove;
                //return (this.roletype == "0");
            }
            set
            {
                this.allowmove = value;
            }
        }

        #endregion 
    }
}
