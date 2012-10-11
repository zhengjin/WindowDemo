namespace XL.Client
{
    partial class MainForm
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
            this.TopMenuP = new System.Windows.Forms.Panel();
            this.BottomP = new System.Windows.Forms.Panel();
            this.BottomInfoLB1 = new System.Windows.Forms.Label();
            this.SubMenuParentP = new System.Windows.Forms.Panel();
            this.SubHeaderLB = new System.Windows.Forms.Label();
            this.SubMenuP = new System.Windows.Forms.Panel();
            this.SubMenuSplitorP = new System.Windows.Forms.Panel();
            this.SubMenuMainSp = new System.Windows.Forms.Splitter();
            this.MainParentP = new System.Windows.Forms.Panel();
            this.MainContainerP = new System.Windows.Forms.Panel();
            this.MainSplitorP = new System.Windows.Forms.Panel();
            this.TabContainerP = new System.Windows.Forms.Panel();
            this.BottomP.SuspendLayout();
            this.SubMenuParentP.SuspendLayout();
            this.MainParentP.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopMenuP
            // 
            this.TopMenuP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(185)))), ((int)(((byte)(205)))));
            this.TopMenuP.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopMenuP.Location = new System.Drawing.Point(0, 0);
            this.TopMenuP.Name = "TopMenuP";
            this.TopMenuP.Size = new System.Drawing.Size(853, 56);
            this.TopMenuP.TabIndex = 0;
            // 
            // BottomP
            // 
            this.BottomP.Controls.Add(this.BottomInfoLB1);
            this.BottomP.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomP.Location = new System.Drawing.Point(0, 720);
            this.BottomP.Name = "BottomP";
            this.BottomP.Size = new System.Drawing.Size(853, 22);
            this.BottomP.TabIndex = 1;
            // 
            // BottomInfoLB1
            // 
            this.BottomInfoLB1.AutoSize = true;
            this.BottomInfoLB1.ForeColor = System.Drawing.Color.White;
            this.BottomInfoLB1.Location = new System.Drawing.Point(13, 5);
            this.BottomInfoLB1.Name = "BottomInfoLB1";
            this.BottomInfoLB1.Size = new System.Drawing.Size(0, 12);
            this.BottomInfoLB1.TabIndex = 0;
            // 
            // SubMenuParentP
            // 
            this.SubMenuParentP.Controls.Add(this.SubHeaderLB);
            this.SubMenuParentP.Controls.Add(this.SubMenuP);
            this.SubMenuParentP.Controls.Add(this.SubMenuSplitorP);
            this.SubMenuParentP.Dock = System.Windows.Forms.DockStyle.Left;
            this.SubMenuParentP.Location = new System.Drawing.Point(0, 56);
            this.SubMenuParentP.Name = "SubMenuParentP";
            this.SubMenuParentP.Size = new System.Drawing.Size(149, 664);
            this.SubMenuParentP.TabIndex = 2;
            // 
            // SubHeaderLB
            // 
            this.SubHeaderLB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SubHeaderLB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(96)))), ((int)(((byte)(130)))));
            this.SubHeaderLB.ForeColor = System.Drawing.Color.White;
            this.SubHeaderLB.Location = new System.Drawing.Point(0, 5);
            this.SubHeaderLB.Name = "SubHeaderLB";
            this.SubHeaderLB.Size = new System.Drawing.Size(149, 27);
            this.SubHeaderLB.TabIndex = 1;
            this.SubHeaderLB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SubMenuP
            // 
            this.SubMenuP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SubMenuP.BackColor = System.Drawing.SystemColors.Control;
            this.SubMenuP.Location = new System.Drawing.Point(0, 35);
            this.SubMenuP.Name = "SubMenuP";
            this.SubMenuP.Size = new System.Drawing.Size(149, 629);
            this.SubMenuP.TabIndex = 5;
            // 
            // SubMenuSplitorP
            // 
            this.SubMenuSplitorP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SubMenuSplitorP.BackColor = System.Drawing.SystemColors.Info;
            this.SubMenuSplitorP.Location = new System.Drawing.Point(0, 28);
            this.SubMenuSplitorP.Name = "SubMenuSplitorP";
            this.SubMenuSplitorP.Size = new System.Drawing.Size(149, 14);
            this.SubMenuSplitorP.TabIndex = 0;
            // 
            // SubMenuMainSp
            // 
            this.SubMenuMainSp.Location = new System.Drawing.Point(149, 56);
            this.SubMenuMainSp.Name = "SubMenuMainSp";
            this.SubMenuMainSp.Size = new System.Drawing.Size(6, 664);
            this.SubMenuMainSp.TabIndex = 5;
            this.SubMenuMainSp.TabStop = false;
            // 
            // MainParentP
            // 
            this.MainParentP.BackColor = System.Drawing.Color.Transparent;
            this.MainParentP.Controls.Add(this.MainContainerP);
            this.MainParentP.Controls.Add(this.MainSplitorP);
            this.MainParentP.Controls.Add(this.TabContainerP);
            this.MainParentP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainParentP.Location = new System.Drawing.Point(155, 56);
            this.MainParentP.Name = "MainParentP";
            this.MainParentP.Size = new System.Drawing.Size(698, 664);
            this.MainParentP.TabIndex = 4;
            // 
            // MainContainerP
            // 
            this.MainContainerP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MainContainerP.BackColor = System.Drawing.SystemColors.Control;
            this.MainContainerP.Location = new System.Drawing.Point(0, 35);
            this.MainContainerP.Name = "MainContainerP";
            this.MainContainerP.Size = new System.Drawing.Size(698, 629);
            this.MainContainerP.TabIndex = 9;
            // 
            // MainSplitorP
            // 
            this.MainSplitorP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MainSplitorP.BackColor = System.Drawing.SystemColors.Info;
            this.MainSplitorP.Location = new System.Drawing.Point(0, 32);
            this.MainSplitorP.Name = "MainSplitorP";
            this.MainSplitorP.Size = new System.Drawing.Size(698, 10);
            this.MainSplitorP.TabIndex = 10;
            // 
            // TabContainerP
            // 
            this.TabContainerP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TabContainerP.Location = new System.Drawing.Point(0, 4);
            this.TabContainerP.Name = "TabContainerP";
            this.TabContainerP.Size = new System.Drawing.Size(696, 28);
            this.TabContainerP.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.ClientSize = new System.Drawing.Size(853, 742);
            this.Controls.Add(this.MainParentP);
            this.Controls.Add(this.SubMenuMainSp);
            this.Controls.Add(this.SubMenuParentP);
            this.Controls.Add(this.BottomP);
            this.Controls.Add(this.TopMenuP);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XXX管理系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.BottomP.ResumeLayout(false);
            this.BottomP.PerformLayout();
            this.SubMenuParentP.ResumeLayout(false);
            this.MainParentP.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel BottomP;
        private System.Windows.Forms.Panel SubMenuParentP;
        private System.Windows.Forms.Splitter SubMenuMainSp;
        private System.Windows.Forms.Panel MainParentP;
        private System.Windows.Forms.Panel MainSplitorP;
        private System.Windows.Forms.Panel SubMenuSplitorP;
        private System.Windows.Forms.Panel TopMenuP;
        private System.Windows.Forms.Label SubHeaderLB;
        public System.Windows.Forms.Panel TabContainerP;
        public System.Windows.Forms.Panel MainContainerP;
        public System.Windows.Forms.Label BottomInfoLB1;
        private System.Windows.Forms.Panel SubMenuP;





    }
}

