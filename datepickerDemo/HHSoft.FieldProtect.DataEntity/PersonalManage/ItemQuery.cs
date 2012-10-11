using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHSoft.FieldProtect.DataEntity.PersonalManage
{
    public class ItemQuery
    {
        /// <summary>
        /// 组织机构编码
        /// </summary>
        public string CCode { get; set; }
        /// <summary>
        /// 组织机构名称
        /// </summary>
        public string CName { get; set; }
        /// <summary>
        /// 预算金额
        /// </summary>
        public double YsMoney { get; set; }

        public string zjzeitemcodes { get; set; }
        /// <summary>
        /// 拨付金额
        /// </summary>
        public double BfMoney { get; set; }

        public string bfjeitemcodes { get; set; }
        /// <summary>
        /// 立项个数
        /// </summary>
        public double LxNum { get; set; }

        public string lxitemcodes { get; set; }
        /// <summary>
        /// 开工个数
        /// </summary>
        public double KgNum { get; set; }

        public string kgitemcodes { get; set; }
        /// <summary>
        /// 验收个数
        /// </summary>
        public double YsNum { get; set; }

        public string ysitemcodes { get; set; }

        
    }
}
