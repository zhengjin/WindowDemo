using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.Framework.Control;

namespace HHSoft.FieldProtect.DataEntity.SysManage
{
	[Serializable]
	public class XM_XZF
	{

		//自动生成字段
        private ActionEnum action;
		private int xZFID;
		private string xZDM;
		private string zJND;
		private decimal zJSE;
		private string xDWH;
		private DateTime xDSJ;
		private string dQND;

		//自动生成默认无参构造函数
		public XM_XZF()
		{
		}

		//自动生成全参参构造函数
		public XM_XZF(int xZFID,string xZDM,string zJND,decimal zJSE,string xDWH,DateTime xDSJ,string dQND)
		{
			this.xZFID = xZFID;
			this.xZDM = xZDM;
			this.zJND = zJND;
			this.zJSE = zJSE;
			this.xDWH = xDWH;
			this.xDSJ = xDSJ;
			this.dQND = dQND;
		}
        /// <summary>
        /// 命令类型
        /// </summary>
        public virtual ActionEnum Action
        {
            get { return action; }
            set { action = value; }
        }
		//自动生成属性
		public int XZFID
		{
			get { return xZFID;}
			set { xZFID = value;}
		}

		public string XZDM
		{
			get { return xZDM;}
			set { xZDM = value;}
		}

		public string ZJND
		{
			get { return zJND;}
			set { zJND = value;}
		}

		public decimal ZJSE
		{
			get { return zJSE;}
			set { zJSE = value;}
		}

		public string XDWH
		{
			get { return xDWH;}
			set { xDWH = value;}
		}

		public DateTime XDSJ
		{
			get { return xDSJ;}
			set { xDSJ = value;}
		}

		public string DQND
		{
			get { return dQND;}
			set { dQND = value;}
        }

        private int pageIndex = 1;
        /// <summary>
        /// 当前页面
        /// </summary>
        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }
        private int pageSize = 10;
        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        private bool isPager = true;
        /// <summary>
        /// 是否分页
        /// </summary>
        public bool IsPager
        {
            get { return isPager; }
            set { this.isPager = value; }
        }
	}
}
