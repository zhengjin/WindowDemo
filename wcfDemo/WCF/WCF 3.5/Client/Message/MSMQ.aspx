<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MSMQ.aspx.cs"
    Inherits="Message_MSMQ" Title="消息队列(MSMQ - MicroSoft Message Queue)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        详见：</div>
    <div>
        <ul>
            <li>service: ServiceLib/Message/MSMQ.cs</li>
            <li>host: ServiceHost2/Message/MSMQ.cs</li>
            <li>client: Client2/Message/MSMQ.cs</li>
        </ul>
    </div>
    <div>
        &nbsp;</div>
    <div>
        netMsmqBinding的binding属性配置
    </div>
    <div>
        <ul>
            <li>ExactlyOnce - 确保消息只被投递一次。只能应用于事务型队列，默认值 ture</li>
            <li>Durable - 消息是否需要持久化。默认值 enabled，如果设置为disable，当MSMQ服务重启后，先前保存在MSMQ中的消息将会丢失</li>
            <li>TimeToLive - 消息过期并且从原有的队列移动到死信队列的时间。默认值 1.00:00:00 （1天）</li>
            <li>ReceiveRetryCount - 配置队列管理器在一定重试间隔中，尝试重新投递消息的次数，也就是将消息传输到重试队列前尝试发送该消息的最大次数（每隔RetryCycleDelay的时间重试ReceiveRetryCount次）。缺省值 5</li>
            <li>MaxRetryCycles - 配置队列管理器重新投递消息的重试间隔数（执行RetryCycleDelay的次数），也就是重试最大周期数。缺省值 2</li>
            <li>RetryCycleDelay - 表示两次重试之间的间隔时间，也就是重试周期之间的延迟。缺省值 00:30:00</li>
            <li>ReceiveErrorHandling - 指定如何处理错误的消息。Fault、Drop、Reject或Move（具体说明查MSDN）</li>
            <li>DeadLetterQueue - 指定所使用的死信队列的类型。None、System、或Custom（具体说明查MSDN）</li>
            <li>CustomDeadLetterQueue - 本地自定义死信队列的URI</li>
        </ul>
    </div>
</asp:Content>
