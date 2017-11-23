using System;
using System.Collections.Generic;
using System.Text;
using MCP;
using System.Data;
using Util;
using System.Timers;
namespace App.Dispatching.Process
{
    public class ElevatorProcess : AbstractProcess
    {

        //private class rCrnStatus
        //{

        //    public int Action { get; set; }
        //    public int io_flag { get; set; }
        //    public string ServiceName { get; set; }
        //    public string DeviceNo { get; set; }
        //    public string InStationNo { get; set; }
        //    public string OutStationNo { get; set; }

        //    public rCrnStatus()
        //    {
        //        Action = 0;
        //        io_flag = 0;
        //        ServiceName = "";
        //        DeviceNo = "";
        //        InStationNo = "";
        //        OutStationNo = "";
        //    }
        //}

        // 记录堆垛机当前状态及任务相关信息
        BLL.BLLBase bll = new BLL.BLLBase();
        //private Dictionary<string, rCrnStatus> dCrnStatus = new Dictionary<string, rCrnStatus>();
        private Timer tmWorkTimer = new Timer();
        //private string WarehouseCode = "";
        private bool blRun = false;
        private DataTable dtDeviceAlarm;
      
        public override void Initialize(Context context)
        {
            try
            {
                //DataTable dt = bll.FillDataTable("CMD.SelectDevice", new DataParameter[] { new DataParameter("{0}", "Flag=2") });
                //for (int i = 1; i <= dt.Rows.Count; i++)
                //{
                //    string DeviceNo = dt.Rows[i - 1]["DeviceNo2"].ToString();
                //    if (!dCrnStatus.ContainsKey(DeviceNo))
                //    {
                //        rCrnStatus crnsta = new rCrnStatus();
                //        dCrnStatus.Add(DeviceNo, crnsta);
                //        dCrnStatus[DeviceNo].io_flag = 0;
                //        dCrnStatus[DeviceNo].ServiceName = dt.Rows[i - 1]["ServiceName"].ToString();
                //        dCrnStatus[DeviceNo].Action = int.Parse(dt.Rows[i - 1]["State"].ToString());
                //        dCrnStatus[DeviceNo].DeviceNo = dt.Rows[i - 1]["DeviceNo"].ToString();
                //        dCrnStatus[DeviceNo].InStationNo = dt.Rows[i - 1]["InStationNo"].ToString();
                //        dCrnStatus[DeviceNo].OutStationNo = dt.Rows[i - 1]["OutStationNo"].ToString();
                //    }
                //}
                dtDeviceAlarm = bll.FillDataTable("WCS.SelectDeviceAlarm", new DataParameter[] { new DataParameter("{0}", "Flag in(2,3)") });                

                tmWorkTimer.Interval = 1000;
                tmWorkTimer.Elapsed += new ElapsedEventHandler(tmWorker);

                base.Initialize(context);
            }
            catch (Exception ex)
            {
                Logger.Error("ElevatorProcess提升机初始化出错，原因：" + ex.Message);
            }
        }
        #region StateChanged
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            switch (stateItem.ItemName)
            {
                case "TaskFinished01":
                case "TaskFinished02":
                    object[] obj = ObjectUtil.GetObjects(stateItem.State);

                    if (obj == null)
                        return;
                    string TaskNo = ConvertStringChar.BytesToString(obj);

                    //存储过程处理
                    if (TaskNo.Length > 0)
                    {

                        DataParameter[] para = new DataParameter[] { new DataParameter("{0}", string.Format("Task.TaskNo='{0}'", TaskNo)) };
                        DataTable dt = bll.FillDataTable("WCS.SelectElevatorTask", para);
                        if (dt.Rows.Count > 0)
                        {
                            string TaskType = dt.Rows[0]["TaskType"].ToString();
                           

                            if (TaskType == "12")
                            {
                                bll.ExecNonQuery("WCS.UpdateCellEmpty", new DataParameter[] { new DataParameter("@CellCode", dt.Rows[0]["CellCode"].ToString()) });
                                bll.ExecNonQuery("WCS.UpdateTaskByFilter", new DataParameter[] { new DataParameter("{0}", "State=11,Car_FinishDate=getdate()"), new DataParameter("{1}", string.Format("TaskNo='{0}'", TaskNo)) });
                            }
                            else
                            {
                                DataParameter[] param = new DataParameter[] { new DataParameter("@TaskNo", TaskNo) };
                                bll.ExecNonQueryTran("WCS.Sp_TaskProcess", param); 
                            }
                        }

                        byte[] b = new byte[30];
                        ConvertStringChar.stringToByte("", 30).CopyTo(b, 0);
                        WriteToService(stateItem.Name, stateItem.ItemName, b);
                        Logger.Info(stateItem.ItemName + "完成标志,任务号:" + TaskNo);
                    }
                 
                    break;
                case "Run":
                    blRun = (int)stateItem.State == 1;
                    if (blRun)
                    {
                        tmWorkTimer.Start();
                        Logger.Info("提升机联机");
                    }
                    else
                    {
                        tmWorkTimer.Stop();
                        Logger.Info("提升机脱机");
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

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
                    DataTable dtCar = bll.FillDataTable("CMD.SelectDevice", new DataParameter[] { new DataParameter("{0}", "Flag=2") });
                    for (int i = 0; i < dtCar.Rows.Count; i++)
                    {
                        string DeviceNo = dtCar.Rows[i]["DeviceNo2"].ToString();

                        if (dtCar.Rows[i]["State"].ToString() != "1")
                            continue;
                        string serviceName = dtCar.Rows[i]["ServiceName"].ToString();
                        object objFlag = ObjectUtil.GetObject(WriteToService(serviceName, "WriteFinished"));
                        if (int.Parse(objFlag.ToString()) == 1)
                            continue;
                         object[] CarStatus = ObjectUtil.GetObjects(WriteToService(serviceName, "CarStatus" + DeviceNo));
                        bool IsSent = false;
                        if (Check_Car_Status_IsOk(DeviceNo, serviceName))
                        {
                            IsSent = FindInTask(dtCar, DeviceNo, CarStatus);
                            if (IsSent)
                                continue;

                            IsSent = FindOutTask(dtCar, DeviceNo, CarStatus);
                            if (IsSent)
                                continue;

                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("ElevatorProcess中tmWorker出现异常:" + ex.Message);
                }
                finally
                {
                    tmWorkTimer.Start();
                }
            }
        }
        //获取小车让车可去的空闲的层
        private int GetNoTaskLayer(string serviceName,DataTable dtCar, string carNo, int carLayer)
        {
            int NoTaskLayer = carLayer;
            if (!IsCurrentLayerOK(serviceName, dtCar, carNo, carLayer))
                return NoTaskLayer;

            for (int k = 1; k < 11; k++)
            {
                NoTaskLayer = k;
                if (IsCurrentLayerOK(serviceName, dtCar, carNo, k))
                    continue;
                else
                    break;                
            }
            return NoTaskLayer;
        }
        private bool IsCurrentLayerOK(string serviceName, DataTable dtCar, string carNo, int carLayer)
        {
            bool isExist = false;
            //优先判断小车当前层是否可行
            for (int i = 0; i < dtCar.Rows.Count; i++)
            {
                string CarNo = dtCar.Rows[i]["DeviceNo2"].ToString().Substring(2, 2);
                {
                    if (CarNo != carNo)
                    {
                        //读取小车状态
                        object[] obj = ObjectUtil.GetObjects(WriteToService(serviceName, "CarStatus" + CarNo));

                        int Layer = int.Parse(obj[3].ToString());
                        int FromLayer = int.Parse(obj[6].ToString());
                        int FromColumn = int.Parse(obj[5].ToString());
                        int ToLayer = int.Parse(obj[9].ToString());
                        int Column = int.Parse(obj[2].ToString());
                        int ToColumn = int.Parse(obj[8].ToString());

                        if (FromLayer == carLayer || ToLayer == carLayer || Layer == carLayer)
                            isExist = true;

                        if (isExist)
                            break;
                    }
                }
            }
            return isExist;
        }
        
        private bool FindInTask(DataTable dtCar,  string carNo, object[] CarStatus)
        {
            int carAisleNo = int.Parse(CarStatus[1].ToString()); //当前巷道
            int carLayer = int.Parse(CarStatus[3].ToString()); //当前层
            bool IsSendTask = false;
            DataTable dtTask = bll.FillDataTable("WCS.SelectElevatorTask", new DataParameter[] { new DataParameter("{0}", "Task.State='5' and Task.TaskType='11'") });
            string filter = string.Format("AisleNo='{0}' and CellRow={1}", carAisleNo, carLayer); //当前巷道，当前层
            DataRow[] drTasks = dtTask.Select(filter, "Crane_FinishDate");
            if (drTasks.Length > 0)
                IsSendTask = SendTask(dtCar, carNo, drTasks, CarStatus);
            
            if (!IsSendTask) 
            {
                //再找不在这层的入库任务
                filter = string.Format("AisleNo='{0}' and CellRow<>{1}", carAisleNo, carLayer); //当前巷道，不同层
                drTasks = dtTask.Select(filter, "Crane_FinishDate,CellRow");
                if (drTasks.Length > 0)
                    IsSendTask = SendTask(dtCar, carNo, drTasks, CarStatus);
            }
            if (!IsSendTask)
            {
                filter = string.Format("AisleNo<>'{0}'", carAisleNo); //其它巷道
                drTasks = dtTask.Select(filter, "Crane_FinishDate,CellRow");
                if (drTasks.Length > 0)
                    IsSendTask = SendTask(dtCar, carNo, drTasks, CarStatus);
            }

            return IsSendTask;
        }
        private bool FindOutTask(DataTable dtCar, string carNo, object[] CarStatus)
        {
            //判断是否应该下穿梭车出库任务



            int carAisleNo = int.Parse(CarStatus[1].ToString());
            int carLayer = int.Parse(CarStatus[3].ToString());
            bool IsSendTask = false;
            DataTable dtTask = bll.FillDataTable("WCS.SelectElevatorTask",new DataParameter[] { new DataParameter("{0}", "Task.State='0' and Task.TaskType='12'") });
            string filter = string.Format("AisleNo='{0}' and CellRow={1}", carAisleNo, carLayer); //当前巷道，当前层
            DataRow[] drTasks = dtTask.Select(filter, "CellColumn,Depth");
            if (drTasks.Length > 0)
                IsSendTask = SendTask(dtCar, carNo, drTasks, CarStatus);
            
            if (!IsSendTask)
            {
                //再找不在这层的出库任务
                filter = string.Format("AisleNo='{0}' and CellRow<>{1}", carAisleNo, carLayer); //当前巷道，不同层
                drTasks = dtTask.Select(filter, "CellRow desc,CellColumn,Depth ");
                if (drTasks.Length > 0)
                    IsSendTask = SendTask(dtCar, carNo, drTasks, CarStatus);
            }
            if (!IsSendTask)
            {
                //再找不在这层的出库任务
                filter = string.Format("AisleNo<>'{0}'", carAisleNo); //其它巷道
                drTasks = dtTask.Select(filter, "CellRow desc,CellColumn,Depth");
                if (drTasks.Length > 0)
                    IsSendTask = SendTask(dtCar, carNo, drTasks, CarStatus);
            }
            return IsSendTask;
        }
        private bool SendTask(DataTable dtCar, string carNo, DataRow[] drsTask, object[] obj)
        {
            string serviceName = dtCar.Rows[0]["ServiceName"].ToString();
            bool IsSend = false;
            for (int i = 0; i < drsTask.Length; i++)
            {
                DataRow drTask = drsTask[i];
                string TaskType = drTask["TaskType"].ToString();
                if (CheckOtherCarStatus(dtCar, carNo, drTask, obj))
                {
                    //给小车下达任务
                    Send2PLC(serviceName, drTask, carNo);

                    IsSend = true;

                }
            }
            return IsSend;
        }

        /// <summary>
        /// 判断能否给小车下任务
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="carNo"></param>
        /// <param name="carToLayer"></param>
        /// <returns></returns>
        private bool CheckOtherCarStatus(DataTable dtCar, string carNo, DataRow drTask, object[] carobj)
        {
            int carLayer = int.Parse(carobj[3].ToString());
            int carAisleNo = int.Parse(carobj[1].ToString());
            string carTaskType = drTask["TaskType"].ToString();
            int carFromLayer = int.Parse(drTask["CellRow"].ToString());
            int carToLayer = int.Parse(drTask["CellRow"].ToString());
            int carFromAisleNo = int.Parse(drTask["AisleNo"].ToString());
            int carToAisleNo = int.Parse(drTask["AisleNo"].ToString());

            bool carOK = true;
            for (int i = 0; i < dtCar.Rows.Count; i++)
            {
                string DeviceNo = dtCar.Rows[i]["DeviceNo2"].ToString();
                string serviceName = dtCar.Rows[i]["ServiceName"].ToString();
                if (DeviceNo != carNo)
                {
                    //读取小车状态
                    object[] obj = ObjectUtil.GetObjects(WriteToService(serviceName, "CarStatus" + DeviceNo));
                    int AisleNo = int.Parse(obj[1].ToString());
                    
                    int Layer = int.Parse(obj[3].ToString());
                    int FromAisleNo = int.Parse(obj[10].ToString());
                    int FromLayer = int.Parse(obj[6].ToString());
                    int ToLayer = int.Parse(obj[9].ToString());
                    int ToAisleNo = int.Parse(obj[11].ToString());
                  
 
                    //入库类型 入库起始层即小车当前所在层
                    if (carTaskType == "11")
                    {
                        if ((carToAisleNo == FromAisleNo && carToLayer == FromLayer) || (carToAisleNo == ToAisleNo && carToLayer == ToLayer) || (carToAisleNo == AisleNo && carToLayer == Layer))
                        {
                            carOK = false;
                            break;
                        }
                    }
                    if (carTaskType == "12")
                    {
                        if ((carFromAisleNo == FromAisleNo && carFromLayer == FromLayer) || (carFromAisleNo == ToAisleNo && carFromLayer == ToLayer) || (carFromAisleNo == AisleNo && carFromLayer == Layer))
                        {
                            carOK = false;
                            break;
                        }                        
                    }
                }
            }
            return carOK;
        }
        /// <summary>
        /// 检查小车入库状态
        /// </summary>
        /// <param name="piCrnNo"></param>
        /// <returns></returns>
        private bool Check_Car_Status_IsOk(string carNo, string serviceName)
        {
            
            try
            {
                object[] obj = ObjectUtil.GetObjects(Context.ProcessDispatcher.WriteToService(serviceName, "CarStatus" + carNo));
                int CarMode = int.Parse(obj[0].ToString());
                int State = int.Parse(obj[12].ToString());
                //int CraneAlarmCode = int.Parse(obj[0].ToString());

                if (CarMode == 1 && State == 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Logger.Error("检查小车" + carNo + "状态时出现错误:" + ex.Message);
                return false;
            }
        }

        private void Send2PLC(string serviceName, DataRow dr, string carNo)
        {
            string TaskNo = dr["TaskNo"].ToString();
            string TaskType = dr["TaskType"].ToString();            
            string NextState = "6";
            string AisleNo = dr["AisleNo"].ToString();

            string FromStation = dr["FromStation"].ToString();
            string ToStation = dr["ToStation"].ToString();
            int[] cellAddr = new int[10];
            cellAddr[0] = byte.Parse(FromStation.Substring(0, 3));
            cellAddr[1] = byte.Parse(FromStation.Substring(3, 3));
            cellAddr[2] = int.Parse(FromStation.Substring(6, 3));
            cellAddr[3] = byte.Parse(ToStation.Substring(0, 3));
            cellAddr[4] = byte.Parse(ToStation.Substring(3, 3));
            cellAddr[5] = byte.Parse(ToStation.Substring(6, 3));
            cellAddr[6] = byte.Parse(AisleNo);
            cellAddr[7] = byte.Parse(AisleNo);
            if (TaskType == "11")
            {
                cellAddr[8] = 10;
                NextState = "6";
            }
            else if (TaskType == "12")
            {
                cellAddr[8] = 11;
                NextState = "10";
            }
            cellAddr[9] = int.Parse(carNo);

            sbyte[] staskNo = new sbyte[30];
            Util.ConvertStringChar.stringToBytes(TaskNo, 30).CopyTo(staskNo, 0);
            Context.ProcessDispatcher.WriteToService(serviceName, "TaskNo", staskNo);
            Context.ProcessDispatcher.WriteToService(serviceName, "TaskAddress", cellAddr);

            string DeviceNo = "Car" + carNo;
            if (WriteToService(serviceName, "WriteFinished", 1))
            {
                bll.ExecNonQuery("WCS.UpdateTaskByFilter", new DataParameter[] { new DataParameter("{0}", string.Format("State='{0}' and DeviceNo='{1}'", NextState, DeviceNo)), new DataParameter("{1}", string.Format("TaskNo='{0}'", TaskNo)) });
                Logger.Info("任务:" + dr["TaskNo"].ToString() + "已下发给" + carNo + "穿梭车;起始地址:" + FromStation + ",目标地址:" + ToStation);
            }
            
        }        
    }
}