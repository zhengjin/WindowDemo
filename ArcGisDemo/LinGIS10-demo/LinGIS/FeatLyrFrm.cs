using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;

namespace LinGIS
{
    public partial class FeatLyrFrm : DevComponents.DotNetBar.Office2007Form
    {
        private ILayer pLayer;
        public ILayer Layer
        {
            set
            {
                this.pLayer = value;
            }
        }

        public FeatLyrFrm()
        {
            InitializeComponent();
        }

        private void FeatLyrFrm_Load(object sender, EventArgs e)
        {

        }

        private void tabCtrlLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(this.tpgFields.SelectedTab.Name);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                this.resultGeneralTab();
                this.resultSelectionTab();
                this.resultSymbologyTab();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FeatLyrFrm_Shown(object sender, EventArgs e)
        {
            this.loadGeneralTab();
            this.loadSelectionTab();
            this.loadSourceTab();
            this.loadFieldsTab();
            this.loadSymbologyTab();
        }



        #region GerneralTab
        private void loadGeneralTab()
        {
            this.txtLayerName.Text = this.pLayer.Name;
            this.txtLayerDescription.Text = ((ILayerGeneralProperties)pLayer).LayerDescription;
            this.ckbVisible.Checked = pLayer.Visible;
            if (pLayer.MaximumScale == 0 && pLayer.MinimumScale == 0)
            {
                this.rbnNone.Checked = true;
                this.cbbMaxScale.Enabled = false;
                this.cbbMinScale.Enabled = false;
            }
            else
            {
                this.rbnRange.Checked = true;
                this.cbbMaxScale.Enabled = true;
                this.cbbMaxScale.Text = pLayer.MaximumScale.ToString();
                this.cbbMinScale.Enabled = true;
                this.cbbMinScale.Text = pLayer.MinimumScale.ToString();
            }
        }

        private void rbnNone_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbnNone.Checked == true)
            {
                this.cbbMinScale.Enabled = false;
                this.cbbMaxScale.Enabled = false;
            }
            else
            {
                this.cbbMinScale.Enabled = true;
                this.cbbMaxScale.Enabled = true;
            }
        }

        private void resultGeneralTab()
        {
            pLayer.Name = this.txtLayerName.Text;
            ((ILayerGeneralProperties)pLayer).LayerDescription = this.txtLayerDescription.Text;
            if (this.rbnNone.Checked)
            {
                pLayer.MinimumScale = 0;
                pLayer.MaximumScale = 0;
            }
            else if (this.rbnRange.Checked)
            {
                pLayer.MaximumScale = double.Parse(this.cbbMaxScale.Text);
                pLayer.MinimumScale = double.Parse(this.cbbMinScale.Text);
            }
        }

        #endregion

        #region SourceTab
        private void loadSourceTab()
        {
            //Extent
            IEnvelope pEnvelope = ((IGeoDataset)((IFeatureLayer)pLayer).FeatureClass).Extent;
            this.txtExtentTop.Text = pEnvelope.YMax.ToString();
            this.txtExtentButtom.Text = pEnvelope.YMin.ToString();
            this.txtExtentLeft.Text = pEnvelope.XMin.ToString();
            this.txtExtentRight.Text = pEnvelope.XMax.ToString();
            //DataSource
            StringBuilder strDataSource = new StringBuilder();

            strDataSource.Append("DataType:");
            IFeatureLayer pFeatureLayer = (IFeatureLayer)this.pLayer;
            strDataSource.Append(pFeatureLayer.DataSourceType);
            IDataLayer2 pDataLayer2 = (IDataLayer2)this.pLayer;
            strDataSource.Append(":\n");
            strDataSource.Append(pDataLayer2.DataSourceName.NameString);

            IGeoDataset pGeoDataset = (IGeoDataset)this.pLayer;
            ISpatialReference pSpatialReference = pGeoDataset.SpatialReference;
            IProjectedCoordinateSystem5 pProCoorSys = pSpatialReference as IProjectedCoordinateSystem5;
            if (pProCoorSys != null)
            {
                strDataSource.Append("\nProjected Corrdinate System:");
                strDataSource.Append(pProCoorSys.Name);
                strDataSource.Append("\nProjection:");
                strDataSource.Append(pProCoorSys.Projection.Name);
                strDataSource.Append("\nFalse_Easting:");
                strDataSource.Append(pProCoorSys.FalseEasting);
                //strDataSource.AppendFormat("F8", pProCoorSys.FalseEasting);
                strDataSource.Append("\nFalse_Northing:");
                strDataSource.Append(pProCoorSys.FalseNorthing);
                strDataSource.Append("\nCentral_Meridian:");
                strDataSource.Append(pProCoorSys.get_CentralMeridian(true));
                strDataSource.Append("\nStandard)Parallel_1:");
                strDataSource.Append(pProCoorSys.StandardParallel1);
                strDataSource.Append("\nStandard_Parallel_2:");
                strDataSource.Append(pProCoorSys.StandardParallel2);
                strDataSource.Append("\nLatitude_Of_Origin:");
                strDataSource.Append(pProCoorSys.LatitudeOfOrigin);
                strDataSource.Append("\nLinear_Unit:");
                strDataSource.Append(pProCoorSys.CoordinateUnit.Name);
                strDataSource.Append("\n");
            }

            //IGeographicCoordinateSystem pGeoCoorSys = pProCoorSys.GeographicCoordinateSystem;
            IGeographicCoordinateSystem pGeoCoorSys = pSpatialReference as IGeographicCoordinateSystem;
            if (pGeoCoorSys != null)
            {
                strDataSource.Append("\nGeographic Coordinate System:");
                strDataSource.Append(pGeoCoorSys.Name);
                strDataSource.Append("\nDatum:");
                strDataSource.Append(pGeoCoorSys.Datum.Name);
                strDataSource.Append("\nPrime_Meridian:");
                strDataSource.Append(pGeoCoorSys.PrimeMeridian.Longitude);
                strDataSource.Append("\nAangular_Unit:");
                strDataSource.Append(pGeoCoorSys.CoordinateUnit.Name);
            }

            this.txtDataSource.Text = strDataSource.ToString();
        }
        #endregion

        #region SelectionTab
        private void loadSelectionTab()
        {
            IFeatureSelection pFeatureSelection = this.pLayer as IFeatureSelection;
            if (pFeatureSelection.SetSelectionSymbol == true)
            {
                this.rbnSelectionSymbol.Checked = true;
                this.selectionSymbol = pFeatureSelection.SelectionSymbol;
            }
            else
            {
                this.rbnSelectionDefault.Checked = true;
            }
        }
        private ISymbol selectionSymbol;
        private void btnSelectionSymbol_Click(object sender, EventArgs e)
        {
            SymbolSelectorFrm selectionSymbolSelectorFrm = new SymbolSelectorFrm(null, this.pLayer);
            if (selectionSymbolSelectorFrm.ShowDialog() == DialogResult.OK)
            {
                this.rbnSelectionSymbol.Checked = true;
                this.btnSelectionSymbol.BackgroundImage = selectionSymbolSelectorFrm.pSymbolImage;
                this.selectionSymbol = selectionSymbolSelectorFrm.pSymbol;
            }
        }

        private void btnSelectionColor_Click(object sender, EventArgs e)
        {
            if (this.colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.rbnSelectionColor.Checked = true;
                this.btnSelectionColor.BackColor = this.colorDialog.Color;
            }
        }
        /// <summary>
        /// 将.NET中的Color结构转换至于ArcGIS Engine中的IColor接口
        /// </summary>
        /// <param name="color">.NET中的System.Drawing.Color结构表示ARGB颜色</param>
        /// <returns>IColor</returns>
        public IColor ConvertColorToIColor(Color color)
        {
            IColor pColor = new RgbColorClass();
            pColor.RGB = color.B * 65536 + color.G * 256 + color.R;
            return pColor;
        }
        /// <summary>
        /// 将.NET中的Color结构转换至于ArcGIS Engine中的IRgbColor接口
        /// </summary>
        /// <param name="color">.NET中的System.Drawing.Color结构表示ARGB颜色</param>
        /// <returns>IRgbColor</returns>

        private void resultSelectionTab()
        {
            IFeatureSelection pFeatureSeletion = (IFeatureSelection)this.pLayer;
            if (this.rbnSelectionDefault.Checked == true)
            {
                pFeatureSeletion.SetSelectionSymbol = false;
            }
            else if (this.rbnSelectionSymbol.Checked == true)
            {
                pFeatureSeletion.SetSelectionSymbol = true;
                pFeatureSeletion.SelectionSymbol = this.selectionSymbol;
            }
            else if (this.rbnSelectionColor.Checked == true)
            {
                pFeatureSeletion.SetSelectionSymbol = true;
                pFeatureSeletion.SelectionColor = this.ConvertColorToIColor(this.btnSelectionColor.BackColor);
            }
        }
        #endregion

        #region FieldsTab
        string[] strfieldType = {"Short Integer","Long Integer","Float","Double",
                                 "Text","Date","Object ID","Geometry",
                                 "Blob","Raster","GUID","GlobalID"};
        private void loadFieldsTab()
        {
            this.lsvFields.Items.Clear();
            ITable pTable = this.pLayer as ITable;
            IFields pFields = pTable.Fields;
            IField pField;
            //string fieldType;
            for (int i = 0; i < pFields.FieldCount; i++)
            {
                pField = pFields.get_Field(i);
                this.lsvFields.Items.Add(pField.Name);
                this.lsvFields.Items[i].SubItems.Add(pField.AliasName);
                this.lsvFields.Items[i].SubItems.Add(this.strfieldType[(int)pField.Type]);
                this.lsvFields.Items[i].SubItems.Add(pField.Length.ToString());
                this.lsvFields.Items[i].SubItems.Add(pField.Precision.ToString());
                this.lsvFields.Items[i].SubItems.Add(pField.Scale.ToString());
            }
        }
        #endregion

        #region SymbologyTab
        
        private ISymbol singleSymbol;

        private void loadSymbologyTab()
        {
            IFeatureRenderer pFeatureRender = ((IGeoFeatureLayer)this.pLayer).Renderer;
            if (pFeatureRender is ISimpleRenderer)
            {
                this.trvSymbologyShows.SelectedNode = this.trvSymbologyShows.Nodes[0].Nodes[0];
                this.singleSymbol = ((ISimpleRenderer)pFeatureRender).Symbol;
            }
            else if (pFeatureRender is IUniqueValueRenderer)
            {
                this.trvSymbologyShows.SelectedNode = this.trvSymbologyShows.Nodes[1].Nodes[0];
            }
            else if (pFeatureRender is IClassBreaksRenderer)
            {
                this.trvSymbologyShows.SelectedNode = this.trvSymbologyShows.Nodes[2].Nodes[0];

                this.cbbClassBreakField.SelectedIndex = this.cbbClassBreakField.FindString(((IClassBreaksRenderer)pFeatureRender).Field);
            }
        }

        private void btnSingleSymbol_Click(object sender, EventArgs e)
        {
            SymbolSelectorFrm SingleSymbolSelctor = new SymbolSelectorFrm(null, this.pLayer);
            if (SingleSymbolSelctor.ShowDialog() == DialogResult.OK)
            {
                this.btnSingleSymbol.BackgroundImage = SingleSymbolSelctor.pSymbolImage;
                this.singleSymbol = SingleSymbolSelctor.pSymbol;
            }
        }

        private void resultSymbologyTab()
        {

            IGeoFeatureLayer pGeoFeatureLayer = this.pLayer as IGeoFeatureLayer;
            TreeNode currentNode = this.trvSymbologyShows.SelectedNode;
            if (currentNode.Text == "单一符号")
            {
                if (this.singleSymbol == null)
                {
                    return;
                }
                ISimpleRenderer pSimpleRender = new SimpleRendererClass();
                pSimpleRender.Symbol = this.singleSymbol;
                pSimpleRender.Label = this.txtSingleSymbolLabel.Text;
                pSimpleRender.Description = this.txtSingleSymbolDescription.Text;
                pGeoFeatureLayer.Renderer = pSimpleRender as IFeatureRenderer;
            }
            else if (currentNode.Text == "唯一值")
            {
                if (this.lsvUniqueValue.Items.Count == 0 || this.pUniValueColorRamp == null)
                {
                    return;
                }
                this.pUniValueColorRamp.Size = this.lsvUniqueValue.Items.Count - 1;
                bool IsColorRampCreatedOK = false;
                this.pUniValueColorRamp.CreateRamp(out IsColorRampCreatedOK);
                if (IsColorRampCreatedOK)
                {
                    IEnumColors pEnumColors = pUniValueColorRamp.Colors;
                    pEnumColors.Reset();

                    IUniqueValueRenderer pUniqueValueRender = new UniqueValueRendererClass();
                    pUniqueValueRender.FieldCount = 1;
                    pUniqueValueRender.set_Field(0, this.cbbUniValueField.Text);

                    IColor pColor;

                    if (((IFeatureLayer2)this.pLayer).ShapeType == esriGeometryType.esriGeometryPolygon)
                    {
                        ISimpleFillSymbol pSimpleFillSymbol;
                        for (int i = 0; i < pUniValueColorRamp.Size; i++)
                        {
                            pColor = pEnumColors.Next();
                            pSimpleFillSymbol = new SimpleFillSymbolClass();
                            pSimpleFillSymbol.Color = pColor;
                            pUniqueValueRender.AddValue(this.lsvUniqueValue.Items[i + 1].Text, "", (ISymbol)pSimpleFillSymbol);
                        }
                    }
                    else if (((IFeatureLayer2)this.pLayer).ShapeType == esriGeometryType.esriGeometryPolyline)
                    {
                        ISimpleLineSymbol pSimpleLineSymbol;
                        for (int i = 0; i < pUniValueColorRamp.Size; i++)
                        {
                            pColor = pEnumColors.Next();
                            pSimpleLineSymbol = new SimpleLineSymbolClass();
                            pSimpleLineSymbol.Color = pColor;
                            pUniqueValueRender.AddValue(this.lsvUniqueValue.Items[i + 1].Text, "", (ISymbol)pSimpleLineSymbol);
                        }
                    }
                    else if (((IFeatureLayer2)this.pLayer).ShapeType == esriGeometryType.esriGeometryPoint)
                    {
                        ISimpleMarkerSymbol pSimpleMarkerSymbol;
                        for (int i = 0; i < pUniValueColorRamp.Size; i++)
                        {
                            pColor = pEnumColors.Next();
                            pSimpleMarkerSymbol = new SimpleMarkerSymbolClass();
                            pSimpleMarkerSymbol.Color = pColor;
                            pUniqueValueRender.AddValue(this.lsvUniqueValue.Items[i + 1].Text, "", (ISymbol)pSimpleMarkerSymbol);
                        }
                    }

                    pGeoFeatureLayer.Renderer = (IFeatureRenderer)pUniqueValueRender;
                }
            }
            else if (currentNode.Text == "分级颜色")
            {
                if (this.lsvClassBreaksSymbol.Items.Count == 0 || this.pClassBreaksColorRamp == null)
                {
                    return;
                }
                int classCount = int.Parse(this.cbbClassBreaksCount.Text);
                
                IClassBreaksRenderer pClassBreaksRenderer = new ClassBreaksRendererClass();
                pClassBreaksRenderer.BreakCount = classCount;
                pClassBreaksRenderer.Field = this.cbbClassBreakField.Text;
                pClassBreaksRenderer.SortClassesAscending = true;


                IColorRamp pColorRamp = this.pClassBreaksColorRamp;
                pColorRamp.Size = classCount;
                bool ok;
                pColorRamp.CreateRamp(out ok);
                if (!ok)
                {
                    return;
                }
                IEnumColors pEnumColors = pColorRamp.Colors;
                pEnumColors.Reset();
                IColor pColor;
                if (((IFeatureLayer2)this.pLayer).ShapeType == esriGeometryType.esriGeometryPolygon)
                {
                    for (int i = 0; i < classCount; i++)//为每个值范围设置符号(此处为SimpleFillSymbol)
                    {
                        pColor = pEnumColors.Next();
                        ISimpleFillSymbol pSimpleFillSymbol = new SimpleFillSymbolClass();
                        pSimpleFillSymbol.Color = pColor;
                        pSimpleFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
                        
                        pClassBreaksRenderer.set_Break(i, this.classBreaks[i + 1]);//设置临界值,注意下标,关键!!!

                        pClassBreaksRenderer.set_Symbol(i, (ISymbol)pSimpleFillSymbol);//设置Symbol,关键!!!

                        pClassBreaksRenderer.set_Label(i, this.lsvClassBreaksSymbol.Items[i].Text);
                        pClassBreaksRenderer.set_Description(i, this.lsvClassBreaksSymbol.Items[i].Text);
                    }
                }
                else if (((IFeatureLayer2)this.pLayer).ShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    for (int i = 0; i < classCount; i++)//为每个值范围设置符号
                    {
                        pColor = pEnumColors.Next();
                        ISimpleLineSymbol pSimpleLineSymbol = new SimpleLineSymbolClass();
                        pSimpleLineSymbol.Color = pColor;
                        pClassBreaksRenderer.set_Symbol(i, (ISymbol)pSimpleLineSymbol);//设置Symbol,关键!!!
                        pClassBreaksRenderer.set_Label(i, this.lsvClassBreaksSymbol.Items[i].Text);
                        pClassBreaksRenderer.set_Break(i, this.classBreaks[i + 1]);//设置临界值,注意下标,关键!!!
                    }
                }
                else if (((IFeatureLayer2)this.pLayer).ShapeType == esriGeometryType.esriGeometryPoint)
                {
                    for (int i = 0; i < classCount; i++)//为每个值范围设置符号
                    {
                        pColor = pEnumColors.Next();
                        ISimpleMarkerSymbol pSimpleMarkerSymbol = new SimpleMarkerSymbolClass();
                        pSimpleMarkerSymbol.Color = pColor;
                        pClassBreaksRenderer.set_Symbol(i, (ISymbol)pSimpleMarkerSymbol);//设置Symbol,关键!!!
                        pClassBreaksRenderer.set_Label(i, this.lsvClassBreaksSymbol.Items[i].Text);
                        pClassBreaksRenderer.set_Break(i, this.classBreaks[i + 1]);//设置临界值,注意下标,关键!!!
                    }
                }

                pGeoFeatureLayer.Renderer = pClassBreaksRenderer as IFeatureRenderer;
            }

        }
        #endregion

        private void trvSymbologyShows_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Node.ExpandAll();
                this.trvSymbologyShows.SelectedNode = e.Node.NextVisibleNode;
            }
            else
            {
                string fieldType;
                if (e.Node.Text == "单一符号")
                {
                    this.panelUniValueSymbol.Visible = false;
                    this.panelClassBreaksSymbol.Visible = false;
                    this.panelSingleSymbol.Location = new System.Drawing.Point(170, 4);
                    this.panelSingleSymbol.Visible = true;
                }
                else if (e.Node.Text == "唯一值")
                {
                    this.panelSingleSymbol.Visible = false;
                    this.panelClassBreaksSymbol.Visible = false;
                    this.panelUniValueSymbol.Location = new System.Drawing.Point(170, 4);
                    this.panelUniValueSymbol.BringToFront();
                    this.panelUniValueSymbol.Visible = true;
                    //this.lsvUniqueValue.Items.Clear();
                    this.cbbUniValueField.Items.Clear();
                    this.cbbClassBreakField.Items.Add("无");
                    for (int i = 0; i < this.lsvFields.Items.Count; i++)
                    {
                        fieldType = this.lsvFields.Items[i].SubItems[2].Text;
                        if (fieldType != "Geometry" && fieldType != "Object ID" && fieldType != "Raster" && fieldType != "Blob")
                        {
                            this.cbbUniValueField.Items.Add(this.lsvFields.Items[i].Text);
                        }
                    }
                    this.cbbUniValueField.SelectedIndex = 0;
                }
                else if (e.Node.Text == "分级颜色")
                {
                    this.panelSingleSymbol.Visible = false;
                    this.panelUniValueSymbol.Visible = false;
                    this.panelClassBreaksSymbol.Location = new System.Drawing.Point(170, 4);
                    this.panelClassBreaksSymbol.Visible = true;
                    this.cbbClassBreakField.Items.Clear();
                    //this.cbbClassBreakNomalization.Items.Clear();
                    this.cbbClassBreakField.Items.Add("无");
                    for (int i = 0; i < this.lsvFields.Items.Count; i++)
                    {
                        fieldType = this.lsvFields.Items[i].SubItems[2].Text;
                        if (fieldType == "Short Integer" || fieldType == "Long Integer" || fieldType == "Float" || fieldType == "Double")
                        {
                            this.cbbClassBreakField.Items.Add(this.lsvFields.Items[i].Text);
                            //this.cbbClassBreakNomalization.Items.Add(this.lsvFields.Items[i].Text);
                        }
                    }
                    this.cbbClassBreakField.SelectedIndex = 0;
                    this.cbbClassBreakNomalization.SelectedIndex = 0;
                }
                
            }

        }

        /// <summary>
        /// 按下"添加所有值"按钮后,让table按当前字段的值进行排序,并把信息添加到listView里面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUniValueAddAllValues_Click(object sender, EventArgs e)
        {
            try
            {
                string currentFieldName = this.cbbUniValueField.Text;//当前字段名

                string currentFieldType = this.lsvFields.FindItemWithText(currentFieldName).SubItems[2].Text;//当前字段类型
                bool currentTypeIsNumeric = false;//判断当前字段类型是否为数字类型
                if (currentFieldType == "Short Integer" || currentFieldType == "Long Integer" || currentFieldType == "Float" || currentFieldType == "Double")
                {
                    currentTypeIsNumeric = true;
                }

                this.lsvUniqueValue.Items.Clear();

                //对Table中当前字段进行排序,把结果赋给Cursor
                ITable pTable = this.pLayer as ITable;
                ITableSort pTableSort = new TableSortClass();
                pTableSort.Table = pTable;
                pTableSort.Fields = currentFieldName;
                pTableSort.set_Ascending(currentFieldName, true);
                pTableSort.set_CaseSensitive(currentFieldName, true);
                pTableSort.Sort(null);//排序
                ICursor pCursor = pTableSort.Rows;

                //字段统计
                IDataStatistics pDataStatistics = new DataStatisticsClass();
                pDataStatistics.Cursor = pCursor;
                pDataStatistics.Field = currentFieldName;
                System.Collections.IEnumerator pEnumeratorUniqueValues = pDataStatistics.UniqueValues;//唯一值枚举
                int uniqueValueCount = pDataStatistics.UniqueValueCount;//唯一值的个数

                //table中当前字段有值(不为null)的row的个数,并把信息添加到listView的第一行
                 IQueryFilter pQueryFilter = new QueryFilterClass();
                pQueryFilter.AddField(currentFieldName);
                int valueSum = pTable.RowCount(pQueryFilter);
                this.lsvUniqueValue.Items.Add(currentFieldName);
                this.lsvUniqueValue.Items[0].SubItems.Add(currentFieldName);
                this.lsvUniqueValue.Items[0].SubItems.Add(valueSum.ToString());

                //循环把信息添加到listView里
                int i = 1;//注意！是从1开始,因为第一行已经被占用
                string currentValue = null;//指示当前的值
                //IDataStatistics pUniValueStatistics = new DataStatisticsClass();
                int currentValueCount;
                for (pEnumeratorUniqueValues.Reset(); pEnumeratorUniqueValues.MoveNext(); i++)
                {
                    currentValue = pEnumeratorUniqueValues.Current.ToString();//当前值
                    this.lsvUniqueValue.Items.Add(currentValue);
                    this.lsvUniqueValue.Items[i].SubItems.Add(currentValue);
                    //需要这个if的原因是SQL语句中数字和非数字的写法不一样
                    if (currentTypeIsNumeric)
                    {
                        pQueryFilter.WhereClause = "\"" + currentFieldName + "\"" + " = " + currentValue;
                    }
                    else
                    {
                        pQueryFilter.WhereClause = "\"" + currentFieldName + "\"" + " = " + "'" + currentValue + "'";
                    }
                    currentValueCount = pTable.RowCount(pQueryFilter);//table中该字段是当前值的row的个数
                    this.lsvUniqueValue.Items[i].SubItems.Add(currentValueCount.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Cafe版主帮忙写的正则表达式,用于判断字符串是否可以转化为数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        bool IsNumeric(string str)
        {
            System.Text.RegularExpressions.Regex reg1
                = new System.Text.RegularExpressions.Regex(@"^[-]?\d+[.]?\d*$");
            return reg1.IsMatch(str);
        }

        private ColorRampSelectorFrm newColorRanmpSelectorFrm = null;
        private IColorRamp pUniValueColorRamp = null;
        private IColorRamp pClassBreaksColorRamp = null;

        private void btnUniValueColorRamp_Click(object sender, EventArgs e)
        {
            if (this.newColorRanmpSelectorFrm == null)
            {
                this.newColorRanmpSelectorFrm = new ColorRampSelectorFrm();
            }
            if (this.newColorRanmpSelectorFrm.ShowDialog() == DialogResult.OK)
            {
                this.pUniValueColorRamp = this.newColorRanmpSelectorFrm.pColorRamp;
                this.btnUniValueColorRamp.BackgroundImage = this.newColorRanmpSelectorFrm.pColorRampImage;
            }
        }

        private void cbbUniValueField_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lsvUniqueValue.Items.Clear();
        }

        private void btnUniValueUp_Click(object sender, EventArgs e)
        {
            try
            {
                ListView.SelectedListViewItemCollection pItemCollection = this.lsvUniqueValue.SelectedItems;
                if (pItemCollection.Count == 0)
                {
                    return;
                }
                ListViewItem item = (ListViewItem)pItemCollection[0].Clone();
                int itemIndex = pItemCollection[0].Index;
                if (itemIndex == 0 || itemIndex == 1 || itemIndex == -1)
                {
                    return;
                }
                //this.lsvUniqueValue.Items.Remove(pItemCollection[0]);
                this.lsvUniqueValue.Items.Insert(itemIndex - 1, item);
                this.lsvUniqueValue.Items.Remove(pItemCollection[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUniValueDown_Click(object sender, EventArgs e)
        {
            try
            {
                ListView.SelectedListViewItemCollection pItemCollection = this.lsvUniqueValue.SelectedItems;
                if (pItemCollection.Count == 0)
                {
                    return;
                }
                ListViewItem item = (ListViewItem)pItemCollection[0].Clone();
                int itemIndex = pItemCollection[0].Index;
                if (itemIndex == this.lsvUniqueValue.Items.Count - 1)
                {
                    return;
                }
                this.lsvUniqueValue.Items.Remove(pItemCollection[0]);
                this.lsvUniqueValue.Items.Insert(itemIndex + 1, item);
                //this.lsvUniqueValue.Items.Remove(pItemCollection[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lsvUniqueValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lsvUniqueValue.SelectedIndices.Count != 0)
            {
                int selectedItemIndex = this.lsvUniqueValue.SelectedIndices[0];
                int lsvLastIndex = this.lsvUniqueValue.Items.Count - 1;
                //switch (selectedItemIndex)
                //{
                //    case 0||1:
                //        this.btnUniValueUp.Enabled = false;
                //        break;
                //    case this.lsvUniqueValue.Items.Count-1:
                //        this.btnUniValueDown.Enabled = false;
                //        break;
                //}
                if (selectedItemIndex != 0 && selectedItemIndex != 1 && selectedItemIndex != lsvLastIndex)
                {
                    this.btnUniValueDown.Enabled = true;
                    this.btnUniValueUp.Enabled = true;
                }
                else if (selectedItemIndex == 0)
                {
                    this.btnUniValueUp.Enabled = false;
                    this.btnUniValueDown.Enabled = false;
                }
                else if (selectedItemIndex == 1)
                {
                    this.btnUniValueUp.Enabled = false;
                    this.btnUniValueDown.Enabled = true;
                }
                else if (selectedItemIndex == lsvLastIndex)
                {
                    this.btnUniValueUp.Enabled = true;
                    this.btnUniValueDown.Enabled = false;
                }
            }
        }

        //分级字段ComboBox选择变化
        private void cbbClassBreakField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbbClassBreakField.SelectedIndex == 0)
            {
                this.lblClassBreaksCount.Enabled = false;
                this.cbbClassBreaksCount.Enabled = false;
                this.lblClassBreaksMethod.Enabled = false;
                this.lblClassBreaksMethod.Enabled = false;
                this.groupBox8.Enabled = false;
            }
            else
            {
                this.lblClassBreaksCount.Enabled = true;
                this.cbbClassBreaksCount.Enabled = true;
                this.lblClassBreaksMethod.Enabled = true;
                this.cbbClassBreaksMethod.Enabled = true;
                this.groupBox8.Enabled = true;

                string currentFieldName = this.cbbClassBreakField.Text;//当前字段名
                
                ITable pTable = this.pLayer as ITable;
                ICursor pCursor = pTable.Search(null, true);

                //字段统计
                IDataStatistics pDataStatistics = new DataStatisticsClass();
                pDataStatistics.Cursor = pCursor;
                pDataStatistics.Field = currentFieldName;
                System.Collections.IEnumerator pEnumeratorUniqueValues = pDataStatistics.UniqueValues;//唯一值枚举
                int uniqueValueCount = pDataStatistics.UniqueValueCount;//唯一值的个数

                this.cbbClassBreaksCount.Items.Clear();
                for (int i = 1; i <= uniqueValueCount; i++)
                {
                    this.cbbClassBreaksCount.Items.Add(i.ToString());
                }
                this.cbbClassBreaksCount.SelectedIndex = 0;
                this.cbbClassBreaksMethod.SelectedIndex = 0;
            }
        }

        private double[] classBreaks;

        private void btnClassiFy_Click(object sender, EventArgs e)
        {
            ITable pTable = this.pLayer as ITable;

            object dataValues;
            object dataFrequency;
            
            //以下代码用TableHistogram和BasicHistogram统计出Table某一字段的值和值的频率
            ITableHistogram pTableHistogram;
            pTableHistogram = new BasicTableHistogramClass();
            pTableHistogram.Table = pTable;//需传入一个ITable
            pTableHistogram.Field = this.cbbClassBreakField.Text;//统计的字段
            IBasicHistogram pBasicHistogram;
            pBasicHistogram = pTableHistogram as IBasicHistogram;
            pBasicHistogram.GetHistogram(out dataValues, out dataFrequency);//关键

            //以下代码用IClassifyGEN和EqualInterval对象,基于分段数目,生成各段的临界值,并放在一个数组当中
            IClassifyGEN pClassifyGEN;
            switch (this.cbbClassBreaksMethod.SelectedIndex)
            {
                case 0:
                    pClassifyGEN = new EqualIntervalClass();
                    break;
                case 1:
                    pClassifyGEN = new GeometricalIntervalClass();
                    break;
                case 2:
                    pClassifyGEN = new NaturalBreaksClass();
                    break;
                case 3:
                    pClassifyGEN = new QuantileClass();
                    break;
                default:
                    pClassifyGEN = new EqualIntervalClass();
                    break;
            }
            //double[] classes;
            int classCount = int.Parse(this.cbbClassBreaksCount.Text);
            pClassifyGEN.Classify(dataValues, dataFrequency, ref classCount);//用到了上面ITableHistogram生成的值和值频率数组,关键!!!!!!
            this.classBreaks = pClassifyGEN.ClassBreaks as double[];//注意,此对象下标从1开始(我觉得0为最开头,所以临界值从1开始有意义),关键!!!!!!

            this.lsvClassBreaksSymbol.Items.Clear();
            string currentRange;
            for (int i = 0; i < classCount; i++)
            {
                currentRange = this.classBreaks[i].ToString() + " ～ " + this.classBreaks[i + 1].ToString();
                this.lsvClassBreaksSymbol.Items.Add(currentRange);
                this.lsvClassBreaksSymbol.Items[i].SubItems.Add(currentRange);
            }
        }

        private void btnClassBreaksColorRamp_Click(object sender, EventArgs e)
        {
            if (this.newColorRanmpSelectorFrm == null)
            {
                this.newColorRanmpSelectorFrm = new ColorRampSelectorFrm();
            }
            if (this.newColorRanmpSelectorFrm.ShowDialog() == DialogResult.OK)
            {
                this.pClassBreaksColorRamp = this.newColorRanmpSelectorFrm.pColorRamp;
                this.btnClassBreaksColorRamp.BackgroundImage = this.newColorRanmpSelectorFrm.pColorRampImage;
            }
        }


    }
}