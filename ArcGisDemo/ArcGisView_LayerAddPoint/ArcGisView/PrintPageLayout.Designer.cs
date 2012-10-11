namespace ArcGisView
{
    partial class PrintPageLayout
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintPageLayout));
            this.axPageLayoutControl1 = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
            this.fraPrint = new System.Windows.Forms.GroupBox();
            this.txbOverlap = new System.Windows.Forms.TextBox();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.txbStartPage = new System.Windows.Forms.TextBox();
            this.txbEndPage = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.fraPrinter = new System.Windows.Forms.GroupBox();
            this.lblPrinterOrientation = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.lblPrinterName = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.lblPrinterSize = new System.Windows.Forms.Label();
            this.lblPdcdcrinter = new System.Windows.Forms.Label();
            this.Frame2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.optLandscape = new System.Windows.Forms.RadioButton();
            this.optPortrait = new System.Windows.Forms.RadioButton();
            this.cboPageToPrinterMapping = new System.Windows.Forms.ComboBox();
            this.cboPageSize = new System.Windows.Forms.ComboBox();
            this.lblPageCount = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).BeginInit();
            this.fraPrint.SuspendLayout();
            this.fraPrinter.SuspendLayout();
            this.Frame2.SuspendLayout();
            this.SuspendLayout();
            // 
            // axPageLayoutControl1
            // 
            this.axPageLayoutControl1.Location = new System.Drawing.Point(12, 12);
            this.axPageLayoutControl1.Name = "axPageLayoutControl1";
            this.axPageLayoutControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutControl1.OcxState")));
            this.axPageLayoutControl1.Size = new System.Drawing.Size(396, 505);
            this.axPageLayoutControl1.TabIndex = 26;
            // 
            // fraPrint
            // 
            this.fraPrint.BackColor = System.Drawing.SystemColors.Control;
            this.fraPrint.Controls.Add(this.txbOverlap);
            this.fraPrint.Controls.Add(this.cmdPrint);
            this.fraPrint.Controls.Add(this.txbStartPage);
            this.fraPrint.Controls.Add(this.txbEndPage);
            this.fraPrint.Controls.Add(this.Label5);
            this.fraPrint.Controls.Add(this.Label1);
            this.fraPrint.Controls.Add(this.Label2);
            this.fraPrint.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fraPrint.ForeColor = System.Drawing.SystemColors.ControlText;
            this.fraPrint.Location = new System.Drawing.Point(414, 402);
            this.fraPrint.Name = "fraPrint";
            this.fraPrint.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fraPrint.Size = new System.Drawing.Size(273, 115);
            this.fraPrint.TabIndex = 25;
            this.fraPrint.TabStop = false;
            this.fraPrint.Text = "打印";
            // 
            // txbOverlap
            // 
            this.txbOverlap.AcceptsReturn = true;
            this.txbOverlap.BackColor = System.Drawing.SystemColors.Window;
            this.txbOverlap.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txbOverlap.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbOverlap.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txbOverlap.Location = new System.Drawing.Point(192, 26);
            this.txbOverlap.MaxLength = 0;
            this.txbOverlap.Name = "txbOverlap";
            this.txbOverlap.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txbOverlap.Size = new System.Drawing.Size(68, 20);
            this.txbOverlap.TabIndex = 9;
            this.txbOverlap.Text = "0";
            this.txbOverlap.Leave += new System.EventHandler(this.txbOverlap_Leave);
            // 
            // cmdPrint
            // 
            this.cmdPrint.BackColor = System.Drawing.SystemColors.Control;
            this.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdPrint.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdPrint.Location = new System.Drawing.Point(10, 77);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdPrint.Size = new System.Drawing.Size(250, 28);
            this.cmdPrint.TabIndex = 8;
            this.cmdPrint.Text = "打印";
            this.cmdPrint.UseVisualStyleBackColor = false;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // txbStartPage
            // 
            this.txbStartPage.AcceptsReturn = true;
            this.txbStartPage.BackColor = System.Drawing.SystemColors.Window;
            this.txbStartPage.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txbStartPage.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbStartPage.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txbStartPage.Location = new System.Drawing.Point(71, 51);
            this.txbStartPage.MaxLength = 0;
            this.txbStartPage.Name = "txbStartPage";
            this.txbStartPage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txbStartPage.Size = new System.Drawing.Size(58, 20);
            this.txbStartPage.TabIndex = 7;
            this.txbStartPage.Text = "1";
            // 
            // txbEndPage
            // 
            this.txbEndPage.AcceptsReturn = true;
            this.txbEndPage.BackColor = System.Drawing.SystemColors.Window;
            this.txbEndPage.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txbEndPage.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbEndPage.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txbEndPage.Location = new System.Drawing.Point(192, 51);
            this.txbEndPage.MaxLength = 0;
            this.txbEndPage.Name = "txbEndPage";
            this.txbEndPage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txbEndPage.Size = new System.Drawing.Size(68, 20);
            this.txbEndPage.TabIndex = 6;
            this.txbEndPage.Text = "0";
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.SystemColors.Control;
            this.Label5.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label5.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label5.Location = new System.Drawing.Point(10, 51);
            this.Label5.Name = "Label5";
            this.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label5.Size = new System.Drawing.Size(61, 19);
            this.Label5.TabIndex = 12;
            this.Label5.Text = "页数";
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.SystemColors.Control;
            this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label1.Location = new System.Drawing.Point(141, 51);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label1.Size = new System.Drawing.Size(30, 19);
            this.Label1.TabIndex = 11;
            this.Label1.Text = "到";
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.SystemColors.Control;
            this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label2.Location = new System.Drawing.Point(10, 26);
            this.Label2.Name = "Label2";
            this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label2.Size = new System.Drawing.Size(222, 36);
            this.Label2.TabIndex = 10;
            this.Label2.Text = "打印份数";
            // 
            // fraPrinter
            // 
            this.fraPrinter.BackColor = System.Drawing.SystemColors.Control;
            this.fraPrinter.Controls.Add(this.lblPrinterOrientation);
            this.fraPrinter.Controls.Add(this.Label10);
            this.fraPrinter.Controls.Add(this.lblPrinterName);
            this.fraPrinter.Controls.Add(this.Label7);
            this.fraPrinter.Controls.Add(this.lblPrinterSize);
            this.fraPrinter.Controls.Add(this.lblPdcdcrinter);
            this.fraPrinter.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fraPrinter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.fraPrinter.Location = new System.Drawing.Point(415, 298);
            this.fraPrinter.Name = "fraPrinter";
            this.fraPrinter.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fraPrinter.Size = new System.Drawing.Size(273, 100);
            this.fraPrinter.TabIndex = 24;
            this.fraPrinter.TabStop = false;
            this.fraPrinter.Text = "打印属性";
            // 
            // lblPrinterOrientation
            // 
            this.lblPrinterOrientation.BackColor = System.Drawing.SystemColors.Control;
            this.lblPrinterOrientation.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblPrinterOrientation.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrinterOrientation.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPrinterOrientation.Location = new System.Drawing.Point(141, 73);
            this.lblPrinterOrientation.Name = "lblPrinterOrientation";
            this.lblPrinterOrientation.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblPrinterOrientation.Size = new System.Drawing.Size(122, 18);
            this.lblPrinterOrientation.TabIndex = 25;
            // 
            // Label10
            // 
            this.Label10.BackColor = System.Drawing.SystemColors.Control;
            this.Label10.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label10.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label10.Location = new System.Drawing.Point(10, 73);
            this.Label10.Name = "Label10";
            this.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label10.Size = new System.Drawing.Size(250, 18);
            this.Label10.TabIndex = 24;
            this.Label10.Text = "纸张定位:";
            // 
            // lblPrinterName
            // 
            this.lblPrinterName.BackColor = System.Drawing.SystemColors.Control;
            this.lblPrinterName.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblPrinterName.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrinterName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPrinterName.Location = new System.Drawing.Point(58, 20);
            this.lblPrinterName.Name = "lblPrinterName";
            this.lblPrinterName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblPrinterName.Size = new System.Drawing.Size(209, 28);
            this.lblPrinterName.TabIndex = 4;
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.SystemColors.Control;
            this.Label7.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label7.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label7.Location = new System.Drawing.Point(10, 21);
            this.Label7.Name = "Label7";
            this.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label7.Size = new System.Drawing.Size(250, 17);
            this.Label7.TabIndex = 3;
            this.Label7.Text = "名字:";
            // 
            // lblPrinterSize
            // 
            this.lblPrinterSize.BackColor = System.Drawing.SystemColors.Control;
            this.lblPrinterSize.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblPrinterSize.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrinterSize.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPrinterSize.Location = new System.Drawing.Point(86, 49);
            this.lblPrinterSize.Name = "lblPrinterSize";
            this.lblPrinterSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblPrinterSize.Size = new System.Drawing.Size(181, 19);
            this.lblPrinterSize.TabIndex = 2;
            // 
            // lblPdcdcrinter
            // 
            this.lblPdcdcrinter.BackColor = System.Drawing.SystemColors.Control;
            this.lblPdcdcrinter.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblPdcdcrinter.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPdcdcrinter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPdcdcrinter.Location = new System.Drawing.Point(10, 49);
            this.lblPdcdcrinter.Name = "lblPdcdcrinter";
            this.lblPdcdcrinter.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblPdcdcrinter.Size = new System.Drawing.Size(250, 19);
            this.lblPdcdcrinter.TabIndex = 1;
            this.lblPdcdcrinter.Text = "页面大小:";
            // 
            // Frame2
            // 
            this.Frame2.BackColor = System.Drawing.SystemColors.Control;
            this.Frame2.Controls.Add(this.label3);
            this.Frame2.Controls.Add(this.optLandscape);
            this.Frame2.Controls.Add(this.optPortrait);
            this.Frame2.Controls.Add(this.cboPageToPrinterMapping);
            this.Frame2.Controls.Add(this.cboPageSize);
            this.Frame2.Controls.Add(this.lblPageCount);
            this.Frame2.Controls.Add(this.Label9);
            this.Frame2.Controls.Add(this.Label8);
            this.Frame2.Controls.Add(this.Label6);
            this.Frame2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Frame2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Frame2.Location = new System.Drawing.Point(414, 12);
            this.Frame2.Name = "Frame2";
            this.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Frame2.Size = new System.Drawing.Size(273, 281);
            this.Frame2.TabIndex = 23;
            this.Frame2.TabStop = false;
            this.Frame2.Text = "页面属性";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 191);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(261, 82);
            this.label3.TabIndex = 27;
            this.label3.Text = "改变页面导向或大小可能导致地图框缩小．";
            // 
            // optLandscape
            // 
            this.optLandscape.BackColor = System.Drawing.SystemColors.Control;
            this.optLandscape.Cursor = System.Windows.Forms.Cursors.Default;
            this.optLandscape.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optLandscape.ForeColor = System.Drawing.SystemColors.ControlText;
            this.optLandscape.Location = new System.Drawing.Point(89, 129);
            this.optLandscape.Name = "optLandscape";
            this.optLandscape.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.optLandscape.Size = new System.Drawing.Size(174, 27);
            this.optLandscape.TabIndex = 22;
            this.optLandscape.TabStop = true;
            this.optLandscape.Text = "横向";
            this.optLandscape.UseVisualStyleBackColor = false;
            this.optLandscape.Click += new System.EventHandler(this.optLandscape_Click);
            // 
            // optPortrait
            // 
            this.optPortrait.BackColor = System.Drawing.SystemColors.Control;
            this.optPortrait.Cursor = System.Windows.Forms.Cursors.Default;
            this.optPortrait.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optPortrait.ForeColor = System.Drawing.SystemColors.ControlText;
            this.optPortrait.Location = new System.Drawing.Point(10, 129);
            this.optPortrait.Name = "optPortrait";
            this.optPortrait.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.optPortrait.Size = new System.Drawing.Size(119, 27);
            this.optPortrait.TabIndex = 21;
            this.optPortrait.TabStop = true;
            this.optPortrait.Text = "纵向";
            this.optPortrait.UseVisualStyleBackColor = false;
            this.optPortrait.Click += new System.EventHandler(this.optPortrait_Click);
            // 
            // cboPageToPrinterMapping
            // 
            this.cboPageToPrinterMapping.BackColor = System.Drawing.SystemColors.Window;
            this.cboPageToPrinterMapping.Cursor = System.Windows.Forms.Cursors.Default;
            this.cboPageToPrinterMapping.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPageToPrinterMapping.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboPageToPrinterMapping.Location = new System.Drawing.Point(10, 95);
            this.cboPageToPrinterMapping.Name = "cboPageToPrinterMapping";
            this.cboPageToPrinterMapping.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cboPageToPrinterMapping.Size = new System.Drawing.Size(250, 22);
            this.cboPageToPrinterMapping.TabIndex = 20;
            this.cboPageToPrinterMapping.Text = "Combo2";
            this.cboPageToPrinterMapping.Click += new System.EventHandler(this.cboPageToPrinterMapping_Click);
            // 
            // cboPageSize
            // 
            this.cboPageSize.BackColor = System.Drawing.SystemColors.Window;
            this.cboPageSize.Cursor = System.Windows.Forms.Cursors.Default;
            this.cboPageSize.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPageSize.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboPageSize.Location = new System.Drawing.Point(10, 43);
            this.cboPageSize.Name = "cboPageSize";
            this.cboPageSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cboPageSize.Size = new System.Drawing.Size(250, 22);
            this.cboPageSize.TabIndex = 18;
            this.cboPageSize.Text = "Combo1";
            this.cboPageSize.SelectedIndexChanged += new System.EventHandler(this.cboPageSize_SelectedIndexChanged);
            // 
            // lblPageCount
            // 
            this.lblPageCount.BackColor = System.Drawing.SystemColors.Control;
            this.lblPageCount.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblPageCount.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageCount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPageCount.Location = new System.Drawing.Point(116, 163);
            this.lblPageCount.Name = "lblPageCount";
            this.lblPageCount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblPageCount.Size = new System.Drawing.Size(144, 19);
            this.lblPageCount.TabIndex = 26;
            // 
            // Label9
            // 
            this.Label9.BackColor = System.Drawing.SystemColors.Control;
            this.Label9.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label9.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label9.Location = new System.Drawing.Point(10, 163);
            this.Label9.Name = "Label9";
            this.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label9.Size = new System.Drawing.Size(161, 19);
            this.Label9.TabIndex = 23;
            this.Label9.Text = "页数: ";
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.SystemColors.Control;
            this.Label8.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label8.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label8.Location = new System.Drawing.Point(10, 77);
            this.Label8.Name = "Label8";
            this.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label8.Size = new System.Drawing.Size(250, 28);
            this.Label8.TabIndex = 19;
            this.Label8.Text = "页面打印机的映射";
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.SystemColors.Control;
            this.Label6.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label6.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label6.Location = new System.Drawing.Point(10, 26);
            this.Label6.Name = "Label6";
            this.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label6.Size = new System.Drawing.Size(250, 36);
            this.Label6.TabIndex = 17;
            this.Label6.Text = "页面大小";
            // 
            // PrintPageLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 526);
            this.Controls.Add(this.axPageLayoutControl1);
            this.Controls.Add(this.fraPrint);
            this.Controls.Add(this.fraPrinter);
            this.Controls.Add(this.Frame2);
            this.Name = "PrintPageLayout";
            this.Text = "PrintPageLayout";
            this.Load += new System.EventHandler(this.PrintPageLayout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).EndInit();
            this.fraPrint.ResumeLayout(false);
            this.fraPrint.PerformLayout();
            this.fraPrinter.ResumeLayout(false);
            this.Frame2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxPageLayoutControl axPageLayoutControl1;
        public System.Windows.Forms.GroupBox fraPrint;
        public System.Windows.Forms.TextBox txbOverlap;
        public System.Windows.Forms.Button cmdPrint;
        public System.Windows.Forms.TextBox txbStartPage;
        public System.Windows.Forms.TextBox txbEndPage;
        public System.Windows.Forms.Label Label5;
        public System.Windows.Forms.Label Label1;
        public System.Windows.Forms.Label Label2;
        public System.Windows.Forms.GroupBox fraPrinter;
        public System.Windows.Forms.Label lblPrinterOrientation;
        public System.Windows.Forms.Label Label10;
        public System.Windows.Forms.Label lblPrinterName;
        public System.Windows.Forms.Label Label7;
        public System.Windows.Forms.Label lblPrinterSize;
        public System.Windows.Forms.Label lblPdcdcrinter;
        public System.Windows.Forms.GroupBox Frame2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.RadioButton optLandscape;
        public System.Windows.Forms.RadioButton optPortrait;
        public System.Windows.Forms.ComboBox cboPageToPrinterMapping;
        public System.Windows.Forms.ComboBox cboPageSize;
        public System.Windows.Forms.Label lblPageCount;
        public System.Windows.Forms.Label Label9;
        public System.Windows.Forms.Label Label8;
        public System.Windows.Forms.Label Label6;
    }
}