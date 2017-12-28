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

namespace App.View.Task
{
    public partial class frmInStock : BaseForm
    {
        BLL.BLLBase bll = new BLL.BLLBase();

        public frmInStock()
        {
            InitializeComponent();
        }

       

        private void BindData()
        {
            DataTable dt = bll.FillDataTable("WCS.SelectTask", new DataParameter[] { new DataParameter("{0}", string.Format("WCS_TASK.State in('0','100') and WCS_TASK.TaskType='11'")) });
            bsMain.DataSource = dt;
        }
        private void BindData(string filter)
        {
            DataTable dt = bll.FillDataTable("WCS.SelectTask", new DataParameter[] { new DataParameter("{0}", string.Format("WCS_TASK.TaskType='11' and {0}", filter)) });
            bsMain.DataSource = dt;
        }
        private void frmInStock_Load(object sender, EventArgs e)
        {
            this.BindData();
            //for (int i = 1; i < this.dgvMain.Columns.Count - 1; i++)
            //    ((DataGridViewAutoFilterTextBoxColumn)this.dgvMain.Columns[i]).FilteringEnabled = true;

            DataTable dt = Program.dtUserPermission;
            //入库任务--取消任务
            if (dt != null)
            {
                string filter = "SubModuleCode='MNU_W00A_00D' and OperatorCode='2'";
                DataRow[] drs = dt.Select(filter);

                filter = "SubModuleCode='MNU_W00A_00D' and OperatorCode='3'";
                drs = dt.Select(filter);
                if (drs.Length <= 0)
                {
                    this.toolStripButton_Stop.Visible = false;
                    this.toolStripButton_Start.Visible = false;
                }
                else
                {
                    this.toolStripButton_Stop.Visible = true;
                    this.toolStripButton_Start.Visible = true;
                }
            }
            else
            {
                this.toolStripButton_Stop.Visible = false;
                this.toolStripButton_Start.Visible = false;
            }
        }

       
        //private void toolStripMenuItem2_Click(object sender, EventArgs e)
        //{
        //    UpdatedgvMainState("0");       
        //}

        //private void toolStripMenuItem3_Click(object sender, EventArgs e)
        //{
        //    UpdatedgvMainState("3");
        //}

        //private void toolStripMenuItem4_Click(object sender, EventArgs e)
        //{
        //    UpdatedgvMainState("7");
        //}

        //private void toolStripMenuItem5_Click(object sender, EventArgs e)
        //{
        //    UpdatedgvMainState("9");
        //}
        //private void UpdatedgvMainState(string State)
        //{
        //    if (this.dgvMain.CurrentCell != null)
        //    {
        //        BLL.BLLBase bll = new BLL.BLLBase();
        //        string TaskNo = this.dgvMain.Rows[this.dgvMain.CurrentCell.RowIndex].Cells[0].Value.ToString();
        //        string state = this.dgvMain.Rows[this.dgvMain.CurrentCell.RowIndex].Cells[2].Value.ToString();
        //        bll.ExecNonQuery("WCS.UpdateTaskStateByTaskNo", new DataParameter[] { new DataParameter("@State", State), new DataParameter("@TaskNo", TaskNo) });

        //        //堆垛机完成执行
        //        if (State == "7")
        //        {
        //            DataParameter[] param = new DataParameter[] { new DataParameter("@TaskNo", TaskNo) };
        //            bll.ExecNonQueryTran("WCS.Sp_TaskProcess", param);
        //        }
               
        //        BindData();
        //    }
        //}

        //private void toolStripMenuItem1_Click(object sender, EventArgs e)
        //{
        //    UpdatedgvMainState("1");
        //}

        //private void toolStripMenuItem6_Click(object sender, EventArgs e)
        //{
        //    UpdatedgvMainState("2");
        //}

        private void frmInStock_Activated(object sender, EventArgs e)
        {
            this.BindData();
        }

       

        private void dgvMain_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                e.RowBounds.Location.Y,
                this.dgvMain.RowHeadersWidth - 4,
                e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dgvMain.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dgvMain.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);

        }

        private void toolStripButton_Query_Click(object sender, EventArgs e)
        {
            frmTaskDialog f = new frmTaskDialog("11");
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BindData(f.filter);
            }
        }
        private void toolStripButton_Refresh_Click(object sender, EventArgs e)
        {
            BindData();
        }
        private void toolStripButton_CheckAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <dgvMain.Rows.Count ; i++)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgvMain.Rows[i].Cells[0];
                checkCell.Value = true;
            }  
        }
        private void toolStripButton_Stop_Click(object sender, EventArgs e)
        {
            string TaskNo = "";
            for (int i = 0; i < dgvMain.Rows.Count; i++)
            {
                if ((bool)((DataGridViewCheckBoxCell)dgvMain.Rows[i].Cells[0]).Value)
                {
                    TaskNo += "'" + dgvMain.Rows[i].Cells[1].Value.ToString() + "',";
                }
            }
            if (TaskNo != "")
            {
                bll.ExecNonQuery("WCS.UpdateTaskState", new DataParameter[] { new DataParameter("{0}", "State=100"), new DataParameter("{1}", string.Format("TaskNo in ({0}) and State=0 and AGVTaskID=0", TaskNo.TrimEnd(','))) });
                MCP.Logger.Info("暂停入库单：" + ((DataRowView)dgvMain.Rows[0].DataBoundItem).Row["BillID"].ToString() + "任务！");
                this.BindData();
            }
        }

        private void toolStripButton_Start_Click(object sender, EventArgs e)
        {
            string TaskNo = "";
            for (int i = 0; i < dgvMain.Rows.Count; i++)
            {
                if ((bool)((DataGridViewCheckBoxCell)dgvMain.Rows[i].Cells[0]).Value)
                {
                    TaskNo += "'" + dgvMain.Rows[i].Cells[1].Value.ToString() + "',";
                }
            }
            if (TaskNo != "")
            {
                bll.ExecNonQuery("WCS.UpdateTaskState", new DataParameter[] { new DataParameter("{0}", "State=0"), new DataParameter("{1}", string.Format("TaskNo in ({0}) and State=100", TaskNo.TrimEnd(','))) });

                MCP.Logger.Info("开始入库单：" + ((DataRowView)dgvMain.Rows[0].DataBoundItem).Row["BillID"].ToString() + "任务！");
                this.BindData();
            }
        }
        private void toolStripButton_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
         

      

       
    }
}
