<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage_Dialog.aspx.cs" Inherits="MainPage_Dialog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
	<br />
	<asp:Button ID="Button1" runat="server"
        Text="Return the value" OnClientClick="sendAndClose(); return false;" />
		<br />
		Fill and return the value in the textbox to the parent page

    <script type="text/javascript">
	//this function is used to get a reference to the RadWindow in which the content page is opened
        function GetRadWindow()
        {
            var oWindow = null;
            if (window.radWindow)
                oWindow = window.radWindow;
            else if (window.frameElement.radWindow)
                oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function sendAndClose()
        {
			//get a reference to the RadWindow
            var oWnd = GetRadWindow();
			//create the argument
            var arg = $get("<%= TextBox1.ClientID %>").value;
            if (arg == "")
            {
                alert("Please insert value first");
            }
            else
            {
				//send the argument in RadWindow's close() method
                oWnd.close(arg);
            }
        }
    
    </script>

    </form>
</body>
</html>
