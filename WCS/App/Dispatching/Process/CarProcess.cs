 
using System;
using System.Collections.Generic;
using System.Text;
using MCP;
using System.Data;
using Util;
using System.Timers;

namespace App.Dispatching.Process
{
    public class CarProcess : AbstractProcess
    {

       private Dictionary<int, string> dicCarErr = new Dictionary<int, string>();
       public override void Initialize(Context context)
       {
           base.Initialize(context);
           dicCarErr.Add(0, "");
           dicCarErr.Add(1, "输送线急停");
           dicCarErr.Add(2, "输送线变频器报警");
           dicCarErr.Add(3, "货物超重");
           dicCarErr.Add(4, "输送线货物操高");
           dicCarErr.Add(5, "输送线超宽");
           dicCarErr.Add(6, "条码未读到");
       }

        // 记录堆垛机当前状态及任务相关信息
        BLL.BLLBase bll = new BLL.BLLBase(); 
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            object obj = ObjectUtil.GetObject(stateItem.State);
            if (obj == null)
                return;

            switch (stateItem.ItemName)
            {
                case "ReadFinished":
                    try
                    {
                       object o = ObjectUtil.GetObject(stateItem.State);
                        string TaskFinish = o.ToString();
                        if (TaskFinish.Equals("True") || TaskFinish.Equals("1"))
                        {
                            string BarCode = Util.ConvertStringChar.BytesToString(ObjectUtil.GetObjects(Context.ProcessDispatcher.WriteToService(stateItem.Name, "BarCode")));

                            if (BarCode.Length <= 0 || BarCode == "?")
                                return;
                            bool blnInnver = false;
                            bool blnHasOutTask = false;
                            int Count = bll.GetRowCount("WCS_TASK", string.Format("TaskType='11' and State in (0,3) and PalletCode!='{0}'", BarCode));
                            if (Count > 0)
                            {
                                WriteToService(stateItem.Name, "WriteFinished", 2);
                                Logger.Error("还有其他入库任务未完成,不能入库!");
                                return;
                            }
                            Count = bll.GetRowCount("WCS_TASK", string.Format("TaskType='11' and State in (0,3) and PalletCode='{0}'", BarCode));
                            if (Count == 0)
                            {

                                Count = bll.GetRowCount("WCS_TASK", "TaskType='12' and State=0");
                                if (Count > 0)
                                    blnHasOutTask = true;
                                else
                                {
                                    if (bll.GetRowCount("WCS_TASK", string.Format("TaskType='14' and State<7 and PalletCode='{0}'", BarCode)) > 0)
                                        blnInnver = true;
                                }

                            }
                            if (blnHasOutTask)
                            {
                                WriteToService(stateItem.Name, "WriteFinished", 2);
                                Logger.Error("堆垛机正在执行出库任务,不能入库!");
                                return;
                            }


                            //判断货位表中是否含有该条码的货位.
                            int i = bll.GetRowCount("CMD_CELL", string.Format("CellName='{0}' and IsTurnover=0", BarCode));
                            if (i == 0 && !blnInnver)
                            {
                                WriteToService(stateItem.Name, "WriteFinished", 4);
                                Logger.Error("条码不正确,不存在货位名称为 " + BarCode + " 的货位!");
                                return;
                            }
                            if (i > 0)
                            {
                                //判断该货位是否含有货,托盘条码是否重复
                                int k = bll.GetRowCount("CMD_CELL", string.Format("CellName='{0}' and PalletCode='{1}' and InDate is not Null ", BarCode, BarCode));
                                if (k == 1 && !blnInnver)
                                {
                                    WriteToService(stateItem.Name, "WriteFinished", 3);
                                    Logger.Error("条码重复,货位名称为 " + BarCode + " 的货位已经入库!");
                                    return;
                                }
                            }
                            bll.ExecNonQuery("WCS.SpCreateInTaskByPallet", new DataParameter[] { new DataParameter("@PalletCode", BarCode) });
                            string strValue = "";
                            //入库时输入冲程数
                            DataTable dtTask = bll.FillDataTable("WCS.SelectTask", new DataParameter[] { new DataParameter("{0}", string.Format("WCS_Task.TaskType='11' and WCS_Task.State in (0,3) and WCS_Task.PalletCode='{0}'", BarCode)) });
                            if (dtTask.Rows.Count > 0)
                            {
                                string[] str = new string[3];
                                str[0] = "1";
                                while ((strValue = FormDialog.ShowDialog(str, dtTask)) != "")
                                {
                                    break;
                                }
                            }

                            WriteToService(stateItem.Name, "WriteFinished", 1);
                            Logger.Info("托盘条码为 " + BarCode + " 开始入库!");
                        }
                    }
                    catch (Exception ex1)
                    {
                        Logger.Info("CarProcess中ReadFinished出错：" + ex1.Message);
                    }
                    break;
                case "AlarmCode":
                    object o1 = ObjectUtil.GetObject(stateItem.State);
                    if (o1.ToString() != "0")
                    {
                        Logger.Error(dicCarErr[int.Parse(o1.ToString())]);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}