<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChartCDProBilling.aspx.cs" Inherits="ChartDemo.ChartCDProBilling" %>
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
                <telerik:RadComboBox ID="rcomCompany" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="rcomCompany_SelectedIndexChanged">
                    <Items>
                        <telerik:RadComboBoxItem Text="N/A" Value="N/A"/>
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td>
                Cycle
            </td>
            <td>
                <telerik:RadComboBox ID="rcomCycle" runat="server" Enabled="False">
                    <Items>
                        <telerik:RadComboBoxItem Text="N/A" Value="N/A"/>
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td>
                Account Nmuber
            </td>
            <td>
                <telerik:RadTextBox ID="rtxtAccNum" runat="server">
                </telerik:RadTextBox>
            </td>
        </tr>
    </table>
    <div class="charCorner">
        <table>
            <tr>
                <td style="font-size: 15px">
                    Billing Summary
                    <div class="lineThrough">
                    &nbsp;
                    </div>
                    <telerik:RadChart ID="RChartCDProBilling" runat="server" DefaultType="Bar" Height="500px"
                        Width="860px" AutoLayout="true" onprerender="RChartCDProBilling_PreRender">
                                    
                        <Legend>
                            <Appearance Position-AlignedPosition="right">
                            </Appearance>
                        </Legend>
                        <PlotArea>
                            <XAxis DataLabelsColumn="month">
                                <Appearance>
                                    <TextAppearance TextProperties-Font="Arial, 8.25pt, style=Bold" />
                                    <LabelAppearance RotationAngle="30" >
                                    </LabelAppearance>
                                </Appearance>
                            </XAxis>
                        </PlotArea>
                        <ChartTitle Visible="False">
                            <Appearance Position-AlignedPosition="None" Visible="False">
                            </Appearance>
                            <TextBlock Text="Transaction Summary">
                            </TextBlock>
                        </ChartTitle>
                    </telerik:RadChart>
                </td>
            </tr>
        </table>
    </div>
    <div style="padding-top:5px;">
        <telerik:RadButton ID="rbtnApply" runat="server" Text="Apply" 
            onclick="rbtnApply_Click">
        </telerik:RadButton>
        <telerik:RadButton ID="rbtn" runat="server" Text="Cancel" onclick="rbtn_Click">
        </telerik:RadButton>
    </div>
</asp:Content>
