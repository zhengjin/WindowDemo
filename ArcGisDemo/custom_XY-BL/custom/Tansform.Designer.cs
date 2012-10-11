namespace custom
{
    partial class Tansform
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCoordinateSystem = new System.Windows.Forms.ComboBox();
            this.cmbNumberName = new System.Windows.Forms.ComboBox();
            this.btnXYtoBL = new System.Windows.Forms.Button();
            this.btnBLtoXY = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "坐标系:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "分带:";
            // 
            // cmbCoordinateSystem
            // 
            this.cmbCoordinateSystem.FormattingEnabled = true;
            this.cmbCoordinateSystem.Items.AddRange(new object[] {
            "西安80坐标系",
            "北京54坐标系"});
            this.cmbCoordinateSystem.Location = new System.Drawing.Point(57, 18);
            this.cmbCoordinateSystem.Name = "cmbCoordinateSystem";
            this.cmbCoordinateSystem.Size = new System.Drawing.Size(121, 20);
            this.cmbCoordinateSystem.TabIndex = 2;
            // 
            // cmbNumberName
            // 
            this.cmbNumberName.FormattingEnabled = true;
            this.cmbNumberName.Items.AddRange(new object[] {
            "6度分带",
            "3度分带"});
            this.cmbNumberName.Location = new System.Drawing.Point(57, 41);
            this.cmbNumberName.Name = "cmbNumberName";
            this.cmbNumberName.Size = new System.Drawing.Size(121, 20);
            this.cmbNumberName.TabIndex = 3;
            // 
            // btnXYtoBL
            // 
            this.btnXYtoBL.Location = new System.Drawing.Point(189, 14);
            this.btnXYtoBL.Name = "btnXYtoBL";
            this.btnXYtoBL.Size = new System.Drawing.Size(75, 23);
            this.btnXYtoBL.TabIndex = 4;
            this.btnXYtoBL.Text = "XY->BL";
            this.btnXYtoBL.UseVisualStyleBackColor = true;
            this.btnXYtoBL.Click += new System.EventHandler(this.btnXYtoBL_Click);
            // 
            // btnBLtoXY
            // 
            this.btnBLtoXY.Location = new System.Drawing.Point(189, 37);
            this.btnBLtoXY.Name = "btnBLtoXY";
            this.btnBLtoXY.Size = new System.Drawing.Size(75, 23);
            this.btnBLtoXY.TabIndex = 5;
            this.btnBLtoXY.Text = "BL->XY";
            this.btnBLtoXY.UseVisualStyleBackColor = true;
            this.btnBLtoXY.Click += new System.EventHandler(this.btnBLtoXY_Click);
            // 
            // Tansform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnBLtoXY);
            this.Controls.Add(this.btnXYtoBL);
            this.Controls.Add(this.cmbNumberName);
            this.Controls.Add(this.cmbCoordinateSystem);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Tansform";
            this.Size = new System.Drawing.Size(272, 69);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCoordinateSystem;
        private System.Windows.Forms.ComboBox cmbNumberName;
        private System.Windows.Forms.Button btnXYtoBL;
        private System.Windows.Forms.Button btnBLtoXY;
    }
}
