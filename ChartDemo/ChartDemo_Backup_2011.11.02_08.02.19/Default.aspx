<%@ Page Title="Chart" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="ChartDemo._Default" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Charting" tagprefix="telerik" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    
    <script type="text/javascript">
        $(".charCorner").corner();
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
    <!-- content start -->
    <table style="width: 100%;">
        <tr>
            <td>
                <div class="charCorner">
                    <table>
                        <tr>
                            <td style="font-size: 15px">
                                Transaction Summary
                                <div class="lineThrough">
                                &nbsp;
                                </div>
                                <telerik:RadChart ID="RChartCDProTransaction" runat="server" DefaultType="Line" Width="435px" AutoLayout="true">
                                    
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
            </td>
            <td>
                <div class="charCorner">
                    <table>
                        <tr>
                            <td style="font-size: 15px">
                                Year to Date Usage
                                <div class="lineThrough">
                                &nbsp;
                                </div>
                                <telerik:RadChart ID="RChartCDProUsage" runat="server" DefaultType="Line" 
                                    Width="435px" AutoLayout="true" onclick="RChartCDProUsage_Click">
                                    
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
            </td>
            
        </tr>
        <tr>
            <td>
                <div class="charCorner">
                    <table>
                        <tr>
                            <td style="font-size: 15px">
                                Customer Count Summary
                                <div class="lineThrough">
                                &nbsp;
                                </div>
                                <telerik:RadChart ID="RChartCDProCustomer" runat="server" DefaultType="Line" Width="435px" AutoLayout="true">
                                    
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
            </td>
            <td>
                <div class="charCorner">
                    <table>
                        <tr>
                            <td style="font-size: 15px">
                                Billing Summary
                                <div class="lineThrough">
                                &nbsp;
                                </div>
                                <telerik:RadChart ID="RChartCDProBilling" runat="server" DefaultType="Line" 
                                    Width="435px" AutoLayout="true" onclick="RChartCDProBilling_Click">
                                    
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
            </td>
            
        </tr>
        <tr>
            <td>
                <div class="charCorner">
                    <table>
                        <tr>
                            <td style="font-size: 15px">
                                Payment Summary
                                <div class="lineThrough">
                                &nbsp;
                                </div>
                                <telerik:RadChart ID="RChartCDProPayment" runat="server" DefaultType="Line" Width="435px" 
                                    onitemdatabound="RChartCDProPayment_ItemDataBound" AutoLayout="true">
                                    <Legend>
                                        <Appearance Position-AlignedPosition="right">
                                        </Appearance>
                                    </Legend>
                                    <ChartTitle TextBlock-Text="Temperature" Visible="False">
                                    </ChartTitle>
                                    <Series>
                                        <telerik:ChartSeries DataYColumn="Payment amount" Name="Payment amount" Type="Line">
                                            <Appearance>
                                                <TextAppearance TextProperties-Font="Arial, 8.25pt" />
                                                <PointMark Visible="True" Border-Width="2" Border-Color="DarkKhaki" Dimensions-AutoSize="false"
                                                    Dimensions-Height="12px" Dimensions-Width="12px">
                                                    <FillStyle MainColor="186, 207, 141" FillType="solid">
                                                    </FillStyle>
                                                </PointMark>
                                                <LineSeriesAppearance Width="5" />
                                            </Appearance>
                                        </telerik:ChartSeries>
                                    </Series>
                                    <PlotArea>
                                        <XAxis DataLabelsColumn="QuarterDate">
                                            <Appearance>
                                                <TextAppearance TextProperties-Font="Arial, 8.25pt, style=Bold" />
                                                <LabelAppearance RotationAngle="30" >
                                                </LabelAppearance>
                                            </Appearance>
                                        </XAxis>
                                        <YAxis AxisMode="Extended">
                                            <Appearance>
                                                <TextAppearance TextProperties-Font="Arial, 8.25pt, style=Bold" />
                                            </Appearance>
                                        </YAxis>
                                    </PlotArea>
                                </telerik:RadChart>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <!-- content end -->
    <telerik:RadToolTipManager ID="RadToolTipManager1" runat="server" Skin="Telerik"
        Width="170px" Animation="Slide" Position="TopCenter" EnableShadow="true" ToolTipZoneID="RChartCDPro" AutoTooltipify="true">
    </telerik:RadToolTipManager>
</asp:Content>
