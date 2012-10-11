using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.CustomAttribute;

namespace HHSoft.FieldProtect.DataEntity.ItemManage
{
    /// <summary>
    /// 规划设计预算信息
    /// </summary>
    public class Xm_GhsjYsxx
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        [Pk]
        public string ItemCode { get; set; }

        /// <summary>
        /// 规划批复单位
        /// </summary>
        public string Ghpfdw { get; set; }

        /// <summary>
        /// 规划批复时间
        /// </summary>
        public DateTime? Ghpfsj { get; set; }

        /// <summary>
        /// 预算批复单位
        /// </summary>
        public string Yspfdw { get; set; }

        /// <summary>
        /// 预算批复时间
        /// </summary>
        public DateTime? Yspfsj { get; set; }

        /// <summary>
        /// 预算下达文号
        /// </summary>
        public string Ysxdwh { get; set; }

        /// <summary>
        /// 是否预算调整
        /// </summary>
        public int Sftz { get; set; }

        /// <summary>
        /// 项目资金
        /// </summary>
        [IsDbColumn(false)]
        public Xm_Xmzj Xmzj { get; set; }

        /// <summary>
        /// 可研编制单位
        /// </summary>
        [IsDbColumn(false)]
        public Xm_Xmdw GhDw { get; set; }
    }
}
