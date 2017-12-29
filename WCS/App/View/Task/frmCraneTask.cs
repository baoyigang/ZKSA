using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Util;

namespace App.View.Task
{
    public partial class frmCraneTask : BaseForm
    {
        BLL.BLLBase bll = new BLL.BLLBase();
        
        public frmCraneTask()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
 
        private void btnRead_Click(object sender, EventArgs e)
        {
            string strvalue = MCP.ObjectUtil.GetObject(Context.ProcessDispatcher.WriteToService(txtServerName.Text, txtItemName.Text)).ToString();
            string strValue1 = Util.ConvertStringChar.BytesToString(MCP.ObjectUtil.GetObjects(Context.ProcessDispatcher.WriteToService(txtServerName.Text, txtItemName.Text)));
            this.textBox3.Text = strvalue;
        }

      

        private void btnWrite_Click(object sender, EventArgs e)
        {
            Context.ProcessDispatcher.WriteToService(txtServerName.Text, this.txtItemName.Text, this.textBox3.Text);
        }



        private void btnCraneTask_Click(object sender, EventArgs e)
        {
            string serviceName = "CraneService";

            int[] cellAddr = new int[6];


            cellAddr[0] = int.Parse(this.txtSL.Text);
            cellAddr[1] = int.Parse(this.txtSC.Text);
            cellAddr[2] = 2;
            cellAddr[3] = int.Parse(this.txtTL.Text);
            cellAddr[4] = int.Parse(this.txtTC.Text);
            cellAddr[5] = 1;

            sbyte[] sTaskNo = new sbyte[20];
            Util.ConvertStringChar.stringToBytes(this.txtNo.Text, 20).CopyTo(sTaskNo, 0);
            Context.ProcessDispatcher.WriteToService(serviceName, "TaskNo1", sTaskNo);
            Context.ProcessDispatcher.WriteToService(serviceName, "TaskAddress1", cellAddr);
            Context.ProcessDispatcher.WriteToService(serviceName, "TaskType1", chk1.Checked ? 2 : 1);

            //    Logger.Info("任务号:" + drs[i]["TaskNo"].ToString() + "已下发载货提升机工位" + TaskIndex + "地址:" + drs[i]["CellCode"].ToString());

            if (this.txtSL2.Text != "")
            {
                cellAddr = new int[6];


                cellAddr[0] = int.Parse(this.txtSL2.Text);
                cellAddr[1] = int.Parse(this.txtSC2.Text);
                cellAddr[2] = 2;
                cellAddr[3] = int.Parse(this.txtTL2.Text);
                cellAddr[4] = int.Parse(this.txtTC2.Text);
                cellAddr[5] = 1;

                sTaskNo = new sbyte[20];
                Util.ConvertStringChar.stringToBytes(this.txtNo2.Text, 20).CopyTo(sTaskNo, 0);
                Context.ProcessDispatcher.WriteToService(serviceName, "TaskNo2", sTaskNo);
                Context.ProcessDispatcher.WriteToService(serviceName, "TaskAddress2", cellAddr);
                Context.ProcessDispatcher.WriteToService(serviceName, "TaskType2", chk1.Checked ? 2 : 1);
            }
            Context.ProcessDispatcher.WriteToService(serviceName, "WriteFinished", 1);

            MCP.Logger.Info("测试出库任务号已下发载货提升机工位");

        }

        private void btnAGVTask_Click(object sender, EventArgs e)
        {
            ushort AgvTaskID = ushort.Parse(this.txtAGVTaskID.Text);
            ushort FromStation = ushort.Parse(this.txtFromStation.Text);
            ushort ToStation = ushort.Parse(this.txtToStation.Text);
            ushort AGVActionID = ushort.Parse(this.txtActionID.Text);
            byte[] GetAGVbyte = App.Dispatching.Process.SendAGVMessage.GetSendTask1(AgvTaskID, FromStation, ToStation, AGVActionID);
            Context.ProcessDispatcher.WriteToService("AGVService", "ACK", GetAGVbyte);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string serviceName = "CraneService";

            int[] cellAddr = new int[6];


            cellAddr[0] = int.Parse(this.txtSL3.Text);
            cellAddr[1] = int.Parse(this.txtSR3.Text);
            cellAddr[2] = 1;
            cellAddr[3] = int.Parse(this.txtTL3.Text);
            cellAddr[4] = int.Parse(this.txtTR3.Text);
            cellAddr[5] = 2;

            sbyte[] sTaskNo = new sbyte[20];
            Util.ConvertStringChar.stringToBytes(this.txtNo.Text, 20).CopyTo(sTaskNo, 0);
            Context.ProcessDispatcher.WriteToService(serviceName, "TaskNo1", sTaskNo);
            Context.ProcessDispatcher.WriteToService(serviceName, "TaskAddress1", cellAddr);
            Context.ProcessDispatcher.WriteToService(serviceName, "TaskType1", chk1.Checked ? 2 : 1);

            //    Logger.Info("任务号:" + drs[i]["TaskNo"].ToString() + "已下发载货提升机工位" + TaskIndex + "地址:" + drs[i]["CellCode"].ToString());

            if (this.txtSL2.Text != "")
            {
                cellAddr = new int[6];


                cellAddr[0] = int.Parse(this.txtSL4.Text);
                cellAddr[1] = int.Parse(this.txtSR4.Text);
                cellAddr[2] = 1;
                cellAddr[3] = int.Parse(this.txtTL4.Text);
                cellAddr[4] = int.Parse(this.txtTR4.Text);
                cellAddr[5] = 2;

                sTaskNo = new sbyte[20];
                Util.ConvertStringChar.stringToBytes(this.txtNo2.Text, 20).CopyTo(sTaskNo, 0);
                Context.ProcessDispatcher.WriteToService(serviceName, "TaskNo2", sTaskNo);
                Context.ProcessDispatcher.WriteToService(serviceName, "TaskAddress2", cellAddr);
                Context.ProcessDispatcher.WriteToService(serviceName, "TaskType2", chk1.Checked ? 2 : 1);
            }
            Context.ProcessDispatcher.WriteToService(serviceName, "WriteFinished", 1);

            MCP.Logger.Info("测试入库任务号已下发载货提升机工位");
        }

        
 

       

    }
}
