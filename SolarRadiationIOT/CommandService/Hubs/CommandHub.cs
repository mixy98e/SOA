using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace CommandService.Hubs
{
    public class CommandHub : Hub
    {
        public async Task InitCommunication()
        {
            await Clients.All.SendAsync("ReceiveMessage"); // Specifies the name of the method
        }

        public async Task SendData(string msg)
        {
            await Clients.All.SendAsync("SendMessage", msg);
        }

        public async Task SendMsg(string msg)
        {
            await Clients.All.SendAsync("RecvMsg", msg);
        }
    }
}
