<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModuelIDForm.aspx.cs" Inherits="ModuelIDForm" %>

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
            width: 74px;
            text-align:right;
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
	<script type="text/javascript">
		//Put your JavaScript code here.
    </script>
	<telerik:RadAjaxManager ID="RadAjaxManager" runat="server">
	</telerik:RadAjaxManager>
	<div class="moduel_id">
        <table style="width:300px; margin:20px 0px 20px 50px;" cellpadding="5">
            <tr>
                <td class="style1">
                    <asp:Label ID="lblModuelId" runat="server" Text="<%$ Resources: WebResource, ModuelIDForm_lblModuelId_text %>"></asp:Label>
                </td>
                <td>
                    
                    <telerik:RadTextBox ID="rtxtModuelId" Runat="server" Width="180px" 
                        BorderColor="#267BC6" BorderStyle="Solid" BorderWidth="1px">
                    </telerik:RadTextBox>
                    
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="lblURL" runat="server" Text="<%$ Resources: WebResource, ModuelIDForm_lblURL_text %>"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="rtxtURL" Runat="server" Width="180px" 
                        BorderColor="#267BC6" BorderStyle="Solid" BorderWidth="1px">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <telerik:RadButton ID="rbtnOK" runat="server" 
                        Text="<%$ Resources: WebResource, ModuelIDForm_rbtnOK_text %>" 
                         EnableAjaxSkinRendering="False" 
                        ForeColor="White" Skin="Web20">
                    </telerik:RadButton>
                    <telerik:RadButton ID="rbtnCancel" runat="server" 
                        Text="<%$ Resources: WebResource, ModuelIDForm_rbtnCancel_text %>" 
                        EnableAjaxSkinRendering="False" 
                        ForeColor="White" Skin="Web20">
                    </telerik:RadButton>
                </td>
            </tr>
        </table>
	</div>
	</form>
      </div>
     <div class="footer">Copyright @ 2010 Able Software, Inc. . All Rights Reserved d </div>
</div>
</body>
</html>
