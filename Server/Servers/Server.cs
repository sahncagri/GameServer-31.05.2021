using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Servers
{
    class Server
    {
        public List<Client> clientList;
        public List<Room> roomList;

        private Socket serverSocket;
        private IPEndPoint endPoint;

        public Server(string ipStr)
        {
            endPoint = new IPEndPoint(IPAddress.Parse(ipStr),9688);
            serverSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            serverSocket.Bind(endPoint);
            serverSocket.Listen(0);

        }
        public void Start()
        {
            serverSocket.BeginAccept(AcceptCallback,null);
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            var clientSocket = serverSocket.EndAccept(ar);
            Client client = new Client(clientSocket,endPoint,this);
            clientList.Add(client);
            Start();
        }
    }
}
