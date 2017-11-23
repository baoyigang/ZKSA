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
    public partial class frmChangePWD : Form
    {
        public frmChangePWD()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtUser.Text.Trim().Length == 0)
            {
                MessageBox.Show("請輸入用戶！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.txtPWD.Text.Trim().Length == 0)
            {
                MessageBox.Show("請輸入密碼！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.txtNewPWD.Text.Trim().Length == 0)
            {
                MessageBox.Show("請輸入新密碼！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.txtNewPWD.Text.Trim()!=this.txtNewPWD2.Text.Trim())
            {
                MessageBox.Show("密碼不一致！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            BLL.BLLBase userBll = new BLL.BLLBase();

            DataTable dtUserList = userBll.FillDataTable("Security.SelectUserInfoByUserName", new DataParameter[] { new DataParameter("@UserName", this.txtUser.Text) });
            if (dtUserList.Rows.Count > 0)
            {
                dtUserList.Rows[0].BeginEdit();
                dtUserList.Rows[0]["UserPassword"] = Util.DESEncrypt.Decrypt(dtUserList.Rows[0]["UserPassword"].ToString());
                dtUserList.Rows[0].EndEdit();
            }
            if (dtUserList.Rows[0]["UserPassword"].ToString().Trim() == this.txtPWD.Text.Trim())
            {
                string strPwd = Util.DESEncrypt.Encrypt(this.txtNewPWD.Text.Trim());
                userBll.ExecNonQuery("Security.UpdateUserPWD", new DataParameter[] { new DataParameter("@UserName", this.txtUser.Text.Trim()), new DataParameter("@PWD", strPwd) });

                MessageBox.Show("密码修改成功!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                
            }
            else
            {
                MessageBox.Show("原密碼錯誤!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
