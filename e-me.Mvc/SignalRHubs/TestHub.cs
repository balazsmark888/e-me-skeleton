using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace e_me.Mvc.SignalRHubs
{
    public class TestHub : Hub
    {
        public Task Announce(string message)
        {
            return Clients.All.SendAsync("receiveMessage", message);
        }
    }
}
