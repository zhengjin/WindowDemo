using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.SysManage;
using HHSoft.FieldProtect.DataEntity.Chart;
using System.Data.OracleClient;
using HHSoft.FieldProtect.DataAccess;
using System.Data;
using HHSoft.FieldProtect.DataEntity;
using Microsoft.Office.Interop.Owc11;
using System.Web.UI;
using System.IO;

namespace HHSoft.FieldProtect.Business.Chart
{
    public class Chart
    {
        public Chart() { }        

        public string PieChart(Pie pie, Page page)
        {
            //创建X坐标的值
            string[] str = pie.DataNames;

            //创建Y坐标的值，表示销售额
            double[] count = pie.DataValues;
            string strDataName = "";
            string strData = "";

            //创建图表空间
            ChartSpace mychartSpace = new ChartSpace();
            mychartSpace.Border.Color = pie.ChartBorderColor;

            //在图表空间内添加一个图表对象
            ChChart mychart = mychartSpace.Charts.Add(0);
            
            //设置每块饼的数据
            for (int i = 0; i < count.Length; i++)
            {
                strDataName += str[i].ToString() + "\t";
                strData += count[i].ToString() + "\t";
            }
            strDataName = strDataName.Substring(0, strDataName.Length - 1);
            strData = strData.Substring(0, strData.Length - 1);

            //设置图表类型，本例使用饼
            switch (pie.PicChartType)
            {
                case PieType.Pie:
                    mychart.Type = ChartChartTypeEnum.chChartTypePie;
                    break;
                case PieType.Pie3D:
                    mychart.Type = ChartChartTypeEnum.chChartTypePie3D;
                    break;
                case PieType.Exploded:
                    mychart.Type = ChartChartTypeEnum.chChartTypePieExploded;
                    break;
                case PieType.Exploded3D:
                    mychart.Type = ChartChartTypeEnum.chChartTypePieExploded3D;
                    break;
                default:
                    mychart.Type = ChartChartTypeEnum.chChartTypePie;
                    break;
            }


            //设置图表的一些属性
            //是否需要图例
            mychart.HasLegend = true;
            //是否需要主题
            mychart.HasTitle = true;


            //主题内容
            mychart.Title.Caption = pie.ChartTitle;
            mychart.Title.Font.Size = pie.ChartTitleSize;
            mychart.Title.Font.Bold = pie.ChartTitleBold;
            mychart.Title.Font.Color = pie.ChartTitleColor;
            switch (pie.LegendPosition)
            {
                case LegendPosition.Top:
                    mychart.Legend.Position = ChartLegendPositionEnum.chLegendPositionTop;
                    break;
                case LegendPosition.Bottom:
                    mychart.Legend.Position = ChartLegendPositionEnum.chLegendPositionBottom;
                    break;
                case LegendPosition.Left:
                    mychart.Legend.Position = ChartLegendPositionEnum.chLegendPositionLeft;
                    break;
                case LegendPosition.Right:
                    mychart.Legend.Position = ChartLegendPositionEnum.chLegendPositionRight;
                    break;
                default:
                    mychart.Legend.Position = ChartLegendPositionEnum.chLegendPositionRight;
                    break;
            }



            mychart.Legend.Interior.Color = pie.LegendBgColor;
            mychart.Legend.Font.Bold = pie.LegenFontBold;
            mychart.Legend.Font.Size = pie.LegendFontSize;
            mychart.Legend.Border.Color = pie.LegendBorderColor;



            //添加图表块
            mychart.SeriesCollection.Add(0);
            //设置图表块的属性
            //分类属性
            mychart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimCategories,
            (int)ChartSpecialDataSourcesEnum.chDataLiteral, strDataName);
            //mychart.SeriesCollection[0].Interior.Color = "#C1DBEE";
            //mychart.SeriesCollection[1].Interior.Color = "#D1A00B";
            

            //值属性
            mychart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimValues,
            (int)ChartSpecialDataSourcesEnum.chDataLiteral, strData);
            for (int j = 0; j < mychart.SeriesCollection[0].Points.Count; j++)
            {
                mychart.SeriesCollection[0].Points[j].Border.Color = pie.SeriesCollectionBorderColor;
                if (pie.DataColor != null)
                {
                    mychart.SeriesCollection[0].Points[j].Interior.Color = pie.DataColor[j].ToString(); 
                }
            }         

            //显示百分比
            ChDataLabels mytb = mychart.SeriesCollection[0].DataLabelsCollection.Add();
            mytb.HasPercentage = pie.HasPercentage;
            mytb.Font.Color = pie.DataFontColor;
            mytb.Font.Size = pie.DataFontSize;
            mytb.HasValue = true;
            //生成图片
            //劉宏哲修改，先删除文件再创建文件。解决第一次生成图片以后，再次生成报错。时间：2010-04-21 9:44。
            string path = page.MapPath(".") + @"\" + pie.PicName + ".gif";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            mychartSpace.ExportPicture(path, "gif", pie.ChartWidth, pie.ChartHeight);

            //返回图片路径
            return pie.PicName + ".gif" + "?temp=" + System.DateTime.Now.Ticks.ToString() + "";
        }

        public string ColumnChart(Column column, Page page)
        {
            //创建图表空间
            ChartSpace mychartSpace = new ChartSpace();

            //在图表空间内添加一个图表对象
            ChChart mychart = mychartSpace.Charts.Add(0);
            mychartSpace.Border.Color = column.ChartBorderColor;
            //设置图表类型，本例使用柱形
            mychart.Type = ChartChartTypeEnum.chChartTypeColumnClustered;
            //设置图表的一些属性
            //是否需要图例
            mychart.HasLegend = true;
            //是否需要主题
            mychart.HasTitle = true;
            //主题内容
            mychart.Title.Caption = column.ChartTitle;
            mychart.Title.Font.Size = column.ChartTitleSize;
            mychart.Title.Font.Bold = column.ChartTitleBold;

            switch (column.LegendPosition)
            {
                case LegendPosition.Top:
                    mychart.Legend.Position = ChartLegendPositionEnum.chLegendPositionTop;
                    break;
                case LegendPosition.Bottom:
                    mychart.Legend.Position = ChartLegendPositionEnum.chLegendPositionBottom;
                    break;
                case LegendPosition.Left:
                    mychart.Legend.Position = ChartLegendPositionEnum.chLegendPositionLeft;
                    break;
                case LegendPosition.Right:
                    mychart.Legend.Position = ChartLegendPositionEnum.chLegendPositionRight;
                    break;
                default:
                    mychart.Legend.Position = ChartLegendPositionEnum.chLegendPositionRight;
                    break;
            }
            mychart.Legend.Interior.Color = column.LegendBgColor;
            mychart.Legend.Font.Size = column.LegendFontSize;
            mychart.Legend.Border.Color = column.LegendBorderColor;


            //设置x,y坐标

            mychart.Axes[1].HasTitle = column.ShowYAxes;
            mychart.Axes[1].Title.Caption = column.YAxesCaption;
            mychart.Axes[1].Title.Font.Size = 10;

            mychart.Axes[0].HasTitle = column.ShowXAxes;
            mychart.Axes[0].Title.Caption = column.XAxesCaption;
            mychart.Axes[0].Font.Name = "宋体";
            mychart.Axes[0].Font.Size = 10;

            string seriesName = "";
            string strValue = "";
            string category = "";

            for (int i = 0; i < column.SeriesNames.Length; i++)
            {
                seriesName = column.SeriesNames[i];
                strValue = "";
                category = "";
                for (int j = 0; j < column.Values[i].Length; j++)
                {
                    strValue += column.Values[i][j].ToString() + "\t";
                }

                for (int j = 0; j < column.Categorys.Length; j++)
                {
                    category += column.Categorys[j] + "\t";
                }
                mychart.SeriesCollection.Add(i);
                mychart.SeriesCollection[i].SetData(ChartDimensionsEnum.chDimSeriesNames, (int)ChartSpecialDataSourcesEnum.chDataLiteral, seriesName);
                mychart.SeriesCollection[i].SetData(ChartDimensionsEnum.chDimCategories, (int)ChartSpecialDataSourcesEnum.chDataLiteral, category);
                mychart.SeriesCollection[i].SetData(ChartDimensionsEnum.chDimValues, (int)ChartSpecialDataSourcesEnum.chDataLiteral, strValue);
                mychart.SeriesCollection[i].DataLabelsCollection.Add();
                mychart.SeriesCollection[i].DataLabelsCollection[0].HasValue = true;
                //mychart.SeriesCollection.Add(1);

                //if (column.DataColor != null)
                //{
                //    mychart.SeriesCollection[i].Points[0].Interior.Color = column.DataColor[i].ToString();
                //}
            }

            //生成图片
            //劉宏哲修改，先删除文件再创建文件。解决第一次生成图片以后，再次生成报错。时间：2010-04-21 9:44。
            
            string path = page.MapPath(".") + @"\" + column.PicName + ".gif";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            mychartSpace.ExportPicture(path, "gif", column.ChartWidth, column.ChartHeight);

            //返回图片路径
            return column.PicName + ".gif" + "?temp=" + System.DateTime.Now.Ticks.ToString() + "";

        }
    }
}
