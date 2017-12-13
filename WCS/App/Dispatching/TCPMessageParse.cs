using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using MCP;
 

namespace App.Dispatching
{
    public class TCPMessageParse : MCP.IProtocolParse
    {
        private const int HEADER_KEY = 0x87CD;
        private const int HEADER_SIZE = 8;
        public Message Parse(string msg)
        {
            Message result = null;

            try
            {

                string Comd = "";
                Dictionary<string, string> dictionary = new Dictionary<string, string>();


                string[] msgs = msg.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (msgs.Length < 4)
                {
                    msgs = new string[4];
                    msgs[0] = "";
                    msgs[1] = "";
                    msgs[2] = "";
                    msgs[3] = "";
                }

                Comd = msgs[0];
                dictionary.Add("BillNo", msgs[1]);
                dictionary.Add("Result", msgs[2]);
                dictionary.Add("MSG", msgs[3]);
                result = new Message(true, msg, Comd, dictionary);
            }
            catch
            {
                result = new Message(msg);
            }
            return result;
        }

        public Message Parse(byte[] msg)
        {
            Message result = null;
            try
            {
                ushort messageSize = BitConverter.ToUInt16(ShiftBytes(msg, 4, 2), 0);
                Array.Reverse(msg, 8, 2);  // message type
                Array.Reverse(msg, 10, 2); // num par

                // read num par to get size of telegram
                int telegramSize = BitConverter.ToUInt16(msg, 10);

                // get message part and type of data array
                byte[] messageData = msg.Skip(HEADER_SIZE + 4).Take(telegramSize).ToArray();
                ushort messageType = BitConverter.ToUInt16(msg, HEADER_SIZE);

                Dictionary<string, int> dictionary = new Dictionary<string, int>();

                string Comd = messageType.ToString();
                Array.Reverse(messageData, 0, 2); //index
                Array.Reverse(messageData, 4, 2); //phase
                Array.Reverse(messageData, 10, 2); //小车状态
                Array.Reverse(messageData, 12, 2); //目标站台
                Array.Reverse(messageData, 14, 2); //IKEY

                dictionary.Add("AGVIndex", BitConverter.ToInt16(messageData, 0));
                dictionary.Add("AGVPhase", BitConverter.ToInt16(messageData, 4));
                dictionary.Add("AGVDeviceNo", messageData[8]);
                dictionary.Add("AGVToStation", BitConverter.ToInt16(messageData, 12));
                dictionary.Add("AGVTaskID", BitConverter.ToInt16(messageData, 14));

                result = new Message(true, messageData.ToString(), Comd, dictionary);
            }
            catch
            {
                result = new Message(msg);
            }
            return result;
        }

        private byte[] ShiftBytes(byte[] buffer, int offset, int size)
        {
            return buffer.Skip(offset).Take(size).Reverse().ToArray();
        }
    }
}