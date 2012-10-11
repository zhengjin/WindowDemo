<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Sample.aspx.cs"
    Inherits="Transaction_Sample" Title="事务(Transaction)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <p>
        <asp:Label ID="lblErr" runat="server" ForeColor="Red" />
    </p>
    <p>
        <asp:Button ID="btnSubmit" runat="server" Text="事务测试" OnClick="btnSubmit_Click" />
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
    </p>
    <p>
        2PC（Two Phase Commitment Protocol）两阶段提交协议（WCF的事务的实现基于此协议）
        <br />
        实现分布式事务的关键就是两阶段提交协议。在此协议中，一个或多个资源管理器的活动均由一个称为事务协调器的单独软件组件来控制。此协议中的五个步骤如下：
        <br />
        1、应用程序调用事务协调器中的提交方法。
        <br />
        2、事务协调器将联络事务中涉及的每个资源管理器，并通知它们准备提交事务（这是第一阶段的开始）。
        <br />
        3、为 了以肯定的方式响应准备阶段，资源管理器必须将自己置于以下状态：确保能在被要求提交事务时提交事务，或在被要求回滚事务时回滚事务。大多数资源管理器会将包含其计划更改的日记文件（或等效文件）写入持久存储区中。如果资源管理器无法准备事务，它会以一个否定响应来回应事务协调器。
        <br />
        4、事务协调器收集来自资源管理器的所有响应。
        <br />
        5、在 第二阶段，事务协调器将事务的结果通知给每个资源管理器。如果任一资源管理器做出否定响应，则事务协调器会将一个回滚命令发送给事务中涉及的所有资源管理 器。如果资源管理器都做出肯定响应，则事务协调器会指示所有的资源管理器提交事务。一旦通知资源管理器提交，此后的事务就不能失败了。通过以肯定的方式响应第一阶段，每个资源管理器均已确保，如果以后通知它提交事务，则事务不会失败。
    </p>
</asp:Content>
