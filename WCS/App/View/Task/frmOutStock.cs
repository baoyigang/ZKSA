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
namespace App.View.Task
{
    public partial class frmOutStock : BaseForm
    {
        BLL.BLLBase bll = new BLL.BLLBase();

        public frmOutStock()
        {
            InitializeComponent();
            this.dgvMain.DataError += delegate(object sender, DataGridViewDataErrorEventArgs e) { }; 
        }

        private void toolStripButton_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton_Refresh_Click(object sender, EventArgs e)
        {
            BindData();
            
        }      

        private void toolStripButton_Cancel_Click(object sender, EventArgs e)
        {
            if (this.dgvMain.CurrentRow == null)
                return;
            if (this.dgvMain.CurrentRow.Index >= 0)
            {
                if (this.dgvMain.SelectedRows[0].Cells["colState"].Value.ToString() == "等待")
                {
                    string TaskNo = this.dgvMain.SelectedRows[0].Cells["colTaskNo"].Value.ToString();
                    string state = bll.GetFieldValue("WCS_Task", "State", string.Format("TaskNo='{0}'", TaskNo));
                    if (state == "0")
                    {
                        if (DialogResult.Yes == MessageBox.Show("您确定要取消此任务吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        {
                           
                            //bll.ExecNonQuery("WCS.UpdateTaskStateByTaskNo", new DataParameter[] { new DataParameter("@State", 9), new DataParameter("@TaskNo", TaskNo) });

                            DataParameter[] param = new DataParameter[] { new DataParameter("@TaskNo", TaskNo) };
                            bll.ExecNonQueryTran("WCS.Sp_TaskCancelProcess", param);
                            this.BindData();
                        }
                    }
                    else
                    {
                        BindData();
                        Logger.Info("选中的状态非[等待],请确认！");
                        return;

                    }
                }
                else
                {
                    Logger.Info("选中的状态非[等待],请确认！");
                    return;
                }
            }
        }

        private void toolStripButton_EmptyOut_Click(object sender, EventArgs e)
        {
            frmPalletOutTask f = new frmPalletOutTask();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BindData();
            }
        }
        private void BindData()
        {
            DataTable dt = bll.FillDataTable("WCS.SelectTask", new DataParameter[] { new DataParameter("{0}", "WCS_TASK.State in('0','1','2','3') and WCS_TASK.TaskType in ('12','15')") }); 
            bsMain.DataSource = dt;
        }

        private void frmOutStock_Load(object sender, EventArgs e)
        {
            //this.BindData();
            for (int i = 0; i < this.dgvMain.Columns.Count - 1; i++)
                ((DataGridViewAutoFilterTextBoxColumn)this.dgvMain.Columns[i]).FilteringEnabled = true;
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
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            UpdatedgvMainState("0");
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            UpdatedgvMainState("3");
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            UpdatedgvMainState("7");
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            UpdatedgvMainState("9");
        }
        private void UpdatedgvMainState(string State)
        {
            if (this.dgvMain.CurrentCell != null)
            {
                string TaskNo = this.dgvMain.Rows[this.dgvMain.CurrentCell.RowIndex].Cells["colTaskNo"].Value.ToString();
                DataParameter[] param = new DataParameter[] { new DataParameter("@TaskNo", TaskNo), new DataParameter("@State", State) };

                bll.ExecNonQueryTran("WCS.Sp_UpdateTaskState", param);
                MCP.Logger.Info("frmOutStock中任何号：" + TaskNo + "手动更新为" + State);
                BindData();
            }
        }

        private void frmOutStock_Activated(object sender, EventArgs e)
        {
            this.BindData();
        }
        private void dgvMain_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            string TaskNo = this.dgvMain.Rows[e.RowIndex].Cells[1].Value.ToString();
            DataTable dt = bll.FillDataTable("WCS.SelectTaskDetail", new DataParameter[] { new DataParameter("{0}", string.Format("T.TaskNo='{0}'", TaskNo)) });
            bsDetail.DataSource = dt;
        }
    }
}
