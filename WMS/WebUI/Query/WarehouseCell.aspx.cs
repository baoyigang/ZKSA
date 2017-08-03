using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Util;

public partial class WebUI_Query_WarehouseCell : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string WareHouse = Request.QueryString["WareHouse"].ToString();
        string ShelfCode = Request.QueryString["ShelfCode"].ToString();
        string AreaCode = Request.QueryString["AreaCode"].ToString();
        BLL.BLLBase bll = new BLL.BLLBase();
        writeJsvar("", "", "");
        DataTable tableCell;
        if (WareHouse!=""&&AreaCode=="")
        {
            tableCell = bll.FillDataTable("CMD.SelectWareHouseCellQueryByWareHouse", new DataParameter[] { new DataParameter("@WareHouse", WareHouse) });
            ShowWareHouseChart(tableCell);
        }
        else if(AreaCode!="" && ShelfCode=="")
        {
            tableCell = bll.FillDataTable("CMD.SelectWareHouseCellQueryByArea", new DataParameter[] { new DataParameter("@AreaCode", AreaCode) });
            ShowCellChart(tableCell);
        }
        else
        {
            tableCell = bll.FillDataTable("CMD.SelectWareHouseCellQueryByShelf", new DataParameter[] { new DataParameter("{0}", string.Format("ShelfCode='{0}' and AreaCode='{1}'", ShelfCode, AreaCode)) });
            ShowCellChart(tableCell);
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Resize", "resize();", true);
    }

    private void ShowCellChart(DataTable tableCell)
    {
        this.pnlCell.Controls.Clear();
        if (tableCell.Rows.Count == 0)
            return;
        DataTable dtShelf=tableCell.DefaultView.ToTable(true,"AreaCode","ShelfCode");
        for (int i = 0; i < dtShelf.Rows.Count; i++)
        {

            Table shelfchar = CreateShelfChart(dtShelf.Rows[i]["AreaCode"].ToString(), dtShelf.Rows[i]["ShelfCode"].ToString());
            this.pnlCell.Controls.Add(shelfchar);
        }
    }

    protected void ShowWareHouseChart(DataTable tableCell)
    {
        this.pnlCell.Controls.Clear();
        if (tableCell.Rows.Count == 0)
            return;
        DataTable dtShelf = tableCell.DefaultView.ToTable(true, "ShelfCode");
        for (int i = 0; i <dtShelf.Rows.Count; i++)
        {
            Table shelfchar = CreateShelfChart("",dtShelf.Rows[i]["ShelfCode"].ToString());
            this.pnlCell.Controls.Add(shelfchar);
        }
            
    }
    protected Table CreateShelfChart(string AreaCode, string shelfCode)
    {
        BLL.BLLBase bll = new BLL.BLLBase();

        string strWhere = "";
        if (AreaCode == "")
            strWhere = string.Format("ShelfCode='{0}'", shelfCode);
        else
            strWhere = string.Format("ShelfCode='{0}' and AreaCode='{1}'", shelfCode, AreaCode);

        DataTable ShelfCell = bll.FillDataTable("CMD.SelectWareHouseCellQueryByShelf", new DataParameter[] { new DataParameter("{0}", strWhere) });

        int Rows = int.Parse(ShelfCell.Rows[0]["Rows"].ToString());
        int Columns = int.Parse(ShelfCell.Rows[0]["Columns"].ToString());
        string Width = (90.0 / (double)Columns) + "%";
        Table tb = new Table();
        string tbstyle = "width:100%";
        tb.Attributes.Add("style", tbstyle);
        for (int i = Rows; i >= 1; i--)
        {
            TableRow row = new TableRow();
            for (int j = 1; j <= Columns; j++)
            {
                if (AreaCode == "")
                    strWhere = string.Format("CellRow={0} and CellColumn={1}", i, j);
                else
                    strWhere = string.Format("CellRow={0} and CellColumn={1} and AreaCode='{2}'", i, j, AreaCode);


                DataRow[] drs = ShelfCell.Select(strWhere, "");
                if (drs.Length > 0)
                {
                    TableCell cell = new TableCell();
                    cell.ID = drs[0]["CellCode"].ToString();

                    string style = "height:25px;width:" + Width + ";border:2px solid #008B8B;";
                    string backColor = ReturnColorFlag(drs[0]["BarCode"].ToString(), drs[0]["IsActive"].ToString(), drs[0]["IsLock"].ToString(), drs[0]["ErrorFlag"].ToString(),drs[0]["IsTurnover"].ToString(), ToYMD(drs[0]["InDate"]));
                    style += "background-color:" + backColor + ";";
                    /*if (drs[0]["PalletCode"].ToString() != "")
                    {
                        style += "background-color:" + backColor + ";";
                    }*/
                    cell.Attributes.Add("style", style);
                    cell.Attributes.Add("onclick", "ShowCellInfo('" + cell.ID + "');");
                    row.Cells.Add(cell);
                }
                else
                {
                    TableCell cell = new TableCell();
                    string style = "height:25px;width:" + Width + ";border:0px solid #008B8B";

                    cell.Attributes.Add("style", style);

                    row.Cells.Add(cell);
                }
                if (j == Columns)
                {
                    TableCell cellTag = new TableCell();
                    cellTag.Attributes.Add("style", "height:25px;border:0px solid #008B8B");
                    cellTag.Attributes.Add("align", "right");
                    cellTag.Text = "<font color=\"#008B8B\"> 第" + int.Parse(shelfCode.Substring(4, 2)).ToString() + "排第" + i.ToString() + "层</font>";
                    row.Cells.Add(cellTag);
                }
            }
            tb.Rows.Add(row);

            if (i == 1)
            {
                TableRow rowNum = new TableRow();
                for (int j = 1; j <= Columns; j++)
                {
                    string K = j.ToString();
                    TableCell cellNum = new TableCell();
                    cellNum.Attributes.Add("style", "height:40px;width:" + Width.ToString() + "px;border:0px solid #008B8B");
                    cellNum.Attributes.Add("align", "center");
                    cellNum.Attributes.Add("Valign", "top");
                    cellNum.Text = "<font color=\"#008B8B\">" + K + "</font>";

                    rowNum.Cells.Add(cellNum);
                }
                tb.Rows.Add(rowNum);
            }
        }
        return tb;
    }
    private string ReturnColorFlag(string PalletCode, string IsActive, string IsLock, string ErrFlag, string Isturnonve,string Indate)
    {
        string Flag = "";
        if (PalletCode == "" || Indate == "") //空货位锁定
        {
            if (IsLock == "1")
            {
                Flag = "Yellow";
            }
            if (Isturnonve=="1")
            {
                Flag = "Black";
            }
        }
        else
        {
            if (IsLock == "1")
            {
                Flag = "Green";
            }
            else
            {
                Flag = "Blue";
            }
        }
        if (IsActive == "0")
            Flag = "Gray";
        if (ErrFlag == "1")
            Flag = "Red";
        return Flag;
    }
}