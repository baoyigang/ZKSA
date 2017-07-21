namespace App.View
{
    partial class frmMonitor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMonitor));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemDelCraneTask = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemReassign = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemStateChange = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem14 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem15 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem16 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem17 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem19 = new System.Windows.Forms.ToolStripMenuItem();
            this.bsMain = new System.Windows.Forms.BindingSource(this.components);
            this.pnlMain = new System.Windows.Forms.Panel();
            this.splitContainer_Main = new System.Windows.Forms.SplitContainer();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.Column5 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column1 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column11 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column3 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column12 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column13 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Column14 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.colErrCode = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.colErrDesc = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.colTaskType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.picCrane = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnClearAlarm = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnBack1 = new System.Windows.Forms.Button();
            this.btnStop1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtState1 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtErrorNo1 = new System.Windows.Forms.TextBox();
            this.txtHeight1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtForkStatus1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtErrorDesc1 = new System.Windows.Forms.TextBox();
            this.txtCraneAction1 = new System.Windows.Forms.TextBox();
            this.txtColumn1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTaskNo1 = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_Main)).BeginInit();
            this.splitContainer_Main.Panel1.SuspendLayout();
            this.splitContainer_Main.Panel2.SuspendLayout();
            this.splitContainer_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCrane)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemDelCraneTask,
            this.ToolStripMenuItemReassign,
            this.ToolStripMenuItemStateChange});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 70);
            // 
            // ToolStripMenuItemDelCraneTask
            // 
            this.ToolStripMenuItemDelCraneTask.Name = "ToolStripMenuItemDelCraneTask";
            this.ToolStripMenuItemDelCraneTask.Size = new System.Drawing.Size(160, 22);
            this.ToolStripMenuItemDelCraneTask.Text = "删除堆垛机任务";
            this.ToolStripMenuItemDelCraneTask.Click += new System.EventHandler(this.ToolStripMenuItemDelCraneTask_Click);
            // 
            // ToolStripMenuItemReassign
            // 
            this.ToolStripMenuItemReassign.Name = "ToolStripMenuItemReassign";
            this.ToolStripMenuItemReassign.Size = new System.Drawing.Size(160, 22);
            this.ToolStripMenuItemReassign.Text = "重新下发任务";
            this.ToolStripMenuItemReassign.Click += new System.EventHandler(this.ToolStripMenuItemReassign_Click);
            // 
            // ToolStripMenuItemStateChange
            // 
            this.ToolStripMenuItemStateChange.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem10,
            this.ToolStripMenuItem13,
            this.ToolStripMenuItem14,
            this.ToolStripMenuItem15,
            this.ToolStripMenuItem16,
            this.ToolStripMenuItem17,
            this.ToolStripMenuItem19});
            this.ToolStripMenuItemStateChange.Name = "ToolStripMenuItemStateChange";
            this.ToolStripMenuItemStateChange.Size = new System.Drawing.Size(160, 22);
            this.ToolStripMenuItemStateChange.Text = "任务状态切换";
            // 
            // ToolStripMenuItem10
            // 
            this.ToolStripMenuItem10.Name = "ToolStripMenuItem10";
            this.ToolStripMenuItem10.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem10.Text = "等待";
            this.ToolStripMenuItem10.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // ToolStripMenuItem13
            // 
            this.ToolStripMenuItem13.Name = "ToolStripMenuItem13";
            this.ToolStripMenuItem13.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem13.Text = "执行";
            this.ToolStripMenuItem13.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // ToolStripMenuItem14
            // 
            this.ToolStripMenuItem14.Name = "ToolStripMenuItem14";
            this.ToolStripMenuItem14.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem14.Text = "盘点出库";
            this.ToolStripMenuItem14.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // ToolStripMenuItem15
            // 
            this.ToolStripMenuItem15.Name = "ToolStripMenuItem15";
            this.ToolStripMenuItem15.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem15.Text = "盘点入库";
            this.ToolStripMenuItem15.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // ToolStripMenuItem16
            // 
            this.ToolStripMenuItem16.Name = "ToolStripMenuItem16";
            this.ToolStripMenuItem16.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem16.Text = "执行";
            this.ToolStripMenuItem16.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // ToolStripMenuItem17
            // 
            this.ToolStripMenuItem17.Name = "ToolStripMenuItem17";
            this.ToolStripMenuItem17.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem17.Text = "完成";
            this.ToolStripMenuItem17.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // ToolStripMenuItem19
            // 
            this.ToolStripMenuItem19.Name = "ToolStripMenuItem19";
            this.ToolStripMenuItem19.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem19.Text = "取消";
            this.ToolStripMenuItem19.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.splitContainer_Main);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1436, 750);
            this.pnlMain.TabIndex = 3;
            // 
            // splitContainer_Main
            // 
            this.splitContainer_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_Main.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_Main.Name = "splitContainer_Main";
            this.splitContainer_Main.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_Main.Panel1
            // 
            this.splitContainer_Main.Panel1.Controls.Add(this.dgvMain);
            // 
            // splitContainer_Main.Panel2
            // 
            this.splitContainer_Main.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer_Main.Size = new System.Drawing.Size(1436, 750);
            this.splitContainer_Main.SplitterDistance = 355;
            this.splitContainer_Main.TabIndex = 0;
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
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
            this.Column5,
            this.Column1,
            this.Column11,
            this.Column3,
            this.Column12,
            this.Column13,
            this.Column14,
            this.colErrCode,
            this.colErrDesc,
            this.colTaskType});
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
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            this.dgvMain.RowHeadersWidth = 40;
            this.dgvMain.RowTemplate.Height = 23;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.Size = new System.Drawing.Size(1436, 355);
            this.dgvMain.TabIndex = 6;
            this.dgvMain.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMain_CellMouseClick);
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "TaskNo";
            this.Column5.FilteringEnabled = false;
            this.Column5.Frozen = true;
            this.Column5.HeaderText = "任务号";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.Width = 120;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "BillTypeName";
            this.Column1.FilteringEnabled = false;
            this.Column1.Frozen = true;
            this.Column1.HeaderText = "任务类型";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "StateDesc";
            this.Column11.FilteringEnabled = false;
            this.Column11.Frozen = true;
            this.Column11.HeaderText = "状态";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "FromStation";
            this.Column3.FilteringEnabled = false;
            this.Column3.HeaderText = "起始地址";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "ToStation";
            this.Column12.FilteringEnabled = false;
            this.Column12.HeaderText = "目标地址";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column13
            // 
            this.Column13.DataPropertyName = "RequestDate";
            this.Column13.FilteringEnabled = false;
            this.Column13.HeaderText = "请求时间";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column13.Width = 150;
            // 
            // Column14
            // 
            this.Column14.DataPropertyName = "StartDate";
            this.Column14.FilteringEnabled = false;
            this.Column14.HeaderText = "开始时间";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column14.Width = 150;
            // 
            // colErrCode
            // 
            this.colErrCode.DataPropertyName = "CraneErrCode";
            this.colErrCode.FilteringEnabled = false;
            this.colErrCode.HeaderText = "错误代码";
            this.colErrCode.Name = "colErrCode";
            this.colErrCode.ReadOnly = true;
            this.colErrCode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // colErrDesc
            // 
            this.colErrDesc.DataPropertyName = "CraneErrDesc";
            this.colErrDesc.FilteringEnabled = false;
            this.colErrDesc.HeaderText = "错误描述";
            this.colErrDesc.Name = "colErrDesc";
            this.colErrDesc.ReadOnly = true;
            this.colErrDesc.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colErrDesc.Width = 180;
            // 
            // colTaskType
            // 
            this.colTaskType.DataPropertyName = "TaskType";
            this.colTaskType.HeaderText = "任务类型ID";
            this.colTaskType.Name = "colTaskType";
            this.colTaskType.ReadOnly = true;
            this.colTaskType.Visible = false;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.picCrane);
            this.splitContainer2.Panel1.Controls.Add(this.pictureBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer2.Size = new System.Drawing.Size(1436, 391);
            this.splitContainer2.SplitterDistance = 912;
            this.splitContainer2.TabIndex = 7;
            // 
            // picCrane
            // 
            this.picCrane.BackColor = System.Drawing.SystemColors.Control;
            this.picCrane.Image = global::App.Properties.Resources.Crane11;
            this.picCrane.Location = new System.Drawing.Point(1031, 98);
            this.picCrane.Name = "picCrane";
            this.picCrane.Size = new System.Drawing.Size(90, 44);
            this.picCrane.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCrane.TabIndex = 6;
            this.picCrane.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1164, 254);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnClearAlarm);
            this.groupBox4.Controls.Add(this.btnReset);
            this.groupBox4.Controls.Add(this.btnBack1);
            this.groupBox4.Controls.Add(this.btnStop1);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.txtState1);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.txtErrorNo1);
            this.groupBox4.Controls.Add(this.txtHeight1);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.txtForkStatus1);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.txtErrorDesc1);
            this.groupBox4.Controls.Add(this.txtCraneAction1);
            this.groupBox4.Controls.Add(this.txtColumn1);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.txtTaskNo1);
            this.groupBox4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.ForeColor = System.Drawing.Color.Red;
            this.groupBox4.Location = new System.Drawing.Point(3, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(512, 251);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "堆垛机";
            // 
            // btnClearAlarm
            // 
            this.btnClearAlarm.BackColor = System.Drawing.Color.Orange;
            this.btnClearAlarm.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClearAlarm.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnClearAlarm.Location = new System.Drawing.Point(95, 188);
            this.btnClearAlarm.Name = "btnClearAlarm";
            this.btnClearAlarm.Size = new System.Drawing.Size(75, 37);
            this.btnClearAlarm.TabIndex = 22;
            this.btnClearAlarm.Text = "解警";
            this.btnClearAlarm.UseVisualStyleBackColor = false;
            this.btnClearAlarm.Click += new System.EventHandler(this.btnClearAlarm_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.Snow;
            this.btnReset.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReset.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnReset.Location = new System.Drawing.Point(192, 188);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 37);
            this.btnReset.TabIndex = 21;
            this.btnReset.Text = "复位";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnBack1
            // 
            this.btnBack1.BackColor = System.Drawing.Color.Lime;
            this.btnBack1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnBack1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnBack1.Location = new System.Drawing.Point(386, 188);
            this.btnBack1.Name = "btnBack1";
            this.btnBack1.Size = new System.Drawing.Size(75, 37);
            this.btnBack1.TabIndex = 16;
            this.btnBack1.Text = "召回";
            this.btnBack1.UseVisualStyleBackColor = false;
            this.btnBack1.Click += new System.EventHandler(this.btnBack1_Click);
            // 
            // btnStop1
            // 
            this.btnStop1.BackColor = System.Drawing.Color.Red;
            this.btnStop1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStop1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnStop1.Location = new System.Drawing.Point(289, 188);
            this.btnStop1.Name = "btnStop1";
            this.btnStop1.Size = new System.Drawing.Size(75, 37);
            this.btnStop1.TabIndex = 9;
            this.btnStop1.Text = "急停";
            this.btnStop1.UseVisualStyleBackColor = false;
            this.btnStop1.Click += new System.EventHandler(this.btnStop1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(3, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "任务编号";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(267, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "错误代码";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(3, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "货叉位置";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(6, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "工作方式";
            // 
            // txtState1
            // 
            this.txtState1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtState1.Location = new System.Drawing.Point(332, 26);
            this.txtState1.Name = "txtState1";
            this.txtState1.ReadOnly = true;
            this.txtState1.Size = new System.Drawing.Size(177, 26);
            this.txtState1.TabIndex = 10;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label20.Location = new System.Drawing.Point(397, 97);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(51, 20);
            this.label20.TabIndex = 18;
            this.label20.Text = "当前层";
            // 
            // txtErrorNo1
            // 
            this.txtErrorNo1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtErrorNo1.Location = new System.Drawing.Point(332, 58);
            this.txtErrorNo1.Name = "txtErrorNo1";
            this.txtErrorNo1.ReadOnly = true;
            this.txtErrorNo1.Size = new System.Drawing.Size(177, 26);
            this.txtErrorNo1.TabIndex = 3;
            // 
            // txtHeight1
            // 
            this.txtHeight1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtHeight1.Location = new System.Drawing.Point(450, 94);
            this.txtHeight1.Name = "txtHeight1";
            this.txtHeight1.ReadOnly = true;
            this.txtHeight1.Size = new System.Drawing.Size(59, 26);
            this.txtHeight1.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(267, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "工作状态";
            // 
            // txtForkStatus1
            // 
            this.txtForkStatus1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtForkStatus1.Location = new System.Drawing.Point(70, 61);
            this.txtForkStatus1.Name = "txtForkStatus1";
            this.txtForkStatus1.ReadOnly = true;
            this.txtForkStatus1.Size = new System.Drawing.Size(177, 26);
            this.txtForkStatus1.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(281, 97);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 20);
            this.label7.TabIndex = 15;
            this.label7.Text = "当前列";
            // 
            // txtErrorDesc1
            // 
            this.txtErrorDesc1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtErrorDesc1.Location = new System.Drawing.Point(70, 131);
            this.txtErrorDesc1.Name = "txtErrorDesc1";
            this.txtErrorDesc1.ReadOnly = true;
            this.txtErrorDesc1.Size = new System.Drawing.Size(439, 26);
            this.txtErrorDesc1.TabIndex = 12;
            // 
            // txtCraneAction1
            // 
            this.txtCraneAction1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCraneAction1.Location = new System.Drawing.Point(70, 96);
            this.txtCraneAction1.Name = "txtCraneAction1";
            this.txtCraneAction1.ReadOnly = true;
            this.txtCraneAction1.Size = new System.Drawing.Size(177, 26);
            this.txtCraneAction1.TabIndex = 1;
            // 
            // txtColumn1
            // 
            this.txtColumn1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtColumn1.Location = new System.Drawing.Point(335, 94);
            this.txtColumn1.Name = "txtColumn1";
            this.txtColumn1.ReadOnly = true;
            this.txtColumn1.Size = new System.Drawing.Size(59, 26);
            this.txtColumn1.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(3, 134);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 20);
            this.label6.TabIndex = 13;
            this.label6.Text = "错误描述";
            // 
            // txtTaskNo1
            // 
            this.txtTaskNo1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTaskNo1.Location = new System.Drawing.Point(70, 26);
            this.txtTaskNo1.Name = "txtTaskNo1";
            this.txtTaskNo1.ReadOnly = true;
            this.txtTaskNo1.Size = new System.Drawing.Size(177, 26);
            this.txtTaskNo1.TabIndex = 0;
            // 
            // frmMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1436, 750);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMain);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMonitor";
            this.Text = "监控";
            this.Load += new System.EventHandler(this.frmMonitor_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.splitContainer_Main.Panel1.ResumeLayout(false);
            this.splitContainer_Main.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_Main)).EndInit();
            this.splitContainer_Main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCrane)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.SplitContainer splitContainer_Main;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtErrorNo1;
        private System.Windows.Forms.TextBox txtForkStatus1;
        private System.Windows.Forms.TextBox txtCraneAction1;
        private System.Windows.Forms.TextBox txtTaskNo1;
        private System.Windows.Forms.Button btnStop1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtErrorDesc1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtState1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtColumn1;
        private System.Windows.Forms.PictureBox picCrane;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.BindingSource bsMain;
        private System.Windows.Forms.Button btnBack1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtHeight1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemReassign;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemStateChange;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem13;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem14;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem15;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem16;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem17;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem19;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDelCraneTask;
        private System.Windows.Forms.Button btnClearAlarm;
        private System.Windows.Forms.Button btnReset;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column5;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column1;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column11;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column3;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column12;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column13;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Column14;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn colErrCode;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn colErrDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskType;
        private System.Windows.Forms.SplitContainer splitContainer2;
    }
}