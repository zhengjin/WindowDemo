using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Controls;

namespace LinGIS
{
    public partial class SymbolSelectorFrm : DevComponents.DotNetBar.Office2007Form
    {
        private IStyleGalleryItem pStyleGalleryItem;
        private ILegendClass pLegendClass;
        private ILayer pLayer;
        public ISymbol pSymbol;
        public Image pSymbolImage;

        /// <summary>
        /// 构造函数,初始化全局变量
        /// </summary>
        /// <param name="tempLegendClass">TOC图例</param>
        /// <param name="tempLayer">图层</param>
        public SymbolSelectorFrm(ILegendClass tempLegendClass, ILayer tempLayer)
        {
            InitializeComponent();
            this.pLegendClass = tempLegendClass;
            this.pLayer = tempLayer;
        }
       
        /// <summary>
        /// 窗体Load,设好哪些空间可见,哪些不可见,设好按钮的颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SymbolSelectorFrm_Load(object sender, EventArgs e)
        {
            //Get the ArcGIS install location
            string sInstall = ReadRegistry("SOFTWARE\\ESRI\\CoreRuntime");

            //Load the ESRI.ServerStyle file into the SymbologyControl
            this.axSymbologyControl.LoadStyleFile(sInstall + "\\Styles\\ESRI.ServerStyle");
            
            //确定图层的类型(点线面),设置好SymbologyControl的StyleClass,设置好各控件的可见性(visible)
            IGeoFeatureLayer pGeoFeatureLayer = (IGeoFeatureLayer)pLayer;
            switch (((IFeatureLayer)pLayer).FeatureClass.ShapeType)
            {
                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint:
                    this.SetFeatureClassStyle(esriSymbologyStyleClass.esriStyleClassMarkerSymbols);
                    this.lblAngle.Visible = true;
                    this.nudAngle.Visible = true;
                    this.lblSize.Visible = true;
                    this.nudSize.Visible = true;
                    this.lblWidth.Visible = false;
                    this.nudWidth.Visible = false;
                    this.lblOutlineColor.Visible = false;
                    this.btnOutlineColor.Visible = false;
                    break;
                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline:
                    this.SetFeatureClassStyle(esriSymbologyStyleClass.esriStyleClassLineSymbols);
                    this.lblAngle.Visible = false;
                    this.nudAngle.Visible = false;
                    this.lblSize.Visible = false;
                    this.nudSize.Visible = false;
                    this.lblWidth.Visible = true;
                    this.nudWidth.Visible = true;
                    this.lblOutlineColor.Visible = false;
                    this.btnOutlineColor.Visible = false;
                    break;
                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon:
                    this.SetFeatureClassStyle(esriSymbologyStyleClass.esriStyleClassFillSymbols);
                    this.lblAngle.Visible = false;
                    this.nudAngle.Visible = false;
                    this.lblSize.Visible = false;
                    this.nudSize.Visible = false;
                    this.lblWidth.Visible = true;
                    this.nudWidth.Visible = true;
                    this.lblOutlineColor.Visible = true;
                    this.btnOutlineColor.Visible = true;
                    break;
                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryMultiPatch:
                    this.SetFeatureClassStyle(esriSymbologyStyleClass.esriStyleClassFillSymbols);
                    this.lblAngle.Visible = false;
                    this.nudAngle.Visible = false;
                    this.lblSize.Visible = false;
                    this.nudSize.Visible = false;
                    this.lblWidth.Visible = true;
                    this.nudWidth.Visible = true;
                    this.lblOutlineColor.Visible = true;
                    this.btnOutlineColor.Visible = true;
                    break;
                default:
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// 设置好SymbologyControl的StyleClass,如果有图例,把当前的TOC图例的符号添加到当前SymbologyStyleClass中去,并让之处于选中状态
        /// </summary>
        /// <param name="symbologyStyleClass"></param>
        private void SetFeatureClassStyle(esriSymbologyStyleClass symbologyStyleClass)
        {
            this.axSymbologyControl.StyleClass = symbologyStyleClass;
            ISymbologyStyleClass pSymbologyStyleClass = this.axSymbologyControl.GetStyleClass(symbologyStyleClass);
            if (this.pLegendClass != null)
            {
                IStyleGalleryItem currentStyleGalleryItem = new ServerStyleGalleryItem();
                currentStyleGalleryItem.Name = "当前符号";
                currentStyleGalleryItem.Item = pLegendClass.Symbol;
                pSymbologyStyleClass.AddItem(currentStyleGalleryItem, 0);
                this.pStyleGalleryItem = currentStyleGalleryItem;
            }
            pSymbologyStyleClass.SelectItem(0);
        }

        /// <summary>
        /// 读取注册表中的制定软件的路径
        /// </summary>
        /// <param name="sKey"></param>
        /// <returns></returns>
        private string ReadRegistry(string sKey)
        {
            //Open the subkey for reading
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(sKey, true);
            if (rk == null) return "";
            // Get the data from a specified item in the key.
            return (string)rk.GetValue("InstallDir");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void axSymbologyControl_OnItemSelected(object sender, ISymbologyControlEvents_OnItemSelectedEvent e)
        {
            pStyleGalleryItem = (IStyleGalleryItem)e.styleGalleryItem;
            Color color;
            switch (this.axSymbologyControl.StyleClass)
            {
                case esriSymbologyStyleClass.esriStyleClassMarkerSymbols:
                    color = this.ConvertIRgbColorToColor(((IMarkerSymbol)pStyleGalleryItem.Item).Color as IRgbColor);
                    break;
                case esriSymbologyStyleClass.esriStyleClassLineSymbols:
                    color = this.ConvertIRgbColorToColor(((ILineSymbol)pStyleGalleryItem.Item).Color as IRgbColor);
                    break;
                case esriSymbologyStyleClass.esriStyleClassFillSymbols:
                    color=this.ConvertIRgbColorToColor(((IFillSymbol)pStyleGalleryItem.Item).Color as IRgbColor);
                    this.btnOutlineColor.BackColor = this.ConvertIRgbColorToColor(((IFillSymbol)pStyleGalleryItem.Item).Outline.Color as IRgbColor);
                    break;
                default:
                    color = Color.Black;
                    break;
            }
            this.btnColor.BackColor = color;
            this.PreviewImage();
        }
        /// <summary>
        /// 把选中并设置好的符号在picturebox中预览
        /// </summary>
        private void PreviewImage()
        {
            stdole.IPictureDisp picture = this.axSymbologyControl.GetStyleClass(this.axSymbologyControl.StyleClass).PreviewItem(pStyleGalleryItem, this.ptbPreview.Width, this.ptbPreview.Height);
            System.Drawing.Image image = System.Drawing.Image.FromHbitmap(new System.IntPtr(picture.Handle));
            this.ptbPreview.Image = image;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //pLegendClass.Symbol = (ISymbol)pStyleGalleryItem.Item;
            this.pSymbol = (ISymbol)pStyleGalleryItem.Item;
            this.pSymbolImage = this.ptbPreview.Image;
            this.Close();
        }

        private void nudSize_ValueChanged(object sender, EventArgs e)
        {
            ((IMarkerSymbol)this.pStyleGalleryItem.Item).Size = (double)this.nudSize.Value;
            this.PreviewImage();
        }

        private void nudAngle_ValueChanged(object sender, EventArgs e)
        {
            ((IMarkerSymbol)this.pStyleGalleryItem.Item).Angle = (double)this.nudAngle.Value;
            this.PreviewImage();
        }

        /// <summary>
        /// 将ArcGIS Engine中的IRgbColor接口转换至.NET中的Color结构
        /// </summary>
        /// <param name="pRgbColor">IRgbColor</param>
        /// <returns>.NET中的System.Drawing.Color结构表示ARGB颜色</returns>
        public Color ConvertIRgbColorToColor(IRgbColor pRgbColor)
        {
            return ColorTranslator.FromOle(pRgbColor.RGB);
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
        public IRgbColor ConvertColorToIRgbColor(Color color)
        {
            IRgbColor pRgbColor = new RgbColorClass();
            pRgbColor.RGB = color.B * 65536 + color.G * 256 + color.R;
            return pRgbColor;
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (this.colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.btnColor.BackColor = this.colorDialog.Color;
                switch (this.axSymbologyControl.StyleClass)
                {
                    case esriSymbologyStyleClass.esriStyleClassMarkerSymbols:
                        ((IMarkerSymbol)this.pStyleGalleryItem.Item).Color = this.ConvertColorToIColor(this.colorDialog.Color);
                        break;
                    case esriSymbologyStyleClass.esriStyleClassLineSymbols:
                        ((ILineSymbol)this.pStyleGalleryItem.Item).Color = this.ConvertColorToIColor(this.colorDialog.Color);
                        break;
                    case esriSymbologyStyleClass.esriStyleClassFillSymbols:
                        ((IFillSymbol)this.pStyleGalleryItem.Item).Color=this.ConvertColorToIColor(this.colorDialog.Color);
                        break;
                }
                this.PreviewImage();
            }
        }

        private void axSymbologyControl_OnDoubleClick(object sender, ISymbologyControlEvents_OnDoubleClickEvent e)
        {
            this.btnOK.PerformClick();
        }

        private void axSymbologyControl_OnStyleClassChanged(object sender, ISymbologyControlEvents_OnStyleClassChangedEvent e)
        {
            switch ((esriSymbologyStyleClass)(e.symbologyStyleClass))
            {
                case esriSymbologyStyleClass.esriStyleClassMarkerSymbols:
                    this.lblAngle.Visible = true;
                    this.nudAngle.Visible = true;
                    this.lblSize.Visible = true;
                    this.nudSize.Visible = true;
                    this.lblWidth.Visible = false;
                    this.nudWidth.Visible = false;
                    this.lblOutlineColor.Visible = false;
                    this.btnOutlineColor.Visible = false;
                    break;
                case esriSymbologyStyleClass.esriStyleClassLineSymbols:
                    this.lblAngle.Visible = false;
                    this.nudAngle.Visible = false;
                    this.lblSize.Visible = false;
                    this.nudSize.Visible = false;
                    this.lblWidth.Visible = true;
                    this.nudWidth.Visible = true;
                    this.lblOutlineColor.Visible = false;
                    this.btnOutlineColor.Visible = false;
                    break;
                case esriSymbologyStyleClass.esriStyleClassFillSymbols:
                    this.lblAngle.Visible = false;
                    this.nudAngle.Visible = false;
                    this.lblSize.Visible = false;
                    this.nudSize.Visible = false;
                    this.lblWidth.Visible = true;
                    this.nudWidth.Visible = true;
                    this.lblOutlineColor.Visible = true;
                    this.btnOutlineColor.Visible = true;
                    break;
            }
        }

        private void nudWidth_ValueChanged(object sender, EventArgs e)
        {
            switch (this.axSymbologyControl.StyleClass)
            {
                case esriSymbologyStyleClass.esriStyleClassLineSymbols:
                    ((ILineSymbol)this.pStyleGalleryItem.Item).Width = Convert.ToDouble(this.nudWidth.Value);
                    break;
                case esriSymbologyStyleClass.esriStyleClassFillSymbols:
                    ILineSymbol pLineSymbol = ((IFillSymbol)this.pStyleGalleryItem.Item).Outline;
                    pLineSymbol.Width = Convert.ToDouble(this.nudWidth.Value);
                    ((IFillSymbol)this.pStyleGalleryItem.Item).Outline = pLineSymbol;
                    break;
            }
            this.PreviewImage();
        }

        private void btnOutlineColor_Click(object sender, EventArgs e)
        {
            if (this.colorDialog.ShowDialog() == DialogResult.OK)
            {
                ILineSymbol pLineSymbol = ((IFillSymbol)this.pStyleGalleryItem.Item).Outline;
                pLineSymbol.Color = this.ConvertColorToIColor(this.colorDialog.Color);
                ((IFillSymbol)this.pStyleGalleryItem.Item).Outline = pLineSymbol;
                this.btnOutlineColor.BackColor = this.colorDialog.Color;
                this.PreviewImage();
            }
        }

        bool contextMenuMoreSymbolInitiated = false;
        private void btnMoreSymbols_Click(object sender, EventArgs e)
        {
            if (this.contextMenuMoreSymbolInitiated == false)
            {
                string sInstall = ReadRegistry("SOFTWARE\\ESRI\\CoreRuntime");
                string path = System.IO.Path.Combine(sInstall, "Styles");
                string[] styleNames = System.IO.Directory.GetFiles(path, "*.ServerStyle");
                ToolStripMenuItem[] symbolContextMenuItem = new ToolStripMenuItem[styleNames.Length + 1];
                for (int i = 0; i < styleNames.Length; i++)
                {
                    symbolContextMenuItem[i] = new ToolStripMenuItem();
                    symbolContextMenuItem[i].CheckOnClick = true;
                    symbolContextMenuItem[i].Text = System.IO.Path.GetFileNameWithoutExtension(styleNames[i]);
                    if (symbolContextMenuItem[i].Text == "ESRI")
                    {
                        symbolContextMenuItem[i].Checked = true;
                    }
                    symbolContextMenuItem[i].Name = styleNames[i];
                    symbolContextMenuItem[i].Click += new EventHandler(symbolContextMenuItem_Click);
                }
                symbolContextMenuItem[styleNames.Length] = new ToolStripMenuItem();
                symbolContextMenuItem[styleNames.Length].Text = "更多符号";
                symbolContextMenuItem[styleNames.Length].Click += new EventHandler(symbolContextMenuItemMoreSymbols_Click);
                this.contextMenuStripMoreSymbol.Items.AddRange(symbolContextMenuItem);
                this.contextMenuMoreSymbolInitiated = true;
            }
            this.contextMenuStripMoreSymbol.Show(this.btnMoreSymbols.Location);
        }

        void symbolContextMenuItemMoreSymbols_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.axSymbologyControl.LoadStyleFile(this.openFileDialog.FileName);
                this.axSymbologyControl.Refresh();
            }
        }

        private void symbolContextMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem pToolStripMenuItem = ((ToolStripMenuItem)sender);
            //Load the style file into the SymbologyControl
            if (pToolStripMenuItem.Checked == true)
            {
                this.axSymbologyControl.LoadStyleFile(pToolStripMenuItem.Name);
                this.axSymbologyControl.Refresh();
            }
            else
            {
                this.axSymbologyControl.RemoveFile(pToolStripMenuItem.Name);
                this.axSymbologyControl.Refresh();
            }
        }
    }
}