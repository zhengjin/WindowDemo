using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHSoft.FieldProtect.DataEntity.SysManage
{
    /// <summary>
    /// 项目列表列。
    /// </summary>
    public class ItemListColumn
    {
        /// <summary>
        /// 列名。
        /// </summary>
        public string ColumnHeaderName { get; set; }

        /// <summary>
        /// 列宽。
        /// </summary>
        public string Width { get; set; }

        /// <summary>
        /// 标题格Class名称。
        /// </summary>
        public string HeaderClass { get; set; }

        /// <summary>
        /// 标题格对其方式。
        /// </summary>
        public string HeaderAlign { get; set; }

        /// <summary>
        /// 标题格高度。
        /// </summary>
        public string HeaderHeight { get; set; }

        /// <summary>
        /// 内容格Class名称。
        /// </summary>
        public string CellClass { get; set; }

        /// <summary>
        /// 内容格对其方式。
        /// </summary>
        public string CellAlign { get; set; }

        /// <summary>
        /// 内容格高度。
        /// </summary>
        public string CellHeight { get; set; }

        /// <summary>
        /// 列数据项名称。
        /// </summary>
        public string ColumnDataName { get; set; }

        /// <summary>
        /// 项目列表列类型。
        /// </summary>
        public ItemListColumnType Type { get; set; }
    }

    /// <summary>
    /// 项目列表列类型。
    /// </summary>
    public enum ItemListColumnType
    {
        /// <summary>
        /// 常规字符串。
        /// </summary>
        Common,

        /// <summary>
        /// 时间格式。
        /// </summary>
        Time,

        /// <summary>
        /// 操作。
        /// </summary>
        Operation,

        /// <summary>
        /// 项目编号。
        /// </summary>
        ItemCode,

        /// <summary>
        /// 流程状态。
        /// </summary>
        WorkflowState,

        /// <summary>
        /// 项目状态。
        /// </summary>
        ProgramState
    }
}
