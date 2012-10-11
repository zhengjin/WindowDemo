using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.CustomAttribute;

namespace HHSoft.FieldProtect.DataEntity.ItemManage
{
    /// <summary>
    /// 项目资金类
    /// </summary>
    [Serializable]
    public class Xm_Xmzj
    {

        public Xm_Xmzj() { }

        /// <summary>
        /// 项目编号
        /// </summary>
        public virtual string ItemCode { get; set; }
        

        /// <summary>
        /// 项目阶段
        /// </summary>
        [EnumValueColumn(true)]
        public virtual ItemStage Stage { get; set; }
       

        /// <summary>
        /// 项目环节
        /// </summary>
        [EnumValueColumn(true)]
        public virtual WorkFlowNode NodeId { get; set; }
  

        /// <summary>
        /// 序号
        /// </summary>
        public virtual string Xh { get; set; }


        /// <summary>
        /// 资金总额
        /// </summary>
        public virtual string Zjze { get; set; }
    

        /// <summary>
        /// 设备购置费
        /// </summary>
        public virtual string Sbgzf { get; set; }
        

        /// <summary>
        /// 土地平整工程 
        /// </summary>
        public virtual string Tdpzgcf { get; set; }


        /// <summary>
        /// 农田水利工程
        /// </summary>
        public virtual string Ntslgcf { get; set; }

        /// <summary>
        /// 道路工程
        /// </summary>
        public virtual string Dlgcf { get; set; }

        /// <summary>
        /// 其它工程
        /// </summary>
        public virtual string Qtgcf { get; set; }     

        /// <summary>
        /// 不可预见费 
        /// </summary>
        public virtual string Bkyjf { get; set; }

        /// <summary>
        /// 其它费用
        /// </summary>
        public virtual string Qtfy { get; set; }


        /// <summary>
        /// 中央分配的新增费资金
        /// </summary>
        public virtual string fee1 { get; set; }

        /// <summary>
        /// 地方留成新增费资金
        /// </summary>
        public virtual string fee2 { get; set; }

        /// <summary>
        /// 耕地开垦费
        /// </summary>
        public virtual string fee3 { get; set; }

        /// <summary>
        /// 自行补充耕地资金
        /// </summary>
        public virtual string fee4 { get; set; }

        /// <summary>
        /// 用于农业土地开发的土地出让收入
        /// </summary>
        public virtual string fee5 { get; set; }

        /// <summary>
        /// 土地复垦费
        /// </summary>
        public virtual string fee6 { get; set; }

        /// <summary>
        /// 土地复垦义务人投资
        /// </summary>
        public virtual string fee7 { get; set; }

        /// <summary>
        /// 其他涉农资金
        /// </summary>
        public virtual string fee8 { get; set; }
 
    }
}
