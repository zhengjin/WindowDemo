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
    /// 在图层过滤器中显示的图层的属性
    /// </summary>
    internal class LayerFilterProperties
    {
        private string                  layerFilterName;
        //private Image                   headerImage;
        private string                  layerCategory;
        private LayerFeatureType        layerFeatureType;
        private int                     layerFilterIndex;
        private ILayer                  targetLayer;
        //private MapWindow               mapWindow;
        //////////////////////////////////////////////////////


        #region < 上述私有变量的属性 >

        /// <summary>
        /// 获取或设置过滤器对应的图层对象
        /// </summary>
        public ILayer TargetLayer
        {
            get { return targetLayer; }
            set { targetLayer = value; }
        }
        /// <summary>
        /// 获取或设置图层在下拉列表中次序
        /// </summary>
        public int LayerFilterIndex
        {
            get { return layerFilterIndex; }
            set { layerFilterIndex = value; }
        }
        /// <summary>
        /// 获取或设置对应的图层显示名称
        /// </summary>
        public string LayerFilterName
        {
            get { return layerFilterName; }
            set { layerFilterName = value; }
        }
        /// <summary>
        /// 获取或设置对应的图层的显示图标
        /// </summary>
        //public Image HeaderImage
        //{
        //    get { return headerImage; }
        //    set { headerImage = value; }
        //}
        /// <summary>
        /// 获取或设置对应的图层的分级结构
        /// </summary>
        public string LayerCategory
        {
            get { return layerCategory; }
            set { layerCategory = value; }
        }
        /// <summary>
        /// 获取或设置对应的图层的要素类型
        /// </summary>
        public LayerFeatureType LayerFeatureType
        {
            get { return layerFeatureType; }
            set { layerFeatureType = value; }
        }
        /// <summary>
        /// 设置查询所在的地图窗口
        /// </summary>
        //public MapWindow MapWindow 
        //{
        //    set { mapWindow = value; }
        //}

        #endregion
    }
}
