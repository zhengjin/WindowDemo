<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BackupSettingForm.aspx.cs" Inherits="BackupSettingForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<telerik:RadStyleSheetManager id="RadStyleSheetManager" runat="server" />
    <link href="~/Styles/main.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
         body 
        {
          font-size:12px;
          font-family:Arial, Helvetica, sans-serif;}
        .style1
        {
            width: 170px;
            text-align:right;  vertical-align:top;
            padding-top:8px;
        }
        .backup_setting {width:520px;
                         margin:0 auto;
                         border:1px solid #267bc6;
        }
        .spanRed
        {
            color: red;
        }
        .spanRed_port {color:Red;
                       padding-top:-5px;
                       padding-left:3px;}
         .message { width:520px;
                 height:35px;
                border:1px solid #d0d049;
                margin:0 auto;
                background-color:#fefad5;
                 
                }
          .error { width:520px;
                 height:35px;
                border:1px solid #ee5147;
                margin:0 auto;
                background-color:#fba09a;
                 
                }
            .right { width:520px;
                 height:35px;
                border:1px solid #9dbf3d;
                margin:0 auto;
                background-color:#dff2a9;
                 
                }
        .e_text {width:50px;
                 padding-left:10px;
               
                 }
           .m_text {width:50px;
                 padding-left:10px;
                 }
            .r_text {width:50px;
                 padding-left:10px;
                 }
    </style>
</head>
<body>
<div class="wrapper">
    <div class="content">
    <div class="header"><div class="logo"><img src="../Image/logo.png" /></div></div>
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
    <telerik:RadScriptBlock ID="RadScriptBlock" runat="server">
        <script type="text/javascript">
             //<![CDATA[
            function ConfirmBox(sender, args) {
                var txtServerText = $find("<%= txtServer.ClientID %>");
                var txtDatabaseText = $find("<%= txtDatabase.ClientID %>");
                var txtBackupFolderText = $find("<%= txtBackupFolder.ClientID %>");
                var rtxtServerNameText = $find("<%= rtxtServerName.ClientID %>");
                var rtxtFromText = $find("<%= rtxtFrom.ClientID %>");
                var rntxtPortText = $find("<%= rntxtPort.ClientID %>");
                var rtxtNameText = $find("<%= rtxtName.ClientID %>");
                var rtxtPwdText = $find("<%= rtxtPwd.ClientID %>");
                var tempBoolean;

                //alert(txtServerText.get_value());
                //alert(rtxtPwdText.get_value());

                if (txtServerText.get_value() != "" && txtDatabaseText.get_value() != "" && txtBackupFolderText.get_value() != "" && rtxtServerNameText.get_value() != ""
                                    && rtxtFromText.get_value() != "" && rntxtPortText.get_value() != "" && rtxtNameText.get_value() != "" && rtxtPwdText.get_value() != "")
                {
                    tempBoolean = confirm("Do you want to save changes?");
                    args.IsValid = tempBoolean;
                }
            }

             //]]>
        </script>
    </telerik:RadScriptBlock>
	<telerik:RadAjaxManager ID="RadAjaxManager" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnOK">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlMessage" />
                    <telerik:AjaxUpdatedControl ControlID="pnlRight" />
                    <telerik:AjaxUpdatedControl ControlID="pnlError" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
	</telerik:RadAjaxManager>
    <asp:Panel ID="pnlMessage" runat="server">
        <table class="message">
            <tr>
               <td class="m_text" valign="top"><img src="../Image/arrow.png" /></td>
               <td><asp:Literal ID="lnkMessage" runat="server"></asp:Literal></td>
            </tr>
        </table>
       
    </asp:Panel>
    <asp:Panel ID="pnlRight" runat="server">
        <table class="right">
             <tr>
                 <td class="r_text" valign="top"><img src="../Image/right.png" /></td>
                 <td><asp:Literal ID="lnkRight" runat="server"></asp:Literal></td>
             </tr>
        </table>
       
    </asp:Panel>
    <asp:Panel ID="pnlError" runat="server">
        <table class="error">
            <tr>
                <td class="e_text" valign="top"><img src="../Image/error.png" /></td>
                <td><asp:Literal ID="lnkError" runat="server"></asp:Literal></td>
            </tr>
        </table>
       
    </asp:Panel><br/>
     <div class="backup_title"><span class="t_text">Settings</span></div>
	<div class="backup_setting">
        <table style="width: 100%; margin:20px 0px 20px 0px;" cellpadding="5" cellspacing="0">
                <tr>
                    <td class="style1">
                      <asp:Label ID="lblServer" runat="server" 
                            Text="<%$ Resources:WebResource, BackupSettingForm_lblServer_text %>"></asp:Label>
                       
                    </td>
                    <td colspan="2">
                        <telerik:RadTextBox ID="txtServer" Runat="server" Width="220px" 
                            BorderColor="#267BC6" BorderStyle="Solid" BorderWidth="1px" MaxLength="50">
                        </telerik:RadTextBox>
                        <span class="spanRed">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                            runat="server" ErrorMessage="<%$ Resources:WebResource, BackupSettingForm_rfvServer_ErrorMessage %>" 
                            ControlToValidate="txtServer" CssClass="must_text"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblDatabase" runat="server" Text="<%$ Resources: WebResource, BackupSettingForm_lblDatabase_text %>"></asp:Label>
                    </td>
                    <td colspan="2">
                        <telerik:RadTextBox ID="txtDatabase" Runat="server" Width="220px" 
                            BorderColor="#267BC6" BorderStyle="Solid" BorderWidth="1px" MaxLength="50">
                        </telerik:RadTextBox>
                        <span class="spanRed">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                            runat="server" ErrorMessage="<%$ Resources:WebResource, BackupSettingForm_rfvServer_ErrorMessage %>" 
                            ControlToValidate="txtDatabase" CssClass="must_text"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style1" style=" border-bottom:1px solid #0065b3;" >
                        <asp:Label ID="lblBackupFolder" runat="server" Text="<%$ Resources: WebResource, BackupSettingForm_lblBackupFolder_text %>"></asp:Label>
                    </td>
                    <td colspan="2" style=" border-bottom:1px solid #0065b3;" >
                        <telerik:RadTextBox ID="txtBackupFolder" Runat="server" Width="220px" 
                            BorderColor="#267BC6" BorderStyle="Solid" BorderWidth="1px" 
                            MaxLength="100">
                        </telerik:RadTextBox>
                        <span class="spanRed">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                            runat="server" ErrorMessage="<%$ Resources:WebResource, BackupSettingForm_rfvServer_ErrorMessage %>" 
                            ControlToValidate="txtBackupFolder" CssClass="must_text"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style1" style="padding-top:27px;">
                        <asp:Label ID="lblServerName" runat="server" Text="<%$ Resources: WebResource, EmailSettingForm_lblServerName_Text %>"></asp:Label>
                    </td>
                    <td colspan="2" style="padding-top:25px;">
                    <telerik:RadTextBox ID="rtxtServerName" Runat="server" Width="220px" 
                        BorderColor="#267BC6" BorderStyle="Solid" BorderWidth="1px" MaxLength="50">
                    </telerik:RadTextBox>
                    <span class="spanRed">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                        runat="server" 
                        ErrorMessage="<%$ Resources: WebResource, EmailSettingForm_rfvRequiredFrom_ErrorMessage %>" 
                        ControlToValidate="rtxtServerName" CssClass="must_text"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblFrom" runat="server" Text="<%$ Resources: WebResource, EmailSettingForm_lblFrom_text %>"></asp:Label>
                    </td>
                    <td colspan="2">
                        <telerik:RadTextBox ID="rtxtFrom" runat="server" Width="220px" 
                            BorderColor="#267BC6" BorderStyle="Solid" BorderWidth="1px">
                        </telerik:RadTextBox>
                        <span class="spanRed">* 
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                            ErrorMessage="<%$ Resources: WebResource, EmailSettingForm_revRegularFrom_ErrorMessage %>" ControlToValidate="rtxtFrom" 
                            ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" 
                            CssClass="error_message" Font-Size="X-Small"></asp:RegularExpressionValidator></span>
                        <asp:RequiredFieldValidator ID="rfvRequiredFrom"
                            runat="server" 
                            ErrorMessage="<%$ Resources: WebResource, EmailSettingForm_rfvRequiredFrom_ErrorMessage %>" 
                            ControlToValidate="rtxtFrom" CssClass="must_text"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td class="style1">
                        <asp:Label ID="lblSSL" runat="server" Text="<%$ Resources: WebResource, EmailSettingForm_lblSSL_text %>"></asp:Label>
                    </td>
                    <td>
                    <div class="ssl">
                    <asp:CheckBox ID="ckbSSL" runat="server" AutoPostBack="True" 
                        oncheckedchanged="ckbSSL_CheckedChanged" /></div>
                    <div class="port">
                    <asp:Label ID="lblPorts" runat="server" 
                            Text="<%$ Resources:WebResource, EmailSettingForm_lblPorts_text %>" 
                            ></asp:Label>
                    <telerik:RadNumericTextBox ID="rntxtPort" Runat="server" Width="60px" 
                        BorderColor="#267BC6" BorderStyle="Solid" BorderWidth="1px" 
                         ><NumberFormat DecimalDigits="0" />
                     </telerik:RadNumericTextBox><span class="spanRed_port">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator"
                        runat="server" ErrorMessage="<%$ Resources: WebResource, EmailSettingForm_rfvRequiredFrom_ErrorMessage %>" 
                        ControlToValidate="rntxtPort" CssClass="must_text"></asp:RequiredFieldValidator></div>
                    </td>
                </tr>
                <tr>
                <td class="style1">
                    <asp:Label ID="lblSMTPName" runat="server" Text="<%$ Resources: WebResource, EmailSettingForm_lblSMTPName_text %>"></asp:Label>
                </td>
                <td colspan="2">
                    <telerik:RadTextBox ID="rtxtName" Runat="server" Width="220px" 
                        BorderColor="#267BC6" BorderStyle="Solid" BorderWidth="1px" MaxLength="50">
                    </telerik:RadTextBox>
             <span class="spanRed">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                        runat="server" 
                        ErrorMessage="<%$ Resources: WebResource, EmailSettingForm_rfvRequiredFrom_ErrorMessage %>" 
                        ControlToValidate="rtxtName" CssClass="must_text"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="lblSMTPPwd" runat="server" Text="<%$ Resources: WebResource, EmailSettingForm_lblSMTPPwd_text %>"></asp:Label>
                </td>
                <td colspan="2">
                    <telerik:RadTextBox ID="rtxtPwd" Runat="server" Width="220px" 
                        BorderColor="#267BC6" BorderStyle="Solid" BorderWidth="1px" MaxLength="50" 
                        TextMode="Password">
                    </telerik:RadTextBox>
                    <span class="spanRed">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                        runat="server" 
                        ErrorMessage="<%$ Resources: WebResource, EmailSettingForm_rfvRequiredFrom_ErrorMessage %>" 
                        ControlToValidate="rtxtPwd" CssClass="must_text"></asp:RequiredFieldValidator>
                </td>
            </tr>
                <tr>
                    <td colspan="3" class="td_backup">
                        <telerik:RadButton ID="btnOK" runat="server" 
                            Text="<%$ Resources: WebResource, BackupSettingForm_btnOK_text %>" 
                            onclick="btnOK_Click" ForeColor="White" 
                             Skin="Web20">
                        </telerik:RadButton>
                        <telerik:RadButton ID="btnCancel" runat="server" 
                            Text="<%$ Resources: WebResource, BackupSettingForm_btnCancel_text %>" 
                            onclick="btnCancel_Click" ForeColor="White" 
                           Skin="Web20" CausesValidation="False">
                        </telerik:RadButton>
                        <asp:CustomValidator ID="CustomValidator" runat="server" 
                            ClientValidationFunction="ConfirmBox"></asp:CustomValidator>
                    </td>
                </tr>
            </table>
	</div>
	</form>
     </div>
     <div class="footer">Copyright @ 2010 Able Software, Inc. . All Rights Reserved </div>
</div>
</body>
</html>
