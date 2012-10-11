<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Demo.aspx.cs" Inherits="Demo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AJAX调用WCF</title>
</head>
<body>
    <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Services>
                <asp:ServiceReference Path="Service/AJAX.svc" />
            </Services>
        </asp:ScriptManager>
        <asp:Label ID="lblMsg" runat="server" />
        <script type="text/javascript">
        
            function pageLoad() 
            {
                var proxy = new WCF.IAJAX();
                proxy.GetUser("webabcd", onSuccess);
            }
            
            function onSuccess(result) 
            {
                $get('<%= lblMsg.ClientID %>').innerHTML = 
                    String.format("姓名：{0}<br />生日：{1}", result.Name, result.DayOfbirth.format("yyyy-MM-dd"));
            }

        </script>
    </form>
</body>
</html>
