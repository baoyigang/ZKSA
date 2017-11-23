using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Util;

namespace App.View.Dispatcher
{
    public partial class frmCellOpDialog : BaseForm
    {
        string BillTypeCode = "";
        string CellCode = "";
        string AreaCode = "";
        DataRow dr;
    
        BLL.BLLBase bll = new BLL.BLLBase();

        public frmCellOpDialog()
        {
            InitializeComponent();
        }
        public frmCellOpDialog(DataRow dr)
        {
            InitializeComponent();
            this.dr = dr;
        }

        private void CellOpDialog_Load(object sender, EventArgs e)
        {
            

            CellCode = dr["CellCode"].ToString();
            AreaCode = dr["AreaCode"].ToString();
            this.txtCellCode.Text = CellCode;
            this.txtCellName.Text = dr["CellName"].ToString();

           
            this.txtProductCode.Text = dr["PalletCode"].ToString();

            this.checkBox3.Checked = dr["ErrorFlag"].ToString() == "1";
            if (dr["InDate"].ToString() == "")
            {
                this.dtpInDate.Checked = false;
            }
            else
            {
                this.dtpInDate.Checked = true;
                this.dtpInDate.Value = DateTime.Parse(dr["InDate"].ToString());
            }
            this.groupBox2.Enabled = false;
            this.groupBox2.Enabled = radioButton5.Checked;
          
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确定要对货位" + this.txtCellCode.Text + "修改吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                DataParameter[] param;
                 
               if (this.radioButton3.Checked)
                {
                    param = new DataParameter[] { new DataParameter("{0}", "ErrorFlag=''"), new DataParameter("{1}", string.Format("CellCode='{0}'", this.txtCellCode.Text)) };
                    bll.ExecNonQuery("WCS.UpdateCellByFilter", param);
                    MCP.Logger.Info("手動修改貨位:" + this.txtCellCode.Text + ",清除異常.");

                }
                else if (this.radioButton4.Checked)
                {
                    param = new DataParameter[] { new DataParameter("{0}", "ErrorFlag='',Palletcode='',InDate=NULL,BillNo=''"), new DataParameter("{1}", string.Format("CellCode='{0}'", this.txtCellCode.Text)) };
                    bll.ExecNonQuery("WCS.UpdateCellByFilter", param);
                    MCP.Logger.Info("手動修改貨位:" + this.txtCellCode.Text + ",清除異常并清除貨位信息");
                }
               
                else if (this.radioButton5.Checked)
                {
                   
                    string ErrorFlag = this.checkBox3.Checked ? "1" : "0";

                    string sql = string.Format("ErrorFlag='{0}'", ErrorFlag);

                    //if (this.txtProductCode.Text.Trim().Length > 0)
                    sql += string.Format(",Palletcode='{0}'", this.txtProductCode.Text.Trim());


                    if (this.dtpInDate.Checked)
                        sql += string.Format(",InDate='{0}'", this.dtpInDate.Value.ToString("yyyy/MM/dd HH:mm:ss"));              

                    param = new DataParameter[] { new DataParameter("{0}", sql), new DataParameter("{1}", string.Format("CellCode='{0}'", this.txtCellCode.Text)) };
                    bll.ExecNonQuery("WCS.UpdateCellByFilter", param);
                    MCP.Logger.Info("手動修改貨位:" + this.txtCellCode.Text);
                }
            }
            DialogResult = DialogResult.OK;
        }

        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            this.groupBox2.Enabled = !radioButton3.Checked;
            
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            this.groupBox2.Enabled = !radioButton4.Checked;
            
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            this.groupBox2.Enabled = radioButton5.Checked;
           
        }

       
    }
}
