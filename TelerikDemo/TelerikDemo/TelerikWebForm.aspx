<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TelerikWebForm.aspx.cs" Inherits="TelerikWebForm" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Charting" tagprefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<telerik:RadStyleSheetManager id="RadStyleSheetManager1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
	<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
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
	<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
	</telerik:RadAjaxManager>
    <telerik:RadChart ID="RadChart1" runat="server" 
        onitemdatabound="RadChart1_ItemDataBound">
    </telerik:RadChart>
    <telerik:RadToolTipManager ID="RadToolTipManager1" runat="server" Skin="Telerik"
        Width="200px" Animation="Slide" Position="TopCenter" EnableShadow="true" ToolTipZoneID="RadChart1" AutoTooltipify="true">
    </telerik:RadToolTipManager>

	</form>
</body>
</html>
