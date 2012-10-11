<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScheduleTasksForm.aspx.cs" Inherits="ScheduleTasksForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<telerik:RadStyleSheetManager id="RadStyleSheetManager" runat="server" />
    <link href="~/Styles/main.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
         
        .message { width:800px;
                 
               border:1px solid #d0d049;
                margin:0 auto;
                background-color:#fefad5;
                 
                }
          .error { width:800px;
                 height:35px;
                 border:1px solid #ee5147;
                margin:0 auto;
                background-color:#fba09a;
                 
                }
            .right { width:800px;
                 height:35px;
                 border:1px solid #9dbf3d;
                margin:0 auto;
                background-color:#dff2a9;
                 
                }
        .e_text { width:50px;
                
                      padding-left:10px;
               
                 }
           .m_text { width:50px;
                
                      padding-left:10px;
                 }
            .r_text { width:50px;
                
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
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
	<div class="s_task_title"><span class="t_text">Scheduled Tasks</span></div>
	<div class="s_task">
        <telerik:RadButton ID="rbtnInsert"  runat="server" onclick="rbtnInsert_Click" 
            CssClass="new" ToggleType="CustomToggle" Text="<%$ Resources: WebResource, TaskDetailForm_rbtnNewTask_text %>" 
            EnableAjaxSkinRendering="False" BorderStyle="None" Skin="Web20" 
           >
        </telerik:RadButton>
        <span class="task_setting">
        <telerik:RadButton ID="rbtnBackupSetting" runat="server" 
            Text="<%$ Resources: WebResource, TaskDetailForm_rbtnBackupSetting_text %>" 
            onclick="rbtnBackupSetting_Click" 
           ForeColor="White" Skin="Web20" 
             ToggleType="CustomToggle">
        </telerik:RadButton></span>
        <telerik:RadAjaxPanel ID="RadAjaxPanel" runat="server" Width="800px" 
        LoadingPanelID="RadAjaxLoadingPanel" BorderStyle="None" CssClass="task_box">
            <telerik:RadGrid ID="RadGrid" runat="server" AutoGenerateColumns="False" 
                GridLines="None" AllowPaging="True" AllowSorting="True" 
                onitemcommand="RadGrid_ItemCommand" BorderStyle="None" PageSize="10">
                <MasterTableView DataKeyNames="ScheduleID" EditMode="PopUp" Width="100%" HorizontalAlign="NotSet" AutoGenerateColumns="False">
                  
                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>

                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                    </RowIndicatorColumn>

                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                    </ExpandCollapseColumn>
                        <Columns>
                            
                            <telerik:GridBoundColumn DataField="TaskName" HeaderText="<%$ Resources: WebResource, ScheduleTasksForm_RadGridName_HeaderText %>" FilterControlAltText="TaskName" SortExpression="TaskName"
                                UniqueName="TaskName">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="ScheduleType" HeaderText="<%$ Resources: WebResource, ScheduleTasksForm_RadGridType_HeaderText %>" FilterControlAltText="ScheduleType" SortExpression="ScheduleType"
                                UniqueName="ScheduleType">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Last_run_date" HeaderText="<%$ Resources: WebResource, ScheduleTasksForm_RadGridStatus_HeaderText %>" FilterControlAltText="Last_run_date" SortExpression="Last_run_date"
                                UniqueName="Last_run_date">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Next_run_date" HeaderText="<%$ Resources: WebResource, ScheduleTasksForm_RadGridStartTime_HeaderText %>" FilterControlAltText="Next_run_date" SortExpression="Next_run_date"
                                UniqueName="Next_run_date">
                            </telerik:GridBoundColumn>
                            
                            <%--<telerik:GridBoundColumn DataField="Status" HeaderText="<%$ Resources: WebResource, ScheduleTasksForm_RadGridViewStatus_HeaderText %>" FilterControlAltText="Status" SortExpression="Status"
                                UniqueName="Status">
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridButtonColumn CommandName="LogResult" HeaderText="<%$ Resources: WebResource, ScheduleTasksForm_RadGridViewStatus_HeaderText %>" FilterControlAltText="LogResult" 
                                DataTextField="LogResult" UniqueName="LogResult" SortExpression="LogResult">
                            </telerik:GridButtonColumn>
                            
                            <telerik:GridButtonColumn CommandName="Edit" FilterControlAltText="View" Text="<%$ Resources: WebResource, ScheduleTasksForm_RadGridView_Text %>" UniqueName="View" 
                                ButtonCssClass="edit" ItemStyle-ForeColor="#0066CC" HeaderStyle-Width="15px">
                                <HeaderStyle Width="15px" />
                                <ItemStyle ForeColor="#0066CC" />
                            </telerik:GridButtonColumn>
                            
                            <telerik:GridButtonColumn ButtonType="LinkButton" CommandName="<%$ Resources: WebResource, ScheduleTasksForm_RadGridDelete_Text %>" 
                                ConfirmText="<%$ Resources: WebResource, ScheduleTasksForm_RadGridDelete_ConfirmText %>" ConfirmDialogType="RadWindow"
                             ConfirmTitle="Delete" FilterControlAltText="Delete" 
                                Text="Delete" UniqueName="Delete" HeaderButtonType="LinkButton"
                             ConfirmDialogHeight="100px" ConfirmDialogWidth="220px" ButtonCssClass="delete" ItemStyle-ForeColor="Red" HeaderStyle-Width="15px">
                                <HeaderStyle Width="15px" />
                                <ItemStyle ForeColor="Red" />
                            </telerik:GridButtonColumn>
                            
                            <%--<telerik:GridButtonColumn CommandName="Update" ConfirmTitle="Update" ButtonType="ImageButton"
                             ConfirmText="<%$ Resources: WebResource, ScheduleTasksForm_RadGridUpdate_ConfirmText %>"
                                FilterControlAltText="Update" UniqueName="Update" 
                                DataTextField="StatusImageURL" HeaderStyle-Width="15px" ImageUrl="~/Image/Enable.png">
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>--%>
                        </Columns>
                        <EditFormSettings>
                            <EditColumn FilterControlAltText="Scheduled Tasks">
                            </EditColumn>
                        </EditFormSettings>
                </MasterTableView>

                <HeaderStyle CssClass="task_header" ForeColor="#0066CC" />

                <FilterMenu EnableImageSprites="False" ></FilterMenu>

                <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default"></HeaderContextMenu>
            
            </telerik:RadGrid>
        </telerik:RadAjaxPanel>
	</div>
    <telerik:RadAjaxManager ID="RadAjaxManager" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlMessage" />
                    <telerik:AjaxUpdatedControl ControlID="pnlRight" />
                    <telerik:AjaxUpdatedControl ControlID="pnlError" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
	</form>
    </div>
     <div class="footer">Copyright @ 2010 Able Software, Inc. . All Rights Reserved </div>
</div>
</body>
</html>
