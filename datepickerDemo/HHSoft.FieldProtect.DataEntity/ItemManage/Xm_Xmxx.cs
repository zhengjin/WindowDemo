using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.CustomAttribute;

namespace HHSoft.FieldProtect.DataEntity.ItemManage
{
    /// <summary>
    /// 项目基本信息
    /// </summary>
    [Serializable]
    public class Xm_Xmxx
    {
        public Xm_Xmxx() { }

        /// <summary>
        /// 命令类型
        /// </summary>
        [IsDbColumn(false)]
        public virtual ActionEnum Action { get; set; }
    
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
        /// 申报单位机构代码
        /// </summary>
        public virtual string Ccode { get; set; }

        /// <summary>
        /// 申报单位机构名称
        /// </summary>
        public virtual string Cname { get; set; }

        /// <summary>
        /// 项目类型
        /// </summary>
        public virtual string Xmlx { get; set; }

        /// <summary>
        /// 项目属性(1 省　2 市　3 县)
        /// </summary>
        public virtual string ItemType { get; set; }

        /// <summary>
        /// 省(机构代码)
        /// </summary>
        public virtual string ShengCode { get; set; }

        /// <summary>
        /// 省(机构名称)
        /// </summary>
        public virtual string ShengName { get; set; }

        /// <summary>
        /// 市(机构代码)
        /// </summary>
        public virtual string ShiCode { get; set; }

        /// <summary>
        /// 市(机构名称)
        /// </summary>
        public virtual string ShiName { get; set; }

        /// <summary>
        /// 县(机构代码)
        /// </summary>
        public virtual string XianCode { get; set; }

        /// <summary>
        /// 县(机构名称)
        /// </summary>
        public virtual string XianName { get; set; }

        /// <summary>
        /// 项目地址
        /// </summary>
        public virtual string Address { get; set; }

        /// <summary>
        /// 资金类型
        /// </summary>
        public virtual ZjType ZjType { get; set; }

        /// <summary>
        /// 资金年度
        /// </summary>
        public virtual string Zjnd { get; set; }
        
        /// <summary>
        /// 项目年度
        /// </summary>
        public virtual string Xmnd { get; set; }

        /// <summary>
        /// 建设工期
        /// </summary>
        public virtual string Jsgq { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? CreateDate { get; set; }

        /// <summary>
        /// 申报时间
        /// </summary>
        public virtual DateTime? Sbsj { get; set; }

        /// <summary>
        /// 资金批复时间
        /// </summary>
        public virtual DateTime? ZjPfsj { get; set; }

        /// <summary>
        /// 开标时间
        /// </summary>
        public virtual DateTime? Kbsj { get; set; }

        /// <summary>
        /// 开工时间
        /// </summary>
        public virtual DateTime? Kgsj { get; set; }

        /// <summary>
        /// 竣工时间
        /// </summary>
        public virtual DateTime? Jgsj { get; set; }

        /// <summary>
        /// 验收时间
        /// </summary>
        public virtual DateTime? Yssj { get; set; }

        /// <summary>
        /// 决算时间
        /// </summary>
        public virtual DateTime? Jssj { get; set; }

        /// <summary>
        /// 归档时间
        /// </summary>
        public virtual DateTime? Gdsj { get; set; }

        /// <summary>
        /// 项目批次
        /// </summary>
        public virtual string Xmpc { get; set; }

        /// <summary>
        /// 立项时间
        /// </summary>
        public virtual DateTime? LxSj { get; set; }

        /// <summary>
        /// 立项文号
        /// </summary>
        public virtual string LxWh { get; set; }
 
        /// <summary>
        /// 资金批复文号
        /// </summary>
        public virtual string ZjPfwh { get; set; }

        /// <summary>
        /// 项目验收文号
        /// </summary>
        public virtual string Yswh { get; set; }

        /// <summary>
        /// 项目阶段
        /// </summary>
        public virtual ItemStage ItemStage { get; set; }

        /// <summary>
        /// 流程环节
        /// </summary>
        public virtual WorkFlowNode NodeId { get; set; }

        /// <summary>
        /// 项目状态
        /// </summary>
        public virtual ItemState ItemState { get; set; }      

        /// <summary>
        /// 流程状态
        /// </summary>
        public virtual WfState WfState { get; set; }

        /// <summary>
        /// 项目流程 ID
        /// </summary>
        public virtual string FlowId { get; set; }


        /// <summary>
        /// 项目描述(暂停、终止)
        /// </summary>
        public virtual string ItemDesc { get; set; }

        /// <summary>
        /// 读取标志
        /// </summary>      
        public virtual int Read { get; set; }

        /// <summary>
        /// 同步标志
        /// </summary>
        [IsDbColumn(false)]
        public virtual int Tbzt { get; set; }   


        private int pageIndex = 1;
        /// <summary>
        /// 当前页面
        /// </summary>
        [IsDbColumn(false)]
        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }
        private int pageSize = 10;
        /// <summary>
        /// 页面大小
        /// </summary>
        [IsDbColumn(false)]
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        private bool isPager = true;
        /// <summary>
        /// 是否分页
        /// </summary>
        [IsDbColumn(false)]
        public bool IsPager
        {
            get { return isPager; }
            set { this.isPager = value; }
        }

    }
}
