using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Util;
using DataGridViewAutoFilter;
using MCP;
using OPC;
using MCP.Service.Siemens.Config;
namespace App.View
{
    public partial class frmMonitor : BaseForm
    {
        private System.Timers.Timer tmWorkTimer = new System.Timers.Timer();
        BLL.BLLBase bll = new BLL.BLLBase();
        Dictionary<int, string> dicCraneFork = new Dictionary<int, string>();
        Dictionary<int, string> dicCraneState = new Dictionary<int, string>();
        Dictionary<int, string> dicCraneMode = new Dictionary<int, string>();
        DataTable dtDeviceAlarm;

        public frmMonitor()
        {
            InitializeComponent();
        }

        private void frmMonitor_Load(object sender, EventArgs e)
        {
            AddDicKeyValue();
            try
            {
                ServerInfo[] Servers = new MonitorConfig("Monitor.xml").Servers;
                Miniloads.OnMiniload += new MiniloadEventHandler(Monitor_OnMiniload);
                System.Threading.Thread.Sleep(300);
                Cranes.OnCrane += new CraneEventHandler(Monitor_OnCrane);
                for (int i = 0; i < Servers.Length; i++)
                {
                    OPCServer opcServer = new OPCServer(Servers[i].Name);
                    opcServer.Connect(Servers[i].ProgID, Servers[i].ServerName);// opcServer.Connect(config.ConnectionString);

                    OPCGroup group = opcServer.AddGroup(Servers[i].GroupName, Servers[i].UpdateRate);
                    foreach (ItemInfo item in Servers[i].Items)
                    {
                        group.AddItem(item.ItemName, item.OpcItemName, item.ClientHandler, item.IsActive);
                    }
                    //if (Servers[i].Name == "TranLineServer")
                    //{
                    //    opcServer.Groups.DefaultGroup.OnDataChanged += new OPCGroup.DataChangedEventHandler(Conveyor_OnDataChanged);
                    //}
                    //if (Servers[i].Name == "Car0101Server" || Servers[i].Name == "Car0102Server")
                    //{
                    //    //opcServer.Groups.DefaultGroup.OnDataChanged += new OPCGroup.DataChangedEventHandler(Car_OnDataChanged);
                    //}
                    if (Servers[i].Name.IndexOf("MiniloadServer")>=0)
                    {
                        opcServer.Groups.DefaultGroup.OnDataChanged += new OPCGroup.DataChangedEventHandler(Miniload_OnDataChanged);
                    }
                    if (Servers[i].Name.IndexOf("CraneServer")>=0)
                    {
                        opcServer.Groups.DefaultGroup.OnDataChanged += new OPCGroup.DataChangedEventHandler(Crane_OnDataChanged);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        #region Miniload监控

        private Dictionary<string, Miniload> dicMiniload = new Dictionary<string, Miniload>();

        void Miniload_OnDataChanged(object sender, DataChangedEventArgs e)
        {
            try
            {
                if (e.State == null)
                    return;

                string miniloadNo = e.ServerName.Replace("MiniloadServer", "");
                GetMiniload(miniloadNo);
                if  (e.ItemName.IndexOf("Mode") >= 0)
                    dicMiniload[miniloadNo].Mode = int.Parse(e.State.ToString());
                else if (e.ItemName.IndexOf("State1") >= 0)
                    dicMiniload[miniloadNo].State1 = int.Parse(e.State.ToString());
                else if (e.ItemName.IndexOf("Fork1") >= 0)
                    dicMiniload[miniloadNo].Fork1 = int.Parse(e.State.ToString());
                else if (e.ItemName.IndexOf("TaskNo1") >= 0)
                    dicMiniload[miniloadNo].TaskNo1 =Util.ConvertStringChar.BytesToString(ObjectUtil.GetObjects(e.States));
                else if (e.ItemName.IndexOf("State2") >= 0)
                    dicMiniload[miniloadNo].State2 = int.Parse(e.State.ToString());
                else if (e.ItemName.IndexOf("Fork2") >= 0)
                    dicMiniload[miniloadNo].Fork2 = int.Parse(e.State.ToString());
                else if (e.ItemName.IndexOf("TaskNo2") >= 0)
                    dicMiniload[miniloadNo].TaskNo2 = Util.ConvertStringChar.BytesToString(ObjectUtil.GetObjects(e.States));
                else if (e.ItemName.IndexOf("AlarmCode") >= 0)
                    dicMiniload[miniloadNo].AlarmCode = int.Parse(e.State.ToString());
                else if (e.ItemName.IndexOf("Station") >= 0)
                    dicMiniload[miniloadNo].Station = e.States;

                Miniloads.MiniloadInfo(dicMiniload[miniloadNo]);

            }
            catch (Exception ex)
            {
                MCP.Logger.Error("Miniload监控界面中Miniload_OnDataChanged出现异常" + ex.Message);
            }
        }

        private Miniload GetMiniload(string miniloadNo)
        {
            Miniload miniload = null;
            if (dicMiniload.ContainsKey(miniloadNo))
            {
                miniload = dicMiniload[miniloadNo];
            }
            else
            {
                miniload = new Miniload();
                miniload.MiniloadNo = miniloadNo;
                dicMiniload.Add(miniloadNo, miniload);
            }
            return miniload;
        }

        void Monitor_OnMiniload(MiniloadEventArgs args)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MiniloadEventHandler(Monitor_OnMiniload), args);
            }
            else
            {
                try
                {
                    Miniload miniload = args.miniload;
                    SetTextBoxText("txtMMode{0}", miniload.MiniloadNo, dicCraneMode[miniload.Mode]);
                    SetTextBoxText("txtMState{0}1", miniload.MiniloadNo, dicCraneState[miniload.State1]);
                    SetTextBoxText("txtMFork{0}1", miniload.MiniloadNo, dicCraneFork[miniload.Fork1]);
                    SetTextBoxText("txtMTaskNo{0}1", miniload.MiniloadNo, miniload.TaskNo1);

                    SetTextBoxText("txtMState{0}2", miniload.MiniloadNo, dicCraneState[miniload.State2]);
                    SetTextBoxText("txtMFork{0}2", miniload.MiniloadNo, dicCraneFork[miniload.Fork2]);
                    SetTextBoxText("txtMTaskNo{0}2", miniload.MiniloadNo, miniload.TaskNo2);
                    string strErrMsg = "";
                    if (miniload.AlarmCode > 0)
                    {
                        DataRow[] drs = dtDeviceAlarm.Select(string.Format("Flag=1 and AlarmCode={0}", miniload.AlarmCode));
                        if (drs.Length > 0)
                            strErrMsg = drs[0]["AlarmDesc"].ToString();
                        else
                            strErrMsg = "設備未知錯誤！";
                        SetControlColor("txtMAlarmCode{0}", miniload.MiniloadNo, Color.Red);
                        SetControlColor("btnMClearAlarm{0}", miniload.MiniloadNo, Color.Red);
                    }
                    else
                    {
                        SetControlColor("txtMAlarmCode{0}", miniload.MiniloadNo, SystemColors.Control);
                        SetControlColor("btnMClearAlarm{0}", miniload.MiniloadNo, SystemColors.Control);
                    }
                    SetTextBoxText("txtMAlarmCode{0}", miniload.MiniloadNo, miniload.AlarmCode.ToString());
                    SetTextBoxText("txtMAlarmDesc{0}", miniload.MiniloadNo, strErrMsg);
                    SetTextBoxText("txtMColumn{0}", miniload.MiniloadNo, miniload.Station[0].ToString());
                    SetTextBoxText("txtMRow{0}", miniload.MiniloadNo, miniload.Station[1].ToString());
                }
                catch (Exception ex)
                {
                    MCP.Logger.Error("监控界面中Monitor_OnMiniload出现异常" + ex.Message);
                }
            }
        }

        #endregion

        #region 输送线监控

        //private Dictionary<string, Conveyor> dicConveyor = new Dictionary<string, Conveyor>();

        //void Conveyor_OnDataChanged(object sender, DataChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (e.State == null)
        //            return;

        //        string txt = e.ItemName.Split('_')[0];
        //        Conveyor conveyor = GetConveyorByID(txt);
        //        conveyor.value = e.State.ToString();

        //        conveyor.ID = txt;


        //        Conveyors.ConveyorInfo(conveyor);

        //    }
        //    catch (Exception ex)
        //    {
        //        MCP.Logger.Error("输送线监控界面中Conveyor_OnDataChanged出现异常" + ex.Message);
        //    }
        //}

        //private Conveyor GetConveyorByID(string ID)
        //{
        //    Conveyor conveyor = null;
        //    if (dicConveyor.ContainsKey(ID))
        //    {
        //        conveyor = dicConveyor[ID];
        //    }
        //    else
        //    {
        //        conveyor = new Conveyor();
        //        conveyor.ID = ID;
        //        dicConveyor.Add(ID, conveyor);
        //    }
        //    return conveyor;
        //}

        //void Monitor_OnConveyor(ConveyorEventArgs args)
        //{
        //    if (InvokeRequired)
        //    {
        //        BeginInvoke(new ConveyorEventHandler(Monitor_OnConveyor), args);
        //    }
        //    else
        //    {
        //        try
        //        {
        //            Conveyor conveyor = args.conveyor;
        //            Button btn = GetButton(conveyor.ID);

        //            if (btn == null)
        //                return;

        //            if (conveyor.value == "0" && conveyor.ID.IndexOf("Conveyor") >= 0)
        //                btn.Text = "";
        //            else if (conveyor.value == "0" && conveyor.ID.IndexOf("UpDown") >= 0)
        //                btn.Text = "◎";
        //            else if (conveyor.value == "0" && conveyor.ID.IndexOf("Move") >= 0)
        //                btn.Text = "";
        //            else if (conveyor.value == "1" && conveyor.ID.IndexOf("Conveyor") >= 0) //有货未转
        //                btn.Text = "■";
        //            else if (conveyor.value == "1" && conveyor.ID.IndexOf("UpDown") >= 0) //有货未转
        //                btn.Text = "●";
        //            else if (conveyor.value == "1" && conveyor.ID.IndexOf("Move") >= 0)
        //                btn.Text = btn.Tag.ToString();
        //            else if (conveyor.value == "2") //无货未转
        //                btn.Text = "";
        //            else if (conveyor.value == "3") //转
        //                btn.Text = btn.Tag.ToString();
        //            else if (conveyor.value == "4")
        //                btn.BackColor = Color.Red;
        //            else
        //                btn.Text = "";

        //        }
        //        catch (Exception ex)
        //        {
        //            MCP.Logger.Error("监控界面中Monitor_OnConveyor出现异常" + ex.Message);
        //        }
        //    }
        //}

        #endregion

        #region 堆垛机监控
        void Crane_OnDataChanged(object sender, DataChangedEventArgs e)
        {
            if (e.State == null)
                return;
            string CraneNo = e.ServerName.Replace("CraneServer", "");
            GetCrane(CraneNo);
            if (e.ItemName.IndexOf("Mode") >= 0)
                dicCrane[CraneNo].Mode = int.Parse(e.State.ToString());
            else if (e.ItemName.IndexOf("State") >= 0)
                dicCrane[CraneNo].State = int.Parse(e.State.ToString());
            else if (e.ItemName.IndexOf("Fork") >= 0)
                dicCrane[CraneNo].Fork = int.Parse(e.State.ToString());
            else if (e.ItemName.IndexOf("TaskNo") >= 0)
                dicCrane[CraneNo].TaskNo = Util.ConvertStringChar.BytesToString(ObjectUtil.GetObjects(e.States));
            else if (e.ItemName.IndexOf("AlarmCode") >= 0)
                dicCrane[CraneNo].AlarmCode = int.Parse(e.State.ToString());
            else if (e.ItemName.IndexOf("Station") >= 0)
                dicCrane[CraneNo].Station = e.States;
            Cranes.CraneInfo(dicCrane[CraneNo]);
        }

        void Monitor_OnCrane(CraneEventArgs args)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new CraneEventHandler(Monitor_OnCrane), args);
            }
            else
            {
                Crane crane = args.crane;
                try
                {

                    if (crane.Mode != null)
                        SetTextBoxText("txtMode{0}", crane.CraneNo, dicCraneMode[crane.Mode]);
                    if (crane.State != null)
                        SetTextBoxText("txtState{0}", crane.CraneNo, dicCraneState[crane.State]);
                    if (crane.Fork != null)
                        SetTextBoxText("txtFork{0}", crane.CraneNo, dicCraneFork[crane.Fork]);
                    if (crane.TaskNo == null)
                        crane.TaskNo = "";
                    SetTextBoxText("txtTaskNo{0}", crane.CraneNo, crane.TaskNo);
                    string strErrMsg = "";
                    if (crane.AlarmCode != null)
                    {
                        if (crane.AlarmCode > 0)
                        {
                            DataRow[] drs = dtDeviceAlarm.Select(string.Format("Flag=1 and AlarmCode={0}", crane.AlarmCode));
                            if (drs.Length > 0)
                                strErrMsg = drs[0]["AlarmDesc"].ToString();
                            else
                                strErrMsg = "設備未知錯誤！";
                            SetControlColor("txtAlarmCode{0}", crane.CraneNo, Color.Red);
                            SetControlColor("btnClearAlarm{0}", crane.CraneNo, Color.Red);
                        }
                        else
                        {
                            SetControlColor("txtAlarmCode{0}", crane.CraneNo, SystemColors.Control);
                            SetControlColor("btnClearAlarm{0}", crane.CraneNo, SystemColors.Control);
                        }
                    }
                    SetTextBoxText("txtAlarmCode{0}", crane.CraneNo, crane.AlarmCode.ToString());
                    SetTextBoxText("txtAlarmDesc{0}", crane.CraneNo, strErrMsg);
                    SetTextBoxText("txtColumn{0}", crane.CraneNo, crane.Station[0].ToString());
                    SetTextBoxText("txtRow{0}", crane.CraneNo, crane.Station[1].ToString());
                    
                }
                catch (Exception ex)
                {
                    MCP.Logger.Error("监控界面中Monitor_OnCrane出现异常UL" + crane.CraneNo + " 錯誤內容:" + ex.Message);
                }
            }
        }
        private Dictionary<string, Crane> dicCrane = new Dictionary<string, Crane>();
        private Crane GetCrane(string craneno)
        {
            Crane crane = null;
            if (dicCrane.ContainsKey(craneno))
            {
                crane = dicCrane[craneno];
            }
            else
            {
                crane = new Crane();
                crane.CraneNo = craneno;
                dicCrane.Add(craneno, crane);
            }
            return crane;
        }
        #endregion

        private void AddDicKeyValue()
        {
            dicCraneFork.Add(0, "原點");
            dicCraneFork.Add(1, "左側");
            dicCraneFork.Add(2, "右側");

            dicCraneState.Add(0, "未知");
            dicCraneState.Add(1, "空閒");
            dicCraneState.Add(2, "檢查任務數據");
            dicCraneState.Add(3, "定位到取貨位");
            dicCraneState.Add(4, "取貨中");
            dicCraneState.Add(7, "取貨完成");
            dicCraneState.Add(8, "等待調度柜允許");
            dicCraneState.Add(9, "移動到放貨位置");
            dicCraneState.Add(10, "放貨中");
            dicCraneState.Add(13, "搬運完成");
            dicCraneState.Add(14, "空載避讓");
            dicCraneState.Add(15, "檢查任務數據");
            dicCraneState.Add(20, "檢查源位置");
            dicCraneState.Add(21, "檢查目標位置");
            dicCraneState.Add(99, "報警");
         
            dicCraneMode.Add(0, "關機模式");
            dicCraneMode.Add(1, "自動模式");
            dicCraneMode.Add(2, "手動模式");
            dicCraneMode.Add(3, "半自動模式");
            dicCraneMode.Add(4, "維修模式");

            dtDeviceAlarm = bll.FillDataTable("WCS.SelectDeviceAlarm", new DataParameter[] { new DataParameter("{0}", "Flag=1") });
        }

        private void SetTextBoxText(string name, string CraneNo, string msg)
        {
            TextBox txt;
            Control[] ctl = this.Controls.Find(string.Format(name, CraneNo), true);
            if (ctl.Length > 0)
                txt = (TextBox)ctl[0];
            else
                txt = null;
            if (txt != null)
                txt.Text = msg;
        }
        private void SetControlColor(string name, string CraneNo,Color red)
        {
            Control txt;
            Control[] ctl = this.Controls.Find(string.Format(name, CraneNo), true);
            if (ctl.Length > 0)
                txt = ctl[0];
            else
                txt = null;
            if (txt != null)
                txt.BackColor = red;
        }

      

        private void btnStop_Click(object sender, EventArgs e)
        {
            string PrefixName = "CranePLC";
            string AreaCode = "UL";
            string CraneNo = "";
            string btnNam = ((Button)sender).Name;
            if (btnNam.IndexOf("btnStop") >= 0)
            {
                PrefixName = "CranePLC";
                AreaCode = "UL";
                CraneNo = btnNam.Replace("btnStop", "");
            }
            else
            {
                PrefixName = "MiniLoad";
                AreaCode = "ML";
                CraneNo = btnNam.Replace("btnMStop", "");
            }
            string ServerName = PrefixName + CraneNo;
            if (MessageBox.Show(string.Format("是否要急停{0}堆垛機?", AreaCode + CraneNo), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                Context.ProcessDispatcher.WriteToService(ServerName, "Stop", 1);
                Logger.Info(string.Format("{0}堆垛機下發急停命令", AreaCode + CraneNo));
            }
        }

        //private void btnConveyor_MouseEnter(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Button btn = (Button)sender;
        //        string Number = btn.Name.Substring(btn.Name.Length - 2, 2);
        //        string Barcode = "";
        //        string AreaCode = BLL.Server.GetAreaCode();
        //        if (AreaCode != "001")
        //        {
        //            Barcode = Util.ConvertStringChar.BytesToString(ObjectUtil.GetObjects(Context.ProcessDispatcher.WriteToService("TranLine", "ConveyorInfo" + Number)));
        //            this.toolTip1.SetToolTip(btn, Barcode);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex.Message);
        //    }
        //}

        private void btnBack_Click(object sender, EventArgs e)
        {
            string PrefixName = "CranePLC";
            string AreaCode="UL";
            string CraneNo = "";
            string ItemName = "TaskType";
            string btnNam=((Button)sender).Name;
            if (btnNam.IndexOf("btnBack") >= 0)
            {
                PrefixName = "CranePLC";
                AreaCode = "UL";
                CraneNo = btnNam.Replace("btnBack", "");
            }
            else
            {
                PrefixName = "MiniLoad";
                AreaCode = "ML";
                CraneNo = btnNam.Replace("btnMBack", "");
                ItemName = "TaskType2";
            }
            string ServerName = PrefixName + CraneNo;
            if (MessageBox.Show(string.Format("是否要召回{0}堆垛機到初始位置?", AreaCode + CraneNo), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                Context.ProcessDispatcher.WriteToService(ServerName, ItemName, 4);
                Context.ProcessDispatcher.WriteToService(ServerName, "WriteFinished", 1);
                Logger.Info(string.Format("{0}堆垛機下發召回命令", AreaCode + CraneNo));
            }
        }

        private void btnClearAlarm_Click(object sender, EventArgs e)
        {
            string PrefixName = "CranePLC";
            string AreaCode = "UL";
            string CraneNo = "";
            
            string btnNam = ((Button)sender).Name;
            if (btnNam.IndexOf("btnClearAlarm") >= 0)
            {
                PrefixName = "CranePLC";
                AreaCode = "UL";
                CraneNo = btnNam.Replace("btnClearAlarm", "");
            }
            else
            {
                PrefixName = "MiniLoad";
                AreaCode = "ML";
                CraneNo = btnNam.Replace("btnMClearAlarm", "");
                
            }
            string ServerName = PrefixName + CraneNo;

            Context.ProcessDispatcher.WriteToService(ServerName, "Reset", 1);
            Logger.Info(string.Format("{0}堆垛機解警", AreaCode + CraneNo));

        }

        

           
    }
}