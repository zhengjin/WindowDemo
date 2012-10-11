using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHSoft.FieldProtect.Framework.Control
{
    /// <summary>
    /// DoubleDropDownList控件使用的数据源接口
    /// </summary>
    public interface IDoubleDropDownSource
    {
        /// <summary>
        /// 值
        /// </summary>
        string DisplayValue { get; set; }
        /// <summary>
        /// 显示内容
        /// </summary>
        string DisplayName { get; set; }

        /// <summary>
        /// 是否可移动(针对目标数据中不可以移动的项目)
        /// </summary>
        bool AllowMove { get;}
    }
}
