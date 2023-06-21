using backend.Hubs.Clients;
using backend.Models;
using Microsoft.AspNetCore.SignalR;

namespace backend.Hubs
{
    public class RoomHub : Hub<IGroupClient>
    {
        public async Task SendGroup(Group group)
        {
            await Clients.All.ReceiveGroup(group);
        }
    }
}