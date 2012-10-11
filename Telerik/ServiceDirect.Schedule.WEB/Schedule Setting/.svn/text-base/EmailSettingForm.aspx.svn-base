<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailSettingForm.aspx.cs" Inherits="EmailSettingForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<telerik:RadStyleSheetManager id="RadStyleSheetManager" runat="server" />
    <link href="~/Styles/main.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
         body 
        {
          font-size:12px;
          font-family:Arial, Helvetica, sans-serif;}
        .style1
        {
            width: 120px;
            text-align:right;
            vertical-align:top;
            padding-top:8px;
            
        }
      
       .style3 { padding-top:8px;
                 width:30px;}
        .style4
        {
            width: 90px;
            text-align: right;
            height: 26px;
        }
      
        .email_setting 
      {
          width:480px;
          margin:0 auto;
          border:1px solid #267bc6;}
       .spanRed
        {
            color: red;
        }
         .spanRed_port
        {
            color: red;
            line-height:22px;
            float:left;
            margin-left:3px;
            margin-top:-12px\9;
            
          
          
            
        }
       td { vertical-align:top;}
       .message { width:490px;
                 height:35px;
                 border:1px solid #267bc6;
                margin:0 auto;
                background-color:#fefad5;
                 
                }
          .error { width:490px;
                 height:35px;
                 border:1px solid #267bc6;
                margin:0 auto;
                background-color:#fba09a;
                 
                }
            .right { width:490px;
                 height:35px;
                 border:1px solid #267bc6;
                margin:0 auto;
                background-color:#dff2a9;
                 
                }
        .e_text {height:35px;
                 line-height:35px; 
                 display:block;
                 background:url(../Image/error.png) no-repeat 15px 4px;
                 text-indent:60px;
               
                 }
           .m_text {height:35px;
                 line-height:35px; 
                 display:block;
                 background:url(../Image/arrow.png) no-repeat 15px 4px;
                 text-indent:60px;
                 }
            .r_text {height:35px;
                 line-height:35px; 
                 display:block;
                 background:url(../Image/right.png) no-repeat 15px 4px;
                 text-indent:60px;
                 }
    </style>
</head>
<body>
<div class="wrapper">
    <div class="content">
    <div class="header"><div class="logo"><img src="../Image/logo.png" /></div></div>
    <form id="form" runat="server">
	<telerik:RadScriptManager ID="RadScriptManager" runat="server">
		<Scripts>
			<%--Needed for JavaScript IntelliSense in VS2010--%>
			<%--For VS2008 replace RadScriptManager with ScriptManager--%>
			<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
			<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
			<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
		</Scripts>
	</telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnOK">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlMessage" />
                    <telerik:AjaxUpdatedControl ControlID="pnlRight" />
                    <telerik:AjaxUpdatedControl ControlID="pnlError" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <asp:Panel ID="pnlMessage" runat="server">
        <div class="message"><span class="m_text"><asp:Literal ID="lnkMessage" runat="server"></asp:Literal></span></div>
    </asp:Panel>
    <asp:Panel ID="pnlRight" runat="server">
        <div class="right"><span class="r_text"><asp:Literal ID="lnkRight" runat="server"></asp:Literal></span></div>
    </asp:Panel>
    <asp:Panel ID="pnlError" runat="server">
        <div class="error"><span class="e_text"><asp:Literal ID="lnkError" runat="server"></asp:Literal></span></div>
    </asp:Panel><br/>
    <div class="email_title"><span class="t_text">E-mail settings</span></div>
	<div class="email_setting">
        <table style="width: 490px; margin:20px 0px 20px 0px;" cellpadding="5">
            <tr>
                <td class="style1">
                    <asp:Label ID="lblTo" runat="server" Text="<%$ Resources: WebResource, EmailSettingForm_lblTo_text %>"></asp:Label>
                </td>
                <td colspan="3" class="style5">
                    <telerik:RadTextBox ID="rtxtTo" runat="server" Width="220px" 
                        BorderColor="#267BC6" BorderStyle="Solid" BorderWidth="1px" 
                        MaxLength="100">
                    </telerik:RadTextBox>
             <span class="spanRed">* 
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                 runat="server" ErrorMessage="<%$ Resources: WebResource, EmailSettingForm_revRegularFrom_ErrorMessage %>" 
                        ControlToValidate="rtxtTo" 
                        ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" 
                        CssClass="error_message" Font-Size="X-Small"></asp:RegularExpressionValidator></span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                        runat="server" 
                        ErrorMessage="<%$ Resources: WebResource, EmailSettingForm_rfvRequiredFrom_ErrorMessage %>" 
                        ControlToValidate="rtxtTo" CssClass="must_text"></asp:RequiredFieldValidator>
                </td>
            </tr>
            
            <tr>
                <td colspan="4" align="center">
                    <telerik:RadButton ID="btnOK" runat="server" 
                        Text="<%$ Resources: WebResource, EmailSettingForm_btnOK_text %>" 
                        onclick="btnOK_Click"
                        EnableAjaxSkinRendering="False" ForeColor="White" Skin="Web20" 
                        >
                    </telerik:RadButton>
                    <telerik:RadButton ID="btnCancel" runat="server" 
                        Text="<%$ Resources: WebResource, EmailSettingForm_btnCancel_text %>" 
                        onclick="btnCancel_Click" 
                        EnableAjaxSkinRendering="False"  ForeColor="White" 
                        CausesValidation="False" Skin="Web20">
                    </telerik:RadButton>
                </td>
            </tr>
        </table>
	</div>
	</form>
     </div>
     <div class="footer">Copyright @ 2010 Able Software, Inc. . All Rights Reserved </div>
</div>
</body>
</html>
