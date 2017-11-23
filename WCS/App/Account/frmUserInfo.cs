using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Util;


namespace App.Account
{
    public partial class frmUserInfo : Form
    {
        private DataRow drUser;
        public frmUserInfo()
        {
            InitializeComponent();
        }
        public frmUserInfo(DataRow dr)
        {
            InitializeComponent();
            drUser = dr;
        }

        private void frmUserInfo_Load(object sender, EventArgs e)
        {
            if (drUser != null)
            {
                txtUserName.Text = drUser["UserName"].ToString();
                if (txtUserName.Text.ToLower() == "admin")
                    this.txtUserName.ReadOnly = true;
                txtEmployeeCode.Text = drUser["EmployeeCode"].ToString();
                txtMemo.Text = drUser["Memo"].ToString();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            

            try
            {
                BLL.BLLBase bll = new BLL.BLLBase();
                if (drUser != null)
                {
                    int count = bll.GetRowCount("SYS_USERLIST", string.Format("UserID<>{0} and UserName='{1}'", drUser["UserID"].ToString(), this.txtUserName.Text.Trim()));

                    if (count > 0)
                    {
                        MessageBox.Show("该用户名已经存在!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    bll.ExecNonQuery("Security.UpdateUserInfo", new DataParameter[]{new DataParameter("@UserName",this.txtUserName.Text.Trim()),
                                                                new DataParameter("@UserID", int.Parse(drUser["UserID"].ToString())),
                                                                 new DataParameter("@EmployeeCode",this.txtEmployeeCode.Text.Trim()),
                                                                 new DataParameter("@Memo",this.txtMemo.Text.Trim())});


                }
                else
                {

                    int count = bll.GetRowCount("SYS_USERLIST", string.Format("UserName='{0}'", this.txtUserName.Text.Trim()));
                    if (count > 0)
                    {
                        MessageBox.Show("該用戶名已經存在!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (this.txtEmployeeCode.Text.Trim() == "")
                    {
                        txtEmployeeCode.Text = txtUserName.Text.Trim();
                    }
                    string strPwd = Util.DESEncrypt.Encrypt("123456");

                    bll.ExecNonQuery("Security.InsertUser", new DataParameter[]{new DataParameter("@UserName",this.txtUserName.Text.Trim()),
                                                                               new DataParameter("@UserPassword",strPwd),
                                                                               new DataParameter("@EmployeeCode",this.txtEmployeeCode.Text),
                                                                               new DataParameter("@Memo",this.txtMemo.Text)});

                  
                }
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
             
            

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
