using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace AETest
{
    public partial class GotoXY : Form
    {
        AxMapControl pAxMapControl;
        IMap map;
        IActiveView activeview;
        IScreenDisplay pScreenDisplay;
        IGraphicsContainer pGraphicsContainer;

        public GotoXY(AxMapControl ax)
        {
            InitializeComponent();
            pAxMapControl = ax;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            IEnvelope pEnvelope = new EnvelopeClass();
            IPoint pPoint = new PointClass();
            pPoint.PutCoords(Convert.ToDouble(txtlong.Text), Convert.ToDouble(txtlat.Text));

            pEnvelope = pAxMapControl.Extent;
            pEnvelope.CenterAt(pPoint);
            pAxMapControl.Extent = pEnvelope.Envelope;
            pAxMapControl.FlashShape(pPoint as IGeometry);
            //ccc(pPoint);
            //ddd(pPoint);
        }
        private void ccc(IPoint p)
        {
            IEnvelope pEnvelope = new EnvelopeClass();
            pEnvelope.SetEmpty();
            pEnvelope = pAxMapControl.Extent;
            pEnvelope.CenterAt(p);
            pAxMapControl.Extent = pEnvelope.Envelope;
            pAxMapControl.Refresh();
        }
        private void ddd( IPoint p)
        {
            pAxMapControl.FlashShape(p as IGeometry);
        }
        //private void GetLongLat(float jing, float wei, out float Long, float lat)
        //{

        //}

        private void GotoXY_Load(object sender, EventArgs e)
        {
            activeview = pAxMapControl.Map as IActiveView;
            map = activeview.FocusMap;
            pScreenDisplay = activeview.ScreenDisplay;
            pGraphicsContainer = map as IGraphicsContainer;
            IGraphicsContainerSelect pGraphconSel = map as IGraphicsContainerSelect;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            IPoint pPoint = new PointClass();
            pPoint.PutCoords(Convert.ToDouble(txtlong.Text), Convert.ToDouble(txtlat.Text));
            pAxMapControl.FlashShape(pPoint as IGeometry);
            //pAxMapControl.ActiveView.ScreenDisplay.DrawPoint(pPoint);

            //pAxMapControl.FromMapPoint(pPoint);
            //pAxMapControl.PointToScreen()
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            IPoint pPoint = new PointClass();
            pPoint.PutCoords(Convert.ToDouble(txtlong.Text), Convert.ToDouble(txtlat.Text));
            IEnvelope pEnvelope = new EnvelopeClass();
            pEnvelope = pAxMapControl.Extent;
            //pEnvelope.Expand(10, -10, true);
            pEnvelope.Width = 20;
            pEnvelope.Height = 15;
            pEnvelope.CenterAt(pPoint);
            pAxMapControl.Extent = pEnvelope.Envelope;
            pAxMapControl.FlashShape(pPoint as IGeometry);
        }
    }
}