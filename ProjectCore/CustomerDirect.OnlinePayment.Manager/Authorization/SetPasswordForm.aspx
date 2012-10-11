<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetPasswordForm.aspx.cs" Inherits="SetPasswordForm" %>

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
    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)

            return oWindow;
        }

        function CancelEdit() {
            GetRadWindow().close();
        }

        function ShowTooltip() {
            window.setTimeout(function () {
                var tooltip = $find("RadToolTip");
                //API: show the tooltip
                tooltip.show();
            }, 5);
        }

        function CheckIfShow(sender, args) {
            var summaryElem = document.getElementById("ValidationSummary");

            //check if summary is visible
            if (summaryElem.style.display == "none") {
                //API: if there are no errors, do not show the tooltip
                args.set_cancel(true);
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <%--皮肤控制--%>
    <qsf:SkinsControl runat="server" ID="skinControl" />

	<div class="otherWindow">
            <fieldset id="WindowsFieldSet" style="width: 91%; height: 120px; margin:0 auto;">
                <legend class="classInfo" >
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources: en_US, Home_WindowsFieldSet_Title %>"></asp:Literal>
                </legend>
                <div style="height: 20px">&nbsp;</div>
                <table class="homeWindow">
                    <tr>
                        <td class="lblText">
                            <asp:Label ID="lblNewPasswork" runat="server" Text="<%$ Resources: en_US, Home_lblNewPasswork_Text %>"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="rtxtNewPasswork" runat="server" Width="177px" 
                                TextMode="Password" MaxLength="255">
		                    </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="rfvNewPasswork" Text="!" runat="server" ControlToValidate="rtxtNewPasswork"
                                ErrorMessage="<%$ Resources: en_US, ChangePassword_rfvNewPasswork_Text %>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lblText">
                            <asp:Label ID="lblConfirmPassword" runat="server" Text="<%$ Resources: en_US, Home_lblConfirmPassword_Text %>"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="rtxtConfirmPassword" runat="server" Width="177px" 
                                TextMode="Password" MaxLength="255">
		                    </telerik:RadTextBox>
                          <%--  <asp:RequiredFieldValidator ID="rfvConfirmPassword" Text="!" runat="server" ControlToValidate="rtxtConfirmPassword"
                                ErrorMessage="<%$ Resources: en_US, ChangePassword_rfvConfirmPassword_Text %>"></asp:RequiredFieldValidator>--%>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Text="!" runat="server" ControlToValidate="rtxtConfirmPassword"
                                ErrorMessage="<%$ Resources: en_US, UserManagerForm_rtxtConfirmPassword %>"></asp:RequiredFieldValidator>
                               <asp:CompareValidator ID="CompareValidator1" runat="server"  Text="!"
                                    ControlToCompare="rtxtNewPasswork" ControlToValidate="rtxtConfirmPassword" 
                                    ErrorMessage="<%$ Resources: en_US, UserManagerForm_ThePasswordsDoNotMatch_Text%>" ></asp:CompareValidator>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <div class="homeWindowsbtn">
			    <telerik:RadButton ID="rbtnSaveUser" runat="server" Text="<%$ Resources: en_US, Home_rbtnSave_Text %>" 
				    Width="80px" onclick="rbtnSaveUser_Click" onclientclicking="ShowTooltip">
			    </telerik:RadButton>
			    <telerik:RadButton ID="rbtnUserCancel" runat="server" Text="<%$ Resources: en_US, Home_rbtnCancel_Text %>" 
				    Width="80px" onclientclicking="CancelEdit">
			    </telerik:RadButton>
		    </div>
            <telerik:RadToolTip runat="server" ID="RadToolTip" Position="BottomCenter" HideEvent="LeaveToolTip"
                ShowEvent="FromCode" Width="300px" RelativeTo="Element" EnableShadow="true" OnClientBeforeShow="CheckIfShow"
                TargetControlID="rbtnSaveUser">
                <asp:ValidationSummary ID="ValidationSummary" runat="server" />
            </telerik:RadToolTip>
            <telerik:RadToolTip ID="ServerRadToolTip" runat="server" Position="BottomCenter" HideEvent="LeaveToolTip"
                ShowEvent="FromCode" Width="300px" Height="50px" RelativeTo="Element" EnableShadow="true"
                TargetControlID="rbtnSaveUser">
                <asp:Label ID="lblUserErrorMessage" runat="server">&nbsp;</asp:Label>
            </telerik:RadToolTip>
        </div>
	</form>
</body>
</html>
