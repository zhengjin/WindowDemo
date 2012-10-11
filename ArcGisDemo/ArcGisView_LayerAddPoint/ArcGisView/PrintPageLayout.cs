using System;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Output;

namespace ArcGisView
{
    public partial class PrintPageLayout : Form
    {
        private string sFilePath = "";
        public PrintPageLayout(string iPath)
        {
            InitializeComponent();
            sFilePath = iPath;
        }
        private void loadmap()
        {
            //Validate and load the Mx document
            if (axPageLayoutControl1.CheckMxFile(sFilePath) == true)
            {
                axPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerHourglass;
                axPageLayoutControl1.LoadMxFile(sFilePath, "");
                axPageLayoutControl1.Extent = axPageLayoutControl1.FullExtent;
                axPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
            }
            else
            {
                MessageBox.Show(sFilePath + " is not a valid ArcMap document");
            }
            //Update page display
            cboPageSize.SelectedIndex = (int)axPageLayoutControl1.Page.FormID;
            cboPageToPrinterMapping.SelectedIndex = (int)axPageLayoutControl1.Page.PageToPrinterMapping;
            if (axPageLayoutControl1.Page.Orientation == 1)
            {
                optPortrait.Checked = true;
            }
            else
            {
                optLandscape.Checked = true;
            }

            //Zoom to whole page
            axPageLayoutControl1.ZoomToWholePage();

            //Update printer page display
            UpdatePrintPageDisplay();
        }

        private void cmdPrint_Click(object sender, System.EventArgs e)
        {
            if (axPageLayoutControl1.Printer != null)
            {
                //Set mouse pointer
                axPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerHourglass;

                //Get IPrinter interface through the PageLayoutControl's printer
                IPrinter printer = axPageLayoutControl1.Printer;

                //Determine whether printer paper's orientation needs changing
                if (printer.Paper.Orientation != axPageLayoutControl1.Page.Orientation)
                {
                    printer.Paper.Orientation = axPageLayoutControl1.Page.Orientation;
                    //Update the display
                    UpdatePrintingDisplay();
                }

                //Print the page range with the specified overlap
                axPageLayoutControl1.PrintPageLayout(Convert.ToInt16(txbStartPage.Text), Convert.ToInt16(txbEndPage.Text), Convert.ToDouble(txbOverlap.Text));

                //Set the mouse pointer
                axPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
            }
        }

        private void UpdatePrintPageDisplay()
        {
            //Determine the number of pages
            short iPageCount = axPageLayoutControl1.get_PrinterPageCount(Convert.ToDouble(txbOverlap.Text));
            lblPageCount.Text = iPageCount.ToString();

            //Validate start and end pages
            int iPageStart = Convert.ToInt32(txbStartPage.Text);
            int iPageEnd = Convert.ToInt32(txbEndPage.Text);
            if ((iPageStart < 1) | (iPageStart > iPageCount))
            {
                txbStartPage.Text = "1";
            }
            if ((iPageEnd < 1) | (iPageEnd > iPageCount))
            {
                txbEndPage.Text = iPageCount.ToString();
            }
        }

        private void UpdatePrintingDisplay()
        {
            if (axPageLayoutControl1.Printer != null)
            {
                //Get IPrinter interface through the PageLayoutControl's printer
                IPrinter printer = axPageLayoutControl1.Printer;

                //Determine the orientation of the printer's paper
                if (printer.Paper.Orientation == 1)
                {
                    lblPrinterOrientation.Text = "Portrait";
                }
                else
                {
                    lblPrinterOrientation.Text = "Landscape";
                }

                //Determine the printer name
                lblPrinterName.Text = printer.Paper.PrinterName;

                //Determine the printer's paper size
                double dWidth;
                double dheight;
                printer.Paper.QueryPaperSize(out dWidth, out dheight);
                lblPrinterSize.Text = dWidth.ToString("###.000") + " by " + dheight.ToString("###.000") + " Inches";
            }
        }

        private void txbOverlap_Leave(object sender, System.EventArgs e)
        {
            //Update printer page display
            UpdatePrintPageDisplay();
        }

        private void cboPageToPrinterMapping_Click(object sender, System.EventArgs e)
        {
            //Set the printer to page mapping
            axPageLayoutControl1.Page.PageToPrinterMapping = (esriPageToPrinterMapping)cboPageToPrinterMapping.SelectedIndex;
            //Update printer page display
            UpdatePrintPageDisplay();
        }

        private void optLandscape_Click(object sender, System.EventArgs e)
        {
            if (optLandscape.Checked == true)
            {
                //Set the page orientation
                if (axPageLayoutControl1.Page.FormID != esriPageFormID.esriPageFormSameAsPrinter)
                {
                    axPageLayoutControl1.Page.Orientation = 2;
                }
                //Update printer page display
                UpdatePrintPageDisplay();
            }
        }

        private void optPortrait_Click(object sender, System.EventArgs e)
        {
            if (optPortrait.Checked == true)
            {
                //Set the page orientation
                if (axPageLayoutControl1.Page.FormID != esriPageFormID.esriPageFormSameAsPrinter)
                {
                    axPageLayoutControl1.Page.Orientation = 1;
                }
                //Update printer page display
                UpdatePrintPageDisplay();
            }
        }

        private void cboPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Orientation cannot change if the page size is set to 'Same as Printer'
            if (cboPageSize.SelectedIndex == 13)
                EnableOrientation(false);
            else
                EnableOrientation(true);
            //Set the page size
            axPageLayoutControl1.Page.FormID = (esriPageFormID)cboPageSize.SelectedIndex;
            //Update printer page display
            UpdatePrintPageDisplay();
        }

        private void EnableOrientation(bool b)
        {
            optPortrait.Enabled = b;
            optLandscape.Enabled = b;
        }

        private void PrintPageLayout_Load(object sender, EventArgs e)
        {
            
            //Add esriPageFormID constants to drop down
            cboPageSize.Items.Add("Letter - 8.5in x 11in.");
            cboPageSize.Items.Add("Legal - 8.5in x 14in.");
            cboPageSize.Items.Add("Tabloid - 11in x 17in.");
            cboPageSize.Items.Add("C - 17in x 22in.");
            cboPageSize.Items.Add("D - 22in x 34in.");
            cboPageSize.Items.Add("E - 34in x 44in.");
            cboPageSize.Items.Add("A5 - 148mm x 210mm.");
            cboPageSize.Items.Add("A4 - 210mm x 297mm.");
            cboPageSize.Items.Add("A3 - 297mm x 420mm.");
            cboPageSize.Items.Add("A2 - 420mm x 594mm.");
            cboPageSize.Items.Add("A1 - 594mm x 841mm.");
            cboPageSize.Items.Add("A0 - 841mm x 1189mm.");
            cboPageSize.Items.Add("Custom Page Size.");
            cboPageSize.Items.Add("Same as Printer Form.");
            cboPageSize.SelectedIndex = 7;

            //Add esriPageToPrinterMapping constants to drop down
            cboPageToPrinterMapping.Items.Add("0: Crop");
            cboPageToPrinterMapping.Items.Add("1: Scale");
            cboPageToPrinterMapping.Items.Add("2: Tile");
            cboPageToPrinterMapping.SelectedIndex = 1;
            optPortrait.Checked = true;
            EnableOrientation(false);

            //Display printer details
            UpdatePrintingDisplay();
            loadmap();
        }
    }
}
