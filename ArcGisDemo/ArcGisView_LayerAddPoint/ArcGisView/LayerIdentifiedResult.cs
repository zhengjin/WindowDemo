using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace ArcGisView
{
    /// <summary>
    /// 对某个单一图层查询的结果
    /// </summary>
    public class LayerIdentifiedResult 
    {
        private ILayer                          identifyLayer;
        private LayerFeatureType                geometryType;
        private List<IFeatureIdentifyObj>       identifiedFeatureObjList;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public LayerIdentifiedResult() 
        {
            //新建结果列表对象
            identifiedFeatureObjList = new List<IFeatureIdentifyObj>();
        }
        /// <summary>
        /// 图层要素的集合类型
        /// </summary>
        public LayerFeatureType GeometryType
        {
            get { return geometryType; }
            set { geometryType = value; }
        }
        /// <summary>
        /// 查询的对象图层
        /// </summary>
        public ILayer IdentifyLayer
        {
            get { return identifyLayer; }
            set { identifyLayer = value; }
        }
        /// <summary>
        /// 此图层的要素查询列表
        /// </summary>
        public List<IFeatureIdentifyObj> IdentifiedFeatureObjList
        {
            get { return identifiedFeatureObjList; }
        }
    }
}
