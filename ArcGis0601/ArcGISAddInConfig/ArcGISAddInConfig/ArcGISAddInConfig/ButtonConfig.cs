using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ArcGISAddInConfig
{
    public class ButtonConfig : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public ButtonConfig()
        {
        }

        protected override void OnClick()
        {
            //
            //  TODO: Sample code showing how to access button host
            //
            //ArcMap.Application.CurrentTool = null;

            ArcMap.Application.CurrentTool = null;
            ConfigForm frm = null;
            if (frm == null || frm.IsDisposed)
                frm = new ConfigForm();

            frm.Show();
            frm.TopMost = true;
            frm.Width = 428;
            frm.Height = 300;
            frm.Left = 500;

            
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}
