using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NDC8.ACINET.ACI;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            VCP9412 test = VCP9412.Instance;
            test.Open("192.168.1.100", 30001);

            List<int> intList=new List<int>();
            intList.Add(1);
            intList.Add(2);
            Message_q msg = new Message_q(1, 128, 1,1, intList);

            test.SendMessage(msg);

            Console.ReadKey(true);
        }
    }
}
