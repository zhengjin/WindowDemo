<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaskDetailForm.aspx.cs" Inherits="TaskDetailForm" %>

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
            width: 220px;
            text-align:right;
        }
        .style2
        {
            width: 190px;
        }
         .style3
        {
            width: 220px;
            text-align:right;
              vertical-align:top;
            padding-top:8px;
        }
        .spanRed
        {
            color: red;
        }
        .message { width:658px;
                 
                 border:1px solid #d0d049;
                margin:0 auto;
                background-color:#fefad5;
                 
                }
          .error { width:658px;
                 height:35px;
                 border:1px solid #ee5147;
                margin:0 auto;
                background-color:#fba09a;
                 
                }
            .right { width:658px;
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
            var rdStartTime;
            var rdEndTime;

            function onLoadRadTimePickerStart(sender, args) {
                rdStartTime = sender;
            }

            function onLoadRadTimePickerEnd(sender, args) {
                rdEndTime = sender;
            }

            function JudgeRunTimeHour(sender, args) {
                var Date1 = new Date(rdStartTime.get_selectedDate());
                var Date2 = new Date(rdEndTime.get_selectedDate());
                var tempBoolean;
                var timeNow;

                args.IsValid = true;
                //alert((Date2 - Date1));
                if ((Date2 - Date1) < 3600000 && (Date2 - Date1) > 0|| (Date2 - Date1) == 0){
                    tempBoolean = confirm("Warning: the scheduled task period you selected is less than 1 hour, are you sure to keep current settings?");
                    args.IsValid = tempBoolean;
                }

                if ((Date2 - Date1) < 0) {
                    alert("The end date should be greater than start date!");
                    args.IsValid = false;
                }

                if ((Date2 - Date1) > 86400000) {
                    tempBoolean = confirm("Warning: the scheduled task period you selected is greater than 24 hours, are you sure to keep current settings?");
                    args.IsValid = tempBoolean;
                }

                timeNow = new Date();
                if ((Date1 - timeNow) < 0) {
                    alert("The Start Time cannot be earier than current time.");
                    args.IsValid = false;
                }
            }

            function getParam(param) {
                var r = new RegExp("\\?KeyGuid" + "=(.*?)(?:&.*)?$");
                var m = window.location.toString().match(r);
                //alert(param);
                //alert(m);
                return m ? m[1] : ""; //如果需要处理中文，可以用返回decodeURLComponent(m[1])
            }
            function SetEndTime() {

                var pageUrl = "";
                pageUrl = window.location;
                pageUrl = pageUrl.toString();
                var urlValue = getParam(pageUrl);
                if (urlValue == "") {
                    var startTimePicker = $find("<%= rdStartTime.ClientID %>");
                    var endTimePicker = $find("<%= rdEndTime.ClientID %>");

                    var startTimeView = startTimePicker.get_timeView();
                    var startTime = startTimeView.getTime();
                    var endTimeView = endTimePicker.get_timeView();
                    var endTime = endTimeView.getTime();

                    if (startTime == null) {
                        startTime = new Date();
                    }

                    endTime = startTime;
                    var dayTemp = endTime.getDate();
                    var hourTemp = endTime.getHours() + 3;
                    var minutesTemp = endTime.getMinutes();
                    var dateTemp = new Date();
                    dateTemp.setDate(dayTemp);
                    dateTemp.setHours(hourTemp, minutesTemp, 0, 0)
                    endTimePicker.set_selectedDate(dateTemp);
                }
            }

             //]]>
        </script>
    </telerik:RadScriptBlock>
	<telerik:RadAjaxManager ID="RadAjaxManager" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rtxtTaskName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlMessage"/>
                    <telerik:AjaxUpdatedControl ControlID="pnlRight" />
                    <telerik:AjaxUpdatedControl ControlID="pnlError" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rdStartTime">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rdEndTime" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
	</telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
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
    <div class="title"><span class="t_text">Task details</span></div>
	<div class="box">
        <table style="width: 100%; margin:20px 0px 20px 3px;" cellpadding="5">
                <tr>
                    <td class="style3">
                        <asp:Label ID="lblTaskName" runat="server" Text="<%$ Resources: WebResource, TaskDetailForm_lblTaskName_text %>"></asp:Label>
                    </td>
                    <td colspan="2">
                        <telerik:RadTextBox ID="rtxtTaskName" Runat="server" AutoPostBack="True" 
                            ontextchanged="rtxtTaskName_TextChanged" MaxLength="50">
                        </telerik:RadTextBox>
                        <span class="spanRed">*</span>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblScheduleType" runat="server" Text="<%$ Resources: WebResource, TaskDetailForm_lblScheduleType_text %>"></asp:Label>
                    </td>
                    <td colspan="2">
                        <telerik:RadComboBox ID="rcbScheduleType" Runat="server" AutoPostBack="True" 
                            onselectedindexchanged="rcbScheduleType_SelectedIndexChanged">
                            <Items>
                                <%--<telerik:RadComboBoxItem Text="Daily" Value="Daily" Selected="true" />
                                <telerik:RadComboBoxItem Text="Weekly" Value="Weekly" />
                                <telerik:RadComboBoxItem Text="Monthly" Value="Monthly" />--%>
                                <telerik:RadComboBoxItem Text="Run Once" Value="Run Once" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblStartTime" runat="server" Text="<%$ Resources: WebResource, TaskDetailForm_lblStartTime_text %>"></asp:Label>
                    </td>
                    <td colspan="2">
                        <telerik:RadDateTimePicker ID="rdStartTime" Runat="server" Culture="en-US" 
                            AutoPostBack="True">
                            <DateInput ID="DateInput" runat="server">
                                <ClientEvents OnLoad="onLoadRadTimePickerStart" />
                            </DateInput>
                            <TimeView CellSpacing="-1"></TimeView>

                            <TimePopupButton ImageUrl="" HoverImageUrl=""></TimePopupButton>

                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

                            <DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy"></DateInput>

                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <ClientEvents OnDateSelected="SetEndTime" />
                        </telerik:RadDateTimePicker>
                    </td>
                </tr>
                <%--<tr>
                    <td class="style1">
                        <asp:Label ID="lblROOTD" runat="server" Text="<%$ Resources: WebResource, TaskDetailForm_lblROOTD_text %>"></asp:Label>
                    </td>
                    <td colspan="2">
                    <asp:CheckBoxList ID="ckxlROOTD" runat="server" RepeatColumns="2" Width="200px" 
                            BorderColor="#ABABAB" BorderStyle="Solid" BorderWidth="1px">
                        <asp:ListItem Text="Sunday" Value="Sunday"></asp:ListItem>
                        <asp:ListItem Text="Monday" Value="Monday"></asp:ListItem>
                        <asp:ListItem Text="Tuesday" Value="Tuesday"></asp:ListItem>
                        <asp:ListItem Text="Wednesday" Value="Wednesday"></asp:ListItem>
                        <asp:ListItem Text="Thursday" Value="Thursday"></asp:ListItem>
                        <asp:ListItem Text="Friday" Value="Friday"></asp:ListItem>
                        <asp:ListItem Text="Saturday" Value="Saturday"></asp:ListItem>
                    </asp:CheckBoxList>
                    </td>
                </tr>--%>
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblROBTT" runat="server" Text="<%$ Resources: WebResource, TaskDetailForm_lblROBTT_text %>"></asp:Label>
                    </td>
                    <td colspan="2">
                        <telerik:RadDateTimePicker ID="rdEndTime" Runat="server" Culture="en-US">
                            <DateInput ID="DateInput1" runat="server">
                                <ClientEvents OnLoad="onLoadRadTimePickerEnd" />
                            </DateInput>
                            <TimeView CellSpacing="-1"></TimeView>

                            <TimePopupButton ImageUrl="" HoverImageUrl=""></TimePopupButton>

                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

                            <DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy"></DateInput>

                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                        </telerik:RadDateTimePicker>
                        <asp:CustomValidator ID="CustomValidator" runat="server" 
                            ControlToValidate="rdEndTime" ClientValidationFunction="JudgeRunTimeHour"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        
                    </td>
                    <td >
                        <asp:CheckBox ID="chkBDBB" runat="server" Text="<%$ Resources: WebResource, TaskDetailForm_chkBDBB_text %>"/>
                    </td>
                    <td class="style2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblAction" runat="server" Text="<%$ Resources: WebResource, TaskDetailForm_lblAction_text %>"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="rcbAction" Runat="server">
                            <Items>
                                <telerik:RadComboBoxItem Text="Billing" Value="Billing" Selected="true" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td class="style2">
                        <telerik:RadButton ID="rbtnBillingSetting" runat="server" 
                            Text="<%$ Resources: WebResource, TaskDetailForm_rbtnBillingSetting_text %>" 
                            onclick="rbtnBillingSetting_Click" 
                             EnableAjaxSkinRendering="False"  ForeColor="White" 
                            ToggleType="CustomToggle" CausesValidation="False" Skin="Web20">
                        </telerik:RadButton>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblPostAction" runat="server" Text="<%$ Resources: WebResource, TaskDetailForm_lblPostAction_text %>"></asp:Label>
                    </td>
                    <td >
                        <telerik:RadComboBox ID="rcbPostAction" Runat="server">
                            <Items>
                                <telerik:RadComboBoxItem Text="Email" Value="Email" Selected="true" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td class="style2">
                        <telerik:RadButton ID="rbtnMailSetting" runat="server" 
                            Text="<%$ Resources: WebResource, TaskDetailForm_rbtnMailSetting_text  %>" 
                            onclick="rbtnMailSetting_Click"  EnableAjaxSkinRendering="False" 
                            ForeColor="White" 
                            ToggleType="CustomToggle" CausesValidation="False" Skin="Web20">
                        </telerik:RadButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" class="td_task">
                        <telerik:RadButton ID="btnOK" runat="server" 
                            Text="<%$ Resources: WebResource, TaskDetailForm_btnOK_text %>" 
                            onclick="btnOK_Click"
                            EnableAjaxSkinRendering="False" ForeColor="White" 
                            Skin="Web20">
                        </telerik:RadButton>
                        <telerik:RadButton ID="btnCancel" runat="server" 
                            Text="<%$ Resources: WebResource, TaskDetailForm_btnCancel_text %>" 
                            onclick="btnCancel_Click" 
                            EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" ForeColor="White" 
                            Skin="Web20" CausesValidation="False">
                        </telerik:RadButton>
                    </td>
                </tr>
            </table> 
	</div>
	</form>
     </div>
     <div class="footer">Copyright @ 2010 Able Software, Inc. . All Rights Reserved  Able Software, Inc. . All Rights Reserved </div>
</div>
</body>
</html>
