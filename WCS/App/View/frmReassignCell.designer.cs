namespace App.View
{
    partial class frmReassignCell
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
            this.bsMain = new System.Windows.Forms.BindingSource(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.txtTaskNo = new System.Windows.Forms.TextBox();
            this.txtAisleNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCellCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPalletCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtAreaCode = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtNewCellCode = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCellCodeEnd = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.bsMain)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 17);
            this.label6.TabIndex = 4;
            this.label6.Text = "任务號:";
            // 
            // txtTaskNo
            // 
            this.txtTaskNo.Location = new System.Drawing.Point(62, 27);
            this.txtTaskNo.Name = "txtTaskNo";
            this.txtTaskNo.ReadOnly = true;
            this.txtTaskNo.Size = new System.Drawing.Size(171, 23);
            this.txtTaskNo.TabIndex = 11;
            // 
            // txtAisleNo
            // 
            this.txtAisleNo.Location = new System.Drawing.Point(165, 59);
            this.txtAisleNo.Name = "txtAisleNo";
            this.txtAisleNo.ReadOnly = true;
            this.txtAisleNo.Size = new System.Drawing.Size(68, 23);
            this.txtAisleNo.TabIndex = 25;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(127, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 17);
            this.label4.TabIndex = 24;
            this.label4.Text = "巷道:";
            // 
            // txtCellCode
            // 
            this.txtCellCode.Location = new System.Drawing.Point(335, 54);
            this.txtCellCode.Name = "txtCellCode";
            this.txtCellCode.ReadOnly = true;
            this.txtCellCode.Size = new System.Drawing.Size(171, 23);
            this.txtCellCode.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(265, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 17);
            this.label3.TabIndex = 22;
            this.label3.Text = "起始貨位號:";
            // 
            // txtPalletCode
            // 
            this.txtPalletCode.Location = new System.Drawing.Point(334, 22);
            this.txtPalletCode.Name = "txtPalletCode";
            this.txtPalletCode.ReadOnly = true;
            this.txtPalletCode.Size = new System.Drawing.Size(172, 23);
            this.txtPalletCode.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(265, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 17);
            this.label2.TabIndex = 20;
            this.label2.Text = "箱/托盤號:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCellCodeEnd);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtAreaCode);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtAisleNo);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtCellCode);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPalletCode);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtTaskNo);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(12, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(524, 134);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "入库信息";
            // 
            // txtAreaCode
            // 
            this.txtAreaCode.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAreaCode.Location = new System.Drawing.Point(62, 58);
            this.txtAreaCode.MaxLength = 10;
            this.txtAreaCode.Name = "txtAreaCode";
            this.txtAreaCode.ReadOnly = true;
            this.txtAreaCode.Size = new System.Drawing.Size(55, 23);
            this.txtAreaCode.TabIndex = 70;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(12, 59);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 17);
            this.label12.TabIndex = 69;
            this.label12.Text = "庫區:";
            // 
            // txtNewCellCode
            // 
            this.txtNewCellCode.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtNewCellCode.Location = new System.Drawing.Point(129, 164);
            this.txtNewCellCode.MaxLength = 10;
            this.txtNewCellCode.Name = "txtNewCellCode";
            this.txtNewCellCode.Size = new System.Drawing.Size(159, 23);
            this.txtNewCellCode.TabIndex = 64;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(24, 164);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(99, 17);
            this.label11.TabIndex = 63;
            this.label11.Text = "WMS分配新貨位";
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(289, 219);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 29);
            this.btnCancel.TabIndex = 74;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(170, 219);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 29);
            this.btnOK.TabIndex = 73;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(265, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 17);
            this.label1.TabIndex = 71;
            this.label1.Text = "目標貨位號:";
            // 
            // txtCellCodeEnd
            // 
            this.txtCellCodeEnd.Location = new System.Drawing.Point(335, 93);
            this.txtCellCodeEnd.Name = "txtCellCodeEnd";
            this.txtCellCodeEnd.ReadOnly = true;
            this.txtCellCodeEnd.Size = new System.Drawing.Size(171, 23);
            this.txtCellCodeEnd.TabIndex = 72;
            // 
            // frmReassignCell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 276);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtNewCellCode);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReassignCell";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "重新指定空貨位";
            this.Load += new System.EventHandler(this.frmReassignEmptyCell_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsMain)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bsMain;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTaskNo;
        private System.Windows.Forms.TextBox txtAisleNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCellCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPalletCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNewCellCode;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtAreaCode;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtCellCodeEnd;
        private System.Windows.Forms.Label label1;

    }
}