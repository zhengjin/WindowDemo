namespace LinGIS
{
    partial class DisplayFeedbackFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayFeedbackFrm));
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNewPolyline = new System.Windows.Forms.ToolStripButton();
            this.btnNewCircle = new System.Windows.Forms.ToolStripButton();
            this.btnNewEnvelope = new System.Windows.Forms.ToolStripButton();
            this.btnNewPolygon = new System.Windows.Forms.ToolStripButton();
            this.btnNewBezierCurve = new System.Windows.Forms.ToolStripButton();
            this.btnStretchLine = new System.Windows.Forms.ToolStripButton();
            this.btnLineMovePoint = new System.Windows.Forms.ToolStripButton();
            this.btnPolygonMovePoint = new System.Windows.Forms.ToolStripButton();
            this.btnMoveGeometry = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(270, 221);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 0;
            // 
            // axMapControl1
            // 
            this.axMapControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.axMapControl1.Location = new System.Drawing.Point(12, 42);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(628, 420);
            this.axMapControl1.TabIndex = 1;
            this.axMapControl1.OnExtentUpdated += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnExtentUpdatedEventHandler(this.axMapControl1_OnExtentUpdated);
            this.axMapControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
            this.axMapControl1.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.axMapControl1_OnMouseMove);
            this.axMapControl1.OnDoubleClick += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnDoubleClickEventHandler(this.axMapControl1_OnDoubleClick);
            this.axMapControl1.OnMouseUp += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseUpEventHandler(this.axMapControl1_OnMouseUp);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewPolyline,
            this.btnNewCircle,
            this.btnNewEnvelope,
            this.btnNewPolygon,
            this.btnNewBezierCurve,
            this.btnStretchLine,
            this.btnLineMovePoint,
            this.btnPolygonMovePoint,
            this.btnMoveGeometry,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(652, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNewPolyline
            // 
            this.btnNewPolyline.CheckOnClick = true;
            this.btnNewPolyline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewPolyline.Image = ((System.Drawing.Image)(resources.GetObject("btnNewPolyline.Image")));
            this.btnNewPolyline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewPolyline.Name = "btnNewPolyline";
            this.btnNewPolyline.Size = new System.Drawing.Size(23, 22);
            this.btnNewPolyline.Text = "新建多义线";
            this.btnNewPolyline.Click += new System.EventHandler(this.btnNewPolyline_Click);
            // 
            // btnNewCircle
            // 
            this.btnNewCircle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewCircle.Image = ((System.Drawing.Image)(resources.GetObject("btnNewCircle.Image")));
            this.btnNewCircle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewCircle.Name = "btnNewCircle";
            this.btnNewCircle.Size = new System.Drawing.Size(23, 22);
            this.btnNewCircle.Text = "新建圆";
            this.btnNewCircle.Click += new System.EventHandler(this.btnNewPolyline_Click);
            // 
            // btnNewEnvelope
            // 
            this.btnNewEnvelope.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewEnvelope.Image = ((System.Drawing.Image)(resources.GetObject("btnNewEnvelope.Image")));
            this.btnNewEnvelope.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewEnvelope.Name = "btnNewEnvelope";
            this.btnNewEnvelope.Size = new System.Drawing.Size(23, 22);
            this.btnNewEnvelope.Text = "新建矩形";
            this.btnNewEnvelope.Click += new System.EventHandler(this.btnNewPolyline_Click);
            // 
            // btnNewPolygon
            // 
            this.btnNewPolygon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewPolygon.Image = ((System.Drawing.Image)(resources.GetObject("btnNewPolygon.Image")));
            this.btnNewPolygon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewPolygon.Name = "btnNewPolygon";
            this.btnNewPolygon.Size = new System.Drawing.Size(23, 22);
            this.btnNewPolygon.Text = "新建多边形";
            this.btnNewPolygon.Click += new System.EventHandler(this.btnNewPolyline_Click);
            // 
            // btnNewBezierCurve
            // 
            this.btnNewBezierCurve.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewBezierCurve.Image = ((System.Drawing.Image)(resources.GetObject("btnNewBezierCurve.Image")));
            this.btnNewBezierCurve.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewBezierCurve.Name = "btnNewBezierCurve";
            this.btnNewBezierCurve.Size = new System.Drawing.Size(23, 22);
            this.btnNewBezierCurve.Text = "新建Bezier曲线";
            this.btnNewBezierCurve.Click += new System.EventHandler(this.btnNewPolyline_Click);
            // 
            // btnStretchLine
            // 
            this.btnStretchLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnStretchLine.Image = ((System.Drawing.Image)(resources.GetObject("btnStretchLine.Image")));
            this.btnStretchLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStretchLine.Name = "btnStretchLine";
            this.btnStretchLine.Size = new System.Drawing.Size(71, 22);
            this.btnStretchLine.Text = "拉伸多义线";
            this.btnStretchLine.Click += new System.EventHandler(this.btnNewPolyline_Click);
            // 
            // btnLineMovePoint
            // 
            this.btnLineMovePoint.CheckOnClick = true;
            this.btnLineMovePoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnLineMovePoint.Image = ((System.Drawing.Image)(resources.GetObject("btnLineMovePoint.Image")));
            this.btnLineMovePoint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLineMovePoint.Name = "btnLineMovePoint";
            this.btnLineMovePoint.Size = new System.Drawing.Size(95, 22);
            this.btnLineMovePoint.Text = "移动多义线节点";
            this.btnLineMovePoint.Click += new System.EventHandler(this.btnNewPolyline_Click);
            // 
            // btnPolygonMovePoint
            // 
            this.btnPolygonMovePoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnPolygonMovePoint.Image = ((System.Drawing.Image)(resources.GetObject("btnPolygonMovePoint.Image")));
            this.btnPolygonMovePoint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPolygonMovePoint.Name = "btnPolygonMovePoint";
            this.btnPolygonMovePoint.Size = new System.Drawing.Size(95, 22);
            this.btnPolygonMovePoint.Text = "移动多边形节点";
            this.btnPolygonMovePoint.Click += new System.EventHandler(this.btnNewPolyline_Click);
            // 
            // btnMoveGeometry
            // 
            this.btnMoveGeometry.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnMoveGeometry.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveGeometry.Image")));
            this.btnMoveGeometry.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveGeometry.Name = "btnMoveGeometry";
            this.btnMoveGeometry.Size = new System.Drawing.Size(115, 22);
            this.btnMoveGeometry.Text = "移动几何对象(多个)";
            this.btnMoveGeometry.Click += new System.EventHandler(this.btnNewPolyline_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(135, 22);
            this.toolStripButton1.Text = "新建多义线(自定义Tool)";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(10, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(532, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "本程序尝试了AE中支持IDisplayFeedback大部分对象，并尝试了自定义Command和Tool";
            // 
            // DisplayFeedbackFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 474);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.axMapControl1);
            this.Name = "DisplayFeedbackFrm";
            this.Text = "DisplayFeedback全演练";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNewPolyline;
        private System.Windows.Forms.ToolStripButton btnLineMovePoint;
        private System.Windows.Forms.ToolStripButton btnNewCircle;
        private System.Windows.Forms.ToolStripButton btnNewEnvelope;
        private System.Windows.Forms.ToolStripButton btnNewPolygon;
        private System.Windows.Forms.ToolStripButton btnPolygonMovePoint;
        private System.Windows.Forms.ToolStripButton btnNewBezierCurve;
        private System.Windows.Forms.ToolStripButton btnMoveGeometry;
        private System.Windows.Forms.ToolStripButton btnStretchLine;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Label label1;
    }
}

