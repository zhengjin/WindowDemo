using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XL.Models
{
    public class RoleModel
    {
        #region Model
        private Guid _id;
        private string _rolename;
        private Guid _parentid;
        private string _roledes;
        /// <summary>
        /// 
        /// </summary>
        public Guid Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RoleName
        {
            set { _rolename = value; }
            get { return _rolename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid ParentId
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RoleDes
        {
            set { _roledes = value; }
            get { return _roledes; }
        }
        #endregion Model
    }
}
