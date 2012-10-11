<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="logon.aspx.cs" Inherits="logon" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<telerik:RadStyleSheetManager id="RadStyleSheetManager" runat="server" />
    <link type="text/css" rel="stylesheet" href="Styles/main.css"/>
    <%--<link type="text/css" rel="stylesheet" href="Styles/Forms.css"/>
    <link type="text/css" rel="stylesheet" href="Styles/Global.css"/>
    <link type="text/css" rel="stylesheet" href="Styles/Menu.css"/>
    <link type="text/css" rel="stylesheet" href="Styles/Telerik.css"/>--%>
</head>
<body class="BODY">
    <form id="form" runat="server">
	<telerik:RadScriptManager ID="RadScriptManager" runat="server">
		<Scripts>
			<%--Needed for JavaScript IntelliSense in VS2010--%>
			<%--For VS2008 replace RadScriptManager with ScriptManager--%>
			<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
			<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
			<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
		</Scripts>
	</telerik:RadScriptManager>
	<script type="text/javascript">
		//Put your JavaScript code here.
    </script>
	<telerik:RadAjaxManager ID="RadAjaxManager" runat="server">
	</telerik:RadAjaxManager>
    <div>
        <div id="content" class="center">
            <!--LOG In -->
            <div id="LogCss" style="width: 250px; height: 0px; position: relative; left: 830px;
                top: 5px;" class="style2">
                <a href="#" class="a1">Log In</a> or <a href="#" class="a1">Register </a>(Why?)
            </div>
            <!--LOG In -->
            <div class="bottom">
                <div class="box_outer">
                    <!--横向菜单-->
                    <div>
                        <!--LOGO-->
                        <div style="background-color: #000000; height: 100px;">
                            <br />
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Image/logo.png" />
                            <br />
                        </div>
                    </div>
                    <!--横向菜单-->
                    <div class="test">
                        &nbsp;
                        <br />
                        &nbsp;&nbsp;<span class="style3">Log In to your mobile work order</span>
                        <br />
                        &nbsp;
                        <!--中间白色DIV-->
                        <table width="950" align="center" style="height: 200px">
                            <tr>
                                <td valign="top">
                                    <div id="loginMain">
                                        <div id="loginForm_panelLogin">
                                            <fieldset class="tForm">
                                            
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
                                                <telerik:RadButton ID="rbtnLogin" runat="server" Text="Login">
                                                </telerik:RadButton>
                                            </p>
                                            </fieldset>
                                        </div>
                                        <p class="loghelp">
                                            <strong>Help: </strong>
                                            <a id="ctl00_PageContent_usercontrols_public_clientnet_signinform_ascx5_hlForgotPassword" href="https://www.telerik.com/registration-login/forgotten-password.aspx">I forgot my email or password</a>
                                        </p>
		                            </div>
                                </td>
                            </tr>
                        </table>
                        <!--中间白色DIV-->
                    </div>
                </div>
            </div>
        </div>
    </div>
	</form>
</body>
</html>
