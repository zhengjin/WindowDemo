using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace HHSoft.FieldProtect.DataEntity
{
    /// <summary>
    /// 行政区单位类别
    /// </summary>
    public enum CompanyType
    {
        [Description("市辖区")]
        City = 1,

        [Description("县辖区")]
        County = 2
    }

    /// <summary>
    /// 操作类型
    /// </summary>
    public enum ActionEnum
    {
        [Description("添加")]
        Insert = 1,

        [Description("修改")]
        Update = 2,

        [Description("删除")]
        Delete = 3,

        [Description("保存并进入流程")]
        SaveAndWorkFlow = 4
    }

    /// <summary>
    /// 行政级别
    /// </summary>
    public enum CompanyTypeEnum
    {
        /// <summary>
        /// 省
        /// </summary>
        [Description("省")]
        SHENG = 1,

        /// <summary>
        /// 市
        /// </summary>
        [Description("市")]
        SHI = 2,

        /// <summary>
        /// 县
        /// </summary>
        [Description("县")]
        XIAN = 3
    }

    /// <summary>
    /// 结点类型
    /// </summary>
    public enum NodeType
    {
        /// <summary>
        /// 开始结点
        /// </summary>
        [Description("开始节点")]
        BeginNode = 0,
        /// <summary>
        /// 任务结点
        /// </summary>
        [Description("任务节点")]
        TaskNode = 1,
        /// <summary>
        /// 结束结点
        /// </summary>
        [Description("结束节点")]
        EndNode = 2
    }

    /// <summary>
    /// 项目阶段
    /// </summary>
    public enum ItemStage
    {
        /// <summary>
        /// 申报阶段
        /// </summary>
        [Description("申报阶段")]
        ShenBo = 11,
        /// <summary>
        /// 可研阶段
        /// </summary>
        [Description("可研阶段")]
        KeYan = 12,
        /// <summary>
        /// 规划设计
        /// </summary>
        [Description("规划设计")]
        GuiHua = 13,
        /// <summary>
        /// 预算审核
        /// </summary>
        [Description("预算审核")]
        YuSuan = 14,
        /// <summary>
        /// 实施阶段
        /// </summary>
        [Description("实施阶段")]
        ShiShi = 15,
        /// <summary>
        /// 竣工阶段
        /// </summary>
        [Description("竣工阶段")]
        JunGong = 16,
        /// <summary>
        /// 验收阶段
        /// </summary>
        [Description("验收阶段")]
        YanShou = 17,
        /// <summary>
        /// 决算阶段
        /// </summary>
        [Description("决算阶段")]
        JueSuan = 18,
        /// <summary>
        /// 归档阶段
        /// </summary>
        [Description("归档")]
        GuiDang = 19
    }

    /// <summary>
    /// 项目环节
    /// </summary>
    public enum WorkFlowNode
    {
        /// <summary>
        /// 开始节点
        /// </summary>
        [Description("开始节点")]
        Begin = 30000,
        /// <summary>
        /// 上报项目
        /// </summary>
        [Description("上报项目")]
        TB = 30001,
        /// <summary>
        /// 筛选项目
        /// </summary>
        [Description("筛选项目")]
        SX = 30002,
        /// <summary>
        /// 上报可研
        /// </summary>
        [Description("上报可研")]
        KY = 30003,
        /// <summary>
        /// 可研论证
        /// </summary>
        [Description("可研论证")]
        KYSH = 30004,
        /// <summary>
        /// 上报规划预算
        /// </summary>
        [Description("上报规划预算")]
        GHSJYS = 30005,
        /// <summary>
        /// 规划设计评审
        /// </summary>
        [Description("规划设计评审")]
        GHSJSH = 30006,
        /// <summary>
        /// 预算审核
        /// </summary>
        [Description("预算审核")]
        YSSH = 30007,
        /// <summary>
        /// 预算调整
        /// </summary>
        [Description("预算调整")]
        YSTZ = 30008,
        /// <summary>
        /// 项目实施
        /// </summary>
        [Description("项目实施")]
        ShiShi = 30009,
        /// <summary>
        /// 项目竣工
        /// </summary>
        [Description("项目竣工")]
        JunGong = 30010,
        /// <summary>
        /// 项目初验
        /// </summary>
        [Description("项目初验")]
        ChuYan = 30011,
        /// <summary>
        /// 项目终验
        /// </summary>
        [Description("项目终验")]
        ZhongYan = 30012,
        /// <summary>
        /// 项目决算
        /// </summary>
        [Description("项目决算")]
        JueSuan = 30013,
        /// <summary>
        /// 项目归档
        /// </summary>
        [Description("项目归档")]
        GuiDang = 30014,
        /// <summary>
        /// 结束节点
        /// </summary>
        [Description("结束节点")]
        End = 40000,
    }

    /// <summary>
    /// 项目状态
    /// </summary>
    public enum ItemState
    {
        /// <summary>
        /// 未进入流程
        /// </summary>
        [Description("未开始")]        
        Beginning = 0,
        /// <summary>
        /// 流程进行中
        /// </summary>
        [Description("进行中")]
        Progressing = 1,
        /// <summary>
        /// 流程已结束
        /// </summary>
        [Description("已结束")]
        Ending = 2
    }

    /// <summary>
    /// 流程状态
    /// </summary>
    public enum WfState
    {
        /// <summary>
        /// 正常状态
        /// </summary>
        [Description("正常")]
        Normal = 0,
        /// <summary>
        /// 暂停状态
        /// </summary>
        [Description("暂停")]
        Stop = 1,
        /// <summary>
        /// 终止状态
        /// </summary>
        [Description("终止")]
        Delete = 2
    }

    /// <summary>
    /// 通知类型
    /// </summary>
    public enum WfNotifyType
    {
        /// <summary>
        /// 任务创建人
        /// </summary>
        [Description("任务创建人")]
        Begin = 1,
        /// <summary>
        /// 下一处理人
        /// </summary>
        [Description("下一处理人")]
        Next = 2,
        /// <summary>
        /// 历史处理人
        /// </summary>
        [Description("历史处理人")]
        History = 3
    }

    /// <summary>
    /// 流程处理类型
    /// </summary>
    public enum WfResult
    {
        /// <summary>
        /// 通过
        /// </summary>
        [Description("通过")]
        Agree = 1,
        /// <summary>
        /// 退回
        /// </summary>
        [Description("退回")]
        Return = 2,
        /// <summary>
        /// 终止
        /// </summary>
        [Description("终止")]
        Delete = 3,
        /// <summary>
        /// 暂停
        /// </summary>
        [Description("暂停")]
        Stop = 4,
        /// <summary>
        /// 启动
        /// </summary>
        [Description("启动")]
        Start = 5
    }

    public enum ZjType
    {
        /// <summary>
        /// 新增费
        /// </summary>
        [Description("新增费")]
        XZF = 1
    }

    /// <summary>
    /// 权属性质
    /// </summary>
    public enum QSXZ
    {
        /// <summary>
        /// 国有
        /// </summary>
        [Description("国有")]
        GY = 1,
        /// <summary>
        /// 集体
        /// </summary>
        [Description("集体")]
        JT = 2,
    }

    /// <summary>
    /// 项目性质
    /// </summary>
    public enum ItemXz
    {
        /// <summary>
        /// 开发
        /// </summary>
        [Description("开发")]
        KF = 1,
        /// <summary>
        /// 整理
        /// </summary>
        [Description("整理")]
        ZL = 2,
        /// <summary>
        /// 复垦
        /// </summary>
        [Description("复垦")]
        FK = 3
    }

    /// <summary>
    /// 项目地貌类型
    /// </summary>
    public enum ItemFiledType
    {
        /// <summary>
        /// 平原
        /// </summary>
        [Description("平原")]
        PY = 1,
        /// <summary>
        /// 丘陵
        /// </summary>
        [Description("丘陵")]
        QL = 2,
        /// <summary>
        /// 山地
        /// </summary>
        [Description("山地")]
        SD = 3
    }

    /// <summary>
    /// 项目菜单
    /// </summary>
    public enum ItemMenu
    {
        /// <summary>
        /// 项目基本信息
        /// </summary>
        [Description("项目基本信息")]
        ItemInfo_BASE = 0,
        /// <summary>
        /// 申报信息
        /// </summary>
        [Description("申报信息")]
        ItemInfo_SB = 1,
        /// <summary>
        /// 可研信息
        /// </summary>
        [Description("可研信息")]
        ItemInfo_KY = 2,
        /// <summary>
        /// 规划设计预算
        /// </summary>
        [Description("规划设计预算")]
        ItemInfo_GHYS = 3,
        /// <summary>
        /// 实施信息
        /// </summary>
        [Description("实施信息")]
        ItemInfo_SS = 20,
        /// <summary>
        /// 招投标信息
        /// </summary>
        [Description("招投标信息")]
        ItemInfo_SS_ZTB = 4,
        /// <summary>
        /// 工程监理信息
        /// </summary>
        [Description("工程监理信息")]
        ItemInfo_SS_JL = 5,
        /// <summary>
        /// 工程进度跟踪
        /// </summary>
        [Description("工程进度跟踪")]
        ItemInfo_SS_JD = 6,
        /// <summary>
        /// 项目变更情况
        /// </summary>
        [Description("项目变更情况")]
        ItemInfo_SS_BG = 7,
        /// <summary>
        /// 资金拨付情况
        /// </summary>
        [Description("资金拨付情况")]
        ItemInfo_SS_BF = 8,
        /// <summary>
        /// 竣工信息
        /// </summary>
        [Description("竣工信息")]
        ItemInfo_JG = 9,
        /// <summary>
        /// 验收信息
        /// </summary>
        [Description("验收信息")]
        ItemInfo_YS = 10,
        /// <summary>
        /// 决算信息
        /// </summary>
        [Description("决算信息")]
        ItemInfo_JS = 11,
        /// <summary>
        /// 归档信息
        /// </summary>
        //[Description("归档信息")]
        //ItemInfo_GD = 12
    }

    /// <summary>
    /// 项目单位类型
    /// </summary>
    public enum ItemCompanyType
    {
        /// <summary>
        /// 筛选单位
        /// </summary>
        [Description("筛选单位")]
        SXSH = 71,
        /// <summary>
        /// 可研审核单位
        /// </summary>
        [Description("可研审核单位")]
        KYSH = 72,
        /// <summary>
        /// 规划设计审核单位
        /// </summary>
        [Description("规划设计审核单位")]
        GHSH = 73,
        /// <summary>
        /// 预算审核单位
        /// </summary>
        [Description("预算审核单位")]
        YSSH = 74,
        /// <summary>
        /// 可研编制单位
        /// </summary>
        [Description("可研编制单位")]
        KY = 81,
        /// <summary>
        /// 规划设计单位
        /// </summary>
        [Description("规划设计单位")]
        GH = 82,
        /// <summary>
        /// 招标代理单位
        /// </summary>
        [Description("招标代理单位")]
        ZB = 83,
        /// <summary>
        /// 承担单位
        /// </summary>
        [Description("承担单位")]
        CD = 84,
        /// <summary>
        /// 施工单位
        /// </summary>
        [Description("施工单位")]
        SG = 85,
        /// <summary>
        /// 监理单位
        /// </summary>
        [Description("监理单位")]
        JL = 86,
        /// <summary>
        /// 测绘单位
        /// </summary>
        [Description("测绘单位")]
        CH = 87,
        /// <summary>
        /// 技术复合单位
        /// </summary>
        [Description("技术复合单位")]
        
        JSFH = 88
    }

    public enum SystemStyle
    {
        [Description("待办风格")]
        WorkFlow = 1,

        [Description("阶段显示")]
        Stage = 2,
    }

    /// <summary>
    /// 饼状图类型
    /// </summary>
    public enum PieType
    {
        [Description("二维饼图")]
        Pie = 0,

        [Description("3D饼图")]
        Pie3D = 1,

        [Description("二维分裂饼图")]
        Exploded = 2,

        [Description("3D分裂饼图")]
        Exploded3D = 3,
    }

    /// <summary>
    /// 图例位置
    /// </summary>
    public enum LegendPosition
    {
        [Description("上")]
        Top = 0,

        [Description("下")]
        Bottom = 1,

        [Description("左")]
        Left = 2,

        [Description("右")]
        Right = 3,
    }

    /// <summary>
    /// 进度跟踪报表类型。
    /// </summary>
    public enum JdgzReportType : int
    {
        /// <summary>
        /// 月报。
        /// </summary>
        M = 1,

        /// <summary>
        /// 季报。
        /// </summary>
        Q = 2
    }

    /// <summary>
    /// 变更信息完成状态。
    /// </summary>
    public enum BgxxCompletedState : int
    {
        /// <summary>
        /// 完成。
        /// </summary>
        Completed = 1,

        /// <summary>
        /// 为完成。
        /// </summary>
        UnCompleted = 0
    }

    /// <summary>
    /// 验证类型。
    /// </summary>
    public enum YSType : int
    {
        /// <summary>
        /// 初验。
        /// </summary>
        [Description("初验")]
        Cy = 1,

        /// <summary>
        /// 终验。
        /// </summary>
        [Description("终验")]
        Zy = 2,

        /// <summary>
        /// 全部。
        /// </summary>
        All
    }

    /// <summary>
    /// 验收状态。
    /// </summary>
    public enum YsState : int
    {
        未验收 = 0,
        通过 = 1,
        未通过 = 2
    }



    /// <summary>
    /// 文件编码。
    /// </summary>
    public enum FileCode : int
    {
        [Description("呈报表")]
        呈报表 = 10,
        [Description("现场踏勘单")]
        现场踏勘单 = 11,
        [Description("1:10000现状图")]
        现状图1_10000 = 12,
        [Description("可研报告")]
        可研报告 = 13,
        [Description("规划设计与预算书")]
        项目规划设计书 = 14,
        [Description("项目预算书")]
        项目预算书 = 15,
        [Description("标书")]
        标书 = 16,
        [Description("招标公告")]
        招标公告 = 17,
        [Description("中标通知书")]
        中标通知书 = 18,
        [Description("施工合同")]
        施工合同 = 19,
        [Description("监理合同")]
        监理合同 = 20,
        [Description("开工通知书")]
        开工通知书 = 21,
        [Description("变更申请书")]
        变更申请书 = 22,
        [Description("变更批复文件")]
        变更批复文件 = 23,
        [Description("标段竣工报告")]
        标段竣工报告 = 24,
        [Description("项目竣工报告")]
        项目竣工报告 = 25,
        [Description("监理总结报告")]
        监理总结报告 = 26,
        [Description("项目初验申请")]
        项目初验申请 = 27,
        [Description("项目初验报告")]
        项目初验报告 = 28,
        [Description("项目终验申请")]
        项目终验申请 = 29,
        [Description("项目终验报告")]
        项目终验报告 = 30,
        [Description("技术复核报告")]
        技术复核报告 = 31,
        [Description("项目决算书")]
        项目决算书 = 32,
        [Description("竣工决算评审报告")]
        竣工决算评审报告 = 33,
        [Description("预决算审查定案通知书")]
        预决算审查定案通知书 = 34,
        [Description("项目立项书")]
        项目立项书 = 35,
        [Description("预算下达书")]
        预算下达书 = 36,
        [Description("预算补充文件")]
        预算补充文件 = 37,
        [Description("项目建议书")]
        项目建议书 = 38,
        [Description("图幅图斑明细(附表)")]
        图幅图斑明细 = 39,
        [Description("地类统计表")]
        地类统计表 = 40,
        [Description("群众意见书")]
        群众意见书 = 41,
    }

    /// <summary>
    /// 合同状态。
    /// </summary>
    public enum ContractState : int
    {
        作废 = 0,
        正常 = 1
    }

    /// <summary>
    /// 文件部署位置
    /// </summary>
    public enum FileFolder
    {
        /// <summary>
        /// 项目文件
        /// </summary>
        [Description("~/Upload/Item")]
        Item = 1,
        /// <summary>
        /// 消息文件
        /// </summary>
        [Description("~/Upload/Message")]
        Message = 2,
        /// <summary>
        /// 地图文件
        /// </summary>
        [Description("~/Upload/Gis")]
        Gis = 3
    }

    /// <summary>
    /// 同步类型。
    /// </summary>
    public enum SyncType
    {
        [Description("先删除再添加")]
        AddAfterDelete = 1,

        [Description("比对")]
        Comparer = 2
    }

    /// <summary>
    /// 文件控件绑定方式。
    /// </summary>
    public enum ItemFileBindingType : int
    {
        CustomBinding = 1,
        WithInBindFileType = 2,
        WithInBindFileTypeWithOutOther = 3,
        WithOutBindFileType = 4
    }

    /// <summary>
    /// 部里接口的三种状态
    /// </summary>
    public enum TdzlStage
    {
        /// <summary>
        /// 土地整治项目_计划和预算下达
        /// </summary>
        [Description("土地整治项目_计划和预算下达")]
        Tdzl_Jh = 1,
        /// <summary>
        /// 土地整治项目_实施
        /// </summary>
        [Description("土地整治项目_实施")]
        Tdzl_Ss = 2,
        /// <summary>
        /// 土地整治项目_验收
        /// </summary>
        [Description("土地整治项目_验收")]
        Tdzl_Ys = 3
    }
}
