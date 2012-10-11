<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginForm.aspx.cs" Inherits="WEB.LoginForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!--[if lt IE 7]>  
<script src="styles/unitpngfix.js" type="text/javascript"></script>
<![endif]-->  
    <style type="text/css">
       body { margin:0;
       padding:0;
	   font-family:Arial, Helvetica, sans-serif;
      background:url(styles/images/loginbg.jpg) repeat-x;
	  }
#head {
       height:125px;
	   border-top:5px #2b508d solid;
	   }
#logo { 
        width:331px;
		margin:0 auto;
		
		}

#box { width:492px;
       height:415px;
	   background:url(styles/images/formbg.png) no-repeat;
	   margin:0 auto;
	   color:#fff;
	   padding:0;
	   
}
#footer {height:35px;
         font-size:12px;
		 color:#2e448d;
		 text-align:center;
		 font-weight:bold;
         background:url(styles/images/line.jpg) no-repeat bottom;
		 margin-top:70px;
		 }
#form_title {
             text-align:center;
			 font-size:26px;
			 font-weight:bold;
			 padding-top:90px;
			
			 
			 }
.beta { text-align:center;
        padding-top:10px;
		font-size:15px;}
.login { margin:20px 0px 0px 40px;}
.style {
         background:url(styles/images/button.png) no-repeat;
		 width:103px;
		 height:43px;
		 border:none;
		 margin-right:123px;
}
.labeltext { padding-right:5px; }

.input {line-height:27px;}
    
    </style>
</head>
<body>
  <div id="head">
      <div id="logo" dir="ltr">
       <img src="Image/logo.png" width="331" height="80" style="margin-top:35px;" alt="logo" />
      </div>
</div>

<div id="box">
     <form id="form1" runat="server" class="form">
     <div id="form_title">Schedule Billing Management</div>
     <div class="beta"><asp:Label ID="labVersion" runat="server" Text="Version" 
             onload="labVersion_Load"></asp:Label> </div>
     <asp:Login ID="lgiSystem" runat="server" DestinationPageUrl="~/Default.aspx" 
        DisplayRememberMe="False" FailureText="Your login attempt was not successful." 
        LoginButtonText="" PasswordLabelText="Password:" 
        PasswordRequiredErrorMessage="You must enter the password." TitleText="" 
        UserNameLabelText="User Name:" 
        UserNameRequiredErrorMessage="You must enter the username." 
        onauthenticate="lgiSystem_Authenticate" Height="178px"  Width="360px" 
         CssClass="login">
        <LabelStyle Width="125px" Font-Size="12px" CssClass="labeltext" />
        <LoginButtonStyle CssClass="style" BorderStyle="None" Font-Strikeout="False" />
        <TextBoxStyle Width="210px" BorderColor="#0D2C52" BorderStyle="Solid" 
             Height="27px" BackColor="#1E4F8A" BorderWidth="1px" ForeColor="White" 
             CssClass="input" />
    </asp:Login>
    </form>   
</div>

<div id="footer">
  Copyright @ 2011 Able Software, Inc. . All Rights Reserved 
</div>
                          
  
</body>
</html>
