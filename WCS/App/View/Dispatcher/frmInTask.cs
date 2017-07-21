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

namespace App.View.Dispatcher
{
    public partial class frmInTask: Form
    {

        public string strValue;
        private DataTable dtTask;
        BLL.BLLBase bll = new BLL.BLLBase();
        private DataTable dtPallet;
        private bool blnOk = false;
        private string PalletCode;

        public frmInTask()
        {
            InitializeComponent();
        }
        public frmInTask(int flag, DataTable dtInfo)
        {
            InitializeComponent();
            dtTask = dtInfo;
        }

        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        private void frmScan_Load(object sender, EventArgs e)
        {
            DataRow dr = dtTask.Rows[0];

         
            PalletCode = dr["PalletCode"].ToString();
            this.txtPalletCode.Text = dr["PalletCode"].ToString();
            this.txtTaskNo.Text = dr["TaskNo"].ToString();
            this.txtCellCode.Text = dr["CellCode"].ToString();

            dtPallet = bll.FillDataTable("WCS.SelectTaskDetail", new DataParameter[] { new DataParameter("{0}", string.Format("T.TaskNo='{0}'", this.txtTaskNo.Text)) });
            this.bsMain.DataSource = dtPallet;
            

        }

       

        private void btnGetBack_Click(object sender, EventArgs e)
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



            strValue = "1";
            this.DialogResult = DialogResult.OK;
            
        }

        private void frmScan_FormClosing(object sender, FormClosingEventArgs e)
        {
            
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
                dtPallet = bll.FillDataTable("WCS.SelectTaskDetail", new DataParameter[] { new DataParameter("{0}", string.Format("T.TaskNo='{0}'",this.txtTaskNo.Text)) });
                this.bsMain.DataSource = dtPallet;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dtPallet = bll.FillDataTable("WCS.SelectTaskDetail", new DataParameter[] { new DataParameter("{0}", string.Format("T.TaskNo='{0}'", this.txtTaskNo.Text)) });
            this.bsMain.DataSource = dtPallet;
        }

        



    }
}
