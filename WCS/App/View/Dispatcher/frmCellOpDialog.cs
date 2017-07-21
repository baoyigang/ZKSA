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
    public partial class frmCellOpDialog : Form
    {
        string CellCode = "";
        string AreaCode = "";
        DataRow dr;
        private Dictionary<string, string> BillFields = new Dictionary<string, string>();
        private Dictionary<string, string> ProductFields = new Dictionary<string, string>();
        private Dictionary<string, string> StateFields = new Dictionary<string, string>();
        private Dictionary<string, string> TaskFields = new Dictionary<string, string>();
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
            DataTable dtBillType = bll.FillDataTable("Cmd.SelectBillType", new DataParameter[] { new DataParameter("{0}", "Flag=1") });
            DataRow drNew = dtBillType.NewRow();
            drNew["BillTypeCode"] = "";
            drNew["BillTypeName"] = "请选择";
            dtBillType.Rows.InsertAt(drNew, 0);

            CellCode = dr["CellCode"].ToString();
            AreaCode = dr["AreaCode"].ToString();
            this.txtCellCode.Text = CellCode;

            this.txtPalletCode.Text = dr["PalletCode"].ToString();
            
            this.checkBox1.Checked = dr["IsLock"].ToString() == "1";
            this.checkBox2.Checked = dr["IsActive"].ToString() == "0";
            this.checkBox3.Checked = dr["ErrorFlag"].ToString() == "1";
            this.checkBox5.Checked = dr["IsTurnover"].ToString() == "1";
            this.groupBox2.Enabled = false;

            this.groupBox2.Enabled = !radioButton1.Checked;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确定要对货位" + this.txtCellCode.Text + "修改吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                DataParameter[] param;
                if (this.radioButton1.Checked)
                {
                    param = new DataParameter[] { new DataParameter("{0}", "IsLock='0'"), new DataParameter("{1}", string.Format("CellCode='{0}'", this.txtCellCode.Text)) };
                    bll.ExecNonQuery("WCS.UpdateCellByFilter", param);
                    MCP.Logger.Info("货位编辑,货位号:" + this.txtCellCode.Text + "解除锁定!");
                }
                else if (this.radioButton2.Checked)
                {
                    param = new DataParameter[] { new DataParameter("{0}", "IsLock='0',PalletCode='',InDate=NULL"), new DataParameter("{1}", string.Format("CellCode='{0}'", this.txtCellCode.Text)) };
                    bll.ExecNonQuery("WCS.UpdateCellByFilter", param);
                    param = new DataParameter[] { new DataParameter("{0}", string.Format("CellCode='{0}'", this.txtCellCode.Text)) };
                    bll.ExecNonQuery("WCS.DeleteCellDetail", param);
                    MCP.Logger.Info("货位编辑,货位号:" + this.txtCellCode.Text + "解除锁定,并删除货位信息!");
                }
                else if (this.radioButton3.Checked)
                {
                    param = new DataParameter[] { new DataParameter("{0}", "ErrorFlag=''"), new DataParameter("{1}", string.Format("CellCode='{0}'", this.txtCellCode.Text)) };
                    bll.ExecNonQuery("WCS.UpdateCellByFilter", param);
                    MCP.Logger.Info("货位编辑,货位号:" + this.txtCellCode.Text + "解除异常!");
                }
                else if (this.radioButton4.Checked)
                {
                    param = new DataParameter[] { new DataParameter("{0}", "ErrorFlag='',PalletCode='',InDate=NULL"), new DataParameter("{1}", string.Format("CellCode='{0}'", this.txtCellCode.Text)) };
                    bll.ExecNonQuery("WCS.UpdateCellByFilter", param);
                    param = new DataParameter[] { new DataParameter("{0}", string.Format("CellCode='{0}'", this.txtCellCode.Text)) };
                    bll.ExecNonQuery("WCS.DeleteCellDetail", param);
                    MCP.Logger.Info("货位编辑,货位号:" + this.txtCellCode.Text + "解除异常,并删除货位信息!");
                }
                else if (this.radioButton5.Checked)
                {
                    string IsLock = this.checkBox1.Checked ? "1" : "0";
                    string IsActive = this.checkBox2.Checked ? "0" : "1";
                    string ErrorFlag = this.checkBox3.Checked ? "1" : "0";
                    string IsTurnover = this.checkBox5.Checked ? "1" : "0";
                    string sql = string.Format("IsLock='{0}'", IsLock);
                    sql += string.Format(",IsActive='{0}'", IsActive);
                    sql += string.Format(",ErrorFlag='{0}'", ErrorFlag);
                    sql += string.Format(",IsTurnover='{0}'", IsTurnover);
                    sql += string.Format(",PalletCode='{0}'", this.txtPalletCode.Text.Trim());
                    if (this.txtPalletCode.Text.Trim() == "")
                        sql += ",InDate=null";
                    else
                        sql += ",InDate=getdate()";

                    param = new DataParameter[] { new DataParameter("{0}", sql), new DataParameter("{1}", string.Format("CellCode='{0}'", this.txtCellCode.Text)) };
                    bll.ExecNonQuery("WCS.UpdateCellByFilter", param);
                    if (this.txtPalletCode.Text.Trim() == "")
                    {
                        param = new DataParameter[] { new DataParameter("{0}", string.Format("CellCode='{0}'", this.txtCellCode.Text)) };
                        bll.ExecNonQuery("WCS.DeleteCellDetail", param);
                        MCP.Logger.Info("货位编辑,货位号:" + this.txtCellCode.Text + "更新货位信息,并删除货位信息!");

                    }
                    else
                    {
                        MCP.Logger.Info("货位编辑,货位号:" + this.txtCellCode.Text + "更新货位信息!");
                    }
                }
            }
            DialogResult = DialogResult.OK;
        }        

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.groupBox2.Enabled = !radioButton1.Checked;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.groupBox2.Enabled = !radioButton2.Checked;
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
