using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Util;
using DataGridViewAutoFilter;

namespace App.View.Dispatcher
{
    public partial class frmTaskQuery :BaseForm
    {
        
        BLL.BLLBase bll = new BLL.BLLBase();

        public frmTaskQuery()
        {
            InitializeComponent();
        }
        private void BindData()
        {

            string filter = " 1=1 ";
            if (this.checkBox1.Checked)
            {
                filter += string.Format(" AND CONVERT(nvarchar,TaskDate,120)>='{0}'", dtpStartTime.Value.ToString("yyyy-MM-dd HH:mm:00"));
                filter += string.Format(" AND CONVERT(nvarchar,TaskDate,120)<='{0}'", dtpEndTime.Value.ToString("yyyy-MM-dd HH:mm:59"));
            }
            if (cmbTaskType.Text != "")
            {
                filter += string.Format(" AND WCS_Task.TYPE='{0}'", cmbTaskType.Text);
            }
            if (cmbAreaNo.Text != "")
            {
                filter += string.Format(" AND WCS_Task.AreaCode='{0}'", cmbAreaNo.Text);
            }
            if (cmbAisleNo.Text != "")
            {
                filter += string.Format(" AND WCS_Task.AisleNo='{0}'", cmbAisleNo.Text);
            }
            if (txtPalletCode.Text.Trim() != "")
            {
                filter += string.Format(" AND WCS_Task.PalletCode LIKE '%{0}%'", txtPalletCode.Text.Trim());
            }
            if (txtCellCode.Text.Trim() != "")
            {
                filter += string.Format(" AND (from_location_id LIKE '%{0}%' OR to_location_id LIKE '%{0}%') ", txtCellCode.Text.Trim());
            }

            DataTable dt = bll.FillDataTable("WCS.SelectTaskQuery", new DataParameter[] { new DataParameter("{0}", filter) });
            bsMain.DataSource = dt;
        }

        private void frmTaskQuery_Load(object sender, EventArgs e)
        {
            this.dtpStartTime.Value = DateTime.Now.AddHours(-1);
            for (int i = 0; i < this.dgvMain.Columns.Count - 1; i++)
                ((DataGridViewAutoFilterTextBoxColumn)this.dgvMain.Columns[i]).FilteringEnabled = true;
        }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            BindData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbAreaNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAreaNo.Text != "")
            {
                this.cmbAisleNo.Items.Clear();
                this.cmbAisleNo.Items.Add("");
                if (cmbAreaNo.Text == "UL")
                {
                    for (int i = 1; i <= 9; i++)
                    {
                        this.cmbAisleNo.Items.Add("0" + i.ToString());
                    }
                }
                else
                {
                    for (int i = 1; i <= 3; i++)
                    {
                        this.cmbAisleNo.Items.Add("0" + i.ToString());
                    }
                }
            }
            else
            {
                this.cmbAisleNo.Items.Clear();
                this.cmbAisleNo.Items.Add("");
                for (int i = 1; i <= 9; i++)
                {
                    this.cmbAisleNo.Items.Add("0" + i.ToString());
                }
            }
        }

      
       
    }
}

