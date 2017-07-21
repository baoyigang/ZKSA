using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FastReport;
using FastReport.Data;
using FastReport.Utils;
using System.Data;
using System.IO;
using Util;


public partial class WebUI_Query_BarCodeQuery: BasePage
{
    private string strWhere;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            rptview.Visible = false;
            rptview2.Visible = false;
            BindOther();
            writeJsvar("", "", "");

        }
        else
        {
            string hdnwh = HdnWH.Value;
            int W = int.Parse(hdnwh.Split('#')[0]);
            int H = int.Parse(hdnwh.Split('#')[1]);
            WebReport1.Width = W - 60;
            WebReport1.Height = H - 55;
        }

    }
    
    //private void BindSelect() 
    //{
    //    DataTable dt = new DataTable();
    //    dt.Columns.Add("SType");
    //    dt.Rows.Add("普通查询");
    //    dt.Rows.Add("范围查询");
    //    dt.AcceptChanges();

    //    this.SelectType.DataTextField = "SType";
    //    this.SelectType.DataSource = dt;
    //    this.SelectType.DataBind();
    //}

    private void BindOther()
    {
        BLL.BLLBase bll = new BLL.BLLBase();
        DataTable ProductType = bll.FillDataTable("Cmd.SelectArea", new DataParameter[] { new DataParameter("{0}", "1=1") });
        DataRow dr = ProductType.NewRow();
        dr["AreaCode"] = "";
        dr["AreaName"] = "请选择";
        ProductType.Rows.InsertAt(dr, 0);
        ProductType.AcceptChanges();

        this.ddlProductType.DataValueField = "AreaCode";
        this.ddlProductType.DataTextField = "AreaName";
        this.ddlProductType.DataSource = ProductType;
        this.ddlProductType.DataBind();

    }
    protected void WebReport1_StartReport(object sender, EventArgs e)
    {
        if (!rptview.Visible) return;
        LoadRpt();
    }

    protected void WebReport2_StartReport(object sender, EventArgs e)
    {
        if (!rptview2.Visible) return;
        LoadRpt1();
    }

    protected void btnPreview_Click(object sender, EventArgs e)
    {
        rptview2.Visible = false;
        rptview.Visible = true;
        WebReport1.Refresh();
    }
    private void GetStrWhere()
    {
        strWhere = "IsTurnover=0";
        if (this.ddlProductType.SelectedValue != "")
        {
            strWhere += string.Format(" and AreaCode='{0}'", this.ddlProductType.SelectedValue);
        }


        if (this.HdnProduct.Value.Length == 0)
        {
            if (this.txtProductCode.Text.Trim().Length > 0)
                strWhere += string.Format(" and CellCode like '%{0}%'", this.txtProductCode.Text);
        }
        else
        {
            strWhere += " and CellCode in (" + this.HdnProduct.Value + ") ";
        }
        

    }
    private bool LoadRpt()
    {
        try
        {
            GetStrWhere();
            string frx = "BarCodeQuery.frx";
            string Comds = "Cmd.SelectCell";

           
            WebReport1.Report = new Report();
            WebReport1.Report.Load(System.AppDomain.CurrentDomain.BaseDirectory + @"RptFiles\" + frx);

            BLL.BLLBase bll = new BLL.BLLBase();

            DataTable dt = bll.FillDataTable(Comds, new DataParameter[] { new DataParameter("{0}", strWhere) });



            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('您所选择的条件没有资料!');", true);
            }

            WebReport1.Report.RegisterData(dt, "BarCode");
        }
        catch (Exception ex)
        {
        }
        return true;
    }
    private bool LoadRpt1()
    {
        try
        {
            strWhere = "1=1";
            strWhere += string.Format("and CellCode between '{0}' AND '{1}'", SelectBegin.Text, SelectEnd.Text);
            string frx = "BarCodeQuery.frx";
            string Comds = "Cmd.SelectCell";


            WebReport2.Report = new Report();
            WebReport2.Report.Load(System.AppDomain.CurrentDomain.BaseDirectory + @"RptFiles\" + frx);

            BLL.BLLBase bll = new BLL.BLLBase();

            DataTable dt = bll.FillDataTable(Comds, new DataParameter[] { new DataParameter("{0}", strWhere) });



            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('您所选择的条件没有资料!');", true);
            }

            WebReport2.Report.RegisterData(dt, "BarCode");
        }
        catch (Exception ex)
        {
        }
        return true;
    }

    protected void btnArea_Click(object sender, EventArgs e)
    {
        rptview.Visible = false;
        rptview2.Visible = true;
        WebReport2.Refresh();
    }
}
 