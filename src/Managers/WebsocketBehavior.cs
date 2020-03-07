using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace NepPure.Bilibili.Managers
{
    public class WebsocketBehavior : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs args)
        {
            App.MainWin.MainVm.UpdateSearchAsync().Wait();
        }
    }
}
