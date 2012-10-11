<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Mtom.aspx.cs"
    Inherits="Message_Mtom" Title="消息处理(使用消息传输优化机制 - MTOM)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <p>
        MTOM(Message Transmission Optimization Mechanism) - 消息传输优化机制
    </p>
    <div>
        <ul>
            <li>可以指定用 MTOM 还是 Text 对 SOAP 消息编码</li>
            <li>抓soap消息的时候可以用tcpTrace</li>
            <li>用17,766,901字节大小的文件测试：Text编码（soap大小：31,591,929字节）；MTOM编码（soap大小：23,696,066字节）</li>
        </ul>
    </div>
    <div>
        源文件：
        <asp:FileUpload ID="file" runat="server" />
        &nbsp;
        上传路径：
        <asp:TextBox ID="txtDestination" runat="server" Text="C:\"></asp:TextBox>
        &nbsp;
        <asp:Button ID="btnUpload" runat="server" Text="上传" OnClick="btnUpload_Click" />
    </div>
</asp:Content>
