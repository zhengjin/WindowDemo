<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Hello.aspx.cs"
    Inherits="InstanceMode_Hello" Title="实例模型(InstanceContextMode)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Button ID="btnPerCallMode" runat="server" Text="PerCallMode" 
        onclick="btnPerCallMode_Click" />
    &nbsp;
    <asp:Button ID="btnPerSessionMode" runat="server" Text="PerSessionMode" 
        onclick="btnPerSessionMode_Click" />
    &nbsp;
    <asp:Button ID="btnSingleMode" runat="server" Text="SingleMode" 
        onclick="btnSingleMode_Click" />
</asp:Content>
