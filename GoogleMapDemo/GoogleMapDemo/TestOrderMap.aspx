<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestOrderMap.aspx.cs" Inherits="WEB.Business_Logic.TestOrderMap" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/GoogleMap.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>
</head>
<body onload="OnLoad()">
    <div id="placelist"> 
          <div id="search"> 
            <div id="searchform"></div>
          </div> 
          
          <div id="results"> 
            <div id="searchwell"></div> 
          </div> 
          <div id="map"></div> 
          <div id="selected"></div> 
    </div>
    <form runat="server">
        <asp:HiddenField ID="HiddenField_testOrderAdress" runat="server" />
        <asp:HiddenField ID="HiddenField_Longitude" runat="server" />
        <asp:HiddenField ID="HiddenField_Latitude" runat="server" />
    </form>
    <script src="../Scripts/GoogleMap.js" type="text/javascript"></script>
</body>
</html>
