<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dataTable.aspx.cs" Inherits="TreeTableDemo.dataTable" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript">
        function addit() {
            var n = $("#paramTable tr:last td").eq(1).html();
            if (isNaN(n)) n = 1;
            else n++;
            $("#paramTable").append("<tr><td>name</td><td>" + n + "</td><td onclick='delit(this);'>删除</td></tr>")
        }
        function delit(obj) {
            $(obj).parent().remove();
        }
    </script>
</head>

<body>
    <form id="form1" runat="server">
    <input type="button" onclick="addit()" value="添加">
    <table border=1 id="paramTable"> 
        <tr> 
            <td width=100> 
                参数名称 
            </td> 
            <td width=100> 
                对应列号 
            </td> 
            <td width=60> 
                是否删除 
            </td> 
        </tr> 
        <tr> 
            <td>name</td> 
            <td>1</td> 
            <td onclick="delit(this);">删除</td> 
        </tr> 
    </table>
    </form>
</body>
</html>
