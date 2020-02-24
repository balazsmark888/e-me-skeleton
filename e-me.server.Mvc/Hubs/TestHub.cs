using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace e_me.server.Mvc.Hubs
{
    [Authorize]
    public class TestHub : Hub
    {
        public async Task ReceiveMessageFromClient(string message)
        {
            Console.WriteLine(message);
            await Clients.All.SendAsync("ReceiveMessageFromServer", message);
        }
    }
}
