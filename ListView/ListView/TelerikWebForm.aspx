<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TelerikWebForm.aspx.cs" Inherits="TelerikWebForm" %>

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
    <telerik:RadListView ID="CDProListView" runat="server" Height="100px" ItemPlaceholderID="CardImage" DataSourceID="EntityDataSource1">
        <LayoutTemplate>
            <asp:PlaceHolder ID="CardImage" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <EmptyItemTemplate>
            No data display.
        </EmptyItemTemplate>
        <ItemTemplate>
            <input name="checkCon" type="radio" id="checkCon" value='<%#Eval("ImgURL") %>'/>
            <asp:Image ID="imgCard" runat="server" ImageUrl='<%#Eval("ImgURL") %>' />
        </ItemTemplate>
    </telerik:RadListView>
    <asp:EntityDataSource ID="EntityDataSource1" runat="server">
    </asp:EntityDataSource>
	</form>
</body>
</html>
