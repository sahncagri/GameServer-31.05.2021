using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Servers
{
    class Room
    {
        private List<Client> clientRoomList = new List<Client>();


        public void AddClient(Client client)
        {
            if(!clientRoomList.Contains(client))
            {
                clientRoomList.Add(client);
            }
        }

        public void RemoveClient(Client client)
        {
            if(clientRoomList.Contains(client))
            {
                clientRoomList.Remove(client);
            }
        }
    }
}
  