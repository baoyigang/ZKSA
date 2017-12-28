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
        private Timer tmWorkTimer;
        private bool blRun = false;
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
                tmWorkTimer = new Timer();
                tmWorkTimer.Interval = 2000;
                tmWorkTimer.Elapsed += new ElapsedEventHandler(tmWorker);
                dtDeviceAlarm = bll.FillDataTable("WCS.SelectDeviceAlarm", new DataParameter[] { new DataParameter("{0}", "Flag=1") });

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
                        if (TaskFinish.Equals("1"))
                        {
                            int taskIndex = int.Parse(stateItem.ItemName.Substring(stateItem.ItemName.Length - 1, 1));
                            string TaskNo = Util.ConvertStringChar.BytesToString(ObjectUtil.GetObjects(Context.ProcessDispatcher.WriteToService(stateItem.Name, "CraneTaskNo" + taskIndex)));

                            if (TaskNo.Length == 0)
                                return;
                            DataParameter[] param = new DataParameter[] { new DataParameter("{0}", string.Format("TaskNo='{0}'", TaskNo)), new DataParameter("{1}", "TaskNo"), new DataParameter("{2}", 1) };
                            DataTable dtTask = bll.FillDataTable("WCS.SelectCraneTask", param);
                            if (dtTask.Rows.Count > 0)
                            {
                                string TaskType = dtTask.Rows[0]["TaskType"].ToString();
                                string CellCode = dtTask.Rows[0]["CellCode"].ToString();
                               
                                string Msg = "上架";
                                string strState = "5";

                                if (TaskType == "12")
                                {
                                    Msg = "下架";
                                    strState = "13";
                                }
                                bll.ExecNonQuery("WCS.UpdateTaskState", new DataParameter[] { new DataParameter("{0}", string.Format("State={0},Crane_FinishDate=getdate()", strState)), new DataParameter("{1}", string.Format("TaskNo='{0}'", TaskNo)) });
                                Logger.Info("载货提升机" + Msg + "任务完成,任务號:" + TaskNo + " 货位:" + CellCode);
                            }
                            else
                            {
                                Logger.Error("载货提升机任务完成,但WCS中找不到任务號:" + TaskNo);
                            }
                            sbyte[] ClearTaskNo = new sbyte[20];
                            Util.ConvertStringChar.stringToBytes("", 20).CopyTo(ClearTaskNo, 0);
                            WriteToService(stateItem.Name, "TaskNo" + taskIndex, ClearTaskNo);
                        }
                    }
                    catch (Exception ex1)
                    {
                        Logger.Info("CraneProcess中CraneTaskFinished出錯:" + ex1.Message);
                    }
                    break;
                case "Run":
                    blRun = (int)stateItem.State == 1;
                    if (blRun)
                    {
                        tmWorkTimer.Start();
                        Logger.Info("载货提升机联机");
                    }
                    else
                    {
                        tmWorkTimer.Stop();
                        Logger.Info("载货提升机脱机");
                    }
                    break;
                case "PLCCheck":
                    
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
                        Logger.Error("CraneProcess.StateChanged中PLCCheck出現錯誤,內容:" + ex.Message);
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
                    Logger.Error("载货提升机" + strError);
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

            string serviceName = dCrnStatus[craneNo].ServiceName;
            DataParameter[] parameter = new DataParameter[] { new DataParameter("{0}", string.Format("TaskType='12' and State in ('10','11') ")), new DataParameter("{1}", "Car_StartDate"), new DataParameter("{2}", 2) };
            DataTable dt = bll.FillDataTable("WCS.SelectCraneTask", parameter);

            DataRow[] drs = dt.Select("State='11'", "RowIndex,CellRow");
            if (drs.Length == 2)
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
                Logger.Error("CraneProcess中Craneout状态檢查錯誤:" + e.Message.ToString());
                return;
            }
            DataParameter[] parameter = new DataParameter[] { new DataParameter("{0}", string.Format("TaskType='11' and State='3'")), new DataParameter("{1}", "RowIndex"), new DataParameter("{2}", 2) };
            DataTable dt = bll.FillDataTable("WCS.SelectCraneTask", parameter);
            if (dt.Rows.Count > 0)
            {

                DataRow[] drs = dt.Select("", "RowIndex,CellRow");
                if (drs.Length > 0)
                    Send2PLC(craneNo, drs, true);

            }
        }
        private void Send2PLC(int CraneNo, DataRow[] drs, bool blnInStock)
        {
            if (drs.Length == 0)
                return;

            string strWhere = "";

            //更新A任务,及B任务

            bll.ExecNonQuery("WCS.UpdateTaskAB", new DataParameter[] { new DataParameter("@TaskAB", "A"), 
                                                                           new DataParameter("@MergeTaskNo", drs[0]["TaskNo"].ToString()), 
                                                                           new DataParameter("@TaskNo", drs[0]["TaskNo"].ToString()) });
            strWhere = string.Format("TaskNo='{0}'", drs[0]["TaskNo"].ToString());

            bll.ExecNonQuery("WCS.UpdateTaskAB", new DataParameter[] { new DataParameter("@TaskAB", "A"), 
                                                                           new DataParameter("@MergeTaskNo",drs[0]["TaskNo"].ToString()), 
                                                                           new DataParameter("@TaskNo", drs[0]["TaskNo"].ToString()) });
            if (drs.Length > 1)
            {

                bll.ExecNonQuery("WCS.UpdateTaskAB", new DataParameter[] { new DataParameter("@TaskAB","B"), 
                                                                               new DataParameter("@MergeTaskNo",drs[0]["TaskNo"].ToString()), 
                                                                               new DataParameter("@TaskNo", drs[1]["TaskNo"].ToString()) });


            }
            string serviceName = dCrnStatus[CraneNo].ServiceName;

            for (int i = 0; i < drs.Length; i++)
            {
                int TaskIndex = i + 1;
                string TaskNo = drs[i]["TaskNo"].ToString();
                int[] cellAddr = new int[6];
                string NextState;
                int FromColumn, FromRow, FromShelf, ToColumn, toRow, toShelf;


                if (blnInStock)
                {
                    NextState = "4";
                    FromColumn = 1;
                    if (drs[i]["AGVStation"].ToString() == "5")
                        FromColumn = 2;
                    FromRow = int.Parse(drs[i]["RowIndex"].ToString());
                    FromShelf = 1;

                    ToColumn = int.Parse(drs[i]["AisleNo"].ToString());
                    toRow = int.Parse(drs[i]["CellRow"].ToString());
                    toShelf = 2;
                }
                else
                {
                    NextState = "12";
                    FromColumn = int.Parse(drs[i]["AisleNo"].ToString());
                    FromRow = int.Parse(drs[i]["CellRow"].ToString());
                    FromShelf = 2;
                    ToColumn = int.Parse(drs[i]["AisleNo"].ToString());
                    if (drs[i]["AGVStation"].ToString() == "5")
                        ToColumn = 2;
                    //判断前一个任务的所在的巷道

                    int count = bll.GetRowCount("WCS_TASK", "TaskType='12' and State in (12,13)");
                    toRow = count + 1;
                    toShelf = 1;
                }
                cellAddr[0] = FromColumn;
                cellAddr[1] = FromRow;
                cellAddr[2] = FromShelf;
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
                Logger.Info("任务号:" + drs[i]["TaskNo"].ToString() + "已下发载货提升机工位" + TaskIndex + "地址:" + drs[i]["CellCode"].ToString());
            }
            WriteToService(serviceName, "WriteFinished", 1);
        }
    }
}