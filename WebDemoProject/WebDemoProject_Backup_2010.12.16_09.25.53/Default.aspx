<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<telerik:RadStyleSheetManager id="RadStyleSheetManager1" runat="server" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style5
        {
            width: 16px;
        }
        .style6
        {
            width: 112px;
        }
        .style11
        {
            width: 193px;
        }
        .style14
        {
            width: 115px;
        }
        .style20
        {
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
	<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
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
	<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
	</telerik:RadAjaxManager>
 
        <telerik:RadSplitter ID="MainSplitter" runat="server" Height="223px" 
            Width="790px">
            <telerik:RadPane ID="RadPane2" Runat="server" Height="134px" Width="764px">
                <div>
                    <table class="style1">
                        <tr>
                            <td align="right" class="style11">
                                &nbsp;</td>
                            <td class="style5">
                                &nbsp;</td>
                            <td class="style20">
                                <asp:HiddenField ID="hfGaugeID" runat="server" />
                            </td>
                            <td class="style14">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right" class="style11">
                                Gauge Code</td>
                            <td class="style5">
                                &nbsp;</td>
                            <td class="style20" colspan="3">
                                <telerik:RadTextBox ID="txtGaugeCode" Runat="server">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="txtGaugeCode" Display="Dynamic" 
                                    ErrorMessage="The textbox can not be empty!" Font-Bold="True" 
                                    Font-Names="Arial Black" Font-Size="Smaller" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="style11">
                                Size</td>
                            <td class="style5">
                                &nbsp;</td>
                            <td class="style20">
                                <telerik:RadComboBox ID="cboSize" Runat="server" width="126px">
                                </telerik:RadComboBox>
                            </td>
                            <td class="style14">
                                Model</td>
                            <td>
                                <telerik:RadComboBox ID="cboModel" Runat="server">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="style11">
                                Last Calibration Date</td>
                            <td class="style5">
                                &nbsp;</td>
                            <td class="style20">
                                <telerik:RadDatePicker ID="dtpLastCalibrationDate" Runat="server" width="153px">
                                </telerik:RadDatePicker>
                            </td>
                            <td class="style14">
                                Manufacturer</td>
                            <td>
                                <telerik:RadComboBox ID="cboManufacturer" Runat="server">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style11">
                                &nbsp;</td>
                            <td class="style5">
                                &nbsp;</td>
                            <td class="style20">
                                <table class="style1">
                                    <tr>
                                        <td class="style6">
                                            <telerik:RadButton ID="RadButton3" runat="server" 
                                                onclick="RadButton3_Click" Text="Save" Width="60px">
                                            </telerik:RadButton>
                                        </td>
                                        <td>
                                            <telerik:RadButton ID="RadButton4" runat="server" 
                                                onclick="RadButton4_Click" Text="Cancel" Width="60px">
                                            </telerik:RadButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="style14">
                                <telerik:RadButton ID="btnNewGauge" runat="server" onclick="btnNewGauge_Click" 
                                    Text="New Gauge">
                                </telerik:RadButton>
                            </td>
                            <td>
                                <telerik:RadButton ID="RadButton5" runat="server" 
                                    onclick="RadButton5_Click1" Text="Search">
                                </telerik:RadButton>
                            </td>
                        </tr>
                        <tr>
                            <td class="style11">
                                &nbsp;</td>
                            <td class="style5">
                                &nbsp;</td>
                            <td class="style20">
                                &nbsp;</td>
                            <td class="style14">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </div>
            </telerik:RadPane>
        </telerik:RadSplitter>

	</form>
</body>
</html>
