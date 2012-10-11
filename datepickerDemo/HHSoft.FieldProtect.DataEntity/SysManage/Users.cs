using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.Framework.Control;

namespace HHSoft.FieldProtect.DataEntity.SysManage
{
    [Serializable]
    public class Users : IDoubleDropDownSource
    {
        private ActionEnum action;
        private string userid;
        private string companycode;
        private string departcode;
        private Company company;
        private Department depart;
        private Role role;
        private string username;
        private string password;
        private string realname;
        private bool sex;
        private string telephone;
        private bool state;
        private string fullname;
        private DateTime createdate;
        private bool isfiltersysUser;

        public Users() 
        {
            company = new Company();
            role = new Role();
            depart = new Department();
        }

        /// <summary>
        /// 命令类型
        /// </summary>
        public virtual ActionEnum Action
        {
            get { return action; }
            set { action = value; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public virtual string UserId 
        {
            get { return userid; }
            set { userid = value; }
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
        public virtual string DepartCode
        {
            get { return departcode; }
            set { departcode = value; }
        }

        /// <summary>
        /// 单位
        /// </summary>
        public virtual Company Company
        {
            get { return company; }
            set { company = value; }
        }

        /// <summary>
        /// 部门
        /// </summary>
        public virtual Department DepartMent
        {
            get { return depart; }
            set { depart = value; }
        }

        /// <summary>
        /// 角色
        /// </summary>
        public virtual Role Role
        {
            get { return role; }
            set { role = value; }
        }
        /// <summary>
        /// 用户名称
        /// </summary>
        public virtual string UserName
        {
            get { return username; }
            set { username = value; }
        }

        /// <summary>
        /// 是否显示系统默认的管理员
        /// </summary>
        public virtual bool IsFilterSYSUser
        {
            get { return this.isfiltersysUser; }
            set { isfiltersysUser = value; }
        }

        /// <summary>
        /// 用户密码
        /// </summary>
        public virtual string PassWord
        {
            get { return password; }
            set { password = value; }
        }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public virtual string RealName
        {
            get { return realname; }
            set { realname = value; }
        }
        /// <summary>
        /// 性别(True 男 False 女)
        /// </summary>
        public virtual bool Sex
        {
            get { return sex; }
            set { sex = value; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string TelePhone
        {
            get { return telephone; }
            set { telephone = value; }
        }
        
        /// <summary>
        /// 状态(True 正常 False 停用)
        /// </summary>
        public virtual bool State
        {
            get { return state; }
            set { state = value; }
        }
        
        /// <summary>
        /// 创建日期
        /// </summary>
        public virtual DateTime CreateDate
        {
            get { return createdate; }
            set { createdate = value; }
        }


        /// <summary>
        /// 单位名称 + 用户姓名
        /// </summary>
        public virtual string FullName
        {
            get { return fullname; }
            set { fullname = value; }
        }

        /// <summary>
        /// 查询用户的级别
        /// </summary>
        public virtual string QueryUserLevel
        {
            get;
            set;
        }

        private int pageIndex = 1;
        /// <summary>
        /// 当前页面
        /// </summary>
        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }
        private int pageSize = 10;
        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        private bool isPager = true;
        /// <summary>
        /// 是否分页
        /// </summary>
        public bool IsPager
        {
            get { return isPager; }
            set { this.isPager = value; }
        }

        #region 重写Equals和HashCode
        /// <summary>
        /// 用唯一值实现Equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if ((obj == null) || (obj.GetType() != GetType())) return false;
            Users castObj = (Users)obj;
            return (castObj != null) &&
                (userid == castObj.UserId);
        }

        /// <summary>
        /// 用唯一值实现GetHashCode
        /// </summary>
        public override int GetHashCode()
        {
            int hash = 57;
            hash = 27 * hash * userid.GetHashCode();
            return hash;
        }
        #endregion

        #region IDoubleDropDownSource 成员

        public string DisplayValue
        {
            get
            {
                return this.userid;
            }
            set
            {
                this.userid = value;
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
