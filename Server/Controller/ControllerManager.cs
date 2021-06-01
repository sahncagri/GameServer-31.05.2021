using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common;
using Server.Servers;


namespace Server.Controller
{
    class ControllerManager
    {
        private NetworkServer server;
        private Dictionary<RequestCode, BaseController> controllerDictionary = new Dictionary<RequestCode, BaseController>();

        public ControllerManager(NetworkServer server)
        {
            this.server = server;
            Init();
        }

        private void Init()
        {
            controllerDictionary.Add(RequestCode.User, new UserController());
            controllerDictionary.Add(RequestCode.Room, new RoomController());
            controllerDictionary.Add(RequestCode.Game, new GameController());
        }

        public void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data, Client client)
        {
            Console.WriteLine("Handleing Request");
            BaseController controller;
            bool isGet = controllerDictionary.TryGetValue(requestCode, out controller);
            if (!isGet)
            {
                Console.WriteLine("Can't found controller for: " + requestCode);
                return;
            }

            string methodName = Enum.GetName(typeof(ActionCode), actionCode);
            MethodInfo mi = controller.GetType().GetMethod(methodName);
            if (mi == null)
            {
                Console.WriteLine("Can't found method for: " + actionCode);
                return;
            }

            object[] parameters = new object[] { data, client, server };
            var o = mi.Invoke(controller, parameters);
            if (o == null) return;

            server.SendResponse(client,actionCode,o as string);

        }


    }
}
