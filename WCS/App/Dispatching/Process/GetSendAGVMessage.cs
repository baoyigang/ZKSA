using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Dispatching.Process
{
    public class GetSendAGVMessage
    {
        /// <summary>
        /// 正常发送Q消息，TS为1
        /// </summary>
        public static byte[] GetSendTask1()
        {
            byte[] msg = new byte[10];
            //msg[0] = 0x87cd;
            return null;
        }

        /// <summary>
        /// 正常发送Q消息，TS为100
        /// </summary>
        public static byte[] SendTask100()
        {
            return null;
        }
        /// <summary>
        /// 发送M消息，确认
        /// </summary>
        public static byte[] SendCheckMsg()
        {
            return null;
        }
        /// <summary>
        /// 发送M消息，取消任务
        /// </summary>
        public static byte[] SendCancelTask()
        {
            return null;
        }

        /// <summary>
        /// 发送M消息，取消任务
        /// </summary>
        public static byte[] SendChangeStation()
        {
            return null;
        }
    }
}
