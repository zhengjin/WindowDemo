<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TelerikWebForm1.aspx.cs" Inherits="TelerikWebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<style type="text/css">
	    .RemoveRotatorBorder div.rrClipRegion
        {
            border: 0px none;
        }
	    .itemTemplate
        {
            text-align:center;
            padding:15px;
            vertical-align:middle;
        }
          .RotatorImage
        {
            margin:0px 11px;
            cursor:hand;
            cursor: pointer;
        }
	</style>
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
	    function ShowWindow(oRotator, args) {
	        var elementH = args.get_item().get_element().getElementsByTagName("input");
	        if (elementH[0].type = "hidden") {
	            alert(elementH[0].value);
	            args.set_cancel(true);
	        }
        }
    </script>
	<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
	</telerik:RadAjaxManager>
	<div>
        <telerik:RadRotator ID="thumbRotator" runat="server" 
            RotatorType="SlideShowButtons" Width="695px" Height="100px" 
            ItemHeight="60px" ItemWidth="100px" 
            ScrollDirection="Left,Right" WrapFrames="False" 
            onclientitemclicking="ShowWindow" CssClass="RemoveRotatorBorder">
            <ItemTemplate>
                <div class="itemTemplate">
                    <telerik:RadBinaryImage ID="ServiceLogo" runat="server" DataValue='<%# DataBinder.Eval(Container.DataItem, "Image") %>' CssClass="RotatorImage"/>
                        <br />
                    <%# DataBinder.Eval(Container.DataItem, "Name")%>
                    <input id="Hidden1" type="hidden" value='<%# DataBinder.Eval(Container.DataItem, "serviceId")%>'/>
                </div>
            </ItemTemplate>
        </telerik:RadRotator>
	</div>
	</form>
</body>
</html>
