using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace NepPure.Bilibili.Managers
{
    public static class WebsocketServer
    {
        private static WebSocketServer _server;

        public static void Start(int port)
        {
            if (_server != null)
            {
                _server.Stop();
            }

            _server = new WebSocketServer(string.Format("ws://localhost:{0}", port));
            _server.AddWebSocketService<WebsocketBehavior>("/live");
            _server.Start();
        }

        public static void Broadcast(string json)
        {
            if (_server == null)
            {
                return;
            }

            _server.WebSocketServices.Broadcast(json);
        }

        public static void Stop()
        {
            if (_server != null)
            {
                _server.Stop();
            }
            _server = null;
        }
    }
}
