using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XL.Models.Sys
{
    public class LogModel
    {
        #region Model
        private Guid _id;
        private string _logname;
        private string _loginfo;
        private string _ip;
        private string _hostname;
        private int? _logtype;
        private DateTime? _logtime;
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
        public string LogName
        {
            set { _logname = value; }
            get { return _logname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LogInfo
        {
            set { _loginfo = value; }
            get { return _loginfo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IP
        {
            set { _ip = value; }
            get { return _ip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HostName
        {
            set { _hostname = value; }
            get { return _hostname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? LogType
        {
            set { _logtype = value; }
            get { return _logtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LogTime
        {
            set { _logtime = value; }
            get { return _logtime; }
        }
        #endregion Model
    }
}
