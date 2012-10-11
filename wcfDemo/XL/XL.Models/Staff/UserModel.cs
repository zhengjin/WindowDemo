using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace XL.Models
{
    [DataContract]
    public class UserModel
    {
        #region Model
        private Guid _id;
        private string _username;
        private string _password;
        private DateTime? _addtime;
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
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string PassWord
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public DateTime? AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        #endregion Model
    }
}
