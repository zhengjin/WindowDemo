using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using ESRI.ArcGIS.ArcMapUI;



using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesGDB;
using System.Collections;



namespace ArcGISAddInDemo02
{
    /// <summary>
    /// Designer class of the dockable window add-in. It contains user interfaces that
    /// make up the dockable window.
    /// </summary>
    public partial class DockableWindow1demo : UserControl
    {
        public DockableWindow1demo(object hook)
        {
            InitializeComponent();
            this.Hook = hook;
        }

        /// <summary>
        /// Host object of the dockable window
        /// </summary>
        private object Hook
        {
            get;
            set;
        }

        /// <summary>
        /// Implementation class of the dockable window add-in. It is responsible for 
        /// creating and disposing the user interface class of the dockable window.
        /// </summary>
        public class AddinImpl : ESRI.ArcGIS.Desktop.AddIns.DockableWindow
        {
            private DockableWindow1demo m_windowUI;

            public AddinImpl()
            {
            }

            protected override IntPtr OnCreateChild()
            {
                m_windowUI = new DockableWindow1demo(this.Hook);
                return m_windowUI.Handle;
            }

            protected override void Dispose(bool disposing)
            {
                if (m_windowUI != null)
                    m_windowUI.Dispose(disposing);

                base.Dispose(disposing);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 获取IRGBColor接口
            IRgbColor color = new RgbColor();
            
            
            string strcomvalue = comboBox1.SelectedItem.ToString();
            if (strcomvalue != string.Empty)
            {
                string strwhere = string.Empty;
                if (strcomvalue == "Route01")
                {
                    // 设置颜色属性
                    color.Red = 255;
                    color.Green = 0;
                    color.Blue = 0;
                    color.Transparency = 255;
                    strwhere = " where routetype='01'";
                }
                if (strcomvalue == "Route02")
                {
                    // 设置颜色属性
                    color.Red = 152;
                    color.Green = 251;
                    color.Blue = 152;
                    color.Transparency = 255;
                    strwhere = " where routetype='02'";
                }
                if (strcomvalue == "Route03")
                {
                    // 设置颜色属性
                    color.Red = 238;
                    color.Green = 150;
                    color.Blue = 238;
                    color.Transparency = 255;
                    color.Transparency = 255;
                    strwhere = " where routetype='03'";
                }
                SqlConnection sqlconn = new SqlConnection("database=JobTest;user id=sa;password=sqlserver; server=dev-db;");
                SqlCommand insertcomm = new SqlCommand("select * from ArcTest "+strwhere+" order by orders", sqlconn);
                sqlconn.Open(); //打开与数据库的连接
                //SqlDataReader sqlread = insertcomm.ExecuteReader(); //发送命令到数据库
                SqlDataReader sqlread = insertcomm.ExecuteReader(); //发送命令到数据库                
                DrawLine(sqlread,color);
                sqlread.Close();
                sqlconn.Close(); //关闭与数据库的连接
                IMxDocument doc = ArcMap.Document;
                doc.ActiveView.Refresh();
            }
        }

        private void DrawLine(SqlDataReader sqlread,IRgbColor color)
        {


            IMxDocument doc = ArcMap.Document;
            IMap map = doc.FocusMap;


            //IActiveView activeView = this.axMapControl.ActiveView.FocusMap as IActiveView;
            IActiveView activeView = doc.ActiveView.FocusMap as IActiveView;

            //删除以前的element
            //DeleteOldElement(activeView.GraphicsContainer);
           // DeleteOldElement(activeView.GraphicsContainer);
            
            //// 获取IRGBColor接口
            //IRgbColor color = new RgbColor();
            //// 设置颜色属性
            //color.Red = 255;
            //color.Transparency = 255;

            //点
            IPoint pPoint = new PointClass();

            //线样式
            ISimpleLineSymbol lineSymbol = new SimpleLineSymbolClass();
            lineSymbol.Color = color;

            

            //lineSymbol.Style = esriSimpleLineStyle.esriSLSInsideFrame;
            lineSymbol.Style = esriSimpleLineStyle.esriSLSDashDotDot;
            lineSymbol.Width = 3;


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




            while (sqlread.Read()) //前进一条纪录
            {
                int i = sqlread.GetInt32(0);
                double x = sqlread.GetDouble(1);
                double y = sqlread.GetDouble(2);
                int index = sqlread.GetInt32(3);
                //标记
                IPoint pPoints = new PointClass();
                //调用GetProject2方法经纬度转换成米
                pPoints = GetProject2(x, y);
                CreateTextElment(pPoints.X, pPoints.Y, index);

                pPoint.PutCoords(Convert.ToDouble(pPoints.X), Convert.ToDouble(pPoints.Y));
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
            activeView.Refresh();


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


        #region 标记
        public void CreateTextElment(double x, double y, int index)
        {

            IMxDocument doc = ArcMap.Document;
            IMap map = doc.FocusMap;

            IPoint pPoint = new PointClass();
            //IMap pMap = axMapControl.Map;
            IMap pMap = doc.FocusMap;
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
            ((ITextElement)pTElement).Text = index.ToString(); ;
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


    }
}
