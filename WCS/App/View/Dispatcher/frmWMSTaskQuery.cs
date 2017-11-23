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
    public partial class frmWMSTaskQuery :BaseForm
    {

        BLL.BLLBase bll = new BLL.BLLBase("MiddleDB");

        public frmWMSTaskQuery()
        {
            InitializeComponent();
        }
        private void BindData()
        {

            string filter = " 1=1 ";
            if (cmbTaskType.Text != "")
            {
                filter += string.Format(" AND Main.type='{0}'", cmbTaskType.Text);
            }
            if (cmbAreaNo.Text != "")
            {
                filter += string.Format(" AND Main.equipment='{0}'", cmbAreaNo.Text);
            }
            //if (cmbAisleNo.Text  != "")
            //{
            //    filter += string.Format(" AND WCS_Task.AisleNo='{0}'", cmbAisleNo.Text);
            //}
            if ( txtPalletCode.Text.Trim() != "")
            {
                filter += string.Format(" AND detail.hu_id LIKE '%{0}%'", txtPalletCode.Text.Trim());
            }
            //if (txtCellCode.Text.Trim() != "")
            //{
            //    filter += string.Format(" AND (from_location_id LIKE '%{0}%' OR to_location_id LIKE '%{0}%') ", txtCellCode.Text.Trim());
            //}

            DataTable dt = bll.FillDataTable("Middle.SelectWMSTaskQuery", new DataParameter[] { new DataParameter("{0}", filter) });
            bsMain.DataSource = dt;
        }

        private void frmTaskQuery_Load(object sender, EventArgs e)
        {

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

      
       
    }
}

