using backend.Hubs.Clients;
using backend.Models;
using Microsoft.AspNetCore.SignalR;

namespace backend.Hubs
{
    public class RoomHub : Hub<IGroupClient>
    {
        public async Task JoinRoom(RoomAttendee roomAttendee)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomAttendee.RoomId.ToString());

            await Clients.Group(roomAttendee.RoomId.ToString()).SendMessage($"{roomAttendee.AppUser.UserName} has joined {roomAttendee.RoomId.ToString()}");
        }

        public async Task LeaveRoom(RoomAttendee roomAttendee)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomAttendee.RoomId.ToString());

            await Clients.Group(roomAttendee.RoomId.ToString()).SendMessage($"{roomAttendee.AppUser.UserName} has left {roomAttendee.RoomId.ToString()}");
        }

        public async Task ReceiveGroups(List<Group> groups)
        {
            await Clients.OthersInGroup(Context.ConnectionId).UpdateGroups(groups);
        }
    }
}