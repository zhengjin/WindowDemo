<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BillingForm.aspx.cs" Inherits="BillingForm" %>

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
            width:125px;
            text-align:right;
            padding-right:7px;
        }
      
        .message { width:400px;
                 height:35px;
                 border:1px solid #d0d049;
                margin:0 auto;
                background-color:#fefad5;
                 
                }
          .error { width:400px;
                 height:35px;
                 border:1px solid #ee5147;
                margin:0 auto;
                background-color:#fba09a;
                 
                }
            .right { width:400px;
                 height:35px;
                 border:1px solid #9dbf3d;
                margin:0 auto;
                background-color:#dff2a9;
                 
                }
        .e_text { width:50px;
                 padding-left:10px;
               
                 }
           .m_text { width:50px;
                 padding-left:10px;
                 }
            .r_text { width:50px;
                padding-left:10px;
                 }
        
      
    </style>
</head>
<body>
<div class="wrapper">
    <div class="content">
    <div class="header"><div class="logo"><img src="../Image/logo.png" /></div></div>
    <form id="form" class="form_bl" runat="server">
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
            function JudgeCy(sender, args) {
                var comboCompany = $find("<%= rctxtCompany.ClientID %>");
                var comboStartCycle = $find("<%= rctxtStartingCycle.ClientID %>");
                var comboEndCycle = $find("<%= rctxtEndingCycle.ClientID %>");

                var companyValue = comboCompany.get_selectedItem();
                var startCycleValue = comboStartCycle.get_selectedItem();
                var endCycleValue = comboEndCycle.get_selectedItem();

                args.IsValid = true;
                
                if(companyValue.get_text() == "N/A"||startCycleValue.get_text() == "N/A"||endCycleValue.get_text() == "N/A")
                {
                    alert("The cycles you selected are invalid.");
                    args.IsValid = false;
                }
            }
        </script>
    </telerik:RadScriptBlock>
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
        <div class="form_top">select a company to bill </div> 
        <table style="width: 100%;" height="70">
          
            <tr>
                <td class="style1">
                    <asp:Label ID="lblCompany" runat="server" Text="<%$ Resources: WebResource, BillingForm_lblCompany_text %>"></asp:Label>
                </td>
                <td>
                    <telerik:RadComboBox ID="rctxtCompany" Runat="server" 
                        onselectedindexchanged="rctxtCompany_SelectedIndexChanged" 
                        AutoPostBack="True">
                        <Items>
                            <telerik:RadComboBoxItem Text="N/A" Value="N/A"></telerik:RadComboBoxItem>
                        </Items>
                    </telerik:RadComboBox>
               </td>
            </tr>
        </table> 
        <table style="width: 100%;" cellspacing="0" cellpadding="0">
              <tr>
                  <td colspan="2" class="cycles_title">select billing cycles</td>
              </tr>
        </table>
        <table style="width: 100%;">
            <tr>
                <td class="style1" height="35">
                    <asp:Label ID="lblStartingCycle" runat="server" Text="<%$ Resources: WebResource, BillingForm_lblStartingCycle_text %>"></asp:Label>
                </td>
                <td >
                    <telerik:RadComboBox ID="rctxtStartingCycle" Runat="server" AutoPostBack="True" 
                        onselectedindexchanged="rctxtStartingCycle_SelectedIndexChanged">
                        <Items>
                            <telerik:RadComboBoxItem Text="N/A" Value="N/A"></telerik:RadComboBoxItem>
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="style1" height="35">
                    <asp:Label ID="lblEndingCycle" runat="server" Text="<%$ Resources: WebResource, BillingForm_lblEndingCycle_text %>"></asp:Label>
                </td>
                <td>
                    <telerik:RadComboBox ID="rctxtEndingCycle" Runat="server">
                        <Items>
                            <telerik:RadComboBoxItem Text="N/A" Value="N/A"></telerik:RadComboBoxItem>
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="style1" height="35">
                   
                </td>
                <td>
                    <asp:CheckBox ID="chkAllCycles" runat="server" CssClass="chk" AutoPostBack="True" oncheckedchanged="chkAllCycles_CheckedChanged"/><asp:Label ID="Label2" runat="server" Text="<%$ Resources: WebResource, BillingForm_lblAllCycles_text %>"></asp:Label>
                </td>
               
            </tr>
             <tr>
                <td class="style1" height="35">
                   
                </td>
                <td>
                    <asp:CheckBox ID="chkCopy" runat="server" CssClass="chk"/> <asp:Label ID="lblcopy" runat="server" Text="<%$ Resources: WebResource, BillingForm_lblcopy_text %>"></asp:Label>
                </td>
            </tr>
             <tr>
                <td class="style1" height="35">
                   
                </td>
                <td>
                    <asp:CheckBox ID="chkCalc" runat="server" CssClass="chk"/> <asp:Label ID="lblCalc" runat="server" Text="<%$ Resources: WebResource, BillingForm_lblCalc_text %>"></asp:Label>
                </td>
            </tr>
        </table> 
        <table style="width: 100%;">
            <tr>
                <td class="style1" height="50">
                    <asp:Label ID="lblStatusCode" runat="server" Text="<%$ Resources: WebResource, BillingForm_lblStatusCode_text %>"></asp:Label>
                </td>
                <td>
                    <telerik:RadComboBox ID="rctxtStatusCode" Runat="server">
                        <Items>
                            <telerik:RadComboBoxItem Text="N/A" Value="N/A"></telerik:RadComboBoxItem>
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="td_form">
                   
                    <telerik:RadButton ID="rbtnOK" runat="server"
                        Text="<%$ Resources:WebResource, BillingForm_rbtnOK_text %>" 
                         ForeColor="White"
                        Skin="Web20" BorderStyle="None" 
                        EnableAjaxSkinRendering="False" EnableTheming="False" 
                        SplitButtonPosition="Right" onclick="rbtnOK_Click" 
                        
                        >
                    </telerik:RadButton>
                    <telerik:RadButton ID="rbtnCancel" runat="server" 
                        Text="<%$ Resources: WebResource, BillingForm_rbtnCancel_text %>" 
                         ForeColor="White" 
                       
                        DisabledButtonCssClass="button" 
                        onclick="rbtnCancel_Click" CausesValidation="False" Skin="Web20" >
                    </telerik:RadButton>
                </td>
            </tr>
           
        </table> 
	    <div><img src="../Image/form_bottom.jpg" /></div>

    <telerik:RadAjaxManager ID="RadAjaxManager" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rctxtCompany">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rctxtStartingCycle" />
                    <telerik:AjaxUpdatedControl ControlID="rctxtEndingCycle" />
                    <telerik:AjaxUpdatedControl ControlID="chkAllCycles" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rctxtStartingCycle">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rctxtEndingCycle" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkAllCycles">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rctxtStartingCycle" />
                    <telerik:AjaxUpdatedControl ControlID="rctxtEndingCycle" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbtnOK">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlMessage" />
                    <telerik:AjaxUpdatedControl ControlID="pnlRight" />
                    <telerik:AjaxUpdatedControl ControlID="pnlError" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
	</telerik:RadAjaxManager>
	</form>
   
    </div>
     <div class="footer">Copyright @ 2010 Able Software, Inc. . All Rights Reserved </div>
</div>
</body>
</html>
