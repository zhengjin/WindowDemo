using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.CustomAttribute;

namespace HHSoft.FieldProtect.DataEntity.ItemManage
{
    [Serializable]
    public class Xm_Ss_Jdgz_Xmbb
    {
        /// <summary>
        /// 项目编号。
        /// </summary>
        public string ITEMCODE { get; set; }

        /// <summary>
        /// 项目名称。
        /// </summary>
        [ViewInDataView(DisplayName = "项目名称", Index = 2)]
        public string XMMC { get; set; }

        /// <summary>
        /// 报告类型 ：M月报 Q季度。
        /// </summary>
        public string BBLX { get; set; }

        /// <summary>
        /// 上报时间 ：月份 或 季度。
        /// </summary>
        [ViewInDataView(DisplayNameGroups = "M|月份&&Q|季度", Index = 1)]
        public string SBSJ { get; set; }

        /// <summary>
        /// 报送单位。
        /// </summary>
        [ViewInDataView(DisplayName = "填报单位", Index = 4)]
        public string SBDW { get; set; }

        /// <summary>
        /// 负责人。
        /// </summary>
        public string FZR { get; set; }

        /// <summary>
        /// 填表人。
        /// </summary>
        public string TBR { get; set; }

        /// <summary>
        /// 项目类型：如开发、整理、复垦。
        /// </summary>
        public string XMLX { get; set; }

        /// <summary>
        /// 项目年度。
        /// </summary>
        public string XMND { get; set; }

        /// <summary>
        /// 资金来源。
        /// </summary>
        public string ZJLY { get; set; }

        /// <summary>
        /// 项目规模（公顷）。
        /// </summary>
        public double? XMGM { get; set; }

        /// <summary>
        /// 新增耕地（公顷）。
        /// </summary>
        public double? XZGD { get; set; }

        /// <summary>
        /// 建设周期（年）。
        /// </summary>
        public int? JSZQ { get; set; }

        /// <summary>
        /// 承担单位。
        /// </summary>
        public string CDDW { get; set; }

        /// <summary>
        /// 批准资金（万元）。
        /// </summary>
        public double? PZZJ { get; set; }

        /// <summary>
        /// 承担单位已收资金（万元）。
        /// </summary>
        public double? YSZJ { get; set; }

        /// <summary>
        /// 招标代理单位。
        /// </summary>
        public string ZBDLDW { get; set; }

        /// <summary>
        /// 监理单位。
        /// </summary>
        public string JLDW { get; set; }

        /// <summary>
        /// 施工单位，多个单位逐个填写。
        /// </summary>
        public string SGDW { get; set; }

        /// <summary>
        /// 市级立项文号。
        /// </summary>
        [ViewInDataView(DisplayName = "立项文号", Index = 3)]
        public string LXWH { get; set; }

        /// <summary>
        /// 是否招标 0否1是。
        /// </summary>
        public string SFZB { get; set; }

        /// <summary>
        /// 是否公示 0否1是。
        /// </summary>
        public string SFGS { get; set; }

        /// <summary>
        /// 是否开工 0否1是。
        /// </summary>
        public string SFKG { get; set; }

        /// <summary>
        /// 完成工程量占总工程量百分比。
        /// </summary>
        [ViewInDataView(DisplayName = "工程完成情况(%)", Index = 5)]
        public double? WCGCL { get; set; }

        /// <summary>
        /// 存在的问题。
        /// </summary>
        public string CZWT { get; set; }

        /// <summary>
        /// 拟采取的措施。
        /// </summary>
        public string CQCS { get; set; }

        /// <summary>
        /// 备注。
        /// </summary>
        public string BZ { get; set; }
    }
}
