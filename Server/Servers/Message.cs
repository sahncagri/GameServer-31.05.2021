using Common;
using System;

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


        public void ReadMessage(int newDataAmount,Action<ActionCode,RequestCode,string> OnReadMessage)
        {
            
        }
    }
}
