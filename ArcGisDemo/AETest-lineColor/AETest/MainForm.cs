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
using ESRI.ArcGIS.Geometry;

namespace AETest
{
    public sealed partial class MainForm : Form
    {
        #region class private members
        private IMapControl3 m_mapControl = null;
        private string m_mapDocumentName = string.Empty;

        IMap map;
        IActiveView activeview;
        IScreenDisplay pScreenDisplay;
        IGraphicsContainer pGraphicsContainer;
        IGraphicsContainerSelect pGraphconSel;
        #endregion

        #region class constructor
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        private IColor GetRGB(int r, int g, int b)
        {
            IRgbColor color = new RgbColorClass();
            color.Red = r;
            color.Green = g;
            color.Blue = b;
            return (color as IColor);
        }

        private IColor GetRGB(Color pColor)
        {
            IRgbColor color = new RgbColorClass();
            color.Red = pColor.R;
            color.Green = pColor.G;
            color.Blue = pColor.B;
            return (color as IColor);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.ArcReader))
            {
                if (!ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop))
                {
                    MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.");
                    return;
                }
            }
            //get the MapControl
            m_mapControl = (IMapControl3)axMapControl1.Object;

            //disable the Save menu (since there is no document yet)
            menuSaveDoc.Enabled = false;

            activeview = axMapControl1.Map as IActiveView;
            map = activeview.FocusMap;
            pScreenDisplay = activeview.ScreenDisplay;
            pGraphicsContainer = map as IGraphicsContainer;
            IGraphicsContainerSelect pGraphconSel = map as IGraphicsContainerSelect;
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
                    MessageBox.Show("Map document is read only!");
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
        private void axMapControl1_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
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

        private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            statusBarXY.Text = string.Format("{0}, {1}  {2}", e.mapX.ToString("#######.##"), e.mapY.ToString("#######.##"), axMapControl1.MapUnits.ToString().Substring(4));
        }

        private void styleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IStyleGallery pStyleGlry = new ServerStyleGalleryClass();
            IStyleGalleryStorage pStyleStorage = pStyleGlry as IStyleGalleryStorage;

            IRgbColor pRgbColor = new RgbColorClass();
            pRgbColor.Red = 255;
            pRgbColor.Green = 1;
            pRgbColor.Blue = 2;

            IStyleGalleryItem pStyleItem = new ServerStyleGalleryItemClass();
            pStyleItem.Name = "Red";
            pStyleItem.Category = "Default";
            pStyleItem.Item = pRgbColor;

            string pOldFile = pStyleStorage.TargetFile;
            pStyleStorage.TargetFile = @"E:\test.style";
            pStyleGlry.AddItem(pStyleItem);
            pStyleStorage.TargetFile = pOldFile;
            IColor color = pRgbColor as IColor;
            this.Text = color.RGB.ToString();
        }

        private void algorithmicColorRampToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IMap mmap = axMapControl1.Map;
            IActiveView pActiveView = mmap as IActiveView;

            IGeometry pLine = axMapControl1.TrackLine();

            ISimpleLineSymbol pLineSymbol = new SimpleLineSymbolClass();

            IEnumColors pEnumColors = CreateAlgColorRamp(GetRGB(0, 255, 0), GetRGB(255, 0, 255), 20);
            IColor pColor = pEnumColors.Next();

            pLineSymbol.Color = pColor;
            pLineSymbol.Width = 2;
            pLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;

            IElement element = new LineElementClass();
            element.Geometry = pLine;
            ILineElement pLineElement = element as ILineElement;
            pLineElement.Symbol = pLineSymbol;

            IGraphicsContainer ppGraphicsContainer = mmap as IGraphicsContainer;
            ppGraphicsContainer.AddElement(pLineElement as IElement, 0);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        private IEnumColors CreateAlgColorRamp(IColor fromColor, IColor toColor, int count)
        {
            IAlgorithmicColorRamp pRampColor = new AlgorithmicColorRampClass();
            pRampColor.FromColor = fromColor;
            pRampColor.ToColor = toColor;
            pRampColor.Size = count;
            bool b = true;
            pRampColor.CreateRamp(out b);

            return pRampColor.Colors;
        }

        private void iRandomColorRampToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IMap mmap = axMapControl1.Map;
            IActiveView pActiveView = mmap as IActiveView;

            IGeometry pLine = axMapControl1.TrackLine();

            ISimpleLineSymbol pLineSymbol = new SimpleLineSymbolClass();

            IEnumColors pEnumColors = CreateRdmColorRamp(140, 220, 35, 100, 32, 80, 12, 7);
            IColor pColor = pEnumColors.Next();

            pLineSymbol.Color = pColor;
            pLineSymbol.Width = 2;
            pLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;

            IElement element = new LineElementClass();
            element.Geometry = pLine;
            ILineElement pLineElement = element as ILineElement;
            pLineElement.Symbol = pLineSymbol;

            IGraphicsContainer ppGraphicsContainer = mmap as IGraphicsContainer;
            ppGraphicsContainer.AddElement(pLineElement as IElement, 0);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        private IEnumColors CreateRdmColorRamp(int startHue, int endHue, int minValue, int maxValue, int minSaturation,
            int maxSaturation, int size, int seed)
        {
            IRandomColorRamp pRdmcolor = new RandomColorRampClass();
            pRdmcolor.StartHue = startHue;
            pRdmcolor.EndHue = endHue;
            pRdmcolor.MinValue = minValue;
            pRdmcolor.MaxValue = maxValue;
            pRdmcolor.MinSaturation = minSaturation;
            pRdmcolor.MaxSaturation = maxSaturation;
            pRdmcolor.Size = size;
            pRdmcolor.Seed = seed;
            bool b;
            pRdmcolor.CreateRamp(out b);

            return pRdmcolor.Colors;
        }

        private void convertColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IColor pColor = GetRGB(Color.Black);
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                pColor = GetRGB(colorDlg.Color);
            }

            IMap mmap = axMapControl1.Map;
            IActiveView pActiveView = mmap as IActiveView;

            IGeometry pLine = axMapControl1.TrackLine();

            ISimpleLineSymbol pLineSymbol = new SimpleLineSymbolClass();
            pLineSymbol.Color = pColor;
            pLineSymbol.Width = 2;
            pLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;

            IElement element = new LineElementClass();
            element.Geometry = pLine;
            ILineElement pLineElement = element as ILineElement;
            pLineElement.Symbol = pLineSymbol;

            IGraphicsContainer ppGraphicsContainer = mmap as IGraphicsContainer;
            ppGraphicsContainer.AddElement(pLineElement as IElement, 0);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        private void gotoXYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GotoXY form = new GotoXY(axMapControl1);
            form.Show();
        }
    }
}