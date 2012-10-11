using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XL.Models.Sys
{
    public class PermissionModel
    {
        #region Model
        private Guid _id;
        private Guid _menuid;
        private string _buttontext;
        private int? _buttontype;
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
        public Guid MenuId
        {
            set { _menuid = value; }
            get { return _menuid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ButtonText
        {
            set { _buttontext = value; }
            get { return _buttontext; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ButtonType
        {
            set { _buttontype = value; }
            get { return _buttontype; }
        }
        #endregion Model
    }
}
