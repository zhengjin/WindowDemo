using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.Framework.Utility;

namespace HHSoft.FieldProtect.DataEntity.SysManage
{
    public class LoginUser
    {
        private string userid;
        private string username;
        private string realname;
        private string rolekey;
        private string departcode;
        private string departname;
        private string companycode;
        private string companyname;
        private string rolename;
        private string roleid;        

        public LoginUser()
        { 
        }

        /// <summary>
        /// 用户 Id
        /// </summary>
        public string UserId
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

        /// <summary>
        /// 用户名称 
        /// </summary>
        public string UserName
        {
            get
            {
                return this.username;
            }
            set
            {
                this.username = value;
            }
        }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName
        {
            get
            {
                return this.realname;
            }
            set
            {
                this.realname = value;
            }
        }

        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId
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
        /// <summary>
        /// 角色标识
        /// </summary>
        public string RoleKey
        {
            get
            {
                return this.rolekey;
            }
            set
            {
                this.rolekey = value;
            }
        }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName
        {
            get
            {
                return this.rolename;
            }
            set
            {
                this.rolename = value;
            }
        }

        /// <summary>
        /// 组织机构编码
        /// </summary>
        public string CompanyCode
        {
            get
            {
                return this.companycode;
            }
            set
            {
                this.companycode = value;
            }
        }

        public string CompanyShortCode
        {
            get
            {
                return CommonHelper.GetShortCode(this.companycode);
            }
        }

        /// <summary>
        /// 组织机构名称
        /// </summary>
        public string CompanyName
        {
            get
            {                
                return this.companyname;
            }
            set
            {
                this.companyname = value;
            }
        }

        /// <summary>
        /// 部门编码
        /// </summary>
        public string DepartCode
        {
            get
            {
                return this.departcode;
            }
            set
            {
                this.departcode = value;
            }
        }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartName
        {
            get
            {
                return this.departname;
            }
            set
            {
                this.departname = value;
            }
        }

        /// <summary>
        /// 行政级别
        /// </summary>
        public CompanyTypeEnum CompanyType
        {
            get
            {
                if (this.companycode.Substring(2, 4).Equals("0000"))
                {
                    return CompanyTypeEnum.SHENG; 
                }
                else if (this.companycode.Substring(4, 2).Equals("00") || this.companycode.Substring(4, 2).Equals("01"))
                {
                    return CompanyTypeEnum.SHI;
                }
                else
                {
                    return CompanyTypeEnum.XIAN;
                }
            }  
        }
    }
}
