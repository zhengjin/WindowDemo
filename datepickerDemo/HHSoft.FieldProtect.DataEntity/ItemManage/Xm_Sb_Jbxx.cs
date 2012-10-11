using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.CustomAttribute;

namespace HHSoft.FieldProtect.DataEntity.ItemManage
{
    /// <summary>
    /// 项目申报信息
    /// </summary>
    [Serializable]
    public class Xm_Sb_Jbxx
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        [Pk]
        public virtual string ItemCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public virtual string ItemName { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public virtual string Sheng { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public virtual string Shi { get; set; }

        /// <summary>
        /// 县
        /// </summary>
        public virtual string Xian { get; set; }
       
        /// <summary>
        /// 项目性质
        /// </summary>
        public virtual string Xmlx { get; set; }

        /// <summary>
        /// 资金年度
        /// </summary>
        public virtual string Zjnd { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }

        /// <summary>
        /// 筛选单位
        /// </summary>
        public virtual string SxDw { get; set; }

        /// <summary>
        /// 筛选时间
        /// </summary>
        public virtual DateTime? SxSj { get; set; }

        /// <summary>
        /// 项目资金
        /// </summary>
        [IsDbColumn(false)]
        public Xm_Xmzj Xmzj { get; set; }
    }
}
