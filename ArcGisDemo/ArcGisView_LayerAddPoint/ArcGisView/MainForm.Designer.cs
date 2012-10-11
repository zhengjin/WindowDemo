namespace ArcGisView
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            //Ensures that any ESRI libraries that have been used are unloaded in the correct order. 
            //Failure to do this may result in random crashes on exit due to the operating system unloading 
            //the libraries in the incorrect order. 
            ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown();

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNewDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpenDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.menuExitApp = new System.Windows.Forms.ToolStripMenuItem();
            this.查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关键字查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.点击查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加地物ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加点型图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加线形图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加面形地物ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭添加地物ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加标注ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开始添加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.结束添加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除地物ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusBarXY = new System.Windows.Forms.ToolStripStatusLabel();
            this.axMapControl2 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl2)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.查询ToolStripMenuItem,
            this.添加地物ToolStripMenuItem,
            this.添加标注ToolStripMenuItem,
            this.删除地物ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(852, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNewDoc,
            this.menuOpenDoc,
            this.menuSaveDoc,
            this.menuSaveAs,
            this.menuSeparator,
            this.menuExitApp});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(44, 21);
            this.menuFile.Text = "文件";
            // 
            // menuNewDoc
            // 
            this.menuNewDoc.ImageTransparentColor = System.Drawing.Color.White;
            this.menuNewDoc.Name = "menuNewDoc";
            this.menuNewDoc.Size = new System.Drawing.Size(121, 22);
            this.menuNewDoc.Text = "新文件";
            this.menuNewDoc.Click += new System.EventHandler(this.menuNewDoc_Click);
            // 
            // menuOpenDoc
            // 
            this.menuOpenDoc.Image = ((System.Drawing.Image)(resources.GetObject("menuOpenDoc.Image")));
            this.menuOpenDoc.ImageTransparentColor = System.Drawing.Color.White;
            this.menuOpenDoc.Name = "menuOpenDoc";
            this.menuOpenDoc.Size = new System.Drawing.Size(121, 22);
            this.menuOpenDoc.Text = "打开...";
            this.menuOpenDoc.Click += new System.EventHandler(this.menuOpenDoc_Click);
            // 
            // menuSaveDoc
            // 
            this.menuSaveDoc.Image = ((System.Drawing.Image)(resources.GetObject("menuSaveDoc.Image")));
            this.menuSaveDoc.ImageTransparentColor = System.Drawing.Color.White;
            this.menuSaveDoc.Name = "menuSaveDoc";
            this.menuSaveDoc.Size = new System.Drawing.Size(121, 22);
            this.menuSaveDoc.Text = "保存";
            this.menuSaveDoc.Click += new System.EventHandler(this.menuSaveDoc_Click);
            // 
            // menuSaveAs
            // 
            this.menuSaveAs.Name = "menuSaveAs";
            this.menuSaveAs.Size = new System.Drawing.Size(121, 22);
            this.menuSaveAs.Text = "保存为...";
            this.menuSaveAs.Click += new System.EventHandler(this.menuSaveAs_Click);
            // 
            // menuSeparator
            // 
            this.menuSeparator.Name = "menuSeparator";
            this.menuSeparator.Size = new System.Drawing.Size(118, 6);
            // 
            // menuExitApp
            // 
            this.menuExitApp.Name = "menuExitApp";
            this.menuExitApp.Size = new System.Drawing.Size(121, 22);
            this.menuExitApp.Text = "退出";
            this.menuExitApp.Click += new System.EventHandler(this.menuExitApp_Click);
            // 
            // 查询ToolStripMenuItem
            // 
            this.查询ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关键字查询ToolStripMenuItem,
            this.点击查询ToolStripMenuItem});
            this.查询ToolStripMenuItem.Name = "查询ToolStripMenuItem";
            this.查询ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.查询ToolStripMenuItem.Text = "查询";
            // 
            // 关键字查询ToolStripMenuItem
            // 
            this.关键字查询ToolStripMenuItem.Name = "关键字查询ToolStripMenuItem";
            this.关键字查询ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.关键字查询ToolStripMenuItem.Text = "关键字查询";
            this.关键字查询ToolStripMenuItem.Click += new System.EventHandler(this.关键字查询ToolStripMenuItem_Click);
            // 
            // 点击查询ToolStripMenuItem
            // 
            this.点击查询ToolStripMenuItem.Name = "点击查询ToolStripMenuItem";
            this.点击查询ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.点击查询ToolStripMenuItem.Text = "鼠标查询";
            this.点击查询ToolStripMenuItem.Click += new System.EventHandler(this.点击查询ToolStripMenuItem_Click);
            // 
            // 添加地物ToolStripMenuItem
            // 
            this.添加地物ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加点型图层ToolStripMenuItem,
            this.添加线形图层ToolStripMenuItem,
            this.添加面形地物ToolStripMenuItem,
            this.关闭添加地物ToolStripMenuItem});
            this.添加地物ToolStripMenuItem.Name = "添加地物ToolStripMenuItem";
            this.添加地物ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.添加地物ToolStripMenuItem.Text = "添加地物";
            // 
            // 添加点型图层ToolStripMenuItem
            // 
            this.添加点型图层ToolStripMenuItem.Name = "添加点型图层ToolStripMenuItem";
            this.添加点型图层ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.添加点型图层ToolStripMenuItem.Text = "添加点形地物";
            this.添加点型图层ToolStripMenuItem.Click += new System.EventHandler(this.添加点型图层ToolStripMenuItem_Click);
            // 
            // 添加线形图层ToolStripMenuItem
            // 
            this.添加线形图层ToolStripMenuItem.Name = "添加线形图层ToolStripMenuItem";
            this.添加线形图层ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.添加线形图层ToolStripMenuItem.Text = "添加线形地物";
            this.添加线形图层ToolStripMenuItem.Click += new System.EventHandler(this.添加线形图层ToolStripMenuItem_Click);
            // 
            // 添加面形地物ToolStripMenuItem
            // 
            this.添加面形地物ToolStripMenuItem.Name = "添加面形地物ToolStripMenuItem";
            this.添加面形地物ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.添加面形地物ToolStripMenuItem.Text = "添加面形地物";
            this.添加面形地物ToolStripMenuItem.Click += new System.EventHandler(this.添加面形地物ToolStripMenuItem_Click);
            // 
            // 关闭添加地物ToolStripMenuItem
            // 
            this.关闭添加地物ToolStripMenuItem.Name = "关闭添加地物ToolStripMenuItem";
            this.关闭添加地物ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.关闭添加地物ToolStripMenuItem.Text = "关闭添加地物";
            this.关闭添加地物ToolStripMenuItem.Click += new System.EventHandler(this.关闭添加地物ToolStripMenuItem_Click);
            // 
            // 添加标注ToolStripMenuItem
            // 
            this.添加标注ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.开始添加ToolStripMenuItem,
            this.结束添加ToolStripMenuItem});
            this.添加标注ToolStripMenuItem.Name = "添加标注ToolStripMenuItem";
            this.添加标注ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.添加标注ToolStripMenuItem.Text = "添加标注";
            // 
            // 开始添加ToolStripMenuItem
            // 
            this.开始添加ToolStripMenuItem.Name = "开始添加ToolStripMenuItem";
            this.开始添加ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.开始添加ToolStripMenuItem.Text = "开始添加";
            this.开始添加ToolStripMenuItem.Click += new System.EventHandler(this.开始添加ToolStripMenuItem_Click);
            // 
            // 结束添加ToolStripMenuItem
            // 
            this.结束添加ToolStripMenuItem.Name = "结束添加ToolStripMenuItem";
            this.结束添加ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.结束添加ToolStripMenuItem.Text = "结束添加";
            this.结束添加ToolStripMenuItem.Click += new System.EventHandler(this.结束添加ToolStripMenuItem_Click);
            // 
            // 删除地物ToolStripMenuItem
            // 
            this.删除地物ToolStripMenuItem.Name = "删除地物ToolStripMenuItem";
            this.删除地物ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.删除地物ToolStripMenuItem.Text = "删除地物";
            this.删除地物ToolStripMenuItem.Click += new System.EventHandler(this.删除地物ToolStripMenuItem_Click);
            // 
            // axMapControl1
            // 
            this.axMapControl1.Location = new System.Drawing.Point(191, 24);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(661, 495);
            this.axMapControl1.TabIndex = 2;
            this.axMapControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
            this.axMapControl1.OnMouseUp += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseUpEventHandler(this.axMapControl1_OnMouseUp);
            this.axMapControl1.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.axMapControl1_OnMouseMove);
            this.axMapControl1.OnAfterDraw += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnAfterDrawEventHandler(this.axMapControl1_OnAfterDraw);
            this.axMapControl1.OnExtentUpdated += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnExtentUpdatedEventHandler(this.axMapControl1_OnExtentUpdated);
            this.axMapControl1.OnMapReplaced += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMapReplacedEventHandler(this.axMapControl1_OnMapReplaced);
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Location = new System.Drawing.Point(3, 24);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(188, 319);
            this.axTOCControl1.TabIndex = 4;
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(466, 278);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 5;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 25);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 516);
            this.splitter1.TabIndex = 6;
            this.splitter1.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBarXY});
            this.statusStrip1.Location = new System.Drawing.Point(3, 519);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(849, 22);
            this.statusStrip1.Stretch = false;
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusBar1";
            // 
            // statusBarXY
            // 
            this.statusBarXY.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.statusBarXY.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusBarXY.Name = "statusBarXY";
            this.statusBarXY.Size = new System.Drawing.Size(57, 17);
            this.statusBarXY.Text = "Test 123";
            // 
            // axMapControl2
            // 
            this.axMapControl2.Location = new System.Drawing.Point(3, 343);
            this.axMapControl2.Name = "axMapControl2";
            this.axMapControl2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl2.OcxState")));
            this.axMapControl2.Size = new System.Drawing.Size(188, 176);
            this.axMapControl2.TabIndex = 8;
            this.axMapControl2.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl2_OnMouseDown);
            this.axMapControl2.OnMouseUp += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseUpEventHandler(this.axMapControl2_OnMouseUp);
            this.axMapControl2.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.axMapControl2_OnMouseMove);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 541);
            this.Controls.Add(this.axMapControl2);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.axMapControl1);
            this.Controls.Add(this.axTOCControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "ArcEngine Controls Application";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuNewDoc;
        private System.Windows.Forms.ToolStripMenuItem menuOpenDoc;
        private System.Windows.Forms.ToolStripMenuItem menuSaveDoc;
        private System.Windows.Forms.ToolStripMenuItem menuSaveAs;
        private System.Windows.Forms.ToolStripMenuItem menuExitApp;
        private System.Windows.Forms.ToolStripSeparator menuSeparator;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusBarXY;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl2;
        private System.Windows.Forms.ToolStripMenuItem 查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关键字查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加地物ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加点型图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加线形图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加面形地物ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 点击查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关闭添加地物ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加标注ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 开始添加ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 结束添加ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除地物ToolStripMenuItem;
    }
}

