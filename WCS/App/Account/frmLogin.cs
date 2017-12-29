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
    public partial class frmLogin : Form
    {
        public string UserID = "";
        public frmLogin()
        {
            InitializeComponent();
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.Show();
            if (txtUserName.Text != "")
                txtPassWord.Focus();
            else
                txtUserName.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtUserName.Text.Trim().Length != 0)
            {
                BLL.BLLBase userBll = new BLL.BLLBase();

                DataTable dtUserList = userBll.FillDataTable("Security.SelectUserInfoByUserName", new DataParameter[] { new DataParameter("@UserName", this.txtUserName.Text) });
                if (dtUserList.Rows.Count > 0)
                {
                    dtUserList.Rows[0].BeginEdit();
                    dtUserList.Rows[0]["UserPassword"] = Util.DESEncrypt.Decrypt(dtUserList.Rows[0]["UserPassword"].ToString());
                    dtUserList.Rows[0].EndEdit();
                }
                if (dtUserList.Rows.Count > 0)
                {
                    if (dtUserList.Rows[0]["UserPassword"].ToString().Trim() == this.txtPassWord.Text.Trim())
                    {
                        UserID = this.txtUserName.Text.Trim();
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("对不起，您输入的密码有误!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("对不起，您输入的用户名不存在!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                MessageBox.Show("请输入用户名!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        private void txtPWD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK_Click(null, null);
            }
        }
    }
}
