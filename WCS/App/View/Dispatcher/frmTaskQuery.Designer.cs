namespace App.View.Dispatcher
{
    partial class frmTaskQuery
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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.Column9 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column3 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column11 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.colTaskNo = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column8 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column10 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column1 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column4 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column5 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column6 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column7 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.colConveyFromStation = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.colConveyToStation = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column12 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column13 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.colCraneFromStation = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.colCraneToStation = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column2 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column14 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column15 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.bsMain = new System.Windows.Forms.BindingSource(this.components);
            this.pnlTool = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.cmbAisleNo = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.cmbTaskType = new System.Windows.Forms.ComboBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.txtCellCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPalletCode = new System.Windows.Forms.TextBox();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.cmbAreaNo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlMain.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMain)).BeginInit();
            this.pnlTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlContent);
            this.pnlMain.Controls.Add(this.pnlTool);
            this.pnlMain.Controls.Add(this.pnlBottom);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1243, 379);
            this.pnlMain.TabIndex = 6;
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.dgvMain);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 54);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1243, 269);
            this.pnlContent.TabIndex = 1;
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvMain.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMain.AutoGenerateColumns = false;
            this.dgvMain.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMain.ColumnHeadersHeight = 30;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column9,
            this.Column3,
            this.Column11,
            this.colTaskNo,
            this.Column8,
            this.Column10,
            this.Column1,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.colConveyFromStation,
            this.colConveyToStation,
            this.Column12,
            this.Column13,
            this.colCraneFromStation,
            this.colCraneToStation,
            this.Column2,
            this.Column14,
            this.Column15});
            this.dgvMain.DataSource = this.bsMain;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvMain.RowHeadersWidth = 25;
            this.dgvMain.RowTemplate.Height = 23;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.Size = new System.Drawing.Size(1243, 269);
            this.dgvMain.TabIndex = 5;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "AreaCode";
            this.Column9.FilteringEnabled = false;
            this.Column9.HeaderText = "庫區";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 80;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "AisleNo";
            this.Column3.FilteringEnabled = false;
            this.Column3.HeaderText = "巷道";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.Width = 80;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "BillTypeName";
            this.Column11.FilteringEnabled = false;
            this.Column11.HeaderText = "任务類型";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // colTaskNo
            // 
            this.colTaskNo.DataPropertyName = "TaskNo";
            this.colTaskNo.FilteringEnabled = false;
            this.colTaskNo.HeaderText = "任务编号";
            this.colTaskNo.Name = "colTaskNo";
            this.colTaskNo.ReadOnly = true;
            this.colTaskNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "STATEDESC";
            this.Column8.FilteringEnabled = false;
            this.Column8.HeaderText = "任务状态";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "PalletCode";
            this.Column10.FilteringEnabled = false;
            this.Column10.HeaderText = "托盤/箱號";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "FromStation1";
            this.Column1.FilteringEnabled = false;
            this.Column1.HeaderText = "起始目標";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "ToStation1";
            this.Column4.FilteringEnabled = false;
            this.Column4.HeaderText = "目的地";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "TaskDate";
            this.Column5.FilteringEnabled = false;
            this.Column5.HeaderText = "接收時間";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.Width = 150;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "Convey_StartDate";
            this.Column6.FilteringEnabled = false;
            this.Column6.HeaderText = "輸送線開始";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 150;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "Convey_FinishDate";
            this.Column7.FilteringEnabled = false;
            this.Column7.HeaderText = "輸送線结束";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 150;
            // 
            // colConveyFromStation
            // 
            this.colConveyFromStation.DataPropertyName = "ConveyFromStation";
            this.colConveyFromStation.FilteringEnabled = false;
            this.colConveyFromStation.HeaderText = "輸送線起始地址";
            this.colConveyFromStation.Name = "colConveyFromStation";
            this.colConveyFromStation.ReadOnly = true;
            this.colConveyFromStation.Width = 150;
            // 
            // colConveyToStation
            // 
            this.colConveyToStation.DataPropertyName = "conveyToStation";
            this.colConveyToStation.FilteringEnabled = false;
            this.colConveyToStation.HeaderText = "輸送線目的地址";
            this.colConveyToStation.Name = "colConveyToStation";
            this.colConveyToStation.ReadOnly = true;
            this.colConveyToStation.Width = 150;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "Crane_StartDate";
            this.Column12.FilteringEnabled = false;
            this.Column12.HeaderText = "堆垛机開始";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column12.Width = 150;
            // 
            // Column13
            // 
            this.Column13.DataPropertyName = "Crane_FinishDate";
            this.Column13.FilteringEnabled = false;
            this.Column13.HeaderText = "堆垛机結束";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // colCraneFromStation
            // 
            this.colCraneFromStation.DataPropertyName = "CraneFromStation";
            this.colCraneFromStation.FilteringEnabled = false;
            this.colCraneFromStation.HeaderText = "堆垛机起始地址";
            this.colCraneFromStation.Name = "colCraneFromStation";
            this.colCraneFromStation.ReadOnly = true;
            this.colCraneFromStation.Width = 150;
            // 
            // colCraneToStation
            // 
            this.colCraneToStation.DataPropertyName = "CraneToStation";
            this.colCraneToStation.FilteringEnabled = false;
            this.colCraneToStation.HeaderText = "堆垛机目的地址";
            this.colCraneToStation.Name = "colCraneToStation";
            this.colCraneToStation.ReadOnly = true;
            this.colCraneToStation.Width = 150;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "Type";
            this.Column2.FilteringEnabled = false;
            this.Column2.HeaderText = "WMS類型";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column14
            // 
            this.Column14.DataPropertyName = "TaskID";
            this.Column14.FilteringEnabled = false;
            this.Column14.HeaderText = "WMS任务ID";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column14.Width = 150;
            // 
            // Column15
            // 
            this.Column15.DataPropertyName = "SubTaskID";
            this.Column15.FilteringEnabled = false;
            this.Column15.HeaderText = "WMS任务明細ID";
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            this.Column15.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column15.Width = 160;
            // 
            // pnlTool
            // 
            this.pnlTool.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.pnlTool.Controls.Add(this.checkBox1);
            this.pnlTool.Controls.Add(this.cmbAisleNo);
            this.pnlTool.Controls.Add(this.label7);
            this.pnlTool.Controls.Add(this.btnClose);
            this.pnlTool.Controls.Add(this.cmbTaskType);
            this.pnlTool.Controls.Add(this.btnQuery);
            this.pnlTool.Controls.Add(this.label6);
            this.pnlTool.Controls.Add(this.dtpStartTime);
            this.pnlTool.Controls.Add(this.txtCellCode);
            this.pnlTool.Controls.Add(this.label1);
            this.pnlTool.Controls.Add(this.txtPalletCode);
            this.pnlTool.Controls.Add(this.dtpEndTime);
            this.pnlTool.Controls.Add(this.cmbAreaNo);
            this.pnlTool.Controls.Add(this.label2);
            this.pnlTool.Controls.Add(this.label5);
            this.pnlTool.Controls.Add(this.label3);
            this.pnlTool.Controls.Add(this.label4);
            this.pnlTool.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTool.Location = new System.Drawing.Point(0, 0);
            this.pnlTool.Name = "pnlTool";
            this.pnlTool.Size = new System.Drawing.Size(1243, 54);
            this.pnlTool.TabIndex = 0;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(30, 20);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 19;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // cmbAisleNo
            // 
            this.cmbAisleNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAisleNo.FormattingEnabled = true;
            this.cmbAisleNo.Items.AddRange(new object[] {
            "",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09"});
            this.cmbAisleNo.Location = new System.Drawing.Point(646, 15);
            this.cmbAisleNo.Name = "cmbAisleNo";
            this.cmbAisleNo.Size = new System.Drawing.Size(62, 20);
            this.cmbAisleNo.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(613, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 17;
            this.label7.Text = "巷道";
            // 
            // btnClose
            // 
            this.btnClose.Image = global::App.Properties.Resources.close;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClose.Location = new System.Drawing.Point(1076, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 50);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "关闭";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cmbTaskType
            // 
            this.cmbTaskType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTaskType.FormattingEnabled = true;
            this.cmbTaskType.Items.AddRange(new object[] {
            "",
            "PUT",
            "MRGPUT",
            "PIC",
            "RPLPIC",
            "RPLPUT",
            "MRG",
            "ZONMOV",
            "CNT",
            "MOV"});
            this.cmbTaskType.Location = new System.Drawing.Point(438, 15);
            this.cmbTaskType.Name = "cmbTaskType";
            this.cmbTaskType.Size = new System.Drawing.Size(62, 20);
            this.cmbTaskType.TabIndex = 16;
            // 
            // btnQuery
            // 
            this.btnQuery.Image = global::App.Properties.Resources.zoom;
            this.btnQuery.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnQuery.Location = new System.Drawing.Point(1010, 3);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(60, 50);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "查询";
            this.btnQuery.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(381, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "WMS類型";
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.CustomFormat = "yyyy/MM/dd HH:mm";
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartTime.Location = new System.Drawing.Point(101, 15);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(124, 22);
            this.dtpStartTime.TabIndex = 5;
            // 
            // txtCellCode
            // 
            this.txtCellCode.Location = new System.Drawing.Point(900, 15);
            this.txtCellCode.Name = "txtCellCode";
            this.txtCellCode.Size = new System.Drawing.Size(104, 22);
            this.txtCellCode.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "任务時間";
            // 
            // txtPalletCode
            // 
            this.txtPalletCode.Location = new System.Drawing.Point(785, 15);
            this.txtPalletCode.Name = "txtPalletCode";
            this.txtPalletCode.Size = new System.Drawing.Size(80, 22);
            this.txtPalletCode.TabIndex = 13;
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.CustomFormat = "yyyy/MM/dd HH:mm";
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndTime.Location = new System.Drawing.Point(254, 15);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(122, 22);
            this.dtpEndTime.TabIndex = 7;
            // 
            // cmbAreaNo
            // 
            this.cmbAreaNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAreaNo.FormattingEnabled = true;
            this.cmbAreaNo.Items.AddRange(new object[] {
            "",
            "UL",
            "ML"});
            this.cmbAreaNo.Location = new System.Drawing.Point(541, 15);
            this.cmbAreaNo.Name = "cmbAreaNo";
            this.cmbAreaNo.Size = new System.Drawing.Size(62, 20);
            this.cmbAreaNo.TabIndex = 12;
            this.cmbAreaNo.SelectedIndexChanged += new System.EventHandler(this.cmbAreaNo_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(506, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "庫區";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(231, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "至";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(725, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "托盤/箱號";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(868, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "貨位";
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 323);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1243, 56);
            this.pnlBottom.TabIndex = 2;
            this.pnlBottom.Visible = false;
            // 
            // frmTaskQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1243, 379);
            this.Controls.Add(this.pnlMain);
            this.Name = "frmTaskQuery";
            this.Text = "任务查询";
            this.Load += new System.EventHandler(this.frmTaskQuery_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMain)).EndInit();
            this.pnlTool.ResumeLayout(false);
            this.pnlTool.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.Panel pnlTool;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.BindingSource bsMain;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCellCode;
        private System.Windows.Forms.TextBox txtPalletCode;
        private System.Windows.Forms.ComboBox cmbAreaNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbTaskType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbAisleNo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBox1;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column9;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column3;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column11;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn colTaskNo;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column8;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column10;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column1;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column4;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column5;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column6;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column7;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn colConveyFromStation;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn colConveyToStation;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column12;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column13;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn colCraneFromStation;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn colCraneToStation;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column2;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column14;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column15;
    }
}