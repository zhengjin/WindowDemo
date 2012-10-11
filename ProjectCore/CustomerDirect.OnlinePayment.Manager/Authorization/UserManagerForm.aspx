<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserManagerForm.aspx.cs" Inherits="WebApplication1.Authorization.UserManagerForm" %>
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
            var grid = $find("<%=GridUser.ClientID %>");
            var MasterTable = grid.get_masterTableView();
            var row = MasterTable.get_selectedItems()[0]; //getting selected row
            var cell = MasterTable.getCellByColumnUniqueName(row, "RoleID").innerHTML; //getting cell value of selected row
            alert(cell);
        }

        function ShowEditForm(id, rowIndex) {
            var grid = $find("<%= GridUser.ClientID %>");

            var rowControl = grid.get_masterTableView().get_dataItems()[rowIndex].get_element();
            grid.get_masterTableView().selectItem(rowControl, true);

            window.radopen("SetPasswordForm.aspx?UserID=" + id, "UserListDialog");
            return false;
        }

        function ShowInsertForm() {
            window.radopen("RightsTreeForm.aspx", "UserListDialog");
            return false;
        }

        function alertCallBackFn(arg) {
            radalert("<strong>radalert</strong> returned the following result: <h3 style='color: #ff0000;'>" + arg + "</h3>", null, null, "Result");
        }
    </script>
    <style type="text/css">
        .style5
        {
            width: 394px;
        }
           
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="GridUser">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="GridUser" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RadWindowManager1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" 
    EnableTheming="True">
        <Windows>
            <telerik:RadWindow ID="UserListDialog" runat="server" Title="Set Password" Height="250px"
                Width="370px" Left="170px"  NavigateUrl="SetPasswordForm.aspx"
                Modal="true" Behaviors="Close" />
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
 <asp:Label ID="Label1" runat="server" Text="User Manager" 
        style="font-size: large; font-weight: 700" 
    Font-Names="Arial Rounded MT Bold" Font-Size="Large"></asp:Label>
    <br />&nbsp;
    <telerik:RadGrid ID="GridUser" runat="server" AutoGenerateColumns="False" AllowPaging="True"
        AllowSorting="True" GridLines="None" CellSpacing="0" DataSourceID="EntityDataSourceGridUser"
        OnInsertCommand="GridUser_InsertCommand" OnDeleteCommand="GridUser_DeleteCommand"
        OnItemDataBound="GridUser_ItemDataBound" 
        OnUpdateCommand="GridUser_UpdateCommand" 
        onitemcreated="GridUser_ItemCreated">
        <%-- 如果加下面代码表示在RadGrid上下都会出现 --%>
        <MasterTableView CommandItemDisplay="TopAndBottom" Width="100%" DataKeyNames="UserID"
            HorizontalAlign="NotSet" AutoGenerateColumns="False" DataSourceID="EntityDataSourceGridUser">
            <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
            <RowIndicatorColumn>
                <HeaderStyle Width="20px"></HeaderStyle>
            </RowIndicatorColumn>
            <ExpandCollapseColumn>
                <HeaderStyle Width="20px"></HeaderStyle>
            </ExpandCollapseColumn>
            <Columns>
                <telerik:GridBoundColumn DataField="UserName" HeaderText="<%$ Resources: en_US, UserManagerForm_UserName_Text %>" UniqueName="UserName"
                    SortExpression="UserName">
                  <HeaderStyle Width="120px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="LoginName" HeaderText="<%$ Resources: en_US, UserManagerForm_LblLoginName_Text %>" UniqueName="LoginName"
                    SortExpression="LoginName">
                    <HeaderStyle Width="120px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="State" HeaderText="<%$ Resources: en_US, UserManagerForm_lblState_Text %>" UniqueName="State"
                    SortExpression="State" DataType="System.Decimal">
                    <HeaderStyle Width="50px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RoleName" HeaderText="<%$ Resources: en_US, UserManagerForm_lblRole_Text%>" UniqueName="RoleName" SortExpression="RoleName">
                <HeaderStyle Width="100px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CreatedDate" HeaderText="<%$ Resources: en_US, UserManagerForm_CreatedDate_Text %>" DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}"
                    SortExpression="CreatedDate" UniqueName="CreatedDate">
                    <HeaderStyle Width="80px" />
                </telerik:GridBoundColumn>
                  
                <telerik:GridBoundColumn DataField="Email" DataType="System.String" HeaderText="<%$ Resources: en_US, UserManagerForm_lblEmail_Text %>"
                    SortExpression="Email" UniqueName="Email">
                    <HeaderStyle Width="100px" />
                </telerik:GridBoundColumn>

                    <telerik:GridButtonColumn   ConfirmTitle="Edit" ButtonType="LinkButton" CommandName="Edit"
                    Text="Edit" UniqueName="EditColumn">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="50px" />
                </telerik:GridButtonColumn>

                 <telerik:GridButtonColumn  ConfirmText="Are you sure to delete this user?"
                    ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ConfirmDialogHeight="115px" ConfirmDialogWidth="270px" ButtonType="LinkButton" CommandName="Delete"
                    Text="Delete" UniqueName="DeleteColumn">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="50px" />
                </telerik:GridButtonColumn>
                      <telerik:GridTemplateColumn UniqueName="TemplateEditColumn">
                        <ItemTemplate>
                            <asp:HyperLink ID="EditLink" runat="server" Text="Set Password"></asp:HyperLink>
                        </ItemTemplate><HeaderStyle Width="90px" />
                    </telerik:GridTemplateColumn>
            </Columns>
            <EditFormSettings EditFormType="Template" ColumnNumber="2" CaptionDataField="RoleName">
                <FormTemplate>
                    <table align="center" border="2" cellpadding="2" cellspacing="1" style="width: 100%;
                        border-collapse: collapse;">
                        <tr>
                            <td style="width: 70px;">
                                <asp:Label ID="LblRoleName" runat="server" Text="<%$ Resources: en_US, UserManagerForm_UserName_Text %>"></asp:Label>
                            </td>
                            <td class="style9">
                                <telerik:RadTextBox ID="textUserName" runat="server" Width="150px" 
                                    MaxLength="255">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="Req_UserName" runat="server" 
                                    ControlToValidate="textUserName" ForeColor="Red" 
                                    ErrorMessage="<%$ Resources: en_US, UserManagerForm_Req_Empty_Text%>"></asp:RequiredFieldValidator>
                            </td>
                            <td style="width: 70px">
                                <asp:Label ID="LblRoleCode" runat="server" Text="<%$ Resources: en_US, UserManagerForm_LblLoginName_Text%>"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="textLoginName" runat="server" Width="150px" 
                                    MaxLength="255">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="textLoginName"
                                    ErrorMessage="<%$ Resources: en_US, UserManagerForm_Req_Empty_Text%>" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <asp:Label ID="lblUserCode" runat="server" Text="<%$ Resources: en_US, UserManagerForm_UserCode_Text %>"></asp:Label>
                            </td>
                            <td class="style9">
                                <telerik:RadTextBox ID="textUserCode" runat="server" Width="150px" 
                                    MaxLength="255">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblEmail" runat="server" Text="<%$ Resources: en_US, UserManagerForm_lblEmail_Text %>"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="textEmail" runat="server" Width="150px" MaxLength="255">
                                </telerik:RadTextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                    ControlToValidate="textEmail" ErrorMessage="<%$ Resources: en_US, UserManagerForm_lblEmailError_Text %>" ForeColor="Red" 
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                      <tr style='<%# (Container is GridEditFormInsertItem) ? "display:table-row" : "display:none" %>'>
                            <td style="width:100px;">
                                <asp:Label ID="lblLoginPwd" runat="server" 
                                    
                                    Text="<%$ Resources:en_US, UserManagerForm_LoginPwd_Text %>"></asp:Label>
                            </td>
                            <td class="style9">
                                <telerik:RadTextBox ID="textLoginPwd" runat="server" Width="150px" 
                                    TextMode="Password" MaxLength="255">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="Req_UserName0" runat="server"  Visible='<%# (Container is GridEditFormInsertItem) ? true : false %>'
                                    ControlToValidate="textLoginPwd" 
                                    ErrorMessage="<%$ Resources: en_US, UserManagerForm_Req_Empty_Text%>" 
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td style="width:130px;">
                                <asp:Label ID="lblConfirmLoginPwd" runat="server" 
                                    
                                    Text="<%$ Resources:en_US, UserManagerForm_lblConfirmLoginPwd_Text %>"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="textConfirmLoginPwd" runat="server" Width="150px" 
                                    TextMode="Password" MaxLength="255">
                                </telerik:RadTextBox>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                    ControlToCompare="textLoginPwd" ControlToValidate="textConfirmLoginPwd" 
                                    ErrorMessage="<%$ Resources: en_US, UserManagerForm_ThePasswordsDoNotMatch_Text%>" 
                                    ForeColor="Red"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <asp:Label ID="lblLoginPwd0" runat="server" Text="<%$ Resources: en_US, UserManagerForm_lblRole_Text%>"></asp:Label>
                            </td>
                            <td class="style9">
                                <telerik:RadComboBox ID="ComBoxRole" runat="server" 
                                    ShowDropDownOnTextboxClick="False" Width="150px">                                   
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:Label ID="LblState" runat="server" Text="<%$ Resources: en_US, UserManagerForm_lblState_Text%>"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="ComBoxState" runat="server" 
                                    ShowDropDownOnTextboxClick="False" Width="150px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="True" Value="True" />
                                        <telerik:RadComboBoxItem Text="False" Value="False" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                  
                        <tr>
                            <td >
                                <asp:Label ID="LblDesc" runat="server" Text="<%$ Resources: en_US, UserManagerForm_lblDesc_Text%>"></asp:Label>
                            </td>
                            <td class="style9" colspan="3">
                                <telerik:RadTextBox ID="textDesc" runat="server" Width="450px" MaxLength="255">
                                </telerik:RadTextBox>
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
    <asp:EntityDataSource ID="EntityDataSourceGridUser" runat="server" EnableFlattening="False">
    </asp:EntityDataSource>
    <asp:EntityDataSource ID="EntityDataSourceRole" runat="server">
    </asp:EntityDataSource>
</asp:Content>
