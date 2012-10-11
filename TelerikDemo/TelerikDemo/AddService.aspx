<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddService.aspx.cs" Inherits="CustomerDirect.OnlinePayment.Manager.Maintenance.AddService" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table border="2" cellpadding="2" cellspacing="1" style="width: 100%;">
        <tr>
            <td>
                Name :
            </td>
            <td>
                <telerik:RadTextBox ID="rtxtMerchantName" runat="server"
                    MaxLength="50">
                </telerik:RadTextBox>
                <asp:RequiredFieldValidator ID="rfvMerchantName" Text="!" runat="server" ControlToValidate="rtxtMerchantName"
                    ErrorMessage="<%$ Resources: en_US, EditMember_rfvMerchantName_Text %>"
                ValidationGroup="vcGroup"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Description :
            </td>
            <td >
                <telerik:RadTextBox ID="rtxtRegCode" runat="server"
                    MaxLength="50">
                </telerik:RadTextBox>
                <asp:RequiredFieldValidator ID="rfvRegCode" Text="!" runat="server" ControlToValidate="rtxtRegCode"
                    ErrorMessage="<%$ Resources: en_US, EditMember_rfvRegCode_Text %>"
                ValidationGroup="vcGroup"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Logo
            </td>
            <td >
                <telerik:RadAsyncUpload runat="server" ID="AsyncUpload1" AllowedFileExtensions="jpg,jpeg,png,gif"
                    MaxFileSize="548576" MaxFileInputsCount="1">
                </telerik:RadAsyncUpload>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnUpdate" runat="server"
                    Text="Add" ValidationGroup="vcGroup" onclick="btnUpdate_Click"/>
                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                    Text="Cancel" />
            </td>
        </tr>
    </table>
</asp:Content>
