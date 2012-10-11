using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

using ESRI.ArcGIS.esriSystem;
using System.Collections;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.GlobeCore;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.CartoUI;
using ESRI.ArcGIS.LocationUI;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
namespace ArcGisDemo
{
    public partial class Form1 : Form
    {
        private IPoint pSmallViewerMouseDownPt;//拖动时鼠标落点
        private bool isTrackingSmallViewer = false; //标识是否在拖动
        private IMoveEnvelopeFeedback pSmallViewerEnvelope;//鹰眼小地图的红框,IMoveEnvelopeFeedback,用来移动Envelope的接口
        static int moveCount = 0;//记录移动的个数，为移动过程中显示红框用。
        public Form1()
        {
            InitializeComponent();         

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            axMapControl2.AutoMouseWheel = false;//设定axMapControl2鼠标滚轴缩放地图无效
            axMapControl2.AutoKeyboardScrolling = false;//设定axMapControl2鼠标滚轴缩放地图无效          
        }

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




            //axMapControl.CenterAt(pPoint);
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

        private void CreatePoint(double x, double y)
        {
            IPoint pPoint = new PointClass();
            IMap pMap = axMapControl.Map;
            IActiveView pActiveView = pMap as IActiveView;
            IGraphicsContainer pGraphicsContainer;
            IElement pElement = new MarkerElementClass();

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
            //pTextSymbol.Background = pTextBackground;
            //((ITextElement)pElement).Symbol = pTextSymbol;
            //((ITextElement)pElement).Text = "测试";
            pPoint.X = x + 42;
            pPoint.Y = y + 42;

            pPoint.PutCoords(x, y);
            ISpatialReferenceFactory pSRF = new SpatialReferenceEnvironmentClass();
            pPoint.SpatialReference = pSRF.CreateProjectedCoordinateSystem(2414);
            pPoint.Project(pSRF.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_Beijing1954));


            pElement.Geometry = pPoint;
            pGraphicsContainer.AddElement(pElement, 1);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        #region 各种标记
        //public static void FlashFeature(AxMapControl mapControl, IMap iMap)
        public static void FlashFeature(AxMapControl mapControl, IFeature iFeature, IMap iMap)
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

        //定位
        private void button1_Click(object sender, EventArgs e)
        {
           

            IEnvelope pEnvelope = new EnvelopeClass();
            IPoint pPoint = new PointClass();
            //调用GetProject2方法经纬度转换成米
            pPoint = GetProject2(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text));
            //axMapControl.MapUnits = esriUnits.esriDecimalDegrees;
            //axMapControl.MapUnits = esriUnits.esriMeters; //米           
           // esriUnits mapUnits = axMapControl.MapUnits; 
            //axMapControl.MapUnits = esriUnits.esriDecimalDegrees;
            //pPoint.PutCoords(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text));//该句中的txtlong，txtlat是输入经纬值的TextBox
            //pPoint.X = Convert.ToDouble(textBox1.Text);
            //pPoint.Y = Convert.ToDouble(textBox2.Text);
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

                    RubberPointClass pRubberBand = new RubberPointClass();//你的RUBBERBAND随着你的图形耳边
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

                    //调用GetProject2方法经纬度转换成米
                    pPoint = GetProject2(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text));

                    //pPoint.PutCoords(Convert.ToDouble(textBox3.Text), Convert.ToDouble(textBox4.Text));
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
                   // axMapControl.get_Layer(0)

                    // ILayer pLayer = this.axMapControl.get_Layer(1);//所要加的层  

                    
                    pGraphicsContainer.AddElement(pMElement, 0);//显示储存在 IElement 中图形，这样就持久化了。
                    axMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                    
                    //axMapControl.Refresh();
                    break;

                case "Line":

                    //ILineElement pLineElement;
                    //IElement pLElement;

                    //IPolyline pLine;
                    // IGraphicsContainer pGraphicsContainerl = axMapControl.ActiveView as IGraphicsContainer;//把地图的当前view作为图片的容器
                    //pLineElement = new LineElementClass();
                    //pLElement = pLineElement as IElement;
                    
                    //RubberLineClass pRubberBandd = new RubberLineClass();
                    //pLine = pRubberBandd.TrackNew(axMapControl.ActiveView.ScreenDisplay, null) as IPolyline;

                    //pLElement.Geometry = pLine;

                    //pGraphicsContainerl = axMapControl.ActiveView as IGraphicsContainer;//把地图的当前view作为图片的容器


                    //pGraphicsContainerl.AddElement(pLElement, 0);//把刚刚的element转到容器上
                    //axMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);



                    //00000000000
                    //调用GetProject2方法经纬度转换成米
                    IPoint pPointl=new PointClass();//你画的图形式什么就是什么，特别的是LINE则需要定义为POLYLINE
                    pPointl = GetProject2(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text));

                    IPoint pPointl2=new PointClass();//你画的图形式什么就是什么，特别的是LINE则需要定义为POLYLINE
                    pPointl2 = GetProject2(-120, 40);

                    //创建一个Line对象
                    ILine pLine2 = new LineClass();
                    
                    //Polyline pLine2 = new PolylineClass();
                    //设置Line对象的起始终止点
                    pLine2.PutCoords(pPointl, pPointl2);
                    
                    //IPointCollection pMultipoint = new MultipointClass();
                    //object o=Type.Missing;
                    //pMultipoint.AddPoint(pPointl, ref o, ref o);
                    //pMultipoint.AddPoint(pPointl2, ref o, ref o);
                    
                      axMapControl.ActiveView.Refresh();//刷新当前视图
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
        /// <summary>
        /// 画线
        /// </summary>
        internal void FlashFeature()
        {
            IPoint point1 = new PointClass();
            //point1.PutCoords(101, 40);
            point1 = GetProject2(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text));
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
        }
        public void showAnnotationByScale()
        {

            IMap pMap = axMapControl.Map;
            IFeatureLayer pFeaturelayer = pMap.get_Layer(0) as IFeatureLayer;
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
        //画点
        private void button2_Click(object sender, EventArgs e)
        {
            points();

        }
        private void ConstrainDistance()
        {
            string strx = "100";
            string stry = "40";
            ///-2097733.22628655
            /////-2097733.23
            string strs = string.Format("{0}, {1}  {2}", strx, stry, axMapControl.MapUnits.ToString().Substring(4));
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
        //地图点击事件，获取点击区域的X Y 单位：米
        private void axMapControl_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            label1.Text = "X: " + e.mapX + "    Y:" + e.mapY+"  米";
            Demo01 frm = null;
            if (frm == null || frm.IsDisposed)
                frm = new Demo01();

            frm.Show();
            frm.TopMost = true;
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


        #region 将经纬度点转换为平面坐标。        
        /// 将经纬度点转换为平面坐标。
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

        //加载地图
        private void button3_Click(object sender, EventArgs e)
        {
            //axMapControl.MapUnits = esriUnits.esriMeters;
            string filePath = @"C:\Documents and Settings\Administrator\桌面\United States\USA Base Map.mxd";
            //string filePath = @"C:\Documents and Settings\Administrator\桌面\United States\USA Thematic Maps.mxd";
            if (axMapControl.CheckMxFile(filePath))
                axMapControl.LoadMxFile(filePath, Type.Missing, Type.Missing);
            //axMapControl.MapUnits = esriUnits.esriFeet;
        }
        //标记
        private void button4_Click(object sender, EventArgs e)
        {
            IPoint pPoint = new PointClass();
            //调用GetProject2方法经纬度转换成米
            pPoint = GetProject2(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text));

            CreateTextElment(pPoint.X, pPoint.Y);
        }

        //private void button5_Click(object sender, EventArgs e)
        //{

        //    GetProject2(-120,40);
        //    IPoint pPoint = new PointClass();
        //    pPoint.X = Convert.ToDouble(textBox1.Text);
        //    pPoint.Y = Convert.ToDouble(textBox2.Text);
        //    //Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text)
        //    ISpatialReferenceFactory pSRF = new SpatialReferenceEnvironmentClass();
        //   // pPoint.SpatialReference = pSRF.CreateProjectedCoordinateSystem(54013);
        //    pPoint.SpatialReference = pSRF.CreateProjectedCoordinateSystem(26948);
        //   // pPoint.Project(pSRF.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_NAD1983));
        //    pPoint.Project(pSRF.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_NAD1983));
            
        //    GeoToGra(pPoint);
        //    //MCT84Bl2xy(-100, 37);


        //    esriUnits eu = new esriUnits();
        //    //esriUnits.esriMeters;
        //    axMapControl.MapUnits = esriUnits.esriMeters;
        //}
        /// <summary>
        /// GPS经纬度换算成x,y坐标
        /// </summary>
        /// <param name="l">精度</param>
        /// <param name="B">纬度</param>
        /// <param name="xc">X坐标</param>
        /// <param name="yc">Y坐标</param>
        public static void MCT84Bl2xy(double l, double B)
        {
            double xc;
            double yc;
            try
            {
                l = l * Math.PI / 180;
                B = B * Math.PI / 180;

                double B0 = 30 * Math.PI / 180;

                double N = 0, e = 0, a = 0, b = 0, e2 = 0, K = 0;
                a = 6378137;
                b = 6356752.3142;
                e = Math.Sqrt(1 - (b / a) * (b / a));
                e2 = Math.Sqrt((a / b) * (a / b) - 1);
                double CosB0 = Math.Cos(B0);
                N = (a * a / b) / Math.Sqrt(1 + e2 * e2 * CosB0 * CosB0);
                K = N * CosB0;

                double Pi = Math.PI;
                double SinB = Math.Sin(B);

                double tan = Math.Tan(Pi / 4 + B / 2);
                double E2 = Math.Pow((1 - e * SinB) / (1 + e * SinB), e / 2);
                double xx = tan * E2;

                xc = K * Math.Log(xx);
                yc = K * l;
                return;
            }
            catch (Exception ErrInfo)
            {
            }
            xc = -1;
            yc = -1;
        }
        /// <summary>
        /// 高斯坐标转经纬度算法
        /// </summary>
        /// <param name="x">大地坐标X</param>
        /// <param name="y">大地坐标Y</param>
        /// <param name="B">纬度（单位：弧度）</param>
        /// <param name="L">经度（单位：弧度）</param>
        /// <param name="center">中央经线（单位：弧度）</param>
        //private void ComputeXYGeo(double x, double y, ref double B, ref double L, double center, int n)
        private void ComputeXYGeo(double x, double y, ref double B, ref double L, double center, int n)
        {
             double ParaE1 = 6.69438499958795E-03;//椭球体第一偏心率 
             double Parak0 = 1.57048687472752E-07;//有关椭球体的常量
             double Parak1 = 5.05250559291393E-03;//有关椭球体的常量
             double Parak2 = 2.98473350966158E-05;//有关椭球体的常量
             double Parak3 = 2.41627215981336E-07;//有关椭球体的常量
             double Parak4 = 2.22241909461273E-09;//有关椭球体的常量
             double ParaC = 6399596.65198801;//极点子午圈曲率半径
             double y1, bf, e, se, v, t, N, nl, vt, yn, t2, g, cbf;
            y1 = y - 500000 - 1000000 * n;//减去带号
            e = Parak0 * x;
            se = Math.Sin(e);
            bf = e + Math.Cos(e) * (Parak1 * se - Parak2 * Math.Pow(se, 3) + Parak3 * Math.Pow(se, 5) - Parak4 * Math.Pow(se, 7));

            g = 1;
            t = Math.Tan(bf);
            nl = ParaE1 * Math.Pow(Math.Cos(bf), 2);
            v = Math.Sqrt(1 + nl);
            N = ParaC / v;
            yn = y1 / N;
            vt = Math.Pow(v, 2) * t;
            t2 = Math.Pow(t, 2);
            B = bf - vt * Math.Pow(yn, 2) / 2.0 + (5.0 + 3.0 * t2 + nl - 9.0 * nl * t2) * vt * Math.Pow(yn, 4) / 24.0 - (61.0 + 90.0 * t2 + 45 * Math.Pow(t2, 2)) * vt * Math.Pow(yn, 6) / 720.0;
            cbf = 1 / Math.Cos(bf);
            L = cbf * yn - (1.0 + 2.0 * t2 + nl) * cbf * Math.Pow(yn, 3) / 6.0 + (5.0 + 28.0 * t2 + 24.0 * Math.Pow(t2, 2) + 6.0 * nl + 8.0 * nl * t2) * cbf * Math.Pow(yn, 5) / 120.0 + center;

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
        public void  CreateXYLayer()
        {
        try
        {
        //IMxDocument pMxDoc;
        //IMap pMap = axMapControl.Map;
        ////IApplication m_app;     
        //IApplication m_app = default(IApplication);
        ////IApplication m_app = Application;
        // //IMxApplication m_appsd;

        ////pMxDoc = (IMxDocument)m_app.Document;
        ////pMap = pMxDoc.FocusMap;
        //int i =pMap.MapSurroundCount;
            
            IApplication m_app = axMapControl.ActiveView.ScreenDisplay as IApplication;
            //IApplication m_app = ESRI.ArcGIS.ArcMap.Application;
            
            //IApplication m_app = axMapControl.app
            
            
            IMxDocument pMxDoc;
            IMap pMap;
            pMxDoc = (IMxDocument)m_app.Document;
            pMap = pMxDoc.FocusMap;
             

        // Get the table named XYSample.txt
        IStandaloneTableCollection pStTabCol;
        IStandaloneTable pStandaloneTable; 
        ITable pTable = null;
        pStTabCol = (IStandaloneTableCollection) pMap;
        for (int intCount = 0; intCount < pStTabCol.StandaloneTableCount; intCount++)
        {
        pStandaloneTable = (IStandaloneTable) pStTabCol.get_StandaloneTable(intCount);
        if (pStandaloneTable.Name == @"c:\XYSample.txt")
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

        // Get the table name object
        IDataset pDataSet;
        IName pTableName;
        pDataSet = (IDataset) pTable;
        pTableName = pDataSet.FullName;

        // Specify the X and Y fields
        IXYEvent2FieldsProperties pXYEvent2FieldsProperties;
        pXYEvent2FieldsProperties = new XYEvent2FieldsPropertiesClass();
        pXYEvent2FieldsProperties.XFieldName = "x";
        pXYEvent2FieldsProperties.YFieldName = "y";
        pXYEvent2FieldsProperties.ZFieldName = "";

        // Specify the projection
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
        IMapDocument pMapDocument = new MapDocumentClass();
        IEnvelope pEn = new EnvelopeClass();
        object oFillobject = new object();
        private void axMapControl2_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (e.button == 1)//左键画红框
            {
                pEn = axMapControl2.TrackRectangle();
                axMapControl.Extent = pEn;
                axMapControl2.DrawShape(pEn, ref oFillobject);
            }
            if (e.button == 2)//右键拖动红框
            {
                pSmallViewerMouseDownPt = new PointClass();
                pSmallViewerMouseDownPt.PutCoords(e.mapX, e.mapY);
                axMapControl.CenterAt(pSmallViewerMouseDownPt);

                isTrackingSmallViewer = true;
                if (pSmallViewerEnvelope == null)
                {
                    pSmallViewerEnvelope = new MoveEnvelopeFeedbackClass();
                    pSmallViewerEnvelope.Display = axMapControl2.ActiveView.ScreenDisplay;
                    pSmallViewerEnvelope.Symbol = (ISymbol)oFillobject;
                }
                pSmallViewerEnvelope.Start(pEn, pSmallViewerMouseDownPt);
            }
        }

        private void axMapControl2_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            if (isTrackingSmallViewer)
            {
                moveCount++;
                if (moveCount % 4 == 0)//因为一刷新，红框就没了。所以每移动4次就刷新一下，保持红框的连续性。
                    axMapControl2.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
                pSmallViewerMouseDownPt.PutCoords(e.mapX, e.mapY);
                pSmallViewerEnvelope.MoveTo(pSmallViewerMouseDownPt);
            }
        }

        private void axMapControl2_OnMouseUp(object sender, IMapControlEvents2_OnMouseUpEvent e)
        {
            if (pSmallViewerEnvelope != null)
            {
                pEn = pSmallViewerEnvelope.Stop();
                axMapControl.Extent = pEn;
                isTrackingSmallViewer = false;
            }
        }

        private void axMapControl_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            //加载鹰眼
            axMapControl2.LoadMxFile(axMapControl.DocumentFilename);
            axMapControl2.Extent = axMapControl.FullExtent;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FlashFeature();
            //DrawCircle(50, 389327, 1613427);
        }
        /// <summary>
        /// 画线
        /// <param name="arrPointAll">点坐标数组</param>
        ///<returns></returns>
        private void DrawLine(ArrayList arrPointAll)
        {
            //if (arrPointAll.Count <= 0)//点坐标数组不能为空
            //{
            //    return;
            //}

            //if (activeView == null)
            //{
            //    activeView = this.MapControl.ActiveView.FocusMap as IActiveView;
            //}
            ////删除以前的element
            //DeleteOldElement(activeView.GraphicsContainer);

            //// 获取IRGBColor接口
            //IRgbColor color = new RgbColor();
            //// 设置颜色属性
            //color.Red = 255;
            //color.Transparency = 255;

            ////点
            //IPoint pPoint = new PointClass();

            ////线样式
            //ISimpleLineSymbol lineSymbol = new SimpleLineSymbolClass();
            //lineSymbol.Color = color;
            //lineSymbol.Style = esriSimpleLineStyle.esriSLSInsideFrame;
            //lineSymbol.Width = 1;

            ////线元素
            //ILineElement lineElement = new LineElementClass();
            //lineElement.Symbol = lineSymbol;

            ////创建线
            //IPolyline m_Polyline = new PolylineClass();
            ////点集合
            //IPointCollection m_PointCollection = new PolylineClass();
            ////点数组
            //ArrayList arrPoint = new ArrayList();
            //object missing = Type.Missing;
            //foreach (object o in arrPointAll)
            //{
            //    arrPoint = (ArrayList)o;
            //    pPoint.PutCoords(int.Parse(arrPoint[0].ToString()), int.Parse(arrPoint[1].ToString()));
            //    m_PointCollection.AddPoint(pPoint, ref missing, ref missing);
            //}

            ////QI for IPolyline
            //m_Polyline = m_PointCollection as IPolyline;

            ////放大地图

            ////折线范围
            //IEnvelope pEnvelope = m_Polyline.Envelope;
            ////折线区域
            //IArea pArea = pEnvelope as IArea;
            //pPoint = pArea.Centroid;

            //this.ChangeEnvelope(pPoint, 0.06, 0.06);
            ////QI for IElement
            //IElement element = lineElement as IElement;

            //element.Geometry = m_Polyline;

            ////加载线元素到地图
            //activeView.GraphicsContainer.AddElement(element, 0);
            ////Refresh the graphics
            //activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        private IElement DrawCircle(double radius, double x, double y)
        {
            IPoint centralPoint = new PointClass();
            centralPoint.PutCoords(x, y);
            // 创建园
            ICircularArc circularArc = new CircularArcClass();
            IConstructCircularArc construtionCircularArc = circularArc as IConstructCircularArc;
            construtionCircularArc.ConstructCircle(centralPoint, radius, true);

            ISegment pSegment1 = circularArc as ISegment;
            //通过ISegmentCollection构建Ring对象
            ISegmentCollection pSegCollection = new RingClass();
            object o = Type.Missing;
            //添加Segement对象即圆
            pSegCollection.AddSegment(pSegment1, ref o, ref o);

            //QI到IRing接口封闭Ring对象，使其有效
            IRing pRing = pSegCollection as IRing;
            pRing.Close();
            //通过Ring对象使用IGeometryCollection构建Polygon对象
            IGeometryCollection pGeometryColl = new PolygonClass();
            pGeometryColl.AddGeometry(pRing, ref o, ref o);
            //构建一个CircleElement对象
            IElement pElement = new CircleElementClass();
            pElement.Geometry = pGeometryColl as IGeometry;

            IFillShapeElement pFillShapeElement = pElement as IFillShapeElement;

            ISimpleFillSymbol pFillSymbol = new SimpleFillSymbolClass();
            //pFillSymbol.Color = pCircleColor;
            ILineSymbol pLineSymbol = new SimpleLineSymbolClass();
          
          //  pLineSymbol.Color = DefineColor(0, 255, 0);
            pFillSymbol.Outline = pLineSymbol;

            pFillShapeElement.Symbol = pFillSymbol;


            IFillShapeElement circleElement = pElement as IFillShapeElement;
            circleElement.Symbol = pFillSymbol;
            IGraphicsContainer pGC = this.axMapControl.ActiveView.GraphicsContainer;
            pGC.AddElement(pElement, 0);
            axMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            return pElement;
        }

        private void CreateShapefileLine()
        {
            string strFolder = @"C:\NewLine.prj";
           // axMapControl.AddShapeFile(strFolder, "点");
           // axMapControl.AddShapeFile(strFolder, "123");
            //axMapControl.AddLayer(
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Title = "请选择图层文件";
            //openFileDialog.Filter = "图层文件（*.lyr）|*.lyr";
            //if (openFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    ILayerFile pLayerFile = new LayerFileClass();
            //    string path = openFileDialog.FileName;
            //    pLayerFile.Open(path);
            //    axSceneControl1.Scene.AddLayer(pLayerFile.Layer, false);
            //    axSceneControl1.SceneGraph.RefreshViewers();
            //}
            
            //创建图层
            IFeatureLayer pFLayer = new FeatureLayerClass();
            IMap imaps = axMapControl.Map;
                        
            //图层名称
            pFLayer.Name = "123 layer";
            //51
            pFLayer.ScaleSymbols = true;
            //Activate
           
           // pFLayer.Cached = true;
            //加载图层
            axMapControl.AddLayer(pFLayer,1);
            
            //axMapControl.ActiveView.Activate(1);
            //axTOCControl1.ActiveView.Activate(1);
            
            axMapControl.ActiveView.Activate(axMapControl.hWnd);
            //bool bs= axTOCControl1.ActiveView.IsActive();
            //imaps.ActiveGraphicsLayer = pFLayer;
            //移动图层：将制定的图层移动到制定的图层
            //axMapControl.MoveLayerTo(0, 1);
            //axMapControl.ActiveView.IsMapActivated = false;
            axMapControl.Refresh();
            axTOCControl1.Refresh();


           
            //IFeatureWorkspace pFWS;
            //IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactoryClass();
            //pFWS = pWorkspaceFactory.OpenFromFile(strFolder, 0) as IFeatureWorkspace;

            //IFields fields = new FieldsClass();
            //IFieldsEdit fieldsEdit = (IFieldsEdit)fields;
            //fieldsEdit.FieldCount_2 = 5;

            //IGeometryDef geoDef = new GeometryDefClass();
            //IGeometryDefEdit geometryDefEdit = (IGeometryDefEdit)geoDef;
            //geometryDefEdit.AvgNumPoints_2 = 1;
            //geometryDefEdit.GridCount_2 = 0;
            //geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolyline;

            //IField shapeField = new FieldClass();
            //IFieldEdit shapeFieldEdit = (IFieldEdit)shapeField;
            //shapeFieldEdit.Name_2 = "SHAPE";
            //shapeFieldEdit.IsNullable_2 = true;
            //shapeFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            //shapeFieldEdit.GeometryDef_2 = geoDef;
            //shapeFieldEdit.Required_2 = true;

            //SpatialReferenceEnvironment spatialReferenceEnvironment = new SpatialReferenceEnvironment();


            //IGeographicCoordinateSystem geographicCoordinateSystem = spatialReferenceEnvironment.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_NAD1983);
            ////IProjectedCoordinateSystem projectedCoordinateSystem = spatialReferenceEnvironment.CreateProjectedCoordinateSystem((int)esriSRProjCSType.esriSRProjCS_Beijing1954GK_21N);
            //geometryDefEdit.SpatialReference_2 = geographicCoordinateSystem;
            ////geometryDefEdit.SpatialReference_2 = projectedCoordinateSystem;

            //IField oidField = new FieldClass();
            //IFieldEdit oidFieldEdit = (IFieldEdit)oidField;
            //oidFieldEdit.Name_2 = "123123123ObjectID";
            //oidFieldEdit.AliasName_2 = "FID";
            //oidFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;

            //IField FieldXlmc = new FieldClass();
            //IFieldEdit FieldEditXlmc = (IFieldEdit)FieldXlmc;
            //FieldEditXlmc.Name_2 = "xlmc";
            //FieldEditXlmc.AliasName_2 = "xlmc";
            //FieldEditXlmc.Type_2 = esriFieldType.esriFieldTypeString;
            //FieldEditXlmc.Length_2 = 10;

            //IField FieldGtxh = new FieldClass();
            //IFieldEdit FieldEditGtxh = (IFieldEdit)FieldGtxh;
            //FieldEditGtxh.Name_2 = "gtxh";
            //FieldEditGtxh.AliasName_2 = "gtxh";
            //FieldEditGtxh.Type_2 = esriFieldType.esriFieldTypeString;
            //FieldEditGtxh.Length_2 = 10;

            //IField FieldWddj = new FieldClass();
            //IFieldEdit FieldEditWddj = (IFieldEdit)FieldWddj;
            //FieldEditWddj.Name_2 = "wddj";
            //FieldEditWddj.AliasName_2 = "wddj";
            //FieldEditWddj.Type_2 = esriFieldType.esriFieldTypeString;
            //FieldEditWddj.Length_2 = 10;

            //fieldsEdit.set_Field(0, oidField);
            //fieldsEdit.set_Field(1, shapeField);
            //fieldsEdit.set_Field(2, FieldXlmc);
            //fieldsEdit.set_Field(3, FieldGtxh);
            //fieldsEdit.set_Field(4, FieldWddj);
            //try
            //{
            //    IFeatureClass featureClass = pFWS.CreateFeatureClass("NewLine", fields, null, null, esriFeatureType.esriFTSimple, "shape", null);
            //    //axMapControl.AddShapeFile(strFolder + "NewLine.prj", "点");


            //    MessageBox.Show("新线路图层建立成功!!");
            //    axTOCControl1.Refresh();
            //    this.axMapControl.Refresh();//刷新地图  
            //}
            //catch
            //{
            //    MessageBox.Show("该图层已经存在，请先删除原来的图层!!");
            //}
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CreateShapefileLine();
           // CreateLine();
        }

        public void CreateLine()
        {
            ILayer pLayer = this.axMapControl.get_Layer(1);//所要加的层  
            
            IFeatureLayer pFeatureLyr = pLayer as IFeatureLayer;//将ILayer转换为IFeaturelayer，为了对图层上的要素进行编辑  
            IFeatureClass pFeatCls = pFeatureLyr.FeatureClass;//定义一个要素集合，并获取图层的要素集合  
            IFeatureClassWrite fr = (IFeatureClassWrite)pFeatCls;//定义一个实现新增要素的接口实例，并该实例作用于当前图层的要素集  
            IWorkspaceEdit w = (pFeatCls as IDataset).Workspace as IWorkspaceEdit;//定义一个工作编辑工作空间，用于开启前图层的编辑状态  
            IFeature f;//定义一个IFeature实例，用于添加到当前图层上  
            w.StartEditing(true);//开启编辑状态  
            w.StartEditOperation();//开启编辑操作  
            IPoint p;//定义一个点，用来作为IFeature实例的形状属性，即shape属性  
            //下面是设置点的坐标和参考系  
            p = new PointClass();
            p.SpatialReference = this.axMapControl.SpatialReference;
            p = GetProject2(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text));

            //将IPoint设置为IFeature的shape属性时，需要通过中间接口IGeometry转换  
            IGeometry peo;
            peo = p;
            f = pFeatCls.CreateFeature();//实例化IFeature对象， 这样IFeature对象就具有当前图层上要素的字段信息  
            f.Shape = peo;//设置IFeature对象的形状属性  
            f.set_Value(0, "house1");//设置IFeature对象的索引是3的字段值  
            //f.set_Value(3, "Marker01");//设置IFeature对象的索引是3的字段值  
            
            f.Store();//保存IFeature对象  
            fr.WriteFeature(f);//将IFeature对象，添加到当前图层上  
            w.StopEditOperation();//停止编辑操作  
            w.StopEditing(true);//关闭编辑状态，并保存修改  
            this.axMapControl.Refresh();//刷新地图  
        }

        private void button7_Click(object sender, EventArgs e)
        {
           
            //CreateLines();
            //CreateLine();
            ArrayList arrlis = new ArrayList();
            //arrlis.Add(-1520445);
            //arrlis.Add(1038916);
            //arrlis.Add(389327);
            //arrlis.Add(1613427);
            arrlis.Add(500);
            arrlis.Add(2);
            //arrlis.Add(3);
            //arrlis.Add(4);
            DrawLine2(arrlis);
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
            //insertpoint = true;
        }
        /// <summary>
        /// 画线
        /// <param name="arrPointAll">点坐标数组</param>
        ///<returns></returns>
        private void DrawLine2(ArrayList arrPointAll)
        {
            if (arrPointAll.Count <= 0)//点坐标数组不能为空
            {
                return;
            }


            IActiveView activeView = this.axMapControl.ActiveView.FocusMap as IActiveView;
            
            //删除以前的element
            //DeleteOldElement(activeView.GraphicsContainer);

            // 获取IRGBColor接口
            IRgbColor color = new RgbColor();
            // 设置颜色属性
            color.Red = 255;
            color.Transparency = 255;

            //点
            IPoint pPoint = new PointClass();

            //线样式
            ISimpleLineSymbol lineSymbol = new SimpleLineSymbolClass();
            lineSymbol.Color = color;
            lineSymbol.Style = esriSimpleLineStyle.esriSLSInsideFrame;
            lineSymbol.Width = 10;

            //线元素
            ILineElement lineElement = new LineElementClass();
            lineElement.Symbol = lineSymbol;

            //创建线
            IPolyline m_Polyline = new PolylineClass();
            //点集合
            IPointCollection m_PointCollection = new PolylineClass();
            //点数组
            ArrayList arrPoint = new ArrayList();
            object missing = Type.Missing;
            
            for (int i = 0; i < arrPointAll.Count; i++)
            {
                //pPoint.PutCoords(int.Parse(arrPointAll[i].ToString()), int.Parse(arrPointAll[i].ToString()));
                pPoint.PutCoords(-1520445, 1038916);
                m_PointCollection.AddPoint(pPoint, ref missing, ref missing);
                pPoint.PutCoords(389327, 1613427);
                m_PointCollection.AddPoint(pPoint, ref missing, ref missing);
            }
                //QI for IPolyline
                m_Polyline = m_PointCollection as IPolyline;

            //放大地图

            //折线范围
            IEnvelope pEnvelope = m_Polyline.Envelope;
            //折线区域
            IArea pArea = pEnvelope as IArea;
            pPoint = pArea.Centroid;

           // this.ChangeEnvelope(pPoint, 0.06, 0.06);
            //QI for IElement
            IElement element = lineElement as IElement;

            element.Geometry = m_Polyline;

            //加载线元素到地图
            activeView.GraphicsContainer.AddElement(element, 0);
            //Refresh the graphics
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }
        /// 将平面坐标转换为经纬度。
        //private IPoint GetGeo(double x, double y)
        //{
        //    flatref = pfactory.CreateProjectedCoordinateSystem(54013);
        //    IPoint pt = new PointClass();

        //    pt.PutCoords(x, y);

        //    IGeometry geo = (IGeometry)pt;
        //    geo.SpatialReference = flatref;
        //    geo.Project(earthref);
        //    double xx = pt.X;
        //    return pt;
        //}
    }
}

