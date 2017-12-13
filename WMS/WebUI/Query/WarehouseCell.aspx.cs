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
        string RegionCode = Request.QueryString["RegionCode"].ToString();
        string AreaCode = Request.QueryString["AreaCode"].ToString();
        BLL.BLLBase bll = new BLL.BLLBase();
         
        DataTable tableCell;
        if (WareHouse!=""&&AreaCode=="")
        {
            tableCell = bll.FillDataTable("CMD.SelectWareHouseCellQueryByWareHouse", new DataParameter[] { new DataParameter("@WareHouse", WareHouse) });
            ShowCellChart(tableCell);
        }
        else if(AreaCode!="" && RegionCode=="")
        {
            tableCell = bll.FillDataTable("CMD.SelectWareHouseCellQueryByArea", new DataParameter[] { new DataParameter("@AreaCode", AreaCode) });
            ShowCellChart(tableCell);
        }
        else
        {
            tableCell = bll.FillDataTable("CMD.SelectWareHouseCellQueryByWhere", new DataParameter[] { new DataParameter("{0}", string.Format("RegionCode='{0}' and AreaCode='{1}'", RegionCode, AreaCode)) });
            ShowCellChart(tableCell);
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Resize", "resize();", true);
    }

    #region 显示货位图表
 

    protected void ShowCellChart(DataTable tableCell)
    {
        this.pnlCell.Controls.Clear();
        if (tableCell.Rows.Count == 0)
            return;
        DataTable dtShelf = tableCell.DefaultView.ToTable(true, "ShelfCode");
        for (int i = 0; i < dtShelf.Rows.Count; i++)
        {
            Table shelfchar = CreateShelfChart(tableCell, dtShelf.Rows[i]["ShelfCode"].ToString());
            this.pnlCell.Controls.Add(shelfchar);
        }
    }

    //货架显示图；
    protected Table CreateShelfChart(DataTable dtCell, string shelfCode)
    {
        BLL.BLLBase bll = new BLL.BLLBase();

        string strWhere = "";

        strWhere = string.Format("ShelfCode='{0}'", shelfCode);





        int Rows = int.Parse(dtCell.Rows[0]["Rows"].ToString());
        int Columns = int.Parse(dtCell.Rows[0]["Columns"].ToString());
        string Width = (90.0 / Columns) + "%";
        int Depth = int.Parse(dtCell.Rows[0]["Depths"].ToString());


        Table tb = new Table();
        string tbstyle = "width:100%";
        tb.Attributes.Add("style", tbstyle);

        for (int dh = 1; dh <= Depth; dh++)
        {
            for (int i = Rows; i >= 1; i--)
            {
                TableRow row = new TableRow();
                for (int j = 1; j <= Columns; j++)
                {

                    strWhere = string.Format("CellRow={0} and CellColumn={1} and Depth={2} and shelfCode='{3}'", i, j, dh, shelfCode);
                    DataRow[] drs = dtCell.Select(strWhere, "");

                    if (drs.Length > 0)
                    {
                        TableCell cell = new TableCell();
                        cell.ID = drs[0]["CellCode"].ToString();
                        
                        string style = "height:25px;width:" + Width + ";border:2px solid #008B8B;";
                        string backColor = ReturnColorFlag(drs[0]["PalletBarCode"].ToString(), drs[0]["IsActive"].ToString(), drs[0]["IsLock"].ToString(), drs[0]["ErrorFlag"].ToString(),  ToYMD(drs[0]["InDate"]));
                        style += "background-color:" + backColor + ";";
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
                        cellTag.Text = "<font color=\"#008B8B\"> 第" + int.Parse(shelfCode.Substring(4, 2)).ToString() + "排第" + i.ToString() + "层深" + dh.ToString() + "</font>";
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
        }
        return tb;

    }
    private string ReturnColorFlag(string ProductCode, string IsActive, string IsLock, string ErrFlag, string Indate)
    {
        string Flag = "White";
        if (ProductCode != "") //空货位锁定 
        {
            if (IsLock == "1")
            {
                if (Indate == "")
                    Flag = "LawnGreen";
                else
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
    #endregion
}