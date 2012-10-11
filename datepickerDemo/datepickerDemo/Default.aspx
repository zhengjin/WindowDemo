<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="datepickerDemo._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <script type="text/javascript">
        function SetWorkingTime() {
            var sUrl = "SetWorkingDays.aspx";
            var returnValue = window.open(sUrl, "", "dialogWidth:50;dialogHeight:40;center:1;scroll:0;status:no;help:no;");
            if (returnValue == "1") {
                window.document.location.href = window.document.location.href;
            }
            return false;
        }
    </script>
    <script src="/js/jquery.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="Button1" runat="server" Text="Button" OnClientClick="SetWorkingTime()"/>
    </form>
</body>
</html>
