<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Sample.aspx.cs"
    Inherits="Binding_Sample" Title="绑定Binding(basicHttpBinding和netTcpBinding)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        详见：</div>
    <div>
        <ul>
            <li>service: ServiceLib/Binding/Hello.cs</li>
            <li>host: ServiceHost2/Binding/Hello.cs</li>
            <li>client: Client2/Binding/Hello.cs</li>
        </ul>
    </div>
</asp:Content>
