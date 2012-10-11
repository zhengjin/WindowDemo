using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.CustomAttribute;

namespace HHSoft.FieldProtect.DataEntity.ItemManage
{
    /// <summary>
    /// 可研基本信息
    /// </summary>
    [Serializable]
    public class Xm_Ky_Jbxx
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
        /// 建设工期
        /// </summary>
        public virtual string Jsgq { get; set; }

        /// <summary>
        /// 立项文号
        /// </summary>
        public string Lxwh { get; set; }

        /// <summary>
        /// 项目批次
        /// </summary>
        public string Xmpc { get; set; }

        /// <summary>
        /// 立项审批单位
        /// </summary>
        public string Lxdw { get; set; }

        /// <summary>
        /// 立项审批时间
        /// </summary>
        public DateTime? Lxsj { get; set; }

        /// <summary>
        /// 项目资金
        /// </summary>
        [IsDbColumn(false)]
        public Xm_Xmzj Xmzj { get; set; }

        /// <summary>
        /// 可研编制单位
        /// </summary>
        [IsDbColumn(false)]
        public Xm_Xmdw KyDw { get; set; }

        /// <summary>
        /// 项目属性(1 省　2 市　3 县)
        /// </summary>
        public string ItemType { get; set; }
    }
}
