<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sample.aspx.cs" Inherits="Serialization_Sample" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>序列化(DataContractSerializer, XmlSerializer, DataContractJsonSerializer, SoapFormatter, BinaryFormatter)</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>
            <asp:Label ID="lblDataContractSerializer" runat="server" />
            <br />
            <asp:TextBox ID="txtDataContractSerializer" runat="server" TextMode="MultiLine" Width="100%"
                Height="120px" />
        </p>
        <p>
            <asp:Label ID="lblXmlSerializer" runat="server" />
            <br />
            <asp:TextBox ID="txtXmlSerializer" runat="server" TextMode="MultiLine" Width="100%"
                Height="120px" />
        </p>
        <p>
            <asp:Label ID="lblSoapFormatter" runat="server" />
            <br />
            <asp:TextBox ID="txtSoapFormatter" runat="server" TextMode="MultiLine" Width="100%"
                Height="120px" />
        </p>
        <p>
            <asp:Label ID="lblBinaryFormatter" runat="server" />
            <br />
            <asp:TextBox ID="txtBinaryFormatter" runat="server" TextMode="MultiLine" Width="100%"
                Height="120px" />
        </p>
        <p>
            <asp:Label ID="lblDataContractJsonSerializer" runat="server" />
            <br />
            <asp:TextBox ID="txtDataContractJsonSerializer" runat="server" TextMode="MultiLine"
                Width="100%" Height="120px" />
        </p>
    </div>
    </form>
</body>
</html>
