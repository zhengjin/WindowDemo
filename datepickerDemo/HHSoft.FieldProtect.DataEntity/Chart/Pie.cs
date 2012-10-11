using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.Framework.Control;

namespace HHSoft.FieldProtect.DataEntity.Chart
{
    [Serializable]
    public class Pie
    {
       	private string chartTitle;
		private int chartTitleSize;
		private string chartTitleColor;
		private bool chartTitleBold;
		private string chartBorderColor;
        private PieType pieChartType;
        private string legendBorderColor;
		private string legendBgColor;
        private bool legenFontBold;
        private LegendPosition legendPosition;
        private int legendFontSize;
        private string seriesCollectionBorderColor;
        private bool hasPercentage;
        private string dataFontColor;
        private int dataFontSize;
        private int chartWidth;
        private int chartHeight;
        private string[] dataNames;
        private double[] dataValues;
        private string picName;


		//自动生成默认无参构造函数
		public Pie(){}		
     
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
        ///图例字体加粗
        /// </summary>
        public bool LegenFontBold
        {
            get { return legenFontBold; }
            set { legenFontBold = value; }
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
        /// 饼状图类型
        /// </summary>
        public PieType PicChartType
        {
            get { return pieChartType; }
            set { pieChartType = value; }
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
        /// 图序列边框颜色
        /// </summary>
        public string SeriesCollectionBorderColor
        {
            get { return seriesCollectionBorderColor; }
            set { seriesCollectionBorderColor = value; }
        }

        /// <summary>
        /// 是否显示百分比
        /// </summary>
        public bool HasPercentage
        {
            get { return hasPercentage; }
            set { hasPercentage = value; }
        }
        
        /// <summary>
        /// 数值字体颜色
        /// </summary>
        public string DataFontColor
        {
            get { return dataFontColor; }
            set { dataFontColor = value; }
        }
        /// <summary>
        /// 数值字体大小
        /// </summary>
        public int DataFontSize
        {
            get { return dataFontSize; }
            set { dataFontSize = value; }
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
        /// 数值名称
        /// </summary>
        public string[] DataNames
        {
            get { return dataNames; }
            set { dataNames = value; }
        }
        /// <summary>
        /// 数值
        /// </summary>
        public double[] DataValues
        {
            get { return dataValues; }
            set { dataValues = value; }
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
