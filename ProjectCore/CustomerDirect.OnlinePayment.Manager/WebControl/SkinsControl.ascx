<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SkinsControl.ascx.cs" Inherits="WebApplication1.WebControl.SkinsControl" %>

<telerik:RadScriptManager ID="rsmCDPro" runat="server">
	<Scripts>
		<%--Needed for JavaScript IntelliSense in VS2010--%>
		<%--For VS2008 replace RadScriptManager with ScriptManager--%>
		<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
		<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
		<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
	</Scripts>
</telerik:RadScriptManager>
    
<%--皮肤管理--%>
<telerik:RadFormDecorator ID="rfdCDPro" runat="server" DecoratedControls="All"/>
<telerik:RadSkinManager ID="rsmSDPro" runat="server">
</telerik:RadSkinManager>
