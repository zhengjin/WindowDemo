<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="differentSkin.aspx.cs" Inherits="cssControl.differentSkin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Skinnable Form Controls</title>
    <meta http-equiv="Content-Type" content="text/html;charset=windows-1251" />
    <style type="text/css">
    body, a
    {
        font-family: Arial, Verdana, Sans-serif;
        color: #666;
        cursor: default;
    }
    
    h1, h2, h3
    {
        clear: both;
        padding: 0;
        margin: 0;
    }
    
    h2
    {
        border-bottom: solid 1px #404040;
    }
    
    h2, h3
    {
        margin: 12px 0;
    }
    
    p
    {
        font-size: 11px;
    }
    
    select
    {
        width: 180px;
        color: #404040;
        display: block;
    }
    
    div.controls
    {
        font-size: 11px;
        background: #ededed;
        padding: 8px;
        border: solid 1px #666;
    }
    
    div.controls ul
    {
        padding: 0;
        margin: 0;
        list-style: none;
        margin-bottom: 12px;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" 
        DecoratedControls="All" Skin="Telerik"/>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <h1>Skinnable Form Controls</h1>
        <div class="controls">
            <p>If you wish to skin the checkboxes or radiobuttons on your page just add a FormDecorator control on your form. You can choose whether to skin only the checkboxes, only the radiobuttons, or both by setting the DecoratedControls property. The FormDecorator offers four skins - Classic, Vista, Mac, and XP.</p>    
            <ul>
                <li><label for="SkinsList">Select a skin:</label>
                <asp:dropdownlist runat="server" id="SkinsList" autopostback="true" onselectedindexchanged="SkinsList_SelectedIndexChanged">
                    <asp:listitem value="Black">Black</asp:listitem>
                    <asp:listitem value="Classic">Classic</asp:listitem>
                    <asp:listitem value="Default">Default</asp:listitem>
                    <asp:listitem value="Hay">Hay</asp:listitem>
                    <asp:listitem value="Mac">Mac</asp:listitem>
                    <asp:listitem value="Outlook">Outlook</asp:listitem>
                    <asp:listitem value="Telerik">Telerik</asp:listitem>
                    <asp:listitem selected="True" value="Office2007">Office2007</asp:listitem>
                    <asp:listitem value="Web20">Web20</asp:listitem>
                    <asp:listitem value="WebBlue">WebBlue</asp:listitem>
                </asp:dropdownlist>
                </li>
            </ul>
            <ul>
                <li>Choose which controls to decorate:
                    <asp:radiobuttonlist id="DecoratedControlsList" runat="server" autopostback="true" onselectedindexchanged="DecoratedControlsList_SelectedIndexChanged">
                        <asp:listitem selected="True" value="7">All</asp:listitem>
                        <asp:listitem value="1">CheckBoxes</asp:listitem>
                        <asp:listitem value="2">RadioButtons</asp:listitem>
                        <asp:listitem value="4">Buttons</asp:listitem>
                        <asp:listitem value="0">None</asp:listitem>
                    </asp:radiobuttonlist>
                </li>
            </ul>
        </div>
        <h2>Skinned Checkboxes</h2>
        <h3>Single Checkboxes</h3>
        <asp:checkbox id="Checkbox1" runat="server" text="Checkbox 1" />
        <asp:checkbox id="Checkbox2" runat="server" text="Checkbox 2" />
        <asp:checkbox id="Checkbox3" runat="server" text="Checkbox 2" />
        <h3>Checkbox List</h3>
        <asp:checkboxlist id="ChList1" runat="server">
            <asp:listitem selected="True" text="Checkbox 1 (Checked)"></asp:listitem>
            <asp:listitem enabled="false" text="Checkbox 2 (Disabled)"></asp:listitem>
            <asp:listitem text="Checkbox 3"></asp:listitem>
        </asp:checkboxlist>
        <h2>Skinned Radiobuttons</h2>
        <h3>Single Radiobuttons</h3>
        <asp:radiobutton id="RadioButton1" runat="server" checked="true" groupname="Group"
            text="Radiobutton 1 (Checked)" />
        <asp:radiobutton id="RadioButton2" runat="server" groupname="Group" text="Radiobutton 2" />
        <asp:radiobutton id="RadioButton3" runat="server" groupname="Group" text="Radiobutton 3" />
        <h3>Radiobutton List</h3>
        <asp:radiobuttonlist id="RadioButtonList1" runat="server">
            <asp:listitem selected="True" text="Radiobutton 1"></asp:listitem>
            <asp:listitem enabled="false" text="Radiobutton 2 (Disabled)"></asp:listitem>
            <asp:listitem text="Radiobutton 3"></asp:listitem>
        </asp:radiobuttonlist>
        <h2>Skinned Buttons</h2>
        <input type="button" id="Baton1" value="Click here" onclick="alert(this.value);" />
        <input type="button" id="Button3" value="Disabled button" disabled="disabled" />
        <telerik:RadButton ID="RadButton1" runat="server" Text="RadButton">
        </telerik:RadButton>
        <asp:Button ID="Button1" runat=server Text="ASP:Button" />
        
        
      
    
        <p>Copyright 2002-2008 &copy;&nbsp;<a href="http://www.telerik.com/" rel="external" title="Telerik">Telerik</a>. All rights reserved.</p>
    </form>
</body>
</html>
