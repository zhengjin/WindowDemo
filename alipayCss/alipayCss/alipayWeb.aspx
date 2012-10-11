<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="alipayWeb.aspx.cs" Inherits="alipayWeb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<telerik:RadStyleSheetManager id="RadStyleSheetManager1" runat="server" />
    <script type="text/javascript" charset="utf-8" src="/Scripts/arale.js"></script>
    <script type="text/javascript" charset="utf-8" src="/Scripts/pa.js"></script>
    <link type="text/css" media="screen" charset="utf-8" rel="stylesheet" href="/Styles/pa.css"/>
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
	
	<div id="container">
        <div id="nav">
	        <div class="nav-container">
		        <ul>
			        <li class="nav-master current">
				        <a class="nav-master-a" href="alipayWeb.aspx" seed="nav0"><strong>我的支付宝</strong></a>
				        <ul class="nav-sub">
					        <li><a href="alipayWeb.aspx" class="current" seed="nav1"><span>首页</span></a></li>
					        <li><a href="alipayWeb.aspx" seed="nav2"><span>我的账户</span></a></li>
					        <li><a href="alipayWeb.aspx" seed="nav3"><span>付款方式</span></a></li>
					        <li><a href="alipayWeb.aspx" seed="nav4"><span>手机服务</span></a></li>
					        <li><a href="alipayWeb.aspx" seed="nav5"><span>联系人</span></a></li>
					        <li><a href="alipayWeb.aspx" seed="nav6"><span>购物券</span></a></li>				
					
			          </ul>
			        </li>
			        <li class="nav-master">
				        <a class="nav-master-a" href="alipayWeb.aspx" seed="nav10"><strong>消费记录</strong></a>
				        <ul class="nav-sub">
					        <li><a href="alipayWeb.aspx" class="" seed="nav11"><span>消费记录</span></a></li>
					        <li><a href="alipayWeb.aspx" seed="nav12"><span>充提记录</span></a></li>
				        </ul>
			        </li>
			        <li class="nav-master">
				        <a class="nav-master-a" href="alipayWeb.aspx" seed="nav18"><strong>转账</strong></a>
				        <ul class="nav-sub nav-sub-transfer">
					        <li><a href="alipayWeb.aspx"  seed="nav19"><span>转账首页</span></a></li>
					        <li><a href="alipayWeb.aspx"  seed="nav20"><span>我要付款</span></a></li>					
			          </ul>
			        </li>			
		        </ul>
	        </div>
        </div>
    </div>
	</form>
</body>
</html>
