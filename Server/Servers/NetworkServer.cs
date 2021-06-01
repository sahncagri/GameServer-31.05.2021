using Common;
using Server.Controller;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Server.Servers
{
    class NetworkServer
    {
        public List<Client> clientList;
        public List<Room> roomList;

        private Socket serverSocket;
        private IPEndPoint endPoint;
        private ControllerManager controllerManager;

        public NetworkServer(string ipStr)
        {
            endPoint = new IPEndPoint(IPAddress.Parse(ipStr), 9688);
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(endPoint);
            serverSocket.Listen(0);
            clientList = new List<Client>();
            controllerManager = new ControllerManager(this);
        }
        public void Start()
        {
            serverSocket.BeginAccept(AcceptCallback, null);
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            var clientSocket = serverSocket.EndAccept(ar);
            Client client = new Client(clientSocket, this);
            clientList.Add(client);
            client.Start();
            Start();
        }

        public void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data, Client client)
        {
            controllerManager.HandleRequest(requestCode,actionCode,data,client);
        }

        public void SendResponse(Client client, ActionCode actionCode, string data)
        {
            client.SendData(actionCode,data);
        }
    }
}
