using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Output;
using System.Collections.Generic;
using ESRI.ArcGIS.DataSourcesGDB;

namespace ArcGisView
{
    public sealed partial class MainForm : Form
    {
        #region class private members
        private IMapControl3 m_mapControl = null;
        private string m_mapDocumentName = string.Empty;

        private IMoveEnvelopeFeedback pSmallViewerEnvelope;//鹰眼小地图的红框,IMoveEnvelopeFeedback,用来移动Envelope的接口
        private IPoint pSmallViewerMouseDownPt;//拖动时鼠标落点
        private bool isTrackingSmallViewer = false; //标识是否在拖动
        static int moveCount = 0;//记录移动的个数，为移动过程中显示红框用。

        private ArcGisPublic agp = new ArcGisPublic();

        private IToolbarMenu m_ToolbarMenu = new ToolbarMenuClass();//右键菜单



        #endregion

        #region class constructor
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            pEn = axMapControl1.Extent;//鹰眼红框初始化
            CreateOverviewSymbol();
            //get the MapControl
            axMapControl1.KeyIntercept = (int)esriKeyIntercept.esriKeyInterceptArrowKeys;//设定鼠标滚轴缩放地图有效
            axMapControl1.AutoMouseWheel = true;//设定axMapControl1鼠标滚轴缩放地图有效
            axMapControl1.AutoKeyboardScrolling = true;//设定axMapControl1鼠标滚轴缩放地图有效
            axMapControl2.AutoMouseWheel = false;//设定axMapControl2鼠标滚轴缩放地图无效
            axMapControl2.AutoKeyboardScrolling = false;//设定axMapControl2鼠标滚轴缩放地图无效

            m_mapControl = (IMapControl3)axMapControl1.Object;

            //disable the Save menu (since there is no document yet)
            menuSaveDoc.Enabled = false;
            BindRightMenu();
        }

        #region Main Menu event handlers
        private void menuNewDoc_Click(object sender, EventArgs e)
        {
            //execute New Document command
            ICommand command = new CreateNewDocument();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuOpenDoc_Click(object sender, EventArgs e)
        {
            //execute Open Document command
            ICommand command = new ControlsOpenDocCommandClass();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuSaveDoc_Click(object sender, EventArgs e)
        {
            //execute Save Document command
            if (m_mapControl.CheckMxFile(m_mapDocumentName))
            {
                //create a new instance of a MapDocument
                IMapDocument mapDoc = new MapDocumentClass();
                mapDoc.Open(m_mapDocumentName, string.Empty);

                //Make sure that the MapDocument is not readonly
                if (mapDoc.get_IsReadOnly(m_mapDocumentName))
                {
                    MessageBox.Show("地图文件已经准备完毕!");
                    mapDoc.Close();
                    return;
                }

                //Replace its contents with the current map
                mapDoc.ReplaceContents((IMxdContents)m_mapControl.Map);

                //save the MapDocument in order to persist it
                mapDoc.Save(mapDoc.UsesRelativePaths, false);

                //close the MapDocument
                mapDoc.Close();
            }
        }

        private void menuSaveAs_Click(object sender, EventArgs e)
        {
            //execute SaveAs Document command
            ICommand command = new ControlsSaveAsDocCommandClass();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuExitApp_Click(object sender, EventArgs e)
        {
            //exit the application
            Application.Exit();
        }
        #endregion

        //listen to MapReplaced evant in order to update the statusbar and the Save menu
        #region 鹰眼的实现，左键标框，右键拖动
        IMapDocument pMapDocument = new MapDocumentClass();
        IEnvelope pEn = new EnvelopeClass();
        object oFillobject = new object();
        private void CreateOverviewSymbol()
        {
            IRgbColor iRgb = new RgbColorClass();
            iRgb.RGB = 255;
            ILineSymbol pOutline = new SimpleLineSymbolClass();
            pOutline.Color = iRgb;
            pOutline.Width = 2.3;
            ISimpleFillSymbol pSimpleFillSymbol = new SimpleFillSymbolClass();
            pSimpleFillSymbol.Outline = pOutline;
            pSimpleFillSymbol.Style = esriSimpleFillStyle.esriSFSHollow;
            oFillobject = pSimpleFillSymbol;
        }

        private void axMapControl1_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {

            axMapControl2.LoadMxFile(axMapControl1.DocumentFilename);
            axMapControl2.Extent = axMapControl1.FullExtent;
            //get the current document name from the MapControl
            m_mapDocumentName = m_mapControl.DocumentFilename;

            //if there is no MapDocument, diable the Save menu and clear the statusbar
            if (m_mapDocumentName == string.Empty)
            {
                menuSaveDoc.Enabled = false;
                statusBarXY.Text = string.Empty;
            }
            else
            {
                //enable the Save manu and write the doc name to the statusbar
                menuSaveDoc.Enabled = true;
                statusBarXY.Text = System.IO.Path.GetFileName(m_mapDocumentName);
            }
        }

        private void axMapControl1_OnExtentUpdated(object sender, IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            pEn = e.newEnvelope as IEnvelope;
            axMapControl2.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
        }

        private void axMapControl1_OnAfterDraw(object sender, IMapControlEvents2_OnAfterDrawEvent e)
        {
            esriViewDrawPhase viewDrawPhase = (esriViewDrawPhase)e.viewDrawPhase;
            if (viewDrawPhase == esriViewDrawPhase.esriViewForeground)
            {
                axMapControl2.DrawShape(pEn, ref oFillobject);
            }
        }

        private void axMapControl2_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (e.button == 1)//左键画红框
            {
                pEn = axMapControl2.TrackRectangle();
                axMapControl1.Extent = pEn;
                axMapControl2.DrawShape(pEn, ref oFillobject);
            }
            if (e.button == 2)//右键拖动红框
            {
                pSmallViewerMouseDownPt = new PointClass();
                pSmallViewerMouseDownPt.PutCoords(e.mapX, e.mapY);
                axMapControl1.CenterAt(pSmallViewerMouseDownPt);

                isTrackingSmallViewer = true;
                if (pSmallViewerEnvelope == null)
                {
                    pSmallViewerEnvelope = new MoveEnvelopeFeedbackClass();
                    pSmallViewerEnvelope.Display = axMapControl2.ActiveView.ScreenDisplay;
                    pSmallViewerEnvelope.Symbol = (ISymbol)oFillobject;
                }
                pSmallViewerEnvelope.Start(pEn, pSmallViewerMouseDownPt);
            }
        }

        private void axMapControl2_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            if (isTrackingSmallViewer)
            {
                moveCount++;
                if (moveCount % 4 == 0)//因为一刷新，红框就没了。所以每移动4次就刷新一下，保持红框的连续性。
                    axMapControl2.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
                pSmallViewerMouseDownPt.PutCoords(e.mapX, e.mapY);
                pSmallViewerEnvelope.MoveTo(pSmallViewerMouseDownPt);
            }
        }

        private void axMapControl2_OnMouseUp(object sender, IMapControlEvents2_OnMouseUpEvent e)
        {
            if (pSmallViewerEnvelope != null)
            {
                pEn = pSmallViewerEnvelope.Stop();
                axMapControl1.Extent = pEn;
                isTrackingSmallViewer = false;
            }
        }
        #endregion

        private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            if (e.button == 1 && search)//如果是查询状态并且是左键点击 
            {
                identifyDialog.OnMouseMove(e.mapX, e.mapY);
            }
            statusBarXY.Text = string.Format("{0}, {1}  {2}", e.mapX.ToString("#######.##"), e.mapY.ToString("#######.##"), axMapControl1.MapUnits.ToString().Substring(4));
        }


        private IdentifyDialog identifyDialog;

        private void ShowIdentifyDialog()
        {
            //新建属性查询对象
            identifyDialog = IdentifyDialog.CreateInstance(axMapControl1);
            identifyDialog.Owner = this;
            identifyDialog.Show();
        }

        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (e.button == 1 && insertbiaozhu)
            {
                agp.CreateTextElment(axMapControl1, e.mapX, e.mapY, "测试");
            }
            if (e.button == 1 && insertpoint)
            {
                IFeatureLayer oFeatureLayer = axMapControl1.get_Layer(axMapControl1.LayerCount - 1) as IFeatureLayer;
                agp.AddPointByStore(axMapControl1, oFeatureLayer, e.mapX, e.mapY);
            }

            if (e.button == 1 && insertline)
            {
                IFeatureLayer oFeatureLayer = axMapControl1.get_Layer(axMapControl1.LayerCount - 1) as IFeatureLayer;
                agp.AddLineByWrite(this.axMapControl1 ,oFeatureLayer, e.mapX, e.mapY);
            }

            if (e.button == 1 && insertPolygon)
            {
                IFeatureLayer oFeatureLayer = axMapControl1.get_Layer(axMapControl1.LayerCount - 1) as IFeatureLayer;
                agp.AddPolygonByWrite(this.axMapControl1, oFeatureLayer, e.mapX, e.mapY);
            }

            if (e.button == 1 && search)//如果是查询状态并且是左键点击 
            {

                if (identifyDialog.IsDisposed)
                {
                    ShowIdentifyDialog();
                }
                identifyDialog.OnMouseDown(e.button, e.mapX, e.mapY);
                //IMap pMap;
                //int i;
                //IPoint pPoint;
                //pMap = axMapControl1.Map;
                //pPoint = axMapControl1.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);

                //IEnvelope pEnv;
                //pEnv = axMapControl1.ActiveView.Extent;
                //pEnv.Height = 1;
                //pEnv.Width = 1;
                //pEnv.CenterAt(pPoint);

                ////执行查询获取符号条件的要素
                //List<IFeature> pFList = agp.GetSeartchFeatures(axMapControl1.get_Layer(0) as IFeatureLayer, pEnv);
                //if (pFList.Count > 0)
                //{
                //    //消息显示查询目标的信息
                //    SearchViewInfo frm = null;
                //    if (frm == null || frm.IsDisposed)
                //        frm = new SearchViewInfo(pFList[0]);

                //    frm.Show();
                //    frm.TopMost = true;
                //}
                //else

                //{
                //    MessageBox.Show ("没有找到任何节点!");
                //}
            }

            if (e.button == 2 && !bSearch)//如果是右键并且不是查询状态
            {

                if (axMapControl1.DocumentFilename != "")
                {
                    m_ToolbarMenu.PopupMenu(e.x, e.y, axMapControl1.hWnd);
                }
            }

            //if (bSearch && e.button == 1)//如果是查询状态并且是左键点击 
            //{
            //    //设置鼠标样式为十字丝
            //    this.axMapControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
            //    //获取画定范围的Geometry
            //    IGeometry pGeometry = this.axMapControl1.TrackPolygon();
            //    //添加半透名临时图形
            //    agp.AddTransTempEle(this.axMapControl1, pGeometry, true);



            //    //for (int a = 0; a < axMapControl1.LayerCount; a++)
            //    //{


            //    IFeatureLayer pFeatureLayer = this.axMapControl1.get_Layer(1) as IFeatureLayer;

            //    attribute pAttribute = new attribute(pFeatureLayer);
            //    //执行查询获取符号条件的要素
            //    List<IFeature> pFList = agp.GetSeartchFeatures(pFeatureLayer, pGeometry);
            //    //设置信息显示窗体中DataGridView的属性
            //    //设置行数pFList.Count+1包括字段名哪一行即表头
            //    pAttribute.dataGridView1.RowCount = pFList.Count + 1;
            //    //设置边界风格
            //    pAttribute.dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;
            //    //设置列数
            //    pAttribute.dataGridView1.ColumnCount = pFList[0].Fields.FieldCount;
            //    //遍历第一个要素的字段用于给列头赋值（字段的名称）
            //    for (int m = 0; m < pFList[0].Fields.FieldCount; m++)
            //    {
            //        pAttribute.dataGridView1.Columns[m].HeaderText = pFList[0].Fields.get_Field(m).AliasName;
            //    }
            //    //遍历要素
            //    for (int i = 0; i < pFList.Count; i++)
            //    {
            //        IFeature pFeature = pFList[i];
            //        for (int j = 0; j < pFeature.Fields.FieldCount; j++)
            //        {
            //            //填充字段值
            //            pAttribute.dataGridView1[j, i].Value = pFeature.get_Value(j).ToString();
            //        }
            //    }

            //    pAttribute.Show();
            //    //}
            //}
        }

        private void BindRightMenu()//添加右键菜单
        {
            m_ToolbarMenu.AddItem(new FixeZoomIn(), 1, 0, false, esriCommandStyles.esriCommandStyleTextOnly);//地图放大
            m_ToolbarMenu.AddItem(new FixeZoomOut(), 1, 1, false, esriCommandStyles.esriCommandStyleTextOnly);//地图缩小
            m_ToolbarMenu.AddItem(new FullExtent(), 1, 2, false, esriCommandStyles.esriCommandStyleTextOnly);//显示全图
            m_ToolbarMenu.AddItem(new PanClass(), 1, 3, false, esriCommandStyles.esriCommandStyleTextOnly);//地图平移
            m_ToolbarMenu.AddItem(new Measuredis(), 1, 4, false, esriCommandStyles.esriCommandStyleTextOnly);//地图测量
            m_ToolbarMenu.AddItem(new PrintPage(axMapControl1), 1, 5, false, esriCommandStyles.esriCommandStyleTextOnly);//地图打印
            m_ToolbarMenu.AddItem(new ExportImg(axMapControl1, this), 1, 6, false, esriCommandStyles.esriCommandStyleTextOnly);//导成图片
            m_ToolbarMenu.SetHook(axMapControl1);
        }


        bool bSearch = false;　//定义bool变量用于启动多边形查询功能

        private void 关键字查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchKey frm = null;
            if (frm == null || frm.IsDisposed)
                frm = new SearchKey(axMapControl1);

            frm.Show();
            frm.TopMost = true;
        }

        //private void 区域查询ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //向地图控件添加内存图层
        //        IFeatureLayer pFeatureLayer = agp.AddFeatureLayerByMemoryWS(this.axMapControl1, this.axMapControl1.SpatialReference);
        //        string a = pFeatureLayer.DataSourceType;
        //        this.axMapControl1.AddLayer(pFeatureLayer);
        //        //设置鼠标样式为十字丝
        //        this.axMapControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
        //        //启动范围查询功能
        //        bSearch = true;
        //    }
        //    catch
        //    { }
        //}
        bool insertpoint = false;
        private void 添加点型图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //创建要素类
            #region 创建新的内存工作空间
            IWorkspaceFactory pWSF = new InMemoryWorkspaceFactoryClass();
            IWorkspaceName pWSName = pWSF.Create("", "Temp", null, 0);

            IName pName = (IName)pWSName;
            IWorkspace pMemoryWS = (IWorkspace)pName.Open();
            #endregion

            IField oField = new FieldClass();
            IFields oFields = new FieldsClass();
            IFieldsEdit oFieldsEdit = null;
            IFieldEdit oFieldEdit = null;
            IFeatureClass oFeatureClass = null;
            IFeatureLayer oFeatureLayer = null;

            oFieldsEdit = oFields as IFieldsEdit;
            oFieldEdit = oField as IFieldEdit;
            oFieldEdit.Name_2 = "OBJECTID";
            oFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
            oFieldEdit.IsNullable_2 = false;
            oFieldEdit.Required_2 = false;
            oFieldsEdit.AddField(oField);

            oField = new FieldClass();
            oFieldEdit = oField as IFieldEdit;
            IGeometryDef pGeoDef = new GeometryDefClass();
            IGeometryDefEdit pGeoDefEdit = (IGeometryDefEdit)pGeoDef;
            pGeoDefEdit.AvgNumPoints_2 = 5;
            pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
            pGeoDefEdit.GridCount_2 = 1;
            pGeoDefEdit.HasM_2 = false;
            pGeoDefEdit.HasZ_2 = false;
            pGeoDefEdit.SpatialReference_2 = axMapControl1.SpatialReference;
            oFieldEdit.Name_2 = "SHAPE";
            oFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            oFieldEdit.GeometryDef_2 = pGeoDef;
            oFieldEdit.IsNullable_2 = true;
            oFieldEdit.Required_2 = true;
            oFieldsEdit.AddField(oField);

            oField = new FieldClass();
            oFieldEdit = oField as IFieldEdit;
            oFieldEdit.Name_2 = "Code";
            oFieldEdit.Type_2 = esriFieldType.esriFieldTypeSmallInteger;
            //oFieldEdit.Length = 10;
            oFieldEdit.IsNullable_2 = true;
            oFieldsEdit.AddField(oField);
            //创建要素类
            oFeatureClass = (pMemoryWS as IFeatureWorkspace).CreateFeatureClass("Temp", oFields, null, null, esriFeatureType.esriFTSimple, "SHAPE", "");
            oFeatureLayer = new FeatureLayerClass();
            oFeatureLayer.Name = "PointLayer";
            oFeatureLayer.FeatureClass = oFeatureClass;
            //创建唯一值符号化对象


            IUniqueValueRenderer pURender = new UniqueValueRendererClass();
            pURender.FieldCount = 1;
            pURender.set_Field(0, "Code");
            pURender.UseDefaultSymbol = false;
            //创建SimpleMarkerSymbolClass对象
            ISimpleMarkerSymbol pSimpleMarkerSymbol = new SimpleMarkerSymbolClass();
            //创建RgbColorClass对象为pSimpleMarkerSymbol设置颜色
            IRgbColor pRgbColor = new RgbColorClass();
            pRgbColor.Red = 255;
            pSimpleMarkerSymbol.Color = pRgbColor as IColor;
            //设置pSimpleMarkerSymbol对象的符号类型，选择钻石
            pSimpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSDiamond;
            //设置pSimpleMarkerSymbol对象大小，设置为５
            pSimpleMarkerSymbol.Size = 5;
            //显示外框线
            pSimpleMarkerSymbol.Outline = true;
            //为外框线设置颜色
            IRgbColor pLineRgbColor = new RgbColorClass();
            pLineRgbColor.Green = 255;
            pSimpleMarkerSymbol.OutlineColor = pLineRgbColor as IColor;
            //设置外框线的宽度
            pSimpleMarkerSymbol.OutlineSize = 1; 

            //半透明颜色

 


            pURender.AddValue("1", "", pSimpleMarkerSymbol as ISymbol);

            //唯一值符号化内存图层
            (oFeatureLayer as IGeoFeatureLayer).Renderer = pURender as IFeatureRenderer;
            ILayerEffects pLyrEffect = oFeatureLayer as ILayerEffects;
            //透明度
            pLyrEffect.Transparency = 0;


            oFeatureLayer.Visible = true;

            this.axMapControl1.AddLayer(oFeatureLayer,axMapControl1.LayerCount);
            insertpoint = true;
        }

        private bool search = false;
        private void 点击查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!search)
            {
                点击查询ToolStripMenuItem.Text = "取消鼠标查询";
                ShowIdentifyDialog();
            }
            else
            {
                点击查询ToolStripMenuItem.Text = "鼠标查询";
                if (!identifyDialog.IsDisposed)
                {
                    identifyDialog.Dispose();
                }
            }
            search = !search;

        }

        private void axMapControl1_OnMouseUp(object sender, IMapControlEvents2_OnMouseUpEvent e)
        {
            if (e.button == 1 && search)//如果是查询状态并且是左键点击 
            {
                identifyDialog.OnMouseUp(e.mapX, e.mapY);
            }
        }

        bool insertline = false;
        private void 添加线形图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //创建要素类
            #region 创建新的内存工作空间
            IWorkspaceFactory pWSF = new InMemoryWorkspaceFactoryClass();
            IWorkspaceName pWSName = pWSF.Create("", "Temp", null, 0);

            IName pName = (IName)pWSName;
            IWorkspace pMemoryWS = (IWorkspace)pName.Open();
            #endregion

            IField oField = new FieldClass();
            IFields oFields = new FieldsClass();
            IFieldsEdit oFieldsEdit = null;
            IFieldEdit oFieldEdit = null;
            IFeatureClass oFeatureClass = null;
            IFeatureLayer oFeatureLayer = null;

            oFieldsEdit = oFields as IFieldsEdit;
            oFieldEdit = oField as IFieldEdit;
            oFieldEdit.Name_2 = "OBJECTID";
            oFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
            oFieldEdit.IsNullable_2 = false;
            oFieldEdit.Required_2 = false;
            oFieldsEdit.AddField(oField);

            oField = new FieldClass();
            oFieldEdit = oField as IFieldEdit;
            IGeometryDef pGeoDef = new GeometryDefClass();
            IGeometryDefEdit pGeoDefEdit = (IGeometryDefEdit)pGeoDef;
            pGeoDefEdit.AvgNumPoints_2 = 5;
            pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolyline;
            pGeoDefEdit.GridCount_2 = 1;
            pGeoDefEdit.HasM_2 = false;
            pGeoDefEdit.HasZ_2 = false;
            pGeoDefEdit.SpatialReference_2 = axMapControl1.SpatialReference;
            oFieldEdit.Name_2 = "SHAPE";
            oFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            oFieldEdit.GeometryDef_2 = pGeoDef;
            oFieldEdit.IsNullable_2 = true;
            oFieldEdit.Required_2 = true;
            oFieldsEdit.AddField(oField);

            oField = new FieldClass();
            oFieldEdit = oField as IFieldEdit;
            oFieldEdit.Name_2 = "Code";
            oFieldEdit.Type_2 = esriFieldType.esriFieldTypeSmallInteger;
            oFieldEdit.IsNullable_2 = true;
            oFieldsEdit.AddField(oField);
            //创建要素类
            oFeatureClass = (pMemoryWS as IFeatureWorkspace).CreateFeatureClass("Temp", oFields, null, null, esriFeatureType.esriFTSimple, "SHAPE", "");
            oFeatureLayer = new FeatureLayerClass();
            oFeatureLayer.Name = "LineLayer";
            oFeatureLayer.FeatureClass = oFeatureClass;
            //创建唯一值符号化对象
            IUniqueValueRenderer pURender = new UniqueValueRendererClass();
            pURender.FieldCount = 1;
            pURender.set_Field(0, "Code");
            pURender.UseDefaultSymbol = false;
            ISimpleFillSymbol pFillSym = new SimpleFillSymbolClass();
            pFillSym.Style = esriSimpleFillStyle.esriSFSSolid;
            //半透明颜色
            IRgbColor pColor = new RgbColorClass();
            pColor.Red = 255;
            pColor.Green = 255;
            pFillSym.Color = pColor;
            pURender.AddValue("1", "", pFillSym as ISymbol);
            pFillSym = new SimpleFillSymbolClass();
            pFillSym.Style = esriSimpleFillStyle.esriSFSSolid;
            //唯一值符号化内存图层
            (oFeatureLayer as IGeoFeatureLayer).Renderer = pURender as IFeatureRenderer;
            ILayerEffects pLyrEffect = oFeatureLayer as ILayerEffects;
            //透明度
            pLyrEffect.Transparency = 0;



            this.axMapControl1.AddLayer(oFeatureLayer, axMapControl1.LayerCount);
            insertline = true;
        }

        private void 关闭添加地物ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            insertline=false;
            insertpoint = false;
            insertPolygon = false;
        }

        private bool insertbiaozhu = false;
        private void 开始添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            insertbiaozhu = true;
        }

        private void 结束添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            insertbiaozhu = false;
        }

        private bool insertPolygon = false;
        private void 添加面形地物ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //创建要素类
            #region 创建新的内存工作空间
            IWorkspaceFactory pWSF = new InMemoryWorkspaceFactoryClass();
            IWorkspaceName pWSName = pWSF.Create("", "Temp", null, 0);

            IName pName = (IName)pWSName;
            IWorkspace pMemoryWS = (IWorkspace)pName.Open();
            #endregion

            IField oField = new FieldClass();
            IFields oFields = new FieldsClass();
            IFieldsEdit oFieldsEdit = null;
            IFieldEdit oFieldEdit = null;
            IFeatureClass oFeatureClass = null;
            IFeatureLayer oFeatureLayer = null;

            oFieldsEdit = oFields as IFieldsEdit;
            oFieldEdit = oField as IFieldEdit;
            oFieldEdit.Name_2 = "OBJECTID";
            oFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
            oFieldEdit.IsNullable_2 = false;
            oFieldEdit.Required_2 = false;
            oFieldsEdit.AddField(oField);

            oField = new FieldClass();
            oFieldEdit = oField as IFieldEdit;
            IGeometryDef pGeoDef = new GeometryDefClass();
            IGeometryDefEdit pGeoDefEdit = (IGeometryDefEdit)pGeoDef;
            pGeoDefEdit.AvgNumPoints_2 = 5;
            pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
            pGeoDefEdit.GridCount_2 = 1;
            pGeoDefEdit.HasM_2 = false;
            pGeoDefEdit.HasZ_2 = false;
            pGeoDefEdit.SpatialReference_2 = axMapControl1.SpatialReference;
            oFieldEdit.Name_2 = "SHAPE";
            oFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            oFieldEdit.GeometryDef_2 = pGeoDef;
            oFieldEdit.IsNullable_2 = true;
            oFieldEdit.Required_2 = true;
            oFieldsEdit.AddField(oField);

            oField = new FieldClass();
            oFieldEdit = oField as IFieldEdit;
            oFieldEdit.Name_2 = "Code";
            oFieldEdit.Type_2 = esriFieldType.esriFieldTypeSmallInteger;
            oFieldEdit.IsNullable_2 = true;
            oFieldsEdit.AddField(oField);
            //创建要素类
            oFeatureClass = (pMemoryWS as IFeatureWorkspace).CreateFeatureClass("Temp", oFields, null, null, esriFeatureType.esriFTSimple, "SHAPE", "");
            oFeatureLayer = new FeatureLayerClass();
            oFeatureLayer.Name = "PolygonLayer";
            oFeatureLayer.FeatureClass = oFeatureClass;
            //创建唯一值符号化对象
            IUniqueValueRenderer pURender = new UniqueValueRendererClass();
            pURender.FieldCount = 1;
            pURender.set_Field(0, "Code");
            pURender.UseDefaultSymbol = false;
            ISimpleFillSymbol pFillSym = new SimpleFillSymbolClass();
            pFillSym.Style = esriSimpleFillStyle.esriSFSSolid;
            //半透明颜色
            IRgbColor pColor = new RgbColorClass();
            pColor.Red = 255;
            pColor.Green = 255;
            pFillSym.Color = pColor;
            pURender.AddValue("1", "", pFillSym as ISymbol);
            pFillSym = new SimpleFillSymbolClass();
            pFillSym.Style = esriSimpleFillStyle.esriSFSSolid;
            //唯一值符号化内存图层
            (oFeatureLayer as IGeoFeatureLayer).Renderer = pURender as IFeatureRenderer;
            ILayerEffects pLyrEffect = oFeatureLayer as ILayerEffects;
            //透明度
            pLyrEffect.Transparency = 0;



            this.axMapControl1.AddLayer(oFeatureLayer, axMapControl1.LayerCount);
            insertPolygon = true;
        }

        private void 删除地物ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!search)
            {
                点击查询ToolStripMenuItem.Text = "取消鼠标查询";
                ShowIdentifyDialog();
            }
            else
            {
                点击查询ToolStripMenuItem.Text = "鼠标查询";
                if (!identifyDialog.IsDisposed)
                {
                    identifyDialog.Dispose();
                }
            }
            search = !search;
        }
    }
}