using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ArcGisDemo
{
    public partial class setCenter : Form
    {
        public setCenter()
        {
            InitializeComponent();

            Initialize();
        }

        private void Initialize()
        {
            string sFilePath;
            sFilePath = @"E:\Downloads\ARCgis\United States\USA Base Map.mxd";
            OpenDocument(sFilePath);

            //ESRI.ArcGIS.ADF.Web.Geometry.Point centerpoint = ESRI.ArcGIS.ADF.Web.Geometry.Geometry.GetCenterPoint(geo);
            //axMapControl.CenterAt(centerpoint);
        }

        private void OpenDocument(string sFilePath)
        {
            if (axMapControl.CheckMxFile(sFilePath))
                axMapControl.LoadMxFile(sFilePath, Type.Missing, Type.Missing);

            //CreateTextElment(101, 37);

            //CreatePoint(101, 37);
        }
    }
}
