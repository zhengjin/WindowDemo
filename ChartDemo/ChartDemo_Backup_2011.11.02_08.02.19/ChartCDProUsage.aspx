<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChartCDProUsage.aspx.cs" Inherits="ChartDemo.ChartCDProUsage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:DropDownList AutoPostBack="true" ID="SubtypeDropdown" runat="server" OnSelectedIndexChanged="SubtypeDropdown_SelectedIndexChanged">
        <asp:ListItem Text="Normal Line" Value="Line" Selected="True" />
        <asp:ListItem Text="Bezier Line" Value="Bezier" />
        <asp:ListItem Text="Spline Line" Value="Spline" />
        <asp:ListItem Text="Stacked Line" Value="StackedLine" />
        <asp:ListItem Text="Stacked Spline Line" Value="StackedSpline" />
    </asp:DropDownList>
    <br />
    <br />
    <div class="charCorner">
        <table>
            <tr>
                <td style="font-size: 15px">
                    Year to Date Usage
                    <div class="lineThrough">
                    &nbsp;
                    </div>
                    <telerik:RadChart ID="RChartCDProUsage" runat="server" DefaultType="Line" 
                        Width="900px" AutoLayout="true">
                                    
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
</asp:Content>
