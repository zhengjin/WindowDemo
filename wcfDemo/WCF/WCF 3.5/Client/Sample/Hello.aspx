<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Hello.aspx.cs"
    Inherits="Sample_Hello" Title="不能免俗，我也从Hello开始" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:TextBox ID="txtName" runat="server" Text="webabcd" />
    &nbsp;
    <asp:Button ID="btnSayHello" runat="server" Text="Hello" OnClick="btnSayHello_Click" />
</asp:Content>
