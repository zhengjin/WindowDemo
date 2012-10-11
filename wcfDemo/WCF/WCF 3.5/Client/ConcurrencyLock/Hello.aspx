<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Hello.aspx.cs"
    Inherits="ConcurrencyLock_Hello" Title="并发控制(锁)(Mutex, Semaphore, Monitor, Lock, ThreadPool, Interlocked, ReaderWriterLock)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <p>
        <asp:Button ID="btnCleanResult" runat="server" Text="清空结果" OnClick="btnCleanResult_Click" />
        &nbsp;
        <asp:Button ID="btnHelloNone" runat="server" Text="HelloNone" OnCommand="btn_Command"
            CommandName="None" />
        &nbsp;
        <asp:Button ID="btnHelloMutex" runat="server" Text="HelloMutex" OnCommand="btn_Command"
            CommandName="Mutex" />
        &nbsp;
        <asp:Button ID="btnHelloSemaphore" runat="server" Text="HelloSemaphore" OnCommand="btn_Command"
            CommandName="Semaphore" />
        &nbsp;
        <asp:Button ID="btnHelloMonitor" runat="server" Text="HelloMonitor" OnCommand="btn_Command"
            CommandName="Monitor" />
        &nbsp;
        <asp:Button ID="btnHelloLock" runat="server" Text="HelloLock" OnCommand="btn_Command"
            CommandName="Lock" />
        <br />
        <ul>
            <li>None：不使用并发控制（有并发问题，会出现重复的计数）</li>
            <li>其他：使用相应的并发控制（无并发问题，不会出现重复的计数）</li>
        </ul>
    </p>
    <div>
        <asp:TextBox ID="txtResult" runat="server" TextMode="MultiLine" Style="width: 98%;
            height: 200px" />
    </div>
    <div>
        <ul>
            <li>Mutex - 提供对资源的独占访问</li>
            <li>Semaphore - 限制可同时访问某一资源或资源池的线程数</li>
            <li>Monitor - 提供同步访问对象的机制</li>
            <li>Lock - 关键字将语句块标记为临界区，方法是获取给定对象的互斥锁，执行语句，然后释放该锁</li>
            <li>ThreadPool - 提供一个线程池，该线程池可用于发送工作项、处理异步 I/O、代表其他线程等待以及处理计时器</li>
            <li>Interlocked - 为多个线程共享的变量提供原子操作</li>
            <li>ReaderWriterLock - 定义支持单个写线程和多个读线程的锁</li>
        </ul>
    </div>
</asp:Content>
