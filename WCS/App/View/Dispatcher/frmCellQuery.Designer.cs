﻿namespace App.View.Dispatcher
{
    partial class frmCellQuery
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.bsMain = new System.Windows.Forms.BindingSource(this.components);
            this.SHELFNAME = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlData = new System.Windows.Forms.Panel();
            this.pnlProgress = new System.Windows.Forms.Panel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.Column4 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column2 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column3 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column1 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column5 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column6 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column7 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column10 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.ColProductName = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.colBatchNo = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.colSectionName = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column11 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.pnlChart = new System.Windows.Forms.Panel();
            this.sbShelf = new System.Windows.Forms.VScrollBar();
            this.pnlTool = new System.Windows.Forms.Panel();
            this.PColor = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnReQuery = new System.Windows.Forms.Button();
            this.btnChart = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bsMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlData.SuspendLayout();
            this.pnlProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.pnlChart.SuspendLayout();
            this.pnlTool.SuspendLayout();
            this.PColor.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // SHELFNAME
            // 
            this.SHELFNAME.DataPropertyName = "ShelfName";
            this.SHELFNAME.FilteringEnabled = false;
            this.SHELFNAME.HeaderText = "货架";
            this.SHELFNAME.Name = "SHELFNAME";
            this.SHELFNAME.Width = 70;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.SystemColors.Control;
            this.pnlMain.Controls.Add(this.pnlContent);
            this.pnlMain.Controls.Add(this.pnlTool);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1183, 505);
            this.pnlMain.TabIndex = 1;
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.pnlData);
            this.pnlContent.Controls.Add(this.pnlChart);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 47);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1183, 458);
            this.pnlContent.TabIndex = 3;
            // 
            // pnlData
            // 
            this.pnlData.Controls.Add(this.pnlProgress);
            this.pnlData.Controls.Add(this.dgvMain);
            this.pnlData.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlData.Location = new System.Drawing.Point(0, 0);
            this.pnlData.Name = "pnlData";
            this.pnlData.Size = new System.Drawing.Size(1183, 171);
            this.pnlData.TabIndex = 3;
            // 
            // pnlProgress
            // 
            this.pnlProgress.Controls.Add(this.lblInfo);
            this.pnlProgress.Location = new System.Drawing.Point(396, 46);
            this.pnlProgress.Name = "pnlProgress";
            this.pnlProgress.Size = new System.Drawing.Size(238, 79);
            this.pnlProgress.TabIndex = 10;
            this.pnlProgress.Visible = false;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(32, 34);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(161, 12);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "正在准备货位数据,请稍候...";
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AllowUserToDeleteRows = false;
            this.dgvMain.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvMain.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMain.AutoGenerateColumns = false;
            this.dgvMain.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.Column2,
            this.Column3,
            this.Column1,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column10,
            this.ColProductName,
            this.colBatchNo,
            this.colSectionName,
            this.Column11});
            this.dgvMain.DataSource = this.bsMain;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMain.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMain.Location = new System.Drawing.Point(0, 0);
            this.dgvMain.MultiSelect = false;
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvMain.RowHeadersWidth = 30;
            this.dgvMain.RowTemplate.Height = 23;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.Size = new System.Drawing.Size(1183, 171);
            this.dgvMain.TabIndex = 10;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "RegionName";
            this.Column4.FilteringEnabled = false;
            this.Column4.HeaderText = "库区";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "CellCode";
            this.Column2.FilteringEnabled = false;
            this.Column2.HeaderText = "货位编号";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "CellName";
            this.Column3.FilteringEnabled = false;
            this.Column3.HeaderText = "货位名称";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "ShelfValue";
            this.Column1.FilteringEnabled = false;
            this.Column1.HeaderText = "排";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.Width = 40;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "CellColumn";
            this.Column5.FilteringEnabled = false;
            this.Column5.HeaderText = "列";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.Width = 40;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "CellRow";
            this.Column6.FilteringEnabled = false;
            this.Column6.HeaderText = "层";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column6.Width = 40;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "IsActive";
            this.Column7.FilteringEnabled = false;
            this.Column7.HeaderText = "状态";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "PalletBarCode";
            this.Column10.FilteringEnabled = false;
            this.Column10.HeaderText = "产品编号";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ColProductName
            // 
            this.ColProductName.DataPropertyName = "ProductName";
            this.ColProductName.FilteringEnabled = false;
            this.ColProductName.HeaderText = "产品名称";
            this.ColProductName.Name = "ColProductName";
            this.ColProductName.ReadOnly = true;
            // 
            // colBatchNo
            // 
            this.colBatchNo.DataPropertyName = "BatchNo";
            this.colBatchNo.FilteringEnabled = false;
            this.colBatchNo.HeaderText = "批次号";
            this.colBatchNo.Name = "colBatchNo";
            this.colBatchNo.ReadOnly = true;
            // 
            // colSectionName
            // 
            this.colSectionName.DataPropertyName = "SectionName";
            this.colSectionName.FilteringEnabled = false;
            this.colSectionName.HeaderText = "阶段";
            this.colSectionName.Name = "colSectionName";
            this.colSectionName.ReadOnly = true;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "InDate";
            this.Column11.FilteringEnabled = false;
            this.Column11.HeaderText = "入库时间";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // pnlChart
            // 
            this.pnlChart.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlChart.Controls.Add(this.sbShelf);
            this.pnlChart.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlChart.Location = new System.Drawing.Point(0, 377);
            this.pnlChart.Name = "pnlChart";
            this.pnlChart.Size = new System.Drawing.Size(1183, 81);
            this.pnlChart.TabIndex = 2;
            this.pnlChart.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlChart_Paint);
            this.pnlChart.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlChart_MouseClick);
            this.pnlChart.MouseEnter += new System.EventHandler(this.pnlChart_MouseEnter);
            this.pnlChart.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlChart_MouseMove);
            this.pnlChart.Resize += new System.EventHandler(this.pnlChart_Resize);
            // 
            // sbShelf
            // 
            this.sbShelf.Dock = System.Windows.Forms.DockStyle.Right;
            this.sbShelf.LargeChange = 30;
            this.sbShelf.Location = new System.Drawing.Point(1164, 0);
            this.sbShelf.Maximum = 120;
            this.sbShelf.Name = "sbShelf";
            this.sbShelf.Size = new System.Drawing.Size(19, 81);
            this.sbShelf.SmallChange = 30;
            this.sbShelf.TabIndex = 0;
            this.sbShelf.Value = 1;
            this.sbShelf.ValueChanged += new System.EventHandler(this.sbShelf_ValueChanged);
            // 
            // pnlTool
            // 
            this.pnlTool.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.pnlTool.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTool.Controls.Add(this.PColor);
            this.pnlTool.Controls.Add(this.btnExit);
            this.pnlTool.Controls.Add(this.btnReQuery);
            this.pnlTool.Controls.Add(this.btnChart);
            this.pnlTool.Controls.Add(this.btnRefresh);
            this.pnlTool.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTool.Location = new System.Drawing.Point(0, 0);
            this.pnlTool.Name = "pnlTool";
            this.pnlTool.Size = new System.Drawing.Size(1183, 47);
            this.pnlTool.TabIndex = 2;
            // 
            // PColor
            // 
            this.PColor.Controls.Add(this.label18);
            this.PColor.Controls.Add(this.label17);
            this.PColor.Controls.Add(this.label2);
            this.PColor.Controls.Add(this.label9);
            this.PColor.Controls.Add(this.label1);
            this.PColor.Controls.Add(this.label10);
            this.PColor.Controls.Add(this.label4);
            this.PColor.Controls.Add(this.label7);
            this.PColor.Controls.Add(this.label3);
            this.PColor.Controls.Add(this.label8);
            this.PColor.Controls.Add(this.label6);
            this.PColor.Controls.Add(this.label5);
            this.PColor.Location = new System.Drawing.Point(273, 3);
            this.PColor.Name = "PColor";
            this.PColor.Size = new System.Drawing.Size(670, 38);
            this.PColor.TabIndex = 53;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(200, 13);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 12);
            this.label18.TabIndex = 54;
            this.label18.Text = "入库锁定";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.LawnGreen;
            this.label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label17.Location = new System.Drawing.Point(170, 7);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(28, 23);
            this.label17.TabIndex = 53;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(476, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 38;
            this.label2.Text = "空货位锁定";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(38, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 46;
            this.label9.Text = "异常货位";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Red;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(7, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 23);
            this.label1.TabIndex = 37;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Yellow;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Location = new System.Drawing.Point(446, 7);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(28, 23);
            this.label10.TabIndex = 45;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Blue;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Location = new System.Drawing.Point(99, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 23);
            this.label4.TabIndex = 39;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(384, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 44;
            this.label7.Text = "禁用货位";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(130, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 40;
            this.label3.Text = "有货";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Gray;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Location = new System.Drawing.Point(354, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(28, 23);
            this.label8.TabIndex = 43;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Green;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Location = new System.Drawing.Point(262, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 23);
            this.label6.TabIndex = 41;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(292, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 42;
            this.label5.Text = "出库锁定";
            // 
            // btnExit
            // 
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnExit.Image = global::App.Properties.Resources.close;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.Location = new System.Drawing.Point(144, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(48, 45);
            this.btnExit.TabIndex = 52;
            this.btnExit.Text = "退出";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnReQuery
            // 
            this.btnReQuery.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnReQuery.Image = global::App.Properties.Resources.refresh24;
            this.btnReQuery.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnReQuery.Location = new System.Drawing.Point(96, 0);
            this.btnReQuery.Name = "btnReQuery";
            this.btnReQuery.Size = new System.Drawing.Size(48, 45);
            this.btnReQuery.TabIndex = 50;
            this.btnReQuery.Text = "刷新";
            this.btnReQuery.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnReQuery.UseVisualStyleBackColor = true;
            this.btnReQuery.Click += new System.EventHandler(this.btnReQuery_Click);
            // 
            // btnChart
            // 
            this.btnChart.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnChart.Image = global::App.Properties.Resources.report;
            this.btnChart.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnChart.Location = new System.Drawing.Point(48, 0);
            this.btnChart.Name = "btnChart";
            this.btnChart.Size = new System.Drawing.Size(48, 45);
            this.btnChart.TabIndex = 49;
            this.btnChart.Text = "圖形";
            this.btnChart.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnChart.UseVisualStyleBackColor = true;
            this.btnChart.Click += new System.EventHandler(this.btnChart_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRefresh.Image = global::App.Properties.Resources.zoom;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRefresh.Location = new System.Drawing.Point(0, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(48, 45);
            this.btnRefresh.TabIndex = 48;
            this.btnRefresh.Text = "查詢";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // frmCellQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 505);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMain);
            this.Name = "frmCellQuery";
            this.Text = "货位资料查询";
            ((System.ComponentModel.ISupportInitialize)(this.bsMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlData.ResumeLayout(false);
            this.pnlProgress.ResumeLayout(false);
            this.pnlProgress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.pnlChart.ResumeLayout(false);
            this.pnlTool.ResumeLayout(false);
            this.PColor.ResumeLayout(false);
            this.PColor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsMain;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.Panel pnlMain;
        protected System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlData;
        private System.Windows.Forms.Panel pnlProgress;
        private System.Windows.Forms.Label lblInfo;
        protected System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.Panel pnlChart;
        private System.Windows.Forms.VScrollBar sbShelf;
        protected System.Windows.Forms.Panel pnlTool;
        protected System.Windows.Forms.Button btnReQuery;
        protected System.Windows.Forms.Button btnChart;
        protected System.Windows.Forms.Button btnRefresh;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn SHELFNAME;
        protected System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel PColor;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column4;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column2;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column3;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column1;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column5;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column6;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column7;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column10;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn ColProductName;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn colBatchNo;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn colSectionName;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column11;
    }
}