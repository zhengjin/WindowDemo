using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;


namespace LinGIS
{
    public partial class MainFrm : DevComponents.DotNetBar.Office2007RibbonForm
    {
        private FeatLyrFrm newFeatLyrFrm;

        private ESRI.ArcGIS.Controls.IMapControl3 m_mapControl = null;
        private ESRI.ArcGIS.Controls.IPageLayoutControl2 m_pageLayoutControl = null;
        private ControlsSynchronizer m_controlsSynchronizer = null;

        private IMapDocument pMapDocument;
        
        public MainFrm()
        {
            if (!ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.ArcReader))
            {
                if (!ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop))
                {
                    MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.");
                    return;
                }
            }

            InitializeComponent();
        }
        

        private void MainFrm_Load(object sender, EventArgs e)
        {
            IAoInitialize m_aoinitialize = new AoInitializeClass();
            m_aoinitialize.Initialize(esriLicenseProductCode.esriLicenseProductCodeArcInfo); 

            //get a reference to the MapControl and the PageLayoutControl
            //取得MapControl和PageLayoutControl的引用
            m_mapControl = (IMapControl3)this.mainMapControl.Object;
            m_pageLayoutControl = (IPageLayoutControl2)this.axPageLayoutControl.Object;

            //initialize the controls synchronization calss
            //初始化controls synchronization calss
            m_controlsSynchronizer = new ControlsSynchronizer(m_mapControl, m_pageLayoutControl);

            //bind the controls together (both point at the same map) and set the MapControl as the active control
            //把MapControl和PageLayoutControl帮顶起来(两个都指向同一个Map),然后设置MapControl为活动的Control
            m_controlsSynchronizer.BindControls(true);

            //add the framework controls (TOC and Toolbars) in order to synchronize then when the
            //active control changes (call SetBuddyControl)
            //m_controlsSynchronizer.AddFrameworkControl(axToolbarControl1.Object);
            //m_controlsSynchronizer.AddFrameworkControl(axToolbarControl2.Object);
            m_controlsSynchronizer.AddFrameworkControl(this.axTOCControl.Object);

            //add the Open Map Document command onto the toolbar
            //OpenNewMapDocument openMapDoc = new OpenNewMapDocument(m_controlsSynchronizer);
            //axToolbarControl1.AddItem(openMapDoc, -1, 0, false, -1, esriCommandStyles.esriCommandStyleIconOnly);
        }




        private ILayer TOCRightLayer;

        private void axTOCControl_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            esriTOCControlItem itemType = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap basicMap = null;
            ILayer layer = null;
            object unk = null;
            object data = null;
            //this.axTOCControl.GetSelectedItem(ref itemType, ref basicMap, ref layer, ref unk, ref data);
            this.axTOCControl.HitTest(e.x,e.y,ref itemType, ref basicMap, ref layer, ref unk, ref data);
            if (e.button == 2)
            {
                //switch (itemType)
                //{
                //    case esriTOCControlItem.esriTOCControlItemNone:
                //        MessageBox.Show("None");
                //        break;
                //    case esriTOCControlItem.esriTOCControlItemMap:
                //        MessageBox.Show("Map\n" + basicMap.Name);
                //        break;
                //    case esriTOCControlItem.esriTOCControlItemLayer:
                //        MessageBox.Show("Layer\n" + layer.Name);
                //        break;
                //    case esriTOCControlItem.esriTOCControlItemLegendClass:
                //        MessageBox.Show("Legend\n" + ((ILegendGroup)unk).get_Class((int)data).Description + "\n" + ((ILegendGroup)unk).get_Class((int)data).Label);
                //        break;
                //    case esriTOCControlItem.esriTOCControlItemHeading:
                //        MessageBox.Show("Heading\n" + ((ILegendGroup)unk).Heading);
                //        break;
                //}
                switch (itemType)
                {
                    case esriTOCControlItem.esriTOCControlItemLayer:
                        this.TOCRightLayer = layer;
                        this.contextMenuTOCFeatureLyr.Show(this.axTOCControl, e.x, e.y);
                        break;
                    case esriTOCControlItem.esriTOCControlItemMap:
                        this.contextMenuTOCMap.Show(this.axTOCControl, e.x, e.y);
                        break;
                }
            }
            if (e.button == 1)
            {
                switch (itemType)
                {
                    case esriTOCControlItem.esriTOCControlItemLegendClass:
                        ILegendClass pLegendClass = ((ILegendGroup)unk).get_Class((int)data);
                        SymbolSelectorFrm newSymbolSelectorFrm = new SymbolSelectorFrm(pLegendClass, layer);
                        if (newSymbolSelectorFrm.ShowDialog() == DialogResult.OK)
                        {
                            this.mainMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                            pLegendClass.Symbol = newSymbolSelectorFrm.pSymbol;
                            this.axTOCControl.Update();
                        }
                        break;
                }
            }
        }
        
        private void axTOCControl_OnDoubleClick(object sender, ITOCControlEvents_OnDoubleClickEvent e)
        {
            esriTOCControlItem itemType = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap basicMap = null;
            ILayer layer = null;
            object unk = null;
            object data = null;
            this.axTOCControl.HitTest(e.x, e.y, ref itemType, ref basicMap, ref layer, ref unk, ref data);
            //this.axTOCControl.GetSelectedItem(ref itemType, ref basicMap, ref layer, ref unk, ref data);
            //if (itemType == esriTOCControlItem.esriTOCControlItemLayer)
            //{
            //    if (layer is IGeoFeatureLayer)
            //    {
            //        FeatLyrFrm newFeatLyrFrm = new FeatLyrFrm(layer);
            //        newFeatLyrFrm.Show();
            //    }
            //}
            switch (itemType)
            {
                case esriTOCControlItem.esriTOCControlItemLayer:
                    if (layer is IGeoFeatureLayer)
                    {
                        if (this.newFeatLyrFrm == null)
                        {
                            this.newFeatLyrFrm = new FeatLyrFrm();
                        }
                        this.newFeatLyrFrm.Layer = layer;
                        if (this.newFeatLyrFrm.ShowDialog() == DialogResult.OK)
                        {
                            this.mainMapControl.Refresh();
                            this.EagleaxMapControl.Refresh();
                            this.axTOCControl.Update();
                        }
                    }
                    break;
                case esriTOCControlItem.esriTOCControlItemLegendClass:
                    break;
            }
            
        }

       

        private IColor getRGBColor(int r, int g, int b)
        {
            IRgbColor pRgbColor = new RgbColorClass();
            pRgbColor.Red = r;
            pRgbColor.Green = g;
            pRgbColor.Blue = b;
            IColor pColor = pRgbColor as IColor;
            return pColor;
        }


        private IHsvColor getHsvColor(int hue, int saturation, int value)
        {
            IHsvColor pHsvColor = new HsvColorClass();
            pHsvColor.Hue = hue;
            pHsvColor.Saturation = saturation;
            pHsvColor.Value = value;
            return pHsvColor;
        }


        #region 专题图按钮
        private void btnSimpleRender_Click(object sender, EventArgs e)
        {
            ISimpleFillSymbol pSimpleFillSym = new SimpleFillSymbolClass();
            pSimpleFillSym.Color = getRGBColor(255, 0, 0);
            pSimpleFillSym.Style = esriSimpleFillStyle.esriSFSSolid;

            ISimpleRenderer pSimpleRender = new SimpleRendererClass();
            pSimpleRender.Symbol = pSimpleFillSym as ISymbol;
            pSimpleRender.Label = "haha";
            pSimpleRender.Description = "hoho";

            ITransparencyRenderer pTransRenderer = pSimpleRender as ITransparencyRenderer;
            pTransRenderer.TransparencyField = "GDP_1999（";//透明字段

            IGeoFeatureLayer pGeoFeatLyr = this.mainMapControl.get_Layer(0) as IGeoFeatureLayer;
            pGeoFeatLyr.Renderer = pTransRenderer as IFeatureRenderer;

            this.mainMapControl.Refresh(esriViewDrawPhase.esriViewBackground, null, null);
            //this.axTOCControl.SetBuddyControl(this.mainMapControl.Object);
            this.axTOCControl.Update();

        }

        private void btnClassBreakRender_Click(object sender, EventArgs e)
        {
            //先用一个ITableHistogram对象从一个要素类获取某一字段的所有值及频率,即dataValues和dataFrequency,这两个数组是分组的基本数据
            //取得两个数组后,系统用IClassGEN对象对它们进行分级,得到classes及classCount,前者是分级临界点值的数组,后者为分级数目
            //根据得到的分级数目和颜色带对象,可以分别设置ClassBreakRender对象的不同符号,产生不同的效果
            IGeoFeatureLayer pGeoFeatLyr;
            ITable pTable;
            
            object dataValues;
            object dataFrequency;
            string strOutput;

            pGeoFeatLyr = this.mainMapControl.get_Layer(0) as IGeoFeatureLayer;
            pTable = pGeoFeatLyr as ITable;
            //以下代码用TableHistogram和BasicHistogram统计出Table某一字段的值和值的频率
            ITableHistogram pTableHistogram;
            pTableHistogram = new BasicTableHistogramClass();
            pTableHistogram.Table = pTable;//需传入一个ITable
            pTableHistogram.Field = "GDP_1999（";//统计的字段
            IBasicHistogram pBasicHistogram;
            pBasicHistogram = pTableHistogram as IBasicHistogram;
            pBasicHistogram.GetHistogram(out dataValues, out dataFrequency);//关键

            //以下代码用IClassifyGEN和EqualInterval对象,基于分段数目,生成各段的临界值,并放在一个数组当中
            IClassifyGEN pClassifyGen = new EqualIntervalClass();
            double[] classes;
            int classCount = 5;
            pClassifyGen.Classify(dataValues, dataFrequency, ref classCount);//用到了上面ITableHistogram生成的值和值频率数组,关键!!!!!!
            classes = pClassifyGen.ClassBreaks as double[];//注意,此对象下标从1开始(我觉得0为最开头,所以临界值从1开始有意义),关键!!!!!!
            //classCount = classes.Length; 此处应该为6
            //for (int i = 0; i < classes.Length; i++)
            //{
            //    MessageBox.Show(classes[i].ToString());
            //}
            //MessageBox.Show(classCount.ToString());

            IClassBreaksRenderer pClassBreakRenderer = new ClassBreaksRendererClass();//设置分段着色属性
            pClassBreakRenderer.Field = "GDP_1999（";
            pClassBreakRenderer.BreakCount = 5;//分成5段
            pClassBreakRenderer.SortClassesAscending = true;

            //以下代码生成颜色带
            IHsvColor pFromColor = getHsvColor(60, 100, 96);
            IHsvColor pToColor = getHsvColor(0, 100, 96);
            IAlgorithmicColorRamp pColorRamp = new AlgorithmicColorRampClass();
            pColorRamp.FromColor = pFromColor as IColor;
            pColorRamp.ToColor = pToColor as IColor;
            pColorRamp.Size = classCount;//生成颜色的数目
            //MessageBox.Show(classCount.ToString());
            bool ok;
            pColorRamp.CreateRamp(out ok);//创建颜色带,关键!!!

            if (ok)//如果颜色带成功生成的话
            {
                IEnumColors pEnumColors = pColorRamp.Colors;//存放生成颜色带的各颜色
                pEnumColors.Reset();//是必须吗?????关键!!!
                
                IColor pColor;
                ISimpleFillSymbol pSimpleFillSymbol;
                //ISimpleMarkerSymbol pSimpleMarkerSymbol;

                for (int i = 0; i < classCount; i++)//为每个值范围设置符号(此处为SimpleFillSymbol)
                {
                    pColor = pEnumColors.Next();
                    pSimpleFillSymbol = new SimpleFillSymbolClass();
                    pSimpleFillSymbol.Color = pColor;
                    pSimpleFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
                    //pSimpleMarkerSymbol = new SimpleMarkerSymbolClass();
                    //pSimpleMarkerSymbol.Color = pColor;
                    pClassBreakRenderer.set_Symbol(i, (ISymbol)pSimpleFillSymbol);//设置Symbol,关键!!!
                    pClassBreakRenderer.set_Break(i, classes[i + 1]);//设置临界值,注意下标,关键!!!
                }

                pGeoFeatLyr.Renderer = pClassBreakRenderer as IFeatureRenderer;
                this.mainMapControl.Refresh(esriViewDrawPhase.esriViewGeography, null, null);
                //this.axTOCControl.SetBuddyControl(this.mainMapControl.Object);
                this.axTOCControl.Update();
            }
        }

        private void UniqueValueRender_Click(object sender, EventArgs e)
        {
            //
            ITable pTable= (ITable)this.mainMapControl.get_Layer(0);
            int fieldIndex = pTable.FindField("NAME");//取得"NAME"字段的index,关键
            
            IQueryFilter pQueryFilter = new QueryFilterClass();
            pQueryFilter.WhereClause = "";
            pQueryFilter.AddField("NAME");
            //IFeatureCursor pFeatureCursor = pTable.Search(pQueryFilter, false);
            
            int rowCount = pTable.RowCount(pQueryFilter);//取得"NAME"字段有值得行的数目,关键!!!!!

            //以下代码创建并设置随机颜色带对象,并生成颜色
            IRandomColorRamp pColorRamp = new RandomColorRampClass();
            pColorRamp.StartHue = 0;
            pColorRamp.MinValue = 99;
            pColorRamp.MinSaturation = 15;
            pColorRamp.EndHue = 360;
            pColorRamp.MaxValue = 100;
            pColorRamp.MaxSaturation = 30;
            pColorRamp.Size = rowCount;
            bool ok = true;
            pColorRamp.CreateRamp(out ok);//产生颜色
            
            if (ok)
            {
                IEnumColors pEnumColors = pColorRamp.Colors;
                
                ISimpleFillSymbol pSimpleFillSymbol;
                IRowBuffer pRowBuffer;
                IColor pColor;
                string value;

                IUniqueValueRenderer pUniqueValueRenderer = new UniqueValueRendererClass();//创建着色对象
                pUniqueValueRenderer.FieldCount = 1;//设置只对一个字段进行着色
                pUniqueValueRenderer.set_Field(0, "NAME");

                for (int i = 0; i < rowCount; i++)
                {
                    pRowBuffer = (IRowBuffer)pTable.GetRow(i);//关键,用于取得字段值
                    value = pRowBuffer.get_Value(fieldIndex).ToString();
                    pColor = pEnumColors.Next();
                    pSimpleFillSymbol = new SimpleFillSymbolClass();
                    pSimpleFillSymbol.Color = pColor;
                    pSimpleFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
                    pUniqueValueRenderer.AddValue(value, "", (ISymbol)pSimpleFillSymbol);//为每个唯一值设置Symbol,关键!!!!!!
                }
                //ITransparencyRenderer pTransparencyRenderer = pUniqueValueRenderer as ITransparencyRenderer;
                //pTransparencyRenderer.TransparencyField = "";
                
                ((IGeoFeatureLayer)pTable).Renderer = pUniqueValueRenderer as IFeatureRenderer;
                this.mainMapControl.Refresh(esriViewDrawPhase.esriViewGeography, null, null);
                //this.axTOCControl.SetBuddyControl(this.mainMapControl.Object);
                this.axTOCControl.Update();
            }
        }

        private void btnProportionalSymbolRender_Click(object sender, EventArgs e)
        {
            IGeoFeatureLayer pGeoFeatLyr = (IGeoFeatureLayer)this.mainMapControl.get_Layer(0);
            ITable pTable = pGeoFeatLyr as ITable;
            IQueryFilter pQueryFilter = new QueryFilterClass();
            pQueryFilter.AddField("GDP_1999（");
            pQueryFilter.WhereClause = "";
            ICursor pCursor;//DataStatistic对象统计需要用一个Cursor
            pCursor = pTable.Search(pQueryFilter, true);

            IDataStatistics pDataStatistics = new DataStatisticsClass();//用于统计字段信息,需要一个Cursor
            pDataStatistics.Cursor = pCursor;
            pDataStatistics.Field = "GDP_1999（";
            IStatisticsResults pStatisticsResults;//字段统计结果
            pStatisticsResults = pDataStatistics.Statistics;
                            
            ISimpleFillSymbol pSimpleFillSymbol = new SimpleFillSymbolClass();//用于背景
            pSimpleFillSymbol.Color = this.getHsvColor(60, 120, 60);

            ISimpleMarkerSymbol pSimpleMarkerSymbol = new SimpleMarkerSymbolClass();//标注符号(最小),注意size属性
            pSimpleMarkerSymbol.Color = this.getHsvColor(120, 100, 75);
            pSimpleMarkerSymbol.Size = 0.5;
            //以下创建并设置着色对象
            IProportionalSymbolRenderer pProportionalSymbolRenderer = new ProportionalSymbolRendererClass();
            pProportionalSymbolRenderer.ValueUnit = esriUnits.esriUnknownUnits;
            pProportionalSymbolRenderer.Field = "GDP_1999（";
            pProportionalSymbolRenderer.FlanneryCompensation = false;
            pProportionalSymbolRenderer.MinDataValue = 1;//为什么用pStatisticsResults.Minimum会出错??????????
            pProportionalSymbolRenderer.MaxDataValue = pStatisticsResults.Maximum;
            pProportionalSymbolRenderer.BackgroundSymbol = pSimpleFillSymbol as IFillSymbol;
            pProportionalSymbolRenderer.MinSymbol = pSimpleMarkerSymbol as ISymbol;//最小值的符号,关键!!!!!!!
            pProportionalSymbolRenderer.LegendSymbolCount = 3;
            pProportionalSymbolRenderer.CreateLegendSymbols();

            pGeoFeatLyr.Renderer = pProportionalSymbolRenderer as IFeatureRenderer;
            this.mainMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            this.axTOCControl.Update();
        }

        private void btnStackedChartRender_Click(object sender, EventArgs e)
        {
            IGeoFeatureLayer pGeoFeatLyr = (IGeoFeatureLayer)this.mainMapControl.get_Layer(0);

            double p1, p2, max;//比较制作专题图所用两个字段的最大值的最大
            ICursor pCursor = (ICursor)pGeoFeatLyr.Search(null, true);
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Cursor = pCursor;
            pDataStatistics.Field = "GDP_1999（";
            p1 = pDataStatistics.Statistics.Maximum;
            pCursor = (ICursor)pGeoFeatLyr.Search(null, true);//重新设置Cursor,为什么一定要这样?
            //pDataStatistics.Cursor = null;
            pDataStatistics.Cursor = pCursor;
            pDataStatistics.Field = "GDP_1994（";
            p2 = pDataStatistics.Statistics.Maximum;
            //MessageBox.Show(p1.ToString() + "\n" + p2.ToString());
            max = p1 > p2 ? p1 : p2;

            //以下创造3个SimpleFillSymbol,供下面使用
            ISimpleFillSymbol pSimpleFillSymbol = new SimpleFillSymbolClass();
            pSimpleFillSymbol.Color = getRGBColor(255, 0, 0);
            pSimpleFillSymbol.Outline = null;
            ISimpleFillSymbol pSimpleFillSymbol2 = new SimpleFillSymbolClass();
            pSimpleFillSymbol2.Color = getRGBColor(0, 0, 255);
            pSimpleFillSymbol2.Outline = null;
            ISimpleFillSymbol pSimpleFillSymbol3 = new SimpleFillSymbolClass();
            pSimpleFillSymbol3.Color = getRGBColor(0, 255, 0);

            //以下代码设置StackedChart符号,和StackedChart符号的各符号
            IStackedChartSymbol pStackedChartSymbol = new StackedChartSymbolClass();//Stached符号
            pStackedChartSymbol.Width = 6;//宽度
            pStackedChartSymbol.Fixed = false;//不固定长度
            IChartSymbol pChartSymbol = pStackedChartSymbol as IChartSymbol;
            pChartSymbol.MaxValue = max;//必须
            IMarkerSymbol pMarkerSymbol = pStackedChartSymbol as IMarkerSymbol;
            pMarkerSymbol.Size = 60;//符号大小
            ISymbolArray pSymbolArray = pStackedChartSymbol as ISymbolArray;//SymbolArray
            pSymbolArray.AddSymbol((ISymbol)pSimpleFillSymbol);//添加符号
            pSymbolArray.AddSymbol((ISymbol)pSimpleFillSymbol2);//添加符号

            //以下代码设置着色对象
            IChartRenderer pChartRenderer = new ChartRendererClass();
            IRendererFields pRendererFields = pChartRenderer as IRendererFields;
            pRendererFields.AddField("GDP_1999（", "GDP_1999（");
            pRendererFields.AddField("GDP_1994（", "GDP_1994（");
            pChartRenderer.ChartSymbol = (IChartSymbol)pStackedChartSymbol;//设置符号
            pChartRenderer.Label = "GDP";
            pChartRenderer.BaseSymbol = pSimpleFillSymbol3 as ISymbol;//背景
            pChartRenderer.UseOverposter = true;//防止重叠
            pChartRenderer.CreateLegend();//创造图例,关键!!!!!!!!!

            pGeoFeatLyr.Renderer = (IFeatureRenderer)pChartRenderer;
            this.mainMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            this.axTOCControl.Update();
        }

        private void btnBarChartRender_Click(object sender, EventArgs e)//此专题图制作跟StackedChart差不多
        {
            IGeoFeatureLayer pGeoFeatLyr = this.mainMapControl.get_Layer(0) as IGeoFeatureLayer;
            
            //用DataStatistics对象,得到两个字段最大值的较大值(最最大)
            double p1, p2, max;
            ICursor pCursor = (ICursor)pGeoFeatLyr.Search(null, true);
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Cursor = pCursor;
            pDataStatistics.Field = "GDP_1999（";
            p1 = pDataStatistics.Statistics.Maximum;
            pCursor = (ICursor)pGeoFeatLyr.Search(null, true);
            pDataStatistics.Cursor = pCursor;
            pDataStatistics.Field = "GDP_1994（";
            p2 = pDataStatistics.Statistics.Maximum;
            max = p1 > p2 ? p1 : p2;
            
            //以下生成好三个SimpleFillSymbol,留作后用
            ISimpleFillSymbol pSimpleFillSymbol = new SimpleFillSymbolClass();
            pSimpleFillSymbol.Color = getRGBColor(255, 0, 0);
            pSimpleFillSymbol.Outline = null;
            ISimpleFillSymbol pSimpleFillSymbol2 = new SimpleFillSymbolClass();
            pSimpleFillSymbol2.Color = getRGBColor(0, 0, 255);
            pSimpleFillSymbol2.Outline = null;
            ISimpleFillSymbol pSimpleFillSymbol3 = new SimpleFillSymbolClass();
            pSimpleFillSymbol3.Color = getRGBColor(0, 255, 0);
            
            //创建并设置BarChartSymbol,以及设置好BarChartSymbol里的各自Symbol
            IBarChartSymbol pBarChartSymbol = new BarChartSymbolClass();
            pBarChartSymbol.Width = 6;//宽度
            pBarChartSymbol.Spacing = 1;//Bar之间的间隔
            IChartSymbol pChartSymbol = pBarChartSymbol as IChartSymbol;
            pChartSymbol.MaxValue = max;//用到了上面求出的最大值
            ISymbolArray pSymbolArray = pBarChartSymbol as ISymbolArray;//用于在BarChartSymbol里添加Symbol,关键
            pSymbolArray.AddSymbol((ISymbol)pSimpleFillSymbol);
            pSymbolArray.AddSymbol((ISymbol)pSimpleFillSymbol2);
            IMarkerSymbol pMarkerSymbol = pBarChartSymbol as IMarkerSymbol;
            pMarkerSymbol.Size = 60;//BarChartSymbol符号大小
            
            //以下创建并设置着色对象
            IChartRenderer pChartRenderer = new ChartRendererClass();
            IRendererFields pRendererFields = pChartRenderer as IRendererFields;//添加用于着色的字段,关键
            pRendererFields.AddField("GDP_1994（", "GDP_1994（");
            pRendererFields.AddField("GDP_1999（", "GDP_1999（");
            pChartRenderer.ChartSymbol = pChartSymbol;//赋给上面创建的BarChartSymbol
            pChartRenderer.BaseSymbol = pSimpleFillSymbol3 as ISymbol;
            pChartRenderer.Label = "GDP";
            pChartRenderer.UseOverposter = true;
            pChartRenderer.CreateLegend();//创建图例,关键

            pGeoFeatLyr.Renderer = pChartRenderer as IFeatureRenderer;
            this.mainMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            this.axTOCControl.Update();
        }

        private void btnPieChartRender_Click(object sender, EventArgs e)
        {
            IGeoFeatureLayer pGeoFeatLyr = this.mainMapControl.get_Layer(0) as IGeoFeatureLayer;

            //找出两个用于着色字段的最大值的最大值(这次加上最小值之和,这个函数的尾后部分用到,用于在着色对象里确定最小的PieChartSymbol的大小)
            double p1, p2, max, m1, m2, min;
            ICursor pCursor = pGeoFeatLyr.Search(null, true) as ICursor;
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Cursor = pCursor;
            pDataStatistics.Field = "GDP_1994（";
            p1 = pDataStatistics.Statistics.Maximum;
            m1 = pDataStatistics.Statistics.Minimum;
            pCursor = pGeoFeatLyr.Search(null, true) as ICursor;
            pDataStatistics.Cursor = pCursor;
            pDataStatistics.Field = "GDP_1999（";
            p2 = pDataStatistics.Statistics.Maximum;
            m2 = pDataStatistics.Statistics.Minimum;
            max = p1 > p2 ? p1 : p2;
            min = m1 + m2;//最小值之和

            //以下生成好三个SimpleFillSymbol,留作后用
            ISimpleFillSymbol pSimpleFillSymbol = new SimpleFillSymbolClass();
            pSimpleFillSymbol.Color = getRGBColor(255, 0, 0);
            pSimpleFillSymbol.Outline = null;
            ISimpleFillSymbol pSimpleFillSymbol2 = new SimpleFillSymbolClass();
            pSimpleFillSymbol2.Color = getRGBColor(0, 0, 255);
            pSimpleFillSymbol2.Outline = null;
            ISimpleFillSymbol pSimpleFillSymbol3 = new SimpleFillSymbolClass();
            pSimpleFillSymbol3.Color = getRGBColor(0, 255, 0);

            //以下创建并设置PieChartSymbol对象,并设置PieChartSymbol对象的各个SimpleFillSymbol
            IPieChartSymbol pPieChartSymbol = new PieChartSymbolClass();
            pPieChartSymbol.Clockwise = true;
            pPieChartSymbol.UseOutline = false;
            IChartSymbol pChartSymbol = pPieChartSymbol as IChartSymbol;
            pChartSymbol.MaxValue = max;
            ISymbolArray pSymbolArray = pPieChartSymbol as ISymbolArray;
            pSymbolArray.AddSymbol((ISymbol)pSimpleFillSymbol);
            pSymbolArray.AddSymbol((ISymbol)pSimpleFillSymbol2);
            IMarkerSymbol pMarkerSymbol = pPieChartSymbol as IMarkerSymbol;
            pMarkerSymbol.Size = 5;//PieChartSymbol的大小(我觉得是饼的厚度)
            
            //以下创建并设置着色对象
            IChartRenderer pChartRenderer = new ChartRendererClass();
            pChartRenderer.BaseSymbol = pSimpleFillSymbol3 as ISymbol;
            pChartRenderer.ChartSymbol = pChartSymbol;
            pChartRenderer.UseOverposter = true;
            pChartRenderer.Label = "GDP";
            IPieChartRenderer pPieChartRenderer = pChartRenderer as IPieChartRenderer;
            pPieChartRenderer.MinSize = 0.5;//最小的尺码关键
            pPieChartRenderer.MinValue = 1;//最小尺码的值,为什么0不可以??????????????关键
            pPieChartRenderer.ProportionalBySum = true;//PieChartSymbol的大小按照字段和成比例,关键
                                                       //若PieChartSymbol的大小只按一个字段,则须设置.ProportionalField为该字段
            IRendererFields pRenderFields = pChartRenderer as IRendererFields;//添加用于着色的字段
            pRenderFields.AddField("GDP_1994（", "GDP_1994（");
            pRenderFields.AddField("GDP_1999（", "GDP_1999（");
            pChartRenderer.CreateLegend();//生成图例

            pGeoFeatLyr.Renderer = pChartRenderer as IFeatureRenderer;
            this.mainMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            this.axTOCControl.Update();
        }

        private void btnDotDensityRender_Click(object sender, EventArgs e)
        {
            IGeoFeatureLayer pGeoFeatLyr = this.mainMapControl.get_Layer(0) as IGeoFeatureLayer;
            
            //创建SimpleMarkerSymbol,将会用在DotDensitySymbol中
            IMarkerSymbol pMarkerSymbol = new SimpleMarkerSymbolClass();
            pMarkerSymbol.Color = getRGBColor(255, 0, 0);
            
            //创建并设置DotDensitySymbol,以及DotDensitySymbol中的Symbol(上面的MarkerSymbol)
            IDotDensityFillSymbol pDotDensityFillSymbol = new DotDensityFillSymbolClass();
            pDotDensityFillSymbol.DotSize = 5;//大小
            pDotDensityFillSymbol.Color = getRGBColor(0, 255, 0);//此颜色不知为何物???????????
            pDotDensityFillSymbol.BackgroundColor = getRGBColor(239, 228, 190);
            ISymbolArray pSymbolArray = pDotDensityFillSymbol as ISymbolArray;
            pSymbolArray.AddSymbol((ISymbol)pMarkerSymbol);//添加符号,关键
            
            //以下创建并设置着色对象
            IDotDensityRenderer pDotDensityRenderer = new DotDensityRendererClass();
            pDotDensityRenderer.DotDensitySymbol = pDotDensityFillSymbol;//着色对象所用的DotDensitySymbol
            pDotDensityRenderer.DotValue = 100;//一个点代表的值,关键!!!
            pDotDensityRenderer.MaintainSize = true;//地图缩放时DotDensitySymbol符号大小保持
            IRendererFields pRendererFields = pDotDensityRenderer as IRendererFields;
            pRendererFields.AddField("GDP_1999（", "GDP_1999（");//用于着色的字段,关键
            pDotDensityRenderer.CreateLegend();

            pGeoFeatLyr.Renderer = pDotDensityRenderer as IFeatureRenderer;
            this.mainMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            this.axTOCControl.Update();
        }

        private void btnAnno_Click(object sender, EventArgs e)
        {
            IGeoFeatureLayer pGeoFeatLyr = this.mainMapControl.get_Layer(0) as IGeoFeatureLayer;
            //获得AnnotateLayerPropertiesCollection
            IAnnotateLayerPropertiesCollection pAnnoLyrProCollection = pGeoFeatLyr.AnnotationProperties;
            pAnnoLyrProCollection.Clear();
            //创建并设置TextSymbol
            ITextSymbol pTextSymbol = new TextSymbolClass();
            System.Drawing.Font pFont = new System.Drawing.Font("Castellar", 15.0F);
            //pTextSymbol.Font = ESRI.ArcGIS.Utility.COMSupport.OLE.GetIFontDispFromFont(pFont) as stdole.IFontDisp;//转换.NET的Font
            pTextSymbol.Color = getRGBColor(255, 0, 0);
            //创建LineLabelPosition
            ILineLabelPosition pLineLabelPosition = new LineLabelPositionClass();
            pLineLabelPosition.Parallel = false;
            pLineLabelPosition.Perpendicular = true;
            //创建LineLabelPlacementPriorities
            ILineLabelPlacementPriorities pLineLablePlacementPriorities = new LineLabelPlacementPrioritiesClass();
            //创建BasicOverposterLayerProperties
            IBasicOverposterLayerProperties pBasicOverposterLayerProperties = new BasicOverposterLayerPropertiesClass();
            pBasicOverposterLayerProperties.FeatureType = esriBasicOverposterFeatureType.esriOverposterPolyline;
            pBasicOverposterLayerProperties.LineLabelPlacementPriorities = pLineLablePlacementPriorities;
            pBasicOverposterLayerProperties.LineLabelPosition = pLineLabelPosition;
            //创建LabelEngineLayerProperties
            ILabelEngineLayerProperties pLabelEnginLayerProperties = new LabelEngineLayerPropertiesClass();
            pLabelEnginLayerProperties.Symbol = pTextSymbol;
            pLabelEnginLayerProperties.BasicOverposterLayerProperties = pBasicOverposterLayerProperties;
            pLabelEnginLayerProperties.Expression = "[NAME]";

            IAnnotateLayerProperties pAnnoLyrPro = pLabelEnginLayerProperties as IAnnotateLayerProperties;
            pAnnoLyrProCollection.Add(pAnnoLyrPro);//添加,关键
            pGeoFeatLyr.DisplayAnnotation = true;//确定显示

            this.mainMapControl.Refresh();
        }
        #endregion

        #region tools工具条制作
        //private void btnZoomIn_Click(object sender, EventArgs e)
        //{
        //    ICommand pCommand = new ControlsMapZoomInToolClass();
        //    ITool pTool = pCommand as ITool;
        //    if (this.tabCtrlDataAndLayout.SelectedTab == this.tpgData)
        //    {
        //        pCommand.OnCreate(this.mainMapControl.Object);
        //        this.mainMapControl.CurrentTool = pTool;
        //    }
        //    else
        //    {
        //        pCommand.OnCreate(this.axPageLayoutControl.Object);
        //        this.axPageLayoutControl.CurrentTool = pTool;
        //    }
        //}

        //private void btnZoomOut_Click(object sender, EventArgs e)
        //{
        //    ICommand pCommand = new ControlsMapZoomOutToolClass();
        //    ITool pTool = pCommand as ITool;
        //    if (this.tabCtrlDataAndLayout.SelectedTab == this.tpgData)
        //    {
        //        pCommand.OnCreate(this.mainMapControl.Object);
        //        this.mainMapControl.CurrentTool = pTool;
        //    }
        //    else
        //    {
        //        pCommand.OnCreate(this.axPageLayoutControl.Object);
        //        this.axPageLayoutControl.CurrentTool = pTool;
        //    }
        //}

        //private void btnFixedZoomIn_Click(object sender, EventArgs e)
        //{
        //    ICommand pCommand = new ControlsMapZoomInFixedCommandClass();
        //    if (this.tabCtrlDataAndLayout.SelectedTab == this.tpgData)
        //    {
        //        pCommand.OnCreate(this.mainMapControl.Object);
        //        pCommand.OnClick();
        //    }
        //    else
        //    {
        //        pCommand.OnCreate(this.axPageLayoutControl.Object);
        //        pCommand.OnClick();
        //    }
        //}

        //private void btnFixedZoomOut_Click(object sender, EventArgs e)
        //{
        //    ICommand pCommand = new ControlsMapZoomOutFixedCommandClass();
        //    if (this.tabCtrlDataAndLayout.SelectedTab == this.tpgData)
        //    {
        //        pCommand.OnCreate(this.mainMapControl.Object);
        //        pCommand.OnClick();
        //    }
        //    else
        //    {
        //        pCommand.OnCreate(this.axPageLayoutControl.Object);
        //        pCommand.OnClick();
        //    }
        //}

        //private void btnPan_Click(object sender, EventArgs e)
        //{
        //    ICommand pCommand = new ControlsMapPanToolClass();
        //    ITool pTool = pCommand as ITool;
        //    if (this.tabCtrlDataAndLayout.SelectedTab == this.tpgData)
        //    {
        //        pCommand.OnCreate(this.mainMapControl.Object);
        //        this.mainMapControl.CurrentTool = pTool;
        //    }
        //    else
        //    {
        //        pCommand.OnCreate(this.axPageLayoutControl.Object);
        //        this.axPageLayoutControl.CurrentTool = pTool;
        //    }
        //}

        //private void btnFullExtent_Click(object sender, EventArgs e)
        //{
        //    ICommand pCommand = new ControlsMapFullExtentCommandClass();
        //    if (this.tabCtrlDataAndLayout.SelectedTab == this.tpgData)
        //    {
        //        pCommand.OnCreate(this.mainMapControl.Object);
        //        pCommand.OnClick();
        //    }
        //    else
        //    {
        //        pCommand.OnCreate(this.axPageLayoutControl.Object);
        //        pCommand.OnClick();
        //    }
        //}

        //private void btnPreviousExtent_Click(object sender, EventArgs e)
        //{
        //    ICommand pCommand = new ControlsMapZoomToLastExtentBackCommandClass();
        //    if (this.tabCtrlDataAndLayout.SelectedTab == this.tpgData)
        //    {
        //        pCommand.OnCreate(this.mainMapControl.Object);
        //        pCommand.OnClick();
        //    }
        //    else
        //    {
        //        pCommand.OnCreate(this.axPageLayoutControl.Object);
        //        pCommand.OnClick();
        //    }
        //}

        //private void btnNextExtent_Click(object sender, EventArgs e)
        //{
        //    ICommand pCommand = new ControlsMapZoomToLastExtentForwardCommandClass();
        //    if (this.tabCtrlDataAndLayout.SelectedTab == this.tpgData)
        //    {
        //        pCommand.OnCreate(this.mainMapControl.Object);
        //        pCommand.OnClick();
        //    }
        //    else
        //    {
        //        pCommand.OnCreate(this.axPageLayoutControl.Object);
        //        pCommand.OnClick();
        //    }
        //}

        //private void btnSelectFeature_Click(object sender, EventArgs e)
        //{
        //    ICommand pCommand = new ControlsSelectFeaturesToolClass();
        //    ITool pTool = pCommand as ITool;
        //    if (this.tabCtrlDataAndLayout.SelectedTab == this.tpgData)
        //    {
        //        pCommand.OnCreate(this.mainMapControl.Object);
        //        this.mainMapControl.CurrentTool = pTool;
        //    }
        //    else
        //    {
        //        pCommand.OnCreate(this.axPageLayoutControl.Object);
        //        this.axPageLayoutControl.CurrentTool = pTool;
        //    }
        //}

        //private void btnClearSelect_Click(object sender, EventArgs e)
        //{
        //    ICommand pCommand = new ControlsClearSelectionCommandClass();
        //    if (this.tabCtrlDataAndLayout.SelectedTab == this.tpgData)
        //    {
        //        pCommand.OnCreate(this.mainMapControl.Object);
        //        pCommand.OnClick();
        //    }
        //    else
        //    {
        //        pCommand.OnCreate(this.axPageLayoutControl.Object);
        //        pCommand.OnClick();
        //    }
        //}

        //private void btnSelectElement_Click(object sender, EventArgs e)
        //{
        //    ICommand pCommand = new ControlsSelectToolClass();
        //    ITool pTool = pCommand as ITool;
        //    if (this.tabCtrlDataAndLayout.SelectedTab == this.tpgData)
        //    {
        //        pCommand.OnCreate(this.mainMapControl.Object);
        //        this.mainMapControl.CurrentTool = pTool;
        //    }
        //    else
        //    {
        //        pCommand.OnCreate(this.axPageLayoutControl.Object);
        //        this.axPageLayoutControl.CurrentTool = pTool;
        //    }
        //}

        //private void btnIdentify_Click(object sender, EventArgs e)
        //{
        //    ICommand pCommand = new ControlsMapIdentifyToolClass();
        //    ITool pTool = pCommand as ITool;
        //    if (this.tabCtrlDataAndLayout.SelectedTab == this.tpgData)
        //    {
        //        pCommand.OnCreate(this.mainMapControl.Object);
        //        this.mainMapControl.CurrentTool = pTool;
        //    }
        //    else
        //    {
        //        pCommand.OnCreate(this.axPageLayoutControl.Object);
        //        this.axPageLayoutControl.CurrentTool = pTool;
        //    }
        //}

        //private void btnFind_Click(object sender, EventArgs e)
        //{
        //    ICommand pCommand = new ControlsMapFindCommandClass();
        //    if (this.tabCtrlDataAndLayout.SelectedTab == this.tpgData)
        //    {
        //        pCommand.OnCreate(this.mainMapControl.Object);
        //        pCommand.OnClick();
        //    }
        //    else
        //    {
        //        pCommand.OnCreate(this.axPageLayoutControl.Object);
        //        pCommand.OnClick();
        //    }
        //}

        //private void btnGoToXY_Click(object sender, EventArgs e)
        //{
        //    ICommand pCommand = new ControlsMapGoToCommandClass();
        //    if (this.tabCtrlDataAndLayout.SelectedTab == this.tpgData)
        //    {
        //        pCommand.OnCreate(this.mainMapControl.Object);
        //        pCommand.OnClick();
        //    }
        //    else
        //    {
        //        pCommand.OnCreate(this.axPageLayoutControl.Object);
        //        pCommand.OnClick();
        //    }
        //}

        //private void btnMeasure_Click(object sender, EventArgs e)
        //{
        //    ICommand pCommand = new ControlsMapMeasureToolClass();
        //    ITool pTool = pCommand as ITool;
        //    if (this.tabCtrlDataAndLayout.SelectedTab == this.tpgData)
        //    {
        //        pCommand.OnCreate(this.mainMapControl.Object);
        //        this.mainMapControl.CurrentTool = pTool;
        //    }
        //    else
        //    {
        //        pCommand.OnCreate(this.axPageLayoutControl.Object);
        //        this.axPageLayoutControl.CurrentTool = pTool;
        //    }
        //}

        //private void btnHyperlink_Click(object sender, EventArgs e)
        //{
        //    ICommand pCommand = new ControlsMapHyperlinkToolClass();
        //    ITool pTool = pCommand as ITool;
        //    if (this.tabCtrlDataAndLayout.SelectedTab == this.tpgData)
        //    {
        //        pCommand.OnCreate(this.mainMapControl.Object);
        //        this.mainMapControl.CurrentTool = pTool;
        //    }
        //    else
        //    {
        //        pCommand.OnCreate(this.axPageLayoutControl.Object);
        //        this.axPageLayoutControl.CurrentTool = pTool;
        //    }
        //}
        #endregion

        #region 主窗体Size变化event handler
        private void MainFrm_ResizeBegin(object sender, EventArgs e)
        {
            this.mainMapControl.SuppressResizeDrawing(true, 0);
            this.EagleaxMapControl.SuppressResizeDrawing(true, 0);
            this.axPageLayoutControl.SuppressResizeDrawing(true, 0);
            this.axTOCControl.SetBuddyControl(null);
        }

        private void MainFrm_ResizeEnd(object sender, EventArgs e)
        {
            this.mainMapControl.SuppressResizeDrawing(false, 0);
            this.axPageLayoutControl.SuppressResizeDrawing(false, 0);
            this.EagleaxMapControl.SuppressResizeDrawing(false, 0);
            this.axTOCControl.SetBuddyControl(this.mainMapControl.Object);
        }
        #endregion


        private void btnFlashShape_Click(object sender, EventArgs e)
        {
            //IEnvelope pEnvelope = this.mainMapControl.TrackRectangle();
            //IIdentify2 pIdentify = this.mainMapControl.get_Layer(0) as IIdentify2;
            //IArray pArray = pIdentify.Identify(pEnvelope as IGeometry, null);
            //IIdentifyObj pIdentifyObj;
            //for (int i = 0; i < pArray.Count; i++)
            //{
            //    pIdentifyObj = (IIdentifyObj)pArray.get_Element(i);
            //    pIdentifyObj.Flash(this.mainMapControl.ActiveView.ScreenDisplay);
            //}
            double a = Convert.ToDouble(txtDouble.Text);
            lblDouble.Text = a.ToString(txtFormat.Text);
        }

     

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 12; i++)
            {
                   
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ILayer pLayer = this.mainMapControl.Map.get_Layer(0);
            if (pLayer is IFeatureLayer)
            {
                if (((IFeatureLayer)pLayer).FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                {
                    IFeatureSelection pFeatureSelection = pLayer as IFeatureSelection;
                    ISelectionSet pSelectionSet = pFeatureSelection.SelectionSet;
                    ICursor pCursor;
                    pSelectionSet.Search(null, false, out pCursor);
                    if (pCursor != null)
                    {
                        IRow pRow = pCursor.NextRow();
                        if (pRow != null)
                        {
                            IArea pArea = ((IFeature)pRow).Shape as IArea;
                            MessageBox.Show(pArea.Area.ToString());
                        }
                    }
                }
            }

            //ICommand pCommand = new ControlsNewCircleToolClass();
            //pCommand.OnCreate(this.mainMapControl.Object);
            //ITool pTool = pCommand as ITool;
            //this.mainMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            //this.mainMapControl.CurrentTool = pTool;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ICommand pCommand = new ControlsNewMarkerToolClass();
            pCommand.OnCreate(this.mainMapControl.Object);
            ITool pTool = pCommand as ITool;
            this.mainMapControl.CurrentTool = pTool;
            this.mainMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ICommand pCommand = new ControlsRotateElementToolClass();
            pCommand.OnCreate(this.mainMapControl.Object);
            ITool pTool = pCommand as ITool;
            this.mainMapControl.CurrentTool = pTool;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ICommand pCommand = new ControlsAlignBottomCommandClass();
            pCommand.OnCreate(this.mainMapControl.Object);
            pCommand.OnClick();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            ICommand pCommand = new ControlsGroupCommandClass();
            pCommand.OnCreate(this.mainMapControl.Object);
            pCommand.OnClick();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            this.selectionChangedHandler = new IActiveViewEvents_SelectionChangedEventHandler(this.onSelectionChanged);
            this.pMap = this.mainMapControl.ActiveView.FocusMap;
            ((IActiveViewEvents_Event)this.pMap).SelectionChanged += this.selectionChangedHandler;
        }
        private IMap pMap;
        private IActiveViewEvents_SelectionChangedEventHandler selectionChangedHandler;
        private void onSelectionChanged()
        {
            MessageBox.Show("Selection Changed");
        }

        private void mainMapControl_OnExtentUpdated(object sender, IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            IEnvelope pEnvelope = e.newEnvelope as IEnvelope;

            ISimpleLineSymbol pSimpleLineSymbol = new SimpleLineSymbolClass();
            pSimpleLineSymbol.Style = esriSimpleLineStyle.esriSLSDash;
            pSimpleLineSymbol.Width = 2;
            pSimpleLineSymbol.Color = this.getRGBColor(0, 0, 0);

            ISimpleFillSymbol pSimpleFillSymbol = new SimpleFillSymbolClass();
            pSimpleFillSymbol.Color = this.getRGBColor(255, 0, 0);
            pSimpleFillSymbol.Outline = pSimpleLineSymbol as ILineSymbol;
            pSimpleFillSymbol.Style = esriSimpleFillStyle.esriSFSHollow;


            IRectangleElement pRectangleElement = new RectangleElementClass();
            IElement pElement = pRectangleElement as IElement;
            pElement.Geometry = pEnvelope as IGeometry;
            IFillShapeElement pFillShapeElement = pRectangleElement as IFillShapeElement;
            pFillShapeElement.Symbol = pSimpleFillSymbol as IFillSymbol;

            IGraphicsContainer pGraphicsContainer = this.EagleaxMapControl.Map as IGraphicsContainer;
            pGraphicsContainer.DeleteAllElements();
            pGraphicsContainer.AddElement(pElement, 0);

            this.EagleaxMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        private void EagleaxMapControl_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (this.EagleaxMapControl.Map.LayerCount != 0)
            {

                if (e.button == 1)
                {
                    IPoint pPoint = new PointClass();
                    pPoint.PutCoords(e.mapX, e.mapY);

                    IEnvelope pEnvelope = this.mainMapControl.Extent;
                    pEnvelope.CenterAt(pPoint);
                    this.mainMapControl.Extent = pEnvelope;
                    this.mainMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                }
                else if (e.button == 2)
                {
                    IEnvelope pEnvelop = this.EagleaxMapControl.TrackRectangle();
                    this.mainMapControl.Extent = pEnvelop;
                    this.mainMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                }

            }
        }

        private void EagleaxMapControl_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            if (e.button != 1) return;
            IPoint pPoint = new PointClass();
            pPoint.PutCoords(e.mapX, e.mapY);

            this.mainMapControl.CenterAt(pPoint);
            this.mainMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

        private void mainMapControl_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            //this.lblCurrentLocation.Text = string.Format("{0},{1},{2}", e.mapX.ToString("#######.###"), e.mapY.ToString("#######.###"), this.mainMapControl.MapUnits.ToString().Substring(4));
            this.lblCurrentLocation.Text = string.Format("{0}  {1}  {2}", e.mapX.ToString("#######.###"), e.mapY.ToString("#######.###"), this.mainMapControl.MapUnits.ToString().Substring(4));
        }

        private void axPageLayoutControl_OnMouseMove(object sender, IPageLayoutControlEvents_OnMouseMoveEvent e)
        {
            //this.lblCurrentLocation.Text = string.Format("{0},{1},{2}", e.pageX.ToString("#######.###"), e.pageY.ToString("#######.###"), this.axPageLayoutControl.Page.Units.ToString().Substring(4));
            this.lblCurrentLocation.Text = string.Format("{0}  {1}  {2}", e.pageX.ToString("#######.###"), e.pageY.ToString("#######.###"), this.axPageLayoutControl.Page.Units.ToString().Substring(4));
        }

        private void SaveDocToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.pMapDocument != null)
                {
                    this.pMapDocument.Save(this.pMapDocument.UsesRelativePaths, true);
                }
                else
                {
                    this.SaveDocAsToolStripMenuItem_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveDocAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.pMapDocument = new MapDocumentClass();
                this.saveFileDialog.Filter = "MXD文件|*.mxd";
                if (this.saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.pMapDocument.New(this.saveFileDialog.FileName);
                    IMxdContents pMxdContents = this.m_controlsSynchronizer as IMxdContents;
                    this.pMapDocument.ReplaceContents(pMxdContents);
                    this.pMapDocument.Save(true, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            this.barToc.Hide();
        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            //this.barEagelMap.ReDock();
        }

        private void tabCtrlDataAndLayout_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void tabControl_SelectedTabChanged(object sender, DevComponents.DotNetBar.TabStripTabChangedEventArgs e)
        {
            if (this.tabControl.SelectedTabIndex == 0)
            {
                //activate the MapControl and deactivate the PageLayoutControl
                m_controlsSynchronizer.ActivateMap();
            }
            else
            {
                //activate the PageLayoutControl and deactivate the MapControl
                m_controlsSynchronizer.ActivatePageLayout();
            }
        }

        #region 泡泡工具条之地图浏览
        private void bubbleBtnMapZoomIn_Click(object sender, DevComponents.DotNetBar.ClickEventArgs e)
        {
            ICommand pCommand = new ControlsMapZoomInToolClass();
            ITool pTool = pCommand as ITool;
            switch (this.tabControl.SelectedTabIndex)
            {
                case 0:
                    pCommand.OnCreate(this.mainMapControl.Object);
                    this.mainMapControl.CurrentTool = pTool;
                    break;
                case 1:
                    pCommand.OnCreate(this.axPageLayoutControl.Object);
                    this.axPageLayoutControl.CurrentTool = pTool;
                    break;
            }
        }

        private void bubbleBtnMapZoomOut_Click(object sender, DevComponents.DotNetBar.ClickEventArgs e)
        {
            ICommand pCommand = new ControlsMapZoomOutToolClass();
            ITool pTool = pCommand as ITool;
            switch (this.tabControl.SelectedTabIndex)
            {
                case 0:
                    pCommand.OnCreate(this.mainMapControl.Object);
                    this.mainMapControl.CurrentTool = pTool;
                    break;
                case 1:
                    pCommand.OnCreate(this.axPageLayoutControl.Object);
                    this.axPageLayoutControl.CurrentTool = pTool;
                    break;
            }
        }

        private void bubbleBtnPan_Click(object sender, DevComponents.DotNetBar.ClickEventArgs e)
        {
            ICommand pCommand = new ControlsMapPanToolClass();
            ITool pTool = pCommand as ITool;
            switch (this.tabControl.SelectedTabIndex)
            {
                case 0:
                    pCommand.OnCreate(this.mainMapControl.Object);
                    this.mainMapControl.CurrentTool = pTool;
                    break;
                case 1:
                    pCommand.OnCreate(this.axPageLayoutControl.Object);
                    this.axPageLayoutControl.CurrentTool = pTool;
                    break;
            }
        }

        private void bubbleBtnSelectFeature_Click(object sender, DevComponents.DotNetBar.ClickEventArgs e)
        {
            ICommand pCommand = new ControlsSelectFeaturesToolClass();
            ITool pTool = pCommand as ITool;
            switch (this.tabControl.SelectedTabIndex)
            {
                case 0:
                    pCommand.OnCreate(this.mainMapControl.Object);
                    this.mainMapControl.CurrentTool = pTool;
                    break;
                case 1:
                    pCommand.OnCreate(this.axPageLayoutControl.Object);
                    this.axPageLayoutControl.CurrentTool = pTool;
                    break;
            }
        }

        private void bubbleBtnSelectElement_Click(object sender, DevComponents.DotNetBar.ClickEventArgs e)
        {
            ICommand pCommand = new ControlsSelectToolClass();
            ITool pTool = pCommand as ITool;
            switch (this.tabControl.SelectedTabIndex)
            {
                case 0:
                    pCommand.OnCreate(this.mainMapControl.Object);
                    this.mainMapControl.CurrentTool = pTool;
                    break;
                case 1:
                    pCommand.OnCreate(this.axPageLayoutControl.Object);
                    this.axPageLayoutControl.CurrentTool = pTool;
                    break;
            }
        }

        private void bubbleBtnIdentify_Click(object sender, DevComponents.DotNetBar.ClickEventArgs e)
        {
            ICommand pCommand = new ControlsMapIdentifyToolClass();
            ITool pTool = pCommand as ITool;
            switch (this.tabControl.SelectedTabIndex)
            {
                case 0:
                    pCommand.OnCreate(this.mainMapControl.Object);
                    this.mainMapControl.CurrentTool = pTool;
                    break;
                case 1:
                    pCommand.OnCreate(this.axPageLayoutControl.Object);
                    this.axPageLayoutControl.CurrentTool = pTool;
                    break;
            }
        }

        private void bubbleBtnMeasure_Click(object sender, DevComponents.DotNetBar.ClickEventArgs e)
        {
            ICommand pCommand = new ControlsMapMeasureToolClass();
            ITool pTool = pCommand as ITool;
            switch (this.tabControl.SelectedTabIndex)
            {
                case 0:
                    pCommand.OnCreate(this.mainMapControl.Object);
                    this.mainMapControl.CurrentTool = pTool;
                    break;
                case 1:
                    pCommand.OnCreate(this.axPageLayoutControl.Object);
                    this.axPageLayoutControl.CurrentTool = pTool;
                    break;
            }
        }

        private void bubbleBtnFullExtent_Click(object sender, DevComponents.DotNetBar.ClickEventArgs e)
        {
            ICommand pCommand = new ControlsMapFullExtentCommandClass();
            pCommand.OnCreate(this.mainMapControl.Object);
            pCommand.OnClick();
        }

        private void bubbleBtnMapFixedZoomIn_Click(object sender, DevComponents.DotNetBar.ClickEventArgs e)
        {
            ICommand pCommand = new ControlsMapZoomInFixedCommandClass();
            pCommand.OnCreate(this.mainMapControl.Object);
            pCommand.OnClick();
        }

        private void bubbleBtnFixedZoomOut_Click(object sender, DevComponents.DotNetBar.ClickEventArgs e)
        {
            ICommand pCommand = new ControlsMapZoomOutFixedCommandClass();
            pCommand.OnCreate(this.mainMapControl.Object);
            pCommand.OnClick();
        }

        private void bubbleBtnPreExtent_Click(object sender, DevComponents.DotNetBar.ClickEventArgs e)
        {
            ICommand pCommand = new ControlsMapZoomToLastExtentBackCommandClass();
            pCommand.OnCreate(this.mainMapControl.Object);
            pCommand.OnClick();
        }

        private void bubbleBtnNextExtent_Click(object sender, DevComponents.DotNetBar.ClickEventArgs e)
        {
            ICommand pCommand = new ControlsMapZoomToLastExtentForwardCommandClass();
            pCommand.OnCreate(this.mainMapControl.Object);
            pCommand.OnClick();
        }

        private void bubbleBtnClearSelection_Click(object sender, DevComponents.DotNetBar.ClickEventArgs e)
        {
            ICommand pCommand = new ControlsClearSelectionCommandClass();
            pCommand.OnCreate(this.mainMapControl.Object);
            pCommand.OnClick();
        }

        private void bubbleBtnFind_Click(object sender, DevComponents.DotNetBar.ClickEventArgs e)
        {
            ICommand pCommand = new ControlsMapFindCommandClass();
            pCommand.OnCreate(this.mainMapControl.Object);
            pCommand.OnClick();
        }

        private void bubbleBtnGotoXY_Click(object sender, DevComponents.DotNetBar.ClickEventArgs e)
        {
            ICommand pCommand = new ControlsMapGoToCommandClass();
            pCommand.OnCreate(this.mainMapControl.Object);
            pCommand.OnClick();
        }


        #endregion 泡泡工具条之地图浏览

        private void btnNewDoc_Click(object sender, EventArgs e)
        {
            if (this.mainMapControl.LayerCount > 0)
            {
                DialogResult result = MessageBox.Show("是否保存当前地图？", "警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Cancel) return;
                if (result == DialogResult.Yes) this.btnSaveDoc_Click(null, null);
            }
            this.mainMapControl.Map = new MapClass();
            this.mainMapControl.DocumentFilename = "";
            this.m_controlsSynchronizer.ReplaceMap(this.mainMapControl.Map);
            this.Text = "LinGIS - LinInfo";
        }

        private void btnSaveDoc_Click(object sender, EventArgs e)
        {
            if (this.mainMapControl.DocumentFilename != "")
            {
                try
                {
                    this.pMapDocument = new MapDocumentClass();
                    this.pMapDocument.Open(this.mainMapControl.DocumentFilename,"");
                    this.pMapDocument.ReplaceContents((IMxdContents)this.mainMapControl.Map);
                    this.pMapDocument.ReplaceContents((IMxdContents)this.axPageLayoutControl.PageLayout);
                    this.pMapDocument.Save(true, false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(this.pMapDocument);
                    Application.DoEvents();
                    this.pMapDocument = null;
                }
            }
            else
            {
                this.btnSaveDocAs_Click(null, null);
            }
        }

        private void btnSaveDocAs_Click(object sender, EventArgs e)
        {
            this.saveFileDialog.Filter = "MXD地图文档(*.mxd)|*.mxd";
            if (this.saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.pMapDocument = new MapDocumentClass();
                    this.pMapDocument.New(this.saveFileDialog.FileName);
                    this.pMapDocument.ReplaceContents((IMxdContents)this.mainMapControl.Map);
                    this.pMapDocument.ReplaceContents((IMxdContents)this.axPageLayoutControl.PageLayout);
                    this.pMapDocument.Save(true, false);
                    this.mainMapControl.DocumentFilename = this.saveFileDialog.FileName;
                    this.Text = System.IO.Path.GetFileName(this.saveFileDialog.FileName) + " - " + "LinGIS - LinInfo";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(this.pMapDocument);
                    Application.DoEvents();
                    this.pMapDocument = null;
                }
            }
        }

        private void btnOpenDoc_Click(object sender, EventArgs e)
        {

            if (this.mainMapControl.LayerCount > 0)
            {
                DialogResult result = MessageBox.Show("是否保存当前地图？", "警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Cancel) return;
                if (result == DialogResult.Yes) this.btnSaveDoc_Click(null, null);
            }
            this.openFileDialog.Title = "请选择地图文件";
            this.openFileDialog.Filter = "MXD地图文件|*.mxd";
            this.openFileDialog.Multiselect = false;
            this.openFileDialog.RestoreDirectory = true;
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Application.DoEvents();
                string docName = this.openFileDialog.FileName;
                try
                {
                    this.pMapDocument = new MapDocumentClass();
                    if (pMapDocument.get_IsPresent(docName) && !pMapDocument.get_IsPasswordProtected(docName))
                    {
                        pMapDocument.Open(docName, null);
                        IMap pMap = pMapDocument.get_Map(0);
                        m_controlsSynchronizer.ReplaceMap(pMap);
                        this.mainMapControl.DocumentFilename = docName;
                        this.Text = System.IO.Path.GetFileName(this.openFileDialog.FileName) + " - " + "LinGIS - LinInfo";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(this.pMapDocument);
                    Application.DoEvents();
                    this.pMapDocument = null;
                }
            }
        }

        private void btnAddData_Click(object sender, EventArgs e)
        {
            int currentLayerCount = this.mainMapControl.LayerCount;
            
            ICommand pCommand = new ControlsAddDataCommandClass();
            pCommand.OnCreate(this.mainMapControl.Object);
            pCommand.OnClick();
            IMap pMap = this.mainMapControl.Map;
            this.m_controlsSynchronizer.ReplaceMap(pMap);
            if (this.mainMapControl.LayerCount > currentLayerCount)
            {
                this.EagleaxMapControl.AddLayer(this.mainMapControl.get_Layer(0));
                this.EagleaxMapControl.Extent = this.EagleaxMapControl.FullExtent;
                this.EagleaxMapControl.Refresh();
            }
        }

        private void bubbleBtnNewDoc_Click(object sender, DevComponents.DotNetBar.ClickEventArgs e)
        {
            this.btnNewDoc_Click(sender, e as EventArgs);
        }

        private void bubbleBtnOpenDoc_Click(object sender, DevComponents.DotNetBar.ClickEventArgs e)
        {
            this.btnOpenDoc_Click(sender, e as EventArgs);
        }

        private void bubbleBtnAddData_Click(object sender, DevComponents.DotNetBar.ClickEventArgs e)
        {
            this.btnAddData_Click(sender, e);
        }

        private void btnExitApplication_Click(object sender, EventArgs e)
        {
            if (this.mainMapControl.LayerCount > 0)
            {
                DialogResult result = MessageBox.Show("是否保存当前地图？", "警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Cancel) return;
                if (result == DialogResult.Yes) this.btnSaveDoc_Click(null, null);
            }
            Application.Exit();
        }

        #region 绘制元素工具条
        private void btnDrawSelectElement_Click(object sender, EventArgs e)
        {
            this.drawBarCheckOnClick(((DevComponents.DotNetBar.ButtonItem)sender).Name);
            ICommand pCommand = new ControlsSelectToolClass();
            ITool pTool = pCommand as ITool;
            switch (this.tabControl.SelectedTabIndex)
            {
                case 0:
                    pCommand.OnCreate(this.mainMapControl.Object);
                    this.mainMapControl.CurrentTool = pTool;
                    break;
                case 1:
                    pCommand.OnCreate(this.axPageLayoutControl.Object);
                    this.axPageLayoutControl.CurrentTool = pTool;
                    break;
            }
        }



        private void btnDrawNewRenctangle_Click(object sender, EventArgs e)
        {
            this.drawBarCheckOnClick(((DevComponents.DotNetBar.ButtonItem)sender).Name);
            ICommand pCommand = new ControlsNewRectangleToolClass();
            ITool pTool = pCommand as ITool;
            switch (this.tabControl.SelectedTabIndex)
            {
                case 0:
                    pCommand.OnCreate(this.mainMapControl.Object);
                    this.mainMapControl.CurrentTool = pTool;
                    break;
                case 1:
                    pCommand.OnCreate(this.axPageLayoutControl.Object);
                    this.axPageLayoutControl.CurrentTool = pTool;
                    break;
            }
        }

        private void btnDrawNewPolygon_Click(object sender, EventArgs e)
        {
            this.drawBarCheckOnClick(((DevComponents.DotNetBar.ButtonItem)sender).Name);
            ICommand pCommand = new ControlsNewPolygonToolClass();
            ITool pTool = pCommand as ITool;
            switch (this.tabControl.SelectedTabIndex)
            {
                case 0:
                    pCommand.OnCreate(this.mainMapControl.Object);
                    this.mainMapControl.CurrentTool = pTool;
                    break;
                case 1:
                    pCommand.OnCreate(this.axPageLayoutControl.Object);
                    this.axPageLayoutControl.CurrentTool = pTool;
                    break;
            }
        }

        private void btnDrawNewCircle_Click(object sender, EventArgs e)
        {
            this.drawBarCheckOnClick(((DevComponents.DotNetBar.ButtonItem)sender).Name);
            ICommand pCommand = new ControlsNewCircleToolClass();
            ITool pTool = pCommand as ITool;
            switch (this.tabControl.SelectedTabIndex)
            {
                case 0:
                    pCommand.OnCreate(this.mainMapControl.Object);
                    this.mainMapControl.CurrentTool = pTool;
                    break;
                case 1:
                    pCommand.OnCreate(this.axPageLayoutControl.Object);
                    this.axPageLayoutControl.CurrentTool = pTool;
                    break;
            }
        }

        private void btnDrawNewEllipse_Click(object sender, EventArgs e)
        {
            this.drawBarCheckOnClick(((DevComponents.DotNetBar.ButtonItem)sender).Name);
            ICommand pCommand = new ControlsNewEllipseToolClass();
            ITool pTool = pCommand as ITool;
            switch (this.tabControl.SelectedTabIndex)
            {
                case 0:
                    pCommand.OnCreate(this.mainMapControl.Object);
                    this.mainMapControl.CurrentTool = pTool;
                    break;
                case 1:
                    pCommand.OnCreate(this.axPageLayoutControl.Object);
                    this.axPageLayoutControl.CurrentTool = pTool;
                    break;
            }
        }

        private void btnDrawNewLine_Click(object sender, EventArgs e)
        {
            this.drawBarCheckOnClick(((DevComponents.DotNetBar.ButtonItem)sender).Name);
            ICommand pCommand = new ControlsNewLineToolClass();
            ITool pTool = pCommand as ITool;
            switch (this.tabControl.SelectedTabIndex)
            {
                case 0:
                    pCommand.OnCreate(this.mainMapControl.Object);
                    this.mainMapControl.CurrentTool = pTool;
                    break;
                case 1:
                    pCommand.OnCreate(this.axPageLayoutControl.Object);
                    this.axPageLayoutControl.CurrentTool = pTool;
                    break;
            }
        }

        private void btnDrawNewCurve_Click(object sender, EventArgs e)
        {
            this.drawBarCheckOnClick(((DevComponents.DotNetBar.ButtonItem)sender).Name);
            ICommand pCommand = new ControlsNewCurveToolClass();
            ITool pTool = pCommand as ITool;
            switch (this.tabControl.SelectedTabIndex)
            {
                case 0:
                    pCommand.OnCreate(this.mainMapControl.Object);
                    this.mainMapControl.CurrentTool = pTool;
                    break;
                case 1:
                    pCommand.OnCreate(this.axPageLayoutControl.Object);
                    this.axPageLayoutControl.CurrentTool = pTool;
                    break;
            }
        }

        private void btnDrawNewFreeHand_Click(object sender, EventArgs e)
        {
            this.drawBarCheckOnClick(((DevComponents.DotNetBar.ButtonItem)sender).Name);
            ICommand pCommand = new ControlsNewFreeHandToolClass();
            ITool pTool = pCommand as ITool;
            switch (this.tabControl.SelectedTabIndex)
            {
                case 0:
                    pCommand.OnCreate(this.mainMapControl.Object);
                    this.mainMapControl.CurrentTool = pTool;
                    break;
                case 1:
                    pCommand.OnCreate(this.axPageLayoutControl.Object);
                    this.axPageLayoutControl.CurrentTool = pTool;
                    break;
            }
        }

        private void btnDrawNewMarker_Click(object sender, EventArgs e)
        {
            this.drawBarCheckOnClick(((DevComponents.DotNetBar.ButtonItem)sender).Name);
            ICommand pCommand = new ControlsNewMarkerToolClass();
            ITool pTool = pCommand as ITool;
            switch (this.tabControl.SelectedTabIndex)
            {
                case 0:
                    pCommand.OnCreate(this.mainMapControl.Object);
                    this.mainMapControl.CurrentTool = pTool;
                    break;
                case 1:
                    pCommand.OnCreate(this.axPageLayoutControl.Object);
                    this.axPageLayoutControl.CurrentTool = pTool;
                    break;
            }
        }
        #endregion

        private void buttonItemSelectFeature_Click(object sender, EventArgs e)
        {
            ICommand pCommand = new ControlsSelectFeaturesToolClass();
            ITool pTool = pCommand as ITool;
            switch (this.tabControl.SelectedTabIndex)
            {
                case 0:
                    pCommand.OnCreate(this.mainMapControl.Object);
                    this.mainMapControl.CurrentTool = pTool;
                    break;
                case 1:
                    pCommand.OnCreate(this.axPageLayoutControl.Object);
                    this.axPageLayoutControl.CurrentTool = pTool;
                    break;
            }
        }
        
        private void btnSelectByAttr_Click(object sender, EventArgs e)
        {
            SelectByAttrFrm newSelectByAttrFrm = new SelectByAttrFrm(this);
            newSelectByAttrFrm.Show((IWin32Window)this);
        }



        private IPoint mapRightClickPoint;
        private IPoint mapTextPoint;
        private void mainMapControl_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (e.button == 2)
            {
                if (this.mapRightClickPoint == null)
                {
                    this.mapRightClickPoint = new PointClass();
                }
                this.mapRightClickPoint.PutCoords(e.mapX, e.mapY);
                IGraphicsContainer pGraphicContainer = this.mainMapControl.Map as IGraphicsContainer;
                IEnumElement pEnumElement = pGraphicContainer.LocateElements(this.mapRightClickPoint, this.mainMapControl.ActiveView.Extent.Width / 500);
                if (pEnumElement != null)
                {
                    return;
                }
                else
                {
                    this.contextMenuMapNormal.Show(this.mainMapControl as Control, new System.Drawing.Point(e.x, e.y));
                }
            }
            else if (e.button == 1)
            {
                if (this.btnDrawNewText.Checked == false) return;
                
                if (this.mapTextPoint == null)
                {
                    this.mapTextPoint = new PointClass();
                }
                this.mapTextPoint.PutCoords(e.mapX, e.mapY);
                this.txtNewText.Location = new System.Drawing.Point(e.x, e.y);
                this.txtNewText.Text = "文本";
                this.txtNewText.Visible = true;
                this.txtNewText.Focus();
                this.txtNewText.SelectAll();
                this.btnDrawNewText.Checked = false;
            }
        }

        #region MapControl普通右键菜单
        //全图
        private void ctMenuMNFullExtent_Click(object sender, EventArgs e)
        {
            ICommand pCommand = new ControlsMapFullExtentCommandClass();
            pCommand.OnCreate(this.mainMapControl.Object);
            pCommand.OnClick();
        }
        //前一视图
        private void ctMenuMNPreviousExtent_Click(object sender, EventArgs e)
        {
            ICommand pCommand = new ControlsMapZoomToLastExtentBackCommandClass();
            pCommand.OnCreate(this.mainMapControl.Object);
            pCommand.OnClick();
        }
        //下一视图
        private void ctMenuMNNextExtent_Click(object sender, EventArgs e)
        {
            ICommand pCommand = new ControlsMapZoomToLastExtentForwardCommandClass();
            pCommand.OnCreate(this.mainMapControl.Object);
            pCommand.OnClick();
        }
        //局部放大
        private void ctMenuMNFixedZoomIn_Click(object sender, EventArgs e)
        {
            ICommand pCommand = new ControlsMapZoomInFixedCommandClass();
            pCommand.OnCreate(this.mainMapControl.Object);
            pCommand.OnClick();
        }
        //局部缩小
        private void ctMenuMNFixedZoomOut_Click(object sender, EventArgs e)
        {
            ICommand pCommand = new ControlsMapZoomOutFixedCommandClass();
            pCommand.OnCreate(this.mainMapControl.Object);
            pCommand.OnClick();
        }
        //选择要素
        private void ctMenuMNSelectFeature_Click(object sender, EventArgs e)
        {
            IMap pMap = this.mainMapControl.Map;
            ISelectionEnvironment pSelectionEnvironment = new SelectionEnvironmentClass();
            pSelectionEnvironment.LinearSearchDistance = this.mainMapControl.ActiveView.Extent.Width / 200;
            pSelectionEnvironment.PointSearchDistance = this.mainMapControl.ActiveView.Extent.Width / 200;
            pMap.SelectByShape(this.mapRightClickPoint as IGeometry, null, false);
            this.mainMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
        }
        //识别
        private void ctMenuMNFIdentify_Click(object sender, EventArgs e)
        {

        }
        
        //居中
        private void ctMenuMNCenter_Click(object sender, EventArgs e)
        {
            this.mainMapControl.CenterAt(this.mapRightClickPoint);
        }
        #endregion MapControl普通右键菜单

        private void bubbleBtnAddCAD_Click(object sender, DevComponents.DotNetBar.ClickEventArgs e)
        {
            this.openFileDialog.Filter = "DWG文件|*.dwg";
            this.openFileDialog.Title = "请选择CAD文件";
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string dirction = System.IO.Path.GetDirectoryName(this.openFileDialog.FileName);
                //MessageBox.Show(System.IO.Path.GetDirectoryName(this.openFileDialog.FileName) + "\n" +
                //                System.IO.Path.GetFileName(this.openFileDialog.FileName) + "\n" +
                //                System.IO.Path.GetFullPath(this.openFileDialog.FileName));
                string file = System.IO.Path.GetFileName(this.openFileDialog.FileName);

                IWorkspaceFactory pWorkspaceFatory = new CadWorkspaceFactoryClass();
                IWorkspace pWorkspace = pWorkspaceFatory.OpenFromFile(dirction, 0);
                IFeatureWorkspace pFeatureWorkspace = pWorkspace as IFeatureWorkspace;
                IFeatureDataset pFeatureDataset = pFeatureWorkspace.OpenFeatureDataset(file);
                IFeatureClassContainer pFeatureClassContainer = pFeatureDataset as IFeatureClassContainer;
                IFeatureClass pFeatureClass;
                IFeatureLayer pFeatureLayer;
                //for (int i = 0; i < pFeatureClassContainer.ClassCount; i++)
                //{
                //    pFeatureClass = pFeatureClassContainer.get_ClassByID(i);
                //    if (pFeatureClass.FeatureType == esriFeatureType.esriFTAnnotation)
                //    {
                //        pFeatureLayer = new CadAnnotationLayerClass();
                //    }
                //    else
                //    {
                //        pFeatureLayer = new FeatureLayerClass();
                //    }
                //    pFeatureLayer.Name = pFeatureClass.AliasName;
                //    pFeatureLayer.FeatureClass = pFeatureClass;
                //    this.mainMapControl.AddLayer(pFeatureLayer as ILayer);
                //    this.EagleaxMapControl.AddLayer(pFeatureLayer as ILayer);
                //}
                IEnumFeatureClass pEnumFeatureClass = pFeatureClassContainer.Classes;
                pEnumFeatureClass.Reset();
                for (pFeatureClass = pEnumFeatureClass.Next(); pFeatureClass != null; pFeatureClass = pEnumFeatureClass.Next())
                {
                    if (pFeatureClass.FeatureType == esriFeatureType.esriFTAnnotation)
                    {
                        pFeatureLayer = new CadAnnotationLayerClass();
                    }
                    else
                    {
                        pFeatureLayer = new FeatureLayerClass();
                    }
                    pFeatureLayer.Name = pFeatureClass.AliasName;
                    pFeatureLayer.FeatureClass = pFeatureClass;
                    this.mainMapControl.AddLayer(pFeatureLayer as ILayer);
                    this.EagleaxMapControl.AddLayer(pFeatureLayer as ILayer);
                }

                this.m_controlsSynchronizer.ReplaceMap(this.mainMapControl.Map);
                this.EagleaxMapControl.Extent = this.EagleaxMapControl.FullExtent;
                this.EagleaxMapControl.Refresh();
            }
        }

        private void ctMenuTFRemove_Click(object sender, EventArgs e)
        {
            this.mainMapControl.Map.DeleteLayer(this.TOCRightLayer);
            this.m_controlsSynchronizer.ReplaceMap(this.mainMapControl.Map);
            this.EagleaxMapControl.Map.DeleteLayer(this.TOCRightLayer);
            this.EagleaxMapControl.Extent = this.EagleaxMapControl.FullExtent;
            this.EagleaxMapControl.Refresh();
        }

        private void ctMenuTFZoomToLayer_Click(object sender, EventArgs e)
        {
            IFeatureClass pFeatureClass = ((IFeatureLayer)this.TOCRightLayer).FeatureClass;
            IGeoDataset pGeoDataset = pFeatureClass as IGeoDataset;
            this.mainMapControl.Extent = pGeoDataset.Extent;
            this.mainMapControl.Refresh();
        }

        private void ctMenuTFProperties_Click(object sender, EventArgs e)
        {
            if (this.newFeatLyrFrm == null)
            {
                this.newFeatLyrFrm = new FeatLyrFrm();
            }
            this.newFeatLyrFrm.Layer = this.TOCRightLayer;
            if (this.newFeatLyrFrm.ShowDialog() == DialogResult.OK)
            {
                this.mainMapControl.Refresh();
                this.EagleaxMapControl.Refresh();
                this.axTOCControl.Update();
            }
        }

        private void contextMenuTOCFeatureLyr_Opening(object sender, CancelEventArgs e)
        {
            if (this.TOCRightLayer is IGeoFeatureLayer)
            {
                this.ctMenuTFProperties.Enabled = true;
            }
            else
            {
                this.ctMenuTFProperties.Enabled = false;
            }
        }

        //private void buttonItemSelectFeature_Click(object sender, EventArgs e)
        //{
        //    ICommand pCommand = new ControlsSelectToolClass();
        //    ITool pTool = pCommand as ITool;
        //    switch (this.tabControl.SelectedTabIndex)
        //    {
        //        case 0:
        //            pCommand.OnCreate(this.mainMapControl.Object);
        //            this.mainMapControl.CurrentTool = pTool;
        //            break;
        //        case 1:
        //            pCommand.OnCreate(this.axPageLayoutControl.Object);
        //            this.axPageLayoutControl.CurrentTool = pTool;
        //            break;
        //    }
        //}

        private void btnClearSelction_Click(object sender, EventArgs e)
        {
            this.mainMapControl.Map.ClearSelection();
            this.mainMapControl.Refresh();
            this.EagleaxMapControl.Refresh();
            
        }

        private void ctMenuSaveAsLyr_Click(object sender, EventArgs e)
        {
            this.saveFileDialog.Title = "保存为Lyr文件";
            this.saveFileDialog.Filter = "Lyr文件|*.lyr";
            if (this.saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string direction = System.IO.Path.GetDirectoryName(this.saveFileDialog.FileName);
                string file = System.IO.Path.GetFileName(this.saveFileDialog.FileName);
                ILayerFile pLayerFile = new LayerFileClass();
                pLayerFile.New(this.saveFileDialog.FileName);
                pLayerFile.ReplaceContents(this.TOCRightLayer);
                pLayerFile.Save();
            }
        }

        private void ctMenwMapCollapseAll_Click(object sender, EventArgs e)
        {
            IEnumLayer pEnumLayer = this.mainMapControl.Map.get_Layers(null, false);
            if (pEnumLayer == null) return;
            ILayer pLayer;
            ILegendInfo pLengendInfo;
            ILegendGroup pLengendGroup;
            pEnumLayer.Reset();
            for (pLayer = pEnumLayer.Next(); pLayer != null; pLayer = pEnumLayer.Next())
            {
                if (pLayer is ILegendInfo)
                {
                    pLengendInfo = pLayer as ILegendInfo;
                    for (int i = 0; i < pLengendInfo.LegendGroupCount; i++)
                    {
                        pLengendGroup = pLengendInfo.get_LegendGroup(i);
                        pLengendGroup.Visible = false;
                    }
                }
            }
            this.axTOCControl.Update();
        }

        private void ctMenuMapExpandAll_Click(object sender, EventArgs e)
        {
            IEnumLayer pEnumLayer = this.mainMapControl.Map.get_Layers(null, false);
            if (pEnumLayer == null) return;
            ILayer pLayer;
            ILegendInfo pLengendInfo;
            ILegendGroup pLengendGroup;
            pEnumLayer.Reset();
            for (pLayer = pEnumLayer.Next(); pLayer != null; pLayer = pEnumLayer.Next())
            {
                if (pLayer is ILegendInfo)
                {
                    pLengendInfo = pLayer as ILegendInfo;
                    for (int i = 0; i < pLengendInfo.LegendGroupCount; i++)
                    {
                        pLengendGroup = pLengendInfo.get_LegendGroup(i);
                        pLengendGroup.Visible = true;
                    }
                }
            }
            this.axTOCControl.Update();
        }

        private void ctMenuMapTurnOnAll_Click(object sender, EventArgs e)
        {
            IEnumLayer pEnumLayer = this.mainMapControl.Map.get_Layers(null, false);
            if (pEnumLayer == null) return;
            ILayer pLayer;
            pEnumLayer.Reset();
            for (pLayer = pEnumLayer.Next(); pLayer != null; pLayer = pEnumLayer.Next())
            {
                pLayer.Visible = true;
            }
            this.mainMapControl.Refresh();
            this.axTOCControl.Update();
        }

        private void ctMenuMapTurnOffAll_Click(object sender, EventArgs e)
        {
            IEnumLayer pEnumLayer = this.mainMapControl.Map.get_Layers(null, false);
            if (pEnumLayer == null) return;
            ILayer pLayer;
            pEnumLayer.Reset();
            for (pLayer = pEnumLayer.Next(); pLayer != null; pLayer = pEnumLayer.Next())
            {
                pLayer.Visible = false;
            }
            this.mainMapControl.Refresh();
            this.axTOCControl.Update();
        }

        private void btnDrawNewText_Click(object sender, EventArgs e)
        {
            this.drawBarCheckOnClick(((DevComponents.DotNetBar.ButtonItem)sender).Name);
            this.mainMapControl.CurrentTool = null;
        }

        private void drawBarCheckOnClick(string name)
        {
            foreach (DevComponents.DotNetBar.ButtonItem pButtonItem in this.barDraw.Items)
            {
                pButtonItem.Checked = false;
            }
            ((DevComponents.DotNetBar.ButtonItem)this.barDraw.Items[name]).Checked = true;
        }

        private void txtNewText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == 27)
            {
                ITextElement pTextElement = new TextElementClass();
                pTextElement.Text = this.txtNewText.Text;
                IElement pElement = pTextElement as IElement;
                pElement.Geometry = this.mapTextPoint;
                IGraphicsContainer pGraphicsContainer = this.mainMapControl.Map as IGraphicsContainer;
                pGraphicsContainer.AddElement(pElement, 0);
                this.mainMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                this.txtNewText.Visible = false;
            }
        }

        private void txtNewText_Leave(object sender, EventArgs e)
        {
            ITextElement pTextElement = new TextElementClass();
            pTextElement.Text = this.txtNewText.Text;
            IElement pElement = pTextElement as IElement;
            pElement.Geometry = this.mapTextPoint;
            IGraphicsContainer pGraphicsContainer = this.mainMapControl.Map as IGraphicsContainer;
            pGraphicsContainer.AddElement(pElement, 0);
            this.mainMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            this.txtNewText.Visible = false;
        }

        private void mainMapControl_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            this.EagleaxMapControl.Map = new MapClass();
            for (int i = 1; i <= this.mainMapControl.LayerCount; i++)
            {
                this.EagleaxMapControl.AddLayer(this.mainMapControl.get_Layer(this.mainMapControl.LayerCount - i));
            }
            this.EagleaxMapControl.Extent = this.mainMapControl.FullExtent;
            this.EagleaxMapControl.Refresh();
        }

        private void btnCloseDoc_Click(object sender, EventArgs e)
        {
            if (this.mainMapControl.LayerCount > 0)
            {
                DialogResult result = MessageBox.Show("是否保存当前地图？", "警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Cancel) return;
                if (result == DialogResult.Yes) this.btnSaveDoc_Click(null, null);
            }
            this.mainMapControl.Map = new MapClass();
            this.mainMapControl.DocumentFilename = "";
            this.m_controlsSynchronizer.ReplaceMap(this.mainMapControl.Map);
            this.Text = "LinGIS - LinInfo";
        }

        private void bubbleBtnSaveDoc_Click(object sender, DevComponents.DotNetBar.ClickEventArgs e)
        {
            this.btnSaveDoc_Click(null, null);
        }

        private void bubbleBtnSaveDocAs_Click(object sender, DevComponents.DotNetBar.ClickEventArgs e)
        {
            this.btnSaveDocAs_Click(null, null);
        }

        private void bubbleBtnCloseDoc_Click(object sender, DevComponents.DotNetBar.ClickEventArgs e)
        {
            this.btnCloseDoc_Click(null, null);
        }
        //000000000000
        private void btnAddLyr_Click(object sender, EventArgs e)
        {
            this.openFileDialog.Filter = "图层文件(*.lyr)|*.lyr";
            this.openFileDialog.Title = "请选择图层文件";
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Application.DoEvents();
                try
                {
                    this.mainMapControl.AddLayerFromFile(this.openFileDialog.FileName);
                    this.EagleaxMapControl.AddLayer(this.mainMapControl.get_Layer(0));
                    this.EagleaxMapControl.Extent = this.mainMapControl.FullExtent;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            } 
        }

        private void btnAddShp_Click(object sender, EventArgs e)
        {
            this.openFileDialog.Filter = "图层文件(*.shp)|*.shp";
            this.openFileDialog.Title = "请选择Shape文件";
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Application.DoEvents();
                try
                {
                    this.mainMapControl.AddShapeFile(System.IO.Path.GetDirectoryName(this.openFileDialog.FileName), System.IO.Path.GetFileName(this.openFileDialog.FileName));
                    this.EagleaxMapControl.AddLayer(this.mainMapControl.get_Layer(0));
                    this.EagleaxMapControl.Extent = this.mainMapControl.FullExtent;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            } 

        }

        private void btnAddCadAsFeat_Click(object sender, EventArgs e)
        {
            this.openFileDialog.Filter = "CAD文件(*.dwg)|*.dwg";
            this.openFileDialog.Title = "请选择CAD文件";
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Application.DoEvents();
                try
                {
                    IWorkspaceFactory pCADWorksapceFactory;
                    pCADWorksapceFactory = new CadWorkspaceFactoryClass();
                    IFeatureWorkspace pFeatureWorkspace;
                    pFeatureWorkspace = pCADWorksapceFactory.OpenFromFile(System.IO.Path.GetDirectoryName(this.openFileDialog.FileName), 0) as IFeatureWorkspace;
                    IFeatureDataset pFeatureDataset;
                    pFeatureDataset = pFeatureWorkspace.OpenFeatureDataset(System.IO.Path.GetFileName(this.openFileDialog.FileName));
                    IFeatureClassContainer pFeatureClassContainer;
                    pFeatureClassContainer = pFeatureDataset as IFeatureClassContainer;
                    IFeatureClass pFeatureClass;
                    IFeatureLayer pFeatureLayer;
                    int i;
                    for (i = 0; i <= pFeatureClassContainer.ClassCount - 1; i++)
                    {
                        pFeatureClass = pFeatureClassContainer.get_Class(i);
                        if (pFeatureClass.FeatureType == esriFeatureType.esriFTCoverageAnnotation)
                        {
                            pFeatureLayer = new CadAnnotationLayerClass();
                        }
                        else
                        {
                            pFeatureLayer = new FeatureLayerClass();
                        }
                        pFeatureLayer.Name = pFeatureClass.AliasName;
                        pFeatureLayer.FeatureClass = pFeatureClass;
                        this.mainMapControl.AddLayer(pFeatureLayer);
                        this.EagleaxMapControl.AddLayer(pFeatureLayer);
                    }
                    this.EagleaxMapControl.Extent = this.mainMapControl.FullExtent;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void btnAddCadAsDraw_Click(object sender, EventArgs e)
        {
            this.openFileDialog.Filter = "CAD文件(*.dwg)|*.dwg";
            this.openFileDialog.Title = "请选择CAD文件";
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Application.DoEvents();
                try
                {
                    IWorkspaceFactory pCADWorksapceFactory;
                    pCADWorksapceFactory = new CadWorkspaceFactoryClass();
                    ICadDrawingWorkspace pCadDrawingWorkspace;
                    pCadDrawingWorkspace = pCADWorksapceFactory.OpenFromFile(System.IO.Path.GetDirectoryName(this.openFileDialog.FileName), 0) as ICadDrawingWorkspace;
                    ICadDrawingDataset pCadDrawingDataset;
                    pCadDrawingDataset = pCadDrawingWorkspace.OpenCadDrawingDataset(System.IO.Path.GetFileName(this.openFileDialog.FileName));
                    ICadLayer pCadLayer;
                    pCadLayer = new CadLayerClass();
                    pCadLayer.CadDrawingDataset = pCadDrawingDataset;
                    pCadLayer.Name = System.IO.Path.GetFileName(this.openFileDialog.FileName);
                    this.mainMapControl.AddLayer(pCadLayer);
                    this.EagleaxMapControl.AddLayer(pCadLayer);
                    this.EagleaxMapControl.Extent = this.mainMapControl.FullExtent;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            } 

        }

        private void btnDisplayFeeback_Click(object sender, EventArgs e)
        {
            LinGIS.DisplayFeedbackFrm newDisplayFeedbackFrm = new LinGIS.DisplayFeedbackFrm();
            newDisplayFeedbackFrm.Show();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            AboutFrm newAboutFrm = new AboutFrm();
            newAboutFrm.ShowDialog();
        }

        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.mainMapControl.LayerCount > 0)
            {
                DialogResult result = MessageBox.Show("是否保存当前地图？", "警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                if (result == DialogResult.Yes) this.btnSaveDoc_Click(null, null);
            }
        }

    }
}