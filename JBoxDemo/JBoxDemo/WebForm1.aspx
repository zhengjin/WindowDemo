<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="JBoxDemo.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function demo_1_3() {
            $.jBox.open($("#divCddw").html(), '项目已暂停', 450, 'auto', { buttons: {}, height: 220 });
        }

        function GetAnohterPage() {
            var s = document.frames("frameInfo");
            s.sd();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Button ID="Button1" OnClientClick="GetAnohterPage();return false;" runat="server"
            Text="Button" />
    <iframe id="frameInfo" name="frameInfo" runat="server" frameborder="0" scrolling="auto"
                                style="width: 100%; height: 100%;overflow-y: hidden;" src="JboxForm.aspx"></iframe>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    </div>
    </form>
</body>
</html>
