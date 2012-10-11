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

namespace ArcGisDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Initialize();
        }

        private void Initialize()
        {
            string sFilePath;
            sFilePath = @"";
            OpenDocument(sFilePath);
        }

        private void OpenDocument(string sFilePath)
        {
            if (axMapControl.CheckMxFile(sFilePath))
                axMapControl.LoadMxFile(sFilePath, Type.Missing, Type.Missing);

            IMap pMap = axMapControl.Map;
            //DrawPoint(pMap as IActiveView,100,54);
            //CreateTextElment(97,35);
            FlashFeature();
            //CreatePoint(101, 37);
        }

        #region 验证正确的代码
        //提示框
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
            pTextSymbol.Background = pTextBackground;
            ((ITextElement)pElement).Symbol = pTextSymbol;
            ((ITextElement)pElement).Text = "测试";
            pPoint.X = x;
            pPoint.Y = y;
            pElement.Geometry = pPoint;
            pGraphicsContainer.AddElement(pElement, 1);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        /// <summary>
        /// 画线
        /// </summary>
        internal void FlashFeature()
        {
            IPoint point1 = new PointClass();
            point1.PutCoords(101, 40);
            IPoint point2 = new PointClass();
            point2.PutCoords(110, 40);
            IPoint point3 = new PointClass();
            point3.PutCoords(112, 40);

            object o = Type.Missing;
            IPointCollection pointCollection = new PolygonClass();
            pointCollection.AddPoint(point1, ref o, ref o);
            pointCollection.AddPoint(point2, ref o, ref o);
            pointCollection.AddPoint(point3, ref o, ref o);
            IPolygon polygon = pointCollection as IPolygon;

            IElement element = new PolygonElementClass();
            element.Geometry = polygon;
            IGraphicsContainer graphicsContainer = axMapControl.Map as IGraphicsContainer;
            graphicsContainer.AddElement(element, 0);
            axMapControl.Refresh();
        }
        #endregion

        

        #region 各种标记
        
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

        public void DrawPoint(ESRI.ArcGIS.Carto.IActiveView activeView, System.Int32 x, System.Int32 y)
        {

            if (activeView == null)
            {
                return;
            }
            ESRI.ArcGIS.Display.IScreenDisplay screenDisplay = activeView.ScreenDisplay;


            // Constant
            screenDisplay.StartDrawing(screenDisplay.hDC, (System.Int16)ESRI.ArcGIS.Display.esriScreenCache.esriNoScreenCache); // Explicit Cast
            ESRI.ArcGIS.Display.ISimpleMarkerSymbol simpleMarkerSymbol = new ESRI.ArcGIS.Display.SimpleMarkerSymbolClass();

            ESRI.ArcGIS.Display.ISymbol symbol = simpleMarkerSymbol as ESRI.ArcGIS.Display.ISymbol; // Dynamic Cast
            screenDisplay.SetSymbol(symbol);
            ESRI.ArcGIS.Display.IDisplayTransformation displayTransformation = screenDisplay.DisplayTransformation;

            // x and y are in device coordinates
            ESRI.ArcGIS.Geometry.IPoint point = displayTransformation.ToMapPoint(x, y);


            screenDisplay.DrawPoint(point);
            screenDisplay.FinishDrawing();
        }
    }
}
