using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.Analyst3D;



namespace ArcGisView
{
    public class ArcGisPublic
    {
        public void export(AxMapControl MapCtrl, Form hwin)//导出成图片
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "(*.tif)|*.tif|(*.jpeg)|*.jpeg|(*.pdf)|*.pdf|(*.bmp)|*.bmp";
                if (sfd.ShowDialog(hwin) == DialogResult.OK)
                {
                    IExporter pExport = null;
                    if (1 == sfd.FilterIndex)
                    {
                        pExport = new TiffExporter() as IExporter;
                        pExport.ExportFileName = sfd.FileName;
                    }
                    else if (2 == sfd.FilterIndex)
                    {
                        pExport = new JpegExporter() as IExporter;
                        pExport.ExportFileName = sfd.FileName;
                    }
                    else if (3 == sfd.FilterIndex)
                    {
                        pExport = new PDFExporter() as IExporter;
                        pExport.ExportFileName = sfd.FileName;
                    }
                    else if (4 == sfd.FilterIndex)
                    {
                        pExport = new DibExporter() as IExporter; pExport.ExportFileName = sfd.FileName;
                    }
                    short res = 96;
                    pExport.Resolution = res;
                    tagRECT exportRECT = MapCtrl.ActiveView.ExportFrame;
                    IEnvelope pENV = new EnvelopeClass();
                    pENV.PutCoords(exportRECT.left, exportRECT.top, exportRECT.right, exportRECT.bottom);
                    pExport.PixelBounds = pENV;
                    int Hdc = pExport.StartExporting();
                    IEnvelope pVisibleBounds = null;
                    ITrackCancel pTrack = null;
                    MapCtrl.ActiveView.Output(Hdc, (int)pExport.Resolution, ref exportRECT, pVisibleBounds, pTrack);
                    Application.DoEvents();
                    pExport.FinishExporting();
                }
            }
            catch { }
        }

        /// <summary>
        /// 在线要素集里面查找与图形相交的要素，返回其游标
        /// </summary>
        /// <param name="LineFeatClass"></param>
        /// <param name="geo"></param>
        /// <returns></returns>
        public static IFeatureCursor SearchIntersectLineFeat(IFeatureClass LineFeatClass, IGeometry geo)
        {
            ISpatialFilter pSpatialFilter = new SpatialFilterClass();
            pSpatialFilter.Geometry = geo;
            pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            pSpatialFilter.SearchOrder = esriSearchOrder.esriSearchOrderSpatial;
            IFeatureCursor pfeatCursor = LineFeatClass.Search(pSpatialFilter, false);
            return pfeatCursor;
        }
        /// <summary>
        /// 返回落入图形区域内的要素
        /// </summary>
        /// <param name="fc"></param>
        /// <param name="geo"></param>
        /// <returns></returns>
        public static IFeatureCursor SearchContainFeat(IFeatureClass fc, IGeometry geo)
        {
            ISpatialFilter pSpatialFilter = new SpatialFilterClass();
            pSpatialFilter.Geometry = geo;
            pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
            pSpatialFilter.SearchOrder = esriSearchOrder.esriSearchOrderSpatial;
            IFeatureCursor pfeatCursor = fc.Search(pSpatialFilter, false);
            return pfeatCursor;
        }

        ///<summary>
        ///在程序运行时的内存中创建矢量要素层，并加到地图控件最顶端
        ///</summary>
        ///<param name="pMapCtrl">地图控件</param>
        ///<returns>IFeatureLayer 新加的要素层</returns>
        public IFeatureLayer AddFeatureLayerByMemoryWS(AxMapControl pMapCtrl, ISpatialReference pSReference)
        {
            try
            {
                if (pMapCtrl == null)
                    return null;

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
                try
                {
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
                    pGeoDefEdit.SpatialReference_2 = pSReference;
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
                    oFeatureLayer.Name = "TransTemp";
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
                    pLyrEffect.Transparency = 80;
                }
                catch (Exception Err)
                {
                    MessageBox.Show(Err.Message);
                }
                finally
                {
                    try
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oField);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oFields);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oFieldsEdit);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oFieldEdit);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(pName);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(pWSF);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(pWSName);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(pMemoryWS);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oFeatureClass);
                    }
                    catch
                    {
                    }
                    GC.Collect();
                }
                return oFeatureLayer;
            }
            catch (Exception Err)
            {
                MessageBox.Show(Err.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
        }


        /// <summary>
        /// 获取查询要素
        /// </summary>
        /// <param name="pFeatureLayer">要素图层</param>
        /// <param name="pGeometry">图形范围参数</param>
        /// <returns>符号条件要素集合</returns>
        public List<IFeature> GetSeartchFeatures(IFeatureLayer pFeatureLayer, IGeometry pGeometry)
        {
            try
            {
                List<IFeature> pList = new List<IFeature>();
                //创建SpatialFilter空间过滤器对象
                ISpatialFilter pSpatialFilter = new SpatialFilterClass();
                IQueryFilter pQueryFilter = pSpatialFilter as ISpatialFilter;
                //设置过滤器的Geometry
                pSpatialFilter.Geometry = pGeometry;
                //设置空间关系类型
                pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                //获取FeatureCursor游标
                IFeatureCursor pFeatureCursor = pFeatureLayer.Search(pQueryFilter, false);
                //遍历FeatureCursor
                IFeature pFeature = pFeatureCursor.NextFeature();
                while (pFeature != null)
                {
                    //获取要素对象
                    pList.Add(pFeature);
                    pFeature = pFeatureCursor.NextFeature();
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
                return pList;
            }
            catch (Exception Err)
            {
                MessageBox.Show(Err.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
        }


        /// <summary> 在地图控件上添加透明临时图元/// </summary>
        /// <param name="pMapCtrl">地图控件</param>
        /// <param name="pGeo">Envelope或Polygon几何实体</param>
        /// <param name="bAutoClear">是否清除原有内容</param>
        public void AddTransTempEle(AxMapControl pMapCtrl, IGeometry pGeo, bool bAutoClear)
        {
            try
            {
                if (pMapCtrl == null) return;
                if (pGeo == null) return;
                if (pGeo.IsEmpty) return;
                IGeometry pPolygon = null;
                if (pGeo is IEnvelope)
                {
                    object Miss = Type.Missing;
                    pPolygon = new PolygonClass();
                    IGeometryCollection pGeoColl = pPolygon as IGeometryCollection;
                    pGeoColl.AddGeometry(pGeo, ref Miss, ref Miss);
                }
                else if (pGeo is IPolygon)
                {
                    (pGeo as ITopologicalOperator).Simplify();
                    pPolygon = pGeo;
                }
                else
                {
                    MessageBox.Show("几何实体类型不匹配", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //获取透明要素层
                IFeatureLayer pFlyr = null;
                for (int i = 0; i < pMapCtrl.LayerCount; i++)
                {
                    if (pMapCtrl.get_Layer(i).Name == "TransTemp")
                    {
                        pFlyr = pMapCtrl.get_Layer(i) as IFeatureLayer;
                        break;
                    }
                }
                //透明临时层不存在需要创建
                if (pFlyr == null)
                {
                    pFlyr = AddFeatureLayerByMemoryWS(pMapCtrl, pMapCtrl.SpatialReference);
                    if (pFlyr == null)
                    {
                        MessageBox.Show("创建透明临时图层发生异常", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                IFeatureClass pFC = pFlyr.FeatureClass;
                if (bAutoClear)
                {
                    if (pFC.FeatureCount(null) > 0)
                    {
                        IFeatureCursor pFCursor = pFC.Search(null, false);
                        if (pFCursor != null)
                        {

                            IFeature pFeature = pFCursor.NextFeature();
                            if (pFeature != null)
                            {
                                while (pFeature != null)
                                {
                                    pFeature.Delete();
                                    pFeature = pFCursor.NextFeature();
                                }
                            }
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFCursor);
                        }
                    }
                }
                //创建要素
                IFeature pNFeature = pFC.CreateFeature();
                pNFeature.Shape = pPolygon;
                pNFeature.set_Value(pFC.FindField("Code"), "1");
                pNFeature.Store();
                pMapCtrl.Refresh(esriViewDrawPhase.esriViewGeography, pFlyr, pFlyr.AreaOfInterest);
            }
            catch (Exception Err)
            {
                MessageBox.Show(Err.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void AddPointByStore(AxMapControl axMapControl1, IFeatureLayer l, double x, double y)
        {
            ESRI.ArcGIS.Geometry.esriGeometryType featype = l.FeatureClass.ShapeType;
            if (featype == esriGeometryType.esriGeometryPoint)//判断层是否为点层
            {
                //得到要添加地物的图层 
                //IFeatureLayer l = MapCtr.Map.get_Layer(0) as IFeatureLayer;
                //定义一个地物类,把要编辑的图层转化为定义的地物类 
                IFeatureClass fc = l.FeatureClass;
                //先定义一个编辑的工作空间,然后把转化为数据集,最后转化为编辑工作空间, 
                IWorkspaceEdit w = (fc as IDataset).Workspace as IWorkspaceEdit;
                IFeature f;
                IPoint p;
                //开始事务操作 
                w.StartEditing(false);
                //开始编辑 
                w.StartEditOperation();

                //创建一个地物 
                f = fc.CreateFeature();
                p = new PointClass();
                //设置点的坐标 
                p.PutCoords(x, y);



                ////确定图形类型 
                f.Shape = p;
                //保存地物 
                f.Store();

                //结束编辑 
                w.StopEditOperation();
                //结束事务操作 
                w.StopEditing(true);
                AddPoint(axMapControl1, x, y);
                //UniqueValueRenderFlyr(axMapControl1, l);
                //axMapControl1.Refresh();
            }
        }

        /// <summary>
        /// 添加点
        /// </summary>
        public void AddPoint(AxMapControl axMapControl1, double x, double y)
        {
            try
            {
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


                string AppPath = Application.StartupPath;
                IPoint Pt = new PointClass();
                Pt.PutCoords(x, y);

                IMarkerElement ipMarkerElement = new MarkerElementClass();
                ipMarkerElement.Symbol = pSimpleMarkerSymbol as IMarkerSymbol;
                IElement ipElement = ipMarkerElement as IElement;
                ipElement.Geometry = Pt as IGeometry;
                axMapControl1.ActiveView.GraphicsContainer.AddElement(ipElement, 0);
                axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, ipElement, null);
            }
            catch
            {

            }
        }

        public void AddPointByWrite(IFeatureLayer l, double x, double y)
        {
            ESRI.ArcGIS.Geometry.esriGeometryType featype = l.FeatureClass.ShapeType;
            if (featype == esriGeometryType.esriGeometryPoint)//判断层是否为点层
            {
                // IFeatureLayer l = MapCtr.Map.get_Layer(0) as IFeatureLayer;
                IFeatureClass fc = l.FeatureClass;
                IFeatureClassWrite fr = fc as IFeatureClassWrite;
                IWorkspaceEdit w = (fc as IDataset).Workspace as IWorkspaceEdit;
                IFeature f;
                IPoint p;

                w.StartEditing(true);
                w.StartEditOperation();

                f = fc.CreateFeature();
                p = new PointClass();
                p.PutCoords(x, y);
                f.Shape = p;
                fr.WriteFeature(f);

                w.StopEditOperation();
                w.StopEditing(true);
            }
        }

        public void AddPointByBuffer(IFeatureLayer l, double x, double y)
        {
            ESRI.ArcGIS.Geometry.esriGeometryType featype = l.FeatureClass.ShapeType;
            if (featype == esriGeometryType.esriGeometryPoint)//判断层是否为点层
            {
                //IFeatureLayer l = MapCtr.Map.get_Layer(0) as IFeatureLayer;
                IFeatureClass fc = l.FeatureClass;
                IWorkspaceEdit w = (fc as IDataset).Workspace as IWorkspaceEdit;
                w.StartEditing(true);
                w.StartEditOperation();
                IPoint p;
                IFeatureBuffer f;
                IFeatureCursor cur = fc.Insert(true);

                f = fc.CreateFeatureBuffer();
                p = new PointClass();
                p.PutCoords(x, y);
                f.Shape = p;
                cur.InsertFeature(f);
                w.StopEditOperation();
                w.StopEditing(true);
            }
        }

        public void AddLineByWrite(AxMapControl axMapControl1, IFeatureLayer l, double x, double y)
        {
            ESRI.ArcGIS.Geometry.esriGeometryType featype = l.FeatureClass.ShapeType;
            if (featype == esriGeometryType.esriGeometryPolyline)//判断层是否为线层
            {
                //IFeatureLayer l = MapCtr.Map.get_Layer(0) as IFeatureLayer;
                IFeatureClass fc = l.FeatureClass;
                IFeatureClassWrite fr = fc as IFeatureClassWrite;
                IWorkspaceEdit w = (fc as IDataset).Workspace as IWorkspaceEdit;
                IFeature f;
                //可选参数的设置 
                object Missing = Type.Missing;
                IPoint p = new PointClass();
                w.StartEditing(true);
                w.StartEditOperation();

                f = fc.CreateFeature();
                //定义一个多义线对象 
                IRgbColor color = new RgbColor();
                // 设置颜色属性
                color.Red = 255;
                color.Transparency = 255;

                //ISimpleLineSymbol PlyLine = new SimpleLineSymbolClass();
                //PlyLine.Color = color;
                //PlyLine.Style = esriSimpleLineStyle.esriSLSInsideFrame;
                //PlyLine.Width = 1;

                IGeometry iGeom = axMapControl1.TrackLine();

                AddLine(axMapControl1, iGeom);

                f.Shape = iGeom;
                fr.WriteFeature(f);
                w.StopEditOperation();
                w.StopEditing(true);
            }
        }

        /// <summary>
        /// 添加线
        /// </summary>
        public void AddLine(AxMapControl axMapControl1, IGeometry GeomLine)
        {
            ISimpleLineSymbol ipLine = new SimpleLineSymbolClass();
            ipLine.Width = 1;
            IRgbColor ipColor = new RgbColorClass();
            ipColor.RGB = 0x0000ff;
            ipLine.Color = ipColor;
            ipLine.Style = esriSimpleLineStyle.esriSLSDashDotDot;
            ILineElement ipLineElem = new LineElementClass();
            ipLineElem.Symbol = ipLine;
            IElement ipElement = ipLineElem as IElement;
            ipElement.Geometry = GeomLine;
            axMapControl1.ActiveView.GraphicsContainer.AddElement(ipElement, 0);
            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, ipElement, null);
        }

        public void AddRegion(AxMapControl axMapControl1, IGeometry GeomArea)
        {
            IPolygonElement ipPolygonElem = new PolygonElementClass();
            ILineSymbol ipLine = new SimpleLineSymbolClass();
            ipLine.Width = 1;
            IRgbColor ipColor = new RgbColorClass();
            ipColor.RGB = 0x0000ff;
            ipLine.Color = ipColor;
            ISimpleFillSymbol ipFillSym = new SimpleFillSymbolClass();
            IRgbColor ipColorFill = new RgbColorClass();
            ipColorFill.RGB = 0xff0000;
            ipFillSym.Outline = ipLine;
            ipFillSym.Color = ipColorFill;
            ipFillSym.Style = esriSimpleFillStyle.esriSFSCross;
            IFillShapeElement ipShape = ipPolygonElem as IFillShapeElement;
            ipShape.Symbol = ipFillSym;
            IElement ipElement = ipPolygonElem as IElement;
            ipElement.Geometry = GeomArea;
            axMapControl1.ActiveView.GraphicsContainer.AddElement(ipElement, 0);
            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, ipElement, null);
        }

        public void AddPolygonByWrite(AxMapControl axMapControl1, IFeatureLayer l, double x, double y)
        {
            ESRI.ArcGIS.Geometry.esriGeometryType featype = l.FeatureClass.ShapeType;
            if (featype == esriGeometryType.esriGeometryPolygon)//判断层是否为线层
            {
                //IFeatureLayer l = MapCtr.Map.get_Layer(0) as IFeatureLayer;
                IFeatureClass fc = l.FeatureClass;
                IFeatureClassWrite fr = fc as IFeatureClassWrite;
                IWorkspaceEdit w = (fc as IDataset).Workspace as IWorkspaceEdit;
                IFeature f;
                //可选参数的设置 
                object Missing = Type.Missing;
                IPoint p = new PointClass();
                w.StartEditing(true);
                w.StartEditOperation();

                f = fc.CreateFeature();
                //定义一个多义线对象 
                IRgbColor color = new RgbColor();
                // 设置颜色属性
                color.Red = 255;
                color.Transparency = 255;
                IGeometry iGeom = axMapControl1.TrackPolygon();
                AddRegion(axMapControl1, iGeom);
                f.Shape = iGeom;
                fr.WriteFeature(f);
                w.StopEditOperation();
                w.StopEditing(true);


            }
        }

        ///<summary>
        ///添加临时元素到地图窗口上
        ///</summary>
        ///<param name="pMapCtrl">地图控件</param>
        ///<param name="pEle">单个元素</param>
        ///<param name="pEleColl">元素集合</param>
        public void AddTempElement(AxMapControl pMapCtrl, IElement pEle, IElementCollection pEleColl)
        {
            try
            {
                IMap pMap = pMapCtrl.Map;
                IGraphicsContainer pGCs = pMap as IGraphicsContainer;
                if (pEle != null)
                    pGCs.AddElement(pEle, 0);

                if (pEleColl != null)
                    if (pEleColl.Count > 0)
                        pGCs.AddElements(pEleColl, 0);
                IActiveView pAV = (IActiveView)pMap;
                //需要刷新才能即时显示
                pAV.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, pAV.Extent);
            }
            catch (Exception Err)
            {
                MessageBox.Show(Err.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        ///<summary>
        ///获取符号库中符号
        ///</summary>
        ///<param name="sServerStylePath">符号库全路径名称</param>
        ///<param name="sGalleryClassName">GalleryClass名称</param>
        ///<param name="symbolName">符号名称</param>
        ///<returns>符号</returns>
        private ISymbol GetSymbol(string sServerStylePath, string sGalleryClassName, string symbolName)
        {
            try
            {
                //ServerStyleGallery对象
                IStyleGallery pStyleGaller = new ServerStyleGalleryClass();
                IStyleGalleryStorage pStyleGalleryStorage = pStyleGaller as IStyleGalleryStorage;

                IEnumStyleGalleryItem pEnumSyleGalleryItem = null;
                IStyleGalleryItem pStyleGallerItem = null;
                IStyleGalleryClass pStyleGalleryClass = null;
                //使用IStyleGalleryStorage接口的AddFile方法加载ServerStyle文件
                pStyleGalleryStorage.AddFile(sServerStylePath);
                //遍历ServerGallery中的Class
                for (int i = 0; i < pStyleGaller.ClassCount; i++)
                {
                    pStyleGalleryClass = pStyleGaller.get_Class(i);
                    if (pStyleGalleryClass.Name != sGalleryClassName)
                        continue;
                    //获取EnumStyleGalleryItem对象
                    pEnumSyleGalleryItem = pStyleGaller.get_Items(sGalleryClassName, sServerStylePath, "");
                    pEnumSyleGalleryItem.Reset();
                    //遍历pEnumSyleGalleryItem
                    pStyleGallerItem = pEnumSyleGalleryItem.Next();
                    while (pStyleGallerItem != null)
                    {
                        if (pStyleGallerItem.Name == symbolName)
                        {
                            //获取符号
                            ISymbol pSymbol = pStyleGallerItem.Item as ISymbol;
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(pEnumSyleGalleryItem);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(pStyleGalleryClass);
                            return pSymbol;
                        }
                        pStyleGallerItem = pEnumSyleGalleryItem.Next();
                    }
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pEnumSyleGalleryItem);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pStyleGalleryClass);
                return null;
            }
            catch (Exception Err)
            {
                MessageBox.Show(Err.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
        }

        ///<summary>
        ///设置要素图层唯一值符号化
        ///</summary>
        ///<param name="pFeatureLayer"></param>
        private void UniqueValueRenderFlyr(AxMapControl axMapControl1, IFeatureLayer pFeatureLayer)
        {
            try
            {
                //创建UniqueValueRendererClass对象
                IUniqueValueRenderer pUVRender = new UniqueValueRendererClass();
                List<string> pFieldValues = new List<string>();
                pFieldValues.Add("Hospital 2");
                pFieldValues.Add("School 1");
                pFieldValues.Add("Airport");
                for (int i = 0; i < pFieldValues.Count; i++)
                {
                    ISymbol pSymbol = new SimpleMarkerSymbolClass();
                    pSymbol = GetSymbol(@"D:\Program Files\ArcGIS\Styles\ESRI.ServerStyle", "Marker Symbols", pFieldValues[i]);
                    //添加唯一值符号化字段值和相对应的符号
                    pUVRender.AddValue(pFieldValues[i], pFieldValues[i], pSymbol);
                }
                //设置唯一值符号化的字段个数和字段名
                pUVRender.FieldCount = 1;
                pUVRender.set_Field(0, "类别");
                IGeoFeatureLayer pGFeatureLyr = pFeatureLayer as IGeoFeatureLayer;
                //设置IGeofeatureLayer的Renderer属性
                pGFeatureLyr.Renderer = pUVRender as IFeatureRenderer;
            }
            catch (Exception Err)
            {
                MessageBox.Show(Err.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 创建文本提示框
        /// </summary>
        /// <param name="x">提示框标识的位置X坐标</param>
        /// <param name="y">提示框标识的位置Y坐标</param>
        public void CreateTextElment(AxMapControl axMapControl1, double x, double y, string strText)
        {
            IPoint pPoint = new PointClass();
            IMap pMap = axMapControl1.Map;
            IActiveView pActiveView = pMap as IActiveView;
            IGraphicsContainer pGraphicsContainer;
            IElement pElement = new MarkerElementClass();
            IElement pTElement = new TextElementClass();
            pGraphicsContainer = (IGraphicsContainer)pActiveView;
            IFormattedTextSymbol pTextSymbol = new TextSymbolClass();
            IBalloonCallout pBalloonCallout = CreateBalloonCallout(x, y);
            IRgbColor pColor = new RgbColorClass();
            pColor.Red = 150;
            pColor.Green = 0;
            pColor.Blue = 0;
            pTextSymbol.Color = pColor;
            ITextBackground pTextBackground;
            pTextBackground = (ITextBackground)pBalloonCallout;
            pTextSymbol.Background = pTextBackground;
            ((ITextElement)pTElement).Symbol = pTextSymbol;
            ((ITextElement)pTElement).Text = strText;

            IPoint p = new PointClass();
            //设置点的坐标 
            p.PutCoords(x, y);
            IElementProperties ipElemProp;
            IMarkerElement ipMarkerElement = new MarkerElementClass();
            IPictureMarkerSymbol ipPicMarker = new PictureMarkerSymbolClass();
            ipPicMarker.CreateMarkerSymbolFromFile(esriIPictureType.esriIPictureBitmap, "D:\\pro\\ArcGisView\\ArcGisView\\1.bmp");
            ipPicMarker.Size = 24;
            IRgbColor ipRGBTrans = new RgbColorClass();
            ipRGBTrans.RGB = 0xffffff;
            ipPicMarker.BitmapTransparencyColor = ipRGBTrans as IColor;
            ipMarkerElement.Symbol = ipPicMarker as IMarkerSymbol;
            IElement ipElement = ipMarkerElement as IElement;
            ipElement.Geometry = p as IGeometry;
            axMapControl1.ActiveView.GraphicsContainer.AddElement(ipElement, 0);

            pPoint.X = x + 42;
            pPoint.Y = y + 42;
            pTElement.Geometry = pPoint;
            pGraphicsContainer.AddElement(pTElement, 1);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }
        /// <summary>
        /// 创建balloon型提示框
        /// </summary>
        /// <param name="x">提示框所在位置X坐标</param>
        /// <param name="y">提示框所在位置Y坐标</param>
        /// <returns></returns>
        public IBalloonCallout CreateBalloonCallout(double x, double y)
        {
            IRgbColor pRgbClr = new RgbColorClass();
            pRgbClr.Red = 255;
            pRgbClr.Blue = 255;
            pRgbClr.Green = 255;
            ISimpleFillSymbol pSmplFill = new SimpleFillSymbolClass();
            pSmplFill.Color = pRgbClr;
            pSmplFill.Style = esriSimpleFillStyle.esriSFSSolid;
            IBalloonCallout pBllnCallout = new BalloonCalloutClass();
            pBllnCallout.Style = esriBalloonCalloutStyle.esriBCSRoundedRectangle;
            pBllnCallout.Symbol = pSmplFill;
            pBllnCallout.LeaderTolerance = 1;
            IPoint pPoint = new ESRI.ArcGIS.Geometry.PointClass();
            pPoint.X = x;
            pPoint.Y = y;
            pBllnCallout.AnchorPoint = pPoint;
            return pBllnCallout;
        }

        /// <summary>
        /// 删除图元
        /// </summary>
        public void DelFeature(AxMapControl axMapControl1, IFeatureLayer feaLayer, string FeaKey)
        {
            IDataset ipDataset = feaLayer.FeatureClass as IDataset;
            IWorkspaceEdit ipwsEdit = ipDataset.Workspace as IWorkspaceEdit;
            IQueryFilter ipQueryFilter = new QueryFilterClass();
            ipQueryFilter.WhereClause = string.Format("{0}={1}", feaLayer.FeatureClass.OIDFieldName, FeaKey);
            IFeatureCursor ipFeatureCursor = feaLayer.FeatureClass.Search(ipQueryFilter, false);
            if (ipFeatureCursor != null)
            {
                IFeature ipFt = ipFeatureCursor.NextFeature();
                if (ipFt != null)
                {
                    //if (!ipwsEdit.IsBeingEdited())
                    //{
                    //    ipwsEdit.StartEditing(false);
                    //}

                    //ipwsEdit.StopEditing(true);
                    //DelFeatureByKeyName("MARKER_PIN_" + SelEditLayer + ipFt.OID.ToString());
                    axMapControl1.ActiveView.Refresh();

                    // this._mapObject.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, GetLayerByName(SelEditLayer), null);
                }
            }
        }

        /// <summary>
        /// 更新图元属性值
        /// </summary>
        /// <param name="FieldTable">图元信息列表</param>
        /// <param name="feaLayer">选择的图层名</param>
        public bool UpdateFeatureValue(Dictionary<string, string> FieldTable, IFeatureLayer feaLayer)
        {
            string FtOId = "";

            FtOId = FieldTable[feaLayer.FeatureClass.OIDFieldName.ToLower()];

            IQueryFilter ipQueryFilter = new QueryFilterClass();
            ipQueryFilter.WhereClause = string.Format("{0}={1}", feaLayer.FeatureClass.OIDFieldName, FtOId);
            IFeatureCursor ipFeatureCursor = feaLayer.FeatureClass.Update(ipQueryFilter, false);
            if (ipFeatureCursor != null)
            {
                IFeature ipFt = ipFeatureCursor.NextFeature();
                if (ipFt != null)
                {
                    for (int i = 0; i < ipFt.Fields.FieldCount; i++)
                    {
                        string strKey = ipFt.Fields.get_Field(i).Name.ToLower();
                        if (FieldTable.ContainsKey(strKey))
                        {
                            switch (ipFt.Fields.get_Field(i).Type)
                            {
                                case esriFieldType.esriFieldTypeString:
                                    {
                                        string PropeValue = FieldTable[strKey];
                                        if (ipFt.Fields.get_Field(i).Length >= PropeValue.Length)
                                        {
                                            if (ipFt.Fields.get_Field(i).CheckValue(PropeValue))
                                            {
                                                ipFt.set_Value(i, PropeValue);
                                            }
                                            else
                                            {
                                                MessageBox.Show("取值不正确",
                                                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                return false;
                                            }
                                        }
                                        else
                                        {

                                            MessageBox.Show("不能超过" + ipFt.Fields.get_Field(i).Length + "字节",
                                                "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return false;

                                        }
                                    }
                                    break;
                                case esriFieldType.esriFieldTypeSmallInteger:
                                    {
                                        if (FieldTable[strKey].Trim().Equals(""))
                                            break;
                                        ushort PropeValue = Convert.ToUInt16(FieldTable[strKey]);
                                        //if (ipFt.Fields.get_Field(i).Length >= PropeValue.ToString().Length)
                                        //{
                                            if (ipFt.Fields.get_Field(i).CheckValue(PropeValue))
                                            {
                                                ipFt.set_Value(i, PropeValue);
                                            }
                                            else
                                            {
                                                MessageBox.Show("取值不正确",
                                                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                return false;
                                            }
                                        //}
                                        //else
                                        //{

                                        //    MessageBox.Show("不能超过" + ipFt.Fields.get_Field(i).Length + "位数字",
                                        //        "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);


                                        //    return false;

                                        //}
                                    }
                                    break;
                                case esriFieldType.esriFieldTypeInteger:
                                    {
                                        if (FieldTable[strKey].Trim().Equals(""))
                                            break;
                                        int PropeValue = Convert.ToInt32(FieldTable[strKey]);
                                        if (ipFt.Fields.get_Field(i).Length >= PropeValue.ToString().Length)
                                        {
                                            if (ipFt.Fields.get_Field(i).CheckValue(PropeValue))
                                            {
                                                ipFt.set_Value(i, PropeValue);
                                            }
                                            else
                                            {
                                                MessageBox.Show("取值不正确",
                                                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);


                                                return false;
                                            }
                                        }
                                        else
                                        {


                                            MessageBox.Show("不能超过" + ipFt.Fields.get_Field(i).Length + "位数字",
                                                "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);


                                            return false;

                                        }
                                    }
                                    break;
                                case esriFieldType.esriFieldTypeDouble:
                                    {
                                        if (FieldTable[strKey].Trim().Equals(""))
                                            break;
                                        double PropeValue = Convert.ToDouble(FieldTable[strKey]);
                                        if (ipFt.Fields.get_Field(i).Length >= PropeValue.ToString().Replace(".", "").Length)
                                        {
                                            if (ipFt.Fields.get_Field(i).CheckValue(PropeValue))
                                            {
                                                ipFt.set_Value(i, PropeValue);
                                            }
                                            else
                                            {
                                                MessageBox.Show("取值不正确",
                                                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);


                                                return false;
                                            }
                                        }
                                        else
                                        {

                                            {
                                                MessageBox.Show("不能超过" + ipFt.Fields.get_Field(i).Length + "位数字",
                                                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);


                                                return false;
                                            }
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    ipFeatureCursor.UpdateFeature(ipFt);
                }
                else
                {
                    string msg = "";
                    msg = "地物已删除！";
                    MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                return true;
            }
            else
            {
                string msg = "";

                msg = "地物已删除！";
                MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }


        /// <summary>
        /// 根据名称获取地图图层接口
        /// </summary>
        /// <param name="layer">图层名</param>
        public ILayer GetLayerByName(AxMapControl axMapControl1, string layer)
        {
            for (int i = 0; i < axMapControl1.LayerCount; i++)
            {
                if (axMapControl1.get_Layer(i).Name.Equals(layer))
                {
                    return axMapControl1.get_Layer(i);
                }
            }
            return null;
        }
    }
}