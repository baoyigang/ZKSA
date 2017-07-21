using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Util;
using MCP;

namespace App.View.Task
{
    public partial class frmInStockTask :BaseForm
    {
        BLL.BLLBase bll = new BLL.BLLBase();
     
        private DataTable dtPallet;
        private int Flag = 0;
        private bool blnInStock = false;
        public frmInStockTask()
        {
            InitializeComponent();
        }

        private void frmInStockTask_Load(object sender, EventArgs e)
        {
            this.txtPalletCode.Focus();
        }
       
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (this.txtTaskNo.Text.Trim() != "")
            {
                Logger.Info("托盘编号已经产生入库任务,无法取消");
            }
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            if (blnInStock)
            {
                if (Flag == 0)
                {
                    try
                    {
                        List<string> list = new List<string>();
                        List<DataParameter[]> paras = new List<DataParameter[]>();
                        DataParameter[] para;
                        //托盘明细,
                        para = new DataParameter[] { new DataParameter("@PalletCode", this.txtPalletCode.Text) };
                        list.Add("WCS.DeletePalletDetail");
                        paras.Add(para);

                        for (int i = 0; i < dtPallet.Rows.Count; i++)
                        {
                            para = new DataParameter[] { new DataParameter("@PalletCode", this.txtPalletCode.Text), 
                                                 new DataParameter("@RowID", dtPallet.Rows[i]["RowID"].ToString()),
                                                 new DataParameter("@ProductCode", dtPallet.Rows[i]["ProductCode"].ToString()) 
                                                };
                            list.Add("WCS.InsertPalletDetail");
                            paras.Add(para);
                        }


                        //任务明细

                        para = new DataParameter[] { new DataParameter("@TaskNo", this.txtTaskNo.Text) };
                        list.Add("WCS.DeleteTaskDetail");
                        paras.Add(para);

                        for (int i = 0; i < dtPallet.Rows.Count; i++)
                        {
                            para = new DataParameter[] { new DataParameter("@TaskNo", this.txtTaskNo.Text),
                                                 new DataParameter("@RowID", dtPallet.Rows[i]["RowID"].ToString()),
                                                 new DataParameter("@PalletCode", this.txtPalletCode.Text),
                                                 new DataParameter("@ProductCode", dtPallet.Rows[i]["ProductCode"].ToString()),
                                                 new DataParameter("@Quantity", dtPallet.Rows[i]["Quantity"]) 
                                                };
                            list.Add("WCS.InsertTaskDetail");
                            paras.Add(para);
                        }

                        bll.ExecTran(list.ToArray(), paras);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex.Message);
                        return;
                    }
                }

                Context.ProcessDispatcher.WriteToService("CarPLC1", "WriteFinished", 1);
                Logger.Info("托盘条码为 " + this.txtPalletCode.Text + " 开始入库!");

                this.DialogResult = DialogResult.OK;
            }
        }
        private void frmInStockTask_Activated(object sender, EventArgs e)
        {
            this.txtPalletCode.Focus();
        }

 

        

        private void btnPallet_Click(object sender, EventArgs e)
        {
            if (this.txtPalletCode.Text.Trim().Length == 0)
            {
                Logger.Error("托盘条码不能为空!");
                this.txtPalletCode.Focus();
                return;
            }

            string BarCode = this.txtPalletCode.Text.Trim();

            bool blnHasOutTask = false;
            bool blnInnver = false;
            int Count = bll.GetRowCount("WCS_TASK", string.Format("TaskType='11' and State in (0,3) and PalletCode!='{0}'", BarCode));
            if (Count > 0)
            {
                Context.ProcessDispatcher.WriteToService("CarPLC1", "WriteFinished", 2);
                Logger.Error("堆垛机正在执行出库任务,不能入库!");
                return;
            }

            Count = bll.GetRowCount("WCS_TASK", string.Format("TaskType='11' and State in (0,3) and PalletCode='{0}'", BarCode));
            if (Count == 0)
            {

                Count = bll.GetRowCount("WCS_TASK", "TaskType='12' and State=0");
                if (Count > 0)
                    blnHasOutTask = true;
                else
                {
                    if (bll.GetRowCount("WCS_TASK", string.Format("TaskType='14' and State<7 and PalletCode='{0}'", BarCode)) > 0)
                        blnInnver = true;
                }
            }
            if (blnHasOutTask)
            {
                Context.ProcessDispatcher.WriteToService("CarPLC1", "WriteFinished", 2);
                Logger.Error("堆垛机正在执行出库任务,不能入库!");
                return;
            }
            //判断货位表中是否含有该条码的货位.
            int i = bll.GetRowCount("CMD_CELL", string.Format("CellName='{0}' and IsTurnover=0 ", BarCode));
            if (i == 0 && !blnInnver)
            {
                Context.ProcessDispatcher.WriteToService("CarPLC1", "WriteFinished", 4);
                Logger.Error("条码不正确,不存在货位名称为 " + BarCode + " 的货位!");
                return;
            }
            if (i > 0)
            {
                //判断该货位是否含有货,托盘条码是否重复
                int k = bll.GetRowCount("CMD_CELL", string.Format("CellName='{0}' and PalletCode='{1}' and InDate is not Null ", BarCode, BarCode));
                if (k == 1 && !blnInnver )
                {
                    Context.ProcessDispatcher.WriteToService("CarPLC1", "WriteFinished", 3);
                    Logger.Error("条码重复,货位名称为 " + BarCode + " 的货位已经入库!");
                    return;
                }
            }
            blnInStock = true;
            bll.ExecNonQuery("WCS.SpCreateInTaskByPallet", new DataParameter[] { new DataParameter("@PalletCode", BarCode) });

            DataTable dtTask = bll.FillDataTable("WCS.SelectTask", new DataParameter[] { new DataParameter("{0}", string.Format("WCS_Task.TaskType='11' and WCS_Task.State in (0,3) and WCS_Task.PalletCode='{0}'", this.txtPalletCode.Text.Trim())) });
            if (dtTask.Rows.Count > 0)
            {
                DataRow dr = dtTask.Rows[0];
                this.txtPalletCode.Text = dr["PalletCode"].ToString();
                this.txtTaskNo.Text = dr["TaskNo"].ToString();
                this.txtCellCode.Text = dr["CellCode"].ToString();

                dtPallet = bll.FillDataTable("WCS.SelectTaskDetail", new DataParameter[] { new DataParameter("{0}", string.Format("T.TaskNo='{0}'",this.txtTaskNo.Text)) });
                this.bsMain.DataSource = dtPallet;

            }
            else
                Flag = 1;

        }

        private void btnDeleteRow_Click(object sender, EventArgs e)
        {
            if (this.dgView.CurrentRow == null)
                return;
            if (this.dgView.CurrentRow.Index >= 0)
            {
                DataRow[] drs = dtPallet.Select("RowID='" + this.dgView.CurrentRow.Cells["colRowID"].Value.ToString() + "'");
                if (drs.Length > 0)
                    dtPallet.Rows.Remove(drs[0]);
            }
        }

        private void txtPalletCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnPallet_Click(null, null);
            }
        }       
    }
}
