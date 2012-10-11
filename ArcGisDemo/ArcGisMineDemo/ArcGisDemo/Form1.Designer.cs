namespace ArcGisDemo
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.axToolbarControl = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.axMapControl = new ESRI.ArcGIS.Controls.AxMapControl();
            this.axTOCControl = new ESRI.ArcGIS.Controls.AxTOCControl();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl)).BeginInit();
            this.SuspendLayout();
            // 
            // axToolbarControl
            // 
            this.axToolbarControl.Location = new System.Drawing.Point(12, 12);
            this.axToolbarControl.Name = "axToolbarControl";
            this.axToolbarControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl.OcxState")));
            this.axToolbarControl.Size = new System.Drawing.Size(802, 28);
            this.axToolbarControl.TabIndex = 0;
            // 
            // axMapControl
            // 
            this.axMapControl.Location = new System.Drawing.Point(289, 55);
            this.axMapControl.Name = "axMapControl";
            this.axMapControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl.OcxState")));
            this.axMapControl.Size = new System.Drawing.Size(525, 396);
            this.axMapControl.TabIndex = 1;
            // 
            // axTOCControl
            // 
            this.axTOCControl.Location = new System.Drawing.Point(12, 55);
            this.axTOCControl.Name = "axTOCControl";
            this.axTOCControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl.OcxState")));
            this.axTOCControl.Size = new System.Drawing.Size(265, 396);
            this.axTOCControl.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 489);
            this.Controls.Add(this.axTOCControl);
            this.Controls.Add(this.axMapControl);
            this.Controls.Add(this.axToolbarControl);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl;

    }
}

