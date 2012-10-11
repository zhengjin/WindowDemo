<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChartCDProUsage.aspx.cs" Inherits="ChartDemo.ChartCDProUsage" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Charting" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%;">
        <tr>
            <td>
                Company
            </td>
            <td>
                <telerik:RadComboBox ID="cboCompany" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="cboCompany_SelectedIndexChanged">
                </telerik:RadComboBox>
            </td>
            <td>
                Cycle
            </td>
            <td>
                <telerik:RadComboBox ID="cboCycle" runat="server">
                </telerik:RadComboBox>
            </td>
            <td>
                Account Nmuber
            </td>
            <td>
                <telerik:RadTextBox ID="txtAcctNum" runat="server">
                </telerik:RadTextBox>
            </td>
        </tr>
    </table>
    
    <table>
        <tr>
            <td style="font-size: 15px">
                Year to Date Usage
                <div class="lineThrough">
                &nbsp;
                </div>
                <telerik:RadChart ID="RadChart1" runat="server" DefaultType="Pie" 
                    Width="860px" AutoLayout="true">
                    <Series>
                        <telerik:ChartSeries Name="UsageSummary" Type="Pie">
                            <Appearance>
                                <FillStyle MainColor="213, 247, 255">
                                </FillStyle>
                            </Appearance>
                        </telerik:ChartSeries>
                    </Series>
                    <ChartTitle Visible="False">
                        <Appearance Visible="False">
                        </Appearance>
                    </ChartTitle>
                </telerik:RadChart>
            </td>
        </tr>
    </table>
    <div style="padding-top:5px;">
        <telerik:RadButton ID="rbtnApply" runat="server" Text="Apply" 
            onclick="btnApply_Click">
        </telerik:RadButton>
        <telerik:RadButton ID="rbtn" runat="server" Text="Cancel" onclick="rbtn_Click">
        </telerik:RadButton>
    </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MBDemoConnectionString %>" 
        SelectCommand="SELECT * FROM [viewUsageSummarybyCompany] WHERE ([CompanyID] = @CompanyID) ORDER BY [BillDt]">
        <SelectParameters>
            <asp:ControlParameter ControlID="cboCompany" Name="CompanyID" 
                PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MBDemoConnectionString %>" 
        
        SelectCommand="SELECT * FROM [viewUsageSummaryByCycle] WHERE (([CompanyID] = @CompanyID) AND ([CycleID] = @CycleID)) ORDER BY [BillDt]">
        <SelectParameters>
            <asp:ControlParameter ControlID="cboCompany" Name="CompanyID" 
                PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="cboCycle" Name="CycleID" 
                PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MBDemoConnectionString %>" 
        SelectCommand="SELECT * FROM [viewUsageSummaryByAcctNum] WHERE ([AcctNum] = @AcctNum) ORDER BY [BillDt]">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtAcctNum" Name="AcctNum" PropertyName="Text" 
                Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MBDemoConnectionString %>" 
        SelectCommand="SELECT * FROM [viewUsageSummaryAll] ORDER BY [BillDt]">
    </asp:SqlDataSource>
</asp:Content>
