<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<telerik:RadStyleSheetManager id="RadStyleSheetManager1" runat="server" />
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
	<div>
        <div style="margin:auto;width:400px; height:300px;">
            <fieldset style="width:400px; height:300px; -moz-border-radius:8px">
                <legend title="Customer Information">Customer Information</legend>
                <div style="margin-top:10px; margin-left:5px; padding-left:20px;">
                    <div style="width:120px; height:20px; float:left; vertical-align:middle">Select Company</div>
                    <div style="margin-left:125px">
                        <telerik:RadComboBox ID="cboCompany" runat="server">
                        </telerik:RadComboBox>
                    </div>
                </div>
                <div style="margin-top:10px; margin-left:5px; padding-left:20px;">
                    <div style="width:120px; height:20px; float:left; vertical-align:middle">Account Number</div>
                    <div style="margin-left:125px">
                        <telerik:RadTextBox ID="txtAccountNum" Runat="server">
                        </telerik:RadTextBox>
                    </div>
                </div>
                <div style="padding-left:150px; padding-top:10px; padding-bottom:10px">
                    <telerik:RadButton ID="btnSearch" runat="server" Text="Search" 
                        onclick="btnSearch_Click">
                    </telerik:RadButton>
                </div>
                <div style="margin-top:10px; margin-left:5px; padding-left:20px;">
                    <div style="width:120px; height:20px; float:left; vertical-align:middle">Customer Name</div>
                    <div style="margin-left:125px">
                        <telerik:RadTextBox ID="txtCustomerName" Runat="server">
                        </telerik:RadTextBox>
                    </div>
                </div>
                <div style="margin-top:10px; margin-left:5px; padding-left:20px;">
                    <div style="width:120px; height:20px; float:left; vertical-align:middle">Current Due</div>
                    <div style="margin-left:125px">
                        <telerik:RadTextBox ID="txtCurrentDue" Runat="server">
                        </telerik:RadTextBox>
                    </div>
                </div>
                <div style="margin-top:10px; margin-left:5px; padding-left:20px;">
                    <div style="width:120px; height:20px; float:left; vertical-align:middle">Total Due</div>
                    <div style="margin-left:125px">
                        <telerik:RadTextBox ID="txtTotalDue" Runat="server">
                        </telerik:RadTextBox>
                    </div>
                </div>
                <div style="margin-top:10px; margin-left:5px; padding-left:20px;">
                    <div style="width:120px; height:20px; float:left; vertical-align:middle">Last Name</div>
                    <div style="margin-left:125px">
                        <telerik:RadTextBox ID="txtLastName" Runat="server">
                        </telerik:RadTextBox>
                    </div>
                </div>
                <div style="margin-top:10px; margin-left:5px; padding-left:20px;">
                    <div style="width:120px; height:20px; float:left; vertical-align:middle">Zip</div>
                    <div style="margin-left:125px">
                        <telerik:RadTextBox ID="txtZip" Runat="server">
                        </telerik:RadTextBox>
                    </div>
                </div>
                <div style="padding-left:150px; padding-top:10px; padding-bottom:10px">
                    <telerik:RadButton ID="rbtnSave" runat="server" Text="Save" 
                        onclick="rbtnSave_Click" >
                    </telerik:RadButton>
                </div>
            </fieldset>
        </div>    
	</div>
	</form>
</body>
</html>
