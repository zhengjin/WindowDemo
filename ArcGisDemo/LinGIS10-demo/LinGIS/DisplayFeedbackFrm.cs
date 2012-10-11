using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;


namespace LinGIS
{
    public partial class DisplayFeedbackFrm : Form
    {
        public DisplayFeedbackFrm()
        {
            InitializeComponent();
        }


        

        private IGraphicsContainer pGraphicContainer;
        private IActiveView pActiveView;
        private IDisplayFeedback pDisplayFeedback;
        private IScreenDisplay pScreenDisplay;
        private IElement hitElement;//鼠标点击的element
        private IEnumElement pEnumElement;//element集合,移动几何对象中需要用到
        private IPoint moveGeometryStartPoint;//移动几何对象时开始的位置

        private void Form1_Load(object sender, EventArgs e)
        {
            this.pGraphicContainer = this.axMapControl1.ActiveView.FocusMap as IGraphicsContainer;
            this.pActiveView = this.axMapControl1.ActiveView;
            this.pScreenDisplay = this.axMapControl1.ActiveView.ScreenDisplay;
        }
        /// <summary>
        /// 鼠标点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axMapControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
        {
            try
            {
                if (e.button == 1)//按左键的话
                {
                    IPoint pPoint = new PointClass();//在鼠标点击的位置生成一个点
                    pPoint.PutCoords(e.mapX, e.mapY);

                    if (this.btnNewPolyline.Checked)//画多义线
                    {
                        if (this.pDisplayFeedback == null)//如果是第一次点击,就建立第一个节点
                        {
                            this.pDisplayFeedback = new NewLineFeedbackClass();
                            this.pDisplayFeedback.Display = this.pScreenDisplay;
                            ((INewLineFeedback)this.pDisplayFeedback).Start(pPoint);
                        }
                        else//如果不是第一次点击,就添加节点
                        {
                            ((INewLineFeedback)this.pDisplayFeedback).AddPoint(pPoint);
                        }
                    }
                    else if (this.btnLineMovePoint.Checked)//移动多义线节点
                    {
                        IElement pElement = this.getElement(pPoint, esriGeometryType.esriGeometryPolyline);
                        if (pElement != null)
                        {
                            IPolyline pPolyline = pElement.Geometry as IPolyline;
                            IHitTest pHitTest = pPolyline as IHitTest;
                            IPoint hitPoint = new PointClass();
                            double distance = 0;
                            bool isOnRightSide = false;
                            int hitPartIndex = 0;
                            int hitSegmentIndex = 0;
                            bool isHit = pHitTest.HitTest(pPoint, this.axMapControl1.ActiveView.Extent.Width / 100, esriGeometryHitPartType.esriGeometryPartVertex, hitPoint, ref distance, ref hitPartIndex, ref hitSegmentIndex, ref isOnRightSide);
                            if (isHit)
                            {
                                this.pDisplayFeedback = new LineMovePointFeedbackClass();
                                this.pDisplayFeedback.Display = this.pScreenDisplay;
                                ((ILineMovePointFeedback)this.pDisplayFeedback).Start(pPolyline, hitSegmentIndex, pPoint);
                            }
                        }
                    }
                    else if (this.btnNewCircle.Checked)//画圆
                    {
                        this.pDisplayFeedback = new NewCircleFeedbackClass();
                        this.pDisplayFeedback.Display = this.pScreenDisplay;
                        ((INewCircleFeedback)this.pDisplayFeedback).Start(pPoint);
                    }
                    else if (this.btnNewEnvelope.Checked)//画矩形
                    {
                        this.pDisplayFeedback = new NewEnvelopeFeedbackClass();
                        this.pDisplayFeedback.Display = this.pScreenDisplay;
                        ((INewEnvelopeFeedback)this.pDisplayFeedback).Constraint = esriEnvelopeConstraints.esriEnvelopeConstraintsNone;
                        ((INewEnvelopeFeedback)this.pDisplayFeedback).Start(pPoint);
                    }
                    else if (this.btnNewPolygon.Checked)//画多边形
                    {
                        if (this.pDisplayFeedback == null)//如果是第一次点击,就建立第一个节点
                        {
                            this.pDisplayFeedback = new NewPolygonFeedbackClass();
                            this.pDisplayFeedback.Display = this.pScreenDisplay;
                            ((INewPolygonFeedback)this.pDisplayFeedback).Start(pPoint);
                        }
                        else//如果不是第一次点击,就添加节点
                        {
                            ((INewPolygonFeedback)this.pDisplayFeedback).AddPoint(pPoint);
                        }
                    }
                    else if (this.btnPolygonMovePoint.Checked)//移动多边形节点
                    {
                        IElement pElement = this.getElement(pPoint, esriGeometryType.esriGeometryPolygon);
                        if (pElement != null)
                        {
                            IPolygon pPolygon = pElement.Geometry as IPolygon;
                            IHitTest pHitTest = pPolygon as IHitTest;
                            IPoint hitPoint = new PointClass();
                            double distance = 0;
                            bool isOnRightSide = true;
                            int hitPartIndex = 0;
                            int hitSegmentIndex = 0;
                            bool isHit = pHitTest.HitTest(pPoint, this.axMapControl1.ActiveView.Extent.Width / 100, esriGeometryHitPartType.esriGeometryPartVertex, hitPoint, ref distance, ref hitPartIndex, ref hitSegmentIndex, ref isOnRightSide);
                            if (isHit)
                            {
                                this.pDisplayFeedback = new PolygonMovePointFeedbackClass();
                                this.pDisplayFeedback.Display = this.pScreenDisplay;
                                ((IPolygonMovePointFeedback)this.pDisplayFeedback).Start(pPolygon, hitSegmentIndex, pPoint);
                            }
                        }
                    }
                    else if (this.btnNewBezierCurve.Checked)//新建Bezier曲线
                    {
                        if (this.pDisplayFeedback == null)//如果是第一次点击
                        {
                            this.pDisplayFeedback = new NewBezierCurveFeedbackClass();
                            this.pDisplayFeedback.Display = this.pScreenDisplay;
                            ((INewBezierCurveFeedback)this.pDisplayFeedback).Start(pPoint);
                        }
                        else//如果不是第一次点击
                        {
                            ((INewBezierCurveFeedback)this.pDisplayFeedback).AddPoint(pPoint);
                        }
                    }
                    else if (this.btnMoveGeometry.Checked)//移动几何对象
                    {
                        this.pEnumElement = this.pGraphicContainer.LocateElements(pPoint, this.pActiveView.Extent.Width / 100);
                        if (this.pEnumElement != null)
                        {
                            this.pDisplayFeedback = new MoveGeometryFeedbackClass();
                            this.pDisplayFeedback.Display = this.pScreenDisplay;

                            IElement pElement;
                            this.pEnumElement.Reset();
                            //需要逐个逐个添加Geometry
                            for (pElement = this.pEnumElement.Next(); pElement != null; pElement = this.pEnumElement.Next())
                            {
                                ((IMoveGeometryFeedback)this.pDisplayFeedback).AddGeometry(pElement.Geometry);
                            }
                            ((IMoveGeometryFeedback)this.pDisplayFeedback).Start(pPoint);
                            this.moveGeometryStartPoint = pPoint;
                        }
                    }
                    else if (this.btnStretchLine.Checked)//拉伸多义线
                    {
                        IElement pElement = this.getElement(pPoint, esriGeometryType.esriGeometryPolyline);
                        if (pElement != null)
                        {
                            IPolyline pPolyline = pElement.Geometry as IPolyline;
                            this.pDisplayFeedback = new StretchLineFeedbackClass();
                            this.pDisplayFeedback.Display = this.pScreenDisplay;
                            ((IStretchLineFeedback)this.pDisplayFeedback).Anchor = pPolyline.FromPoint;
                            ((IStretchLineFeedback)this.pDisplayFeedback).Start(pPolyline, pPoint);
                        }
                    }

                }
                else if (e.button == 2)//按右键的就把画面清空
                {
                    if (this.pDisplayFeedback == null)
                    {
                        this.pGraphicContainer.DeleteAllElements();
                        this.axMapControl1.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        
        /// <summary>
        /// 根据点击位置和需要的element类型，取得在该位置的给类型的element
        /// </summary>
        /// <param name="pPoint"></param>
        /// <param name="geometryType"></param>
        /// <returns></returns>
        private IElement getElement(IPoint pPoint,esriGeometryType geometryType)
        {
            IEnumElement pEnumElement = this.pGraphicContainer.LocateElements(pPoint, this.axMapControl1.ActiveView.Extent.Width / 100);
            if (pEnumElement != null)
            {
                pEnumElement.Reset();
                IElement pElement;
                for (pElement=pEnumElement.Next(); pElement!= null;pElement=pEnumElement.Next())
                {
                    if (pElement.Geometry.GeometryType == geometryType)
                    {
                        this.hitElement = pElement;
                        return pElement;
                    }
                }
                return null;
            }
            return null;

        }
        
        /// <summary>
        /// 鼠标移动,各种工作中鼠标移动的处理是一致的
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            IPoint pPoint = new PointClass();
            pPoint.PutCoords(e.mapX, e.mapY);
            if (this.pDisplayFeedback != null)
            {
                this.pDisplayFeedback.MoveTo(pPoint);
            }
        }
        /// <summary>
        /// 双击鼠标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axMapControl1_OnDoubleClick(object sender, IMapControlEvents2_OnDoubleClickEvent e)
        {
            if (this.pDisplayFeedback != null)
            {
                IGeometry pGeometry = null;
                if (this.btnNewPolyline.Checked)//新建多义线
                {
                    pGeometry = ((INewLineFeedback)this.pDisplayFeedback).Stop();
                }
                else if (this.btnNewPolygon.Checked)//新建多边形
                {
                    pGeometry = ((INewPolygonFeedback)this.pDisplayFeedback).Stop();
                }
                else if (this.btnNewBezierCurve.Checked)//新建Bezier曲线
                {
                    pGeometry = ((INewBezierCurveFeedback)this.pDisplayFeedback).Stop();
                }
                this.pDisplayFeedback = null;
                this.AddElement(pGeometry);
            }
        }
        
        /// <summary>
        /// 把Geometry弄成一个element，添加到地图上
        /// </summary>
        /// <param name="pGeometry"></param>
        private void AddElement(IGeometry pGeometry)
        {
            try
            {
                IElement pElement = null;

                ISimpleLineSymbol pSimpleLineSymbol = new SimpleLineSymbolClass();
                pSimpleLineSymbol.Width = 2;
                pSimpleLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;

                ISimpleFillSymbol pSimpleFillSymbol = new SimpleFillSymbolClass();
                pSimpleFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
                pSimpleFillSymbol.Outline = pSimpleLineSymbol;

                if (pGeometry.GeometryType == esriGeometryType.esriGeometryPolyline)//多义线
                {
                    ILineElement pLineElement = new LineElementClass();
                    pLineElement.Symbol = pSimpleLineSymbol;
                    pElement = pLineElement as IElement;
                    pElement.Geometry = pGeometry;
                }
                else if (pGeometry.GeometryType == esriGeometryType.esriGeometryCircularArc)//圆
                {
                    ISegmentCollection pSegmentCollection;
                    pSegmentCollection = new PolygonClass();
                    object Missing = Type.Missing;//注意
                    pSegmentCollection.AddSegment(pGeometry as ISegment, ref Missing, ref Missing);//后两个参数必须是这样,帮助说的,为什么??

                    pElement = new CircleElementClass();
                    pElement.Geometry = pSegmentCollection as IGeometry;
                    IFillShapeElement pFillShapeElement = pElement as IFillShapeElement;
                    pFillShapeElement.Symbol = pSimpleFillSymbol;
                }
                else if (pGeometry.GeometryType == esriGeometryType.esriGeometryEnvelope)//矩形
                {
                    pElement = new RectangleElementClass();
                    pElement.Geometry = pGeometry;
                    IFillShapeElement pFillShapeElement = pElement as IFillShapeElement;
                    pFillShapeElement.Symbol = pSimpleFillSymbol;
                }
                else if (pGeometry.GeometryType == esriGeometryType.esriGeometryPolygon)//多边形
                {
                    pElement = new PolygonElementClass();
                    pElement.Geometry = pGeometry;
                    IFillShapeElement pFillShapeElement = pElement as IFillShapeElement;
                    pFillShapeElement.Symbol = pSimpleFillSymbol;
                }
                else if (pGeometry.GeometryType == esriGeometryType.esriGeometryBezier3Curve)//Bezier曲线
                {
                    pElement = new LineElementClass();
                    pElement.Geometry = pGeometry;
                    ILineElement pLineElement = pElement as ILineElement;
                    pLineElement.Symbol = pSimpleLineSymbol;
                }

                this.pGraphicContainer.AddElement(pElement, 0);
                this.axMapControl1.Refresh(esriViewDrawPhase.esriViewGraphics, null, null);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNewPolyline_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem i in this.toolStrip1.Items)
            {
                ((ToolStripButton)i).Checked = false;
            }
            ((ToolStripButton)sender).Checked = true;
            this.pDisplayFeedback = null;
        }
        /// <summary>
        /// 松开鼠标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axMapControl1_OnMouseUp(object sender, IMapControlEvents2_OnMouseUpEvent e)
        {
            if (this.pDisplayFeedback != null)
            {
                if (this.btnLineMovePoint.Checked)//移动多义线节点
                {
                    IGeometry pGeometry = ((ILineMovePointFeedback)this.pDisplayFeedback).Stop();
                    if (pGeometry != null)
                    {
                        this.hitElement.Geometry = pGeometry;
                        this.pGraphicContainer.UpdateElement(this.hitElement);
                    }

                    this.hitElement = null;
                    this.pDisplayFeedback = null;
                    this.axMapControl1.Refresh(esriViewDrawPhase.esriViewGraphics, null, null);

                }
                else if (this.btnNewCircle.Checked)//画圆
                {
                    IGeometry pGeometry = ((INewCircleFeedback)this.pDisplayFeedback).Stop();

                    this.AddElement(pGeometry);
                    this.pDisplayFeedback = null;
                }
                else if (this.btnNewEnvelope.Checked)//画矩形
                {
                    IGeometry pGeometry = ((INewEnvelopeFeedback)this.pDisplayFeedback).Stop();

                    this.AddElement(pGeometry);
                    this.pDisplayFeedback = null;
                }
                else if (this.btnPolygonMovePoint.Checked)//移动多边形节点
                {
                    IGeometry pGeometry = ((IPolygonMovePointFeedback)this.pDisplayFeedback).Stop();
                    if (pGeometry != null)
                    {
                        this.hitElement.Geometry = pGeometry;
                        this.pGraphicContainer.UpdateElement(this.hitElement);
                    }

                    this.hitElement = null;
                    this.pDisplayFeedback = null;
                    this.axMapControl1.Refresh(esriViewDrawPhase.esriViewGraphics, null, null);

                }
                else if (this.btnMoveGeometry.Checked)//移动几何对象
                {
                    if (this.pEnumElement != null)
                    {
                        this.pEnumElement.Reset();
                        IElement pElement;
                        ITransform2D pTransform2D;
                        //需要用ITransform2D接口逐个逐个的移动element
                        for (pElement = this.pEnumElement.Next(); pElement != null; pElement = this.pEnumElement.Next())
                        {
                            pTransform2D = pElement as ITransform2D;
                            pTransform2D.Move(e.mapX - this.moveGeometryStartPoint.X, e.mapY - this.moveGeometryStartPoint.Y);
                            this.pGraphicContainer.UpdateElement(pElement);
                        }
                        this.moveGeometryStartPoint = null;
                        this.pEnumElement = null;
                        this.pDisplayFeedback = null;
                        this.axMapControl1.Refresh(esriViewDrawPhase.esriViewGraphics, null, null);
                    }
                }
                else if (this.btnStretchLine.Checked)//拉伸多义线
                {
                    if (this.hitElement != null)
                    {
                        IGeometry pGeometry = ((IStretchLineFeedback)this.pDisplayFeedback).Stop();
                        this.hitElement.Geometry = pGeometry;
                        this.pGraphicContainer.UpdateElement(this.hitElement);

                        this.hitElement = null;
                        this.pDisplayFeedback = null;
                        this.axMapControl1.Refresh(esriViewDrawPhase.esriViewGraphics, null, null);
                    }
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown();
        }

        private void axMapControl1_OnExtentUpdated(object sender, IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            //if (this.pDisplayFeedback != null)
            //{
            //    this.pDisplayFeedback.Refresh(this.pScreenDisplay.hWnd);
            //}
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem i in this.toolStrip1.Items)
            {
                ((ToolStripButton)i).Checked = false;
            }
            ((ToolStripButton)sender).Checked = true;
            this.pDisplayFeedback = null;
            NewPolyline pNewPolyline = new NewPolyline();
            pNewPolyline.OnCreate(this.axMapControl1.Object);
            this.axMapControl1.CurrentTool = ((ITool)pNewPolyline);
        }




       
    }
}