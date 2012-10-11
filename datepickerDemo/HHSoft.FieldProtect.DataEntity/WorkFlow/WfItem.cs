using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHSoft.FieldProtect.DataEntity.WorkFlow
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class WfItem
    {
        private string itemcode;
        private string nodeid;
        private string userid;
        private string username;
        private WfResult result = WfResult.Agree;
        
        private string resultdesc = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public WfItem() { }

        /// <summary>
        /// 项目主键
        /// </summary>
        public virtual string ItemCode
        {
            get { return itemcode; }
            set { itemcode = value; }
        }

        /// <summary>
        /// 环节ID
        /// </summary>
        public virtual string NodeId
        {
            get { return nodeid; }
            set { nodeid = value; }
        }

        /// <summary>
        /// 跳转环节Id
        /// </summary>
        public virtual string SwicthNode
        {
            get;
            set;
        }

        /// <summary>
        /// 处理人ID
        /// </summary>
        public virtual string UserId
        {
            get { return userid; }
            set { userid = value; }
        }

        /// <summary>
        /// 处理人名称
        /// </summary>
        public virtual string UserName
        {
            get { return username; }
            set { username = value; }
        }

        /// <summary>
        /// 处理结果
        /// </summary>
        public virtual WfResult Result
        {
            get { return result; }
            set { result = value; }
        }

        /// <summary>
        /// 处理意见
        /// </summary>
        public virtual string ResultDesc
        {
            get { return resultdesc; }
            set { resultdesc = value; }
        }               
    }
}
