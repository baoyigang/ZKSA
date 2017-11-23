using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Util;

namespace App.View
{
    public partial class frmReassignCell : Form
    {
        BLL.BLLBase bll = new BLL.BLLBase();
        DataRow dr;
        int OptionCode;
        public frmReassignCell()
        {
            InitializeComponent();
        }
        public frmReassignCell(DataRow dr, int Option)
        {
            InitializeComponent();
            this.dr = dr;
            this.OptionCode = Option;
        }
        private void frmReassignEmptyCell_Load(object sender, EventArgs e)
        {
            this.txtTaskNo.Text = dr["TaskNo"].ToString();
            this.txtCellCode.Text = dr["CellCode"].ToString();
            this.txtAisleNo.Text = dr["AisleNo"].ToString();
            this.txtPalletCode.Text = dr["PalletCode"].ToString();
            this.txtCellCodeEnd.Text = dr["ToCellCode"].ToString();
            this.txtAreaCode.Text = dr["AreaCode"].ToString();
            if (OptionCode == 0)
            {
                this.label11.Text = "WMS分配起始貨位";
            }
            else
            {
                this.label11.Text = "WMS分配目標貨位";
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DataTable dt = bll.FillDataTable("Cmd.SelectCell", new DataParameter[] { new DataParameter("{0}", string.Format("CellName='{0}'", this.txtNewCellCode.Text)) });
            if (dt.Rows.Count > 0)
            {
                string AreaCode = dt.Rows[0]["AreaCode"].ToString();
                string AisleNo = dt.Rows[0]["AisleNo"].ToString();
                string CellCode = dt.Rows[0]["CellCode"].ToString();
                if (txtAreaCode.Text != AreaCode)
                {
                    MessageBox.Show("指定的貨位與原有貨位庫區不一致，無法重新分配貨位！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtAisleNo.Text != AisleNo)
                {
                    MessageBox.Show("指定的貨位與原有貨位巷道不一致，無法重新分配貨位！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (OptionCode == 0)
                {
                    bll.ExecNonQuery("WCS.UpdateTaskFromCellCode", new DataParameter[] { new DataParameter("@NewCellCode", CellCode), new DataParameter("@NewCellName", this.txtNewCellCode.Text), new DataParameter("@TaskNo", this.txtTaskNo.Text) });
                }
                else
                {
                    bll.ExecNonQuery("WCS.UpdateTaskToCellCode", new DataParameter[] { new DataParameter("@NewCellCode", CellCode), new DataParameter("@NewCellName", this.txtNewCellCode.Text), new DataParameter("@TaskNo", this.txtTaskNo.Text) });
                }
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                MessageBox.Show("指定的貨位不存在,請確認！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
    }
}
