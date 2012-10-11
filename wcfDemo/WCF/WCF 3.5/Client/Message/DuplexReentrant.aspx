<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DuplexReentrant.aspx.cs"
    Inherits="Message_DuplexReentrant" Title="并发和限流(Concurrent和Throttle)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <ul>
            <li>ConcurrencyMode.Single：单线程并发模式。系统自动加锁，无并发问题</li>
            <ul>
                <li>InstanceContextMode.PerCall：每个线程都会被分配一个新的实例</li>
                <li>InstanceContextMode.PerSession：每个Session被分配一个新的实例，每个Session内同时只会有一个线程操作实例</li>
                <li>InstanceContextMode.Single：唯一实例，并发调用只会有一个线程操作实例</li>
            </ul>
        </ul>
        <ul>
            <li>ConcurrencyMode.Reentrant：可重入的单线程并发模式。有可重入（回调）操作时，此模式才会生效，从回调返回的线程会进入队列尾部排队</li>
            <ul>
                <li>InstanceContextMode.PerCall：每个线程都会被分配一个新的实例，当有回调操作时如果使用Single并发模式的话就会产生死锁（1、调用服务端；2、回调客户端；3、返回服务端，1的时候锁定了，到3的时候就无法执行了，所以死锁了），此时应该用Reentrant并发模式</li>
                <li>InstanceContextMode.PerSession：每个Session被分配一个新的实例，每个Session内同时只会有一个线程操作实例，Session内可重入</li>
                <li>InstanceContextMode.Single：唯一实例，并发调用只会有一个线程操作实例，全局可重入</li>
            </ul>
        </ul>
        <ul>
            <li>ConcurrencyMode.Multiple：多线程并发模式。系统不会自动加锁，有并发问题</li>
            <ul>
                <li>InstanceContextMode.PerCall：每个线程都会被分配一个新的实例，无并发问题</li>
                <li>InstanceContextMode.PerSession：每个Session被分配一个新的实例，每个Session内多线程操作实例的话会有并发问题</li>
                <li>InstanceContextMode.Single：唯一实例，允许多线程并发操作实例，有并发问题</li>
            </ul>
        </ul>
    </div>
    <div>
        ConcurrencyMode.Reentrant的示例详见：</div>
    <div>
        <ul>
            <li>service: ServiceLib/Message/DuplexReentrant.cs</li>
            <li>host: ServiceHost2/Message/DuplexReentrant.cs</li>
            <li>client: Client2/Message/DuplexReentrant.cs</li>
        </ul>
    </div>
    <div>
        实现限流的配置：
<textarea style="width: 98%; height: 180px">
    <behaviors>
        <serviceBehaviors>
            <behavior name="BehaviorPerCall">
                <!--httpGetEnabled - 指示是否发布服务元数据以便使用 HTTP/GET 请求进行检索，如果发布 WSDL，则为 true，否则为 false，默认值为 false-->
                <serviceMetadata httpGetEnabled="true"/>
                <!--maxConcurrentCalls - 服务中同时存在的最大活动消息数，默认值为 16-->
                <!--maxConcurrentInstances - 服务中同时存在的最大服务实例数，默认值为 Int32.MaxValue-->
                <!--maxConcurrentSessions - 服务中同时存在的最大会话数，默认值为 10-->
                <serviceThrottling maxConcurrentCalls="" maxConcurrentInstances="" maxConcurrentSessions="" />
            </behavior>
            <behavior name="BehaviorPerSession">
                <serviceMetadata httpGetEnabled="true"/>
                <serviceThrottling maxConcurrentCalls="" maxConcurrentInstances="" maxConcurrentSessions="" />
            </behavior>
            <behavior name="BehaviorSingle">
                <serviceMetadata httpGetEnabled="true"/>
                <serviceThrottling maxConcurrentCalls="" maxConcurrentInstances="1" maxConcurrentSessions="" />
            </behavior>
        </serviceBehaviors>
    </behaviors>
</textarea>
    </div>
</asp:Content>
