using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.CustomAttribute;

namespace HHSoft.FieldProtect.DataEntity.ItemManage
{
    /// <summary>
    /// 项目工程信息
    /// </summary>
    [Serializable]
    public class Xm_Gcxx
    {
        /// <summary>
        /// 项目编码
        /// </summary>
        [Pk]
        public string ItemCode { get; set; }
        /// <summary>
        /// 项目阶段
        /// </summary>
        [Pk, EnumValueColumn(true)]
        public ItemStage Stage { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        [Pk]
        public string Xh { get; set; }

        #region 建设规模
        /// <summary>
        /// 总规模
        /// </summary>        
        [Query(DisplayName = "总规模(公顷)", Index = 1, Default = true)]
        public string Gm_zgm { get; set; }
        /// <summary>
        /// 开发规模
        /// </summary>
        [Query(DisplayName = "开发规模(公顷)", Index = 2, Default = true)]
        public string Gm_kfgm { get; set; }
        /// <summary>
        /// 复垦规模
        /// </summary>
        [Query(DisplayName = "复垦规模(公顷)", Index = 3, Default = true)]
        public string Gm_fkgm { get; set; }
        /// <summary>
        /// 整理规模
        /// </summary>
        [Query(DisplayName = "整理规模(公顷)", Index = 4, Default = true)]
        public string Gm_zlgm { get; set; }
        /// <summary>
        /// 基本农田整理规模
        /// </summary>
        [Query(DisplayName = "基本农田整理规模(公顷)", Index = 5, Default = true)]
        public string Gm_jbntzlgm { get; set; }
        /// <summary>
        /// 新增耕地面积
        /// </summary>
        [Query(DisplayName = "新增耕地面积(公顷)", Index = 6, Default = true)]
        public string Gm_xzgdmj { get; set; }
        /// <summary>
        /// 新增其它农用地面积
        /// </summary>
        [Query(DisplayName = "新增其它农用地面积(公顷)", Index = 7, Default = true)]
        public string Gm_xzqtnydmj { get; set; }
        /// <summary>
        /// 整理前建设用地面积
        /// </summary>
        [Query(DisplayName = "整理前建设用地面积(公顷)", Index = 8, Default = true)]
        public string Gm_zlqjsydmj { get; set; }
        /// <summary>
        /// 整理后建设用地面积
        /// </summary>
        [Query(DisplayName = "整理后建设用地面积(公顷)", Index = 9, Default = true)]
        public string Gm_zlhjsydmj { get; set; }
        /// <summary>
        /// 减少其他用地而增加的耕地面积
        /// </summary>
        [Query(DisplayName = "减少其他用地而增加的耕地面积(公顷)", Index = 10, Default = true)]
        public string Gm_jsjsydzjgdmj { get; set; }

        #endregion

        #region 成效信息

        /// <summary>
        /// 新增和改善农田灌溉面积
        /// </summary>
        [Query(DisplayName = "新增和改善农田灌溉面积(公顷)", Index = 11, Default = false)]
        public string CX_ZGNTGGMJ { get; set; }

        /// <summary>
        /// 新增和改善农田防涝面积
        /// </summary>
        [Query(DisplayName = "新增和改善农田防涝面积(公顷)", Index = 12, Default = false)]
        public string CX_XGNTFLMJ { get; set; }

        /// <summary>
        /// 项目区建设前耕地平均质量等级
        /// </summary>        
        public string CX_JSQGDDJ { get; set; }

        /// <summary>
        /// 项目区建设后耕地平均质量等级
        /// </summary>        
        public string CX_JSHGDDJ { get; set; }

        /// <summary>
        /// 新增粮食产能
        /// </summary>
        [Query(DisplayName = "新增粮食产能(公斤)", Index = 15, Default = false)]
        public string CX_XZLSCN { get; set; }

        /// <summary>
        /// 农民人均新增年均纯收入
        /// </summary>
        [Query(DisplayName = "农民人均新增年均纯收入(元)", Index = 16, Default = false)]
        public string CX_RJXZNSR { get; set; }

        /// <summary>
        /// 迁村并点减少的村庄数量
        /// </summary>
        [Query(DisplayName = "迁村并点减少的村庄数量(个)", Index = 17, Default = false)]
        public string CX_JSCZSL { get; set; }

        /// <summary>
        /// 新建和改善中心村数量
        /// </summary>
        [Query(DisplayName = "新建和改建中心村数量(个)", Index = 18, Default = false)]
        public string CX_JGZXCSL { get; set; }

        /// <summary>
        /// 项目建设受益人数
        /// </summary>
        [Query(DisplayName = "项目建设受益人数(人)", Index = 19, Default = false)]
        public string CX_SYRS { get; set; }

        #endregion

        #region 工程信息

        /// <summary>
        /// 土地平整面积
        /// </summary>
        [Query(DisplayName = "土地平整面积(公顷)", Index = 20, Default = false)]
        public string GC_PZMJ { get; set; }

        /// <summary>
        /// 挖土石方
        /// </summary>
        [Query(DisplayName = "挖土石方(立方米)", Index = 21, Default = false)]
        public string GC_WTSF { get; set; }

        /// <summary>
        /// 填土石方
        /// </summary>
        [Query(DisplayName = "填土石方(立方米)", Index = 22, Default = false)]
        public string GC_TTSF { get; set; }

        /// <summary>
        /// 运输土石方
        /// </summary>
        [Query(DisplayName = "运输土石方(立方米)", Index = 23, Default = false)]
        public string GC_YSTSF { get; set; }

        /// <summary>
        /// 排水沟渠
        /// </summary>
        [Query(DisplayName = "排水沟渠(千米)", Index = 24, Default = false)]
        public string GC_PSGQ { get; set; }

        /// <summary>
        /// 排水管道
        /// </summary>
        [Query(DisplayName = "排水管道(千米)", Index = 25, Default = false)]
        public string GC_PSGD { get; set; }

        /// <summary>
        /// 灌溉沟渠
        /// </summary>
        [Query(DisplayName = "灌溉沟渠(千米)", Index = 26, Default = false)]
        public string GC_GGGQ { get; set; }

        /// <summary>
        /// 灌溉管道 
        /// </summary>
        [Query(DisplayName = "灌溉管道(千米)", Index = 27, Default = false)]
        public string GC_GGGD { get; set; }

        /// <summary>
        /// 排灌两用沟渠
        /// </summary>
        [Query(DisplayName = "排灌两用沟渠(千米)", Index = 28, Default = false)]
        public string GC_PGLYGQ { get; set; }

        /// <summary>
        /// 排灌两用管道
        /// </summary>
        [Query(DisplayName = "排灌两用管道(千米)", Index = 29, Default = false)]
        public string GC_PGLYGD { get; set; }

        /// <summary>
        /// 桥 座
        /// </summary>
        [Query(DisplayName = "桥(座)", Index = 30, Default = false)]
        public string GC_QIAO { get; set; }

        /// <summary>
        /// 涵 座
        /// </summary>
        [Query(DisplayName = "涵(座)", Index = 31, Default = false)]
        public string GC_HAN { get; set; }

        /// <summary>
        /// 闸 座
        /// </summary>
        [Query(DisplayName = "闸(座)", Index = 32, Default = false)]
        public string GC_ZHA { get; set; }

        /// <summary>
        /// 房 座
        /// </summary>
        [Query(DisplayName = "房(座)", Index = 33, Default = false)]
        public string GC_FANG { get; set; }

        /// <summary>
        /// 筒井 眼
        /// </summary>
        [Query(DisplayName = "筒井(眼)", Index = 34, Default = false)]
        public string GC_TJ { get; set; }

        /// <summary>
        /// 机电井 眼
        /// </summary>
        [Query(DisplayName = "机电井(眼)", Index = 35, Default = false)]
        public string GC_JDJ { get; set; }

        /// <summary>
        /// 管井 眼
        /// </summary>
        [Query(DisplayName = "管井(眼)", Index = 36, Default = false)]
        public string GC_GJ { get; set; }

        /// <summary>
        /// 水泵 台
        /// </summary>
        [Query(DisplayName = "水泵(台)", Index = 37, Default = false)]
        public string GC_SB { get; set; }

        /// <summary>
        /// 高压线 千米
        /// </summary>
        [Query(DisplayName = "高压线(千米)", Index = 38, Default = false)]
        public string GC_GYX { get; set; }

        /// <summary>
        /// 低压线 千米
        /// </summary>
        [Query(DisplayName = "低压线(千米)", Index = 39, Default = false)]
        public string GC_DYX { get; set; }

        /// <summary>
        /// 变压器 台
        /// </summary>
        [Query(DisplayName = "变压器(台)", Index = 40, Default = false)]
        public string GC_BYQ { get; set; }

        /// <summary>
        /// 水泥杆 根
        /// </summary>
        [Query(DisplayName = "水泥杆(根)", Index = 41, Default = false)]
        public string GC_SNG { get; set; }

        /// <summary>
        /// 木杆 根
        /// </summary>
        [Query(DisplayName = "木杆(根)", Index = 42, Default = false)]
        public string GC_MG { get; set; }

        /// <summary>
        /// 田间路 千米
        /// </summary>
        [Query(DisplayName = "田间路(千米)", Index = 43, Default = false)]
        public string GC_TJL { get; set; }

        /// <summary>
        /// 生产路  千米
        /// </summary>
        [Query(DisplayName = "生产路(千米)", Index = 44, Default = false)]
        public string GC_SCL { get; set; }

        /// <summary>
        /// 防护林面积 亩
        /// </summary>
        [Query(DisplayName = "防护林面积(亩)", Index = 45, Default = false)]
        public string GC_FHLMJ { get; set; }

        /// <summary>
        /// 植树 株
        /// </summary>
        [Query(DisplayName = "植树(株)", Index = 46, Default = false)]
        public string GC_ZS { get; set; }

        #endregion


    }
}
