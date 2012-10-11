using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHSoft.FieldProtect.Framework.Utility;

namespace HHSoft.FieldProtect.Framework.Control
{
    [ToolboxData("<{0}:HHRepeaterHead runat=server></{0}:HHRepeaterHead>")]
    
    public class HHRepeaterHead : LinkButton
    {  
        private bool allowsort = false;
        /// <summary>
        /// 是否排序
        /// </summary>
        [
        DefaultValue(false),
        Description("设置是否该控件是否排序,如果排序则必须设置SortExpression属性"),
        ]
        public bool AllowSort
        {
            get { return allowsort; }
            set { this.allowsort = value; }
        }

        /// <summary>
        /// 排序字段
        /// </summary>
        private string sortexpression = string.Empty;
        [
        DefaultValue(""),
        Description("设置排序字段"),
        ]
        public string SortExpression
        {
            get { return sortexpression; }
            set { this.sortexpression = value; }
        }        
        private bool sortdirection = true;
        /// <summary>
        /// 排序方向 Asc为True Desc为false 默认为升序排列

        /// </summary>
        [
        DefaultValue(true),
        Description("设置排序方向"),
        ]
        public bool SortDirection
        {
            get
            {
                return this.sortdirection;
            }
            set { this.sortdirection = value; }
        }
    }
}
