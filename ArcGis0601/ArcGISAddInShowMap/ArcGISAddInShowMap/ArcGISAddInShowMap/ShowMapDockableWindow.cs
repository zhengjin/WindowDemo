using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using System.Collections;

namespace ArcGISAddInShowMap
{
    /// <summary>
    /// Designer class of the dockable window add-in. It contains user interfaces that
    /// make up the dockable window.
    /// </summary>
    public partial class ShowMapDockableWindow : UserControl
    {
        IRgbColor colors = new RgbColorClass();
        public ShowMapDockableWindow(object hook)
        {

            InitializeComponent();
            this.Hook = hook;
            //***************************
            this.CompanyBing();
         
           
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
            private ShowMapDockableWindow m_windowUI;

            public AddinImpl()
            {
            }

            protected override IntPtr OnCreateChild()
            {
                m_windowUI = new ShowMapDockableWindow(this.Hook);
                return m_windowUI.Handle;
            }

            protected override void Dispose(bool disposing)
            {
                if (m_windowUI != null)
                    m_windowUI.Dispose(disposing);
                
                base.Dispose(disposing);
            }
        }

        #region OK 查询
        private void buOk_Click(object sender, EventArgs e)
        {
            if (com_Company.Text == "" || com_Route.Text == "")
            {
                return;
            }
            DBClass dbclass = new DBClass();
            DataSet ds = null;
            string strSql = string.Empty;
            strSql = "select * from ViewGISRoute where GISLatitude!=''";
            string strAnd = string.Empty;
            if (com_Company.Text != string.Empty)
            {
                strAnd = " and Comp='"+com_Company.Text+"'";
            }
            strSql += strAnd + " order by Seq";
           
            ds = dbclass.GetDs(strSql);
             DrawLine(ds, colors);

            // ShowInformation(ds);
        }    
        #endregion    

        #region Company加载数据
        /// <summary>
        /// Company加载数据
        /// </summary>
        public void CompanyBing()
        {
            DBClass dbclass = new DBClass();
            DataSet ds = null;
            string strSql = string.Empty;
            strSql = "select distinct Comp from ViewGISRoute";
            ds = dbclass.GetDs(strSql);
            //com_Company.DataSource = ds.Tables[0];
            //com_Company.DisplayMember = "Comp";
            //com_Company.ValueMember = "Comp";
            com_Company.Items.Insert(0,"");
            //com_Company.Items.Add("");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i <ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i][0] != null)
                    {
                        com_Company.Items.Insert(i, ds.Tables[0].Rows[i][0].ToString());
                    }
                }
            }
        }
        #endregion

        #region 显示信息
        public void ShowInformation(DataSet ds)
        {
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string strname = string.Empty;
                    string strbalance = string.Empty;
                    string strAcctNum = string.Empty;
                    double dx = 0;
                    double dy = 0;
                    int index = 0;
                    if (ds.Tables[0].Rows[i]["GISLongitude"] != null)
                    {
                        dx = Convert.ToDouble(ds.Tables[0].Rows[i]["GISLongitude"]);
                    }
                    if (ds.Tables[0].Rows[i]["GISLatitude"] != null)
                    {
                        dy = Convert.ToDouble(ds.Tables[0].Rows[i]["GISLatitude"]);
                    }
                    if (ds.Tables[0].Rows[i]["Seq"] != null)
                    {
                        index = Convert.ToInt32(ds.Tables[0].Rows[i]["Seq"]);
                    }
                    if (ds.Tables[0].Rows[i]["AcctNum"] != null)
                    {
                        strAcctNum = ds.Tables[0].Rows[i]["AcctNum"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["name"] != null)
                    {
                        strname = ds.Tables[0].Rows[i]["name"].ToString();
                    }
                    //string strbalance = ds.Tables[0].Rows[i]["balance"].ToString();
                    if (ds.Tables[0].Rows[i]["balance"] != null && ds.Tables[0].Rows[i]["balance"].ToString() != string.Empty && ds.Tables[0].Rows[i]["balance"].ToString().Length > 2)
                    {
                        strbalance = ds.Tables[0].Rows[i]["balance"].ToString().Substring(0, ds.Tables[0].Rows[i]["balance"].ToString().Length - 2);
                    }
                    string Information = string.Empty;
                    Information = "Account Num:" + strAcctNum + "\nName:" + strname + "\nbalance:" + strbalance;
                    //标记
                    IPoint pPoints = new PointClass();
                    //调用GetProject2方法经纬度转换成米
                    pPoints = GetProject(dx, dy);
                    CreateTextElmentInformation(pPoints.X, pPoints.Y, Information);
                }
            }
        }
        #endregion

        #region 点击事件，选择颜色
        private void com_Color_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                // 将先中的颜色设置为窗体的背景色
                //this.BackColor = colorDialog1.Color;
                this.com_Color.BackColor = colorDialog1.Color;
                 //pColor = GetRGB(colorDialog1.Color);
                colors = GetRGB(colorDialog1.Color);
            }
        }
        #endregion

        private IRgbColor GetRGB(Color pColor)
        {
            IRgbColor color = new RgbColorClass();
            color.Red = pColor.R;
            color.Green = pColor.G;
            color.Blue = pColor.B;
            //return (color as IColor);
            return color;
        }
        #region 根据提供的数据画Route
        private void DrawLine(DataSet ds, IRgbColor color)
        {
            IMxDocument doc = ArcMap.Document;
            IMap map = doc.FocusMap;
            IActiveView activeView = doc.ActiveView.FocusMap as IActiveView;
            IPoint pPoint = new PointClass();

            //线样式
            ISimpleLineSymbol lineSymbol = new SimpleLineSymbolClass();
            lineSymbol.Color = color;
            lineSymbol.Style = esriSimpleLineStyle.esriSLSDashDotDot;
            lineSymbol.Width = 2;

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

            //while (sqlread.Read()) //前进一条纪录
            //{
            //    int i = sqlread.GetInt32(0);
            //    double x = sqlread.GetDouble(1);
            //    double y = sqlread.GetDouble(2);
            //    int index = sqlread.GetInt32(3);
            //    //标记
            //    IPoint pPoints = new PointClass();
            //    //调用GetProject2方法经纬度转换成米
            //    pPoints = GetProject(x, y);
            //    CreateTextElment(pPoints.X, pPoints.Y, index);

            //    pPoint.PutCoords(Convert.ToDouble(pPoints.X), Convert.ToDouble(pPoints.Y));
            //    m_PointCollection.AddPoint(pPoint, ref missing, ref missing);
            //}
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //画线
                    double dx = Convert.ToDouble(ds.Tables[0].Rows[i]["GISLongitude"]);
                    double dy = Convert.ToDouble(ds.Tables[0].Rows[i]["GISLatitude"]);
                    int index = Convert.ToInt32(ds.Tables[0].Rows[i]["Seq"]);                    
                    IPoint pPoints = new PointClass();
                    //调用GetProject2方法经纬度转换成米
                    pPoints = GetProject(dx, dy);
                    pPoint.PutCoords(Convert.ToDouble(pPoints.X), Convert.ToDouble(pPoints.Y));
                    m_PointCollection.AddPoint(pPoint, ref missing, ref missing);                  
                }
            }

            m_Polyline = m_PointCollection as IPolyline;

            //折线范围
            IEnvelope pEnvelope = m_Polyline.Envelope;
            //折线区域
            IArea pArea = pEnvelope as IArea;
            pPoint = pArea.Centroid;
            IElement element = lineElement as IElement;
            element.Geometry = m_Polyline;

            //加载线元素到地图
            activeView.GraphicsContainer.AddElement(element, 0);
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            activeView.Refresh();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {                   
                    int index = Convert.ToInt32(ds.Tables[0].Rows[i]["Seq"]);
                    double dx = Convert.ToDouble(ds.Tables[0].Rows[i]["GISLongitude"]);
                    double dy = Convert.ToDouble(ds.Tables[0].Rows[i]["GISLatitude"]);

                    //画标记
                    IPoint pPoints = new PointClass();
                    //调用GetProject2方法经纬度转换成米
                    pPoints = GetProject(dx, dy);
                    if (!check_DatailInformation.Checked)
                    {
                        CreateTextElment(pPoints.X, pPoints.Y, index);
                    }                    
                }
            }
            if (check_DatailInformation.Checked)
            {
                ShowInformation(ds);
            }
            activeView.Refresh();
        }
        #endregion

        #region 将经纬度点转换为平面坐标。
        /// 将经纬度点转换为平面坐标。
        private IPoint GetProject(double x, double y)
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
            ((ITextElement)pTElement).Text = index.ToString();
            pPoint.X = x + 420;
            pPoint.Y = y + 420;
            //axMapControl.CenterAt(pPoint);
            pTElement.Geometry = pPoint;
            pGraphicsContainer.AddElement(pTElement, 1);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        public void CreateTextElmentInformation(double x, double y, string Information)
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
            //pColor.Red = 150;
            pColor.Red = 255;
            pColor.Green = 0;
            pColor.Blue = 0;
            pTextSymbol.Color = pColor;
            ITextBackground pTextBackground; 
            pTextBackground = (ITextBackground)pBalloonCallout;
            pTextSymbol.Background = pTextBackground;
            ((ITextElement)pTElement).Symbol = pTextSymbol;
            ((ITextElement)pTElement).Text = Information;
            pPoint.X = x + 420;
            pPoint.Y = y + 420;
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

        #region Company选择事件
                
        private void com_Company_SelectedIndexChanged(object sender, EventArgs e)
        {
            //com_Route.Items.Insert(0, "");
            DBClass dbclass = new DBClass();
            DataSet ds = null;
            string strSql = string.Empty;
            string strAnd = string.Empty;
            if (com_Company.Text != string.Empty)
            {
                strAnd = " and Comp='" + com_Company.Text + "'";
            }
            strSql = "select  distinct Route from ViewGISRoute where 1=1"+strAnd;
            ds = dbclass.GetDs(strSql);
            com_Route.DataSource = ds.Tables[0];
            com_Route.DisplayMember = "Route";
            com_Route.ValueMember = "Route";
        }
        #endregion
    }
}
