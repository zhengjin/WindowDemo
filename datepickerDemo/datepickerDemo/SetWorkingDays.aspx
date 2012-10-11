<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetWorkingDays.aspx.cs" Inherits="datepickerDemo.SetWorkingDays" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>设置工作日</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
    <base target="_self" />
    <meta http-equiv="x-ua-compatible" content="IE=6"/>
    
    <link rel="stylesheet" href="/images/main.css" type="text/css"/>
    <link href="images/all.css" rel="stylesheet" type="text/css"/>
    <link href="images/skin.css" rel="stylesheet" type="text/css"/>
    <link href="images/fontSize12.css" rel="stylesheet" type="text/css"/>
    <link href="images/calendar.css" rel="stylesheet" type="text/css"/>
    
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/calendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0" style="margin-right: 0px">
            <tr>
                <td width="17" valign="top" background="/images/mail_leftbg.gif">
                    <img src="/images/left-top-right.gif" width="17" height="29" />
                </td>
                <td valign="top" background="/image/content-bg.gif">
                    <table width="100%" height="31" border="0" cellpadding="0" cellspacing="0" class="left_topbg"
                        id="table2">
                        <tr>
                            <td height="31" align="left">
                                <div class="titlebt">
                                    <asp:Literal ID="litTitle" runat="server" Text="修改工作时间"></asp:Literal>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="16" valign="top" background="/images/mail_rightbg.gif">
                    <img src="/images/nav-right-bg.gif" width="16" height="29" />
                </td>
            </tr>
            <tr>
                <td valign="middle" background="/images/mail_leftbg.gif">
                    &nbsp;
                </td>
                <td style="height: 100%" valign="top">
                    <table style="width: 100%;">
                        <tr>
                            <td valign="top">
                                <fieldset id="BeginSet" runat="server" style="height: 217px; width:171px; vertical-align: middle;">
                                <legend class="Legend_Standard">图例</legend>
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <img alt="工作日" src="/images/workDay.png" />
                                        </td>
                                        <td>
                                            工作日
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img alt="非工作日" src="/images/weakDay.png" />
                                        </td>
                                        <td>
                                            非工作日
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img alt="上班" src="/images/changeWorkDayIco.png" />
                                        </td>
                                        <td>
                                            上班
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img alt="放假" src="/images/changeWeekDayIco.png" />
                                        </td>
                                        <td>
                                            放假
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            </td>
                            <td>
                                <%-- 白色底纹 --%>
                                <div class="main">
                                    
                                    <%-- 日历外框 --%> 
                                    <div id="myrl" style="width:580px;margin-top:0px; margin-left:auto; margin-right:auto; height:600px; overflow:hidden;">
                                        <form name=CLD>
                                            <table class="biao" width="560px">
                                                <tbody>
                                                <TR>
                                                    <TD class="calTit" colSpan=7 style="height:30px;padding-top:3px;text-align:center;">

                                                        <a href="#" title="上一年" id="nianjian" class="ymNaviBtn lsArrow"></a>
                                                        <a href="#" title="上一月" id="yuejian" class="ymNaviBtn lArrow"></a>

                                                        <div style="width:250px; float:left; padding-left:110px;">
                                                                            <span id="dateSelectionRili" class="dateSelectionRili"
                                                                                  style="cursor:hand;color: white; border-bottom: 1px solid white;"
                                                                                  onclick="dateSelection.show()">
											                                <span id="nian" class="topDateFont"></span><span
                                                                                    class="topDateFont">年</span><span id="yue"
                                                                                                                      class="topDateFont"></span><span
                                                                                    class="topDateFont">月</span>
											                                <span class="dateSelectionBtn cal_next"
                                                                                  onclick="dateSelection.show()">▼</span></span> &nbsp;&nbsp;<font id=GZ
                                                                                                                                                   class="topDateFont"></font>
                                                        </div>

                                                        <!--新加导航功能-->
                                                        <div style="left: 152px; display: none;" id="dateSelectionDiv">
                                                            <div id="dateSelectionHeader"></div>
                                                            <div id="dateSelectionBody">
                                                                <div id="yearList">
                                                                    <div id="yearListPrev" onclick="dateSelection.prevYearPage()">&lt;</div>
                                                                    <div id="yearListContent"></div>
                                                                    <div id="yearListNext" onclick="dateSelection.nextYearPage()">&gt;</div>
                                                                </div>
                                                                <div id="dateSeparator"></div>
                                                                <div id="monthList">
                                                                    <div id="monthListContent"><span id="SM0" class="month"
                                                                                                     onclick="dateSelection.setMonth(0)">1</span><span
                                                                            id="SM1" class="month" onclick="dateSelection.setMonth(1)">2</span><span
                                                                            id="SM2" class="month" onclick="dateSelection.setMonth(2)">3</span><span
                                                                            id="SM3" class="month" onclick="dateSelection.setMonth(3)">4</span><span
                                                                            id="SM4" class="month" onclick="dateSelection.setMonth(4)">5</span><span
                                                                            id="SM5" class="month" onclick="dateSelection.setMonth(5)">6</span><span
                                                                            id="SM6" class="month" onclick="dateSelection.setMonth(6)">7</span><span
                                                                            id="SM7" class="month" onclick="dateSelection.setMonth(7)">8</span><span
                                                                            id="SM8" class="month" onclick="dateSelection.setMonth(8)">9</span><span
                                                                            id="SM9" class="month" onclick="dateSelection.setMonth(9)">10</span><span
                                                                            id="SM10" class="month" onclick="dateSelection.setMonth(10)">11</span><span
                                                                            id="SM11" class="month curr" onclick="dateSelection.setMonth(11)">12</span>
                                                                    </div>
                                                                    <div style="clear: both;"></div>
                                                                </div>
                                                                <div id="dateSelectionBtn">
                                                                    <div id="dateSelectionTodayBtn" onclick="dateSelection.goToday()">今天</div>
                                                                    <div id="dateSelectionOkBtn" onclick="dateSelection.go()">确定</div>
                                                                    <div id="dateSelectionCancelBtn" onclick="dateSelection.hide()">取消</div>
                                                                </div>
                                                            </div>
                                                            <div id="dateSelectionFooter"></div>
                                                        </div>
                                                        <a href="#" id="nianjia" title="下一年" class="ymNaviBtn rsArrow" style="float:right;"></a>
                                                        <a href="#" id="yuejia" title="下一月" class="ymNaviBtn rArrow" style="float:right;"></a>
                                                        <!--	<a id="jintian" href="#" title="今天" class="btn" style="float:right; margin-top:-2px; font-size:12px; text-align:center;">今天</a>-->

                                                    </TD>
                                                </TR>
                                                <TR class="calWeekTit" style="font-size:12px; height:20px;text-align:center;">
                                                    <TD width="80" class="red">星期日</TD>
                                                    <TD width="80">星期一</TD>
                                                    <TD width="80">星期二</TD>
                                                    <TD width="80">星期三</TD>
                                                    <TD width="80">星期四</TD>
                                                    <TD width="80">星期五</TD>
                                                    <TD width="80" class="red">星期六</TD>
                                                </TR>
                                                <script language="JavaScript">

                                                    var gNum;
                                                    for (var i = 0; i < 6; i++) {
                                                        document.write('<tr align=center height="44" id="tt">');
                                                        for (var j = 0; j < 7; j++) {
                                                            gNum = i * 7 + j;
                                                            document.write('<td  id="GD' + gNum + '"  onMouseOver="mOvr(this,' + gNum + ');"  onMouseOut="mOut(this);"><font  id="SD' + gNum + '" style="font-size:22px;"  face="Arial"');
                                                            if (j == 0) {
                                                                //周日
                                                                document.write('color=red');
                                                            };
                                                            if (j == 6) {
                                                                //周六
                                                                if (i % 2 == 1) document.write('color=red');
                                                                else document.write('color=red');
                                                            }
                                                            document.write('  TITLE="">  </font><br><font  id="LD' + gNum + '"  size=2  style="white-space:nowrap;overflow:hidden;cursor:default;">  </font></td>');
                                                        }
                                                        document.write('</tr>');
                                                    }
                                                    
                                                </script>
                                                </tbody>
                                            </table>
                                        </form>
                                    </div>
                                </div>

                                <div id="details" style="margin-top:-1px;"></div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    
                </td>
                <td background="/images/mail_rightbg.gif">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td valign="bottom" background="/images/mail_leftbg.gif">
                    <img src="/images/buttom_left2.gif" width="17" height="17" />
                </td>
                <td background="/images/buttom_bgs.gif">
                    <img src="/images/buttom_bgs.gif" width="17" height="17">
                </td>
                <td valign="bottom" background="/images/mail_rightbg.gif">
                    <img src="/images/buttom_right2.gif" width="16" height="17" />
                </td>
            </tr>
        </table>
    <asp:HiddenField ID="hidWeekDay" runat="server" Value="10-1,2,3,10,15;11-2,4,6;12-1,2,3"/>
    <asp:HiddenField ID="hidWorkDay" runat="server" Value="10-4,5,6;11-8,9,16;12-4,5,9"/>
    <asp:HiddenField ID="yearValue" runat="server" Value="2011"/>
    </form>
</body>
</html>
