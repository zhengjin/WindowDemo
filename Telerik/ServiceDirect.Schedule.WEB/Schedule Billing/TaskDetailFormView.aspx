<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaskDetailFormView.aspx.cs" Inherits="TaskDetailFormView" %>

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
            width: 220px;
            text-align:right;
        }
        .style2
        {
            width: 110px;
        }
        
        .message { width:670px;
                 height:35px;
                 border:1px solid #d0d049;
                margin:0 auto;
                background-color:#fefad5;
                 
                }
          .error { width:670px;
                 height:35px;
                 border:1px solid #ee5147;
                margin:0 auto;
                background-color:#fba09a;
                 
                }
            .right { width:670px;
                 height:35px;
                  border:1px solid #9dbf3d;
                margin:0 auto;
                background-color:#dff2a9;
                 
                }
        .e_text {width:50px;
                 padding-left:10px;
                 }
           .m_text {width:50px;
                 padding-left:10px;
                 }
            .r_text {width:50px;
                 padding-left:10px;
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
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnConfirm">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlMessage" />
                    <telerik:AjaxUpdatedControl ControlID="pnlRight" />
                    <telerik:AjaxUpdatedControl ControlID="pnlError" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
	</telerik:RadAjaxManager>
    <asp:Panel ID="pnlMessage" runat="server">
        <table class="message">
            <tr>
               <td class="m_text" valign="top"><img src="../Image/arrow.png" /></td>
               <td><asp:Literal ID="lnkMessage" runat="server"></asp:Literal></td>
            </tr>
        </table>
       
    </asp:Panel>
    <asp:Panel ID="pnlRight" runat="server">
        <table class="right">
             <tr>
                 <td class="r_text" valign="top"><img src="../Image/right.png" /></td>
                 <td><asp:Literal ID="lnkRight" runat="server"></asp:Literal></td>
             </tr>
        </table>
       
    </asp:Panel>
    <asp:Panel ID="pnlError" runat="server">
        <table class="error">
            <tr>
                <td class="e_text" valign="top"><img src="../Image/error.png" /></td>
                <td><asp:Literal ID="lnkError" runat="server"></asp:Literal></td>
            </tr>
        </table>
       
    </asp:Panel><br/>
    <div class="title"><span class="t_text">Task details</span></div>
	<div class="box">
        <telerik:RadAjaxPanel ID="RadAjaxPanel" runat="server" Width="600px" LoadingPanelID="RadAjaxLoadingPanel">
            <table style="width: 100%; margin:20px 0px 20px 30px;" cellpadding="5">
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblTaskName" runat="server" Text="<%$ Resources: WebResource, TaskDetailForm_lblTaskName_text %>"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblTeskName" runat="server" CssClass="view_label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblScheduleType" runat="server" Text="<%$ Resources: WebResource, TaskDetailForm_lblScheduleType_text %>"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="rcbScheduleType"  runat="server" CssClass="view_label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblStartTime" runat="server" Text="<%$ Resources: WebResource, TaskDetailForm_lblStartTime_text %>"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="rdtimepStartTime" runat="server" CssClass="view_label"></asp:Label>
                    </td>
                </tr>
                <%--<tr>
                    <td class="style1">
                        <asp:Label ID="lblROOTD" runat="server" Text="<%$ Resources: WebResource, TaskDetailForm_lblROOTD_text %>"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:CheckBoxList ID="ckxlROOTD" runat="server" RepeatColumns="2" Width="210px" 
                            BorderColor="#297dc8" BorderStyle="Solid" BorderWidth="1px">
                        <asp:ListItem Text="Sunday" Value="Sunday"></asp:ListItem>
                        <asp:ListItem Text="Monday" Value="Monday"></asp:ListItem>
                        <asp:ListItem Text="Tuesday" Value="Tuesday"></asp:ListItem>
                        <asp:ListItem Text="Wednesday" Value="Wednesday"></asp:ListItem>
                        <asp:ListItem Text="Thursday" Value="Thursday"></asp:ListItem>
                        <asp:ListItem Text="Friday" Value="Friday"></asp:ListItem>
                        <asp:ListItem Text="Saturday" Value="Saturday"></asp:ListItem>
                    </asp:CheckBoxList>
                    </td>
                </tr>--%>
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblROBTT" runat="server" Text="<%$ Resources: WebResource, TaskDetailForm_lblROBTT_text %>"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="RadTimePicker2" runat="server" CssClass="view_label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        
                    </td>
                    <td>
                        <asp:CheckBox ID="chkBDBB" runat="server" 
                            Text="Backup database before billing" CssClass="view_label"/>
                    </td>
                    <td class="style2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblAction" runat="server" Text="<%$ Resources: WebResource, TaskDetailForm_lblAction_text %>"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="rcbAction" CssClass="view_label"></asp:Label>
                    </td>
                    <td class="style2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblPostAction" runat="server" Text="<%$ Resources: WebResource, TaskDetailForm_lblPostAction_text %>"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="rcbPostAction" CssClass="view_label"></asp:Label>
                    </td>
                    <td class="style2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3" class="td_detail" >
                        <telerik:RadButton ID="btnEdit" runat="server" 
                            Text="<%$ Resources: WebResource, TaskDetailForm_btnEdit_text %>" 
                             ForeColor="White" Skin="Web20" onclick="btnEdit_Click">
                        </telerik:RadButton>
                        <telerik:RadButton ID="btnConfirm" runat="server" 
                            Text="<%$ Resources: WebResource, TaskDetailFormView_btnConfirm_text %>" 
                             EnableAjaxSkinRendering="False" 
                             ForeColor="White" Skin="Web20" 
                             onclick="btnConfirm_Click">
                        </telerik:RadButton>
                        <telerik:RadButton ID="btnCancel" runat="server" 
                            Text="<%$ Resources: WebResource, TaskDetailFormView_btnCancel_text %>" 
                             EnableAjaxSkinRendering="False" 
                             ForeColor="White" Skin="Web20" 
                            onclick="btnCancel_Click">
                        </telerik:RadButton>
                    </td>
                </tr>
            </table> 
        </telerik:RadAjaxPanel>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel" runat="server" Skin="Default">
        </telerik:RadAjaxLoadingPanel>
	</div>
	</form>
 </div>
     <div class="footer">Copyright @ 2010 Able Software, Inc. . All Rights Reserved </div>
</div>
</body>
</html>
