using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;

namespace LinGIS
{
    public partial class Form1 : Form
    {
        public Form1()
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
            
            //IMap.ClipGeometry Property;


            string filePath = @"E:\Downloads\ARCgis\United States\USA Base Map.mxd";
            if (axMapControl1.CheckMxFile(filePath))
                axMapControl1.LoadMxFile(filePath, Type.Missing, Type.Missing);
            IPoint point = new PointClass();
            point.X = -100;
            point.Y = 40000;

            System.Drawing.Point p = new System.Drawing.Point(101, 37);
            
           // axMapControl1.CenterAt(point);
            //IEnvelope pEnvelope = this.axMapControl1.Extent;
            axMapControl1.PointToClient(p);
            //pEnvelope.CenterAt(point);
            //this.axMapControl1.Extent = pEnvelope; 

            //axMapControl1.ActiveView.ScreenDisplay.RotateMoveTo(point);
            ////Draw the rotated display.
            //axMapControl1.ActiveView.ScreenDisplay.RotateTimer();

            axMapControl1.Refresh();
        }
        static void SelectFeaturesScreenPoint(IMap pMap, int x, int y, int pixelTol)
        {
            tagRECT r;
            //Construct a small rectangle out of the x,y coordinates' pixel tolerance.
            r.left = x - pixelTol; //Upper left x, top left is 0,0.  
            r.top = y - pixelTol; //Upper left y, top left is 0,0.
            r.right = x + pixelTol; //Lower right x, top left is 0,0. 
            r.bottom = y + pixelTol; //Lower right y, top left is 0,0.

            //Transform the device rectangle into a geographic rectangle via the display transformation.  
            IEnvelope pEnvelope = new EnvelopeClass();
            IActiveView pActiveView = pMap as IActiveView;
            IDisplayTransformation pDisplayTrans = pActiveView.ScreenDisplay.DisplayTransformation;
            pDisplayTrans.TransformRect(pEnvelope, ref r, 5);
            //5 = esriTransformPosition + esriTransformToMap.

            pEnvelope.SpatialReference = pMap.SpatialReference;


            

            //ISelectionEnvironment pSelectionEnvironment = new SelectionEnvironmentClass();
            //pSelectionEnvironment.CombinationMethod =
            //    esriSelectionResultEnum.esriSelectionResultNew;
            //pMap.SelectByShape(pEnvelope, pSelectionEnvironment, false);
        }


    }
}
