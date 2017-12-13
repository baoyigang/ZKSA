using System;
using System.Collections.Generic;
using System.Text;
using MCP;
using System.Data;
using Util;
using System.Timers;

namespace App.Dispatching.Process
{
    public class CraneProcess : AbstractProcess
    {
        private class rCrnStatus
        {
            
            public int Action { get; set; }
            public int io_flag { get; set; }
            public string ServiceName { get; set; }
            public string DeviceNo { get; set; }
            public rCrnStatus()
            {
                Action = 0;
                io_flag = 0;
                ServiceName = "";
                DeviceNo = "";
            }
        }

        // 记录堆垛机当前状态及任务相关信息
        BLL.BLLBase bll = new BLL.BLLBase();
        private Dictionary<int, rCrnStatus> dCrnStatus = new Dictionary<int, rCrnStatus>();
        private Dictionary<string, int> PLCShelf = new Dictionary<string, int>();
        private Timer tmWorkTimer = new Timer();
        private bool blRun = false;
        private string AreaCode = "ML";
        private string ConveyServer = "MConvey";
        private DataTable dtDeviceAlarm;
        public override void Initialize(Context context)
        {
            try
            {
                //获取堆垛机信息
                DataTable dt = bll.FillDataTable("CMD.SelectDevice", new DataParameter[] { new DataParameter("{0}", "Flag=1") });
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    if (!dCrnStatus.ContainsKey(i))
                    {
                        rCrnStatus crnsta = new rCrnStatus();
                        dCrnStatus.Add(i, crnsta);
                     
                        dCrnStatus[i].io_flag = 0;
                        dCrnStatus[i].ServiceName = dt.Rows[i - 1]["ServiceName"].ToString();
                        dCrnStatus[i].Action = int.Parse(dt.Rows[i - 1]["State"].ToString());
                        dCrnStatus[i].DeviceNo = dt.Rows[i - 1]["DeviceNo"].ToString();
                    }
                }
               
                //tmWorkTimer.Interval = 2000;
                //tmWorkTimer.Elapsed += new ElapsedEventHandler(tmWorker);
                //dtDeviceAlarm = bll.FillDataTable("WCS.SelectDeviceAlarm", new DataParameter[] { new DataParameter("{0}", "Flag=1") });

                base.Initialize(context);
            }
            catch (Exception ex)
            {
                Logger.Error("CraneProcess堆垛机初始化出錯,原因:" + ex.Message);
            }
        }
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            //object obj2 = ObjectUtil.GetObject(stateItem.State);
            //if (obj2 == null)
            //    return;

            switch (stateItem.ItemName)
            {
                case "CraneTaskFinished1":
                case "CraneTaskFinished2":
                    try
                    {
                        object obj = ObjectUtil.GetObject(stateItem.State);
                        if (obj == null)
                            return;
                        string TaskFinish = obj.ToString();
                        if (TaskFinish.Equals("True") || TaskFinish.Equals("1"))
                        {
                            int taskIndex = int.Parse(stateItem.ItemName.Substring(stateItem.ItemName.Length - 1, 1));
                            string TaskNo = Util.ConvertStringChar.BytesToString(ObjectUtil.GetObjects(Context.ProcessDispatcher.WriteToService(stateItem.Name, "CraneTaskNo" + taskIndex)));

                            if (TaskNo.Length == 0)
                                return;
                            DataParameter[] param = new DataParameter[] { new DataParameter("{0}", string.Format("TaskNo='{0}'", TaskNo)), new DataParameter("{1}", "1") };
                            DataTable dtTask = bll.FillDataTable("WCS.SelectCraneTask", param);
                            if (dtTask.Rows.Count > 0)
                            {
                                string TaskType = dtTask.Rows[0]["TaskType"].ToString();
                                int Flag = int.Parse(dtTask.Rows[0]["Flag"].ToString());
                                string State = dtTask.Rows[0]["State"].ToString();
                                string ConveyID = dtTask.Rows[0]["OutStationNo"].ToString(); //出庫站台
                                string PalletCode = dtTask.Rows[0]["Palletcode"].ToString();
                                string TaskID = dtTask.Rows[0]["TaskID"].ToString();
                                string SubTaskID = dtTask.Rows[0]["SubTaskID"].ToString();
                                DataTable dtConveyTask = bll.FillDataTable("WCS.SelectConveyTask", param);
                                string Destination = dtConveyTask.Rows[0]["ToStation"].ToString();

                                sbyte[] sTaskNo = new sbyte[40];
                                Util.ConvertStringChar.stringToBytes(TaskNo, 20).CopyTo(sTaskNo, 0);
                                Util.ConvertStringChar.stringToBytes(PalletCode, 20).CopyTo(sTaskNo, 20);

                                if (TaskType == "11" || (TaskType == "13" && Flag == 3) || (TaskType == "13" && Flag == 6 && State == "3"))
                                {
                                    bll.ExecNonQuery("WCS.Sp_TaskProcess", new DataParameter[] { new DataParameter("@TaskNo", TaskNo) });
                                    Logger.Info("ML" + stateItem.Name.Replace("MiniLoad", "") + "堆垛机上架任务完成,任务號:" + TaskNo + " 料箱號:" + PalletCode);
                                }
                                else if (TaskType == "12" || (TaskType == "13" && Flag == 6 && State == "4"))
                                {
                                    Logger.Info("ML" + stateItem.Name.Replace("MiniLoad", "") + "堆垛机下架任务完成,任务號:" + TaskNo + " 料箱號:" + PalletCode);
                                    if (TaskType == "12")
                                    {
                                        bll.ExecNonQuery("WCS.UpdateCellEmpty", new DataParameter[] { new DataParameter("@CellCode", dtTask.Rows[0]["CellCode"].ToString()) });
                                        bll.ExecNonQuery("WCS.UpdateTaskState", new DataParameter[] { new DataParameter("{0}", "State=5,Crane_FinishDate=getdate()"), new DataParameter("{1}", string.Format("TaskNo='{0}'", TaskNo)) });



                                        WriteToService(ConveyServer, ConveyID + "_TaskNo", sTaskNo);
                                        WriteToService(ConveyServer, ConveyID + "_Destination", Destination); //目的地
                                        if (WriteToService(ConveyServer, ConveyID + "_Request", 2))
                                        {
                                            bll.ExecNonQuery("WCS.UpdateTaskState", new DataParameter[] { new DataParameter("{0}", "State=6,Convey_StartDate=getdate()"), new DataParameter("{1}", string.Format("TaskNo='{0}'", TaskNo)) });
                                            Logger.Info("任务號:" + TaskNo + " 料箱號:" + PalletCode + " 已經下達輸送線:" + ConveyID + " 目的地址:" + Destination);
                                        }
                                      
                                    }
                                    
                                }
                            }
                            else
                            {
                                Logger.Error("MiniLoadProcess中ML堆垛机" + stateItem.ItemName + "任务完成,但WCS中找不到任务號:" + TaskNo);
                            }
                            sbyte[] ClearTaskNo = new sbyte[20];
                            Util.ConvertStringChar.stringToBytes("", 20).CopyTo(ClearTaskNo, 0);
                            WriteToService(stateItem.Name, "TaskNo" + taskIndex, ClearTaskNo);
                        }
                    }
                    catch (Exception ex1)
                    {
                        Logger.Info("MiniLoadProcess中CraneTaskFinished出錯:" + ex1.Message);
                    }
                    break;
                case "Run":
                    blRun = (int)stateItem.State == 1;
                    if (blRun)
                    {
                        tmWorkTimer.Start();
                        Logger.Info("堆垛机联机");
                    }
                    else
                    {
                        tmWorkTimer.Stop();
                        Logger.Info("堆垛机脱机");
                    }
                    break;
                case "PLCCheck":
                    //對現有任务進行比較,如果正常,則將標誌位制0后,堆垛机執行。
                    #region PLC CheckTask
                    try
                    {
                        if (ObjectUtil.GetObject(stateItem.State) == null)
                            return;
                        string objCheck = ObjectUtil.GetObject(stateItem.State).ToString();
                        if (objCheck.Equals("True") || objCheck.Equals("1"))
                        {
                            WriteToService(stateItem.Name, "WriteFinished", 0);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("MiniLoadProcess.StateChanged中PLCCheck出現錯誤,內容:" + ex.Message);
                    }
                    #endregion
                    break;
                case "CraneAlarmCode":
                    if (ObjectUtil.GetObject(stateItem.State) == null)
                        return;
                    if (ObjectUtil.GetObject(stateItem.State).ToString() == "0")
                        return;
                    string CraneNo = stateItem.Name.Replace("MiniLoad", "");
                    string strWarningCode = ObjectUtil.GetObject(stateItem.State).ToString();
                    DataRow[] drs = dtDeviceAlarm.Select(string.Format("AlarmCode='{0}'", strWarningCode));
                    string strError = "";
                    if (drs.Length > 0)
                    {
                        strError = drs[0]["AlarmDesc"].ToString();
                    }
                    else
                    {
                        strError = "未知错误，错误号:" + strWarningCode;
                    }
                    if (strWarningCode == "28" || strWarningCode == "29" || strWarningCode == "35" || strWarningCode == "36")
                    {
                        string k = "1";
                        if (strWarningCode == "35" || strWarningCode == "36")
                            k = "2";

                        string TaskNo = Util.ConvertStringChar.BytesToString(ObjectUtil.GetObjects(Context.ProcessDispatcher.WriteToService(stateItem.Name, "CraneTaskNo" + k)));
                        bll.ExecNonQuery("WCS.UpdateTaskError", new DataParameter[] { new DataParameter("@AlarmCode", strWarningCode), new DataParameter("@AlarmDesc", strError), new DataParameter("@TaskNo", TaskNo) });
                    }
                    Logger.Error("ML" + CraneNo + "堆垛机 " + strError);
                    break;
                default:
                    break;
            }
        }
     
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmWorker(object sender, ElapsedEventArgs e)
        {
            lock (this)
            {
                try
                {

                    if (!blRun)
                    {
                        tmWorkTimer.Stop();
                        return;
                    }
                    tmWorkTimer.Stop();
                    DataTable dt = bll.FillDataTable("CMD.SelectDevice", new DataParameter[] { new DataParameter("{0}", "Flag=1") });
                    for (int i = 1; i <= dt.Rows.Count; i++)
                    {
                        if (dCrnStatus.ContainsKey(i))
                            dCrnStatus[i].Action = int.Parse(dt.Rows[i - 1]["State"].ToString());

                        if (dCrnStatus[i].Action != 1)
                            continue;
                        if (dCrnStatus[i].io_flag == 0)
                        {
                            CraneOut(i);
                        }
                        else
                        {
                            CraneIn(i);
                        }
                    }

                }
                finally
                {
                    tmWorkTimer.Start();
                }
            }
        }
        /// <summary>
        /// 检查堆垛机入库状态
        /// </summary>
        /// <param name="piCrnNo"></param>
        /// <returns></returns>
        private bool Check_Crane_Status_IsOk(int craneNo)
        {
            string serviceName = dCrnStatus[craneNo].ServiceName;
            string plcTaskNo1 = Util.ConvertStringChar.BytesToString(ObjectUtil.GetObjects(Context.ProcessDispatcher.WriteToService(serviceName, "CraneTaskNo1")));
            string craneMode = ObjectUtil.GetObject(Context.ProcessDispatcher.WriteToService(serviceName, "CraneMode")).ToString();
            string CraneState1 = ObjectUtil.GetObject(Context.ProcessDispatcher.WriteToService(serviceName, "CraneState1")).ToString();
            string CraneAlarmCode = ObjectUtil.GetObject(Context.ProcessDispatcher.WriteToService(serviceName, "CraneAlarmCode")).ToString();

            string plcTaskNo2 = Util.ConvertStringChar.BytesToString(ObjectUtil.GetObjects(Context.ProcessDispatcher.WriteToService(serviceName, "CraneTaskNo2")));
            string craneState2 = ObjectUtil.GetObject(Context.ProcessDispatcher.WriteToService(serviceName, "CraneState2")).ToString();

            string CraneLoad1 = ObjectUtil.GetObject(Context.ProcessDispatcher.WriteToService(serviceName, "CraneLoad1")).ToString();
            string CraneLoad2 = ObjectUtil.GetObject(Context.ProcessDispatcher.WriteToService(serviceName, "CraneLoad1")).ToString();


            if (plcTaskNo1 == "" && craneMode == "1" && CraneAlarmCode == "0" && CraneState1 == "1" && plcTaskNo2 == "" && craneState2 == "1" && CraneLoad1 == "0" && CraneLoad2 == "0")
                return true;
            else
                return false;

        }    
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="craneNo"></param>
        private void CraneOut(int craneNo)
        {
            try
            {
                if (!Check_Crane_Status_IsOk(craneNo))
                    return;
                dCrnStatus[craneNo].io_flag = 1;
            }
            catch (Exception e)
            {
                Logger.Debug("Crane out 状态检查错误:" + e.Message.ToString());
                return;
            }

            //判断AGV是否处于空闲
            string serviceName = dCrnStatus[craneNo].ServiceName;
            DataParameter[] parameter = new DataParameter[] { new DataParameter("{0}", string.Format("Task.TaskType='12' and Task.State in ('10','11') ")) };
            DataTable dt = bll.FillDataTable("WCS.SelectCraneTask", parameter);

            DataRow[] drs = dt.Select("State='11'", "CellRow");
            if (drs.Length>= 2)
                Send2PLC(craneNo, drs, false);
            else if (drs.Length == 1)
            {
                //判断是否有正在出库的任务，若有则等待
                DataRow[] drsOut = dt.Select("State='10'");
                if (drsOut.Length == 0)
                    Send2PLC(craneNo, drs, false);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="craneNo"></param>
        private void CraneIn(int craneNo)
        {
            // 判断堆垛机的状态 自动  空闲
            try
            {
                //判断堆垛机
                if (!Check_Crane_Status_IsOk(craneNo))
                    return;

                //切换入库优先
                dCrnStatus[craneNo].io_flag = 0;
            }
            catch (Exception e)
            {
                Logger.Error("MiniLoadProcess中Craneout状态檢查錯誤:" + e.Message.ToString());
                return;
            }
            DataParameter[] parameter = new DataParameter[] { new DataParameter("{0}", string.Format("TASK.TaskType='11' and State='3'")) };
            DataTable dt = bll.FillDataTable("WCS.SelectCraneTask", parameter);
            if (dt.Rows.Count > 0)
            {

                DataRow[] drs = dt.Select("", "AGV_FinishDate,RowIndex");
                if (drs.Length > 0)
                    Send2PLC(craneNo, drs, true);

            }
        }
        private void Send2PLC(int CraneNo, DataRow[] drs,bool blnInStock)
        {
            if (drs.Length == 0)
                return;

            string strWhere = "";
            bool blnTwo10 = false;
            //更新A任务,及B任务
            if (drs.Length == 1)
            {
                bll.ExecNonQuery("WCS.UpdateTaskAB", new DataParameter[] { new DataParameter("@TaskAB", "A"), 
                                                                           new DataParameter("@MergeTaskNo", drs[0]["TaskNo"].ToString()), 
                                                                           new DataParameter("@TaskNo", drs[0]["TaskNo"].ToString()) });
                strWhere = string.Format("TaskNo='{0}'", drs[0]["TaskNo"].ToString());
            }
            else
            {
                bll.ExecNonQuery("WCS.UpdateTaskAB", new DataParameter[] { new DataParameter("@TaskAB", "A"), 
                                                                           new DataParameter("@MergeTaskNo",drs[0]["TaskNo"].ToString()), 
                                                                           new DataParameter("@TaskNo", drs[0]["TaskNo"].ToString()) });

                if (drs[1]["CellRow"].ToString() == "10")
                {
                    blnTwo10 = true;
                }
                if (!blnTwo10)
                {

                    bll.ExecNonQuery("WCS.UpdateTaskAB", new DataParameter[] { new DataParameter("@TaskAB","B"), 
                                                                               new DataParameter("@MergeTaskNo",drs[0]["TaskNo"].ToString()), 
                                                                               new DataParameter("@TaskNo", drs[1]["TaskNo"].ToString()) });

                }
                strWhere = string.Format("TaskNo in ('{0}','{1}')", drs[0]["TaskNo"].ToString(), drs[1]["TaskNo"].ToString());
            }

            DataParameter[] parameter = new DataParameter[] { new DataParameter("{0}",   strWhere) };
            DataTable dt = bll.FillDataTable("WCS.SelectCraneTask", parameter);
            string serviceName = dCrnStatus[CraneNo].ServiceName;
            DataRow[] Taskdrs = dt.Select("", "CellRow");

            for (int i = 0; i < Taskdrs.Length; i++)
            {
                int TaskIndex = i + 1;
                if (i == 0)
                {
                    if (Taskdrs[0]["CellRow"].ToString() == "10")
                        TaskIndex = 2;
                }

                string TaskNo = Taskdrs[i]["TaskNo"].ToString();
                string TaskType = Taskdrs[i]["TaskType"].ToString();
                string CellCode = Taskdrs[i]["CellCode"].ToString();
                int[] cellAddr = new int[6];

                string NextState = "4";
                int FromColumn = int.Parse(Taskdrs[i]["AisleNo"].ToString());
                int FromRow = int.Parse(Taskdrs[i]["RowIndex"].ToString());
                int FromShelf = 1;

                int ToColumn = int.Parse(Taskdrs[i]["AisleNo"].ToString());
                int toRow = int.Parse(CellCode.Substring(6, 3));
                int toShelf = 2;
                if (TaskType == "12")
                {
                    NextState = "12";
                    FromColumn = int.Parse(Taskdrs[i]["AisleNo"].ToString());
                    FromRow = int.Parse(CellCode.Substring(6, 3));
                    FromShelf = 2;
                    ToColumn = int.Parse(Taskdrs[i]["AisleNo"].ToString());

                    //判断前一个任务的所在的巷道
                    DataTable dtSendCrane = bll.FillDataTable("WCS.SelectCraneTask", new DataParameter[] { new DataParameter("{0}", "WCS_TASK.State='12'") });
                    if (dtSendCrane.Rows.Count > 0)
                        ToColumn = int.Parse(dtSendCrane.Rows[0]["AisleNo"].ToString());
                    toRow = dtSendCrane.Rows.Count + 1;
                    toShelf = 1;
                }
                cellAddr[0] = FromColumn;
                cellAddr[1] =FromRow;
                cellAddr[2] =FromShelf;
                cellAddr[3] = ToColumn;
                cellAddr[4] = toRow;
                cellAddr[5] = toShelf;

                sbyte[] sTaskNo = new sbyte[20];
                Util.ConvertStringChar.stringToBytes(TaskNo, 20).CopyTo(sTaskNo, 0);
                WriteToService(serviceName, "TaskNo" + TaskIndex, sTaskNo);
                WriteToService(serviceName, "TaskAddress" + TaskIndex, cellAddr);
                if (WriteToService(serviceName, "TaskType" + TaskIndex, 1))
                {
                    bll.ExecNonQuery("WCS.UpdateTaskState", new DataParameter[] { new DataParameter("{0}", string.Format("State={0},Crane_StartDate=getdate()",NextState)),
                                                                                  new DataParameter("{1}", string.Format("TaskNo='{0}'", TaskNo))});
                }
                Logger.Info("任务号:" + dt.Rows[i]["TaskNo"].ToString() +"已下发载货提升机工位" + TaskIndex + "地址:" + CellCode);
                if (blnTwo10)
                    break;

            }
            WriteToService(serviceName, "WriteFinished", 1);

        }

        
       
    }
}