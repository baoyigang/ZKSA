using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace App.View.Task
{
    public partial class frmTaskDialog : Form
    {
        private string TaskType;
        public string filter = "1=1";
        public frmTaskDialog()
        {
            InitializeComponent();
        }
        public frmTaskDialog(string TaskType)
        {
            InitializeComponent();
            this.TaskType = TaskType;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            filter = string.Format("convert(varchar(10),WCS_Task.TaskDate,120) between '{0}' and '{1}'", this.dtpTaskDate1.Value.ToString("yyyy-MM-dd"), this.dtpTaskDate2.Value.ToString("yyyy-MM-dd"));
            if (this.txtProduct.Text.Trim().Length > 0)
                filter += string.Format("and (WCS_TASK.PalletCode like '%{0}%' or product.ProductName like '%{0}%' )", this.txtProduct.Text.Trim());
            if (this.txtSection.Text.Trim().Length > 0)
                filter += string.Format("and SectionName like '%{0}%'", this.txtSection.Text.Trim());
            if (this.txtBatchNo.Text.Trim().Length > 0)
                filter += string.Format("and BatchNo like '%{0}%'", this.txtBatchNo.Text.Trim());

            if (this.txtShelf1.Text.Trim().Length > 0 && this.txtShelf2.Text.Trim().Length <= 0)
                filter += string.Format("and ShelfValue = {0}", this.txtShelf1.Text.Trim());
            else if (this.txtShelf1.Text.Trim().Length <= 0 && this.txtShelf2.Text.Trim().Length > 0)
                filter += string.Format(" and ShelfValue={0}", this.txtShelf2.Text.Trim());
            else if (this.txtShelf1.Text.Trim().Length > 0 && this.txtShelf2.Text.Trim().Length > 0)
                filter += string.Format(" and ShelfValue between {0} and {1} ", this.txtShelf1.Text.Trim(), this.txtShelf2.Text.Trim());

            if (this.txtColumn1.Text.Trim().Length > 0 && this.txtColumn2.Text.Trim().Length <= 0)
                filter += string.Format("and CellColumn = {0}", this.txtColumn1.Text.Trim());
            else if (this.txtColumn1.Text.Trim().Length <= 0 && this.txtColumn2.Text.Trim().Length > 0)
                filter += string.Format(" and CellColumn={0}", this.txtColumn2.Text.Trim());
            else if (this.txtColumn1.Text.Trim().Length > 0 && this.txtColumn2.Text.Trim().Length > 0)
                filter += string.Format(" and CellColumn between {0} and {1} ", this.txtColumn1.Text.Trim(), this.txtColumn2.Text.Trim());

            if (this.txtRow1.Text.Trim().Length > 0 && this.txtRow2.Text.Trim().Length <= 0)
                filter += string.Format("and CellRow = {0}", this.txtRow1.Text.Trim());
            else if (this.txtRow1.Text.Trim().Length <= 0 && this.txtRow2.Text.Trim().Length > 0)
                filter += string.Format(" and CellRow={0}", this.txtRow2.Text.Trim());
            else if (this.txtRow1.Text.Trim().Length > 0 && this.txtRow2.Text.Trim().Length > 0)
                filter += string.Format(" and CellRow between {0} and {1} ", this.txtRow1.Text.Trim(), this.txtRow2.Text.Trim());
            this.DialogResult = DialogResult.OK;
        }

        private void Txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            int k=(int)e.KeyChar;
            if ((k < 48 || k > 57) && k != 8)
                e.Handled = true;
        }
    }
}
