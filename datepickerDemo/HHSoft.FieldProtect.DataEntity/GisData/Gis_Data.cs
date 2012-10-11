using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GBArcGis.Base;
using HHSoft.FieldProtect.DataEntity.CustomAttribute;
using HHSoft.FieldProtect.Framework.Utility;
using HHSoft.FieldProtect.DataEntity.ItemManage;

namespace HHSoft.FieldProtect.DataEntity.GisData
{
    /// <summary>
    /// Gis数据实体类
    /// </summary>
    [Serializable]
    public class Gis_Data<T> : IGBXM<T>
         where T : Gis_Dk
    {

        public Gis_Data()
        {
            this.DkList = new List<T>();
        }
        /// <summary>
        /// 主键
        /// </summary>
        [Pk]
        public string GisId { get; set; }

        /// <summary>
        /// 建设规模
        /// </summary>
        public double? GM { get; set; }

        /// <summary>
        /// 建设规模(开发)
        /// </summary>
        public double? GM_KF { get; set; }

        /// <summary>
        /// 建设规模(整理)
        /// </summary>
        public double? GM_ZL { get; set; }

        /// <summary>
        /// 建设规模 (复垦)
        /// </summary>
        public double? GM_FK { get; set; }

        /// <summary>
        /// 新增耕地面积
        /// </summary>
        public double? XZGDMJ { get; set; }

        /// <summary>
        /// 新增耕地比率
        /// </summary>
        public double? XZGDBL { get; set; }

        /// <summary>
        /// 项目资金
        /// </summary>
        public double? Money { get; set; }

        /// <summary>
        /// 亩均投资
        /// </summary>
        public double? Mjtz { get; set; }

        /// <summary>
        /// 图幅号
        /// </summary>
        public string Tfh { get; set; }

        /// <summary>
        /// 图斑号
        /// </summary>
        public string Tbh { get; set; }

        /// <summary>
        /// 图幅(图斑号)
        /// </summary>
        public string TfTbh { get; set; }

        /// <summary>
        /// 权属性质
        /// </summary>
        public string Qsxz { get; set; }

        /// <summary>
        /// 权属性质面积
        /// </summary>
        public string QsxzMj { get; set; }

        /// <summary>
        /// 地类编码
        /// </summary>
        public string Dlbm { get; set; }

        /// <summary>
        /// 地类面积
        /// </summary>
        public string DlMj { get; set; }

        /// <summary>
        /// 地类名称
        /// </summary>
        public string Dlmc { get; set; }

        /// <summary>
        /// 项目地址
        /// </summary>
        public string Address { get; set; }

        #region IGBXM<T> 成员

        /// <summary>
        /// 行政区划代码
        /// </summary>
        [IsDbColumn(false)]
        public string Ccode { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [IsDbColumn(false)]
        public string ItemName { get; set; }

        /// <summary>
        /// 项目阶段
        /// </summary>
        public EnumStage Stage { get; set; }

        /// <summary>
        /// 阶段序号
        /// </summary>
        public int Xh { get; set; }

        /// <summary>
        /// 起始经度
        /// </summary>
        public string Qsjd { get; set; }

        /// <summary>
        /// 截止经度
        /// </summary>
        public string Jzjd { get; set; }

        /// <summary>
        /// 起始纬度
        /// </summary>
        public string Qswd { get; set; }

        /// <summary>
        /// 截止纬度
        /// </summary>
        public string Jzwd { get; set; }

        /// <summary>
        /// 坐标系
        /// </summary>
        public string Zbx { get; set; }

        /// <summary>
        /// 几度分带
        /// </summary>
        public string Jdfd { get; set; }

        /// <summary>
        /// 投影类型
        /// </summary>
        public string Tylx { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        public string Jldw { get; set; }

        /// <summary>
        /// 带号
        /// </summary>
        public string Dh { get; set; }

        /// <summary>
        /// 精度
        /// </summary>
        public string Jd { get; set; }

        /// <summary>
        /// 转换坐标参数
        /// </summary>
        public string Zhzbcs { get; set; }

        /// <summary>
        /// 数据生产单位
        /// </summary>
        public string Scdw { get; set; }

        /// <summary>
        /// 数据生产日期
        /// </summary>
        public string Scrq { get; set; }

        /// <summary>
        /// 地块列表
        /// </summary>
        [IsDbColumn(false)]
        public List<T> DkList { get; set; }

        #endregion
    }
}
