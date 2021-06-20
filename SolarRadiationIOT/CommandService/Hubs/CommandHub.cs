using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandService.Hubs
{
    public class CommandHub : Hub
    {
        public async Task Notify()
        {
            await Clients.All.SendAsync("ReceivedMsg");
        }

    }
}
