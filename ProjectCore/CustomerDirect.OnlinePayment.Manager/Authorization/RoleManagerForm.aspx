<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RoleManagerForm.aspx.cs" Inherits="WebApplication1.Authorization.RoleManagerForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">

        function StandardConfirm(sender, args) {

            $find("<%=UserListDialog.ClientID %>").show();
            args.set_cancel(true);
        }

        function ShowInsertForm() {
            window.radopen("RightsTreeForm.aspx", "UserListDialog");
            return false;
        }

        function GetSelectedRow() {
            var grid = $find("<%=GridRole.ClientID %>");
            var MasterTable = grid.get_masterTableView();
            var row = MasterTable.get_selectedItems()[0]; //getting selected row
            var cell = MasterTable.getCellByColumnUniqueName(row, "RoleID").innerHTML; //getting cell value of selected row
            alert(cell);
        }

        function ShowEditForm(id, rowIndex) {
            var grid = $find("<%= GridRole.ClientID %>");

            var rowControl = grid.get_masterTableView().get_dataItems()[rowIndex].get_element();
            grid.get_masterTableView().selectItem(rowControl, true);

            window.radopen("RightsTreeForm.aspx?RoleID=" + id, "UserListDialog");
            return false;
        }
        function ShowInsertForm() {
            window.radopen("RightsTreeForm.aspx", "UserListDialog");
            return false;
        }
    </script>

    <style type="text/css">
        .style5
        {
            width: 398px;
        }
        .style6
        {
            width: 383px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="GridRole">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="GridRole" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RadWindowManager1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
         <telerik:RadWindowManager ID="RadWindowManager1" runat="server" 
        EnableTheming="True">
            <Windows>
                <telerik:RadWindow ID="UserListDialog" runat="server" Title="Right" Height="450px"
                    Width="350px" Left="170px"  NavigateUrl="~/Authorization/RightsTreeForm.aspx"
                    Modal="true" Behaviors="Close" />
            </Windows>
        </telerik:RadWindowManager>

    <%--提示等待标签--%>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <asp:Label ID="Label1" runat="server" Text="Role Manager" 
        style="font-size: large; font-weight: 700" 
    Font-Names="Arial Rounded MT Bold" Font-Size="Large"></asp:Label>
    <br />&nbsp;
    <telerik:RadGrid ID="GridRole" runat="server" AutoGenerateColumns="False" AllowPaging="True"
        AllowSorting="True" GridLines="None"  CellSpacing="0" DataSourceID="EntityDataSourceRole"
        OnInsertCommand="GridRole_InsertCommand" OnDeleteCommand="GridRole_DeleteCommand"
        OnItemDataBound="GridRole_ItemDataBound" 
        OnUpdateCommand="GridRole_UpdateCommand" 
        AutoGenerateEditColumn="True" 
        onitemcreated="GridRole_ItemCreated">
        <%-- 如果加下面代码表示在RadGrid上下都会出现 --%>
        <MasterTableView CommandItemDisplay="TopAndBottom" Width="100%" DataKeyNames="RoleID"
            HorizontalAlign="NotSet" AutoGenerateColumns="False" DataSourceID="EntityDataSourceRole">
            <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
            <RowIndicatorColumn>
                <HeaderStyle Width="20px"></HeaderStyle>
            </RowIndicatorColumn>
            <ExpandCollapseColumn>
                <HeaderStyle Width="20px"></HeaderStyle>
            </ExpandCollapseColumn>
            <Columns>
                <telerik:GridBoundColumn DataField="RoleName" HeaderText="<%$ Resources: en_US, RoleManagerForm_RoleName_HeaderText %>" UniqueName="RoleName"
                    SortExpression="RoleName" >
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RoleCode" HeaderText="<%$ Resources: en_US, RoleManagerForm_RoleCode_HeaderText %>" UniqueName="RoleCode"
                    SortExpression="RoleCode">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="State" HeaderText="<%$ Resources: en_US, RoleManagerForm_State_HeaderText %>" UniqueName="State"
                    SortExpression="State" DataType="System.Decimal">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Desc" HeaderText="<%$ Resources: en_US, RoleManagerForm_Description_HeaderText %>" UniqueName="Desc" SortExpression="Desc">
                <HeaderStyle Width="300px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CreatedDate" HeaderText="<%$ Resources: en_US, RoleManagerForm_CreatedDate_HeaderText %>" DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}"
                    SortExpression="CreatedDate" UniqueName="CreatedDate">
                    <HeaderStyle Width="100px" />
                </telerik:GridBoundColumn>
                 <telerik:GridButtonColumn  ConfirmText="Are you sure to delete this role?"
                    ConfirmDialogType="RadWindow" ConfirmDialogHeight="115px" ConfirmDialogWidth="270px" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                    Text="Delete" UniqueName="DeleteColumn">
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridButtonColumn>
               
                <telerik:GridTemplateColumn UniqueName="TemplateEditColumn">
                    <ItemTemplate>
                        <asp:HyperLink ID="EditLink" runat="server" Text="Right"></asp:HyperLink>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                
            </Columns>
            <EditFormSettings EditFormType="Template" ColumnNumber="2" CaptionDataField="RoleName">
                <FormTemplate>
                    <table border="2" cellpadding="2" cellspacing="1" style="width: 100%;
                        border-collapse: collapse;margin:auto;">
                        <tr>
                            <td style="width: 70px">
                                <asp:Label ID="LblRoleName" runat="server" Text="<%$ Resources: en_US, RoleManagerForm_RoleName_HeaderText %>"></asp:Label>
                            </td>
                            <td class="style6">
                                <telerik:RadTextBox ID="txtRoleName" runat="server" Width="150px" 
                                    MaxLength="255">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRoleName"
                                    ErrorMessage="The textbox can not be empty!" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td style="width: 70px">
                                <asp:Label ID="LblRoleCode" runat="server" Text="<%$ Resources: en_US, RoleManagerForm_RoleCode_HeaderText %>"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="textRoleCode" runat="server" Width="150px" 
                                    MaxLength="255">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="textRoleCode"
                                    ErrorMessage="The textbox can not be empty!" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDesc" runat="server" Text="<%$ Resources: en_US, RoleManagerForm_Description_HeaderText %>"></asp:Label>
                            </td>
                            <td class="style6">
                                <telerik:RadTextBox ID="textDesc" runat="server" Width="150px" MaxLength="255">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:Label ID="LblState" runat="server" Text="<%$ Resources: en_US, RoleManagerForm_State_HeaderText %>"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="ComBoxState" runat="server" ShowDropDownOnTextboxClick="False">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="True" Value="1" />
                                        <telerik:RadComboBoxItem Text="False" Value="0" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center">
                                <br />
                                <asp:Button ID="btnUpdate" runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                    Text='<%# (Container is GridEditFormInsertItem) ? "Add" : "Update" %>' />
                                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </FormTemplate>
                <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                <FormCaptionStyle></FormCaptionStyle>
                <FormMainTableStyle GridLines="None" CellSpacing="0" CellPadding="3" BackColor="White"
                    Width="100%" />
                <FormTableStyle CellSpacing="0" CellPadding="2" Height="110px" BackColor="White" />
                <FormTableAlternatingItemStyle Wrap="False"></FormTableAlternatingItemStyle>
                <EditColumn ButtonType="ImageButton" InsertText="Insert Order" UpdateText="Update record"
                    UniqueName="EditCommandColumn1" CancelText="Cancel edit">
                </EditColumn>
                <FormTableButtonRowStyle HorizontalAlign="Right"></FormTableButtonRowStyle>
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings>
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
        </ClientSettings>
        <FilterMenu EnableImageSprites="False">
        </FilterMenu>
        <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
        </HeaderContextMenu>
    </telerik:RadGrid>
    <asp:EntityDataSource ID="EntityDataSourceRole" runat="server" EnableFlattening="False">
    </asp:EntityDataSource>
</asp:Content>
