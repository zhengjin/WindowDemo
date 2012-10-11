<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="openWindow.aspx.cs" Inherits="datepickerDemo.openWindow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    
    <script type="text/javascript" src="js/popup.js"></script>
    <script type="text/javascript" src="js/popupclass.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <p align="center">
        <div style="height: 200px;"></div>
        <input  type="button" value="打开百度" onclick="ShowIframe('百度','http://www.baidu.com',800,450)"/><br/>  
        <input  type="button" value="HTML字符串" onclick="ShowHtmlString('字符串','<B>Hello,PopWin',400,200)"/><br/>  
        <input  type="button" value="信息提示框" onclick="ShowAlert('提示框','<B>Hello,PopWin',200,100)"/><br/>  
        <input  type="button" value="是否确认框" onclick="ShowConfirm('确定','是否删除','Button4','',340,80)"/> 
        <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem Text="sd"></asp:ListItem>
            <asp:ListItem Text="sd"></asp:ListItem>
            <asp:ListItem Text="sd"></asp:ListItem>
        </asp:DropDownList>
    </p>
    </form>
</body>
</html>

