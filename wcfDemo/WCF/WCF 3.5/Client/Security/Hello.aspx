<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Hello.aspx.cs"
    Inherits="Sample_Security" Title="安全(Security)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <p>
        以用户名和密码做验证，通过X.509证书做加密为例
    </p>
    <p>
        <asp:Label ID="lblMsg" runat="server" />
    </p>
    <p>
        用户名：<asp:TextBox ID="txtUserName" runat="server" Text="webabcd" />
        &nbsp; 
        密码：<asp:TextBox ID="txtPassword" runat="server" Text="webabcd" />
    </p>
    <p>
        <asp:TextBox ID="txtName" runat="server" Text="webabcd" />
        &nbsp;
        <asp:Button ID="btnSayHello" runat="server" Text="Hello" OnClick="btnSayHello_Click" />
    </p>
</asp:Content>
