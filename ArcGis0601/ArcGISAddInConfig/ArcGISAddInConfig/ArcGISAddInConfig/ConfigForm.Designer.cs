namespace ArcGISAddInConfig
{
    partial class ConfigForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.com_Database = new System.Windows.Forms.ComboBox();
            this.text_Password = new System.Windows.Forms.TextBox();
            this.text_UserName = new System.Windows.Forms.TextBox();
            this.com_Authentication = new System.Windows.Forms.ComboBox();
            this.text_ServerName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.but_Apply = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.but_Close = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.com_Database);
            this.groupBox1.Controls.Add(this.text_Password);
            this.groupBox1.Controls.Add(this.text_UserName);
            this.groupBox1.Controls.Add(this.com_Authentication);
            this.groupBox1.Controls.Add(this.text_ServerName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(384, 179);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // com_Database
            // 
            this.com_Database.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_Database.FormattingEnabled = true;
            this.com_Database.Location = new System.Drawing.Point(122, 140);
            this.com_Database.Name = "com_Database";
            this.com_Database.Size = new System.Drawing.Size(184, 20);
            this.com_Database.TabIndex = 9;
            this.com_Database.Click += new System.EventHandler(this.com_Database_Click);
            // 
            // text_Password
            // 
            this.text_Password.Location = new System.Drawing.Point(122, 109);
            this.text_Password.Name = "text_Password";
            this.text_Password.Size = new System.Drawing.Size(184, 21);
            this.text_Password.TabIndex = 8;
            this.text_Password.UseSystemPasswordChar = true;
            // 
            // text_UserName
            // 
            this.text_UserName.Location = new System.Drawing.Point(122, 79);
            this.text_UserName.Name = "text_UserName";
            this.text_UserName.Size = new System.Drawing.Size(184, 21);
            this.text_UserName.TabIndex = 7;
            // 
            // com_Authentication
            // 
            this.com_Authentication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_Authentication.FormattingEnabled = true;
            this.com_Authentication.Items.AddRange(new object[] {
            "Windows Authentication",
            "SQL Server Authentication"});
            this.com_Authentication.Location = new System.Drawing.Point(122, 49);
            this.com_Authentication.Name = "com_Authentication";
            this.com_Authentication.Size = new System.Drawing.Size(243, 20);
            this.com_Authentication.TabIndex = 6;
            // 
            // text_ServerName
            // 
            this.text_ServerName.Location = new System.Drawing.Point(122, 19);
            this.text_ServerName.Name = "text_ServerName";
            this.text_ServerName.Size = new System.Drawing.Size(243, 21);
            this.text_ServerName.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(57, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "Database:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(57, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "User name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Authentication:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server name:";
            // 
            // but_Apply
            // 
            this.but_Apply.Location = new System.Drawing.Point(238, 234);
            this.but_Apply.Name = "but_Apply";
            this.but_Apply.Size = new System.Drawing.Size(75, 23);
            this.but_Apply.TabIndex = 7;
            this.but_Apply.Text = "Apply";
            this.but_Apply.UseVisualStyleBackColor = true;
            this.but_Apply.Click += new System.EventHandler(this.but_Apply_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(403, 204);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "DataSource";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(411, 229);
            this.tabControl1.TabIndex = 6;
            // 
            // but_Close
            // 
            this.but_Close.Location = new System.Drawing.Point(326, 234);
            this.but_Close.Name = "but_Close";
            this.but_Close.Size = new System.Drawing.Size(75, 23);
            this.but_Close.TabIndex = 8;
            this.but_Close.Text = "Close";
            this.but_Close.UseVisualStyleBackColor = true;
            this.but_Close.Click += new System.EventHandler(this.but_Close_Click);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(411, 257);
            this.Controls.Add(this.but_Apply);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.but_Close);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UTP Add-In Config";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox text_Password;
        private System.Windows.Forms.TextBox text_UserName;
        private System.Windows.Forms.ComboBox com_Authentication;
        private System.Windows.Forms.TextBox text_ServerName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button but_Apply;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button but_Close;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ComboBox com_Database;
    }
}