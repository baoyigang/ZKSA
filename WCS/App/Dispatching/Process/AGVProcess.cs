using System;
using System.Collections.Generic;
using System.Text;
using MCP;
using System.Data;
using Util;
using System.Timers;

namespace App.Dispatching.Process
{
    public class AGVProcess : AbstractProcess
    {
        private Timer tmWorkTimer;
        private bool blRun = false;
        private BLL.BLLBase bll = new BLL.BLLBase();
        public override void Initialize(Context context)
        {

            base.Initialize(context);
            tmWorkTimer = new Timer();
            tmWorkTimer.Interval = 2000;
            tmWorkTimer.Elapsed += new ElapsedEventHandler(tmWorker);
        }

        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            switch (stateItem.ItemName)
            {
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
                case "s":
                    Dictionary<string, UInt16> obj = (Dictionary<string, UInt16>)stateItem.State;
                    UInt16 AGVIndex = obj["AGVIndex"];
                    UInt16 AGVPhase = obj["AGVPhase"];
                    UInt16 AGVDeviceNo = obj["AGVDeviceNo"];
                    UInt16 AGVTaskID = obj["AGVTaskID"];
                    UInt16 AGVToStation = obj["AGVStation"];
                    
                    DataTable dtTask = bll.FillDataTable("WCS.SelectAGVTask", new DataParameter[] { new DataParameter("{0}", "State not in (7,9) and AGVTaskID=" + AGVTaskID), new DataParameter("{1}", "TaskNo,RowIndex"), new DataParameter("{2}", 4) });
                    if (dtTask.Rows.Count > 0)
                    {
                        string TaskNo = "";
                        string TaskType = dtTask.Rows[0]["TaskType"].ToString();
                        for (int i = 0; i < dtTask.Rows.Count; i++)
                        {
                            TaskNo += "'" + dtTask.Rows[i]["TaskNo"].ToString() + "',";
                        }
                        string StrState = "";
                        if (AGVPhase == 6)
                        {
                            if (TaskType == "11")
                                StrState = string.Format(",State={0}",2);
                            else
                                StrState = string.Format(",State={0}", 14);
                        }
                        else if (AGVPhase == 7)
                        {
                            if (TaskType == "11")
                                StrState = string.Format(",State={0}", 3);
                            else
                                StrState = string.Format(",State={0}", 7);
                        }


                        bll.ExecNonQuery("WCS.UpdateTaskState", new DataParameter[] { new DataParameter("{0}", string.Format("AGVDeviceNo={0},AGVIndex={1},AGVPhase={2}" + StrState, AGVDeviceNo, AGVIndex, AGVPhase)), new DataParameter("{1}", string.Format("TaskNo in ({0})", TaskNo.TrimEnd(','))) });

                    }
                    else
                    {
                        Logger.Error("AGVProcess中无法找到AGVTaskID为" + AGVTaskID + "任务号");
                    }
                    byte[] sendByte = SendAGVMessage.GetSendCheckMsg(AGVIndex, AGVPhase);
                    WriteToService("AGVService", "m", sendByte);
                    Logger.Info("AGV执行任务ID：" + AGVTaskID + "到达阶段：" + AGVPhase);
                    break;
            }


        }

        private void tmWorker(object sender, ElapsedEventArgs e)
        {
            lock (this)
            {
                
                try
                {
                    tmWorkTimer.Stop();
                    if (!blRun)
                        return;

                    //判断是否存在入库任务,且不存在未完成的
                    DataTable dt = bll.FillDataTable("WCS.SelectAGVTask", new DataParameter[] { new DataParameter("{0}", "TaskType='11' and State=0 and not exists(select 1 from WCS_TASK where TaskType='11' and State in (1,2,3,4) )"), new DataParameter("{1}", "TaskNo,RowIndex"), new DataParameter("{2}", 4) });
                    if (dt.Rows.Count > 0)
                    {
                        ushort FromStation = ushort.Parse(dt.Rows[0]["FromStation"].ToString());
                        //获取入库巷道
                        ushort ToStation = ushort.Parse(dt.Rows[0]["ToStation"].ToString());
                        //获取AGV操作码
                        ushort AGVActionID = SendAGVMessage.GetAGVActionID();
                        //获取AGV任务号
                        ushort AGVTaskID = SendAGVMessage.GetAGVTaskID();
                        //更新WCS_Task AGVTaskID
                        string TaskNo = "";
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TaskNo += "'" + dt.Rows[i]["TaskNo"].ToString() + "',";
                        }
                        bll.ExecNonQuery("WCS.UpdateTaskState", new DataParameter[] { new DataParameter("{0}", string.Format("AGVTaskID={0},State=1,AGVStation={1}", AGVTaskID, ToStation)), new DataParameter("{1}", string.Format("TaskNo in ({0})", TaskNo.TrimEnd(','))) });
                        //获取发送信息
                        byte[] sendByte = SendAGVMessage.GetSendTask1(AGVTaskID, FromStation, ToStation, AGVActionID);
                        WriteToService("AGVService", "ACK", sendByte);
                        Logger.Info("下发小车入库任务，小车任务号：" + AGVTaskID);
                    }

                }
                catch (Exception ex)
                {
                    Logger.Error("AGVProcess中下发任务出错：" + ex.Message);
                }
                finally
                {
                    tmWorkTimer.Start();
                }
            }
        }
    }
}
