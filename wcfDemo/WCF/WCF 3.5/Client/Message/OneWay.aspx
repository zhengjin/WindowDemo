<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="OneWay.aspx.cs"
    Inherits="Message_OneWay" Title="消息处理(异步调用OneWay)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        详见：</div>
    <div>
        <ul>
            <li>service: ServiceLib/Message/OneWay.cs</li>
            <li>host: ServiceHost2/Message/OneWay.cs</li>
            <li>client: Client2/Message/OneWay.cs</li>
        </ul>
    </div>
</asp:Content>
