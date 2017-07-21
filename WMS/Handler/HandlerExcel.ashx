<%@ WebHandler Language="C#" Class="HandlerExcel" %>

using System;
using System.Web;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Data;
using Util;
using System.IO;

public class HandlerExcel : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        try
        {
            string Path = System.AppDomain.CurrentDomain.BaseDirectory + @"ExcelLoad\";
            DirectoryInfo di = new DirectoryInfo(Path);
            foreach (FileInfo fi in di.GetFiles())
            {
                if (fi.Name.IndexOf("信息") > 0)
                {
                    fi.Delete();
                }
            }
            BLL.BLLBase bll = new BLL.BLLBase();
            DataTable dtView = bll.FillDataTable("Cmd.SelectProduct", new DataParameter[] { new DataParameter("{0}", "1=1"), new DataParameter("{1}", "1") });
            int dtRows = dtView.Rows.Count;
            IWorkbook wk = new HSSFWorkbook();
            ISheet sheet = wk.CreateSheet("产品信息");
            IRow rowHead = sheet.CreateRow(0);
            rowHead.CreateCell(0).SetCellValue("模具编号");
            rowHead.CreateCell(1).SetCellValue("父模具编号");
            rowHead.CreateCell(2).SetCellValue("托盘编号");
            rowHead.CreateCell(3).SetCellValue("产品类别");
            rowHead.CreateCell(4).SetCellValue("产品编号");
            rowHead.CreateCell(5).SetCellValue("品名");
            rowHead.CreateCell(6).SetCellValue("标准冲程");
            rowHead.CreateCell(7).SetCellValue("入库日期");
            rowHead.CreateCell(8).SetCellValue("存放区域");
            rowHead.CreateCell(9).SetCellValue("客户备注");
            rowHead.CreateCell(10).SetCellValue("开发备注");
            rowHead.CreateCell(11).SetCellValue("状态");
            for (int i = 0; i < dtRows; i++)
            {
                IRow currentRow = sheet.CreateRow(i + 1);
                currentRow.CreateCell(0).SetCellValue(dtView.Rows[i]["ProductCode"].ToString());
                currentRow.CreateCell(1).SetCellValue(dtView.Rows[i]["ModelNo"].ToString());
                currentRow.CreateCell(2).SetCellValue(dtView.Rows[i]["StandardNo"].ToString());
                currentRow.CreateCell(3).SetCellValue(dtView.Rows[i]["CategoryName"].ToString());
                currentRow.CreateCell(4).SetCellValue(dtView.Rows[i]["ProductNo"].ToString());
                currentRow.CreateCell(5).SetCellValue(dtView.Rows[i]["ProductName"].ToString());
                currentRow.CreateCell(6).SetCellValue(dtView.Rows[i]["Weight"].ToString());
                currentRow.CreateCell(7).SetCellValue(dtView.Rows[i]["ValidPeriod"].ToString());
                currentRow.CreateCell(8).SetCellValue(dtView.Rows[i]["Instock"].ToString());
                currentRow.CreateCell(9).SetCellValue(dtView.Rows[i]["Description"].ToString());
                currentRow.CreateCell(10).SetCellValue(dtView.Rows[i]["Memo"].ToString());
                currentRow.CreateCell(11).SetCellValue(dtView.Rows[i]["PartNo"].ToString());
            }
            string fileName = "产品信息" + DateTime.Now.ToString("yyyy-MM-dd");
            string path = Path + fileName + ".xls";
            using (FileStream fsWrite = File.OpenWrite(path))
            {
                wk.Write(fsWrite);
            }
            FileInfo file = new System.IO.FileInfo(path);
            context.Response.Clear();
            context.Response.Charset = "UTF-8";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            context.Response.AddHeader("Content-Disposition", "attachment; filename=" + context.Server.UrlEncode(file.Name));
            context.Response.AddHeader("Content-Length", file.Length.ToString());
            context.Response.ContentType = "application/ms-excel";
            context.Response.WriteFile(file.FullName);
            context.ApplicationInstance.CompleteRequest();
            //context.Response.End();
        }
        catch (Exception ex)
        {
            context.Response.Clear();
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            context.Response.ContentType = "text/html";
            context.Response.Write(ex.Message);
            context.ApplicationInstance.CompleteRequest();
            //context.Response.End();  
        }   
    } 
    public bool IsReusable {
        get {
            return false;
        }
    }
}