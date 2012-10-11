namespace Client2
{
    partial class Client
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnSayHello = new System.Windows.Forms.Button();
            this.btnWithOneWay = new System.Windows.Forms.Button();
            this.btnWithoutOneWay = new System.Windows.Forms.Button();
            this.btnDuplex = new System.Windows.Forms.Button();
            this.btnOpenDialog = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtSourcePath = new System.Windows.Forms.TextBox();
            this.btnHelloStreamed = new System.Windows.Forms.Button();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDuplexReentrant = new System.Windows.Forms.Button();
            this.btnMSMQ = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(26, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 21);
            this.txtName.TabIndex = 0;
            this.txtName.Text = "webabcd";
            // 
            // btnSayHello
            // 
            this.btnSayHello.Location = new System.Drawing.Point(160, 12);
            this.btnSayHello.Name = "btnSayHello";
            this.btnSayHello.Size = new System.Drawing.Size(100, 23);
            this.btnSayHello.TabIndex = 1;
            this.btnSayHello.Text = "HelloBinding";
            this.btnSayHello.UseVisualStyleBackColor = true;
            this.btnSayHello.Click += new System.EventHandler(this.btnSayHello_Click);
            // 
            // btnWithOneWay
            // 
            this.btnWithOneWay.Location = new System.Drawing.Point(26, 41);
            this.btnWithOneWay.Name = "btnWithOneWay";
            this.btnWithOneWay.Size = new System.Drawing.Size(234, 23);
            this.btnWithOneWay.TabIndex = 2;
            this.btnWithOneWay.Text = "HelloWithOneWay";
            this.btnWithOneWay.UseVisualStyleBackColor = true;
            this.btnWithOneWay.Click += new System.EventHandler(this.btnWithOneWay_Click);
            // 
            // btnWithoutOneWay
            // 
            this.btnWithoutOneWay.Location = new System.Drawing.Point(26, 70);
            this.btnWithoutOneWay.Name = "btnWithoutOneWay";
            this.btnWithoutOneWay.Size = new System.Drawing.Size(234, 23);
            this.btnWithoutOneWay.TabIndex = 3;
            this.btnWithoutOneWay.Text = "HelloWithoutOneWay";
            this.btnWithoutOneWay.UseVisualStyleBackColor = true;
            this.btnWithoutOneWay.Click += new System.EventHandler(this.btnWithoutOneWay_Click);
            // 
            // btnDuplex
            // 
            this.btnDuplex.Location = new System.Drawing.Point(26, 99);
            this.btnDuplex.Name = "btnDuplex";
            this.btnDuplex.Size = new System.Drawing.Size(234, 23);
            this.btnDuplex.TabIndex = 4;
            this.btnDuplex.Text = "HelloDuplex";
            this.btnDuplex.UseVisualStyleBackColor = true;
            this.btnDuplex.Click += new System.EventHandler(this.btnDuplex_Click);
            // 
            // btnOpenDialog
            // 
            this.btnOpenDialog.Location = new System.Drawing.Point(194, 126);
            this.btnOpenDialog.Name = "btnOpenDialog";
            this.btnOpenDialog.Size = new System.Drawing.Size(66, 23);
            this.btnOpenDialog.TabIndex = 5;
            this.btnOpenDialog.Text = "浏览";
            this.btnOpenDialog.UseVisualStyleBackColor = true;
            this.btnOpenDialog.Click += new System.EventHandler(this.btnOpenDialog_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "*.*|";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // txtSourcePath
            // 
            this.txtSourcePath.Location = new System.Drawing.Point(83, 128);
            this.txtSourcePath.Name = "txtSourcePath";
            this.txtSourcePath.Size = new System.Drawing.Size(105, 21);
            this.txtSourcePath.TabIndex = 6;
            // 
            // btnHelloStreamed
            // 
            this.btnHelloStreamed.Location = new System.Drawing.Point(194, 155);
            this.btnHelloStreamed.Name = "btnHelloStreamed";
            this.btnHelloStreamed.Size = new System.Drawing.Size(66, 23);
            this.btnHelloStreamed.TabIndex = 7;
            this.btnHelloStreamed.Text = "上传";
            this.btnHelloStreamed.UseVisualStyleBackColor = true;
            this.btnHelloStreamed.Click += new System.EventHandler(this.btnHelloStreamed_Click);
            // 
            // txtDestination
            // 
            this.txtDestination.Location = new System.Drawing.Point(83, 157);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(105, 21);
            this.txtDestination.TabIndex = 8;
            this.txtDestination.Text = "C:\\";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "源 文 件";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "上传路径";
            // 
            // btnDuplexReentrant
            // 
            this.btnDuplexReentrant.Location = new System.Drawing.Point(26, 184);
            this.btnDuplexReentrant.Name = "btnDuplexReentrant";
            this.btnDuplexReentrant.Size = new System.Drawing.Size(234, 23);
            this.btnDuplexReentrant.TabIndex = 11;
            this.btnDuplexReentrant.Text = "HelloDuplexReentrant";
            this.btnDuplexReentrant.UseVisualStyleBackColor = true;
            this.btnDuplexReentrant.Click += new System.EventHandler(this.btnDuplexReentrant_Click);
            // 
            // btnMSMQ
            // 
            this.btnMSMQ.Location = new System.Drawing.Point(26, 213);
            this.btnMSMQ.Name = "btnMSMQ";
            this.btnMSMQ.Size = new System.Drawing.Size(234, 23);
            this.btnMSMQ.TabIndex = 12;
            this.btnMSMQ.Text = "HelloMSMQ";
            this.btnMSMQ.UseVisualStyleBackColor = true;
            this.btnMSMQ.Click += new System.EventHandler(this.btnMSMQ_Click);
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.btnMSMQ);
            this.Controls.Add(this.btnDuplexReentrant);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDestination);
            this.Controls.Add(this.btnHelloStreamed);
            this.Controls.Add(this.txtSourcePath);
            this.Controls.Add(this.btnOpenDialog);
            this.Controls.Add(this.btnDuplex);
            this.Controls.Add(this.btnWithoutOneWay);
            this.Controls.Add(this.btnWithOneWay);
            this.Controls.Add(this.btnSayHello);
            this.Controls.Add(this.txtName);
            this.Name = "Client";
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnSayHello;
        private System.Windows.Forms.Button btnWithOneWay;
        private System.Windows.Forms.Button btnWithoutOneWay;
        private System.Windows.Forms.Button btnDuplex;
        private System.Windows.Forms.Button btnOpenDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtSourcePath;
        private System.Windows.Forms.Button btnHelloStreamed;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDuplexReentrant;
        private System.Windows.Forms.Button btnMSMQ;
    }
}