namespace ArcGisView
{
    partial class SearchViewInfo
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
            this.ListView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // ListView1
            // 
            this.ListView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListView1.GridLines = true;
            this.ListView1.Location = new System.Drawing.Point(0, 0);
            this.ListView1.Name = "ListView1";
            this.ListView1.Size = new System.Drawing.Size(292, 425);
            this.ListView1.TabIndex = 3;
            this.ListView1.UseCompatibleStateImageBehavior = false;
            this.ListView1.View = System.Windows.Forms.View.Details;
            // 
            // SearchViewInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 425);
            this.Controls.Add(this.ListView1);
            this.Name = "SearchViewInfo";
            this.Text = "SearchViewInfo";
            this.Load += new System.EventHandler(this.SearchViewInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ListView ListView1;

    }
}