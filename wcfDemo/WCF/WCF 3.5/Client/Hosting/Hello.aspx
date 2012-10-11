<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Hello.aspx.cs"
    Inherits="Hosting_Hello" Title="宿主Hosting(服务宿主在WindowsService)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <ul>
            <li style="color: Red;">本例为宿主在WindowsService的示例</li>
            <li>宿主在IIS请参见本解决方案的ServiceHost项目</li>
            <li>宿主在应用程序请参见本解决方案的ServiceHost2项目</li>
            <li>应用程序自宿主就是把本解决方案的ServiceLib项目和ServiceHost2项目结合在一起</li>
            <li>宿主在Windows Activation Services(WAS)，因为我没有环境，就先不写示例了</li>
        </ul>
    </div>
    <asp:TextBox ID="txtName" runat="server" Text="webabcd" />
    &nbsp;
    <asp:Button ID="btnSayHello" runat="server" Text="Hello" OnClick="btnSayHello_Click" />
</asp:Content>
