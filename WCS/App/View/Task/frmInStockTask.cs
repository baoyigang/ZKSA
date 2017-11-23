using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Util;
using MCP;

namespace App.View.Task
{
    public partial class frmInStockTask :BaseForm
    {

        int conveyID;
        
        public frmInStockTask()
        {
            InitializeComponent();
        }
        public frmInStockTask(int ErrCode)
        {
            InitializeComponent();
            conveyID = ErrCode;
        }
        private void frmInStockTask_Load(object sender, EventArgs e)
        {
            if (conveyID == 1)
            {
                this.cmbStationNo.SelectedIndex = 0;
            }
            else
            {
                if (conveyID == 101)
                    this.cmbStationNo.SelectedIndex = 0;
                else
                    this.cmbStationNo.SelectedIndex = 1;
            }

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            string PalletCode = this.txtBarcode.Text.Trim();
            string ConveyID = this.cmbStationNo.Text;
            int i = PalletCode.IndexOf((char)3);
            if (i > 0)
                PalletCode = PalletCode.Substring(0, i);
            string HightFlag = ObjectUtil.GetObject(Context.ProcessDispatcher.WriteToService("Convey", ConveyID + "_HightFlag")).ToString();
            if (HightFlag == "5" || HightFlag == "6")
            {

                //根據條碼,獲取任务；先在WCS_Task中獲取任务,如無任务,則在中間表獲取
                DataParameter[] paras = new DataParameter[] { new DataParameter("{0}", string.Format("TaskType in ('11','99') and State in (0,1) and Palletcode='{0}'", PalletCode)) };
                BLL.BLLBase bllStock = new BLL.BLLBase("StockDB");
                DataTable dt = bllStock.FillDataTable("WCS.SelectConveyTask", paras);
                bool blnHasTask = false;
                if (dt.Rows.Count == 0)
                {
                    BLL.BLLBase bllMiddle = new BLL.BLLBase("MiddleDB");
                    dt = bllMiddle.FillDataTable("Middle.SelectInStockRequestTask", new DataParameter[] { new DataParameter("@Device", "UL"), new DataParameter("{0}", string.Format("hu_id='{0}'", PalletCode)) });
                    if (dt.Rows.Count > 0)
                    {
                        blnHasTask = true;
                        DataRow[] drs = dt.Select("len(from_location_id)=3");
                        if (drs.Length > 0)
                        {
                            drs[0]["location_id"] = ConveyID;
                            dt.AcceptChanges();
                        }
                        BLL.Server.InsertTaskToWcs(dt, true);
                    }

                }
                if (blnHasTask)
                    dt = bllStock.FillDataTable("WCS.SelectConveyTask", paras);
                if (dt.Rows.Count > 0)
                {
                    //判斷所在巷道是否擁堵
                    bool bln = false;
                    if (bln)
                    {
                        Context.ProcessDispatcher.WriteToService("Convey", ConveyID + "_WriteFinished", 2);
                    }
                    else
                    {

                        if (dt.Rows[0]["HeightType"].ToString().ToLower() == "l" && HightFlag == "6")
                        {
                            MessageBox.Show("輸送線" + conveyID + " 托盤超高,不能掃碼入庫!", "提示");
                            return;
                        }

                      
                        string TaskNo = dt.Rows[0]["TaskNo"].ToString();
                        string SubTaskID = dt.Rows[0]["subtaskid"].ToString();
                        string Destination = dt.Rows[0]["ToStation"].ToString();

                        sbyte[] sTaskNo = new sbyte[20];
                        Util.ConvertStringChar.stringToBytes(TaskNo, 20).CopyTo(sTaskNo, 0);
                        sbyte[] sPalletCode = new sbyte[30];
                        Util.ConvertStringChar.stringToBytes(PalletCode, 30).CopyTo(sPalletCode, 0);

                        //更新開始入庫

                        Context.ProcessDispatcher.WriteToService("Convey", ConveyID + "_WTaskNo", sTaskNo);
                        Context.ProcessDispatcher.WriteToService("Convey", ConveyID + "_WPalletCode", sPalletCode);
                        Context.ProcessDispatcher.WriteToService("Convey", ConveyID + "_Destination", Destination); //目的地
                        Context.ProcessDispatcher.WriteToService("Convey", ConveyID + "_WTaskType", 1); //托盤類型
                        int WTaskFlag = 0;
                        if (dt.Rows[0]["Type"].ToString() == "MRGPUT")//是否并板
                            WTaskFlag = 1;
                        Context.ProcessDispatcher.WriteToService("Convey", ConveyID + "_WTaskFlag", WTaskFlag);
                        if (Context.ProcessDispatcher.WriteToService("Convey", ConveyID + "_WriteFinished", 3))
                        {
                            //List<string> comds = new List<string>();
                            //List<DataParameter[]> Paras = new List<DataParameter[]>();
                            //comds.Add("WCS.UpdateTaskState");
                            //Paras.Add(new DataParameter[] { new DataParameter("{0}", "State=1,Convey_StartDate=getdate()"),
                            //                                new DataParameter("{1}", string.Format("TaskNo='{0}'", TaskNo))}
                            //         );

                            //comds.Add("WCS.UpdateTaskTmpStatus");
                            //Paras.Add(new DataParameter[] { new DataParameter("@status", "STR"),
                            //                                new DataParameter("@subtaskid", SubTaskID)});
                            //bllStock.ExecTran(comds.ToArray(), Paras);
                            bllStock.ExecNonQuery("WCS.UpdateTaskState", new DataParameter[] { new DataParameter("{0}", "State=1,Convey_StartDate=getdate()"),
                                                                                               new DataParameter("{1}", string.Format("TaskNo='{0}'", TaskNo))});
                            bllStock.ExecNonQuery("WCS.UpdateTaskTmpStatus", new DataParameter[] { new DataParameter("@status", "STR"),new DataParameter("@subtaskid", SubTaskID)});

                            
                            Logger.Info("任务號:" + TaskNo + " 托盤號:" + PalletCode + " 開始入庫,目的地址:" + Destination);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("找不到任务,托盤號:" + PalletCode + "不能掃碼入庫!", "提示");
                    this.txtBarcode.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show("輸送線" + ConveyID + " 不存在讀碼錯誤，不能掃碼入庫!", "提示");
                return;
            }
            this.DialogResult = DialogResult.OK;

        }

        private void frmInStockTask_Activated(object sender, EventArgs e)
        {
            this.txtBarcode.Focus();
        }
        private void cbHeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtBarcode.Focus();
        }

          
    }
}
