using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HHSoft.FieldProtect.Framework.Control
{
    /// <summary>
    /// 带功能路径的权限按钮
    /// </summary>
    [ToolboxData("<{0}:HHImageButton runat=server></{0}:HHImageButton>")]
    public class HHImageButton:ImageButton
    {
        private string functionUrl;
        /// <summary>
        /// 按钮对应的功能路径
        /// </summary>
        [Bindable(true),
        Category("权限路径"),
        DefaultValue(""),
        Description("按钮对应的功能路径")]
        public string FunctionUrl
        {
            get
            {
                return this.functionUrl;
            }
            set
            {
                this.functionUrl = value;
            }
        }
    }
}
