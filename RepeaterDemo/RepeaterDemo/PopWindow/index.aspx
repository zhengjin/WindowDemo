<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="RepeaterDemo.PopWindow.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../JavaScript/jquery-1.4.2.min.js"></script>
    
    <!--jbox-->
    <link id="skin" rel="stylesheet" href="../CSS/Green/jbox.css" />
    <script type="text/javascript" src="../JavaScript/jquery.jBox-2.3.min.js"></script>
    <script type="text/javascript" src="../JavaScript/jquery.jBox-zh-CN.js"></script>
    
    <style type="text/css">
        /*页面样式*/
        body {margin:0;padding:0;font:12px Verdana, Geneva, sans-serif;background:#66C6F6;}
        ul{ margin:0; padding:0; list-style:none;}
        a{color:#ff6600; text-decoration:none;}
        a:hover{color:#ff9900; text-decoration:underline;}
        .grid{ width:auto;width:920px !important;max-width:920px;min-width:800px; background:#FFF; margin:10px auto; border:10px solid #60B7DE;}
        .logo{ width:460px; font:bold 40px "Comic Sans MS", cursive;  margin:10px 5px 5px 25px;}
        .logo span{ font-size:13px; color:#999; margin:0 5px;}
        .demo{ padding:5px;}
        fieldset{ margin:20px; padding:16px;}
        .content{ margin:10px 0 0 0;}
        .content li { margin:5px;}
        .btn{ border:0; background:#4D77A7; color:#FFF; font-size:12px; padding:6px 10px;  margin:5px 0;}
        .copy{ height:30px; background:#E6EFF8; text-align:center; line-height:30px;}
        .t{padding:20px;}
        .update{padding:0 20px 20px 20px;}
        .t li,.update li{ margin:10px 0; line-height:20px;}
        .update{color:#069;padding-bottom:0;}
        .sel-skin{position:absolute;top:300px;right:10px;width:120px; height:80px; padding:10px; background-color:#fff;color:#fff;}
        /*jox应用测试样式*/
        div.msg-div{ padding: 10px; }
        div.msg-div p{ padding: 0px; margin:5px 0 0 0; }
        div.msg-div .field{ padding: 0px; }
        div.msg-div .field textarea{ width: 90%; height: 50px; resize:none; font-size:12px; }
        .errorBlock{ background-color: #FFC6A5; border: solid 1px #ff0000; color: #ff0000; margin: 10px 10px 0 10px; padding:7px; font-weight: bold; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
    </div>
    </form>
</body>
</html>
