using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace HHSoft.FieldProtect.Framework.Control
{
    /// <summary>
    /// 支持MaxLength属性的textarea
    /// </summary>
    public class HHTextArea:TextBox
    {
        protected override void OnPreRender(EventArgs e)
        {
            if (MaxLength > 0 && TextMode == TextBoxMode.MultiLine)
            {
                // Add javascript handlers for paste and keypress
                this.Attributes.Add("onkeyup", string.Format("return doKeyUp('{0}', event);", ClientID));
                this.Attributes.Add("onbeforepaste", string.Format("return doBeforePaste('{0}', event);", ClientID));
                this.Attributes.Add("onpaste", string.Format("return doPaste('{0}', event);", ClientID));
                this.Attributes.Add("maxLength", this.MaxLength.ToString()); 

                if(!Page.ClientScript.IsClientScriptIncludeRegistered("TextArea"))
                {
                    Page.ClientScript.RegisterClientScriptInclude("TextArea", ResolveClientUrl("~/JavaScript/textArea.js"));
                }
            }
            base.OnPreRender(e);
        }

    }
}
