using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using System.Windows.Forms;

namespace LinGIS
{
    /// <summary>
    /// Summary description for NewPolyline.
    /// </summary>
    [Guid("e2d9b6aa-ac14-485d-b7ed-7c6229a43305")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("DisplayFeedBack.NewPolyline")]
    public sealed class NewPolyline : BaseTool
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Register(regKey);
            ControlsCommands.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);
            ControlsCommands.Unregister(regKey);
        }

        #endregion
        #endregion

        private IHookHelper m_hookHelper = null;

        private INewLineFeedback pNewLineFeedback;
        private IScreenDisplay pScreenDisplay;

        public NewPolyline()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = ""; //localizable text 
            base.m_caption = "";  //localizable text 
            base.m_message = "This should work in ArcMap/MapControl/PageLayoutControl";  //localizable text
            base.m_toolTip = "";  //localizable text
            base.m_name = "";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
            try
            {
                //
                // TODO: change resource name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
                base.m_cursor = new System.Windows.Forms.Cursor(GetType(), GetType().Name + ".cur");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overriden Class Methods

        /// <summary>
        /// Occurs when this tool is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            try
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = hook;
                if (m_hookHelper.ActiveView == null)
                {
                    m_hookHelper = null;
                }
            }
            catch
            {
                m_hookHelper = null;
            }

            if (m_hookHelper == null)
                base.m_enabled = false;
            else
            {
                base.m_enabled = true;
                this.pScreenDisplay = this.m_hookHelper.ActiveView.ScreenDisplay;
            }

            // TODO:  Add other initialization code
        }

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add NewPolyline.OnClick implementation
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add NewPolyline.OnMouseDown implementation
            base.OnMouseDown(Button, Shift, X, Y);
            if (Button == 1)
            {
                IPoint pPoint = this.pScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                if (this.pNewLineFeedback == null)
                {
                    this.pNewLineFeedback = new NewLineFeedback();
                    this.pNewLineFeedback.Display = this.pScreenDisplay;
                    this.pNewLineFeedback.Start(pPoint);
                }
                else
                {
                    this.pNewLineFeedback.AddPoint(pPoint);
                }
            }
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add NewPolyline.OnMouseMove implementation
            if (this.pNewLineFeedback != null)
            {
                IPoint pPoint = this.pScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                this.pNewLineFeedback.MoveTo(pPoint);
            }
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add NewPolyline.OnMouseUp implementation
        }

        public override void Refresh(int hDC)
        {
            base.Refresh(hDC);
            if (this.pNewLineFeedback != null)
            {
                this.pNewLineFeedback.Refresh(hDC);
            }
        }

        public override void OnDblClick()
        {
            base.OnDblClick();
            if (this.pNewLineFeedback != null)
            {
                IPolyline pPolyline = this.pNewLineFeedback.Stop();
                IElement pElement = new LineElementClass();
                pElement.Geometry = pPolyline;
                ((IGraphicsContainer)this.m_hookHelper.FocusMap).AddElement(pElement, 0);
                this.m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            }
        }

        #endregion
    }
}
