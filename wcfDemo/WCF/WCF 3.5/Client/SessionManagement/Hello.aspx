<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Hello.aspx.cs"
    Inherits="InstanceMode_Hello" Title="会话状态(Session)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Button ID="btnStartSession" runat="server" Text="StartSession" OnClick="btnStartSession_Click" />
    &nbsp;
    <asp:Button ID="btnCounter" runat="server" Text="Counter" OnClick="btnCounter_Click" />
    &nbsp;
    <asp:Button ID="btnGetSessionId" runat="server" Text="GetSessionId" OnClick="btnGetSessionId_Click" />
    &nbsp;
    <asp:Button ID="btnStopSession" runat="server" Text="StopSession" OnClick="btnStopSession_Click" />
</asp:Content>
