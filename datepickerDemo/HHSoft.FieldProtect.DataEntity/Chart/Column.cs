using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.Framework.Control;

namespace HHSoft.FieldProtect.DataEntity.Chart
{
    [Serializable]
    public class Column
    {
        private string chartTitle;
		private int chartTitleSize;
		private string chartTitleColor;
		private bool chartTitleBold;
		private string chartBorderColor;
       
        private string legendBorderColor;
		private string legendBgColor;
        private LegendPosition legendPosition;
        private int legendFontSize;

        private string xAxesCaption;
        private bool showXAxes;

        private string yAxesCaption;
        private bool showYAxes;

        private int chartWidth;
        private int chartHeight;

        private string[] seriesNames;
        private string[] categorys;
        private double[][] values;

        private string picName;


		//自动生成默认无参构造函数
        public Column() { }
        /// <summary>
        /// 是否现实X轴
        /// </summary>
        public bool ShowXAxes
        {
            get { return showXAxes; }
            set { showXAxes = value; }
        }
        //是否显示Y轴
        public bool ShowYAxes
        {
            get { return showYAxes; }
            set { showYAxes = value; }
        }
        /// <summary>
        /// x轴标题
        /// </summary>
        public string XAxesCaption
        {
            get { return xAxesCaption; }
            set { xAxesCaption = value; }
        }
        /// <summary>
        /// y轴标题
        /// </summary>
        public string YAxesCaption
        {
            get { return yAxesCaption; }
            set { yAxesCaption = value; }
        }
		/// <summary>
		/// 图表标题
		/// </summary>
		public string ChartTitle
		{
			get { return chartTitle;}
			set { chartTitle = value;}
		}

        /// <summary>
        /// 图表标题字体大小
        /// </summary>
        public int ChartTitleSize
        {
            get { return chartTitleSize; }
            set { chartTitleSize = value; }
        }
        /// <summary>
        /// 图表标题字体颜色
        /// </summary>
        public string ChartTitleColor
        {
            get { return chartTitleColor; }
            set { chartTitleColor = value; }
        }

        /// <summary>
        /// 图表字体是否加粗
        /// </summary>
        public bool ChartTitleBold
        {
            get { return chartTitleBold; }
            set { chartTitleBold = value; }
        }

        /// <summary>
        /// 图表边框颜色
        /// </summary>
        public string ChartBorderColor
        {
            get { return chartBorderColor; }
            set { chartBorderColor = value; }
        }

     
        /// <summary>
        /// 图例边框颜色
        /// </summary>
        public string LegendBorderColor
        {
            get { return legendBorderColor; }
            set { legendBorderColor = value; }
        }

        /// <summary>
        /// 图例背景颜色
        /// </summary>
        public string LegendBgColor
        {
            get { return legendBgColor; }
            set { legendBgColor = value; }
        }
        /// <summary>
        /// 图例文字大小
        /// </summary>
        public int LegendFontSize
        {
            get { return legendFontSize; }
            set { legendFontSize = value; }
        }

        /// <summary>
        /// 图例位置
        /// </summary>
        public LegendPosition LegendPosition
        {
            get { return legendPosition; }
            set { legendPosition = value; }
        }

      
     
        /// <summary>
        /// 图表宽度
        /// </summary>
        public int ChartWidth
        {
            get { return chartWidth; }
            set { chartWidth = value; }
        }

        /// <summary>
        /// 图表高度
        /// </summary>
        public int ChartHeight
        {
            get { return chartHeight; }
            set { chartHeight = value; }
        }
        /// <summary>
        /// 序列名称
        /// </summary>
        public string[] SeriesNames
        {
            get { return seriesNames; }
            set { seriesNames = value; }
        }
        /// <summary>
        /// 类别
        /// </summary>
        public string[] Categorys
        {
            get { return categorys; }
            set { categorys = value; }
        }
        /// <summary>
        /// 值
        /// </summary>
        public double[][] Values
        {
            get { return values; }
            set { values = value; }
        }

        /// <summary>
        /// 数据颜色
        /// </summary>
        public string[] DataColor
        {
            get;
            set;
        }
       
        /// <summary>
        /// 生成的图片名称
        /// </summary>
        public string PicName
        {
            get { return picName; }
            set { picName = value; }
        }
    }
}
