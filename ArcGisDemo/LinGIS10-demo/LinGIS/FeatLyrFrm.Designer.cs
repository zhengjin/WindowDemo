namespace LinGIS
{
    partial class FeatLyrFrm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FeatLyrFrm));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("单一符号");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("要素", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("唯一值");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("种类", new System.Windows.Forms.TreeNode[] {
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("分级颜色");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("分级符号");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("比例符号");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("点密度");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("数量", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("饼状");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("条状");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("堆状");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("图表", new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode11,
            treeNode12});
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("多属性");
            this.panelUniValueSymbol = new System.Windows.Forms.Panel();
            this.btnUniValueDown = new System.Windows.Forms.Button();
            this.btnUniValueUp = new System.Windows.Forms.Button();
            this.btnUniValueColorRamp = new System.Windows.Forms.Button();
            this.btnUniValueAddAllValues = new System.Windows.Forms.Button();
            this.lsvUniqueValue = new System.Windows.Forms.ListView();
            this.columnHeaderValue = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderLabel = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCount = new System.Windows.Forms.ColumnHeader();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cbbUniValueField = new System.Windows.Forms.ComboBox();
            this.panelSingleSymbol = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtSingleSymbolDescription = new System.Windows.Forms.TextBox();
            this.txtSingleSymbolLabel = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnSingleSymbol = new System.Windows.Forms.Button();
            this.trvSymbologyShows = new System.Windows.Forms.TreeView();
            this.lsvFields = new System.Windows.Forms.ListView();
            this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderAlias = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderType = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderLength = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderPrecision = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderScale = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderNumberFormat = new System.Windows.Forms.ColumnHeader();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbMaxScale = new System.Windows.Forms.ComboBox();
            this.cbbMinScale = new System.Windows.Forms.ComboBox();
            this.rbnRange = new System.Windows.Forms.RadioButton();
            this.rbnNone = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLayerDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ckbVisible = new System.Windows.Forms.CheckBox();
            this.txtLayerName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtDataSource = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtExtentButtom = new System.Windows.Forms.TextBox();
            this.txtExtentLeft = new System.Windows.Forms.TextBox();
            this.txtExtentRight = new System.Windows.Forms.TextBox();
            this.txtExtentTop = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSelectionSymbol = new System.Windows.Forms.Button();
            this.btnSelectionColor = new System.Windows.Forms.Button();
            this.rbnSelectionColor = new System.Windows.Forms.RadioButton();
            this.rbnSelectionSymbol = new System.Windows.Forms.RadioButton();
            this.rbnSelectionDefault = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.tabControl1 = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.tpgGeneral = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel4 = new DevComponents.DotNetBar.TabControlPanel();
            this.panelClassBreaksSymbol = new System.Windows.Forms.Panel();
            this.btnClassBreaksColorRamp = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btnClassiFy = new System.Windows.Forms.Button();
            this.lblClassBreaksCount = new System.Windows.Forms.Label();
            this.lblClassBreaksMethod = new System.Windows.Forms.Label();
            this.cbbClassBreaksCount = new System.Windows.Forms.ComboBox();
            this.cbbClassBreaksMethod = new System.Windows.Forms.ComboBox();
            this.lsvClassBreaksSymbol = new System.Windows.Forms.ListView();
            this.columnHeaderClassBreaksLabel = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderClassBreakRange = new System.Windows.Forms.ColumnHeader();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.cbbClassBreakNomalization = new System.Windows.Forms.ComboBox();
            this.cbbClassBreakField = new System.Windows.Forms.ComboBox();
            this.tpgSymbology = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel3 = new DevComponents.DotNetBar.TabControlPanel();
            this.tpgFields = new DevComponents.DotNetBar.TabItem(this.components);
            this.tpgSelection = new DevComponents.DotNetBar.TabControlPanel();
            this.tabItem1 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel2 = new DevComponents.DotNetBar.TabControlPanel();
            this.tpgSource = new DevComponents.DotNetBar.TabItem(this.components);
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnApply = new DevComponents.DotNetBar.ButtonX();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.panelUniValueSymbol.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.panelSingleSymbol.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            this.tabControlPanel4.SuspendLayout();
            this.panelClassBreaksSymbol.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tabControlPanel3.SuspendLayout();
            this.tpgSelection.SuspendLayout();
            this.tabControlPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelUniValueSymbol
            // 
            this.panelUniValueSymbol.BackColor = System.Drawing.Color.Transparent;
            this.panelUniValueSymbol.Controls.Add(this.btnUniValueDown);
            this.panelUniValueSymbol.Controls.Add(this.btnUniValueUp);
            this.panelUniValueSymbol.Controls.Add(this.btnUniValueColorRamp);
            this.panelUniValueSymbol.Controls.Add(this.btnUniValueAddAllValues);
            this.panelUniValueSymbol.Controls.Add(this.lsvUniqueValue);
            this.panelUniValueSymbol.Controls.Add(this.groupBox6);
            this.panelUniValueSymbol.Location = new System.Drawing.Point(57, 271);
            this.panelUniValueSymbol.Name = "panelUniValueSymbol";
            this.panelUniValueSymbol.Size = new System.Drawing.Size(430, 280);
            this.panelUniValueSymbol.TabIndex = 4;
            // 
            // btnUniValueDown
            // 
            this.btnUniValueDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnUniValueDown.Image = ((System.Drawing.Image)(resources.GetObject("btnUniValueDown.Image")));
            this.btnUniValueDown.Location = new System.Drawing.Point(391, 159);
            this.btnUniValueDown.Name = "btnUniValueDown";
            this.btnUniValueDown.Size = new System.Drawing.Size(25, 23);
            this.btnUniValueDown.TabIndex = 4;
            this.btnUniValueDown.UseVisualStyleBackColor = true;
            this.btnUniValueDown.Click += new System.EventHandler(this.btnUniValueDown_Click);
            // 
            // btnUniValueUp
            // 
            this.btnUniValueUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnUniValueUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUniValueUp.Image")));
            this.btnUniValueUp.Location = new System.Drawing.Point(391, 109);
            this.btnUniValueUp.Name = "btnUniValueUp";
            this.btnUniValueUp.Size = new System.Drawing.Size(25, 23);
            this.btnUniValueUp.TabIndex = 3;
            this.btnUniValueUp.UseVisualStyleBackColor = true;
            this.btnUniValueUp.Click += new System.EventHandler(this.btnUniValueUp_Click);
            // 
            // btnUniValueColorRamp
            // 
            this.btnUniValueColorRamp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUniValueColorRamp.Location = new System.Drawing.Point(321, 11);
            this.btnUniValueColorRamp.Name = "btnUniValueColorRamp";
            this.btnUniValueColorRamp.Size = new System.Drawing.Size(80, 60);
            this.btnUniValueColorRamp.TabIndex = 1;
            this.btnUniValueColorRamp.UseVisualStyleBackColor = true;
            this.btnUniValueColorRamp.Click += new System.EventHandler(this.btnUniValueColorRamp_Click);
            // 
            // btnUniValueAddAllValues
            // 
            this.btnUniValueAddAllValues.Location = new System.Drawing.Point(9, 254);
            this.btnUniValueAddAllValues.Name = "btnUniValueAddAllValues";
            this.btnUniValueAddAllValues.Size = new System.Drawing.Size(75, 23);
            this.btnUniValueAddAllValues.TabIndex = 2;
            this.btnUniValueAddAllValues.Text = "添加所有值";
            this.btnUniValueAddAllValues.UseVisualStyleBackColor = true;
            this.btnUniValueAddAllValues.Click += new System.EventHandler(this.btnUniValueAddAllValues_Click);
            // 
            // lsvUniqueValue
            // 
            this.lsvUniqueValue.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderValue,
            this.columnHeaderLabel,
            this.columnHeaderCount});
            this.lsvUniqueValue.FullRowSelect = true;
            this.lsvUniqueValue.GridLines = true;
            this.lsvUniqueValue.HideSelection = false;
            this.lsvUniqueValue.LabelEdit = true;
            this.lsvUniqueValue.Location = new System.Drawing.Point(9, 77);
            this.lsvUniqueValue.MultiSelect = false;
            this.lsvUniqueValue.Name = "lsvUniqueValue";
            this.lsvUniqueValue.Size = new System.Drawing.Size(376, 173);
            this.lsvUniqueValue.TabIndex = 1;
            this.lsvUniqueValue.UseCompatibleStateImageBehavior = false;
            this.lsvUniqueValue.View = System.Windows.Forms.View.Details;
            this.lsvUniqueValue.SelectedIndexChanged += new System.EventHandler(this.lsvUniqueValue_SelectedIndexChanged);
            // 
            // columnHeaderValue
            // 
            this.columnHeaderValue.Text = "值";
            this.columnHeaderValue.Width = 119;
            // 
            // columnHeaderLabel
            // 
            this.columnHeaderLabel.Text = "标签";
            this.columnHeaderLabel.Width = 129;
            // 
            // columnHeaderCount
            // 
            this.columnHeaderCount.Text = "数量";
            this.columnHeaderCount.Width = 74;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.cbbUniValueField);
            this.groupBox6.Location = new System.Drawing.Point(9, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(185, 68);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "字段";
            // 
            // cbbUniValueField
            // 
            this.cbbUniValueField.DropDownHeight = 300;
            this.cbbUniValueField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbUniValueField.FormattingEnabled = true;
            this.cbbUniValueField.IntegralHeight = false;
            this.cbbUniValueField.Location = new System.Drawing.Point(6, 20);
            this.cbbUniValueField.Name = "cbbUniValueField";
            this.cbbUniValueField.Size = new System.Drawing.Size(157, 20);
            this.cbbUniValueField.TabIndex = 0;
            this.cbbUniValueField.SelectedIndexChanged += new System.EventHandler(this.cbbUniValueField_SelectedIndexChanged);
            // 
            // panelSingleSymbol
            // 
            this.panelSingleSymbol.BackColor = System.Drawing.Color.Transparent;
            this.panelSingleSymbol.Controls.Add(this.groupBox5);
            this.panelSingleSymbol.Controls.Add(this.groupBox4);
            this.panelSingleSymbol.Location = new System.Drawing.Point(9, 293);
            this.panelSingleSymbol.Name = "panelSingleSymbol";
            this.panelSingleSymbol.Size = new System.Drawing.Size(430, 280);
            this.panelSingleSymbol.TabIndex = 2;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.txtSingleSymbolDescription);
            this.groupBox5.Controls.Add(this.txtSingleSymbolLabel);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Location = new System.Drawing.Point(4, 148);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(423, 129);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "图例";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(24, 60);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(161, 12);
            this.label14.TabIndex = 5;
            this.label14.Text = "会在图例旁边出现的附加信息";
            // 
            // txtSingleSymbolDescription
            // 
            this.txtSingleSymbolDescription.Location = new System.Drawing.Point(197, 57);
            this.txtSingleSymbolDescription.Multiline = true;
            this.txtSingleSymbolDescription.Name = "txtSingleSymbolDescription";
            this.txtSingleSymbolDescription.Size = new System.Drawing.Size(149, 66);
            this.txtSingleSymbolDescription.TabIndex = 4;
            // 
            // txtSingleSymbolLabel
            // 
            this.txtSingleSymbolLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSingleSymbolLabel.Location = new System.Drawing.Point(197, 30);
            this.txtSingleSymbolLabel.Name = "txtSingleSymbolLabel";
            this.txtSingleSymbolLabel.Size = new System.Drawing.Size(220, 21);
            this.txtSingleSymbolLabel.TabIndex = 2;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(24, 33);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(167, 12);
            this.label13.TabIndex = 3;
            this.label13.Text = "在TOC里出现在符号旁边的标签";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnSingleSymbol);
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(424, 119);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "符号";
            // 
            // btnSingleSymbol
            // 
            this.btnSingleSymbol.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSingleSymbol.Location = new System.Drawing.Point(80, 20);
            this.btnSingleSymbol.Name = "btnSingleSymbol";
            this.btnSingleSymbol.Size = new System.Drawing.Size(120, 90);
            this.btnSingleSymbol.TabIndex = 0;
            this.btnSingleSymbol.UseVisualStyleBackColor = true;
            this.btnSingleSymbol.Click += new System.EventHandler(this.btnSingleSymbol_Click);
            // 
            // trvSymbologyShows
            // 
            this.trvSymbologyShows.HideSelection = false;
            this.trvSymbologyShows.Location = new System.Drawing.Point(4, 4);
            this.trvSymbologyShows.Name = "trvSymbologyShows";
            treeNode1.Checked = true;
            treeNode1.Name = "nodeSingleSymbol";
            treeNode1.Text = "单一符号";
            treeNode2.Name = "nodeFeature";
            treeNode2.Text = "要素";
            treeNode3.Name = "nodeUniqueValues";
            treeNode3.Text = "唯一值";
            treeNode4.Name = "nodeCategories";
            treeNode4.Text = "种类";
            treeNode5.Name = "nodeGraduatedColors";
            treeNode5.Text = "分级颜色";
            treeNode6.Name = "nodeGraduatedSymbols";
            treeNode6.Text = "分级符号";
            treeNode7.Name = "nodeProportionalSymbols";
            treeNode7.Text = "比例符号";
            treeNode8.Name = "nodeDotDensity";
            treeNode8.Text = "点密度";
            treeNode9.Name = "nodeQuantities";
            treeNode9.Text = "数量";
            treeNode10.Name = "nodePie";
            treeNode10.Text = "饼状";
            treeNode11.Name = "nodeBar";
            treeNode11.Text = "条状";
            treeNode12.Name = "nodeStacked";
            treeNode12.Text = "堆状";
            treeNode13.Name = "nodeCharts";
            treeNode13.Text = "图表";
            treeNode14.Name = "nodeMultipleAttributes";
            treeNode14.Text = "多属性";
            this.trvSymbologyShows.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode4,
            treeNode9,
            treeNode13,
            treeNode14});
            this.trvSymbologyShows.Size = new System.Drawing.Size(148, 197);
            this.trvSymbologyShows.TabIndex = 1;
            this.trvSymbologyShows.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvSymbologyShows_AfterSelect);
            // 
            // lsvFields
            // 
            this.lsvFields.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderAlias,
            this.columnHeaderType,
            this.columnHeaderLength,
            this.columnHeaderPrecision,
            this.columnHeaderScale,
            this.columnHeaderNumberFormat});
            this.lsvFields.Location = new System.Drawing.Point(7, 4);
            this.lsvFields.MultiSelect = false;
            this.lsvFields.Name = "lsvFields";
            this.lsvFields.Size = new System.Drawing.Size(597, 302);
            this.lsvFields.TabIndex = 0;
            this.lsvFields.UseCompatibleStateImageBehavior = false;
            this.lsvFields.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "名称";
            this.columnHeaderName.Width = 126;
            // 
            // columnHeaderAlias
            // 
            this.columnHeaderAlias.Text = "别名";
            this.columnHeaderAlias.Width = 107;
            // 
            // columnHeaderType
            // 
            this.columnHeaderType.Text = "类型";
            this.columnHeaderType.Width = 109;
            // 
            // columnHeaderLength
            // 
            this.columnHeaderLength.Text = "长度";
            this.columnHeaderLength.Width = 49;
            // 
            // columnHeaderPrecision
            // 
            this.columnHeaderPrecision.Text = "精度";
            this.columnHeaderPrecision.Width = 45;
            // 
            // columnHeaderScale
            // 
            this.columnHeaderScale.Text = "位数";
            this.columnHeaderScale.Width = 46;
            // 
            // columnHeaderNumberFormat
            // 
            this.columnHeaderNumberFormat.Text = "数字格式";
            this.columnHeaderNumberFormat.Width = 106;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbbMaxScale);
            this.groupBox1.Controls.Add(this.cbbMinScale);
            this.groupBox1.Controls.Add(this.rbnRange);
            this.groupBox1.Controls.Add(this.rbnNone);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(58, 125);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(434, 167);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "比例尺范围";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(306, 58);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(78, 41);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(306, 105);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(78, 40);
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(213, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "(最大比例尺)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(213, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "(最小比例尺)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "小于比例尺";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "大于比例尺";
            // 
            // cbbMaxScale
            // 
            this.cbbMaxScale.FormattingEnabled = true;
            this.cbbMaxScale.Location = new System.Drawing.Point(86, 115);
            this.cbbMaxScale.Name = "cbbMaxScale";
            this.cbbMaxScale.Size = new System.Drawing.Size(121, 20);
            this.cbbMaxScale.TabIndex = 4;
            // 
            // cbbMinScale
            // 
            this.cbbMinScale.FormattingEnabled = true;
            this.cbbMinScale.Location = new System.Drawing.Point(86, 76);
            this.cbbMinScale.Name = "cbbMinScale";
            this.cbbMinScale.Size = new System.Drawing.Size(121, 20);
            this.cbbMinScale.TabIndex = 3;
            // 
            // rbnRange
            // 
            this.rbnRange.AutoSize = true;
            this.rbnRange.Location = new System.Drawing.Point(12, 54);
            this.rbnRange.Name = "rbnRange";
            this.rbnRange.Size = new System.Drawing.Size(179, 16);
            this.rbnRange.TabIndex = 2;
            this.rbnRange.TabStop = true;
            this.rbnRange.Text = "当比例尺为以下范围时不可见";
            this.rbnRange.UseVisualStyleBackColor = true;
            // 
            // rbnNone
            // 
            this.rbnNone.AutoSize = true;
            this.rbnNone.Location = new System.Drawing.Point(12, 32);
            this.rbnNone.Name = "rbnNone";
            this.rbnNone.Size = new System.Drawing.Size(143, 16);
            this.rbnNone.TabIndex = 1;
            this.rbnNone.TabStop = true;
            this.rbnNone.Text = "在任何比例尺下都可见";
            this.rbnNone.UseVisualStyleBackColor = true;
            this.rbnNone.CheckedChanged += new System.EventHandler(this.rbnNone_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(197, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "你可以指定此图层可见的比例尺范围";
            // 
            // txtLayerDescription
            // 
            this.txtLayerDescription.Location = new System.Drawing.Point(115, 47);
            this.txtLayerDescription.Multiline = true;
            this.txtLayerDescription.Name = "txtLayerDescription";
            this.txtLayerDescription.Size = new System.Drawing.Size(377, 72);
            this.txtLayerDescription.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(56, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "图层说明";
            // 
            // ckbVisible
            // 
            this.ckbVisible.AutoSize = true;
            this.ckbVisible.BackColor = System.Drawing.Color.Transparent;
            this.ckbVisible.Location = new System.Drawing.Point(420, 9);
            this.ckbVisible.Name = "ckbVisible";
            this.ckbVisible.Size = new System.Drawing.Size(72, 16);
            this.ckbVisible.TabIndex = 2;
            this.ckbVisible.Text = "是否可见";
            this.ckbVisible.UseVisualStyleBackColor = false;
            // 
            // txtLayerName
            // 
            this.txtLayerName.Location = new System.Drawing.Point(115, 7);
            this.txtLayerName.Name = "txtLayerName";
            this.txtLayerName.Size = new System.Drawing.Size(296, 21);
            this.txtLayerName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(56, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "图层名称";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.txtDataSource);
            this.groupBox3.Location = new System.Drawing.Point(28, 139);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(517, 173);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "数据源";
            // 
            // txtDataSource
            // 
            this.txtDataSource.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtDataSource.Location = new System.Drawing.Point(8, 20);
            this.txtDataSource.Name = "txtDataSource";
            this.txtDataSource.ReadOnly = true;
            this.txtDataSource.Size = new System.Drawing.Size(503, 147);
            this.txtDataSource.TabIndex = 1;
            this.txtDataSource.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.txtExtentButtom);
            this.groupBox2.Controls.Add(this.txtExtentLeft);
            this.groupBox2.Controls.Add(this.txtExtentRight);
            this.groupBox2.Controls.Add(this.txtExtentTop);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(28, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(517, 129);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "范围";
            // 
            // txtExtentButtom
            // 
            this.txtExtentButtom.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtExtentButtom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtExtentButtom.Location = new System.Drawing.Point(159, 95);
            this.txtExtentButtom.Name = "txtExtentButtom";
            this.txtExtentButtom.ReadOnly = true;
            this.txtExtentButtom.Size = new System.Drawing.Size(163, 14);
            this.txtExtentButtom.TabIndex = 7;
            // 
            // txtExtentLeft
            // 
            this.txtExtentLeft.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtExtentLeft.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtExtentLeft.Location = new System.Drawing.Point(29, 52);
            this.txtExtentLeft.Name = "txtExtentLeft";
            this.txtExtentLeft.ReadOnly = true;
            this.txtExtentLeft.Size = new System.Drawing.Size(163, 14);
            this.txtExtentLeft.TabIndex = 6;
            // 
            // txtExtentRight
            // 
            this.txtExtentRight.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtExtentRight.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtExtentRight.Location = new System.Drawing.Point(351, 52);
            this.txtExtentRight.Name = "txtExtentRight";
            this.txtExtentRight.ReadOnly = true;
            this.txtExtentRight.Size = new System.Drawing.Size(163, 14);
            this.txtExtentRight.TabIndex = 5;
            // 
            // txtExtentTop
            // 
            this.txtExtentTop.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtExtentTop.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtExtentTop.Location = new System.Drawing.Point(159, 14);
            this.txtExtentTop.Name = "txtExtentTop";
            this.txtExtentTop.ReadOnly = true;
            this.txtExtentTop.Size = new System.Drawing.Size(163, 14);
            this.txtExtentTop.TabIndex = 4;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(143, 95);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 12);
            this.label11.TabIndex = 3;
            this.label11.Text = "下";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(143, 14);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 2;
            this.label10.Text = "上";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 1;
            this.label9.Text = "左";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(328, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "右";
            // 
            // btnSelectionSymbol
            // 
            this.btnSelectionSymbol.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectionSymbol.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSelectionSymbol.Location = new System.Drawing.Point(93, 100);
            this.btnSelectionSymbol.Name = "btnSelectionSymbol";
            this.btnSelectionSymbol.Size = new System.Drawing.Size(80, 60);
            this.btnSelectionSymbol.TabIndex = 5;
            this.btnSelectionSymbol.UseVisualStyleBackColor = false;
            this.btnSelectionSymbol.Click += new System.EventHandler(this.btnSelectionSymbol_Click);
            // 
            // btnSelectionColor
            // 
            this.btnSelectionColor.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectionColor.Location = new System.Drawing.Point(93, 188);
            this.btnSelectionColor.Name = "btnSelectionColor";
            this.btnSelectionColor.Size = new System.Drawing.Size(80, 60);
            this.btnSelectionColor.TabIndex = 4;
            this.btnSelectionColor.UseVisualStyleBackColor = false;
            this.btnSelectionColor.Click += new System.EventHandler(this.btnSelectionColor_Click);
            // 
            // rbnSelectionColor
            // 
            this.rbnSelectionColor.AutoSize = true;
            this.rbnSelectionColor.BackColor = System.Drawing.Color.Transparent;
            this.rbnSelectionColor.Location = new System.Drawing.Point(73, 166);
            this.rbnSelectionColor.Name = "rbnSelectionColor";
            this.rbnSelectionColor.Size = new System.Drawing.Size(95, 16);
            this.rbnSelectionColor.TabIndex = 3;
            this.rbnSelectionColor.Text = "使用以下颜色";
            this.rbnSelectionColor.UseVisualStyleBackColor = false;
            // 
            // rbnSelectionSymbol
            // 
            this.rbnSelectionSymbol.AutoSize = true;
            this.rbnSelectionSymbol.BackColor = System.Drawing.Color.Transparent;
            this.rbnSelectionSymbol.Location = new System.Drawing.Point(73, 78);
            this.rbnSelectionSymbol.Name = "rbnSelectionSymbol";
            this.rbnSelectionSymbol.Size = new System.Drawing.Size(95, 16);
            this.rbnSelectionSymbol.TabIndex = 2;
            this.rbnSelectionSymbol.Text = "使用以下符号";
            this.rbnSelectionSymbol.UseVisualStyleBackColor = false;
            // 
            // rbnSelectionDefault
            // 
            this.rbnSelectionDefault.AutoSize = true;
            this.rbnSelectionDefault.BackColor = System.Drawing.Color.Transparent;
            this.rbnSelectionDefault.Checked = true;
            this.rbnSelectionDefault.Location = new System.Drawing.Point(73, 56);
            this.rbnSelectionDefault.Name = "rbnSelectionDefault";
            this.rbnSelectionDefault.Size = new System.Drawing.Size(95, 16);
            this.rbnSelectionDefault.TabIndex = 1;
            this.rbnSelectionDefault.TabStop = true;
            this.rbnSelectionDefault.Text = "使用默认符号";
            this.rbnSelectionDefault.UseVisualStyleBackColor = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Location = new System.Drawing.Point(42, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(101, 12);
            this.label12.TabIndex = 0;
            this.label12.Text = "显示选择的要素：";
            // 
            // tabControl1
            // 
            this.tabControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.tabControl1.CanReorderTabs = true;
            this.tabControl1.CloseButtonPosition = DevComponents.DotNetBar.eTabCloseButtonPosition.Left;
            this.tabControl1.Controls.Add(this.tabControlPanel4);
            this.tabControl1.Controls.Add(this.tabControlPanel1);
            this.tabControl1.Controls.Add(this.tabControlPanel3);
            this.tabControl1.Controls.Add(this.tpgSelection);
            this.tabControl1.Controls.Add(this.tabControlPanel2);
            this.tabControl1.Location = new System.Drawing.Point(7, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tabControl1.SelectedTabIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(606, 340);
            this.tabControl1.Style = DevComponents.DotNetBar.eTabStripStyle.OneNote;
            this.tabControl1.TabAlignment = DevComponents.DotNetBar.eTabStripAlignment.Top;
            this.tabControl1.TabIndex = 4;
            this.tabControl1.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabControl1.Tabs.Add(this.tpgGeneral);
            this.tabControl1.Tabs.Add(this.tpgSource);
            this.tabControl1.Tabs.Add(this.tabItem1);
            this.tabControl1.Tabs.Add(this.tpgFields);
            this.tabControl1.Tabs.Add(this.tpgSymbology);
            this.tabControl1.Text = "tabControl1";
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.tabControlPanel1.Controls.Add(this.groupBox1);
            this.tabControlPanel1.Controls.Add(this.txtLayerDescription);
            this.tabControlPanel1.Controls.Add(this.label1);
            this.tabControlPanel1.Controls.Add(this.label2);
            this.tabControlPanel1.Controls.Add(this.txtLayerName);
            this.tabControlPanel1.Controls.Add(this.ckbVisible);
            this.tabControlPanel1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 26);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(606, 314);
            this.tabControlPanel1.Style.Alignment = System.Drawing.StringAlignment.Near;
            this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(156)))), ((int)(((byte)(187)))));
            this.tabControlPanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.Custom;
            this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(233)))), ((int)(((byte)(240)))));
            this.tabControlPanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.Custom;
            this.tabControlPanel1.Style.BackgroundImagePosition = DevComponents.DotNetBar.eBackgroundImagePosition.Stretch;
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.SystemColors.ControlDark;
            this.tabControlPanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.Custom;
            this.tabControlPanel1.Style.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.Style.LineAlignment = System.Drawing.StringAlignment.Center;
            this.tabControlPanel1.Style.TextTrimming = System.Drawing.StringTrimming.EllipsisCharacter;
            this.tabControlPanel1.StyleMouseDown.Alignment = System.Drawing.StringAlignment.Near;
            this.tabControlPanel1.StyleMouseDown.BackgroundImagePosition = DevComponents.DotNetBar.eBackgroundImagePosition.Stretch;
            this.tabControlPanel1.StyleMouseDown.Border = DevComponents.DotNetBar.eBorderType.None;
            this.tabControlPanel1.StyleMouseDown.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.tabControlPanel1.StyleMouseDown.BorderSide = ((DevComponents.DotNetBar.eBorderSide)((((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Top)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tabControlPanel1.StyleMouseDown.LineAlignment = System.Drawing.StringAlignment.Center;
            this.tabControlPanel1.StyleMouseDown.TextTrimming = System.Drawing.StringTrimming.EllipsisCharacter;
            this.tabControlPanel1.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Near;
            this.tabControlPanel1.StyleMouseOver.BackgroundImagePosition = DevComponents.DotNetBar.eBackgroundImagePosition.Stretch;
            this.tabControlPanel1.StyleMouseOver.Border = DevComponents.DotNetBar.eBorderType.None;
            this.tabControlPanel1.StyleMouseOver.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.tabControlPanel1.StyleMouseOver.BorderSide = ((DevComponents.DotNetBar.eBorderSide)((((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Top)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tabControlPanel1.StyleMouseOver.LineAlignment = System.Drawing.StringAlignment.Center;
            this.tabControlPanel1.StyleMouseOver.TextTrimming = System.Drawing.StringTrimming.EllipsisCharacter;
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.tpgGeneral;
            // 
            // tpgGeneral
            // 
            this.tpgGeneral.AttachedControl = this.tabControlPanel1;
            this.tpgGeneral.Name = "tpgGeneral";
            this.tpgGeneral.PredefinedColor = DevComponents.DotNetBar.eTabItemColor.Default;
            this.tpgGeneral.Text = "一般";
            // 
            // tabControlPanel4
            // 
            this.tabControlPanel4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.tabControlPanel4.Controls.Add(this.panelClassBreaksSymbol);
            this.tabControlPanel4.Controls.Add(this.panelUniValueSymbol);
            this.tabControlPanel4.Controls.Add(this.panelSingleSymbol);
            this.tabControlPanel4.Controls.Add(this.trvSymbologyShows);
            this.tabControlPanel4.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tabControlPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel4.Location = new System.Drawing.Point(0, 26);
            this.tabControlPanel4.Name = "tabControlPanel4";
            this.tabControlPanel4.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel4.Size = new System.Drawing.Size(606, 314);
            this.tabControlPanel4.Style.Alignment = System.Drawing.StringAlignment.Near;
            this.tabControlPanel4.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(156)))), ((int)(((byte)(187)))));
            this.tabControlPanel4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.Custom;
            this.tabControlPanel4.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(233)))), ((int)(((byte)(240)))));
            this.tabControlPanel4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.Custom;
            this.tabControlPanel4.Style.BackgroundImagePosition = DevComponents.DotNetBar.eBackgroundImagePosition.Stretch;
            this.tabControlPanel4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel4.Style.BorderColor.Color = System.Drawing.SystemColors.ControlDark;
            this.tabControlPanel4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.Custom;
            this.tabControlPanel4.Style.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.tabControlPanel4.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel4.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tabControlPanel4.Style.GradientAngle = 90;
            this.tabControlPanel4.Style.LineAlignment = System.Drawing.StringAlignment.Center;
            this.tabControlPanel4.Style.TextTrimming = System.Drawing.StringTrimming.EllipsisCharacter;
            this.tabControlPanel4.StyleMouseDown.Alignment = System.Drawing.StringAlignment.Near;
            this.tabControlPanel4.StyleMouseDown.BackgroundImagePosition = DevComponents.DotNetBar.eBackgroundImagePosition.Stretch;
            this.tabControlPanel4.StyleMouseDown.Border = DevComponents.DotNetBar.eBorderType.None;
            this.tabControlPanel4.StyleMouseDown.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.tabControlPanel4.StyleMouseDown.BorderSide = ((DevComponents.DotNetBar.eBorderSide)((((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Top)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel4.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tabControlPanel4.StyleMouseDown.LineAlignment = System.Drawing.StringAlignment.Center;
            this.tabControlPanel4.StyleMouseDown.TextTrimming = System.Drawing.StringTrimming.EllipsisCharacter;
            this.tabControlPanel4.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Near;
            this.tabControlPanel4.StyleMouseOver.BackgroundImagePosition = DevComponents.DotNetBar.eBackgroundImagePosition.Stretch;
            this.tabControlPanel4.StyleMouseOver.Border = DevComponents.DotNetBar.eBorderType.None;
            this.tabControlPanel4.StyleMouseOver.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.tabControlPanel4.StyleMouseOver.BorderSide = ((DevComponents.DotNetBar.eBorderSide)((((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Top)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel4.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tabControlPanel4.StyleMouseOver.LineAlignment = System.Drawing.StringAlignment.Center;
            this.tabControlPanel4.StyleMouseOver.TextTrimming = System.Drawing.StringTrimming.EllipsisCharacter;
            this.tabControlPanel4.TabIndex = 5;
            this.tabControlPanel4.TabItem = this.tpgSymbology;
            // 
            // panelClassBreaksSymbol
            // 
            this.panelClassBreaksSymbol.BackColor = System.Drawing.Color.Transparent;
            this.panelClassBreaksSymbol.Controls.Add(this.btnClassBreaksColorRamp);
            this.panelClassBreaksSymbol.Controls.Add(this.groupBox8);
            this.panelClassBreaksSymbol.Controls.Add(this.lsvClassBreaksSymbol);
            this.panelClassBreaksSymbol.Controls.Add(this.groupBox7);
            this.panelClassBreaksSymbol.Location = new System.Drawing.Point(158, 7);
            this.panelClassBreaksSymbol.Name = "panelClassBreaksSymbol";
            this.panelClassBreaksSymbol.Size = new System.Drawing.Size(433, 280);
            this.panelClassBreaksSymbol.TabIndex = 5;
            // 
            // btnClassBreaksColorRamp
            // 
            this.btnClassBreaksColorRamp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClassBreaksColorRamp.Location = new System.Drawing.Point(98, 82);
            this.btnClassBreaksColorRamp.Name = "btnClassBreaksColorRamp";
            this.btnClassBreaksColorRamp.Size = new System.Drawing.Size(117, 19);
            this.btnClassBreaksColorRamp.TabIndex = 3;
            this.btnClassBreaksColorRamp.UseVisualStyleBackColor = true;
            this.btnClassBreaksColorRamp.Click += new System.EventHandler(this.btnClassBreaksColorRamp_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btnClassiFy);
            this.groupBox8.Controls.Add(this.lblClassBreaksCount);
            this.groupBox8.Controls.Add(this.lblClassBreaksMethod);
            this.groupBox8.Controls.Add(this.cbbClassBreaksCount);
            this.groupBox8.Controls.Add(this.cbbClassBreaksMethod);
            this.groupBox8.Location = new System.Drawing.Point(222, 4);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(208, 97);
            this.groupBox8.TabIndex = 2;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "分级";
            // 
            // btnClassiFy
            // 
            this.btnClassiFy.Location = new System.Drawing.Point(128, 72);
            this.btnClassiFy.Name = "btnClassiFy";
            this.btnClassiFy.Size = new System.Drawing.Size(74, 19);
            this.btnClassiFy.TabIndex = 8;
            this.btnClassiFy.Text = "分级";
            this.btnClassiFy.UseVisualStyleBackColor = true;
            this.btnClassiFy.Click += new System.EventHandler(this.btnClassiFy_Click);
            // 
            // lblClassBreaksCount
            // 
            this.lblClassBreaksCount.AutoSize = true;
            this.lblClassBreaksCount.Enabled = false;
            this.lblClassBreaksCount.Location = new System.Drawing.Point(9, 49);
            this.lblClassBreaksCount.Name = "lblClassBreaksCount";
            this.lblClassBreaksCount.Size = new System.Drawing.Size(53, 12);
            this.lblClassBreaksCount.TabIndex = 7;
            this.lblClassBreaksCount.Text = "分级数目";
            // 
            // lblClassBreaksMethod
            // 
            this.lblClassBreaksMethod.AutoSize = true;
            this.lblClassBreaksMethod.Enabled = false;
            this.lblClassBreaksMethod.Location = new System.Drawing.Point(8, 24);
            this.lblClassBreaksMethod.Name = "lblClassBreaksMethod";
            this.lblClassBreaksMethod.Size = new System.Drawing.Size(53, 12);
            this.lblClassBreaksMethod.TabIndex = 6;
            this.lblClassBreaksMethod.Text = "分级方法";
            // 
            // cbbClassBreaksCount
            // 
            this.cbbClassBreaksCount.DropDownHeight = 300;
            this.cbbClassBreaksCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbClassBreaksCount.DropDownWidth = 137;
            this.cbbClassBreaksCount.Enabled = false;
            this.cbbClassBreaksCount.FormattingEnabled = true;
            this.cbbClassBreaksCount.IntegralHeight = false;
            this.cbbClassBreaksCount.Location = new System.Drawing.Point(74, 46);
            this.cbbClassBreaksCount.Name = "cbbClassBreaksCount";
            this.cbbClassBreaksCount.Size = new System.Drawing.Size(128, 20);
            this.cbbClassBreaksCount.TabIndex = 5;
            // 
            // cbbClassBreaksMethod
            // 
            this.cbbClassBreaksMethod.DropDownHeight = 300;
            this.cbbClassBreaksMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbClassBreaksMethod.DropDownWidth = 137;
            this.cbbClassBreaksMethod.Enabled = false;
            this.cbbClassBreaksMethod.FormattingEnabled = true;
            this.cbbClassBreaksMethod.IntegralHeight = false;
            this.cbbClassBreaksMethod.Items.AddRange(new object[] {
            "EqualInterval",
            "GeometricalInterval",
            "NaturalBreaks",
            "Quantile"});
            this.cbbClassBreaksMethod.Location = new System.Drawing.Point(74, 20);
            this.cbbClassBreaksMethod.Name = "cbbClassBreaksMethod";
            this.cbbClassBreaksMethod.Size = new System.Drawing.Size(128, 20);
            this.cbbClassBreaksMethod.TabIndex = 4;
            // 
            // lsvClassBreaksSymbol
            // 
            this.lsvClassBreaksSymbol.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderClassBreaksLabel,
            this.columnHeaderClassBreakRange});
            this.lsvClassBreaksSymbol.LabelEdit = true;
            this.lsvClassBreaksSymbol.Location = new System.Drawing.Point(4, 107);
            this.lsvClassBreaksSymbol.Name = "lsvClassBreaksSymbol";
            this.lsvClassBreaksSymbol.Size = new System.Drawing.Size(423, 170);
            this.lsvClassBreaksSymbol.TabIndex = 1;
            this.lsvClassBreaksSymbol.UseCompatibleStateImageBehavior = false;
            this.lsvClassBreaksSymbol.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderClassBreaksLabel
            // 
            this.columnHeaderClassBreaksLabel.Text = "标签";
            this.columnHeaderClassBreaksLabel.Width = 203;
            // 
            // columnHeaderClassBreakRange
            // 
            this.columnHeaderClassBreakRange.Text = "范围";
            this.columnHeaderClassBreakRange.Width = 215;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label16);
            this.groupBox7.Controls.Add(this.label15);
            this.groupBox7.Controls.Add(this.cbbClassBreakNomalization);
            this.groupBox7.Controls.Add(this.cbbClassBreakField);
            this.groupBox7.Location = new System.Drawing.Point(4, 4);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(211, 75);
            this.groupBox7.TabIndex = 0;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "字段";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 49);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 12);
            this.label16.TabIndex = 3;
            this.label16.Text = "标准化字段";
            this.label16.Visible = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(11, 24);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 2;
            this.label15.Text = "分级字段";
            // 
            // cbbClassBreakNomalization
            // 
            this.cbbClassBreakNomalization.DropDownHeight = 300;
            this.cbbClassBreakNomalization.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbClassBreakNomalization.DropDownWidth = 137;
            this.cbbClassBreakNomalization.FormattingEnabled = true;
            this.cbbClassBreakNomalization.IntegralHeight = false;
            this.cbbClassBreakNomalization.Items.AddRange(new object[] {
            "none"});
            this.cbbClassBreakNomalization.Location = new System.Drawing.Point(77, 46);
            this.cbbClassBreakNomalization.Name = "cbbClassBreakNomalization";
            this.cbbClassBreakNomalization.Size = new System.Drawing.Size(128, 20);
            this.cbbClassBreakNomalization.TabIndex = 1;
            this.cbbClassBreakNomalization.Visible = false;
            // 
            // cbbClassBreakField
            // 
            this.cbbClassBreakField.DropDownHeight = 300;
            this.cbbClassBreakField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbClassBreakField.DropDownWidth = 137;
            this.cbbClassBreakField.FormattingEnabled = true;
            this.cbbClassBreakField.IntegralHeight = false;
            this.cbbClassBreakField.Items.AddRange(new object[] {
            "none"});
            this.cbbClassBreakField.Location = new System.Drawing.Point(77, 20);
            this.cbbClassBreakField.Name = "cbbClassBreakField";
            this.cbbClassBreakField.Size = new System.Drawing.Size(128, 20);
            this.cbbClassBreakField.TabIndex = 0;
            this.cbbClassBreakField.SelectedIndexChanged += new System.EventHandler(this.cbbClassBreakField_SelectedIndexChanged);
            // 
            // tpgSymbology
            // 
            this.tpgSymbology.AttachedControl = this.tabControlPanel4;
            this.tpgSymbology.Name = "tpgSymbology";
            this.tpgSymbology.PredefinedColor = DevComponents.DotNetBar.eTabItemColor.Default;
            this.tpgSymbology.Text = "符号";
            // 
            // tabControlPanel3
            // 
            this.tabControlPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.tabControlPanel3.Controls.Add(this.lsvFields);
            this.tabControlPanel3.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tabControlPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel3.Location = new System.Drawing.Point(0, 26);
            this.tabControlPanel3.Name = "tabControlPanel3";
            this.tabControlPanel3.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel3.Size = new System.Drawing.Size(606, 314);
            this.tabControlPanel3.Style.Alignment = System.Drawing.StringAlignment.Near;
            this.tabControlPanel3.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(156)))), ((int)(((byte)(187)))));
            this.tabControlPanel3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.Custom;
            this.tabControlPanel3.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(233)))), ((int)(((byte)(240)))));
            this.tabControlPanel3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.Custom;
            this.tabControlPanel3.Style.BackgroundImagePosition = DevComponents.DotNetBar.eBackgroundImagePosition.Stretch;
            this.tabControlPanel3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel3.Style.BorderColor.Color = System.Drawing.SystemColors.ControlDark;
            this.tabControlPanel3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.Custom;
            this.tabControlPanel3.Style.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.tabControlPanel3.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tabControlPanel3.Style.GradientAngle = 90;
            this.tabControlPanel3.Style.LineAlignment = System.Drawing.StringAlignment.Center;
            this.tabControlPanel3.Style.TextTrimming = System.Drawing.StringTrimming.EllipsisCharacter;
            this.tabControlPanel3.StyleMouseDown.Alignment = System.Drawing.StringAlignment.Near;
            this.tabControlPanel3.StyleMouseDown.BackgroundImagePosition = DevComponents.DotNetBar.eBackgroundImagePosition.Stretch;
            this.tabControlPanel3.StyleMouseDown.Border = DevComponents.DotNetBar.eBorderType.None;
            this.tabControlPanel3.StyleMouseDown.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.tabControlPanel3.StyleMouseDown.BorderSide = ((DevComponents.DotNetBar.eBorderSide)((((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Top)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel3.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tabControlPanel3.StyleMouseDown.LineAlignment = System.Drawing.StringAlignment.Center;
            this.tabControlPanel3.StyleMouseDown.TextTrimming = System.Drawing.StringTrimming.EllipsisCharacter;
            this.tabControlPanel3.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Near;
            this.tabControlPanel3.StyleMouseOver.BackgroundImagePosition = DevComponents.DotNetBar.eBackgroundImagePosition.Stretch;
            this.tabControlPanel3.StyleMouseOver.Border = DevComponents.DotNetBar.eBorderType.None;
            this.tabControlPanel3.StyleMouseOver.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.tabControlPanel3.StyleMouseOver.BorderSide = ((DevComponents.DotNetBar.eBorderSide)((((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Top)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel3.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tabControlPanel3.StyleMouseOver.LineAlignment = System.Drawing.StringAlignment.Center;
            this.tabControlPanel3.StyleMouseOver.TextTrimming = System.Drawing.StringTrimming.EllipsisCharacter;
            this.tabControlPanel3.TabIndex = 4;
            this.tabControlPanel3.TabItem = this.tpgFields;
            // 
            // tpgFields
            // 
            this.tpgFields.AttachedControl = this.tabControlPanel3;
            this.tpgFields.Name = "tpgFields";
            this.tpgFields.PredefinedColor = DevComponents.DotNetBar.eTabItemColor.Default;
            this.tpgFields.Text = "字段";
            // 
            // tpgSelection
            // 
            this.tpgSelection.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.tpgSelection.Controls.Add(this.btnSelectionSymbol);
            this.tpgSelection.Controls.Add(this.btnSelectionColor);
            this.tpgSelection.Controls.Add(this.label12);
            this.tpgSelection.Controls.Add(this.rbnSelectionColor);
            this.tpgSelection.Controls.Add(this.rbnSelectionDefault);
            this.tpgSelection.Controls.Add(this.rbnSelectionSymbol);
            this.tpgSelection.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tpgSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpgSelection.Location = new System.Drawing.Point(0, 26);
            this.tpgSelection.Name = "tpgSelection";
            this.tpgSelection.Padding = new System.Windows.Forms.Padding(1);
            this.tpgSelection.Size = new System.Drawing.Size(606, 314);
            this.tpgSelection.Style.Alignment = System.Drawing.StringAlignment.Near;
            this.tpgSelection.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(156)))), ((int)(((byte)(187)))));
            this.tpgSelection.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.Custom;
            this.tpgSelection.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(233)))), ((int)(((byte)(240)))));
            this.tpgSelection.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.Custom;
            this.tpgSelection.Style.BackgroundImagePosition = DevComponents.DotNetBar.eBackgroundImagePosition.Stretch;
            this.tpgSelection.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tpgSelection.Style.BorderColor.Color = System.Drawing.SystemColors.ControlDark;
            this.tpgSelection.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.Custom;
            this.tpgSelection.Style.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.tpgSelection.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tpgSelection.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tpgSelection.Style.GradientAngle = 90;
            this.tpgSelection.Style.LineAlignment = System.Drawing.StringAlignment.Center;
            this.tpgSelection.Style.TextTrimming = System.Drawing.StringTrimming.EllipsisCharacter;
            this.tpgSelection.StyleMouseDown.Alignment = System.Drawing.StringAlignment.Near;
            this.tpgSelection.StyleMouseDown.BackgroundImagePosition = DevComponents.DotNetBar.eBackgroundImagePosition.Stretch;
            this.tpgSelection.StyleMouseDown.Border = DevComponents.DotNetBar.eBorderType.None;
            this.tpgSelection.StyleMouseDown.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.tpgSelection.StyleMouseDown.BorderSide = ((DevComponents.DotNetBar.eBorderSide)((((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Top)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tpgSelection.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tpgSelection.StyleMouseDown.LineAlignment = System.Drawing.StringAlignment.Center;
            this.tpgSelection.StyleMouseDown.TextTrimming = System.Drawing.StringTrimming.EllipsisCharacter;
            this.tpgSelection.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Near;
            this.tpgSelection.StyleMouseOver.BackgroundImagePosition = DevComponents.DotNetBar.eBackgroundImagePosition.Stretch;
            this.tpgSelection.StyleMouseOver.Border = DevComponents.DotNetBar.eBorderType.None;
            this.tpgSelection.StyleMouseOver.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.tpgSelection.StyleMouseOver.BorderSide = ((DevComponents.DotNetBar.eBorderSide)((((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Top)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tpgSelection.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tpgSelection.StyleMouseOver.LineAlignment = System.Drawing.StringAlignment.Center;
            this.tpgSelection.StyleMouseOver.TextTrimming = System.Drawing.StringTrimming.EllipsisCharacter;
            this.tpgSelection.TabIndex = 3;
            this.tpgSelection.TabItem = this.tabItem1;
            // 
            // tabItem1
            // 
            this.tabItem1.AttachedControl = this.tpgSelection;
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.PredefinedColor = DevComponents.DotNetBar.eTabItemColor.Default;
            this.tabItem1.Text = "选择";
            // 
            // tabControlPanel2
            // 
            this.tabControlPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.tabControlPanel2.Controls.Add(this.groupBox3);
            this.tabControlPanel2.Controls.Add(this.groupBox2);
            this.tabControlPanel2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel2.Location = new System.Drawing.Point(0, 26);
            this.tabControlPanel2.Name = "tabControlPanel2";
            this.tabControlPanel2.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel2.Size = new System.Drawing.Size(606, 314);
            this.tabControlPanel2.Style.Alignment = System.Drawing.StringAlignment.Near;
            this.tabControlPanel2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(156)))), ((int)(((byte)(187)))));
            this.tabControlPanel2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.Custom;
            this.tabControlPanel2.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(233)))), ((int)(((byte)(240)))));
            this.tabControlPanel2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.Custom;
            this.tabControlPanel2.Style.BackgroundImagePosition = DevComponents.DotNetBar.eBackgroundImagePosition.Stretch;
            this.tabControlPanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel2.Style.BorderColor.Color = System.Drawing.SystemColors.ControlDark;
            this.tabControlPanel2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.Custom;
            this.tabControlPanel2.Style.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.tabControlPanel2.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tabControlPanel2.Style.GradientAngle = 90;
            this.tabControlPanel2.Style.LineAlignment = System.Drawing.StringAlignment.Center;
            this.tabControlPanel2.Style.TextTrimming = System.Drawing.StringTrimming.EllipsisCharacter;
            this.tabControlPanel2.StyleMouseDown.Alignment = System.Drawing.StringAlignment.Near;
            this.tabControlPanel2.StyleMouseDown.BackgroundImagePosition = DevComponents.DotNetBar.eBackgroundImagePosition.Stretch;
            this.tabControlPanel2.StyleMouseDown.Border = DevComponents.DotNetBar.eBorderType.None;
            this.tabControlPanel2.StyleMouseDown.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.tabControlPanel2.StyleMouseDown.BorderSide = ((DevComponents.DotNetBar.eBorderSide)((((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Top)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tabControlPanel2.StyleMouseDown.LineAlignment = System.Drawing.StringAlignment.Center;
            this.tabControlPanel2.StyleMouseDown.TextTrimming = System.Drawing.StringTrimming.EllipsisCharacter;
            this.tabControlPanel2.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Near;
            this.tabControlPanel2.StyleMouseOver.BackgroundImagePosition = DevComponents.DotNetBar.eBackgroundImagePosition.Stretch;
            this.tabControlPanel2.StyleMouseOver.Border = DevComponents.DotNetBar.eBorderType.None;
            this.tabControlPanel2.StyleMouseOver.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.tabControlPanel2.StyleMouseOver.BorderSide = ((DevComponents.DotNetBar.eBorderSide)((((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Top)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tabControlPanel2.StyleMouseOver.LineAlignment = System.Drawing.StringAlignment.Center;
            this.tabControlPanel2.StyleMouseOver.TextTrimming = System.Drawing.StringTrimming.EllipsisCharacter;
            this.tabControlPanel2.TabIndex = 2;
            this.tabControlPanel2.TabItem = this.tpgSource;
            // 
            // tpgSource
            // 
            this.tpgSource.AttachedControl = this.tabControlPanel2;
            this.tpgSource.Name = "tpgSource";
            this.tpgSource.PredefinedColor = DevComponents.DotNetBar.eTabItemColor.Default;
            this.tpgSource.Text = "源";
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCancel.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.Default;
            this.btnCancel.ImagePosition = DevComponents.DotNetBar.eImagePosition.Left;
            this.btnCancel.Location = new System.Drawing.Point(452, 358);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PopupSide = DevComponents.DotNetBar.ePopupSide.Default;
            this.btnCancel.RenderMode = DevComponents.DotNetBar.eRenderMode.Global;
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Center;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnApply.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.btnApply.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnApply.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnApply.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.Default;
            this.btnApply.ImagePosition = DevComponents.DotNetBar.eImagePosition.Left;
            this.btnApply.Location = new System.Drawing.Point(538, 358);
            this.btnApply.Name = "btnApply";
            this.btnApply.PopupSide = DevComponents.DotNetBar.ePopupSide.Default;
            this.btnApply.RenderMode = DevComponents.DotNetBar.eRenderMode.Global;
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.btnApply.TabIndex = 6;
            this.btnApply.Text = "应用";
            this.btnApply.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Center;
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.Default;
            this.btnOK.ImagePosition = DevComponents.DotNetBar.eImagePosition.Left;
            this.btnOK.Location = new System.Drawing.Point(368, 358);
            this.btnOK.Name = "btnOK";
            this.btnOK.PopupSide = DevComponents.DotNetBar.ePopupSide.Default;
            this.btnOK.RenderMode = DevComponents.DotNetBar.eRenderMode.Global;
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "确定";
            this.btnOK.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Center;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FeatLyrFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 392);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FeatLyrFrm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "图层属性";
            this.Load += new System.EventHandler(this.FeatLyrFrm_Load);
            this.Shown += new System.EventHandler(this.FeatLyrFrm_Shown);
            this.panelUniValueSymbol.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.panelSingleSymbol.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabControlPanel1.ResumeLayout(false);
            this.tabControlPanel1.PerformLayout();
            this.tabControlPanel4.ResumeLayout(false);
            this.panelClassBreaksSymbol.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.tabControlPanel3.ResumeLayout(false);
            this.tpgSelection.ResumeLayout(false);
            this.tpgSelection.PerformLayout();
            this.tabControlPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtLayerDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox ckbVisible;
        private System.Windows.Forms.TextBox txtLayerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbnRange;
        private System.Windows.Forms.RadioButton rbnNone;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbbMaxScale;
        private System.Windows.Forms.ComboBox cbbMinScale;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtExtentButtom;
        private System.Windows.Forms.TextBox txtExtentLeft;
        private System.Windows.Forms.TextBox txtExtentRight;
        private System.Windows.Forms.TextBox txtExtentTop;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox txtDataSource;
        private System.Windows.Forms.Button btnSelectionSymbol;
        private System.Windows.Forms.Button btnSelectionColor;
        private System.Windows.Forms.RadioButton rbnSelectionColor;
        private System.Windows.Forms.RadioButton rbnSelectionSymbol;
        private System.Windows.Forms.RadioButton rbnSelectionDefault;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.TreeView trvSymbologyShows;
        private System.Windows.Forms.Panel panelSingleSymbol;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnSingleSymbol;
        private System.Windows.Forms.TextBox txtSingleSymbolLabel;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtSingleSymbolDescription;
        private System.Windows.Forms.ListView lsvFields;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderAlias;
        private System.Windows.Forms.ColumnHeader columnHeaderType;
        private System.Windows.Forms.ColumnHeader columnHeaderLength;
        private System.Windows.Forms.ColumnHeader columnHeaderPrecision;
        private System.Windows.Forms.ColumnHeader columnHeaderScale;
        private System.Windows.Forms.ColumnHeader columnHeaderNumberFormat;
        private System.Windows.Forms.Panel panelUniValueSymbol;
        private System.Windows.Forms.Button btnUniValueColorRamp;
        private System.Windows.Forms.Button btnUniValueAddAllValues;
        private System.Windows.Forms.ListView lsvUniqueValue;
        private System.Windows.Forms.ColumnHeader columnHeaderValue;
        private System.Windows.Forms.ColumnHeader columnHeaderLabel;
        private System.Windows.Forms.ColumnHeader columnHeaderCount;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox cbbUniValueField;
        private System.Windows.Forms.Button btnUniValueUp;
        private System.Windows.Forms.Button btnUniValueDown;
        private DevComponents.DotNetBar.TabControl tabControl1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private DevComponents.DotNetBar.TabItem tpgGeneral;
        private DevComponents.DotNetBar.TabControlPanel tpgSelection;
        private DevComponents.DotNetBar.TabItem tabItem1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel2;
        private DevComponents.DotNetBar.TabItem tpgSource;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel3;
        private DevComponents.DotNetBar.TabItem tpgFields;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel4;
        private DevComponents.DotNetBar.TabItem tpgSymbology;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnApply;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private System.Windows.Forms.Panel panelClassBreaksSymbol;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cbbClassBreakNomalization;
        private System.Windows.Forms.ComboBox cbbClassBreakField;
        private System.Windows.Forms.ListView lsvClassBreaksSymbol;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label lblClassBreaksCount;
        private System.Windows.Forms.Label lblClassBreaksMethod;
        private System.Windows.Forms.ComboBox cbbClassBreaksCount;
        private System.Windows.Forms.ComboBox cbbClassBreaksMethod;
        private System.Windows.Forms.Button btnClassiFy;
        private System.Windows.Forms.ColumnHeader columnHeaderClassBreaksLabel;
        private System.Windows.Forms.ColumnHeader columnHeaderClassBreakRange;
        private System.Windows.Forms.Button btnClassBreaksColorRamp;
    }
}