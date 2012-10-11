<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="openWindow_Dialog.aspx.cs" Inherits="openWindow_Dialog" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

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
        Text="Return the value"
        onclick="Button1_Click" />
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
    <div class="tPageHeadWrap">
        <!-- Section Menu -->
        <div class="tSectionMenu">
            <div class="tSectionTitle"><span style="font-weight: normal; font-size: 22px; color: #74757e;">Your Telerik Account</span><br/> </div>           
        </div><%--
		<div class="tInnerWrap tPageHead tPlainHead">
                <h1></h1>	
		</div>--%>
	</div>

	<div class="tPageMain tPlainPage tClear">
		<div class="tPageLeft">
		    <div class="tRTF">
			    <h1>Just One More Step ...</h1>
                Please, register or log in to get access to:
                <ul> 
                    <li>Free Trials</li> 
                    <li>Free and Purchased Products</li> 
                    <li>Support</li> 
                    <li>Forums</li> 
                </ul>
                <table id="loginForm" style="border-collapse: collapse;" border="0" cellpadding="0" cellspacing="0">
                    <tbody>
                    <tr>
                        <td>
                            <div id="loginForm_panelLogin">
			
                                <fieldset class="tForm">
                                <span id="loginForm_FailureText"></span>
                                <h2>Log In to your Telerik account</h2>
                                <ol class="tFormFieldsList">
                                    <li class="fFirstInGroup" id="tInputToBeFocus">
                                    <label for="loginForm_UserName" id="loginForm_Label">Email</label>
                                        <span class="txtWrapper">
	                                        <input name="$loginForm$UserName" id="loginForm_UserName" class="txt" onchange="TrimUsername(this);" type="text">
	                                        <span id="loginForm_EmailFormat" class="fFieldErrorMsg" style="color:Red;display:none;">
                                        <strong>Email format is not valid</strong>
	                                        </span>
	                                        <span id="loginForm_EmailRequired" class="fFieldErrorMsg" style="color:Red;display:none;">
                                            <strong>Email is required</strong>
	                                        </span>
                                        </span>
                                    </li>
                                    <li class="pass">
                                        <label for="loginForm_Password" id="loginForm_Label2">Password</label>
                                        <span class="txtWrapper">
	                                        <input name="ctl00$PageContent$usercontrols_public_clientnet_signinform_ascx5$loginForm$Password$tbSanitized" id="ctl00_PageContent_usercontrols_public_clientnet_signinform_ascx5_loginForm_Password_tbSanitized" class="txt" onchange="extraValidation" type="password">

	                                        <span id="ctl00_PageContent_usercontrols_public_clientnet_signinform_ascx5_loginForm_PasswordRequired" class="fFieldErrorMsg" style="color:Red;display:none;">
                                            <strong>Password is required</strong>
	                                        </span>
                                        </span>
                                    </li>
                                    <li class="tFormCheckbox">
                                        <input id="ctl00_PageContent_usercontrols_public_clientnet_signinform_ascx5_loginForm_RememberMe" name="ctl00$PageContent$usercontrols_public_clientnet_signinform_ascx5$loginForm$RememberMe" type="checkbox"><label for="ctl00_PageContent_usercontrols_public_clientnet_signinform_ascx5_loginForm_RememberMe">Remember me</label>
                                    </li>
                                </ol>
                                <p class="tFormButton">
                                <input name="ctl00$PageContent$usercontrols_public_clientnet_signinform_ascx5$loginForm$LoginButton" value="Login" onclick='javascript:WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions("ctl00$PageContent$usercontrols_public_clientnet_signinform_ascx5$loginForm$LoginButton", "", true, "ctl00$Login", "", false, false))' id="ctl00_PageContent_usercontrols_public_clientnet_signinform_ascx5_loginForm_LoginButton" class="btnSubmit" type="submit">
                                </p>
                                </fieldset>
		
                            </div>
                        </td>
                    </tr>
                    </tbody>
                </table>
                <p class="loghelp">
                    <strong>Help: </strong>
                    <a id="ctl00_PageContent_usercontrols_public_clientnet_signinform_ascx5_hlForgotPassword" href="https://www.telerik.com/registration-login/forgotten-password.aspx">I forgot my email or password</a>
                </p>
		    </div>
		</div>
	</div>
    </form>
</body>
</html>
