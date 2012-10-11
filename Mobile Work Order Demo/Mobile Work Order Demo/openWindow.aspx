<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="openWindow.aspx.cs" Inherits="openWindow" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<telerik:RadStyleSheetManager id="RadStyleSheetManager1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
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
	 <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="Button2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadWindowManager1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
	<div>
   
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" 
            OnClientClose="OnClientClose" EnableAriaSupport="True">
        <Windows>
                <telerik:RadWindow ID="RadWindowContentTemplate" Title="Insert Test"
                    Width="300px" Height="300px" runat="server" EnableAriaSupport="False" Modal="true"
                    OnClientShow="OnClientShow">
                    <ContentTemplate>
                        <div class="contentDiv">
                            <h4 style="margin-left: 60px;">
                                This is test window content</h4>
                            <br />
                            <div class="inputWrap">
                                <label id="firstNameLabel" for="firstName">
                                    First Name</label>
                                <input type="text" runat="server" name="first-name" id="firstName" />
                            </div>
                            <div class="inputWrap">
                                <label for="lastName">
                                    Last Name</label>
                                <input type="text" runat="server"  name="last-name" id="lastName" />
                            </div>
                            <div class="inputWrap">
                                <label for="email">
                                    Email</label>
                                <input type="text" runat="server" name="email" id="email" />
                            </div>
                            <div class="inputWrap">
                                <input type="submit" value="Submit" name="btnSubmit" id="btnSubmit" class="submitBtn"
                                    onclick="closeWin();return false;" />
                                <telerik:RadButton ID="RadButton1" runat="server" Text="RadButton" onclick="RadButton2_Click">
                                </telerik:RadButton>
                            </div>
                        </div>
                    </ContentTemplate>
                </telerik:RadWindow>
            </Windows>
    </telerik:RadWindowManager>
    1. 
        <telerik:RadButton ID="RadButton2" runat="server" Text="RadButton" 
            onclick="RadButton2_Click">
        </telerik:RadButton>
	<asp:Button ID="Button1" runat="server" Text="Open RadWindow" OnClientClick="openWin(); return false;" />
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:LinkButton ID="LinkButton" runat="server" 
            onclientclick="btnClick();return false;">LinkButton</asp:LinkButton>
            
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            
        <script type="text/javascript">
        function openWin()
        {
            var oWnd = radopen("openWindow_Dialog.aspx", "RadWindow1");
        }

        function OnClientClose(sender, args)
        {
            var txtField = $get("<%= TextBox1.ClientID %>");
            txtField.value = args.get_argument();
        }

        function OnClientShow() {
            //$get("firstName").focus();
        }

        function btnClick() {
            $find('<%= RadWindowContentTemplate.ClientID%>').show();
        }
        function closeWin() {
            $find('<%= RadWindowContentTemplate.ClientID%>').close();
        }
        </script>

    </telerik:RadCodeBlock>
	    <button style="width: 150px;" onclick="$find('<%= RadWindowContentTemplate.ClientID%>').show();return false;">
                Show RadWindow</button>
	</div>

    <telerik:RadListView ID="RadListView1" runat="server"  DataSourceID="EntityDataSource1"
        ItemPlaceholderID="ParentNode" GroupPlaceholderID="ParentNode" 
        GroupItemCount="40" DataKeyNames="MenuPermissionID">
        <LayoutTemplate>
            <asp:PlaceHolder ID="ParentNode" runat="server" />
        </LayoutTemplate>

        <GroupTemplate>
            <fieldset style="float: left; width: 330px; margin-right:15px;">
                <legend>Employees group</legend>
                <table>
                    <tr>
                        <td>
                            <asp:PlaceHolder ID="ParentNode" runat="server" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </GroupTemplate>
        <EmptyItemTemplate>
            <div style="display:none">
                <%--<asp:CheckBox ID="CheckBox" runat="server" />--%>
            </div>
        </EmptyItemTemplate>

        <ItemTemplate>
            <div style="float: left; width: 160px;">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <%--<%# Eval("MenuName")%>--%>
                            <asp:CheckBox ID="CheckBox" runat="server" Text='<%# Eval("MenuName")%>' />
                            <%--<asp:CheckBoxList ID="CheckBoxList_equipment" runat="server" 
                                RepeatDirection="Horizontal" RepeatColumns="5" DataValueField='<%# Eval("MenuName")%>' DataTextField='<%# Eval("MenuName")%>'>
                                <asp:ListItem Text="No Data" Value="No Data"></asp:ListItem>
                            </asp:CheckBoxList>--%>
                        </td>
                    </tr>
                </table>
            </div>
        </ItemTemplate>
    </telerik:RadListView>
	<asp:EntityDataSource ID="EntityDataSource1" runat="server" 
        ConnectionString="name=UnicornDBEntities" 
        DefaultContainerName="UnicornDBEntities" EnableFlattening="False" 
        EntitySetName="tblMenuRight">
    </asp:EntityDataSource>
	</form>
</body>
</html>
