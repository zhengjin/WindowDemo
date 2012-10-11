using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoftTreeView;

namespace HHSoft.FieldProtect.DataEntity.SysManage
{
    [Serializable]
    public class Function : ITreeNode
    {
        public Function() { }

        private string functioncode;
        private string functionname;
        private string functionurl;
        private string description;
        private int orderno;
        private bool nodecheckbox = false;

        /// <summary>
        /// 功能编码
        /// </summary>
        public virtual string FunctionCode
        {
            get { return this.functioncode; }
            set { this.functioncode = value; }
        }
        /// <summary>
        /// 功能名称
        /// </summary>
        public virtual string FunctionName
        {
            get { return this.functionname; }
            set { this.functionname = value; }
        }
        /// <summary>
        /// 功能地址
        /// </summary>
        public virtual string FunctionUrl
        {
            get { return this.functionurl; }
            set { this.functionurl = value; }
        }
        /// <summary>
        /// 功能描述
        /// </summary>
        public virtual string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }
        /// <summary>
        /// 排序字段
        /// </summary>
        public virtual int OrderNo
        {
            get { return this.orderno; }
            set { this.orderno = value; }
        }
        /// <summary>
        /// 首页标识
        /// </summary>
        public virtual bool IsFristPage
        {
            get;
            set;
        }

        #region ITreeNode 成员

        public string NodeCode
        {
            get { return this.functioncode; }
            set { this.functioncode = value; }
        }

        public string NodeName
        {
            get { return this.functionname; }
            set { this.functionname = value; }
        }

        public string NodeUrl
        {
            get { return this.functionurl; }
            set { this.functionurl = value; }
        }

        public string NodeUrlTarget
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int NodeOrderNo
        {
            get { return this.orderno; }
            set { this.orderno = value; }
        }

        public bool NodeCheckBox
        {
            get { return this.nodecheckbox; }
            set { this.nodecheckbox = value; }
        }

        #endregion


    }
}
