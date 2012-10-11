namespace ArcGISAddInShowMap
{
    partial class ShowMapDockableWindow
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
            this.buOk = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.com_Color = new System.Windows.Forms.ComboBox();
            this.com_Route = new System.Windows.Forms.ComboBox();
            this.com_Company = new System.Windows.Forms.ComboBox();
            this.check_DatailInformation = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buOk
            // 
            this.buOk.Location = new System.Drawing.Point(84, 160);
            this.buOk.Name = "buOk";
            this.buOk.Size = new System.Drawing.Size(75, 23);
            this.buOk.TabIndex = 3;
            this.buOk.Text = "OK";
            this.buOk.UseVisualStyleBackColor = true;
            this.buOk.Click += new System.EventHandler(this.buOk_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.com_Color);
            this.groupBox1.Controls.Add(this.com_Route);
            this.groupBox1.Controls.Add(this.com_Company);
            this.groupBox1.Controls.Add(this.check_DatailInformation);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(233, 146);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "UTP Information";
            // 
            // com_Color
            // 
            this.com_Color.FormattingEnabled = true;
            this.com_Color.Location = new System.Drawing.Point(77, 74);
            this.com_Color.Name = "com_Color";
            this.com_Color.Size = new System.Drawing.Size(121, 20);
            this.com_Color.TabIndex = 6;
            this.com_Color.Click += new System.EventHandler(this.com_Color_Click);
            // 
            // com_Route
            // 
            this.com_Route.FormattingEnabled = true;
            this.com_Route.Location = new System.Drawing.Point(77, 48);
            this.com_Route.Name = "com_Route";
            this.com_Route.Size = new System.Drawing.Size(121, 20);
            this.com_Route.TabIndex = 5;
            // 
            // com_Company
            // 
            this.com_Company.FormattingEnabled = true;
            this.com_Company.Location = new System.Drawing.Point(77, 20);
            this.com_Company.Name = "com_Company";
            this.com_Company.Size = new System.Drawing.Size(121, 20);
            this.com_Company.TabIndex = 4;
            this.com_Company.SelectedIndexChanged += new System.EventHandler(this.com_Company_SelectedIndexChanged);
            // 
            // check_DatailInformation
            // 
            this.check_DatailInformation.AutoSize = true;
            this.check_DatailInformation.Location = new System.Drawing.Point(77, 103);
            this.check_DatailInformation.Name = "check_DatailInformation";
            this.check_DatailInformation.Size = new System.Drawing.Size(132, 16);
            this.check_DatailInformation.TabIndex = 3;
            this.check_DatailInformation.Text = "&Detail Information";
            this.check_DatailInformation.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "Color:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Route:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Company:";
            // 
            // ShowMapDockableWindow
            // 
            this.Controls.Add(this.buOk);
            this.Controls.Add(this.groupBox1);
            this.Name = "ShowMapDockableWindow";
            this.Size = new System.Drawing.Size(251, 190);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buOk;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox com_Color;
        private System.Windows.Forms.ComboBox com_Route;
        private System.Windows.Forms.ComboBox com_Company;
        private System.Windows.Forms.CheckBox check_DatailInformation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColorDialog colorDialog1;




    }
}
