<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Streamed.aspx.cs"
    Inherits="Message_Streamed" Title="消息处理(使用流数据传输文件)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        详见：</div>
    <div>
        <ul>
            <li>service: ServiceLib/Message/Streamed.cs</li>
            <li>host: ServiceHost2/Message/Streamed.cs</li>
            <li>client: Client2/Message/Streamed.cs</li>
        </ul>
    </div>
    <div>
        <ul>
            <li>支持数据流传输的绑定有：BasicHttpBinding、NetTcpBinding 和 NetNamedPipeBinding</li>
            <li>流数据类型必须是可序列化的 Stream 或 MemoryStream </li>
            <li>传递时消息体(Message Body)中不能包含其他数据，即传递的参数中只能有一个System.ServiceModel.MessageBodyMember（可以对其进行封装）
            </li>
        </ul>
    </div>
</asp:Content>
