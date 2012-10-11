using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;

namespace HHSoft.FieldProtect.FrameWork
{    
    /// <summary>
    /// 防止重复提交
    /// </summary>
    [ToolboxData("<{0}:HHButton CssClass='StandardBtn50' runat=server></{0}:HHButton>")]
    public class HHButton : System.Web.UI.WebControls.Button
    {
        private string _textonclick = "提交中…";
        private string _clientcheck;
        /// <summary>
        /// 重写Button
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            StringBuilder temp = new StringBuilder();

            temp.Append(this.OnClientClick);

            if (!string.IsNullOrEmpty(_clientcheck)) temp.Append("if(!").Append(_clientcheck).Append(") return false;");

            if (!string.IsNullOrEmpty(_textonclick)) temp.Append("this.value=\"").Append(_textonclick).Append("\";");

            temp.Append("if(null==window.onunload) window.onunload = function(){};");
            temp.Append("this.disabled = true;").Append(Page.GetPostBackEventReference(this));
            this.OnClientClick = temp.ToString();
            base.Render(writer);
        }
        /// <summary>
        /// 显示文字
        /// </summary>
        public string TextOnClick
        {
            set { _textonclick = value; }
        }
        /// <summary>
        /// 客户端验证
        /// </summary>
        public string ClientCheck
        {
            set { _clientcheck = value; }
        }      
    }
}
