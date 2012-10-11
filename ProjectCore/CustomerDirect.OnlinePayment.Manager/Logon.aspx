<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logon.aspx.cs" Inherits="Logon" %>

<%@ Register TagPrefix="qsf" TagName="SkinsControl" Src="~/WebControl/SkinsControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<telerik:RadStyleSheetManager id="rssmSDPro" runat="server" >
        <StyleSheets>
            <telerik:StyleSheetReference IsCommonCss="False" Path="~/Styles/Site.css" />
        </StyleSheets>
    </telerik:RadStyleSheetManager>
    <style type="text/css">
    
    .qsfFooter
        {
            font-size: 11px;
            color:CaptionText;
           
            line-height: 15px;
            text-decoration: none; /*去掉下划线*/
        }

    </style>
</head>
<body class="BODY">
    <form id="form1" runat="server">
	<%--皮肤控制--%>
    <qsf:SkinsControl runat="server" ID="skinControl" />

    <div>
        <div>
            <div id="Div1" class="center">
                <!--LOG In -->
                <div class="bottom">
                    <div class="box_outer">
                        <!--横向菜单-->
                        <div style="background:url(Image/topbg.jpg) repeat-x scroll left top #000;">
                            <!--LOGO-->
                            <div >
                                <br />
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Image/w_logo.png" />
                                <br />
                            </div>
                        </div>
                        <!--横向菜单-->
                        <div class="test">
                            &nbsp;
                            <br /><br /><br />
                            &nbsp;&nbsp;<span class="loginTitle"><asp:Label ID="lblLoginTitle" runat="server" 
                                                            Text="<%$ Resources: en_US, Logon_lblLoginTitle_Text %>"></asp:Label></span>
                            <br /><br /><br />
                            &nbsp;
                            <!--中间白色DIV-->
                            <table width="750" style="height: 200px; margin:auto" >
                                <tr>
                                    <td align="center" valign="middle">
                                        <div style="margin: 0 4px; background: #FFFFFF; height: 1px; overflow: hidden;">
                                        </div>
                                        <div style="margin: 0 2px; border: 1px solid #FFFFFF; border-width: 0 2px; background: #FFFFFF;
                                            height: 1px; overflow: hidden;">
                                        </div>
                                        <div style="margin: 0 1px; border: 1px solid #FFFFFF; border-width: 0 1px; background: #FFFFFF;
                                            height: 1px; overflow: hidden;">
                                        </div>
                                        <div style="margin: 0 1px; border: 1px solid #FFFFFF; border-width: 0 1px; background: #FFFFFF;
                                            height: 1px; overflow: hidden;">
                                        </div>
                                        <div style="background: #FFFFFF; border: 1px solid #FFFFFF; border-width: 0 1px;">
                                            <div style="background: #FFF; margin: 0 3px; font-size: 11px; font-family: Verdana;
                                                color: #333; padding: 5px 10px; overflow: hidden;">
                                                <div id="loginMain">
                                                    <div id="LoginErrorMessage">
                                                        <asp:Label ID="lblLoginMessage" runat="server" Text="lblLoginMessage" 
                                                            Visible="False"></asp:Label>
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                                        ValidationGroup="vcGroup" />
                                                    </div>
                                                    <ol class="tFormFieldsList">
					                                    <li class="name">
                                                            <asp:Label ID="lblUserName" runat="server" Text="<%$ Resources: en_US, Logon_lblUserName_Text %>" CssClass="LoginLabel"></asp:Label>
						                                    <span class="txtWrapper">
                                                                <telerik:RadTextBox ID="rtxtUserName" runat="server" 
                                                                CssClass="LoginTextBox" Width="335px" Font-Size="14pt" MaxLength="255">
                                                                </telerik:RadTextBox>
                                                                <asp:RequiredFieldValidator ID="rfvName" Text="!" runat="server" ControlToValidate="rtxtUserName"
                                                                    ErrorMessage="<%$ Resources: en_US, Logon_UserIDRequest_Text %>" 
                                                                    ValidationGroup="vcGroup"></asp:RequiredFieldValidator>
						                                    </span>
                                                        </li>
					                                    <li class="pwd">
                                                            <asp:Label ID="lblPassword" runat="server" Text="<%$ Resources: en_US, Logon_lblPassword_Text %>" CssClass="LoginLabel"></asp:Label>
						                                    <span class="txtWrapper">
                                                                <telerik:RadTextBox ID="rtxtPassword" runat="server" 
                                                                    CssClass="LoginTextBox" Width="335px" TextMode="Password" Font-Size="14pt" MaxLength="255">
                                                                </telerik:RadTextBox>
                                                                <asp:RequiredFieldValidator ID="rfvPwd" Text="!" runat="server" ControlToValidate="rtxtPassword"
                                                                    ErrorMessage="<%$ Resources: en_US, Logon_PasswordResquest_Text %>" 
                                                                    ValidationGroup="vcGroup"></asp:RequiredFieldValidator>
						                                    </span>
                                                        </li>
					                                    <li class="tFormCheckbox">
					                                    </li>
				                                    </ol>
                                                    <div class="loginBtn">
                                                        <telerik:RadButton ID="rbtnLogin" runat="server" Text="Login" CssClass="loginPosition" 
                                                            Height="29px" Width="90px" onclick="rbtnLogin_Click" ValidationGroup="vcGroup">
                                                            <Image ImageUrl="Image/login-up.png" HoveredImageUrl="Image/login-down.png" />
                                                        </telerik:RadButton>

                                                        <p class="loghelp">
	                                                        <strong>Help: </strong>
                                                            <asp:LinkButton ID="lbtnHelp" runat="server" 
                                                                onclientclick="lbtnHelpClick();return false;">I forgot my password</asp:LinkButton>
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="margin: 0 1px; border: 1px solid #FFFFFF; border-width: 0 1px; background: #FFFFFF;
                                            height: 1px; overflow: hidden;">
                                        </div>
                                        <div style="margin: 0 1px; border: 1px solid #FFFFFF; border-width: 0 2px; background: #FFFFFF;
                                            height: 1px; overflow: hidden;">
                                        </div>
                                        <div style="margin: 0 2px; border: 1px solid #FFFFFF; border-width: 0 2px; background: #FFFFFF;
                                            height: 1px; overflow: hidden;">
                                        </div>
                                        <div style="margin: 0 4px; background: #FFFFFF; height: 1px; overflow: hidden;">
                                        </div>
                                    </td>
                                </tr>
                            </table>

                            <!--中间白色DIV-->
                        </div>
                        <%--版权信息--%>
                        <div style="background: Black" class="style4">
                            <table width="100%" style="background-color:#CCCCCC;height:80px">
                                <tr>
                                    <td align="left" class="style5">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Image/logo_bottom.png" Width="150px"
                                            Height="35px" />
                                        <strong><span class="qsfFooter">
                                            <br />
                                            Able software.Inc,30 Corporate Park, Suite 104 Irvine, CA 92606 
                                            <br />
                                            Copyright @ 2011 Able Software, Inc. . All Rights Reserved
                                            </span></strong>
                                    </td>
                                    <td align="right" valign="top">
                                        <strong><a href="http://www.able-soft.com/" class="qsfFooter">www.able-soft.com</a>
                                        </strong>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
	</form>
</body>
</html>
