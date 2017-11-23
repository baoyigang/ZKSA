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

namespace App.Account
{
    public partial class frmUserList :View.BaseForm
    {
        BLL.BLLBase bll = new BLL.BLLBase();
        private DataTable dtOP;
        public frmUserList()
        {
            InitializeComponent();
        }
        public frmUserList(DataTable dtop)
        {
            InitializeComponent();
            dtOP = dtop;
        }

        private void frmUserList_Load(object sender, EventArgs e)
        {
            toolStripButton_Add.Enabled = (dtOP.Select("FormID='User' and OperatorCode=0").Length > 0 ? true : false);
            toolStripButton_Edit.Enabled = (dtOP.Select("FormID='User' and OperatorCode=2").Length > 0 ? true : false);
            toolStripButton_Del.Enabled = (dtOP.Select("FormID='User' and OperatorCode=1").Length > 0 ? true : false);
        }
        private void frmUserList_Activated(object sender, EventArgs e)
        {
            this.BindData();
        }
        private void dgvMain_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            //{
            //    if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            //    {
            //        //若行已是选中状态就不再进行设置
            //        if (dgvMain.Rows[e.RowIndex].Selected == false)
            //        {
            //            dgvMain.ClearSelection();
            //            dgvMain.Rows[e.RowIndex].Selected = true;
            //        }
            //        //只选中一行时设置活动单元格
            //        if (dgvMain.SelectedRows.Count == 1)
            //        {
            //            dgvMain.CurrentCell = dgvMain.Rows[e.RowIndex].Cells[e.ColumnIndex];
            //        }                    
            //        //弹出操作菜单
            //        contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
            //    }
            //}
        }


       

        private void toolStripButton_Refresh_Click(object sender, EventArgs e)
        {
            BindData();

        }
        private void toolStripButton_Add_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                BindData();
            }
        }
        private void toolStripButton_Edit_Click(object sender, EventArgs e)
        {
            if (this.dgvMain.CurrentRow == null)
                return;
            if (this.dgvMain.CurrentRow.Index >= 0)
            {
                DataRow druser = (this.dgvMain.CurrentRow.DataBoundItem as DataRowView).Row;
                frmUserInfo frm = new frmUserInfo(druser);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    BindData();
                } 
            }
        }
        private void toolStripButton_Del_Click(object sender, EventArgs e)
        {
            if (this.dgvMain.CurrentRow == null)
                return;
            if (this.dgvMain.CurrentRow.Index >= 0)
            {
                if (this.dgvMain.CurrentRow.Cells["colUserName"].Value.ToString() != "admin")
                {
                    if (DialogResult.Yes == MessageBox.Show("您確定要刪除此用戶賬號？", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {


                        bll.ExecNonQuery("Security.DeleteUser", new DataParameter[] { new DataParameter("{0}", (this.dgvMain.CurrentRow.DataBoundItem as DataRowView).Row["UserID"].ToString()) });
                        this.BindData();
                    }
                }
                else
                {
                    Logger.Info("管理員賬號不能刪除！");
                    return;
                }
            }
        }
        private void toolStripButton_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BindData()
        {
            DataTable dt = bll.FillDataTable("Security.SelectUser");
            bsMain.DataSource = dt;
        }
    }
}
