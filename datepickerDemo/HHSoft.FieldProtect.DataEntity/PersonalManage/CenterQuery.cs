using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHSoft.FieldProtect.DataEntity.PersonalManage
{
    [Serializable]
    public class CenterQuery
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? BeginDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 组织结构编码
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// 组织机构短编码
        /// </summary>
        public string CompanyShortCode { get; set; }

        /// <summary>
        /// 部门编码
        /// </summary>
        public string DepartMentCode { get; set; }

        /// <summary>
        /// 数据条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }
    }
}
