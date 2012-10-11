<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScheduleLogForm.aspx.cs" Inherits="ScheduleLogForm" %>

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
            width: 76px;
            padding: 10px 0px 0px 6px;
            color:#0066CC;
        }
        .style2 { padding: 10px 0px 0px 0px;
                  color:#666666;}
       
        .log_box {width:800px;
                  margin:0 auto;
                  border:1px solid #267bc6;
                  }
        .log_title {width:800px;
         height:28px;
         line-height:28px;
         color:#fff;
         font-weight:bold;
         font-size:16px;
         margin:0 auto;
         background:url(../Image/title_bg.png) no-repeat;}
        .log_text {display:block;
           width:154px;
           text-align:center;}
    </style>
</head>
<body>
<div class="wrapper">
    <div class="content">
    <div class="header"><div class="logo"><img src="../Image/logo.png" /></div></div>
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
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
	<telerik:RadAjaxManager ID="RadAjaxManager" runat="server">
	</telerik:RadAjaxManager>
     <div class="log_title"><span class="log_text">Scheduled Logs</span></div>
	<div class="log_box">
        <table style="width: 100%; margin-bottom:20px;">
            <tr>
                <td class="style1">
                    <asp:Label ID="lbl_Name" runat="server" Text="<%$ Resources: WebResource, ScheduleLogForm_lblTaskName_Text %>"></asp:Label>
                </td>
                <td class="style2">
                    <asp:Label ID="lbl_TaskName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="lbl_User" runat="server" Text="<%$ Resources: WebResource, ScheduleLogForm_lblUserName_Text %>"></asp:Label>
                </td>
                <td class="style2">
                    <asp:Label ID="lbl_UserName" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <telerik:RadAjaxPanel ID="RadAjaxPanel" runat="server" Width="800px">
            <telerik:RadGrid ID="RadGrid" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" GridLines="None">
                <MasterTableView DataKeyNames="SID" EditMode="PopUp" Width="100%" HorizontalAlign="NotSet" AutoGenerateColumns="False">
                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" HeaderStyle-ForeColor="#0066CC">
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridDateTimeColumn DataField="SDT" HeaderText="<%$ Resources: WebResource, ScheduleLogForm_RadGridScheeduleDate_HeaderText %>" FilterControlAltText="SDT" SortExpression="SDT"
                                UniqueName="SDT">
                        </telerik:GridDateTimeColumn>
                        <telerik:GridBoundColumn DataField="SSubject" HeaderText="<%$ Resources: WebResource, ScheduleLogForm_RadGridSubject_HeaderText %>" FilterControlAltText="SSubject" SortExpression="SSubject"
                                UniqueName="SSubject">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="SMessage" HeaderText="<%$ Resources: WebResource, ScheduleLogForm_RadGridMessage_HeaderText %>" FilterControlAltText="SMessage" SortExpression="SMessage"
                                UniqueName="TaskName">
                        </telerik:GridBoundColumn>
                        <telerik:GridDateTimeColumn DataField="GDT" HeaderText="<%$ Resources: WebResource, ScheduleLogForm_RadGridLogDate_HeaderText %>" FilterControlAltText="GDT" SortExpression="GDT"
                                UniqueName="GDT">
                        </telerik:GridDateTimeColumn>
                    </Columns>
                    <EditFormSettings>
                        <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <HeaderStyle ForeColor="#0066CC" />
                <FilterMenu EnableImageSprites="False">
                </FilterMenu>
                <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
                </HeaderContextMenu>
            </telerik:RadGrid>
        </telerik:RadAjaxPanel>
         <div style="text-align:center; margin:15px 0px 15px 0px;">
         <telerik:RadButton ID="rbtn_Close" runat="server" onclick="rbtn_Close_Click" 
            Text="<%$ Resources: WebResource, ScheduleLogForm_btnClose_text %>" 
                  EnableAjaxSkinRendering="False" 
                  ForeColor="White" 
                 Skin="Web20">
        </telerik:RadButton></div>
	</div>
    <div >
       
    </div>
	</form>
    </div>
     <div class="footer">Copyright @ 2010 Able Software, Inc. . All Rights Reserved </div>
</div>
</body>
</html>
