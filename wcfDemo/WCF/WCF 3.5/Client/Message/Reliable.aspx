<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Reliable.aspx.cs"
    Inherits="Message_Reliable" Title="可靠性消息(ReliableMessaging)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Button ID="btnReliable" runat="server" Text="可靠性消息测试" OnClick="btnReliable_Click" />
    <p>
        测试方法：
        <br />
        1、用TcpTrace监听8888端口，目标端口3502
        <br />
        2、程序调用proxy.Hello("1")后马上停止Trace，过一会再打开Trace，发现程序还会调用proxy.Hello("2");
    </p>
    <p>
        备注：
        <br />
        1、通过重试的方法来保证消息的可靠传递，默认为8次
        <br />
        2、当配置了“有序传递”的时候，客户端和服务端会开辟缓冲区，服务端缓冲区在接到所有客户端发来的消息后，按照客户端调用的顺序排序各个消息，然后有序地调用服务端
    </p>
</asp:Content>
