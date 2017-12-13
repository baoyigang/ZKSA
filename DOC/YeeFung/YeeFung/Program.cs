using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Messaging;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Soap;

namespace YeeFung
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creates a new TestSimpleObject object.
            TestSimpleObject obj = new TestSimpleObject();

            Console.WriteLine("Before serialization the object contains: ");
            obj.Print();

            //Opens a file and serializes the object into it in binary format.
            Stream stream = File.Open("data.xml", FileMode.Create);
            SoapFormatter formatter = new SoapFormatter();

            //BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(stream, obj);
            stream.Close();

            //Empties obj.
            obj = null;

            //Opens file "data.xml" and deserializes the object from it.
            stream = File.Open("data.xml", FileMode.Open);
            formatter = new SoapFormatter();

            //formatter = new BinaryFormatter();

            obj = (TestSimpleObject)formatter.Deserialize(stream);
            stream.Close();

            Console.WriteLine("");
            Console.WriteLine("After deserialization the object contains: ");
            obj.Print();

        }
        public static byte[] StructToBytes(object structObj)
        {
            int size = Marshal.SizeOf(structObj);
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(structObj, buffer, false);
                byte[] bytes = new byte[size];
                Marshal.Copy(buffer, bytes, 0, size);
                return bytes;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }
        public static object BytesToStruct(byte[] bytes, Type strcutType)
        {
            int size = Marshal.SizeOf(strcutType);
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(bytes, 0, buffer, size);
                return Marshal.PtrToStructure(buffer, strcutType);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }
        public static byte[] PackingMessageToBytes(MessageType messageType, uint featureCode, int messageLength, byte[] msgBytes)
        {
            DatagramHeaderFrame frame = new DatagramHeaderFrame();
            frame.MsgType = messageType;
            frame.FeatureCode = featureCode;
            frame.MessageLength = messageLength;

            byte[] header = StructToBytes(frame);

            byte[] datagram = new byte[header.Length + msgBytes.Length];
            header.CopyTo(datagram, 0);
            //msgBytes.CopyTo(datagram, FrameSize);
            msgBytes.CopyTo(datagram, header.Length);
            return datagram;
        }

        /// <summary>
        /// 封装消息和报文
        /// </summary>
        /// <param name="headerFrame">报文帧头</param>
        /// <param name="message">报文</param>
        /// <param name="encoding">编码器</param>
        /// <returns></returns>
        public static byte[] PackingMessageToBytes(DatagramHeaderFrame headerFrame, byte[] msgBytes)
        {
            byte[] header = StructToBytes(headerFrame);

            byte[] datagram = new byte[header.Length + msgBytes.Length];
            header.CopyTo(datagram, 0);
            //msgBytes.CopyTo(datagram, FrameSize);
            msgBytes.CopyTo(datagram, header.Length);
            return datagram;
        }
//        public static void Receive()
//        {
//            DatagramHeaderFrame headerFrame = new DatagramHeaderFrame();
//headerFrame.MsgType = messageType;
// headerFrame.MessageLength = bytes.Length;
// byte[] datagram = PackingMessageToBytes(headerFrame, bytes);
 
//GetStream().BeginWrite(datagram, 0, datagram.Length, HandleDatagramWritten, this);
//        }

//        public static void Send()
//        {
//        DatagramHeaderFrame headerFrame = new DatagramHeaderFrame();
// 2 byte[] datagramBytes = new byte[0];
// 3 
// 4 byte[] datagramBuffer = (byte[])ar.AsyncState;
// 5 byte[] recievedBytes = new byte[numberOfRecievedBytes];
// 6 
// 7 Buffer.BlockCopy(datagramBuffer, 0, recievedBytes, 0, numberOfRecievedBytes);
// 8                 PrasePacking(recievedBytes, numberOfRecievedBytes, ref headerFrame, ref datagramBytes);
// 9 
//10 GetStream().BeginRead(datagramBuffer, 0, datagramBuffer.Length, HandleDatagramReceived, datagramBuffer);
//        }
        public static void Test()
        {
            byte[] head = new byte[] { 0x7e };
            byte[] type = new byte[] { 0x00 };
            byte[] content = Encoding.Default.GetBytes("ABCDEGF");
            byte[] last = new byte[] { 0x23 };
            byte[] full = new byte[head.Length + type.Length + content.Length + last.Length];
            //head.CopyTo(full,0);   
            //type.CopyTo(full, head.Length);   
            //content.CopyTo(full,head.Length+type.Length);   
            //last.CopyTo(full, head.Length + type.Length + content.Length);   
            Stream s = new MemoryStream();
            s.Write(head, 0, 1);
            s.Write(type, 0, 1);
            s.Write(content, 0, content.Length);
            s.Write(last, 0, 1);
            s.Position = 0;
            int r = s.Read(full, 0, full.Length);
            if (r > 0)
            {
                Console.WriteLine(Encoding.Default.GetString(full));
                Console.WriteLine(full.Length);
                Console.WriteLine(full[0].ToString());
                Console.WriteLine(full[1].ToString());
                Console.WriteLine(full[9].ToString());
                Console.Read();
            }
        }

    }
    // A test object that needs to be serialized.
    [Serializable()]
    public class TestSimpleObject
    {

        public int member1;
        public string member2;
        public string member3;
        public double member4;

        // A field that is not serialized.
        [NonSerialized()]
        public string member5;

        public TestSimpleObject()
        {

            member1 = 11;
            member2 = "hello";
            member3 = "hello";
            member4 = 3.14159265;
            member5 = "hello world!";
        }


        public void Print()
        {

            Console.WriteLine("member1 = '{0}'", member1);
            Console.WriteLine("member2 = '{0}'", member2);
            Console.WriteLine("member3 = '{0}'", member3);
            Console.WriteLine("member4 = '{0}'", member4);
            Console.WriteLine("member5 = '{0}'", member5);
        }
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1, Size = 12)]
    [Serializable()]
    public struct DatagramHeaderFrame
    {
        // MessageType类型：
        public MessageType MsgType;

        //一个四个字节的特征码
        public uint FeatureCode;

        //用于标识报文的长度，用于校验
        public int MessageLength;
    }
    [StructLayout(LayoutKind.Explicit)]
    public struct Rect
    {
        [FieldOffset(0)]
        public int left;
        [FieldOffset(4)]
        public int top;
        [FieldOffset(8)]
        public int right;
        [FieldOffset(12)]
        public int bottom;
    }
}
