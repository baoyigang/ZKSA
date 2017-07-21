namespace App.View.Dispatcher
{
    partial class frmMoveCount
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtMove = new System.Windows.Forms.TextBox();
            this.btnSure = new System.Windows.Forms.Button();
            this.lbtip = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(24, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "修改次数：";
            // 
            // txtMove
            // 
            this.txtMove.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMove.Location = new System.Drawing.Point(131, 110);
            this.txtMove.Name = "txtMove";
            this.txtMove.Size = new System.Drawing.Size(172, 35);
            this.txtMove.TabIndex = 1;
            // 
            // btnSure
            // 
            this.btnSure.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSure.Location = new System.Drawing.Point(320, 110);
            this.btnSure.Name = "btnSure";
            this.btnSure.Size = new System.Drawing.Size(75, 35);
            this.btnSure.TabIndex = 2;
            this.btnSure.Text = "确认";
            this.btnSure.UseVisualStyleBackColor = true;
            this.btnSure.Click += new System.EventHandler(this.btnSure_Click);
            // 
            // lbtip
            // 
            this.lbtip.AutoSize = true;
            this.lbtip.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbtip.Location = new System.Drawing.Point(115, 171);
            this.lbtip.Name = "lbtip";
            this.lbtip.Size = new System.Drawing.Size(0, 28);
            this.lbtip.TabIndex = 3;
            // 
            // frmMoveCount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 286);
            this.Controls.Add(this.lbtip);
            this.Controls.Add(this.btnSure);
            this.Controls.Add(this.txtMove);
            this.Controls.Add(this.label1);
            this.Name = "frmMoveCount";
            this.Text = "frmMoveCount";
            this.Load += new System.EventHandler(this.frmMoveCount_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMove;
        private System.Windows.Forms.Button btnSure;
        private System.Windows.Forms.Label lbtip;
    }
}