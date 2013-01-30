using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using ESRI.ArcGIS.esriSystem;

namespace ArcGisDemo
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.ArcReader))
            {
                if (!ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop))
                {
                    MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.");
                    return;
                }
            }

            LicenseInitializer aoLicenseInitializer = new LicenseInitializer();
            if (!aoLicenseInitializer.InitializeApplication(new esriLicenseProductCode[] { esriLicenseProductCode.esriLicenseProductCodeEngine, esriLicenseProductCode.esriLicenseProductCodeArcView, esriLicenseProductCode.esriLicenseProductCodeArcEditor, esriLicenseProductCode.esriLicenseProductCodeArcInfo },
            new esriLicenseExtensionCode[] { esriLicenseExtensionCode.esriLicenseExtensionCodeNetwork }))
            {
                System.Windows.Forms.MessageBox.Show("This application could not initialize with the correct ArcGIS license and will shutdown. LicenseMessage: " + aoLicenseInitializer.LicenseMessage());
                aoLicenseInitializer.ShutdownApplication();
                return;
            }
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            aoLicenseInitializer.ShutdownApplication();
        }
    }
}
