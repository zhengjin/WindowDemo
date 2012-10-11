using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;

namespace LinGIS
{
    public partial class ColorRampSelectorFrm : DevComponents.DotNetBar.Office2007Form
    {
        public ColorRampSelectorFrm()
        {
            InitializeComponent();
            
            //Get the ArcGIS install location
            string sInstall = ReadRegistry("SOFTWARE\\ESRI\\CoreRuntime");

            //Load the ESRI.ServerStyle file into the SymbologyControl
            this.axSymbologyControl.LoadStyleFile(sInstall + "\\Styles\\ESRI.ServerStyle");

            this.axSymbologyControl.GetStyleClass(esriSymbologyStyleClass.esriStyleClassColorRamps).SelectItem(0);
        }
        public IColorRamp pColorRamp;
        public Image pColorRampImage;
        private IStyleGalleryItem pStyleGalleryItem;

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

        private void ColorRampSelectorFrm_Load(object sender, EventArgs e)
        {
            ////Get the ArcGIS install location
            //string sInstall = ReadRegistry("SOFTWARE\\ESRI\\CoreRuntime");

            ////Load the ESRI.ServerStyle file into the SymbologyControl
            //this.axSymbologyControl.LoadStyleFile(sInstall + "\\Styles\\ESRI.ServerStyle");

            //this.axSymbologyControl.GetStyleClass(esriSymbologyStyleClass.esriStyleClassColorRamps).SelectItem(0);
        }

        private void axSymbologyControl_OnItemSelected(object sender, ISymbologyControlEvents_OnItemSelectedEvent e)
        {
            this.pStyleGalleryItem = e.styleGalleryItem as IStyleGalleryItem;
            this.PreviewImage();
        }

        /// <summary>
        /// 把选中并设置好的符号在picturebox中预览
        /// </summary>
        private void PreviewImage()
        {
            stdole.IPictureDisp picture = this.axSymbologyControl.GetStyleClass(this.axSymbologyControl.StyleClass).PreviewItem(this.pStyleGalleryItem, this.ptbPreview.Width, this.ptbPreview.Height);
            System.Drawing.Image image = System.Drawing.Image.FromHbitmap(new System.IntPtr(picture.Handle));
            this.ptbPreview.Image = image;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.pColorRamp = (IColorRamp)this.pStyleGalleryItem.Item;
            this.pColorRampImage = this.ptbPreview.Image;
        }

        private void axSymbologyControl_OnDoubleClick(object sender, ISymbologyControlEvents_OnDoubleClickEvent e)
        {
            this.btnOK.PerformClick();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}