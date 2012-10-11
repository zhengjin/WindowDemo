namespace custom
{
    partial class Form1
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
            this.LatTBox = new System.Windows.Forms.TextBox();
            this.LonTBox = new System.Windows.Forms.TextBox();
            this.X = new System.Windows.Forms.TextBox();
            this.Y = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tansform1 = new custom.Tansform();
            this.SuspendLayout();
            // 
            // LatTBox
            // 
            this.LatTBox.Location = new System.Drawing.Point(65, 46);
            this.LatTBox.Name = "LatTBox";
            this.LatTBox.Size = new System.Drawing.Size(100, 21);
            this.LatTBox.TabIndex = 0;
            // 
            // LonTBox
            // 
            this.LonTBox.Location = new System.Drawing.Point(65, 86);
            this.LonTBox.Name = "LonTBox";
            this.LonTBox.Size = new System.Drawing.Size(100, 21);
            this.LonTBox.TabIndex = 1;
            // 
            // X
            // 
            this.X.Location = new System.Drawing.Point(209, 46);
            this.X.Name = "X";
            this.X.Size = new System.Drawing.Size(100, 21);
            this.X.TabIndex = 2;
            // 
            // Y
            // 
            this.Y.Location = new System.Drawing.Point(209, 86);
            this.Y.Name = "Y";
            this.Y.Size = new System.Drawing.Size(100, 21);
            this.Y.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "经度:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "纬度:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(167, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "X坐标:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(168, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "Y坐标:";
            // 
            // tansform1
            // 
            this.tansform1.Location = new System.Drawing.Point(12, 126);
            this.tansform1.Name = "tansform1";
            this.tansform1.Size = new System.Drawing.Size(322, 79);
            this.tansform1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 266);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tansform1);
            this.Controls.Add(this.Y);
            this.Controls.Add(this.X);
            this.Controls.Add(this.LonTBox);
            this.Controls.Add(this.LatTBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox LatTBox;
        private System.Windows.Forms.TextBox LonTBox;
        private System.Windows.Forms.TextBox X;
        private System.Windows.Forms.TextBox Y;
        private Tansform tansform1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

