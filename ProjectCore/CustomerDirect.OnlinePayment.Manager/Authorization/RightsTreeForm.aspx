<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RightsTreeForm.aspx.cs" Inherits="RightsTreeForm" %>

<%@ Register TagPrefix="qsf" TagName="SkinsControl" Src="~/WebControl/SkinsControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<telerik:RadStyleSheetManager id="rssmSDPro" runat="server" >
        <StyleSheets>
            <telerik:StyleSheetReference IsCommonCss="False" Path="~/Styles/Site.css" />
        </StyleSheets>
    </telerik:RadStyleSheetManager>
</head>
<body>
    <form id="form1" runat="server">
	<%--皮肤控制--%>
    <qsf:SkinsControl runat="server" ID="skinControl" />
	
    <script type="text/javascript">
        function CloseAndRebind(args) {
            GetRadWindow().BrowserWindow.refreshGrid(args);
            GetRadWindow().close();
        }

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)

            return oWindow;
        }

        function CancelEdit() {
            GetRadWindow().close();
        }
    </script>
    <asp:Label ID="lbl_RoleName" runat="server" Text="Role Name:" CssClass="classInfo"></asp:Label>
    <asp:Label ID="lbl_RoleNames" runat="server" CssClass="classInfo"></asp:Label>
    <table><tr><td style="height:2px;"></td></tr></table>
    <div class="divs" style="width: 100%; height: 300px; overflow: auto; overflow-x: hidden;
        border: 1px solid;">
        <telerik:RadTreeView ID="RadTreeView_Rigth" Width="300px" runat="server" CheckBoxes="True"
            CheckChildNodes="True" TriStateCheckBoxes="True">
        </telerik:RadTreeView>
    </div>
    
    <table width="100%">
    <tr><td style="height:2px"></td></tr>
        <tr>
            <td align="right">
                <telerik:RadButton ID="butSave" runat="server" Text="Save" OnClick="butSave_Click">
                </telerik:RadButton>
                <telerik:RadButton ID="butCancel" runat="server" Text="Cancel" OnClick="butCancel_Click">
                </telerik:RadButton>
            </td>
        </tr>
    </table>
	</form>
</body>
</html>
