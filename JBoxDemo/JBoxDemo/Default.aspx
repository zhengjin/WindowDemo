<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="JBoxDemo._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link id="skin" rel="stylesheet" href="jbox-v2.3/jBox/Skins2/Green/jbox.css" />

    <script type="text/javascript" src="jbox-v2.3/jBox/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="jbox-v2.3/jBox/jquery.jBox-2.3.min.js"></script>
    <script type="text/javascript" src="jbox-v2.3/jBox/i18n/jquery.jBox-zh-CN.js"></script>

    <script type="text/javascript">
        function demo_1_3() {
            //$.jBox.open($("#divCddw").html(), '项目已暂停', 450, 'auto', { buttons: {}, height: 220 });
            document.getElementById('jbox-iframe').contentWindow.test();
            var sd = document.getElementById('jbox-iframe').contentWindow.valueText;
            alert(sd);
        }

        function GetAnohterPage() {
            $.jBox.open('iframe:JboxForm.aspx', '测试', 800, 350, { buttons: { '关闭': true }, submit: demo_1_3 });
        }

        function tooltip() {
            $.jBox.tip('Hello jBox');
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="divCddw" style="display: none">
        <table align="center" cellpadding="1" cellspacing="1" border="0">
            <tr>
                <td align="left" colspan="4">
                    暂停原因:
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <asp:TextBox ID="txtOpDesc" TextMode="MultiLine" ReadOnly="true" class="Text_Standard"
                        runat="server" Height="120px" Width="320px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <input class="btn" type="button" onclick="tooltip();" value="运　行" />
        <asp:Button ID="Button1" OnClientClick="GetAnohterPage();return false;" runat="server"
            Text="Button" />
    </div>
    </form>
</body>
</html>
