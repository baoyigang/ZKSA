using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FastReport;
using System.Data;
using Util;

public partial class WebUI_Query_OutStockQuery : BasePage
{
    BLL.BLLBase bll = new BLL.BLLBase();
    private string strWhere;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            rptview.Visible = false;
            //this.txtEndDate.DateValue = DateTime.Now;
            //this.txtStartDate.DateValue = DateTime.Now.AddMonths(-1);
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
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        rptview.Visible = true;
        WebReport1.Refresh();
    }
    private void GetStrWhere()
    {
        strWhere = "1=1";
        //if (this.txtStartDate.tDate.Text != "")
        //{
        //    strWhere += string.Format(" and CONVERT(nvarchar(10),FinishDate,111)>='{0}'", this.txtStartDate.tDate.Text);
        //}
        //if (this.txtEndDate.tDate.Text != "")
        //{
        //    strWhere += string.Format(" and CONVERT(nvarchar(10),FinishDate,111)<='{0}'", this.txtEndDate.tDate.Text);
        //}
        strWhere += string.Format(" and Detail.ProductCode like '%{0}%'", this.txtProductCode.Text);
        strWhere += string.Format(" and product.ProductName like '%{0}%'", this.txtProductName.Text);
        strWhere += string.Format(" and product.ProductNo like '%{0}%'", this.txtProductNo.Text);

    }
    protected void WebReport1_StartReport(object sender, EventArgs e)
    {
        if (!rptview.Visible) return;
        LoadRpt();
    }
    private bool LoadRpt()
    {
        try
        {
            GetStrWhere();
            string frx = "OutStockQuery.frx";
            string Comds = "WMS.SelectProductQty";


            WebReport1.Report = new Report();
            WebReport1.Report.Load(System.AppDomain.CurrentDomain.BaseDirectory + @"RptFiles\" + frx);

            BLL.BLLBase bll = new BLL.BLLBase();

            DataTable dt = bll.FillDataTable(Comds, new DataParameter[] { new DataParameter("{0}", strWhere) });



            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('您所选择的条件没有资料!');", true);
            }

            WebReport1.Report.RegisterData(dt, "OutStockQuery");
        }
        catch (Exception ex)
        {
        }
        return true;
    }

}