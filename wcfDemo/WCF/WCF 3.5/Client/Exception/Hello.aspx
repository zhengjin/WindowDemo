<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Hello.aspx.cs"
    Inherits="Exception_Hello" Title="异常处理(Exception、FaultException、FaultException&lt;T&gt;、IErrorHandler)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <p>
        <asp:Label ID="lblMsg" runat="server" />
    </p>
    <p>
        <asp:Button ID="btnHelloException" runat="server" Text="HelloException" OnClick="btnHelloException_Click" />
    </p>
    <p>
        <asp:Button ID="btnHelloFaultException" runat="server" Text="HelloFaultException"
            OnClick="btnHelloFaultException_Click" />
    </p>
    <p>
        <asp:Button ID="btnHelloFaultExceptionGeneric" runat="server" Text="HelloFaultExceptionGeneric"
            OnClick="btnHelloFaultExceptionGeneric_Click" />
    </p>
    <p>
        <asp:Button ID="btnHelloIErrorHandler" runat="server" Text="HelloIErrorHandler" OnClick="btnHelloIErrorHandler_Click" />
    </p>
</asp:Content>
