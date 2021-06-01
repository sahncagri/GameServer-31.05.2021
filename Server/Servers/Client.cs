using Common;
using MySql.Data.MySqlClient;
using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Servers
{
    class Client
    {
        private Socket clientSocket;
        private NetworkServer server;
        private IPEndPoint endPoint;
        private Message msg= new Message();

        private MySqlConnection mysqlConn;
        private User user;
        private Result result;

        public MySqlConnection MysqlConn
        {
            get { return mysqlConn; }
        }

        public Client(Socket _clientSocket, NetworkServer server)
        {
            this.clientSocket = _clientSocket;
            this.server = server;
            mysqlConn = ConnHelper.Connect();
        }

        public void SetUserData(User user, Result res)
        {
            this.user = user;
            this.result = res;
        }

        public void Start()
        {
            StartReceive();
        }
        private void StartReceive()
        {
            clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallback, null);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            int count = clientSocket.EndReceive(ar);
            if (count == 0) Close();

            msg.ReadMessage(count, OnReadMessage);
            StartReceive();
        }

        private void OnReadMessage(RequestCode requestCode, ActionCode actionCode, string data)
        {
            server.HandleRequest(requestCode, actionCode, data, this);
        }

        private void Close()
        {
            clientSocket?.Close();
            // server listten clienti sil 
            // Oda olusturduysan odadan cikar
        }

        public void SendData(ActionCode actionCode, string data)
        {
            byte[] dataToSend = Message.PackData(actionCode,data);
            if(clientSocket != null && dataToSend != null)
            {
                clientSocket.Send(dataToSend);
            }
        }
    }
}
