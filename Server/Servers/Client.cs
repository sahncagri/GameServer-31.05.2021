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
        private Server server;
        private IPEndPoint endPoint;

        public Client(Socket _clientSocket,IPEndPoint endPoint, Server server)
        {
            this.clientSocket = _clientSocket;
            this.endPoint = endPoint;
            this.server = server;
        }

        public void Start()
        {
            clientSocket.Connect(endPoint);
            StartReceive();
        }
        private void StartReceive()
        {
            clientSocket.BeginReceive();
        }

        
    }
}
