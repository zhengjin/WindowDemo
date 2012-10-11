using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace ArcGisView
{
    /// <summary>
    /// 属性查询类
    /// 查询数据的属性
    /// </summary>
    public partial class IdentifyDialog : Form
    {
        #region < 单件模式 >

        /// <summary>
        /// 无参数构造函数
        /// </summary>
        private IdentifyDialog()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 构造函数，初始化查询窗口
        /// </summary>
        /// <param name="IdentifyMapWindow"></param>
        private void ImportReference(AxMapControl IdentifyMap)
        {
            //传入的参数必须非空
            if (IdentifyMap == null) 
            {
                throw new NotImplementedException();
            }
            //保存传入的参数
            //associateMapWindow = IdentifyMap;
            associateMapControl = IdentifyMap;
            //初始化地图窗口事件
            //associateMapWindow.FormClosed += new FormClosedEventHandler(MapWindow_FormClosed);
            associateMapControl.OnMapReplaced += new IMapControlEvents2_Ax_OnMapReplacedEventHandler(OnMapReplaced);
            //初始化活动视图事件
            InitializeActiveViewEvents();
        }
        /// <summary>
        /// 主地图窗口关闭时关闭属性查询对话框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
            //this.Dispose();
        }
        /// <summary>
        /// 获取属性查询对象
        /// </summary>
        /// <param name="IdentifyMapWindow"></param>
        /// <returns></returns>
        public static IdentifyDialog CreateInstance(AxMapControl IdentifyMap) 
        {
            IdentifyDialog identifyInstance = Nested.GetInstance;
            identifyInstance.ImportReference(IdentifyMap);
            return identifyInstance;
        }
        /// <summary>
        /// 内嵌类,生成属性查询对象
        /// </summary>
        internal class Nested 
        {
            static Nested() { }
            private static IdentifyDialog instance;
            internal static IdentifyDialog GetInstance
            {
                get
                {
                    if (instance == null || 
                        instance.IsDisposed)
                    {
                        instance = new IdentifyDialog();
                    }
                    return instance;
                }
            }
        }

        #endregion

        /// <summary>
        /// 窗体初始化时加载图层过滤器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IdentifyDialog_Load(object sender, EventArgs e)
        {
            //新建图形闪烁对象
            flashObjects = new FlashObjectsClass();
            //新建图层属性hash表
            //用于保存图层过滤的数据
            layerFilterSet = new List<LayerFilterProperties>();
            //新建查询结果hash表
            //用于保存查询结果
            identifiedResultsList = new List<LayerIdentifiedResult>();
            //加载过滤器列表
            InitializeLayerFilters(null);
            //初始化属性显示窗口数据
            InitializeAttributesList();
        }
        /// <summary>
        /// 窗体关闭时删除不用的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IdentifyDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            //删除事件代理
            activeViewEvents.ItemAdded -= new IActiveViewEvents_ItemAddedEventHandler(InitializeLayerFilters);
            activeViewEvents.ItemDeleted -= new IActiveViewEvents_ItemDeletedEventHandler(InitializeLayerFilters);
            associateMapControl.OnMapReplaced -= new IMapControlEvents2_Ax_OnMapReplacedEventHandler(OnMapReplaced);
            //清空列表
            layerFilterSet = null; 
            identifiedResultsList = null;
        }
        /// <summary>
        /// 当对话框大小变化时自动调整属性显示列表中属性值列的列宽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstProperties_Resize(object sender, EventArgs e)
        {
            if (lstProperties.Columns.Count < 2) return;
            ColumnHeader field = lstProperties.Columns[0];
            ColumnHeader value = lstProperties.Columns[1];
            //设置字段值列宽度
            value.Width = lstProperties.Width - field.Width - 25;
        }
        /// <summary>
        /// 单击列标头时重新排列字段值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstProperties_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (clickedStatus)
            {
                lstProperties.Sorting = SortOrder.Ascending;
            }
            else
            {
                lstProperties.Sorting = SortOrder.Descending;
            }
            //改变单单击状态
            clickedStatus = !clickedStatus;
            lstProperties.Sort();
        }
        /// <summary>
        /// 结果列表节点点击的时候，在右侧显示点击节点数据信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //必须是左键单击
            if (e.Button != MouseButtons.Left) return;
            //显示节点的信息
            ShowClickedNodeInfo(e.Node, true);
        }
        /// <summary>
        /// 初始化活动视图事件
        /// </summary>
        private void InitializeActiveViewEvents()
        {
            activeViewEvents = associateMapControl.Map as IActiveViewEvents_Event;
            activeViewEvents.ItemAdded += new IActiveViewEvents_ItemAddedEventHandler(InitializeLayerFilters);
            activeViewEvents.ItemDeleted += new IActiveViewEvents_ItemDeletedEventHandler(InitializeLayerFilters);
        }
        ///
        private void OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            //初始化活动视图事件
            InitializeActiveViewEvents();
            //加载过滤器列表
            InitializeLayerFilters(null);
        }


        /// <summary>
        /// 显示节点的信息
        /// </summary>
        /// <param name="node"></param>
        private void ShowClickedNodeInfo(TreeNode nodeClicked, bool doFlash)
        {
            if (nodeClicked == null)
            {
                //初始化树型显示窗口
                InitializeAttributesList();
                return;
            }
            //获取点击点对应的要素
            int nodeLevel = nodeClicked.Level;
            //获取点击的是图层还是图层下的要素
            //若featureIndex < 0则表示点击的是图层,闪烁图层下选中的所有要素
            //反之，则表示点击的是图层下的要素，获取图层索引和要素索引，
            //用于在结果列表中安索引获取要素或要素集属性
            int layerIndex = -1;    int featureIndex = -1;
            if (nodeLevel > 0)
            {
                TreeNode parentNode = nodeClicked.Parent;
                layerIndex = parentNode.Index;
                featureIndex = nodeClicked.Index;
            }
            else
            {
                layerIndex = nodeClicked.Index;
            }
            //获取对应要素
            LayerIdentifiedResult layerResult = identifiedResultsList[layerIndex];
            //点击了图层下的要素
            if (featureIndex > -1)
            {
                IFeatureIdentifyObj identifyObjDefault = layerResult.IdentifiedFeatureObjList[featureIndex];
                IFeature featureDefault = (identifyObjDefault as IRowIdentifyObject).Row as IFeature;
                //显示属性
                ShowFeatureAttributes(featureDefault, layerResult);
                //判断是否闪烁要素
                if (doFlash)
                {
                    (identifyObjDefault as IIdentifyObj).Flash(associateMapControl.ActiveView.ScreenDisplay);
                }
            }
            //点击了图层，同时闪烁图层下的所有要素图形
            else
            {
                flashObjects.FlashObjects(layerResult);
            }
        }
        /// <summary>
        /// 加载过滤器列表
        /// </summary>
        private void InitializeLayerFilters(object item)
        {
            //清空列表先
            layerFilterSet.Clear();

            //初始化默认图层过滤器
            InitializeBasicLayerFilters();

            //加载地图中的默认图层
            DisplayLayersFromMapControl();

            //将过滤器添加到下拉框
            DisplayLayerFilters();
        }
        /// <summary>
        /// 加载地图窗口中的图层
        /// </summary>
        private void DisplayLayersFromMapControl()
        {
            IMap map = associateMapControl.Map;
            if (map.LayerCount < 1) return;
            //获取指定类型图层
            UID uid = new UIDClass();
            //表示搜索的是IDataLayer
            uid.Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}";
            //布尔参数表示要搜索GroupLayer中的图层
            IEnumLayer layers = map.get_Layers(uid, true);
            layers.Reset(); ILayer layer = layers.Next();
            while (layer != null)
            {
                LayerFilterProperties layerProperty = new LayerFilterProperties();
                //layerProperty.HeaderImage = null;
                layerProperty.LayerCategory = string.Empty;
                layerProperty.LayerFilterName = layer.Name;
                layerProperty.LayerFeatureType = GetLayerFeatureType(layer);
                layerProperty.TargetLayer = layer;
                //保存引用
                layerFilterSet.Add(layerProperty);
                layer = layers.Next();
            }
            //释放Com对象
            System.Runtime.InteropServices.Marshal.ReleaseComObject(layers);
        }
        /// <summary>
        /// 获取要素图层的类型
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        private LayerFeatureType GetLayerFeatureType(ILayer layer) 
        {
            LayerFeatureType featureType = LayerFeatureType.None;
            if (layer is IFeatureLayer) 
            {
                IFeatureClass featureClass = (layer as IFeatureLayer).FeatureClass;
                switch (featureClass.ShapeType) 
                {
                    case esriGeometryType.esriGeometryPoint:
                        featureType = LayerFeatureType.Point;
                        break;
                    case esriGeometryType.esriGeometryPolyline:
                        featureType = LayerFeatureType.Polyline;
                        break;
                    case esriGeometryType.esriGeometryPolygon:
                        featureType = LayerFeatureType.Polygon;
                        break;
                }
            }
            else if (layer is IRasterLayer)
            {
                featureType = LayerFeatureType.Raster;
            }
            else if (layer is IGroupLayer) 
            {
                featureType = LayerFeatureType.GroupLayer;
            }
            return featureType;
        }
        /// <summary>
        /// 初始化默认图层过滤器
        /// </summary>
        private void InitializeBasicLayerFilters() 
        {
            //添加默认过滤器
            //最顶图层
            LayerFilterProperties topMostLayerProperty = new LayerFilterProperties();
            //topMostLayerProperty.HeaderImage = null;
            topMostLayerProperty.LayerCategory = string.Empty;
            topMostLayerProperty.LayerFeatureType = LayerFeatureType.None;
            topMostLayerProperty.LayerFilterName = TopMostLayer;
            topMostLayerProperty.TargetLayer = null;
            //topMostLayerProperty.MapWindow = associateMapWindow;
            //可见图层
            LayerFilterProperties visibleLayerProperty = new LayerFilterProperties();
            //visibleLayerProperty.HeaderImage = null;
            visibleLayerProperty.LayerCategory = string.Empty;
            visibleLayerProperty.LayerFeatureType = LayerFeatureType.None;
            visibleLayerProperty.LayerFilterName = VisibleLayers;
            visibleLayerProperty.TargetLayer = null;
            //visibleLayerProperty.MapWindow = associateMapWindow;
            //可选图层
            LayerFilterProperties selectableLayerProperty = new LayerFilterProperties();
            //selectableLayerProperty.HeaderImage = null;
            selectableLayerProperty.LayerCategory = string.Empty;
            selectableLayerProperty.LayerFeatureType = LayerFeatureType.None;
            selectableLayerProperty.LayerFilterName = SelectableLayers;
            selectableLayerProperty.TargetLayer = null;
            //selectableLayerProperty.MapWindow = associateMapWindow;
            //所有图层
            LayerFilterProperties allLayerProperty = new LayerFilterProperties();
            //allLayerProperty.HeaderImage = null;
            allLayerProperty.LayerCategory = string.Empty;
            allLayerProperty.LayerFeatureType = LayerFeatureType.None;
            allLayerProperty.LayerFilterName = AllLayers;
            allLayerProperty.TargetLayer = null;
            //allLayerProperty.MapWindow = associateMapWindow;

            //保存图层引用
            layerFilterSet.Add(topMostLayerProperty);
            layerFilterSet.Add(visibleLayerProperty);
            layerFilterSet.Add(selectableLayerProperty);
            layerFilterSet.Add(allLayerProperty);
        }
        /// <summary>
        /// 将图层过滤器添加到下拉框
        /// 注意过滤器的显示顺序尚未实现
        /// </summary>
        private void DisplayLayerFilters()
        {
            //保存清空前的选中状态
            int selectedIndex = cboLayerFilter.SelectedIndex;
            if (selectedIndex < 0)
            {
                selectedIndex = 0;
            }
            //清空先
            cboLayerFilter.Items.Clear();
            int filterCount = layerFilterSet.Count;
            //加载所有图层过滤条件
            for (int i = 0; i < filterCount; i++)
            {
                LayerFilterProperties filterItem = layerFilterSet[i];
                cboLayerFilter.Items.Add(filterItem.LayerFilterName);
            }
            //设定默认过滤条件
            cboLayerFilter.SelectedIndex = selectedIndex;
        }
        /// <summary>
        /// 鼠标按下事件的处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnMouseDown(int button, double mapX, double mapY)
        {
            //窗体对象如果已经释放则不做任何操作
            if (IsDisposed) return;
            //必须是左键按下
            if (button != 1) return;
            //显示当前点击位置
            DisplayCoordinates(mapX, mapY);
            //开始画框操作
            IPoint mouseDownPoint = new PointClass();
            mouseDownPoint.PutCoords(mapX, mapY);
            mouseDownPoint.SpatialReference = associateMapControl.SpatialReference;
            if (trackNewEnvelope == null)
            {
                //新建框选对象
                trackNewEnvelope = new NewEnvelopeFeedbackClass();
                trackNewEnvelope.Display = associateMapControl.ActiveView.ScreenDisplay;
                trackNewEnvelope.Start(mouseDownPoint);
            }
        }
        /// <summary>
        /// 鼠标移动的事件处理
        /// </summary>
        public void OnMouseMove(double mapX, double mapY)
        {
            //必须在画框的过程中才调用鼠标移动函数
            if (trackNewEnvelope == null) return;
            //获取移动点
            IPoint moveToPoint = new PointClass();
            moveToPoint.PutCoords(mapX, mapY);
            moveToPoint.SpatialReference = associateMapControl.SpatialReference;
            //显示当前位置
            DisplayCoordinates(mapX, mapY);
            //
            trackNewEnvelope.MoveTo(moveToPoint);
        }
        /// <summary>
        /// 鼠标弹起的事件处理
        /// </summary>
        public void OnMouseUp(double mapX, double mapY)
        {
            //必须在画框的过程中才调用鼠标弹起函数
            if (trackNewEnvelope == null) return;
            //显示当前位置
            DisplayCoordinates(mapX, mapY);
            //完成画框操作，获取矩形框/点
            identifyEnvelope = trackNewEnvelope.Stop();
            if (identifyEnvelope.IsEmpty)
            {
                IPoint mouseUpPoint = new PointClass();
                mouseUpPoint.PutCoords(mapX, mapY);
                mouseUpPoint.SpatialReference = associateMapControl.SpatialReference;
                identifyEnvelope = mouseUpPoint;
            }
            identifyEnvelope.SpatialReference = associateMapControl.SpatialReference;
            //置空对象
            trackNewEnvelope = null;

            //获取查询图层
            List<LayerFilterProperties> searchLayers = SearchIdentifyLayers();
            //没有图层时不进行任何操作返回
            if (searchLayers == null || searchLayers.Count < 1) return;
            //显示查询信息
            lblFeatureCount.Text = "正在查询...";
            Application.DoEvents();
            //对需查询的图层执行查询操作
            ExecuteIdentify(searchLayers, identifyEnvelope);
            //显示查询结果
            DisplayIdentifyResults();
            //闪烁查询到的结果
            flashObjects.FlashObjects();
        }
        /// <summary>
        /// 初始化属性列表
        /// </summary>
        private void InitializeAttributesList()
        {
            //清空数据列表
            lstProperties.Items.Clear();
            lstProperties.Columns.Clear();
            //添加列头
            ColumnHeader noResultsInfo = new ColumnHeader();
            noResultsInfo.Text = "没有选中要查询的要素。";
            noResultsInfo.Width = 200;
            lstProperties.Columns.Add(noResultsInfo);
        }

        private IFeature ifeat;
        private IFeatureLayer pFeatureLayer;
        /// <summary>
        /// 显示指定要素的属性
        /// </summary>
        /// <param name="identifiedFeature"></param>
        private void ShowFeatureAttributes(IFeature identifiedFeature, LayerIdentifiedResult layerResult)
        {
            ArcGisPublic agp = new ArcGisPublic();
            ifeat = identifiedFeature;
            pFeatureLayer = agp.GetLayerByName(associateMapControl,layerResult.IdentifyLayer.Name) as IFeatureLayer;
            if (identifiedFeature == null)
            {
                //初始化属性列表
                InitializeAttributesList();
                return;
            }
            //清空数据列表
            lstProperties.Items.Clear();
            lstProperties.Columns.Clear();
            //若查询数据不为空则显示数据
            //添加列头
            ColumnHeader fieldHeader = new ColumnHeader();
            fieldHeader.Text = "字段名";
            fieldHeader.Width = 85;
            ColumnHeader valueHeader = new ColumnHeader();
            valueHeader.Text = "属性值";
            valueHeader.Width = lstProperties.Width - fieldHeader.Width - 25;
            lstProperties.Columns.AddRange(new ColumnHeader[] { fieldHeader, valueHeader });
            //添加值对
            IFields fields = identifiedFeature.Fields;
            //几何图形
            IGeometry shape = identifiedFeature.Shape;
            for (int i = 0; i < fields.FieldCount; i++) 
            {
                IField field = fields.get_Field(i);
                ListViewItem lvi = new ListViewItem(field.AliasName);
                string fieldValue = string.Empty;
                if (field.Type == esriFieldType.esriFieldTypeGeometry)
                {
                    fieldValue = shape.GeometryType.ToString().Substring(12);
                }
                else
                {
                    fieldValue = identifiedFeature.get_Value(i).ToString();
                }
                lvi.SubItems.Add(fieldValue);
                lstProperties.Items.Add(lvi);
            }
        }
        /// <summary>
        /// 执行属性查询
        /// 尚未实现如何闪烁选中要素集合
        /// 以及进度条的显示
        /// </summary>
        /// <param name="searchLayers"></param>
        private void ExecuteIdentify(List<LayerFilterProperties> searchLayers, IGeometry identifyGeom)
        {
            object Missing = Type.Missing;
            //清空以前所有的查询结果先
            identifiedResultsList.Clear();
            //用于计算查询得到的要素的数量
            int identifiedObjCount = 0;
            //获取用于查询的图层的数量
            int searchLayersCount = searchLayers.Count;
            //初始化进度条
            IdentifyProgress.Visible = true;
            IdentifyProgress.Maximum = searchLayersCount;
            //初始化闪烁对象
            flashObjects.MapControl = associateMapControl.Object as IMapControl2;
            flashObjects.Init();
            //遍历所有图层
            for (int i = 0; i < searchLayersCount; i++)
            {
                LayerFilterProperties filterProps = searchLayers[i];
                ILayer layer = filterProps.TargetLayer;
                //新建查询结果列表对象
                LayerIdentifiedResult layerIdentifiedResult = new LayerIdentifiedResult();
                //先保存查询图层对象
                layerIdentifiedResult.IdentifyLayer = layer;
                layerIdentifiedResult.GeometryType = filterProps.LayerFeatureType;
                //首先获得查询结果列表对象，以备后面往里添加结果使用
                List<IFeatureIdentifyObj> identifiedObjList = layerIdentifiedResult.IdentifiedFeatureObjList;
                //执行查询，返回查询结果
                IArray identifyResult = Identify(layer, identifyGeom);
                //处理异常情况
                if (identifyResult != null)
                {
                    //依次获取每一个查询结果对象
                    for (int k = 0; k < identifyResult.Count; k++)
                    {
                        identifiedObjCount++;
                        IFeatureIdentifyObj identifiedFeatureObj = identifyResult.get_Element(k) as IFeatureIdentifyObj;
                        //闪烁要素
                        IFeature identifiedFeature = (identifiedFeatureObj as IRowIdentifyObject).Row as IFeature;
                        //添加闪烁图形
                        flashObjects.AddGeometry(identifiedFeature.Shape);
                        //保存查询结果
                        identifiedObjList.Add(identifiedFeatureObj);
                    }
                    identifiedResultsList.Add(layerIdentifiedResult);
                }
                //显示查询进度
                IdentifyProgress.Value = i + 1;
                Application.DoEvents();
            }
            //隐藏进度条
            IdentifyProgress.Visible = false;
            //显示查询到的要素数量
            lblFeatureCount.Text = "查询到 " + identifiedObjCount + " 条记录";
        }
        /// <summary>
        /// 显示鼠标当前位置
        /// </summary>
        /// <param name="mapX"></param>
        /// <param name="mapY"></param>
        private void DisplayCoordinates(double mapX, double mapY)
        {
            if (IsDisposed) return;
            //鼠标在按下移动时显示当时坐标
            this.txtCoordinate.Text = string.Format("{0}, {1}",
                mapX.ToString("########.##"), mapY.ToString("########.##")) + " " +
                MapUnitChinese(associateMapControl.MapUnits);
        }
        /// <summary>
        /// 根据用户选择的下拉框获取需要进行查询的图层
        /// </summary>
        /// <returns></returns>
        private List<LayerFilterProperties> SearchIdentifyLayers()
        {
            List<LayerFilterProperties> IdentifyLayers = null;
            //获取过滤文本
            string identifyFilter = cboLayerFilter.Text.Trim();
            //获取选择的过滤器序号
            int selectedIndex = cboLayerFilter.SelectedIndex;
            if (selectedIndex >= layerFilterSet.Count)
            {
                MessageBox.Show("找不到过滤图层。");
                return IdentifyLayers;
            }
            //获取对应的图层对象
            LayerFilterProperties layerProps = layerFilterSet[selectedIndex];
            switch (layerProps.LayerFilterName)
            {
                case AllLayers:
                    IdentifyLayers = getAllLayers;
                    break;
                case SelectableLayers:
                    IdentifyLayers = getSelectableLayers;
                    break;
                case VisibleLayers:
                    IdentifyLayers = getVisibleLayers;
                    break;
                case TopMostLayer:
                    LayerFilterProperties layerProp = getTopmostLayer;
                    if (layerProp != null)
                    {
                        IdentifyLayers = new List<LayerFilterProperties>();
                        IdentifyLayers.Add(getTopmostLayer);
                    }
                    break;
                default:
                    IdentifyLayers = new List<LayerFilterProperties>();
                    IdentifyLayers.Add(getTargetLayer);
                    break;
            }
            return IdentifyLayers;
        }
        /// <summary>
        /// 在右侧表格中显示查询结果
        /// </summary>
        private void DisplayIdentifyResults()
        {
            //清空以前所有显示结果
            trvDataList.Nodes.Clear();
            //初始化属性显示窗口数据
            lstProperties.Items.Clear();
            lstProperties.Columns.Clear();
            //声明变量
            List<IFeatureIdentifyObj> IdentifyObjs = null;
            IFeatureIdentifyObj IdentifyObj = null;
            //列出查询结果
            int resultsCount = identifiedResultsList.Count;
            for (int i = 0; i < resultsCount; i++)
            {
                LayerIdentifiedResult layerIdentifiedResult = identifiedResultsList[i];
                //添加图层节点
                TreeNode layerNode = trvDataList.Nodes.Add(layerIdentifiedResult.IdentifyLayer.Name);
                //添加要素节点
                IdentifyObjs = layerIdentifiedResult.IdentifiedFeatureObjList;
                for (int k = 0; k < IdentifyObjs.Count; k++)
                {
                    IdentifyObj = IdentifyObjs[k];
                    IRowIdentifyObject rowIdentifyObj = IdentifyObj as IRowIdentifyObject;
                    IFeature identifyFeature = rowIdentifyObj.Row as IFeature;
                    layerNode.Nodes.Add(identifyFeature.OID.ToString());
                }
            }
            //展开所有节点
            trvDataList.ExpandAll();
            //默认显示第一图层的第一个查询要素
            if (trvDataList.Nodes.Count < 1)
            {
                ShowClickedNodeInfo(null, false);
            }
            //显示第一图层的第一个查询要素
            else
            {
                TreeNode topNode = trvDataList.Nodes[0].Nodes[0];
                //显示第一节点
                trvDataList.TopNode = topNode.Parent;
                ShowClickedNodeInfo(topNode, false);
            }
        }
        /// <summary>
        /// 在指定图层使用指定范围查询结果
        /// </summary>
        /// <param name="identifyLayer">查询图层</param>
        /// <param name="identifyGeom">查询范围</param>
        /// <returns>查询结果</returns>
        private IArray Identify(ILayer identifyLayer, IGeometry identifyGeom)
        {
            //保存结果的变量
            IArray identifyObjs = null;
            //设置查询形状
            if (identifyGeom == null)
            {
                //返回空值
                return identifyObjs;
            }
            //若是点的话做点的缓冲区
            IGeometry hitGeometry = identifyGeom;
            //判断图层类型,设置点击要素
            if (hitGeometry.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                ITopologicalOperator topoOp = identifyGeom as ITopologicalOperator;
                hitGeometry = topoOp.Buffer(1);
            }
            ///判断图层的类型并作相应处理
            ///图层是要素图层
            if (identifyLayer is IFeatureLayer)
            {
                //获取要素图层
                IFeatureLayer featureLayer = identifyLayer as IFeatureLayer;
                //开始获取信息操作
                IIdentify2 identify2 = featureLayer as IIdentify2;
                //获取查询结果
                identifyObjs = identify2.Identify(hitGeometry, null);
            }
            ///图层是栅格数据层
            else if (identifyLayer is IRasterLayer)
            {
            }
            //返回获取要素的集合
            return identifyObjs;
        }
        /// <summary>
        /// 显示中文地图单位
        /// </summary>
        /// <param name="mapUnit"></param>
        /// <returns></returns>
        private string MapUnitChinese(esriUnits mapUnit) 
        {
            string mapUnitChinese = "未知单位";
            switch (mapUnit) 
            {
                case esriUnits.esriCentimeters:
                    mapUnitChinese = "厘米";
                    break;
                case esriUnits.esriDecimalDegrees:
                    mapUnitChinese = "分米";
                    break;
                case esriUnits.esriDecimeters:
                    mapUnitChinese = "";
                    break;
                //case esriUnits.esriFeet:
                //    mapUnitChinese = "";
                //    break;
                //case esriUnits.esriInches:
                //    mapUnitChinese = "";
                //    break;
                case esriUnits.esriKilometers:
                    mapUnitChinese = "千米";
                    break;
                case esriUnits.esriMeters:
                    mapUnitChinese = "米";
                    break;
                case esriUnits.esriMiles:
                    mapUnitChinese = "英里";
                    break;
                case esriUnits.esriMillimeters:
                    mapUnitChinese = "毫米";
                    break;
                //case esriUnits.esriYards:
                //    mapUnitChinese = "";
                //    break;
            }
            return mapUnitChinese;
        }



        #region < 指定图层过滤器的获取方法 >

        /// <summary>
        /// 获取所有可选图层
        /// </summary>
        private List<LayerFilterProperties> getSelectableLayers
        {
            get
            {
                List<LayerFilterProperties> layers = new List<LayerFilterProperties>();
                int layerCount = layerFilterSet.Count;
                for (int i = 4; i < layerCount; i++)
                {
                    LayerFilterProperties layerProp = layerFilterSet[i];
                    ILayer esriLayer = layerProp.TargetLayer;
                    if (esriLayer is IFeatureLayer) 
                    {
                        if ((esriLayer as IFeatureLayer).Selectable) 
                        {
                            layers.Add(layerProp);
                        }
                    }
                }
                return layers;
            }
        }
        /// <summary>
        /// 获取最顶图层
        /// </summary>
        private LayerFilterProperties getTopmostLayer
        {
            //必须保证index为0的图层不是GroupLayer或CompositeLayer
            get
            {
                LayerFilterProperties layer = null;
                int layerCount = layerFilterSet.Count;
                for (int i = 4; i < layerCount; i++)
                {
                    LayerFilterProperties layerProp = layerFilterSet[i];
                    ILayer esriLayer = layerProp.TargetLayer;
                    if (!(esriLayer is IGroupLayer) && 
                        !(esriLayer is ICompositeLayer))
                    {
                        layer = layerProp;
                        break;
                    }
                }
                return layer;
            }
        }
        /// <summary>
        /// 获取所有可见图层
        /// </summary>
        private List<LayerFilterProperties> getVisibleLayers
        {
            get
            {
                List<LayerFilterProperties> layers = new List<LayerFilterProperties>();
                int layerCount = layerFilterSet.Count;
                for (int i = 4; i < layerCount; i++)
                {
                    LayerFilterProperties layerProp = layerFilterSet[i];
                    ILayer esriLayer = layerProp.TargetLayer;
                    if ((esriLayer is IGroupLayer) ||
                        (esriLayer is ICompositeLayer))
                    {
                        continue;
                    }
                    if (esriLayer.Visible)
                    {
                        layers.Add(layerProp);
                    }
                }
                return layers;
            }
        }
        /// <summary>
        /// 获取所有图层
        /// </summary>
        private List<LayerFilterProperties> getAllLayers
        {
            get
            {
                List<LayerFilterProperties> layers = new List<LayerFilterProperties>();
                int layerCount = layerFilterSet.Count;
                for (int i = 4; i < layerCount; i++)
                {
                    LayerFilterProperties layerProp = layerFilterSet[i];
                    ILayer esriLayer = layerProp.TargetLayer;
                    if ((esriLayer is IGroupLayer) ||
                        (esriLayer is ICompositeLayer))
                    {
                        continue;
                    }
                    layers.Add(layerProp);
                }
                return layers;
            }
        }
        /// <summary>
        ///  获取图层过滤器中的图层
        /// </summary>
        private LayerFilterProperties getTargetLayer 
        {
            get 
            {
                int selectedIndex = cboLayerFilter.SelectedIndex;
                return layerFilterSet[selectedIndex];
            }
        }

        #endregion

        //////////////////////////////////定义私有变量//////////////////////////////////////
        
        private const string TopMostLayer               = "<最顶图层>";
        private const string SelectableLayers           = "<可选图层>";
        private const string VisibleLayers              = "<可见图层>";
        private const string AllLayers                  = "<所有图层>";

        private AxMapControl                            associateMapControl;
        //private MapWindow                               associateMapWindow;
        private List<LayerFilterProperties>             layerFilterSet;
        private List<LayerIdentifiedResult>             identifiedResultsList;
        private IActiveViewEvents_Event                 activeViewEvents;
        private bool                                    clickedStatus = true;
        private INewEnvelopeFeedback2                   trackNewEnvelope;
        private IGeometry                               identifyEnvelope;
        private FlashObjectsClass                       flashObjects;

       // protected ArcGisPublic agp = new ArcGisPublic();
        private void button2_Click(object sender, EventArgs e)
        {
            if (ifeat != null)
            {
                ifeat.Delete();
                associateMapControl.ActiveView.Refresh();
            }
            else
            {
                MessageBox.Show("请选择数据删除!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            updateFeature frm = null;
            if (frm == null || frm.IsDisposed)
                frm = new updateFeature(pFeatureLayer,ifeat);

            frm.Show();
            frm.TopMost = true;
        }
    }
}
