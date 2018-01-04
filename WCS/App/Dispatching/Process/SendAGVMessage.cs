using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Util;
namespace App.Dispatching.Process
{
    public class SendAGVMessage
    {
        private static Object thisLock = new Object();
        /// <summary>
        /// 正常发送Q消息，TS为1
        /// </summary>
        public static byte[] GetSendTask1(UInt16 AGVTaskID, UInt16 FromStation, UInt16 ToStation, UInt16 AGVActionID)
        {
            byte[] msg = new byte[24];
            byte[] b = BitConverter.GetBytes((UInt16)0x87cd); // Header key
            Array.Reverse(b, 0, 2);
            msg[0] = b[0];
            msg[1] = b[1];
            b = BitConverter.GetBytes((UInt16)0x0008);//Size of Header
            Array.Reverse(b, 0, 2);
            msg[2] = b[0];
            msg[3] = b[1];

            b = BitConverter.GetBytes((UInt16)0x0010);//Size of Message
            Array.Reverse(b, 0, 2);
            msg[4] = b[0];
            msg[5] = b[1];


            b = BitConverter.GetBytes((UInt16)0x0001);//Function code
            Array.Reverse(b, 0, 2);
            msg[6] = b[0];
            msg[7] = b[1];

            b = BitConverter.GetBytes((UInt16)0x0071);//Msg.type
            Array.Reverse(b, 0, 2);
            msg[8] = b[0];
            msg[9] = b[1];

            b = BitConverter.GetBytes((UInt16)0x000C);//Number of parameters
            Array.Reverse(b, 0, 2);
            msg[10] = b[0];
            msg[11] = b[1];

            msg[12] = 0x01; //trp str
            msg[13] = 0x80; //默认优先级

            b = BitConverter.GetBytes((UInt16)0x0001);//固定
            Array.Reverse(b, 0, 2);
            msg[14] = b[0];
            msg[15] = b[1];

            b = BitConverter.GetBytes(AGVTaskID);//IKEY
            Array.Reverse(b, 0, 2);
            msg[16] = b[0];
            msg[17] = b[1];

            b = BitConverter.GetBytes(FromStation);//取货站台
            Array.Reverse(b, 0, 2);
            msg[18] = b[0];
            msg[19] = b[1];

            b = BitConverter.GetBytes(ToStation);//卸货站台
            Array.Reverse(b, 0, 2);
            msg[20] = b[0];
            msg[21] = b[1];

            b = BitConverter.GetBytes(AGVActionID);//取货操作码
            Array.Reverse(b, 0, 2);
            msg[22] = b[0];
            msg[23] = b[1];
            return msg;
        }

        /// <summary>
        /// 正常发送Q消息，TS为100
        /// </summary>
        public static byte[] GetSendTask100(UInt16 AGVTaskID, UInt16 Station, UInt16 AGVActionID)
        {
            byte[] msg = new byte[22];
            byte[] b = BitConverter.GetBytes((UInt16)0x87cd); // Header key
            Array.Reverse(b, 0, 2);
            msg[0] = b[0];
            msg[1] = b[1];
            b = BitConverter.GetBytes((UInt16)0x0008);//Size of Header
            Array.Reverse(b, 0, 2);
            msg[2] = b[0];
            msg[3] = b[1];

            b = BitConverter.GetBytes((UInt16)0x000C);//Size of Message
            Array.Reverse(b, 0, 2);
            msg[4] = b[0];
            msg[5] = b[1];


            b = BitConverter.GetBytes((UInt16)0x0001);//Function code
            Array.Reverse(b, 0, 2);
            msg[6] = b[0];
            msg[7] = b[1];

            b = BitConverter.GetBytes((UInt16)0x0071);//Msg.type
            Array.Reverse(b, 0, 2);
            msg[8] = b[0];
            msg[9] = b[1];

            b = BitConverter.GetBytes((UInt16)0x000A);//Number of parameters
            Array.Reverse(b, 0, 2);
            msg[10] = b[0];
            msg[11] = b[1];

            msg[12] = 100; //trp str
            msg[13] = 0x80; //默认优先级

            b = BitConverter.GetBytes((UInt16)0x0001);//固定
            Array.Reverse(b, 0, 2);
            msg[14] = b[0];
            msg[15] = b[1];

            b = BitConverter.GetBytes(AGVTaskID);//IKEY
            Array.Reverse(b, 0, 2);
            msg[16] = b[0];
            msg[17] = b[1];

            b = BitConverter.GetBytes(Station);//指定站台
            Array.Reverse(b, 0, 2);
            msg[18] = b[0];
            msg[19] = b[1];

            b = BitConverter.GetBytes(AGVActionID);//操作码
            Array.Reverse(b, 0, 2);
            msg[20] = b[0];
            msg[21] = b[1];
            return msg;
        }
        /// <summary>
        /// 发送M消息，确认
        /// </summary>
        public static byte[] GetSendCheckMsg(UInt16 AGVIndex, UInt16 AGVPhase)
        {
            byte[] msg = new byte[18];
            byte[] b = BitConverter.GetBytes((UInt16)0x87cd); // Header key
            Array.Reverse(b, 0, 2);
            msg[0] = b[0];
            msg[1] = b[1];
            b = BitConverter.GetBytes((UInt16)0x0008);//Size of Header
            Array.Reverse(b, 0, 2);
            msg[2] = b[0];
            msg[3] = b[1];

            b = BitConverter.GetBytes((UInt16)0x000a);//Size of Message
            Array.Reverse(b, 0, 2);
            msg[4] = b[0];
            msg[5] = b[1];


            b = BitConverter.GetBytes((UInt16)0x0001);//Function code
            Array.Reverse(b, 0, 2);
            msg[6] = b[0];
            msg[7] = b[1];

            b = BitConverter.GetBytes((UInt16)0x006d);//Msg.type
            Array.Reverse(b, 0, 2);
            msg[8] = b[0];
            msg[9] = b[1];

            b = BitConverter.GetBytes((UInt16)0x0006);//Number of parameters
            Array.Reverse(b, 0, 2);
            msg[10] = b[0];
            msg[11] = b[1];

            b = BitConverter.GetBytes(AGVIndex); //index
            Array.Reverse(b, 0, 2);
            msg[12] = b[0];
            msg[13] = b[1];

            msg[14] = 0x01;
            msg[15] = 23;
            b = BitConverter.GetBytes(AGVPhase); //index
            Array.Reverse(b, 0, 2);
            msg[16] = b[0];
            msg[17] = b[1];
            return msg;
        }
        /// <summary>
        /// 发送M消息，取消任务
        /// </summary>
        public static byte[] GetSendCancelTask(UInt16 AGVIndex)
        {
            byte[] msg = new byte[18];
            byte[] b = BitConverter.GetBytes((UInt16)0x87cd); // Header key
            Array.Reverse(b, 0, 2);
            msg[0] = b[0];
            msg[1] = b[1];
            b = BitConverter.GetBytes((UInt16)0x0008);//Size of Header
            Array.Reverse(b, 0, 2);
            msg[2] = b[0];
            msg[3] = b[1];

            b = BitConverter.GetBytes((UInt16)0x000a);//Size of Message
            Array.Reverse(b, 0, 2);
            msg[4] = b[0];
            msg[5] = b[1];


            b = BitConverter.GetBytes((UInt16)0x0001);//Function code
            Array.Reverse(b, 0, 2);
            msg[6] = b[0];
            msg[7] = b[1];

            b = BitConverter.GetBytes((UInt16)0x006d);//Msg.type
            Array.Reverse(b, 0, 2);
            msg[8] = b[0];
            msg[9] = b[1];

            b = BitConverter.GetBytes((UInt16)0x0006);//Number of parameters
            Array.Reverse(b, 0, 2);
            msg[10] = b[0];
            msg[11] = b[1];

            b = BitConverter.GetBytes(AGVIndex); //index
            Array.Reverse(b, 0, 2);
            msg[12] = b[0];
            msg[13] = b[1];

            msg[14] = 0x01;
            msg[15] = 25;
            b = BitConverter.GetBytes((UInt16)0x008F); //index
            Array.Reverse(b, 0, 2);
            msg[16] = b[0];
            msg[17] = b[1];
            return msg;
        }

        /// <summary>
        /// 发送M消息，变更站台
        /// </summary>
        public static byte[] GetSendChangeStation(UInt16 AGVIndex, UInt16 NewStation)
        {
            byte[] msg = new byte[20];
            byte[] b = BitConverter.GetBytes((UInt16)0x87cd); // Header key
            Array.Reverse(b, 0, 2);
            msg[0] = b[0];
            msg[1] = b[1];
            b = BitConverter.GetBytes((UInt16)0x0008);//Size of Header
            Array.Reverse(b, 0, 2);
            msg[2] = b[0];
            msg[3] = b[1];

            b = BitConverter.GetBytes((UInt16)0x000a);//Size of Message
            Array.Reverse(b, 0, 2);
            msg[4] = b[0];
            msg[5] = b[1];


            b = BitConverter.GetBytes((UInt16)0x0001);//Function code
            Array.Reverse(b, 0, 2);
            msg[6] = b[0];
            msg[7] = b[1];

            b = BitConverter.GetBytes((UInt16)0x006d);//Msg.type
            Array.Reverse(b, 0, 2);
            msg[8] = b[0];
            msg[9] = b[1];

            b = BitConverter.GetBytes((UInt16)0x0008);//Number of parameters
            Array.Reverse(b, 0, 2);
            msg[10] = b[0];
            msg[11] = b[1];

            b = BitConverter.GetBytes(AGVIndex); //index
            Array.Reverse(b, 0, 2);
            msg[12] = b[0];
            msg[13] = b[1];

            msg[14] = 0x01;
            msg[15] = 0x12;
            b = BitConverter.GetBytes((UInt16)0x008E); //index
            Array.Reverse(b, 0, 2);
            msg[16] = b[0];
            msg[17] = b[1];

            b = BitConverter.GetBytes(NewStation); //index
            Array.Reverse(b, 0, 2);
            msg[18] = b[0];
            msg[19] = b[1];
            return msg;
        }

        public static UInt16 GetAGVTaskID()
        {
            int TaskID = 1;
            lock (thisLock)
            {

                BLL.BLLBase bll = new BLL.BLLBase();
                DataTable dtTable = bll.FillDataTable("WCS.SelectSysTmpCode", new DataParameter[] { new DataParameter("{0}", string.Format("RowIndex=1 and SysDate='{0}'", DateTime.Now.ToString("yyyy/MM/dd"))) });
                if (dtTable.Rows.Count > 0)
                {
                    TaskID = UInt16.Parse(dtTable.Rows[0]["TmpCode"].ToString());
                    TaskID++;
                }
                if (TaskID >= 65535)
                    TaskID = 1;

                bll.ExecNonQuery("WCS.UpdateSysTmpCode", new DataParameter[] { new DataParameter("@TmpCode", TaskID), new DataParameter("@SysDate", DateTime.Now.ToString("yyyy/MM/dd")), new DataParameter("@RowIndex", 1) });
            }

            return (UInt16)TaskID;
        }


        public static UInt16 GetAGVActionID()
        {
            return 272;
        }
    }
}
