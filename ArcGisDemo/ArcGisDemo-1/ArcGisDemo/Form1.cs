using System;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.GlobeCore;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.CartoUI;
using ESRI.ArcGIS.LocationUI;
using ESRI.ArcGIS.Framework;

namespace ArcGisDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            Initialize();
        }

        #region 图层画点
        private void Initialize()
        {
            string sFilePath;
            sFilePath = @"E:\Downloads\ARCgis\United States\USA Base Map.mxd";
            OpenDocument(sFilePath);
        }

        private void OpenDocument(string sFilePath)
        {
            if (axMapControl.CheckMxFile(sFilePath))
                axMapControl.LoadMxFile(sFilePath, Type.Missing, Type.Missing);

            //IMap pMap = axMapControl.Map;
            //GetProject2(-102, 44);
            //AddNewGraphicsLayer();
        }

        //画点
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != string.Empty && textBox4.Text != string.Empty)
            {
                //GetProject2(Convert.ToDouble(textBox3.Text), Convert.ToDouble(textBox4.Text));
            }
            //CreateLines();
            AddPointByBuffer(0);
        }

        /// 将经纬度点转换为平面坐标。
        //private IPoint GetProject2(double x, double y)
        //{
        //    //地理坐标转换投影坐标
        //    ISpatialReferenceFactory pfactory = new SpatialReferenceEnvironmentClass();
        //    IProjectedCoordinateSystem flatref = pfactory.CreateProjectedCoordinateSystem(102003);
        //    IGeographicCoordinateSystem earthref = pfactory.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_NAD1983);
        //    IPoint pt = new PointClass();//你画的图形式什么就是什么，特别的是LINE则需要定义为POLYLINE

        //    pt.PutCoords(x, y);

        //    IGeometry geo = (IGeometry)pt;
        //    geo.SpatialReference = earthref;
        //    geo.Project(flatref);

        //    //开始画点
        //    IMarkerElement pMarkerElement;//对于点，线，面的element定义这里都不一样，他是可实例化的类，而IElement是实例化的类，必须通过 IMarkerElement 初始化负值给 IElement 。
        //    IElement pMElement;
        //    pMarkerElement = new MarkerElementClass();

        //    pMElement = pMarkerElement as IElement;
            
        //    pMElement.Geometry = pt;//把你在屏幕中画好的图形付给 IElement 储存
        //    IGraphicsContainer pGraphicsContainer = axMapControl.ActiveView as IGraphicsContainer;//把地图的当前view作为图片的容器

        //    pGraphicsContainer.AddElement(pMElement, 0);//显示储存在 IElement 中图形，这样就持久化了。
            
        //    axMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        //    return pt;
        //}
        private IPoint GetProject2(double x, double y)
        {
            ISpatialReferenceFactory pfactory = new SpatialReferenceEnvironmentClass();
            IProjectedCoordinateSystem flatref = pfactory.CreateProjectedCoordinateSystem(102003);
            IGeographicCoordinateSystem earthref = pfactory.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_NAD1983);
            IPoint pt = new PointClass();
            pt.PutCoords(x, y);
            IGeometry geo = (IGeometry)pt;
            geo.SpatialReference = earthref;
            geo.Project(flatref);
            return pt;
        }
        #endregion

        #region 方块标记
        public void CreateTextElment(double x, double y)
        {
            IPoint pPoint = new PointClass();
            IMap pMap = axMapControl.Map;
            IActiveView pActiveView = pMap as IActiveView;
            IGraphicsContainer pGraphicsContainer;
            IElement pElement = new MarkerElementClass();
            IElement pTElement = new TextElementClass();
            pGraphicsContainer = (IGraphicsContainer)pActiveView;
            IFormattedTextSymbol pTextSymbol = new TextSymbolClass();
            IBalloonCallout pBalloonCallout = CreateBalloonCallout(x, y);
            IRgbColor pColor = new RgbColorClass();
            pColor.Red = 150;
            pColor.Green = 0;
            pColor.Blue = 0;
            pTextSymbol.Color = pColor;
            ITextBackground pTextBackground;
            pTextBackground = (ITextBackground)pBalloonCallout;
            pTextSymbol.Background = pTextBackground;
            ((ITextElement)pTElement).Symbol = pTextSymbol;
            ((ITextElement)pTElement).Text = "测试";
            pPoint.X = x + 42;
            pPoint.Y = y + 42;

            pTElement.Geometry = pPoint;
            pGraphicsContainer.AddElement(pTElement, 1);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        public IBalloonCallout CreateBalloonCallout(double x, double y)
        {
            IRgbColor pRgbClr = new RgbColorClass();
            pRgbClr.Red = 255;
            pRgbClr.Blue = 255;
            pRgbClr.Green = 255;
            ISimpleFillSymbol pSmplFill = new SimpleFillSymbolClass();
            pSmplFill.Color = pRgbClr;
            pSmplFill.Style = esriSimpleFillStyle.esriSFSSolid;
            IBalloonCallout pBllnCallout = new BalloonCalloutClass();
            pBllnCallout.Style = esriBalloonCalloutStyle.esriBCSRectangle;
            pBllnCallout.Symbol = pSmplFill;
            pBllnCallout.LeaderTolerance = 1;
            IPoint pPoint = new ESRI.ArcGIS.Geometry.PointClass();
            pPoint.X = x;
            pPoint.Y = y;
            pBllnCallout.AnchorPoint = pPoint;
            return pBllnCallout;
        }
        #endregion

        #region 各种标记
        public static void FlashFeature(AxMapControl mapControl, IMap iMap)
        //public static void FlashFeature(AxMapControl mapControl, IFeature iFeature, IMap iMap)
        {
            IActiveView iActiveView = iMap as IActiveView;
            if (iActiveView != null)
            {
                iActiveView.ScreenDisplay.StartDrawing(0, (short)esriScreenCache.esriNoScreenCache);
                //根据几何类型调用不同的过程
                //switch (iFeature.Shape.GeometryType)
                //{
                //    case esriGeometryType.esriGeometryPolyline:
                //        FlashLine(mapControl, iActiveView.ScreenDisplay, iFeature.Shape);
                //        break;
                //    case esriGeometryType.esriGeometryPolygon:
                //        FlashPolygon(mapControl, iActiveView.ScreenDisplay, iFeature.Shape);
                //        break;
                //    case esriGeometryType.esriGeometryPoint:
                //        FlashPoint(mapControl, iActiveView.ScreenDisplay, iFeature.Shape);
                //        break;
                //    default:
                //        break;
                //}
                iActiveView.ScreenDisplay.FinishDrawing();
            }
        }
        //闪烁线
        static void FlashLine(AxMapControl mapControl, IScreenDisplay iScreenDisplay, IGeometry iGeometry)
        {
            ISimpleLineSymbol iLineSymbol;
            ISymbol iSymbol;
            IRgbColor iRgbColor;
            iLineSymbol = new SimpleLineSymbol();
            iLineSymbol.Width = 4;
            iRgbColor = new RgbColor();
            iRgbColor.Red = 255;
            iLineSymbol.Color = iRgbColor;
            iSymbol = (ISymbol)iLineSymbol;
            iSymbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
            mapControl.FlashShape(iGeometry, 3, 200, iSymbol);
        }
        //闪烁面
        static void FlashPolygon(AxMapControl mapControl, IScreenDisplay iScreenDisplay, IGeometry iGeometry)
        {
            ISimpleFillSymbol iFillSymbol;
            ISymbol iSymbol;
            IRgbColor iRgbColor;
            iFillSymbol = new SimpleFillSymbol();
            iFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
            iFillSymbol.Outline.Width = 12;
            iRgbColor = new RgbColor();
            iRgbColor.RGB = System.Drawing.Color.FromArgb(100, 180, 180).ToArgb();
            iFillSymbol.Color = iRgbColor;
            iSymbol = (ISymbol)iFillSymbol;
            iSymbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
            iScreenDisplay.SetSymbol(iSymbol);
            mapControl.FlashShape(iGeometry, 3, 200, iSymbol);
        }
        //闪烁点
        static void FlashPoint(AxMapControl mapControl, IScreenDisplay iScreenDisplay, IGeometry iGeometry)
        {
            
            ISimpleMarkerSymbol iMarkerSymbol;
            ISymbol iSymbol;
            IRgbColor iRgbColor;
            iMarkerSymbol = new SimpleMarkerSymbol();
            iMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
            iRgbColor = new RgbColor();
            iRgbColor.RGB = System.Drawing.Color.FromArgb(0, 0, 0).ToArgb();
            iMarkerSymbol.Color = iRgbColor;
            iSymbol = (ISymbol)iMarkerSymbol;
            iSymbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
            mapControl.FlashShape(iGeometry, 3, 200, iSymbol);
        }
        #endregion

        #region 按钮代码
        //定位
        private void button1_Click(object sender, EventArgs e)
        {

            //ESRI.ArcGIS.esriSystem.IUnitConverter unitConverter = (ESRI.ArcGIS.esriSystem.IUnitConverter)ServerContext.CreateObject("esriSystem.UnitConverter");
            

            //project raster
            //IUnitConverter iu = new esriUnits();
           
            IUnitConverter unitConverter = new UnitConverterClass();
            double dFittedBoundsWidthInches = unitConverter.ConvertUnits(-100, esriUnits.esriDecimalDegrees, esriUnits.esriMeters)/100;
            double dFittedBoundsWidthInchesy = unitConverter.ConvertUnits(40, esriUnits.esriDecimalDegrees, esriUnits.esriMeters)/100;
            //ui.ConvertUnits(10, esriUnits.esriMeters, esriUnits.esriMeters);
            //IPoint pPoint = new PointClass();
            //pPoint.X = -1000;
            //pPoint.Y = -1000;

            //IGeometry pGeometry;
            //pGeometry = (IGeometry)pPoint;

            //IMarkerSymbol pMarkerSymbol = new SimpleMarkerSymbolClass();

            //axMapControl.FlashShape(pGeometry, 4, 100, pMarkerSymbol);

            
            
            IEnvelope pEnvelope = new EnvelopeClass();
            IPoint pPoint = new PointClass();
            
            //axMapControl.MapUnits = esriUnits.esriDecimalDegrees;
            //axMapControl.MapUnits = esriUnits.esriMeters; //米

            
           // esriUnits mapUnits = axMapControl.MapUnits; 


            //axMapControl.MapUnits = esriUnits.esriDecimalDegrees;
            //pPoint.PutCoords(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text));//该句中的txtlong，txtlat是输入经纬值的TextBox
            pPoint.X = Convert.ToDouble(textBox1.Text);
            pPoint.Y = Convert.ToDouble(textBox2.Text);
            pEnvelope = axMapControl.Extent;
            pEnvelope.CenterAt(pPoint);
            
            axMapControl.Extent = pEnvelope.Envelope;//前一段代码实现平移
            axMapControl.ActiveView.ScreenDisplay.UpdateWindow();
            axMapControl.FlashShape(pPoint as IGeometry);//此句实现闪烁（效果没有ArcMap中好， ）   
            //axMapControl.ActiveView.Extent = pEnvelope;
            //showAnnotationByScale();
            
        }
        public void points()
        {
            string strs = "Marker";
            switch (strs)
            {
                case "Marker":
                    IMarkerElement pMarkerElement;//对于点，线，面的element定义这里都不一样，他是可实例化的类，而IElement是实例化的类，必须通过 IMarkerElement 初始化负值给 IElement 。
                    IElement pMElement;
                    IPoint pPoint=new PointClass();//你画的图形式什么就是什么，特别的是LINE则需要定义为POLYLINE
                    pMarkerElement = new MarkerElementClass();

                    pMElement = pMarkerElement as IElement;

                    //RubberPointClass pRubberBand = new RubberPointClass();//你的RUBBERBAND随着你的图形耳边
                    //pPoint = pRubberBand.TrackNew(axMapControl.ActiveView.ScreenDisplay, null) as IPoint;
                    ////pPoint = pRubberBand.TrackNew(axMapControl.ActiveView.ScreenDisplay, null) as IPoint;
                    //pPoint.X = 101;
                    //pPoint.Y = 37;

                    // pPoint.PutCoords(Convert.ToDouble(101), Convert.ToDouble(37));
                    //pPoint = axMapControl.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(200, 200);
                    // pPoint.PutCoords(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text));

                   // pPoint = axMapControl.ToMapPoint(334, 344);
                    //pPoint.X = pPoint.X;
                    //pPoint.Y = pPoint.Y;

                    /*00000000000000000000000000000000000经纬度转换坐标*/
                    //IGlobeDisplay m_globeDisplay = axGlobeControl1.GlobeDisplay;

                    //// IGlobeDisplay pGlobeDisplay = axGlobeControl1.GlobeDisplay;

                    ////axGlobeControl1.GlobeDisplay.ActiveViewer;
                    //ISceneViewer sceneViewer = m_globeDisplay.ActiveViewer;
                    //IGlobeViewUtil globeViewUtil = (IGlobeViewUtil)sceneViewer.Camera;

                    //int winX, winY;
                    ////globeViewUtil.GeographicToWindow(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text), 0, out winX, out winY);

                   

                    ////pPoint.PutCoords(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text));//x,y地理坐标
                    
                    //pPoint=axMapControl.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(0, 0);//x,y为屏幕坐标
                    ////pPoint.X = Convert.ToDouble(textBox1.Text);
                    ////pPoint.Y = Convert.ToDouble(textBox2.Text);
                    //pPoint.PutCoords(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text));//x,y地理坐标

                    //axMapControl.ActiveView.ScreenDisplay.DisplayTransformation.FromMapPoint(pPoint, out winX, out winY);
                    // int mx = winX;
                    //int my = winY;
                    //pPoint=axMapControl.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(200, 400);//x,y为屏幕坐标
                    pPoint.PutCoords(Convert.ToDouble(textBox3.Text), Convert.ToDouble(textBox4.Text));
                    /*00000000000000000000000000000000000*/

                   // pPoint = axMapControl.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(mx, my);

                    //pPoint.PutCoords(mx, my);
                    //pPoint.X = mx;
                    //pPoint.Y = my;

                    //ISpatialReferenceFactory pSRF = new SpatialReferenceEnvironmentClass();
                    //pPoint.SpatialReference = pSRF.CreateProjectedCoordinateSystem(2414);
                    //pPoint.Project(pSRF.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_Beijing1954));


                    pMElement.Geometry = pPoint;//把你在屏幕中画好的图形付给 IElement 储存
                    IGraphicsContainer pGraphicsContainer = axMapControl.ActiveView as IGraphicsContainer;//把地图的当前view作为图片的容器

                    pGraphicsContainer.AddElement(pMElement, 0);//显示储存在 IElement 中图形，这样就持久化了。
                    axMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                    //axMapControl.Refresh();
                    break;

                case "Line":

                    ILineElement pLineElement;
                    IElement pLElement;

                    IPolyline pLine;

                    pLineElement = new LineElementClass();
                    pLElement = pLineElement as IElement;

                    RubberLineClass pRubberBandd = new RubberLineClass();
                    pLine = pRubberBandd.TrackNew(axMapControl.ActiveView.ScreenDisplay, null) as IPolyline;

                    pLElement.Geometry = pLine;

                    pGraphicsContainer = axMapControl.ActiveView as IGraphicsContainer;//把地图的当前view作为图片的容器


                    pGraphicsContainer.AddElement(pLElement, 0);//把刚刚的element转到容器上
                    axMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);

                    break;
                case "Fill":
                    IFillShapeElement pFillShapeElement;
                    IElement pgonElemnt;

                    IPolygon pPolygon;
                    pFillShapeElement = new PolygonElementClass();
                    pgonElemnt = pFillShapeElement as IElement;//Element


                    RubberPolygonClass pRubberBand3 = new RubberPolygonClass();//在屏幕上画个多边形
                    pPolygon = pRubberBand3.TrackNew(axMapControl.ActiveView.ScreenDisplay, null) as IPolygon;

                    pgonElemnt.Geometry = pPolygon;//把这个多边形转成Element

                    pGraphicsContainer = axMapControl.ActiveView as IGraphicsContainer;//把地图的当前view作为图片的容器

                    //pGraphicsContainer.DeleteAllElements ();
                    pGraphicsContainer.AddElement(pgonElemnt, 0);//把刚刚的element转到容器上
                    axMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                    break;
            }
        }

        public void showAnnotationByScale()
        {

            IMap pMap = axMapControl.Map;
            IFeatureLayer pFeaturelayer = pMap.get_Layer(1) as IFeatureLayer;
            IGeoFeatureLayer pGeoFeatureLayer = pFeaturelayer as IGeoFeatureLayer;
            //创建标注集接口，可以对标注进行添加、删除、查询、排序等操作
            IAnnotateLayerPropertiesCollection pAnnotateLayerPropertiesCollection = new AnnotateLayerPropertiesCollectionClass();
            pAnnotateLayerPropertiesCollection = pGeoFeatureLayer.AnnotationProperties;
            pAnnotateLayerPropertiesCollection.Clear();
            //创建标注的颜色
            IRgbColor pRgbColor = new RgbColorClass();
            pRgbColor.Red = 255;
            pRgbColor.Green = 0;
            pRgbColor.Blue = 0;
            //创建标注的字体样式
            ITextSymbol pTextSymbol = new TextSymbolClass();
            pTextSymbol.Color = pRgbColor;
            pTextSymbol.Size = 12;
            pTextSymbol.Font.Name = "宋体";
            //定义 ILineLabelPosition接口，用来管理line features的标注属性，指定标注和线要素的位置关系
            ILineLabelPosition pLineLabelPosition = new LineLabelPositionClass();
            pLineLabelPosition.Parallel = false;
            pLineLabelPosition.Perpendicular = true;
            pLineLabelPosition.InLine = true;
            //定义 ILineLabelPlacementPriorities接口用来控制标注冲突
            ILineLabelPlacementPriorities pLineLabelPlacementPriorities = new LineLabelPlacementPrioritiesClass();
            pLineLabelPlacementPriorities.AboveStart = 5;
            pLineLabelPlacementPriorities.BelowAfter = 4;
            //定义 IBasicOverposterLayerProperties 接口实现 LineLabelPosition 和 LineLabelPlacementPriorities对象的控制
            IBasicOverposterLayerProperties pBasicOverposterLayerProperties = new BasicOverposterLayerPropertiesClass();
            pBasicOverposterLayerProperties.LineLabelPlacementPriorities = pLineLabelPlacementPriorities;
            pBasicOverposterLayerProperties.LineLabelPosition = pLineLabelPosition;
            pBasicOverposterLayerProperties.FeatureType = esriBasicOverposterFeatureType.esriOverposterPolygon;
            //创建标注对象
            ILabelEngineLayerProperties pLabelEngineLayerProperties = new LabelEngineLayerPropertiesClass();
            //设置标注符号
            pLabelEngineLayerProperties.Symbol = pTextSymbol;
            pLabelEngineLayerProperties.BasicOverposterLayerProperties = pBasicOverposterLayerProperties;
            //声明标注的Expression是否为Simple
            pLabelEngineLayerProperties.IsExpressionSimple = true;
            //设置标注字段
            pLabelEngineLayerProperties.Expression = "[DIQU]";
            //定义IAnnotateLayerTransformationProperties 接口用来控制feature layer的display of dynamic labels 
            IAnnotateLayerTransformationProperties pAnnotateLayerTransformationProperties = pLabelEngineLayerProperties as IAnnotateLayerTransformationProperties;
            //设置标注参考比例尺
            pAnnotateLayerTransformationProperties.ReferenceScale = 500000;
            //定义IAnnotateLayerProperties接口，决定FeatureLayer动态标注信息
            IAnnotateLayerProperties pAnnotateLayerProperties = pLabelEngineLayerProperties as IAnnotateLayerProperties;
            //设置显示标注最大比例尺
            pAnnotateLayerProperties.AnnotationMaximumScale = 500000;
            //设置显示标注的最小比例
            pAnnotateLayerProperties.AnnotationMinimumScale = 25000000;
            //决定要标注的要素
            pAnnotateLayerProperties.WhereClause = "DIQU<>'宿州市'";
            //将创建好的标注对象添加到标注集对象中
            pAnnotateLayerPropertiesCollection.Add(pAnnotateLayerProperties);
            //声明标注是否显示
            pGeoFeatureLayer.DisplayAnnotation = true;
            //刷新视图
            this.axMapControl.Refresh();
        }
        
        private IPoint PRJtoGCS(double x, double y)
        {
            IPoint pPoint = new PointClass();
            pPoint.PutCoords(x, y);
            ISpatialReferenceFactory pSRF = new SpatialReferenceEnvironmentClass();
            pPoint.SpatialReference = pSRF.CreateProjectedCoordinateSystem(4269);
            pPoint.Project(pSRF.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_NAD1983));
            return pPoint;
        }

        private IPoint GCStoPRJ(IPoint pPoint, int GCSType, int PRJType)
        {
            ISpatialReferenceFactory pSRF = new SpatialReferenceEnvironmentClass();
            pPoint.SpatialReference = pSRF.CreateGeographicCoordinateSystem(GCSType);
            pPoint.Project(pSRF.CreateProjectedCoordinateSystem(PRJType));
            return pPoint;
        }

        private void axMapControl_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            label1.Text = "X: " + e.mapX + "    Y:" + e.mapY;
        }

        public void mypoints()
        {
            IMap pMap = axMapControl.Map;
            //第一个图层是点，第二个图层是线，第三个图层是面。这里就不判断，主要在于功能的实现
            ILayer pLayer = pMap.get_Layer(0);
            IFeatureLayer pFeatureLyr = pLayer as IFeatureLayer;
            IFeatureClass pFeatCls = pFeatureLyr.FeatureClass;
            IDataset pDataset = pFeatCls as IDataset;
            IWorkspace pWS = pDataset.Workspace;
            IWorkspaceEdit pWorkspaceEdit = pWS as IWorkspaceEdit;
            pWorkspaceEdit.StartEditing(false);
            pWorkspaceEdit.StartEditOperation();
            IFeatureBuffer pFeatureBuffer;
            IFeatureCursor pFeatureCuror;
            IFeature pFeature;
            IPoint pPoint = new PointClass();
            pFeatureBuffer = pFeatCls.CreateFeatureBuffer();
            pFeatureCuror = pFeatCls.Insert(true);
            pFeature = pFeatureBuffer as IFeature;
            //pPoint = (IPoint)pSOC.CreateObject("esriGeometry.Point");
            pPoint.X = 110;
            pPoint.Y = 40;
            IGeometry pPointGeo = pPoint as IGeometry;
            pFeature.Shape = pPointGeo;
            pFeatureCuror.InsertFeature(pFeatureBuffer);

            pWorkspaceEdit.StopEditOperation();
            pWorkspaceEdit.StopEditing(true);
            axMapControl.Refresh();
        }

        //加载地图
        private void button3_Click(object sender, EventArgs e)
        {
            string filePath = @"E:\Downloads\ARCgis\United States\USA Base Map.mxd";
            
            if (axMapControl.CheckMxFile(filePath))
                axMapControl.LoadMxFile(filePath, Type.Missing, Type.Missing);
            
        }
        //标记
        private void button4_Click(object sender, EventArgs e)
        {
            CreateTextElment(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            IPoint pPoint = new PointClass();
            pPoint.X = 40;
            pPoint.Y = -101;

            ISpatialReferenceFactory pSRF = new SpatialReferenceEnvironmentClass();
           
            pPoint.SpatialReference = pSRF.CreateProjectedCoordinateSystem(4152);
           
            pPoint.Project(pSRF.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_NAD1983HARN));
            
            GeoToGra(pPoint);
            
            esriUnits eu = new esriUnits();
            
            axMapControl.MapUnits = esriUnits.esriMeters;
        }

        //将任意坐标系统转换为自定义Albers大地坐标（米）
        public static IPoint GeoToGra(IPoint point)
        {
            IPoint pt = new PointClass();
            pt.PutCoords(point.X, point.Y);

            ISpatialReferenceFactory2 pFact = new SpatialReferenceEnvironmentClass();

            //定义地理坐标，由输入的对象决定，也可以自己定义，参考： esriSRGeoCSType
            IGeographicCoordinateSystem pGCS = new GeographicCoordinateSystemClass();
            pGCS = pFact.CreateGeographicCoordinateSystem(point.SpatialReference.FactoryCode);

            //自定义投影方式
            IProjectedCoordinateSystem pProjectedCS = new ProjectedCoordinateSystemClass();
            IProjectedCoordinateSystemEdit pProjectedCSEdit = pProjectedCS as IProjectedCoordinateSystemEdit;

            //定义投影方式，参考： esriSRProjectionType
            IProjection pProjection = new ProjectionClass();
            pProjection = pFact.CreateProjection((int)esriSRProjectionType.esriSRProjection_Albers);

            //定义投影单位，参考：esriSRUnitType
            ILinearUnit pUnit = new LinearUnitClass();
            pUnit = pFact.CreateUnit((int)esriSRUnitType.esriSRUnit_Meter) as ILinearUnit;

            //定义其他参数，参考：esriSRParameterType
            IParameter[] pParm = new IParameter[6];
            pParm[0] = pFact.CreateParameter((int)esriSRParameterType.esriSRParameter_FalseEasting);
            pParm[0].Value = 0;

            pParm[1] = pFact.CreateParameter((int)esriSRParameterType.esriSRParameter_FalseNorthing);
            pParm[1].Value = 0;

            pParm[2] = pFact.CreateParameter((int)esriSRParameterType.esriSRParameter_CentralMeridian);
            pParm[2].Value = 110;

            pParm[3] = pFact.CreateParameter((int)esriSRParameterType.esriSRParameter_StandardParallel1);
            pParm[3].Value = 25;

            pParm[4] = pFact.CreateParameter((int)esriSRParameterType.esriSRParameter_StandardParallel2);
            pParm[4].Value = 47;

            pParm[5] = pFact.CreateParameter((int)esriSRParameterType.esriSRParameter_LatitudeOfOrigin);
            pParm[5].Value = 0;

            //设置投影相关信息
            object name = "User_Defined_Albers";
            object alias = "Albers";
            object abbreviation = "Albers";
            object remarks = "User_Defined_Albers is the projection";
            object usage = "";
            object gcs = pGCS;
            object projectedUnit = pUnit;
            object projection = pProjection;
            object parameters = pParm;
            pProjectedCSEdit.Define(ref name, ref alias, ref abbreviation, ref remarks, ref usage, ref gcs, ref projectedUnit, ref projection, ref parameters);

            //获取自定义空间参考
            ISpatialReference pSpatialRef = pProjectedCS as ISpatialReference;

            IGeometry pGeometry = (IGeometry)pt;
            pGeometry.SpatialReference = pGCS as ISpatialReference;

            //重投影处理
            pGeometry.Project(pSpatialRef);

            return pt;
        }

        //使用addXY进行经纬度描点
        public void  CreateXYLayer()
        {
            try
            {
                IMxDocument pMxDoc;
                IMap pMap = axMapControl.Map;
                
                IApplication m_app;
                
                IDocument doc = new MxDocumentClass();
                m_app = doc.Parent;
                pMxDoc = (IMxDocument)m_app.Document;
                pMap = pMxDoc.FocusMap;
        
                IStandaloneTableCollection pStTabCol;
                IStandaloneTable pStandaloneTable; 
                ITable pTable = null;
                pStTabCol = (IStandaloneTableCollection) pMap;
                for (int intCount = 0; intCount < pStTabCol.StandaloneTableCount; intCount++)
                {
                    pStandaloneTable = (IStandaloneTable) pStTabCol.get_StandaloneTable(intCount);
                    if (pStandaloneTable.Name == @"e:\XYSample.txt")
                    {
                        pTable = pStandaloneTable.Table;
                        break;
                    }
                }
                if (pTable == null)
                {
                    MessageBox.Show("The table was not found");
                    return; 
                }

                IDataset pDataSet;
                IName pTableName;
                pDataSet = (IDataset) pTable;
                pTableName = pDataSet.FullName;

                IXYEvent2FieldsProperties pXYEvent2FieldsProperties;
                pXYEvent2FieldsProperties = new XYEvent2FieldsPropertiesClass();
                pXYEvent2FieldsProperties.XFieldName = "x";
                pXYEvent2FieldsProperties.YFieldName = "y";
                pXYEvent2FieldsProperties.ZFieldName = "";

                ISpatialReferenceFactory pSpatialReferenceFactory;
                pSpatialReferenceFactory = new SpatialReferenceEnvironmentClass();
                IProjectedCoordinateSystem pProjectedCoordinateSystem;
                pProjectedCoordinateSystem = pSpatialReferenceFactory.CreateProjectedCoordinateSystem(26911);
                // esriSRProjCS_NAD1983UTM_11N

                // Create the XY name object and set it's properties 
                IXYEventSourceName pXYEventSourceName = new XYEventSourceNameClass();
                IName pXYName;
                IXYEventSource pXYEventSource;
                pXYEventSourceName.EventProperties = pXYEvent2FieldsProperties;
                pXYEventSourceName.SpatialReference = pProjectedCoordinateSystem;
                pXYEventSourceName.EventTableName = pTableName;
                pXYName = (IName) pXYEventSourceName;
                pXYEventSource = (IXYEventSource) pXYName.Open();

                // Create a new Map Layer 
                IFeatureLayer pFLayer = new FeatureLayerClass();
                pFLayer.FeatureClass = (IFeatureClass) pXYEventSource;
                pFLayer.Name = "Sample XY Event layer";

                // Add the layer extension (this is done so that when you edit
                // the layer's Source properties and click the Set Data Source
                // button, the Add XY Events Dialog appears)
                ILayerExtensions pLayerExt;
                IFeatureLayerSourcePageExtension pRESPageExt = new XYDataSourcePageExtensionClass();
                pLayerExt = (ILayerExtensions) pFLayer;
                pLayerExt.AddExtension(pRESPageExt);

                pMap.AddLayer(pFLayer);
            }
      
            catch (System.Exception SysEx)
            {
                MessageBox.Show(SysEx.Message,".NET Error: ",MessageBoxButtons.OK,MessageBoxIcon.Warning); 
            }
        }
        #endregion

        #region 添加、编辑图层
        public void AddNewGraphicsLayer()
        {
            // 创建graphics layer 并添加到控件上
            IGraphicsContainer globeGraphicsLayer = new GlobeGraphicsLayerClass();
            ILayer layer = (ILayer)globeGraphicsLayer;
            layer.Name = "Add mine layer";

            axMapControl.AddLayer(layer);


            int index = GetIndexNumberFromLayerName(axMapControl.ActiveView, layer.Name.ToString());
            
        }

        //编辑层
        public void AddPointByBuffer(int index)
        {
            IPoint point1 = new PointClass();
            //point1.PutCoords(101, 40);
            point1 = GetProject2(-120, 20);
            IPoint point2 = new PointClass();
            //point2.PutCoords(110, 40);
            point2 = GetProject2(-120, 40);
            //IPoint point3 = new PointClass();
            //point3.PutCoords(112, 40);

            object o = Type.Missing;
            IPointCollection pointCollection = new PolygonClass();
            pointCollection.AddPoint(point1, ref o, ref o);
            pointCollection.AddPoint(point2, ref o, ref o);
            //pointCollection.AddPoint(point3, ref o, ref o);
            IPolygon polygon = pointCollection as IPolygon;

            IElement element = new PolygonElementClass();
            element.Geometry = polygon;
            IGraphicsContainer graphicsContainer = axMapControl.Map as IGraphicsContainer;
            graphicsContainer.AddElement(element, 0);
            axMapControl.Refresh();
            //ILayer pLayer = this.axMapControl.get_Layer(2);//获取当前图层,此图层为组图层，需要转换
            //GlobeGraphicsLayer cLyr = null;
            //if (pLayer is GlobeLayer)
            //{
            //    //cLyr = (GlobeGraphicsLayer)pLayer;

            //    IFeatureLayer pFeatureLyr = pLayer as IFeatureLayer;//将ILayer转换为IFeaturelayer，为了对图层上的要素进行编辑   
            //    IFeatureClass pFeatCls = pFeatureLyr.FeatureClass;//定义一个要素集合，并获取图层的要素集合   
            //    IFeatureClassWrite fr = (IFeatureClassWrite)pFeatCls;//定义一个实现新增要素的接口实例，并该实例作用于当前图层的要素集   
            //    IWorkspaceEdit w = (pFeatCls as IDataset).Workspace as IWorkspaceEdit;//定义一个工作编辑工作空间，用于开启前图层的编辑状态   
            //    IFeature f;//定义一个IFeature实例，用于添加到当前图层上   
            //    w.StartEditing(true);//开启编辑状态   
            //    w.StartEditOperation();//开启编辑操作   
            //    //IPoint p;//定义一个点，用来作为IFeature实例的形状属性，即shape属性   
            //    //下面是设置点的坐标和参考系 



            //    //IFeatureLayer l = axMapControl.Map.get_Layer(index) as IFeatureLayer;

            //    //IFeatureClass fc = l.FeatureClass;

            //    //IFeatureClassWrite fr = fc as IFeatureClassWrite;

            //    //IWorkspaceEdit w = (fc as IDataset).Workspace as IWorkspaceEdit;

            //    //IFeature f;
            //    //可选参数的设置

            //    object Missing = Type.Missing;

            //    IPoint p = new PointClass();

            //    w.StartEditing(true);

            //    w.StartEditOperation();
            //    IPolygon polygon;

            //    for (int i = 0; i < 100; i++)
            //    {

            //        f = pFeatCls.CreateFeature();

            //        //定义一个多义线对象

            //        //IPolyline PlyLine = new PolylineClass();

            //        //定义一个点的集合

            //        IPointCollection ptclo = new PolygonClass();

            //        //定义一系列要添加到多义线上的点对象，并赋初始值

            //        for (int j = 0; j < 4; j++)
            //        {
            //            p.PutCoords(j, j);

            //            ptclo.AddPoint(p, ref Missing, ref Missing);

            //        }
                    
            //        //IPointCollection pointCollection = new PolygonClass();

            //        polygon = ptclo as IPolygon;

            //        IElement element = new PolygonElementClass();
            //        element.Geometry = polygon;
            //        IGraphicsContainer graphicsContainer = axMapControl.Map as IGraphicsContainer;
            //        graphicsContainer.AddElement(element, 0);
            //        axMapControl.Refresh();
            //        //f.Shape = PlyLine;

            //        //fr.WriteFeature(f);
                    
            //        IGeometry geo = (IGeometry)polygon;

            //    //开始画点
            //    IMarkerElement pMarkerElement;//对于点，线，面的element定义这里都不一样，他是可实例化的类，而IElement是实例化的类，必须通过 IMarkerElement 初始化负值给 IElement 。
            //    IElement pMElement;
            //    pMarkerElement = new MarkerElementClass();

            //    pMElement = pMarkerElement as IElement;

            //    pMElement.Geometry = polygon;//把你在屏幕中画好的图形付给 IElement 储存
            //    IGraphicsContainer pGraphicsContainer = axMapControl.ActiveView as IGraphicsContainer;//把地图的当前view作为图片的容器

            //    pGraphicsContainer.AddElement(pMElement, 0);//显示储存在 IElement 中图形，这样就持久化了。

            //    axMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);

            //    }



                


            //    //p = new PointClass();
            //    p.SpatialReference = this.axMapControl.SpatialReference;
            //    //p.X = 600;
            //    //p.Y = 500;

            //    //将IPoint设置为IFeature的shape属性时，需要通过中间接口IGeometry转换   
            //    IGeometry peo;
            //    peo = p;
            //    f = pFeatCls.CreateFeature();//实例化IFeature对象， 这样IFeature对象就具有当前图层上要素的字段信息   
            //    f.Shape = peo;//设置IFeature对象的形状属性   
            //    //f.set_Value(0, "Marker01");//设置IFeature对象的索引是3的字段值   
            //    f.Store();//保存IFeature对象   
            //    fr.WriteFeature(f);//将IFeature对象，添加到当前图层上   
            //    w.StopEditOperation();//停止编辑操作   
            //    w.StopEditing(true);//关闭编辑状态，并保存修改   
            //    this.axMapControl.Refresh();//刷新地图 
            //}

            //IFeatureLayer l = axMapControl.Map.get_Layer(index) as IFeatureLayer;

            //IFeatureClass fc = l.FeatureClass;

            //IFeatureClassWrite fr = fc as IFeatureClassWrite;

            //IWorkspaceEdit w = (fc as IDataset).Workspace as IWorkspaceEdit;

            //IFeature f;
            ////可选参数的设置

            //object Missing = Type.Missing;

            //IPoint p = new PointClass();

            //w.StartEditing(true);

            //w.StartEditOperation();

            //for (int i = 0; i < 100; i++)
            //{

            //    f = fc.CreateFeature();

            //    //定义一个多义线对象

            //    IPolyline PlyLine = new PolylineClass();

            //    //定义一个点的集合

            //    IPointCollection ptclo = PlyLine as IPointCollection;

            //    //定义一系列要添加到多义线上的点对象，并赋初始值

            //    for (int j = 0; j < 4; j++)
            //    {
            //        p.PutCoords(j, j);

            //        ptclo.AddPoint(p, ref Missing, ref Missing);

            //    }

            //    f.Shape = PlyLine;

            //    fr.WriteFeature(f);

            //}

            //w.StopEditOperation();

            //w.StopEditing(true);
        }

        //根据层名，得到层的索引
        public System.Int32 GetIndexNumberFromLayerName(ESRI.ArcGIS.Carto.IActiveView activeView, System.String layerName)
        {
            if (activeView == null || layerName == null)
            {
                return -1;
            }
            ESRI.ArcGIS.Carto.IMap map = activeView.FocusMap;

            // Get the number of layers
            int numberOfLayers = map.LayerCount;

            // Loop through the layers and get the correct layer index
            for (System.Int32 i = 0; i < numberOfLayers; i++)
            {
                if (layerName == map.get_Layer(i).Name)
                {

                    // Layer was found
                    return i;

                }
                //map.get_Layer(i).Visible = false;
            }

            // No layer was found
            return -1;
        }

        private void CreateLines()
        {
            //创建要素类
            #region 创建新的内存工作空间
            IWorkspaceFactory pWSF = new InMemoryWorkspaceFactoryClass();
            IWorkspaceName pWSName = pWSF.Create("", "Temp", null, 0);

            IName pName = (IName)pWSName;
            IWorkspace pMemoryWS = (IWorkspace)pName.Open();
            #endregion

            IField oField = new FieldClass();
            IFields oFields = new FieldsClass();
            IFieldsEdit oFieldsEdit = null;
            IFieldEdit oFieldEdit = null;
            IFeatureClass oFeatureClass = null;
            IFeatureLayer oFeatureLayer = null;

            oFieldsEdit = oFields as IFieldsEdit;
            oFieldEdit = oField as IFieldEdit;
            oFieldEdit.Name_2 = "OBJECTID";
            oFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
            oFieldEdit.IsNullable_2 = false;
            oFieldEdit.Required_2 = false;
            oFieldsEdit.AddField(oField);

            oField = new FieldClass();
            oFieldEdit = oField as IFieldEdit;
            IGeometryDef pGeoDef = new GeometryDefClass();
            IGeometryDefEdit pGeoDefEdit = (IGeometryDefEdit)pGeoDef;
            pGeoDefEdit.AvgNumPoints_2 = 5;
            pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
            pGeoDefEdit.GridCount_2 = 1;
            pGeoDefEdit.HasM_2 = false;
            pGeoDefEdit.HasZ_2 = false;
            pGeoDefEdit.SpatialReference_2 = axMapControl.SpatialReference;
            oFieldEdit.Name_2 = "SHAPE";
            oFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            oFieldEdit.GeometryDef_2 = pGeoDef;
            oFieldEdit.IsNullable_2 = true;
            oFieldEdit.Required_2 = true;
            oFieldsEdit.AddField(oField);

            oField = new FieldClass();
            oFieldEdit = oField as IFieldEdit;
            oFieldEdit.Name_2 = "Code";
            oFieldEdit.Type_2 = esriFieldType.esriFieldTypeSmallInteger;
            //oFieldEdit.Length = 10;
            oFieldEdit.IsNullable_2 = true;
            oFieldsEdit.AddField(oField);
            //创建要素类
            oFeatureClass = (pMemoryWS as IFeatureWorkspace).CreateFeatureClass("Temp", oFields, null, null, esriFeatureType.esriFTSimple, "SHAPE", "");
            oFeatureLayer = new FeatureLayerClass();
            oFeatureLayer.Name = "PointLayer";
            oFeatureLayer.FeatureClass = oFeatureClass;
            //创建唯一值符号化对象

            IUniqueValueRenderer pURender = new UniqueValueRendererClass();
            pURender.FieldCount = 1;
            pURender.set_Field(0, "Code");
            pURender.UseDefaultSymbol = false;
            //创建SimpleMarkerSymbolClass对象
            ISimpleMarkerSymbol pSimpleMarkerSymbol = new SimpleMarkerSymbolClass();
            //创建RgbColorClass对象为pSimpleMarkerSymbol设置颜色
            IRgbColor pRgbColor = new RgbColorClass();
            pRgbColor.Red = 255;
            pSimpleMarkerSymbol.Color = pRgbColor as IColor;
            //设置pSimpleMarkerSymbol对象的符号类型，选择钻石
            pSimpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSDiamond;
            //设置pSimpleMarkerSymbol对象大小，设置为５
            pSimpleMarkerSymbol.Size = 5;
            //显示外框线
            pSimpleMarkerSymbol.Outline = true;
            //为外框线设置颜色
            IRgbColor pLineRgbColor = new RgbColorClass();
            pLineRgbColor.Green = 255;
            pSimpleMarkerSymbol.OutlineColor = pLineRgbColor as IColor;
            //设置外框线的宽度
            pSimpleMarkerSymbol.OutlineSize = 1;

            //半透明颜色
            pURender.AddValue("Marker01", "", pSimpleMarkerSymbol as ISymbol);

            //唯一值符号化内存图层
            (oFeatureLayer as IGeoFeatureLayer).Renderer = pURender as IFeatureRenderer;
            ILayerEffects pLyrEffect = oFeatureLayer as ILayerEffects;
            //透明度
            pLyrEffect.Transparency = 0;

            oFeatureLayer.Visible = true;

            this.axMapControl.AddLayer(oFeatureLayer, axMapControl.LayerCount);

            //AddPointByBuffer(0);
            //insertpoint = true;
        }
        #endregion
    }
}

