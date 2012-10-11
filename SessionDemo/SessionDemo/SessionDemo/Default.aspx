<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SessionDemo._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<script src="jquery-1.4.1.js" type="text/javascript"></script>
<script src="jquery-1.4.1-vsdoc.js" type="text/javascript"></script>
<script type="text/javascript">
    //关闭窗体之前询问事件 
    function ShowLeaveMsg() {
        //event.returnValue = "请确认已经留下您的联系方式，以便我们联系您\n期待您的再次光临！ ";
        VarSessionExit();
        alert('asdf');
    }
    //关闭窗口事件，用户注销 
    function Leave() {
        //Class1.LeaveRoom().value;     //ajax离开事件程序   
        alert('asdf00');
    }

    function VarSessionExit() {
        $.ajax({
            type: "POST",
            url: "Default.aspx/SessionExit",
            data: "{'ABC':'test'}",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (msg) { alert(msg.d); }
        })
    }
       
</script>
<body onunload="Leave() " onbeforeunload="ShowLeaveMsg() ">
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
    </div>
    </form>
</body>
</html>
