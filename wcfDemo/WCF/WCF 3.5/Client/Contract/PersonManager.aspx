<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PersonManager.aspx.cs"
    Inherits="Contract_PersonManager" Title="契约Contract(ServiceContract、OperationContract、DataContract、ServiceKnownType和DataMember)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:TextBox ID="txtName" runat="server" Text="webabcd" />
    &nbsp;
    <asp:Button ID="btnGetName" runat="server" Text="GetName" 
        onclick="btnGetName_Click" />
</asp:Content>
