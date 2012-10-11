<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HighchartDemo.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
	<script type="text/javascript">
		$(function () {
		    var chart;
		    $(document).ready(function () {
		        chart = new Highcharts.Chart({
		            chart: {
		                renderTo: 'container',
		                plotBackgroundColor: null,
		                plotBorderWidth: null,
		                plotShadow: false
		            },
		            title: {
		                text: 'Browser market shares at a specific website, 2010'
		            },
		            tooltip: {
		                pointFormat: '{series.name}: <b>{point.percentage}%</b>',
		                percentageDecimals: 1
		            },
		            plotOptions: {
		                pie: {
		                    allowPointSelect: true,
		                    cursor: 'pointer',
		                    dataLabels: {
		                        enabled: true,
		                        color: '#000000',
		                        connectorColor: '#000000',
		                        formatter: function () {
		                            return '<b>' + this.point.name + '</b>: ' + this.percentage + ' %';
		                        }
		                    }
		                }
		            },
		            series: [{
		                type: 'pie',
		                name: 'Browser share',
		                data: [
                ['Firefox', 45.0],
                ['IE', 26.8],
                {
                    name: 'Chrome',
                    y: 12.8,
                    sliced: true,
                    selected: true
                },
                ['Safari', 8.5],
                ['Opera', 6.2],
                ['Others', 0.7]
            ]
		            }]
		        });
		    });

		});
	</script>
</head>
<body>
    <form id="form1" runat="server">
    <script src="../../Scripts/highcharts.js"></script>
    <script src="../../Scripts/modules/exporting.js"></script>

    <div id="container" style="min-width: 400px; height: 400px; margin: 0 auto"></div>
    </form>
</body>
</html>
