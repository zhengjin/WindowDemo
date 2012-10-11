namespace ArcGisView
{
    partial class IdentifyDialog
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.InfoDisplay = new System.Windows.Forms.StatusStrip();
            this.lblFeatureCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.IdentifyProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.cboLayerFilter = new System.Windows.Forms.ComboBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblLayers = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.trvDataList = new System.Windows.Forms.TreeView();
            this.attributePanel = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lstProperties = new System.Windows.Forms.ListView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtCoordinate = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblHitPointHeader = new System.Windows.Forms.Label();
            this.InfoDisplay.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel2.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.attributePanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // InfoDisplay
            // 
            this.InfoDisplay.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblFeatureCount,
            this.IdentifyProgress});
            this.InfoDisplay.Location = new System.Drawing.Point(0, 341);
            this.InfoDisplay.Name = "InfoDisplay";
            this.InfoDisplay.Size = new System.Drawing.Size(528, 22);
            this.InfoDisplay.TabIndex = 3;
            this.InfoDisplay.Text = "信息显示";
            // 
            // lblFeatureCount
            // 
            this.lblFeatureCount.AutoSize = false;
            this.lblFeatureCount.BorderStyle = System.Windows.Forms.Border3DStyle.Adjust;
            this.lblFeatureCount.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblFeatureCount.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFeatureCount.Name = "lblFeatureCount";
            this.lblFeatureCount.Size = new System.Drawing.Size(160, 17);
            this.lblFeatureCount.Text = "查询到 0 条记录";
            this.lblFeatureCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // IdentifyProgress
            // 
            this.IdentifyProgress.Name = "IdentifyProgress";
            this.IdentifyProgress.Size = new System.Drawing.Size(200, 16);
            this.IdentifyProgress.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(528, 28);
            this.panel1.TabIndex = 4;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.cboLayerFilter);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(74, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(454, 28);
            this.panel7.TabIndex = 1;
            // 
            // cboLayerFilter
            // 
            this.cboLayerFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboLayerFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLayerFilter.FormattingEnabled = true;
            this.cboLayerFilter.ItemHeight = 12;
            this.cboLayerFilter.Location = new System.Drawing.Point(0, 4);
            this.cboLayerFilter.Name = "cboLayerFilter";
            this.cboLayerFilter.Size = new System.Drawing.Size(454, 20);
            this.cboLayerFilter.TabIndex = 5;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.lblLayers);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(74, 28);
            this.panel6.TabIndex = 0;
            // 
            // lblLayers
            // 
            this.lblLayers.AutoSize = true;
            this.lblLayers.Location = new System.Drawing.Point(7, 9);
            this.lblLayers.Name = "lblLayers";
            this.lblLayers.Size = new System.Drawing.Size(65, 12);
            this.lblLayers.TabIndex = 4;
            this.lblLayers.Text = "查询图层：";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.splitContainer);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 28);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(528, 313);
            this.panel2.TabIndex = 5;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.trvDataList);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.attributePanel);
            this.splitContainer.Panel2.Controls.Add(this.panel3);
            this.splitContainer.Size = new System.Drawing.Size(528, 313);
            this.splitContainer.SplitterDistance = 179;
            this.splitContainer.TabIndex = 5;
            // 
            // trvDataList
            // 
            this.trvDataList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvDataList.Location = new System.Drawing.Point(0, 0);
            this.trvDataList.Name = "trvDataList";
            this.trvDataList.Size = new System.Drawing.Size(179, 313);
            this.trvDataList.TabIndex = 0;
            this.trvDataList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.OnNodeMouseClick);
            // 
            // attributePanel
            // 
            this.attributePanel.Controls.Add(this.button2);
            this.attributePanel.Controls.Add(this.button1);
            this.attributePanel.Controls.Add(this.lstProperties);
            this.attributePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.attributePanel.Location = new System.Drawing.Point(0, 23);
            this.attributePanel.Name = "attributePanel";
            this.attributePanel.Size = new System.Drawing.Size(345, 290);
            this.attributePanel.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(182, 251);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "删除";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(35, 251);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "修改";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lstProperties
            // 
            this.lstProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstProperties.FullRowSelect = true;
            this.lstProperties.GridLines = true;
            this.lstProperties.HideSelection = false;
            this.lstProperties.Location = new System.Drawing.Point(0, 2);
            this.lstProperties.Name = "lstProperties";
            this.lstProperties.Size = new System.Drawing.Size(345, 243);
            this.lstProperties.TabIndex = 0;
            this.lstProperties.UseCompatibleStateImageBehavior = false;
            this.lstProperties.View = System.Windows.Forms.View.Details;
            this.lstProperties.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstProperties_ColumnClick);
            this.lstProperties.Resize += new System.EventHandler(this.lstProperties_Resize);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(345, 23);
            this.panel3.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtCoordinate);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(68, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(277, 23);
            this.panel4.TabIndex = 1;
            // 
            // txtCoordinate
            // 
            this.txtCoordinate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCoordinate.Location = new System.Drawing.Point(0, 0);
            this.txtCoordinate.Name = "txtCoordinate";
            this.txtCoordinate.ReadOnly = true;
            this.txtCoordinate.Size = new System.Drawing.Size(277, 21);
            this.txtCoordinate.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.lblHitPointHeader);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(68, 23);
            this.panel5.TabIndex = 0;
            // 
            // lblHitPointHeader
            // 
            this.lblHitPointHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHitPointHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHitPointHeader.Name = "lblHitPointHeader";
            this.lblHitPointHeader.Size = new System.Drawing.Size(68, 23);
            this.lblHitPointHeader.TabIndex = 3;
            this.lblHitPointHeader.Text = "点击位置：";
            this.lblHitPointHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // IdentifyDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 363);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.InfoDisplay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IdentifyDialog";
            this.Text = "属性查询";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.IdentifyDialog_FormClosed);
            this.Load += new System.EventHandler(this.IdentifyDialog_Load);
            this.InfoDisplay.ResumeLayout(false);
            this.InfoDisplay.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.attributePanel.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip InfoDisplay;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblLayers;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TreeView trvDataList;
        private System.Windows.Forms.Panel attributePanel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblHitPointHeader;
        private System.Windows.Forms.ComboBox cboLayerFilter;
        private System.Windows.Forms.ToolStripStatusLabel lblFeatureCount;
        private System.Windows.Forms.ToolStripProgressBar IdentifyProgress;
        private System.Windows.Forms.TextBox txtCoordinate;
        private System.Windows.Forms.ListView lstProperties;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}
