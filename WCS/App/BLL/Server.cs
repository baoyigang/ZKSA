using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using IServices;
using System.Data;
using Util;

namespace BLL
{
    public class Server
    {

        /// <summary>
        /// 通道字典
        /// </summary>
        private static Dictionary<string, object> Channels = new Dictionary<string, object>();
        private static readonly object Locker1 = new object();
       
        /// <summary>
        /// 创建一个指定类型的通道
        /// </summary>
        /// <typeparam name="TChannel">WCF接口类型</typeparam>
        /// <returns></returns>
        public static TChannel GetChannel<TChannel>()
        {
            try
            {
                string endPointConfigName = typeof(TChannel).Name;
                if (Channels.ContainsKey(endPointConfigName))
                {
                    return (TChannel)Channels[endPointConfigName];
                }

                ChannelFactory<TChannel> channelFactory = new ChannelFactory<TChannel>(endPointConfigName);
                TChannel channel = channelFactory.CreateChannel();
                Channels.Add(endPointConfigName, channel);
                return channel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取库区编码
        /// </summary>
        /// <returns></returns>
        public static string GetAreaCode()
        {
            MCP.Config.Configuration confg = new MCP.Config.Configuration();
            confg.Load("Config.xml");
           string AreaCode = confg.Attributes["AreaCode"];
           confg.Release();
           return AreaCode;
        }
        public static string GetTaskTest()
        {
            MCP.Config.Configuration confg = new MCP.Config.Configuration();
            confg.Load("Config.xml");
            string TaskTest = confg.Attributes["TaskTest"];
            confg.Release();
            return TaskTest;
        }

        /// <summary>
        /// 中間數據庫插入WCS數據庫,并轉入WCS_Task
        /// </summary>
        /// <param name="dt"></param>
        internal static void InsertTaskToWcs(DataTable dt,bool blnUpdateAck)
        {


            lock (Locker1)
            {
                try
                {
                    BLL.BLLBase bllStock = new BLLBase("StockDB");
                    bool blnCheck = true;
                    string SubTaskID = "";
                    //判斷中間表貨位是否正常
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        if (dr["from_location_id"].ToString().Trim().Length == 6)
                        {
                            if (bllStock.GetRowCount("CMD_CEll", string.Format("AreaCode='{0}' and CellName='{1}'", dr["equipment"].ToString(), dr["from_location_id"].ToString())) == 0)
                            {
                                MCP.Logger.Error("WMS明細ID:" + dr["subtask_id"].ToString() + "分配貨位出錯!庫區" + dr["equipment"].ToString() + "不存在貨位:" + dr["from_location_id"].ToString());
                                blnCheck = false;
                                break;
                            }
                        }
                        if (dr["to_location_id"].ToString().Trim().Length == 6)
                        {
                            if (bllStock.GetRowCount("CMD_CEll", string.Format("AreaCode='{0}' and CellName='{1}'", dr["equipment"].ToString(), dr["to_location_id"].ToString())) == 0)
                            {
                                MCP.Logger.Error("WMS明細ID:" + dr["subtask_id"].ToString() + "分配貨位出錯!庫區" + dr["equipment"].ToString() + "不存在貨位:" + dr["to_location_id"].ToString());
                                blnCheck = false;
                                break;
                            }
                        }
                        SubTaskID = dr["subtask_id"].ToString() + ",";
                    }
                    if (!blnCheck)
                        return;


                    //删除现有数据，避免重复
                    bllStock.ExecNonQuery("WCS.DeleteAsrsTaskTMP", new DataParameter[] { new DataParameter("{0}", SubTaskID.TrimEnd(',')) });

                    //插入中間表
                    bllStock.BatchInsertTable(dt, "AsrsTask_TMP");

                    DataTable dtTaskID = dt.DefaultView.ToTable(true, "task_id");

                    List<string> MiddleComds = new List<string>();
                    List<DataParameter[]> Middleparas = new List<DataParameter[]>();

                    List<string> StockComds = new List<string>();
                    List<DataParameter[]> Stockparas = new List<DataParameter[]>();
                    for (int i = 0; i < dtTaskID.Rows.Count; i++)
                    {
                        DataRow[] drsTask = dt.Select(string.Format("task_id='{0}'", dtTaskID.Rows[i]["task_id"].ToString()));
                        //更新中間表状态
                        string taskID = drsTask[0]["task_id"].ToString();
                        string SubtaskID = drsTask[0]["subtask_id"].ToString();

                        MiddleComds.Add("Middle.UpdateAsrsTaskAck");
                        MiddleComds.Add("Middle.UpdateAsrsSubTaskAck");
                        string strWhere = string.Format("subtask_id='{0}'", SubtaskID);
                        if (drsTask.Length > 1)
                            strWhere = "1=1";
                        Middleparas.Add(new DataParameter[] { new DataParameter("@TaskID", taskID) });
                        Middleparas.Add(new DataParameter[] { new DataParameter("@TaskID", taskID), new DataParameter("{0}", strWhere) });
                        for (int j = 0; j < drsTask.Length; j++)
                        {
                            SubtaskID = drsTask[j]["subtask_id"].ToString();
                            StockComds.Add("WCS.InsertWCSTask");
                            Stockparas.Add(new DataParameter[] { new DataParameter("@subtaskid", SubtaskID) });
                        }
                    }

                    bllStock.ExecTran(StockComds.ToArray(), Stockparas);

                    BLL.BLLBase bllMiddle = new BLLBase("MiddleDB");
                    if (blnUpdateAck)
                    {
                        bllMiddle.ExecTran(MiddleComds.ToArray(), Middleparas);
                    }



                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
