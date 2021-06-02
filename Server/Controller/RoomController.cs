using Common;
using Server.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Controller
{
    class RoomController : BaseController
    {
        public string CreateRoom(string data, Client client, NetworkServer server)
        {
            if (data.Equals("create"))
            {
                Room room = new Room();
                room.AddClient(client);
                server.AddRoom(room);
            }
            return ((int)ReturnCode.Success).ToString();
        }
    }
}
