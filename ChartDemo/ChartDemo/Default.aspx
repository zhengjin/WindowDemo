<%@ Page Title="Chart" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="ChartDemo._Default" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Charting" tagprefix="telerik" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
    <!-- content start -->
     <table style="width: 100%;">
        <tr>
            <td>
                <table>
                    <tr>
                        <td style="font-size: 15px">
                            <div class="linetxt">Transaction Summary</div>
                            <div class="linebtn">
                                <telerik:RadButton ID="RadButton1" runat="server" Text="Filter">
                                </telerik:RadButton>
                            </div>
                            <div>
                                <telerik:RadChart ID="RChartCDProTransaction" runat="server" DefaultType="Bar" Height="140px"
                                    Width="415px" AutoLayout="true">
                                    
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
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table>
                    <tr>
                        <td style="font-size: 15px">
                            <div class="linetxt">Usage Summary</div>
                            <div class="linebtn">
                                <telerik:RadButton ID="RadButton2" runat="server" Text="Filter" 
                                    onclick="RadButton2_Click">
                                </telerik:RadButton>
                            </div>
                            <div>
                                <telerik:RadChart ID="RChartCDProUsage" runat="server" DefaultType="Pie" Height="140px"
                                    Width="415px" AutoLayout="true" onclick="RChartCDProUsage_Click" 
                                    onbeforelayout="RChartCDProUsage_BeforeLayout" 
                                    onitemdatabound="RChartCDProUsage_ItemDataBound">
                                    
                                    <Legend>
                                        <Appearance Position-AlignedPosition="Right">
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
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td style="font-size: 15px">
                            <div class="linetxt">Customer Count Summary</div>
                            <div class="linebtn">
                                <telerik:RadButton ID="RadButton3" runat="server" Text="Filter">
                                </telerik:RadButton>
                            </div>
                            <div>
                                <telerik:RadChart ID="RChartCDProCustomer" runat="server" DefaultType="Line" Width="415px" 
                                    AutoLayout="true" Height="140px">
                                    
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
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table>
                    <tr>
                        <td style="font-size: 15px">
                            <div class="linetxt">Billing Summary</div>
                            <div class="linebtn">
                                <telerik:RadButton ID="RadButton4" runat="server" Text="Filter" 
                                    onclick="RadButton4_Click">
                                </telerik:RadButton>
                            </div>
                            <div>
                                <telerik:RadChart ID="RChartCDProBilling" runat="server" DefaultType="Bar" Height="140px"
                                    Width="415px" AutoLayout="true" onclick="RChartCDProBilling_Click">
                                    
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
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            
        </tr>
        <tr>
            <td>
                <div class="linetxt">Top 10 Customer</div>
                
                <div style="width:415px">
                    <div class="linebtn">
                    <telerik:RadButton ID="RadButton6" runat="server" Text="Filter">
                    </telerik:RadButton>
                </div>
                <telerik:RadGrid ID="rGridCustomer" runat="server" AllowSorting="True" 
                    Width="410px" CellSpacing="0" GridLines="None" AutoGenerateColumns="False">
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" />
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="103px" />
                    </ClientSettings>
                    <MasterTableView>
                    <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                    <HeaderStyle Width="20px"></HeaderStyle>
                    </RowIndicatorColumn>

                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                    <HeaderStyle Width="20px"></HeaderStyle>
                    </ExpandCollapseColumn>

                        <Columns>
                            <telerik:GridBoundColumn DataField="CompName" HeaderText="Company Name" UniqueName="CompName"
                                SortExpression="CompName" >
                                <HeaderStyle Width="75px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Cycle" HeaderText="Cycle" UniqueName="Cycle"
                                SortExpression="Cycle" >
                                <HeaderStyle Width="45px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Name" HeaderText="Customer Name" UniqueName="Name"
                                SortExpression="Name">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AcctNum" HeaderText="Account Number" UniqueName="AcctNum"
                                SortExpression="AcctNum">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Usage" HeaderText="Usage" UniqueName="Usage"
                                SortExpression="Usage">
                            </telerik:GridBoundColumn>
                        </Columns>

                    <EditFormSettings>
                    <EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
                    </EditFormSettings>
                    </MasterTableView>

                    <FilterMenu EnableImageSprites="False"></FilterMenu>

                    <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default"></HeaderContextMenu>
                </telerik:RadGrid>
                </div>
            </td>
            <td>
                <table>
                    <tr>
                        <td style="font-size: 15px">
                            <div class="linetxt">Payment Summary</div>
                            <div class="linebtn">
                                <telerik:RadButton ID="RadButton5" runat="server" Text="Filter">
                                </telerik:RadButton>
                            </div>
                            <div>
                                <telerik:RadChart ID="RChartCDProPayment" runat="server" DefaultType="bar" 
                                    Width="415px" Height="140px" 
                                    onitemdatabound="RChartCDProPayment_ItemDataBound" AutoLayout="true">
                                    <Legend>
                                        <Appearance Position-AlignedPosition="right">
                                        </Appearance>
                                    </Legend>
                                    <ChartTitle TextBlock-Text="Temperature" Visible="False">
                                    </ChartTitle>
                                    <Series>
                                        <telerik:ChartSeries DataYColumn="Payment amount" Name="Amount" Type="bar">
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
                                        <YAxis AxisMode="Normal">
                                            <Appearance>
                                                <TextAppearance TextProperties-Font="Arial, 8.25pt, style=Bold" />
                                            </Appearance>
                                        </YAxis>
                                    </PlotArea>
                                </telerik:RadChart>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MBDemoConnectionString %>" 
        
        SelectCommand="SELECT top(6) * FROM [viewUsageSummaryAll] where usagesummary&lt;&gt;0 ORDER BY [BillDt] ">
    </asp:SqlDataSource>
    <!-- content end -->
    <telerik:RadToolTipManager ID="RadToolTipManager1" runat="server" Skin="Telerik"
        Width="170px" Animation="Slide" Position="TopCenter" EnableShadow="true" ToolTipZoneID="RChartCDPro" AutoTooltipify="true">
    </telerik:RadToolTipManager>
</asp:Content>
