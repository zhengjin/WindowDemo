<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="my97Demo.aspx.cs" Inherits="datepickerDemo.my97Demo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript" src="/My97DatePicker/WdatePicker.js"></script>
    <script language="javascript">
        function setCheck(obj, checkId) {
            alert(checkId);
        
        }          
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <input class="Wdate" type="text" onClick="WdatePicker()">
    <asp:TextBox ID="TextBox1" runat="server" onchange="setCheck(this,'checkBoxSfzb');" ></asp:TextBox>
    <input class="Wdate" type="text" onFocus="WdatePicker({maxDate:'#F{$dp.$D(\'d4312\')||\'2020-10-01\'}'})">
    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
    </form>
</body>
</html>
