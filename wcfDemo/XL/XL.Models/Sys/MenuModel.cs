using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace XL.Models.Sys
{
    [DataContract]
    public class MenuModel
    {
        #region Model
        private Guid _id;
        private string _menuname;
        private Guid _parentid;
        private string _url;
        private string _menudes;
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public Guid Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string MenuName
        {
            set { _menuname = value; }
            get { return _menuname; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public Guid ParentId
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string MenuDes
        {
            set { _menudes = value; }
            get { return _menudes; }
        }
        public int OrderNum { get; set; }
        #endregion Model
    }
}
