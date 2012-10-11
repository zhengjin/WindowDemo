using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;

namespace ArcGisView
{
    /// <summary>
    /// 图层类型列表
    /// </summary>
    public enum LayerFeatureType 
    {
        None,
        Raster,
        Point,
        Polyline,
        Polygon,
        GroupLayer
    }
}
