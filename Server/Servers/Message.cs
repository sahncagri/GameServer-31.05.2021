using Common;
using System;
using System.Linq;
using System.Text;

namespace Server.Servers
{
    class Message
    {
        private byte[] data = new byte[1024];
        private int startIndex = 0;

        public byte[] Data
        {
            get { return data; }
        }
        public int StartIndex
        {
            get { return startIndex; }
        }
        public int RemainSize
        {
            get { return data.Length - startIndex; }
        }


        public void ReadMessage(int newDataAmount, Action<RequestCode, ActionCode, string> OnReadMessage)
        {
            startIndex += newDataAmount;
            while (true)
            {
                if (startIndex <= 4) return;
                int count = BitConverter.ToInt32(data, 0);
                if (startIndex - 4 >= count)
                {
                    RequestCode requestCode = (RequestCode)BitConverter.ToInt32(data, 4);
                    ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 8);
                    string s = Encoding.Default.GetString(data, 12, count - 8);
                    OnReadMessage(requestCode, actionCode, s);
                    Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);
                    startIndex -= (count + 4);
                    Console.WriteLine("StartIndex : " + startIndex);
                    Console.WriteLine("Count : " + count);
                }
                else
                {
                    break;
                }
            }
        }

        public static byte[] PackData(ActionCode actionCode, string data)
        {
            byte[] actionCodeBytes = BitConverter.GetBytes((int)actionCode);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            int dataAmount = actionCodeBytes.Length + dataBytes.Length;
            byte[] dataAMountBytes = BitConverter.GetBytes(dataAmount);
            return dataAMountBytes.Concat(actionCodeBytes).ToArray().Concat(dataBytes).ToArray();
        }
    }
}
