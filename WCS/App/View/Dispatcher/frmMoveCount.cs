using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Util;
using System.Text.RegularExpressions;

namespace App.View.Dispatcher
{
    public partial class frmMoveCount : BaseForm
    {
        BLL.BLLBase bll = new BLL.BLLBase();
        public frmMoveCount()
        {
            InitializeComponent();
        }
        private void frmMoveCount_Load(object sender, EventArgs e)
        {
           DataTable dt=bll.FillDataTable("WCS.SelectMoveCount");
           this.txtMove.Text = dt.Rows[0][0].ToString();
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            string textWrite = this.txtMove.Text;
            if (Regex.IsMatch(textWrite,@"[^\d]+"))
            {
                this.lbtip.Text = "只能输入数字！";
            }
            else
            {
                DataParameter[] param = new DataParameter[] { new DataParameter("@CraneNo", textWrite) };
                 bll.ExecNonQuery("WCS.UpdateMoveCount", param);
                 this.lbtip.Text = "";
                 this.Close();
            }
            
        }
    }
}
