using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoftTreeView;
using HHSoft.FieldProtect.Framework.Control;

namespace HHSoft.FieldProtect.DataEntity.SysManage
{
    [Serializable]
    public class Department : IDoubleDropDownSource
    {
        private string deptcode;
        private string companycode;   
        private string name;
        private string description;
        private int orderno;
        private string fullname;
        private ActionEnum action;

        public Department() { }
        /// <summary>
        /// 命令类型
        /// </summary>
        public virtual ActionEnum Action
        {
            get { return action; }
            set { action = value; }
        }

        /// <summary>
        /// 单位编码
        /// </summary>
        public virtual string CompanyCode
        {
            get { return companycode; }
            set { companycode = value; }
        }

        /// <summary>
        /// 部门编码
        /// </summary>
        public virtual string DeptCode
        {
            get { return deptcode; }
            set { deptcode = value; }
        }

        /// <summary>
        /// 部门名称
        /// </summary>
        public virtual string DeptName
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// 单位名称 + 部门名称
        /// </summary>
        public virtual string FullName
        {
            get { return fullname; }
            set { fullname = value; }
        }

        /// <summary>
        /// 部门描述
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

        /// <summary>
        /// 部门查询级别
        /// </summary>
        public virtual string QueryDeptLevel
        {
            get;
            set;
        }

        #region 重写Equals和HashCode
        /// <summary>
        /// 用唯一值实现Equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if ((obj == null) || (obj.GetType() != GetType())) return false;
            Department castObj = (Department)obj;
            return (castObj != null) &&
                (deptcode == castObj.DeptCode);
        }

        /// <summary>
        /// 用唯一值实现GetHashCode
        /// </summary>
        public override int GetHashCode()
        {
            int hash = 57;
            hash = 27 * hash * this.deptcode.GetHashCode();
            return hash;
        }
        #endregion

            
        #region IDoubleDropDownSource 成员

        public string DisplayValue
        {
            get
            {
                return this.deptcode;
            }
            set
            {
                this.deptcode = value;
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

        public bool AllowMove
        {
            get
            {
                return true;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
