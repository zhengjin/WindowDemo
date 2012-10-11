<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebScriptEndpointDemo.aspx.cs"
    Inherits="WebHost.WebScriptEndpointDemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <!--
            因为 ajax 要同域，所以此 ajax 演示的 client 端要和 host 端在一起
        -->

        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Services>
                <asp:ServiceReference Path="WebScriptEndpointDemo.svc" />
            </Services>
        </asp:ScriptManager>

        <script type="text/javascript">

            function pageLoad() {
                var proxy = new WCF.IAjaxDemo();
                proxy.Hello("webabcd", onSuccess, onFailed);
            }

            function onSuccess(result) {
                alert(result);
            }

            function onFailed(error) {
                alert(error.get_message());
            }

        </script>
    </div>
    </form>
</body>
</html>
