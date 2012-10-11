using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.ArcMapUI;

namespace ArcGISAddInDemo02
{
    public class Tool1demo : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        public Tool1demo()
        {
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
        protected override void OnMouseDown(ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs arg)
        {
            int intx = arg.X;
            int inty = arg.Y;
            IPoint pPoints = new PointClass();
            pPoints.PutCoords(intx, inty);
            



            IActiveView activeView = ArcMap.Document.ActiveView;
            pPoints = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(-100, 40);//x,y为屏幕坐标


            ESRI.ArcGIS.Display.IScreenDisplay screenDisplay = activeView.ScreenDisplay;
            ESRI.ArcGIS.Display.IDisplayTransformation displayTransformation = screenDisplay.DisplayTransformation;
            pPoints=displayTransformation.ToMapPoint(-120, 40); 


            IMxDocument doc = ArcMap.Document;
            IMap map = doc.FocusMap;
            pPoints=doc.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(-100, 40);
           
            AlterForm frm = null;
            if (frm == null || frm.IsDisposed)
                frm = new AlterForm(intx,inty);

            frm.Show();
            frm.TopMost = true;
            frm.Width = 400;
            frm.Height = 300;
            frm.Left = 500;
            
        }

    }

}
