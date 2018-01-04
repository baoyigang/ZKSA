using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Util;

namespace App.View.Dispatcher
{
    public partial class frmCellQuery : BaseForm
    {
        BLL.BLLBase bll = new BLL.BLLBase();

        private Dictionary<int, DataRow[]> shelf = new Dictionary<int, DataRow[]>();
        private Dictionary<int, string> ShelfCode = new Dictionary<int, string>();
        private Dictionary<int, int> ShelfRow = new Dictionary<int, int>();
        private Dictionary<int, int> ShelfColumn = new Dictionary<int, int>();
        private Dictionary<int, string> ShelfName = new Dictionary<int, string>();
        private DataTable cellTable = null;
        private bool needDraw = false;
        private bool filtered = false;

        private int[] Columns = new int[4];
        private int[] Rows = new int[4];
        private int[] Depth = new int[4];
        private int cellWidth = 0;
        private int cellHeight = 0;
        private int currentPage = 1;
        private int[] top = new int[2];
        private int left = 5;
        string CellCode = "";
        private bool IsWheel = true;

        public frmCellQuery()
        {
            InitializeComponent();
            //设置双缓冲
            SetStyle(ControlStyles.DoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint, true);

            Filter.EnableFilter(dgvMain);
            pnlData.Visible = true;
            pnlData.Dock = DockStyle.Fill;

            pnlChart.Visible = false;
            pnlChart.Dock = DockStyle.Fill;

            pnlChart.MouseWheel += new MouseEventHandler(pnlChart_MouseWheel);

            this.PColor.Visible = false;
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (bsMain.Filter.Trim().Length != 0)
                {
                    DialogResult result = MessageBox.Show("重新读入数据请选择'是(Y)',清除过滤条件请选择'否(N)'", "询问", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    switch (result)
                    {
                        case DialogResult.No:
                            DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn.RemoveFilter(dgvMain);
                            return;
                        case DialogResult.Cancel:
                            return;
                    }
                }
                ShelfCode.Clear();
                ShelfName.Clear();
                DataTable dtShelf = bll.FillDataTable("CMD.SelectShelf");
                for (int i = 1; i <= dtShelf.Rows.Count; i++)
                {
                    ShelfCode.Add(2 * i - 1, dtShelf.Rows[i-1]["ShelfCode"].ToString());
                    ShelfCode.Add(2 * i, dtShelf.Rows[i-1]["ShelfCode"].ToString());
                    ShelfName.Add(2 * i - 1, dtShelf.Rows[i - 1]["ShelfValue"].ToString());
                    ShelfName.Add(2 * i, dtShelf.Rows[i - 1]["ShelfValue"].ToString());
                }
                

                btnRefresh.Enabled = false;
                btnChart.Enabled = false;

                pnlProgress.Top = (pnlMain.Height - pnlProgress.Height) / 3;
                pnlProgress.Left = (pnlMain.Width - pnlProgress.Width) / 2;
                pnlProgress.Visible = true;
                Application.DoEvents();

                cellTable = bll.FillDataTable("WCS.SelectCell");
                bsMain.DataSource = cellTable;

                pnlProgress.Visible = false;
                btnRefresh.Enabled = true;
                btnChart.Enabled = true;
            }
            catch (Exception exp)
            {
                MessageBox.Show("读入数据失败，原因：" + exp.Message);
            }
        }

        private void btnChart_Click(object sender, EventArgs e)
        {
            if (cellTable != null && cellTable.Rows.Count != 0)
            {
                if (pnlData.Visible)
                {
                    this.PColor.Visible = true;
                    filtered = bsMain.Filter != null;
                    needDraw = true;
                    btnRefresh.Enabled = false;
                    pnlData.Visible = false;
                    pnlChart.Visible = true;
                    btnChart.Text = "列表";
                }
                else
                {
                    this.PColor.Visible = false;
                    needDraw = false;
                    btnRefresh.Enabled = true;
                    pnlData.Visible = true;
                    pnlChart.Visible = false;
                    btnChart.Text = "图形";
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pnlChart_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (needDraw)
                {
                    for (int i = 0; i <= 1; i++)
                    {
                        int key = currentPage * 2 + i -1;
                        if (!shelf.ContainsKey(key))
                        {
                            DataRow[] rows = cellTable.Select(string.Format("ShelfCode='{0}' and Depth={1}", ShelfCode[key], i+1), "CellCode desc");
                            shelf.Add(key, rows);
                            ShelfRow.Add(key, int.Parse(rows[0]["Rows"].ToString()));
                            ShelfColumn.Add(key, int.Parse(rows[0]["Columns"].ToString()));
                            SetCellSize(ShelfColumn[key], ShelfRow[key]);
                        }
                        else
                        {
                            DataRow[] rows = cellTable.Select(string.Format("ShelfCode='{0}' and Depth={1} ", ShelfCode[key], i+1), "CellCode desc");
                            shelf[key] = rows;
                        }
                        Font font = new Font("微软雅黑", 10);
                        SizeF size = e.Graphics.MeasureString("1排5层深1", font);
                        float adjustHeight = Math.Abs(size.Height - cellHeight) / 2;
                        size = e.Graphics.MeasureString("13", font);
                        float adjustWidth = (cellWidth - size.Width) / 2;


                        DrawShelf(shelf[key], e.Graphics, top[i], font, adjustWidth);

                        int tmpLeft = left + ShelfColumn[key] * cellWidth + 5;

                        for (int j = 0; j < Rows[currentPage - 1]; j++)
                        {
                            string s = string.Format("{0}排{1}层深{2}", ShelfName[key], Convert.ToString(ShelfRow[key] - j).PadLeft(2, '0'), i + 1);
                            e.Graphics.DrawString(s, font, Brushes.DarkCyan, tmpLeft, top[i] + (j + 1) * cellHeight);
                        }
                    }
                    IsWheel = false;
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        private void DrawShelf(DataRow[] cellRows, Graphics g, int top, Font font, float adjustWidth)
        {
            string shelfCode = "001";
            foreach (DataRow cellRow in cellRows)
            {
                shelfCode = cellRow["ShelfCode"].ToString();
                int column = Convert.ToInt32(cellRow["CellColumn"]);
                string cellCode = cellRow["CellCode"].ToString();

                int row = Rows[currentPage - 1] - Convert.ToInt32(cellRow["CellRow"]) + 1;
                int quantity = ReturnColorFlag(cellRow["PalletBarCode"].ToString(), cellRow["IsActive"].ToString(), cellRow["IsLock"].ToString(), cellRow["ErrorFlag"].ToString(), cellRow["InDate"].ToString());

                int x = left + (column - 1) * cellWidth;
                int y = top + row * cellHeight;

                Pen pen = new Pen(Color.DarkCyan, 2);
                g.DrawRectangle(pen, new Rectangle(x, y, cellWidth, cellHeight));
                FillCell(g, top, row, column, quantity);

            }
            for (int j = 1; j <= Columns[currentPage - 1]; j++)
            {
                if (j == 1 && cellRows.Length < Columns[currentPage - 1] * Rows[currentPage - 1])
                    continue;
                g.DrawString(Convert.ToString(j), new Font("微软雅黑", 10), Brushes.DarkCyan, left + (j - 1) * cellWidth + adjustWidth, top + cellHeight * (Rows[currentPage - 1] + 1) + 3);
            }
        }

        private void FillCell(Graphics g, int top, int row, int column, int quantity)
        {

            //0:空货位，1:空货位锁定 2:有货,3:出库锁定 4:禁用 ,5:有问题 6:入库锁定 7:空箱
            int x = left + (column - 1) * cellWidth;
            int y = top + row * cellHeight;
            if (quantity == 1)  //空货位锁定
                g.FillRectangle(Brushes.Yellow, new Rectangle(x + 2, y + 2, cellWidth - 3, cellHeight - 3));
            else if (quantity == 2) //有货
                g.FillRectangle(Brushes.Blue, new Rectangle(x + 2, y + 2, cellWidth - 3, cellHeight - 3));
            else if (quantity == 3) //出库锁定
                g.FillRectangle(Brushes.Green, new Rectangle(x + 2, y + 2, cellWidth - 3, cellHeight - 3));
            else if (quantity == 4) //禁用
                g.FillRectangle(Brushes.Gray, new Rectangle(x + 2, y + 2, cellWidth - 3, cellHeight - 3));
            else if (quantity == 5) //有问题
                g.FillRectangle(Brushes.Red, new Rectangle(x + 2, y + 2, cellWidth - 3, cellHeight - 3));
            else if (quantity == 6) //入库锁定
                g.FillRectangle(Brushes.LawnGreen, new Rectangle(x + 2, y + 2, cellWidth - 3, cellHeight - 3));
            else if (quantity == 7) //空箱
                g.FillRectangle(Brushes.BlueViolet, new Rectangle(x + 2, y + 2, cellWidth - 3, cellHeight - 3));
        }
        private int ReturnColorFlag(string ProductCode, string IsActive, string IsLock, string ErrFlag, string indate)
        {
            //0:空货位，1:空货位锁定 2:有货,3:出库锁定 4:禁用 ,5:有问题 6:入库锁定 7:空箱
            int Flag = 0;
            if (indate == "")
            {
                if (IsLock == "1" && ProductCode != "")
                    Flag = 6;
                else if (IsLock == "1" && ProductCode == "")
                    Flag = 1;
                else
                    Flag = 0;

            }
            else
            {
                if (ProductCode != "")
                {
                    if (IsLock == "1")
                        Flag = 3;
                    else
                        Flag = 2;
                }
                
            }

            if (IsActive == "0")
                Flag = 4;
            if (ErrFlag == "1")
                Flag = 5;
            return Flag;
        }

        private void pnlChart_Resize(object sender, EventArgs e)
        {
            Columns[0] = 31;
            Columns[1] = 31;
            Columns[2] = 31;
            Columns[3] = 31;
            Rows[0] =10;
            Rows[1] = 10;
            Rows[2] = 10;
            Rows[3] = 10;
            Depth[0] = 2;
            Depth[1] = 2;
            Depth[2] = 2;
            Depth[3] = 2;
            SetCellSize(Columns[currentPage - 1], Rows[currentPage - 1]);
            top[0] = 0;
            top[1] = pnlContent.Height / 2;
        }

        private void SetCellSize(int Columns, int Rows)
        {
            cellWidth = (pnlContent.Width - 90 - sbShelf.Width - 20) / Columns;
            cellHeight = (pnlContent.Height / 2) / (Rows + 2);
        }

        private void pnlChart_MouseClick(object sender, MouseEventArgs e)
        {
            int i = e.Y < top[1] ? 0 : 1;
            int shelf = currentPage * 2 + i - 1;

            int column = (e.X - left) / cellWidth + 1;

            int row = Rows[currentPage - 1] - (e.Y - top[i]) / cellHeight + 1;

            if (column <= Columns[currentPage - 1] && row <= Rows[currentPage - 1])
            {
                string filter = string.Format("ShelfCode='{0}' AND CellColumn='{1}' AND CellRow='{2}'", ShelfCode[shelf], column, row);
                int depth = 1;
                if (shelf == 6)
                    depth = 2;
                DataRow[] cellRows = cellTable.Select(string.Format("ShelfCode='{0}' AND CellColumn='{1}' AND CellRow='{2}' AND Depth={3}", ShelfCode[shelf], column, row, depth));
                if (cellRows.Length != 0)
                    CellCode = cellRows[0]["CellCode"].ToString();
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (cellRows.Length != 0)
                    {
                        if (cellRows[0]["PalletBarCode"].ToString() != "")
                        {
                            //frmCellInfo f = new frmCellInfo(cellRows[0]["PalletBarCode"].ToString(), CellCode);
                            //f.ShowDialog();
                        }
                    }
                }
                 
            }

        }
        private void pnlChart_MouseEnter(object sender, EventArgs e)
        {
            pnlChart.Focus();
        }

        private void pnlChart_MouseWheel(object sender, MouseEventArgs e)
        {
            IsWheel = true;
            if (e.Delta < 0 && currentPage + 1 <= 4)
                sbShelf.Value = (currentPage) * 30;
            else if (e.Delta > 0 && currentPage - 1 >= 1)
                sbShelf.Value = (currentPage - 2) * 30;
        }

        private void sbShelf_ValueChanged(object sender, EventArgs e)
        {
            int pos = sbShelf.Value / 30 + 1;
            if (pos > 4)
                return;
            if (pos != currentPage)
            {
                currentPage = pos;
                pnlChart.Invalidate();
                cellWidth = (pnlContent.Width - 90 - sbShelf.Width - 20) / Columns[currentPage - 1];
                cellHeight = (pnlContent.Height / 2) / (Rows[currentPage - 1] + 2);
            }
        }

        private void dgvMain_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgvMain.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dgvMain.RowHeadersDefaultCellStyle.Font, rectangle, dgvMain.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

    
        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataRow[] drs = cellTable.Select(string.Format("CellCode='{0}'", CellCode));
            if (drs.Length > 0)
            {
                DataRow dr = drs[0];
                frmCellOpDialog cellDialog = new frmCellOpDialog(dr);
                if (cellDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    cellTable = bll.FillDataTable("WCS.SelectCell");
                    bsMain.DataSource = cellTable;
                    pnlChart.Invalidate();
                }
            }
        }

        private int X, Y;
        private void pnlChart_MouseMove(object sender, MouseEventArgs e)
        {

            if (IsWheel) return;
            if (X != e.X || Y != e.Y)
            {
                int i = e.Y < top[1] ? 0 : 1;

                int shelf = currentPage * 2 + i - 1;

                int column = (e.X - left) / cellWidth + 1;
                int row = Rows[currentPage - 1] - (e.Y - top[i]) / cellHeight + 1;

                if (column <= Columns[currentPage - 1] && row <= Rows[currentPage - 1] && row > 0 && column > 0)
                {
                    DataRow[] cellRows = cellTable.Select(string.Format("ShelfCode='{0}' AND CellColumn='{1}' AND CellRow='{2}' AND Depth={3} ", ShelfCode[shelf], column, row, (i + 1)));
                    string tip = "";
                    if (cellRows.Length != 0)
                    {
                        CellCode = cellRows[0]["CellCode"].ToString();
                        if (cellRows[0]["PalletBarCode"].ToString() != "")
                        {
                            tip = "货位:" + cellRows[0]["CellName"].ToString() + "产品:" + cellRows[0]["ProductName"].ToString() + Environment.NewLine +
                                  "批次：" + cellRows[0]["BatchNo"].ToString() + "阶段：" + cellRows[0]["SectionName"].ToString();

                        }
                    }
                    if (tip != "")
                        toolTip1.SetToolTip(pnlChart, tip);
                    else
                        toolTip1.SetToolTip(pnlChart, null);
                }
                else
                    toolTip1.SetToolTip(pnlChart, null);

                X = e.X;
                Y = e.Y;
            }
        }

        

        private void btnReQuery_Click(object sender, EventArgs e)
        {
            cellTable = bll.FillDataTable("WCS.SelectCell");
            bsMain.DataSource = cellTable;
            pnlChart.Invalidate();
        }
    }
}
