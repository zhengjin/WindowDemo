using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;

namespace ArcGISAddInShowMap
{
    public class ButtonShowMap : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public ButtonShowMap()
        {
        }

        protected override void OnClick()
        {
            //
            //  TODO: Sample code showing how to access button host
            //
            //ArcMap.Application.CurrentTool = null;
            UID winID = new UIDClass();
            winID.Value = ThisAddIn.IDs.ShowMapDockableWindow;
            IDockableWindow psalceWin = ArcMap.DockableWindowManager.GetDockableWindow(winID);

            if (!psalceWin.IsVisible())
            {
                psalceWin.Show(true);
            }
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}
