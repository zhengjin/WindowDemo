using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XL.Models
{
    public class DepartmentModel
    {
        #region Model
        private Guid _id;
        private string _departmentname;
        private string _departdesc;
        private Guid _parentid;
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
        public string DepartmentName
        {
            set { _departmentname = value; }
            get { return _departmentname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DepartDesc
        {
            set { _departdesc = value; }
            get { return _departdesc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid ParentId
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        #endregion Model
    }
}
