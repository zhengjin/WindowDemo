using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDPro.BLL
{
    public class Person
    {
        private string name;
        private string department;
        private string position;
        private bool isNameReadOnly;
        private bool isDepartmentReadOnly;
        private bool isPositionReadOnly;
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// 部门
        /// </summary>
        public string Department
        {
            get { return department; }
            set { department = value; }
        }

        /// <summary>
        /// 职位
        /// </summary>
        public string Position
        {
            get { return position; }
            set { position = value; }
        }
        /// <summary>
        /// 名称是否只读
        /// </summary>
        public bool IsNameReadOnly
        {
            get { return isNameReadOnly; }
            set { isNameReadOnly = value; }
        }
        /// <summary>
        /// 部门信息是否只读
        /// </summary>
        public bool IsDepartmentReadOnly
        {
            get { return isDepartmentReadOnly; }
            set { isDepartmentReadOnly = value; }
        }
        /// <summary>
        /// 职位信息是否只读
        /// </summary>
        public bool IsPositionReadOnly
        {
            get { return isPositionReadOnly; }
            set { isPositionReadOnly = value; }
        }
    }
}
