﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MCP;
using Util;
using DataGridViewAutoFilter;

namespace App
{
    public partial class Main : Form
    {
        private bool IsActiveForm = false;
        public bool IsActiveTab = false;
        private Context context = null;
        private System.Timers.Timer tmWorkTimer = new System.Timers.Timer();
        BLL.BLLBase bll = new BLL.BLLBase();
        private Dictionary<string, int> PLCShelf = new Dictionary<string, int>();
       
       
        
        public Main()
        {
            InitializeComponent();
        }

        #region Main方法

        private void Main_Shown(object sender, EventArgs e)
        {
            try
            {
                SetBtnEnabled(false);

                lbLog.Scrollable = true;
                Logger.OnLog += new LogEventHandler(Logger_OnLog);
            
                context = new Context();
                ContextInitialize initialize = new ContextInitialize();
                initialize.InitializeContext(context);

                //View.frmMonitor f = new View.frmMonitor();
                //ShowForm(f);
                //MainData.OnTask += new TaskEventHandler(Data_OnTask);
                //this.BindData();
                //for (int i = 0; i < this.dgvMain.Columns.Count - 1; i++)
                //    ((DataGridViewAutoFilterTextBoxColumn)this.dgvMain.Columns[i]).FilteringEnabled = true;

                tmWorkTimer.Interval = 3000;
                tmWorkTimer.Elapsed += new System.Timers.ElapsedEventHandler(tmWorker);
                tmWorkTimer.Start();
            }
            catch (Exception ee)
            {
                Logger.Error("初始化配置检查失败,原因:" + ee.Message);
            }
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("您确定要退出调度系统吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                Logger.Info("退出系统");
                System.Environment.Exit(0);
            }
            else
                e.Cancel = true;
        }

        #endregion

        private void tmWorker(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
               

                tmWorkTimer.Stop();
                DataTable dt = GetMonitorData();
                MainData.TaskInfo(dt);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            finally
            {
                tmWorkTimer.Start();
            }
        }
        #region 日志
        void Logger_OnLog(MCP.LogEventArgs args)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new LogEventHandler(Logger_OnLog), args);
            }
            else
            {
                lock (lbLog)
                {
                    string msg1 = string.Format("[{0}]", args.LogLevel);
                    string msg2 = string.Format("{0}", DateTime.Now.ToString("yy/MM/dd HH:mm:ss"));
                    string msg3 = string.Format("{0} ", args.Message);
                    if (args.LogLevel != LogLevel.DEBUG)
                    {
                        this.lbLog.BeginUpdate();
                        ListViewItem item = new ListViewItem(new string[] { msg1, msg2, msg3 });

                        if (msg1.Contains("[ERROR]"))
                        {
                            //item.ForeColor = Color.Red;
                            item.BackColor = Color.Red;
                        }
                        lbLog.Items.Insert(0, item);
                        this.lbLog.EndUpdate();
                    }
                    WriteLoggerFile(msg1 + " " + msg2 + "  " + msg3);
                }
                 
            }
        }
       

        private void CreateDirectory(string directoryName)
        {
            if (!System.IO.Directory.Exists(directoryName))
                System.IO.Directory.CreateDirectory(directoryName);
        }

        private void WriteLoggerFile(string text)
        {
            try
            {
                string path = "";
                CreateDirectory("Log");
                path = "Log";
                path = path + @"/" + DateTime.Now.ToString().Substring(0, 4).Trim();
                CreateDirectory(path);
                path = path + @"/" + DateTime.Now.ToString("yyyy-MM-dd").Substring(0, 7).Trim();
                path = path.TrimEnd(new char[] { '-' });
                CreateDirectory(path);
                path = path + @"/" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                System.IO.File.AppendAllText(path, string.Format("{0}",  text + "\r\n"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 打开一个窗体

        /// </summary>
        /// <param name="frm"></param>
        private void ShowForm(Form frm)
        {
            if (OpenOnce(frm))
            {
                frm.MdiParent = this;
                ((View.BaseForm)frm).Context = context;
                frm.Show();
                frm.WindowState = FormWindowState.Maximized;
                AddTabPage(frm.Handle.ToString(), frm.Text);
            }
        }
        /// <summary>
        /// 判断窗体是否已打开
        /// </summary>
        /// <param name="frm"></param>
        /// <returns></returns>
        private bool OpenOnce(Form frm)
        {
            foreach (Form mdifrm in this.MdiChildren)
            {
                int index = mdifrm.Text.IndexOf(" ");
                if (index > 0)
                {
                    if (frm.Name == mdifrm.Name && frm.Text == mdifrm.Text.Substring(0, index))
                    {
                        mdifrm.Activate();
                        return false;
                    }
                }
                else
                {
                    if (frm.Name == mdifrm.Name && frm.Text == mdifrm.Text)
                    {
                        mdifrm.Activate();
                        return false;
                    }
                }
            }
            return true;

        }

        private void AddTabPage(string strKey, string strText)
        {
            IsActiveForm = true;
            TabPage tab = new TabPage();
            tab.Name = strKey.ToString();
            tab.Text = strText;
            tabForm.TabPages.Add(tab);
            tabForm.SelectedTab = tab;
            this.pnlTab.Visible = true;
            IsActiveForm = false;
        }

        public void SetActiveTab(string strKey, bool blnActive)
        {
            foreach (TabPage tab in this.tabForm.TabPages)
            {
                if (tab.Name == strKey)
                {
                    IsActiveForm = true;

                    if (blnActive)
                        tabForm.SelectedTab = tab;
                    else
                    {
                        tabForm.TabPages.Remove(tab);
                        if (this.MdiChildren.Length > 1)
                            this.pnlTab.Visible = true;
                        else
                            this.pnlTab.Visible = false;
                    }

                    IsActiveForm = false; ;
                }
            }
        }
        private void tabForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsActiveForm)
                return;
            foreach (Form mdifrm in this.MdiChildren)
            {
                if (mdifrm.Handle.ToInt32() == int.Parse(((TabControl)sender).SelectedTab.Name))
                {
                    IsActiveTab = true;
                    mdifrm.Activate();
                    IsActiveTab = false;
                }
            }
        }
        #endregion


        private void toolStripButton_Close_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("您确定要退出调度系统吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                Logger.Info("退出系统");
                System.Environment.Exit(0);
            }
        }

        private void toolStripButton_StartCrane_Click(object sender, EventArgs e)
        {

            if (this.toolStripButton_StartCrane.Text == "联机自动")
            {
                context.ProcessDispatcher.WriteToProcess("CraneProcess", "Run", 1);
                context.ProcessDispatcher.WriteToProcess("ElevatorProcess", "Run", 1);
                context.ProcessDispatcher.WriteToProcess("AGVProcess", "Run", 1);
                this.toolStripButton_StartCrane.Image = App.Properties.Resources.process_accept;
                this.toolStripButton_StartCrane.Text = "脱机";
            }
            else
            {
                context.ProcessDispatcher.WriteToProcess("CraneProcess", "Run", 0);
                context.ProcessDispatcher.WriteToProcess("ElevatorProcess", "Run", 0);
                context.ProcessDispatcher.WriteToProcess("AGVProcess", "Run", 0);
                this.toolStripButton_StartCrane.Image = App.Properties.Resources.process_remove;
                this.toolStripButton_StartCrane.Text = "联机自动";
            }
        }
        #region 任务查询
        private void toolStripButton_InStockTask_Click(object sender, EventArgs e)
        {
            App.View.Task.frmInStock f = new App.View.Task.frmInStock();
            ShowForm(f);
        }
        

        private void toolStripButton_OutStockTask_Click(object sender, EventArgs e)
        {
            App.View.Task.frmOutStock f = new View.Task.frmOutStock();
            ShowForm(f);
        }

        private void toolStripButton_CellMonitor_Click(object sender, EventArgs e)
        {
            bool blnEdit = false;
            if (Program.CurrentUser != "")
            {
               // blnEdit = Program.dtUserPermission.Select("FormID='Cell' and OperatorCode=1").Length > 0 ? true : false;
            }
            App.View.Dispatcher.frmCellQuery f = new App.View.Dispatcher.frmCellQuery();
            ShowForm(f);
        }

        #endregion



        #region 正执行任务处理
        void Data_OnTask(TaskEventArgs args)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new TaskEventHandler(Data_OnTask), args);
            }
            else
            {
                lock (this.dgvMain)
                {
                    DataTable dt = args.datatTable;
                    this.bsMain.DataSource = dt;
                }
            }
        }


        private void BindData()
        {
            bsMain.DataSource = GetMonitorData();
        }
        private DataTable GetMonitorData()
        {
            DataTable dt = bll.FillDataTable("WCS.SelectTask", new DataParameter[] { new DataParameter("{0}", string.Format("WCS_TASK.State not in (0,7,9,100)")) });
            return dt;
        }

        private void dgvMain_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (dgvMain.Rows[e.RowIndex].Selected == false)
                    {
                        dgvMain.ClearSelection();
                        dgvMain.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (dgvMain.SelectedRows.Count == 1)
                    {
                        dgvMain.CurrentCell = dgvMain.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                    DataRow dataRow = (this.dgvMain.Rows[this.dgvMain.CurrentCell.RowIndex].DataBoundItem as DataRowView).Row;
                    string AlarmCode = dataRow["AlarmCode"].ToString();
                    string TaskType = dataRow["TaskType"].ToString();

                    //獲取權限
                    bool blnCancel = false; //取消堆垛机任务
                    bool blnReConvey = false; //重下輸送線任务
                    bool blnReCrane = false; //重下堆垛机任务
                    bool blnChangeState = false; //状态修改
                    bool blnReCell = false; //重新分配貨位
                    if (Program.CurrentUser != "")
                    {
                        blnCancel =Program.dtUserPermission.Select("FormID='Task' and OperatorCode=1").Length > 0 ? true : false;
                        blnReConvey = Program.dtUserPermission.Select("FormID='Task' and OperatorCode=4").Length > 0 ? true : false;
                        blnReCrane = Program.dtUserPermission.Select("FormID='Task' and OperatorCode=2").Length > 0 ? true : false;
                        blnChangeState = Program.dtUserPermission.Select("FormID='Task' and OperatorCode=5").Length > 0 ? true : false;
                        blnReCell = Program.dtUserPermission.Select("FormID='Task' and OperatorCode=3").Length > 0 ? true : false;
                    }




                    if (TaskType == "11")
                    {
                        this.ToolStripMenuItemDelCraneTask.Visible = AlarmCode == "0" ? false : blnCancel;
                        ToolStripMenuItemCellCode.Visible = AlarmCode == "0" ? false : blnReCell;
                        ToolStripMenuItemReassign.Visible = AlarmCode == "0" ? false : blnReCrane;
                        ToolStripMenuItemStateChange.Visible = blnChangeState;
                        ToolStripMenuItemRConvey.Visible = blnReConvey;

                        this.ToolStripMenuItem11.Visible = true;
                        this.ToolStripMenuItem12.Visible = true;
                        this.ToolStripMenuItem13.Visible = true;
                        this.ToolStripMenuItem14.Visible = false;
                        this.ToolStripMenuItem15.Visible = false;
                        this.ToolStripMenuItem16.Visible = false;
                        this.ToolStripMenuItem17.Visible = true;
                        this.ToolStripMenuItem110.Visible = false;
                        this.ToolStripMenuItem19.Visible = true;
                    }
                    else if (TaskType == "12")
                    {
                        this.ToolStripMenuItemDelCraneTask.Visible = AlarmCode == "0" ? false : blnCancel;
                        ToolStripMenuItemCellCode.Visible = false;
                        ToolStripMenuItemReassign.Visible = AlarmCode == "0" ? false : blnReCrane;
                        ToolStripMenuItemRConvey.Visible = blnReConvey;
                        ToolStripMenuItemStateChange.Visible = blnChangeState;

                        this.ToolStripMenuItem11.Visible = false;
                        this.ToolStripMenuItem12.Visible = false;
                        this.ToolStripMenuItem13.Visible = false;
                        this.ToolStripMenuItem14.Visible = true;
                        this.ToolStripMenuItem15.Visible = true;
                        this.ToolStripMenuItem16.Visible = true;
                        this.ToolStripMenuItem17.Visible = true;
                        this.ToolStripMenuItem110.Visible = false;
                        this.ToolStripMenuItem19.Visible = true;
                    }
                    else if (TaskType == "13")
                    {
                        this.ToolStripMenuItemDelCraneTask.Visible = AlarmCode == "0" ? false : blnCancel;
                        ToolStripMenuItemCellCode.Visible = AlarmCode == "0" ? false : blnReCell;
                        ToolStripMenuItemReassign.Visible = AlarmCode == "0" ? false : blnReCrane;
                        ToolStripMenuItemStateChange.Visible = blnChangeState;

                        if (dataRow["Flag"].ToString() != "6")
                            ToolStripMenuItemRConvey.Visible = false;
                        else
                            ToolStripMenuItemRConvey.Visible = blnReConvey;

                        this.ToolStripMenuItem11.Visible = false;
                        this.ToolStripMenuItem12.Visible = false;
                        this.ToolStripMenuItem13.Visible = false;
                        this.ToolStripMenuItem14.Visible = true;

                        if (dataRow["Flag"].ToString() != "6")
                        {
                            this.ToolStripMenuItem15.Visible = false;
                            this.ToolStripMenuItem16.Visible = false;
                        }
                        else
                        {
                            this.ToolStripMenuItem15.Visible = true;
                            this.ToolStripMenuItem16.Visible = true;
                        }
                        this.ToolStripMenuItem17.Visible = true;
                        this.ToolStripMenuItem110.Visible = false;
                        this.ToolStripMenuItem19.Visible = true;
                    }
                    else if (TaskType == "99")
                    {
                        ToolStripMenuItemRConvey.Visible = blnReConvey;
                        ToolStripMenuItemDelCraneTask.Visible = false;
                        ToolStripMenuItemCellCode.Visible = false;
                        ToolStripMenuItemReassign.Visible = false;
                        ToolStripMenuItemStateChange.Visible = blnChangeState;

                        this.ToolStripMenuItem11.Visible = false;
                        this.ToolStripMenuItem12.Visible = false;
                        this.ToolStripMenuItem13.Visible = false;
                        this.ToolStripMenuItem14.Visible = false;
                        this.ToolStripMenuItem15.Visible = false;
                        this.ToolStripMenuItem16.Visible = false;
                        this.ToolStripMenuItem17.Visible = true;
                        this.ToolStripMenuItem110.Visible = true;
                        this.ToolStripMenuItem19.Visible = true;

                    }
                    
                    //弹出操作菜单
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        private void ToolStripMenuItemCellCode_Click(object sender, EventArgs e)
        { 
            //重新分配貨位
            if (this.dgvMain.CurrentCell != null)
            {
                BLL.BLLBase bll = new BLL.BLLBase();
                DataRow dataRow = (this.dgvMain.Rows[this.dgvMain.CurrentCell.RowIndex].DataBoundItem as DataRowView).Row;

                string TaskNo = dataRow["TaskNo"].ToString();

                string TaskType = dataRow["TaskType"].ToString();
                string ErrCode = dataRow["AlarmCode"].ToString();

                if (TaskType == "11" || TaskType == "13")
                {
                    DataRow dr = dataRow;
                    App.View.frmReassignEmptyCell f = new App.View.frmReassignEmptyCell(dr);
                    if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        this.BindData();
                }
                //if (TaskType == "13")
                //{
                //    DataRow dr = dataRow;
                //    App.View.frmReassignOption fo = new App.View.frmReassignOption();
                //    if (fo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //    {
                //        if (fo.option == 0)
                //        {
                //            App.View.frmReassignCell f = new App.View.frmReassignCell(dr, fo.option);
                //            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //                this.BindData();
                //        }
                //        if (fo.option == 1)
                //        {
                //            App.View.frmReassignCell fe = new App.View.frmReassignCell(dr, fo.option);
                //            if (fe.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //                this.BindData();
                //        }
                //    }
                //}
            }
               
        }

        private void ToolStripMenuItemReassign_Click(object sender, EventArgs e)
        {
            if (this.dgvMain.CurrentCell != null)
            {
                DataRow dr = (this.dgvMain.Rows[this.dgvMain.CurrentCell.RowIndex].DataBoundItem as DataRowView).Row;
                string State = dr["State"].ToString();
                string TaskNo = dr["TaskNo"].ToString();
                if (dr["FromStation"].ToString().Trim() == "" || dr["ToStation"].ToString().Trim() == "")
                {
                    Logger.Info(TaskNo + "目标位置或者起始位置错误,无法重新下达任务！");
                    return;
                }
                if (State == "3" || State == "4")
                    Send2PLC(TaskNo);
                else
                {
                    Logger.Info("非正在上下架的任务无法重新下发");
                    return;
                }
                this.BindData();
            }
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ItemName = ((ToolStripMenuItem)sender).Name;
            string State = ItemName.Replace("ToolStripMenuItem1", "");

            if (this.dgvMain.CurrentCell != null)
            {
                DataRow dataRow = (this.dgvMain.Rows[this.dgvMain.CurrentCell.RowIndex].DataBoundItem as DataRowView).Row;
                BLL.BLLBase bll = new BLL.BLLBase();
                string TaskNo = dataRow["TaskNo"].ToString();
                string AreaCode = dataRow["AreaCode"].ToString();
                string TaskType = dataRow["TaskType"].ToString();

                DataParameter[] param = new DataParameter[] { new DataParameter("@TaskNo", TaskNo), new DataParameter("@State", State) };
                bll.ExecNonQueryTran("WCS.Sp_UpdateTaskState", param);
                BLL.BLLBase bllMiddle = new BLL.BLLBase("MiddleDB");
                Logger.Info("手動修改任务號:" + TaskNo + ",状态為" + State);
                if (State == "7") //手動執行后，修改中間表
                {
                    string TaskID = dataRow["TaskID"].ToString();
                    string SubTaskID = dataRow["SubTaskID"].ToString();
                    string PalletCode = dataRow["PalletCode"].ToString();
                    if (AreaCode == "UL" && TaskType == "12")
                    {

                        if (bll.GetRowCount("WCS_Task", string.Format("TaskID={0} and Palletcode='{1}' and SubTaskID!={2} and AreaCode='{3}' and State in (0,1,10) ", TaskID, PalletCode, SubTaskID, "UL")) == 0)
                        {
                            DataTable dtMiddle = bllMiddle.FillDataTable("Middle.SelectConveyMoveTask", new DataParameter[] { new DataParameter("@Device", "UL"), new DataParameter("{0}", string.Format("main.task_id={0} and hu_id='{1}' and subtask_id!={2} ", TaskID, PalletCode, SubTaskID)) });
                            if (dtMiddle.Rows.Count > 0)
                            {
                                dtMiddle.Rows[0]["location_id"] = dataRow["ToStation"].ToString();
                                BLL.Server.InsertTaskToWcs(dtMiddle, false);
                            }
                            else
                            {
                                Logger.Error("ConveyPickProcess中找不到托盤號:" + PalletCode + " 的後續處理方式！");
                            }
                        }
                    }
                    //List<DataParameter[]> paras = new List<DataParameter[]>();
                    //List<string> Comds = new List<string>();
                    //Comds.Add("Middle.UpdateAsrsTaskRTN");
                    //Comds.Add("Middle.UpdateAsrsSubTaskRTN");
                    //paras.Add(new DataParameter[] { new DataParameter("@TaskID", TaskID) });
                    //paras.Add(new DataParameter[] { new DataParameter("@SubTaskID", SubTaskID) });
                    //bllMiddle.ExecTran(Comds.ToArray(), paras);

                    bllMiddle.ExecNonQuery("Middle.UpdateAsrsTaskRTN",new DataParameter[] { new DataParameter("@TaskID", TaskID) });
                    bllMiddle.ExecNonQuery("Middle.UpdateAsrsSubTaskRTN", new DataParameter[] { new DataParameter("@SubTaskID", SubTaskID) });

                    if (bllMiddle.GetRowCount("si_asrs_task_Detail", string.Format("task_id={0} and status in ('ADD','ISS','ACK') and subtask_id<>{1}", TaskID, SubTaskID)) == 0)
                    {
                        bll.ExecNonQuery("WCS.sp_FinshedTaskToBak", new DataParameter[] { new DataParameter("@TaskID", TaskID) });
                    }


                }
                else if (State == "9")
                {
 
                }
                BindData();
            }
        }

        private void ToolStripMenuItemDelCraneTask_Click(object sender, EventArgs e)
        {
            if (this.dgvMain.CurrentCell != null)
            {
                DataRow dataRow = (this.dgvMain.Rows[this.dgvMain.CurrentCell.RowIndex].DataBoundItem as DataRowView).Row;
                string AreaCode = dataRow["AreaCode"].ToString();
                string AisleNo = dataRow["AisleNo"].ToString();
                string TaskAB = dataRow["TaskAB"].ToString();
                string TaskNo = dataRow["TaskNo"].ToString();

                string serviceName = "";
                string ReadTaskName = "";
                string WriteItemName = "";
                if (AreaCode == "UL")
                {
                    serviceName = "CranePLC" + AisleNo;
                    ReadTaskName = "CraneTaskNo";
                    WriteItemName = "TaskType";
                }
                else
                {
                    serviceName = "MiniLoad"+AisleNo;
                    if (TaskAB == "A")
                    {
                        ReadTaskName = "CraneTaskNo1";
                        WriteItemName = "TaskType1";
                    }
                    else
                    {
                        ReadTaskName = "CraneTaskNo2";
                        WriteItemName = "TaskType2";
                    }

                }
              
                //判斷當前任务號是否與堆垛机任务號是否一樣
                string ReadTaskNo = Util.ConvertStringChar.BytesToString(ObjectUtil.GetObjects(context.ProcessDispatcher.WriteToService(serviceName, ReadTaskName)));
                if (TaskNo == ReadTaskNo)
                {
                    context.ProcessDispatcher.WriteToService(serviceName, WriteItemName, 5);
                    MCP.Logger.Info("任务號:" + ReadTaskNo + " 已下發取消任务!");
                }
                else
                {
                    MCP.Logger.Info("現有任务與堆垛机任务不一致，無法取消任务!");
                }
            }
        }


        #endregion

         
        private int getTaskType(string TaskType, string State)
        {
            int taskType = 10;
            if (TaskType == "11" || TaskType == "16")
                taskType = 10;
            else if (TaskType == "12" || TaskType == "15")
                taskType = 11;
            else if (TaskType == "13")
                taskType = 9;
            else if (TaskType == "14" && State == "4")
                taskType = 11;
            else if (TaskType == "14" && State == "3")
                taskType = 10;
            return taskType;
        }
        private void Send2PLC(string TaskNo)
        {
            DataParameter[] parameter = new DataParameter[] { new DataParameter("{0}", string.Format("TaskNo='{0}'", TaskNo)), new DataParameter("{1}", "1") };
            DataTable dt = bll.FillDataTable("WCS.SelectCraneTask", parameter);
            if (dt.Rows.Count > 0)
            {
                DataRow dataRow = dt.Rows[0];
                string AreaCode = dataRow["AreaCode"].ToString();
                string AisleNo = dataRow["AisleNo"].ToString();
                string TaskAB = dataRow["TaskAB"].ToString();
               
                string serviceName = "";
               
                string WriteTaskName = "";
                string WriteTaskAddress = "";
                string WriteTaskType = "";
                string ReadCraneLoad="";
                if (AreaCode == "UL")
                {
                    serviceName = "CranePLC" + AisleNo;
                    WriteTaskName = "TaskNo";
                    WriteTaskType = "TaskType";
                    WriteTaskAddress = "TaskAddress";
                    ReadCraneLoad = "CraneLoad";
                }
                else
                {
                    serviceName = "MiniLoad" + AisleNo;
                    if (TaskAB == "A")
                    {
                        WriteTaskName = "TaskNo1";
                        WriteTaskType = "TaskType1";
                        WriteTaskAddress = "TaskAddress1";
                        ReadCraneLoad = "CraneLoad1";
                    }
                    else
                    {
                        WriteTaskName = "TaskNo2";
                        WriteTaskType = "TaskType2";
                        WriteTaskAddress = "TaskAddress2";
                        ReadCraneLoad = "CraneLoad2";
                    }

                }
                string ReadTaskNo = Util.ConvertStringChar.BytesToString(ObjectUtil.GetObjects(context.ProcessDispatcher.WriteToService(serviceName, WriteTaskName)));
                if (TaskNo == ReadTaskNo)
                {
                    context.ProcessDispatcher.WriteToService(serviceName, WriteTaskType, 5);
                    System.Threading.Thread.Sleep(350);
                }
                
                string TaskTypeValue = "1";
                //判斷工位有無貨
                if (ObjectUtil.GetObject(context.ProcessDispatcher.WriteToService(serviceName, ReadCraneLoad)).ToString() == "1")
                    TaskTypeValue="2";
                string fromStation = dataRow["FromStation"].ToString();
                string toStation = dataRow["ToStation"].ToString();
                string SubTaskID = dataRow["subtaskid"].ToString();
                string state = dataRow["State"].ToString();
                int[] cellAddr = new int[6];
                cellAddr[0] = int.Parse(fromStation.Substring(4, 3));
                if (cellAddr[0] == 100)
                    cellAddr[0] = -1;
                cellAddr[1] = int.Parse(fromStation.Substring(7, 3));
                cellAddr[2] = GetPLCShelf(fromStation);
                cellAddr[3] = int.Parse(toStation.Substring(4, 3));
                if (cellAddr[3] == 100)
                    cellAddr[3] = -1;
                cellAddr[4] = int.Parse(toStation.Substring(7, 3));
                cellAddr[5] = GetPLCShelf(toStation);
                //寫入任务號完成之後,讀取任务號是否寫入一致!
                sbyte[] sTaskNo = new sbyte[20];
                Util.ConvertStringChar.stringToBytes(TaskNo, 20).CopyTo(sTaskNo, 0);
                context.ProcessDispatcher.WriteToService(serviceName, WriteTaskName, sTaskNo);
                while (true)
                {
                    ReadTaskNo =Util.ConvertStringChar.BytesToString( ObjectUtil.GetObjects(context.ProcessDispatcher.WriteToService(serviceName, WriteTaskName)));
                    if (TaskNo == ReadTaskNo)
                    {
                        break;
                    }
                    else
                    {
                        context.ProcessDispatcher.WriteToService(serviceName, WriteTaskName, TaskNo);
                        Logger.Debug("任务號寫入不成功,正重新寫入！,讀取任务號:" + ReadTaskNo + " 現有任务號:" + TaskNo);
                    }
                }
                context.ProcessDispatcher.WriteToService(serviceName, WriteTaskAddress, cellAddr);

                context.ProcessDispatcher.WriteToService(serviceName, WriteTaskType, TaskTypeValue);
                context.ProcessDispatcher.WriteToService(serviceName, "WriteFinished", 1);

                Logger.Info("重新下發堆垛机任务,任务號:" + TaskNo);
            }
        }

        
        private int GetPLCShelf(string CellCode)
        {
            string ShelfCode = CellCode.Substring(0,1).PadLeft(3,'0') + CellCode.Substring(1, 3);
            return PLCShelf[ShelfCode];
        }

        private void ToolStripMenuItemMonitor_Click(object sender, EventArgs e)
        {
            View.frmMonitor f = new View.frmMonitor();
            ShowForm(f);
        }

        #region 权限管理

        private void toolStripButton_Login_Click(object sender, EventArgs e)
        {
            if (this.toolStripButton_Login.Text == "用户登录")
            {
                App.Account.frmLogin frm = new Account.frmLogin();
                if (frm.ShowDialog() == DialogResult.OK)
                {

                    Program.CurrentUser = frm.UserID;
                    Program.dtUserPermission = bll.FillDataTable("Security.SelectUserPermission", new DataParameter[] { new DataParameter("@UserName", Program.CurrentUser), new DataParameter("@SystemName", "WCS") });

                    this.toolStripButton_Login.Image = App.Properties.Resources.user_remove;
                    this.toolStripButton_Login.Text = "注销用户";
                    SetBtnEnabled(true);
                    Logger.Debug("操作用户:" + Program.CurrentUser + " 登录!");
                }
            }
            else
            {
                Logger.Debug("操作用户:" + Program.CurrentUser + " 退出!");
                Program.CurrentUser = "";
                Program.dtUserPermission = null;
                this.toolStripButton_Login.Image = App.Properties.Resources.user;
                this.toolStripButton_Login.Text = "用户登录";
                SetBtnEnabled(false);
            }
        }
        private void SetBtnEnabled(bool blnValue)
        {
            ToolStripMenuItem_UserList.Visible = blnValue;
            ToolStripMenuItem_GroupList.Visible = blnValue;
            ToolStripMenuItem_SystemSetUp.Visible = blnValue;
            if (blnValue)
            {
                //ToolStripMenuItem_UserList.Visible = (dtOp.Select("FormID='User'").Length > 0 ? true : false);
                //ToolStripMenuItem_GroupList.Visible = (dtOp.Select("FormID='UserGroup'").Length > 0 ? true : false);
                //ToolStripMenuItem_SystemSetUp.Visible = (dtOp.Select("FormID='Power'").Length > 0 ? true : false);
            }
 
        }

        private void ToolStripMenuItem_UserList_Click(object sender, EventArgs e)
        {
            Account.frmUserList f = new Account.frmUserList(Program.dtUserPermission);
            ShowForm(f);
        }

        private void ToolStripMenuItem_GroupList_Click(object sender, EventArgs e)
        {
            Account.frmGroupList f = new Account.frmGroupList(Program.dtUserPermission);
            ShowForm(f);
 
        }

        private void ToolStripMenuItem_SystemSetUp_Click(object sender, EventArgs e)
        {
            Account.frmGroupManage f = new Account.frmGroupManage();
            ShowForm(f);
        }

        private void ToolStripMenuItem_ChangPWD_Click(object sender, EventArgs e)
        {
            Account.frmChangePWD f = new Account.frmChangePWD();
            f.ShowDialog();
        }
        #endregion
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            View.Task.frmCraneTask frm = new View.Task.frmCraneTask();
            ShowForm(frm);
        }

        private void contextMenuStrip1_VisibleChanged(object sender, EventArgs e)
        {
            if (contextMenuStrip1.Visible)
            {
                this.tmWorkTimer.Stop();
            }
            else
            {
                this.tmWorkTimer.Start();
            }
        }
       


    }
}
