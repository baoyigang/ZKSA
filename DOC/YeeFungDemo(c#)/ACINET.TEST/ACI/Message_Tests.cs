using NDC8.ACINET.ACI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace NDC8.ACINET.ACI.Tests
{
    [TestClass()]
    public class Message_Tests
    {
        [TestMethod()]
        public void Message_b_NoIKEYTest()
        {

            //Arange
            byte[] data = { 00, 99, 01, 01, 255 };
            ushort type = 'b';

            //Act
            IACIMessage message = MessageParser.Parse(type, data);
            Message_b msg = (Message_b)message;

            //Assert
            Assert.AreEqual(msg.Index, 99);
            Assert.AreEqual(msg.TransportStructure, 1);
            Assert.AreEqual(msg.Status, 1);
            Assert.AreEqual(msg.ParNo, 255);
            Assert.AreEqual(msg.IKEY, 0);

        }

        [TestMethod()]
        public void Message_b_IkeyTest()
        {

            //Arange
            byte[] data = { 00, 99, 01, 01, 255, 00, 0x01, 0x99 };
            ushort type = 'b';

            //Act
            IACIMessage message = MessageParser.Parse(type, data);
            Message_b msg = (Message_b)message;

            //Assert
            Assert.AreEqual(msg.Index, 99);
            Assert.AreEqual(msg.TransportStructure, 1);
            Assert.AreEqual(msg.Status, 1);
            Assert.AreEqual(msg.ParNo, 255);
            Assert.AreEqual(msg.IKEY, 409);

        }

        [TestMethod()]
        public void Message_sTest()
        {
            //"002300000000000000020000000100000001562899EE00001C20000000000000000000000100000000060000000000000000";
            //Arange
            byte[] data = new byte[] { 0x00, 0x25, 0x11, 0xFF, 0x00, 0x01, 0x00, 0x02, 0xC8, 0x00, 0x11, 0x22, 0x10, 0x00, 0x00, 0x03, 0x00, 0x20,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0x20};
            ushort type = 's';

            //Act
            IACIMessage message = MessageParser.Parse(type, data);
            Message_s msg = (Message_s)message;

            //Assert
            Assert.AreEqual(msg.Index, 37);
            Assert.AreEqual(msg.TransportStructure, 17);
            Assert.AreEqual(msg.Status, 255);
            Assert.AreEqual(msg.Magic, 1);
            Assert.AreEqual(msg.CarrierNumber, 200);
            Assert.AreEqual(msg.CarrierStatus, 0x1122);
            Assert.AreEqual(msg.CarrierStation, 0x1000);
            Assert.AreEqual(msg.Values[(msg.NoVal - 1)], 32);

        }

        [TestMethod()]
        public void Message_ETest()
        {
            //"002300000000000000020000000100000001562899EE00001C20000000000000000000000100000000060000000000000000";
            //Arange
            byte[] data = new byte[] { 0x00, 0x23, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x56,
                0x28, 0x99, 0xEE, 0x00, 0x00, 0x1C, 0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x06,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            ushort type = 'E';

            //Act
            IACIMessage message = MessageParser.Parse(type, data);
            Message_E msg = (Message_E)message;

            //Assert
            Assert.AreEqual(msg.EventType, 1);

        }

        [TestMethod()]
        public void Message_wTest()
        {
            //Arange
            byte[] data = new byte[] { 0x00, 0x23, 0x05, 0x0A, 0x0B, 0x10, 0x1F, 0x01, 0x00, 0x01, 0x00, 0x02, 0x00, 0x03, 0x00, 0x04, 0x00, 0x05 };
            ushort type = 'w';

            //Act
            IACIMessage message = MessageParser.Parse(type, data);
            Message_w msg = (Message_w)message;

            //Assert
            Assert.AreEqual(msg.Index, 35);
            Assert.AreEqual(msg.NumberOfParameters, 5);
            Assert.AreEqual(msg.Parameter3Number, 31);
            Assert.AreEqual(msg.Parameter4Value, 5);

        }


        [TestMethod()]
        public void Message_rTest()
        {
            //Arange
            byte[] data = new byte[] { 0x00, 0xFF, 0x1E };
            ushort type = 'r';

            //Act
            IACIMessage message = MessageParser.Parse(type, data);
            Message_r msg = (Message_r)message;

            //Assert
            Assert.AreEqual(msg.Index, 255);
            Assert.AreEqual(msg.ParNo, 30);
        }

        [TestMethod()]
        public void Message_vpil_wordTest()
        {
            //Arange             //_ <   00   02    E0    00    01    00    00    03    08    07    01    00    00    02    06    05
            byte[] data = new byte[] { 0x00, 0x02, 0xE0, 0x00, 0x01, 0x00, 0x00, 0x03, 0x08, 0x07, 0x01, 0x00, 0x00, 0x02, 0x06, 0x05 };

            ushort type = '<';

            //Act
            IACIMessage message = MessageParser.Parse(type, data);
            Message_vpil msg = (Message_vpil)message;

            //Assert
            Assert.AreEqual(msg.CarId, 2);
            Assert.AreEqual(msg.Magic, 0xE000);
            Assert.AreEqual(msg.Code1, 1);
            Assert.AreEqual(msg.Value1, 0x0807);
            Assert.AreEqual(msg.Value2, 0x0605);
        }

        [TestMethod()]
        public void Message_vpil_MultibyteTest()
        {
            //Arange             //_ < 00    02     E0    01    06    01    00    00   0A    00     01    02    03    04    05    06    07    08    09    0A
            byte[] data = new byte[] { 0x00, 0x02, 0xE0, 0x01, 0x06, 0x01, 0x00, 0x00, 0x0A, 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A };

            ushort type = '<';

            //Act
            IACIMessage message = MessageParser.Parse(type, data);
            Message_vpil msg = (Message_vpil)message;

            //Assert
            Assert.AreEqual(msg.CarId, 2);
            Assert.AreEqual(msg.Magic, 0xE001);
            Assert.AreEqual(msg.Code, 6);
            Assert.AreEqual(msg.MultiByte[0], 0x01);
        }

        [TestMethod()]
        public void Message_unknownTest()
        {
            //Arange
            byte[] data = new byte[] { 0x00, 0xFF, 0x1E };
            ushort type = 'a';

            //Act
            IACIMessage message = MessageParser.Parse(type, data);
            Message_unknown msg = (Message_unknown)message;

            //Assert
            Assert.AreEqual(msg.Type, "a");
            Assert.AreEqual(msg.Data, data);
        }

        [TestMethod()]
        public void Message_pTest()
        {
            //_p   01    91    01    05    03    E8    00    01    00    02    00    03    00    04    00    05 
            byte[] data = new byte[] { 0x01, 0x91, 0x01, 0x05, 0x03, 0xE8, 0x00, 0x01, 0x00, 0x02, 0x00, 0x03, 0x00, 0x04, 0x00, 0x05 };

            ushort type = 'p';

            //Act
            IACIMessage message = MessageParser.Parse(type, data);
            Message_p msg = (Message_p)message;

            //Assert
            Assert.AreEqual(msg.Magic, 0x0191);
            Assert.AreEqual(msg.Code, 1);
            Assert.AreEqual(msg.NumberOfParameters, 0x05);
            Assert.AreEqual(msg.GlobalParameterOffset, 1000);
            Assert.AreEqual(msg.Values[4], 5);
        }

        [TestMethod()]
        public void Message_oTest()
        {
            //_o 57    04    00    03    00    08    00    00    58    81    B4    5F    64    64    06    02    05    01    00    03    01    00    04    06    FF    FF    01    00    00    00    00    00
            byte[] data = new byte[] { 0x04, 0x57, 0x00, 0x03, 0x00, 0x08, 0x00, 0x00, 0x58, 0x81, 0xB4, 0x5F, 0x64, 0x64, 0x06, 0x02, 0x05, 0x01, 0x00, 0x03, 0x01, 0x00, 0x04, 0x06, 0xFF, 0xFF, 0x01, 0x00, 0x01, 0x05, 0x02, 0x00 };

            ushort type = 'o';
            //Act
            IACIMessage message = MessageParser.Parse(type, data);
            Message_o msg = (Message_o)message;

            Assert.AreEqual(msg.Magic, 0x0457);
            Assert.AreEqual(msg.ItemCode, 3);
            Assert.AreEqual(msg.OrderIndex, 8);
            Assert.AreEqual(msg.CarrierStatus, 0xFFFF);
            Assert.AreEqual(msg.DestinationStation, 512);
        }

        [TestMethod()]
        public void Message_q_With_IkeyTest()
        {
            //Arrange
            byte[] bytearray = new byte[] { 250, 0x80, 0, 1, 0x55, 0x44, 0, 1, 0xF, 0xA1, 1, 0x4D, 0, 4, 0, 5, 0, 6, 0, 7, 0, 8, 0, 9 };
            List<int> parlist = new List<int>(new int[] { 1, 4001, 333, 4, 5, 6, 7, 8, 9 });
            Message_q msg = new Message_q(250, 0x80, 1, 0x5544, parlist);
            //act
            MsgBuffer msgBuffer = msg.ToAciMsgBuffer();


            int i = 0;
            foreach(var bytes in msgBuffer.Buffer)
            {
                Assert.AreEqual(bytes, bytearray[i]);
                i++;
            }
        }

        [TestMethod()]
        public void Message_qTest()
        {
            //Arrange
            byte[] bytearray = new byte[] { 250, 0x01, 0, 1, 0xF, 0xA1, 1, 0x4D, 0, 4, 0, 5, 0, 6, 0, 7, 0, 8, 0, 9 };
            List<int> parlist = new List<int>(new int[] { 1, 4001, 333, 4, 5, 6, 7, 8, 9 });
            Message_q msg = new Message_q(250, 0x01, parlist);
            //act
            MsgBuffer msgBuffer = msg.ToAciMsgBuffer();


            int i = 0;
            foreach(var bytes in msgBuffer.Buffer)
            {
                Assert.AreEqual(bytes, bytearray[i]);
                i++;
            }
        }

        [TestMethod()]
        public void Message_g_WriteTest()
        {
            //Arrange
            byte[] bytearray = new byte[] { 0, 250, 2, 9, 0, 10, 0, 1, 0, 2, 0, 3, 0, 4, 0, 5, 0, 6, 0, 7, 0, 8, 0, 9 };
            List<int> parlist = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            Message_g msg = new Message_g(250, 2, 9, 10, parlist);
            //act
            MsgBuffer msgBuffer = msg.ToAciMsgBuffer();

            int i = 0;
            foreach(var bytes in msgBuffer.Buffer)
            {
                Assert.AreEqual(bytes, bytearray[i]);
                i++;
            }
        }

        [TestMethod()]
        public void Message_g_ReadTest()
        {
            //Arrange
            byte[] bytearray = new byte[] { 0, 250, 2, 9, 0, 10 };
            Message_g msg = new Message_g(250, 2, 9, 10);
            //act
            MsgBuffer msgBuffer = msg.ToAciMsgBuffer();

            int i = 0;
            foreach(var bytes in msgBuffer.Buffer)
            {
                Assert.AreEqual(bytes, bytearray[i]);
                i++;
            }
        }

        [TestMethod()]
        public void Message_hpil_wordTest()
        {
            //Arrange
            byte[] bytearray = new byte[] { 00, 02, 0x01, 0x37, 2, 0, 0, 100, 0, 100, 2, 0, 0, 101, 0, 101 };
            Message_hpil_word msg = new Message_hpil_word(2, 311, (byte)HPIL_CODE.HPIL_CMDWR, 100, 100, (byte)HPIL_CODE.HPIL_CMDWR, 101, 101);

            //act
            MsgBuffer msgBuffer = msg.ToAciMsgBuffer();


            int i = 0;
            foreach(var bytes in msgBuffer.Buffer)
            {
                Assert.AreEqual(bytes, bytearray[i]);
                i++;
            };
        }

        [TestMethod()]
        public void Message_hpil_multiTest()
        {
            //Arrange
            byte[] bytearray = new byte[] { 00, 02, 0x01, 0x37, 5, 100, 0, 100, 20, 0 };
            Message_hpil_multi msg = new Message_hpil_multi(2, 311, (byte)HPIL_CODE.HPIL_CMDRDMU, 100, 100, 20, null);

            //act
            MsgBuffer msgBuffer = msg.ToAciMsgBuffer();


            int i = 0;
            foreach(var bytes in msgBuffer.Buffer)
            {
                Assert.AreEqual(bytes, bytearray[i]);
                i++;
            };
        }

        [TestMethod()]
        public void Message_m_change_prioTest()
        {
            //Arrange
            byte[] bytearray = new byte[] { 00, 55, 4, 99};
            Message_m msg = new Message_m(55 , 4 , 99);

            //act
            MsgBuffer msgBuffer = msg.ToAciMsgBuffer();


            int i = 0;
            foreach(var bytes in msgBuffer.Buffer)
            {
                Assert.AreEqual(bytes, bytearray[i]);
                i++;
            };
        }
    }
}