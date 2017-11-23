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
using MCP;

namespace App.View.Param
{
    public partial class frmCraneHandle : BaseForm
    {
        BLL.BLLBase bll = new BLL.BLLBase();
        string AreaCode = "";
        public frmCraneHandle()
        {
            InitializeComponent();
        }

        private void toolStripButton_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton_Refresh_Click(object sender, EventArgs e)
        {
            BindData();
            
        }

        
        private void BindData()
        {
            DataTable dt = bll.FillDataTable("CMD.SelectDevice", new DataParameter[] { new DataParameter("@AreaCode", AreaCode) });
            bsMain.DataSource = dt;
        }

        private void frmInStock_Load(object sender, EventArgs e)
        {
            AreaCode = BLL.Server.GetAreaCode();
            this.BindData();
        }

        private void dgvMain_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                   
                    //若行已是选中状态就不再进行设置
                    if (dgvMain.Rows[e.RowIndex].Selected == false)
                    {
                        dgvMain.ClearSelection();
                        dgvMain.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (dgvMain.SelectedRows.Count == 1)
                    {
                        DataRow dr = (this.dgvMain.Rows[this.dgvMain.CurrentCell.RowIndex].DataBoundItem as DataRowView).Row;
                        if (dr["State"].ToString() == "0")
                        {
                            this.toolStripButton_Enabled.Enabled = true;
                            this.toolStripButton_NoEnabled.Enabled = false;
                        }
                        else
                        {
                            this.toolStripButton_Enabled.Enabled = false;
                            this.toolStripButton_NoEnabled.Enabled = true;
                        }
                    }                    
                   
                    
                }
            }
        }

        private void frmInStock_Activated(object sender, EventArgs e)
        {
            this.BindData();
        }

        private void toolStripButton_Enabled_Click(object sender, EventArgs e)
        {
            if (this.dgvMain.CurrentCell != null)
            {
                try
                {
                    DataRow dr = (this.dgvMain.Rows[this.dgvMain.CurrentCell.RowIndex].DataBoundItem as DataRowView).Row;
                    bll.ExecNonQuery("WCS.UpdateWCSDevice", new DataParameter[] { new DataParameter("{0}", 1), new DataParameter("{1}", dr["DeviceName"].ToString()) });
                    Logger.Info("啟用" +  dr["DeviceName"].ToString() + " 堆垛机！");

                    BindData();
                }
                catch (Exception ex)
                {
                    Logger.Error("frmCraneHandle中啟用堆垛机出現異常" + ex.Message);

                }
            }
        }

        private void toolStripButton_NoEnabled_Click(object sender, EventArgs e)
        {
            if (this.dgvMain.CurrentCell != null)
            {
                try
                {
                    DataRow dr = (this.dgvMain.Rows[this.dgvMain.CurrentCell.RowIndex].DataBoundItem as DataRowView).Row;
                    bll.ExecNonQuery("WCS.UpdateWCSDevice", new DataParameter[] { new DataParameter("{0}", 0), new DataParameter("{1}", dr["DeviceName"].ToString()) });
                    Logger.Info("禁用" +  dr["DeviceName"].ToString() + " 堆垛机！");

                    BindData();
                }
                catch (Exception ex)
                {
                    Logger.Error("frmCraneHandle中禁用堆垛机出現異常" + ex.Message);

                }
            }
        }
    }
}
