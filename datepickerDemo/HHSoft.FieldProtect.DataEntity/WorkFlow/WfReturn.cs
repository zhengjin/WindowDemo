using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHSoft.FieldProtect.DataEntity.WorkFlow
{
    /// <summary>
    /// 返回结果集合
    /// </summary>
    [Serializable]
    public class WfReturn
    {
        private bool success = false;
        private string resultdesc;
        private string nodename;
        private string userlist;
        private string rolelist;
        private string departlist;
        private string stage;
        

        public WfReturn() { }

        /// <summary>
        /// 返回结果
        /// </summary>
        public virtual bool Success
        {
            get { return success; }
            set { success = value; }
        }

        /// <summary>
        /// 描述信息
        /// </summary>
        public virtual string ResultDesc
        {
            get { return resultdesc; }
            set { resultdesc = value; }
        }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual string UserList
        {
            get { return userlist; }
            set { userlist = value; }
        }

        /// <summary>
        /// 角色
        /// </summary>
        public virtual string RoleList
        {
            get { return rolelist; }
            set { rolelist = value; }
        }

        /// <summary>
        /// 部门
        /// </summary>
        public virtual string DepartList
        {
            get { return departlist; }
            set { departlist = value; }
        }

        /// <summary>
        /// 环节名称
        /// </summary>
        public virtual string NodeName
        {
            get { return nodename; }
            set { nodename = value; }
        }

        /// <summary>
        /// 所属阶段
        /// </summary>
        public virtual string Stage
        {
            get { return stage; }
            set { stage = value; }
        }
    }
}
