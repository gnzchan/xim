using backend.DTOs;
using backend.Hubs.Clients;
using Microsoft.AspNetCore.SignalR;

namespace backend.Hubs
{
    public class RoomHub : Hub<IGroupClient>
    {
        public async Task JoinRoom(RoomAttendeeDto roomAttendee)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomAttendee.RoomId);

            await Clients.Group(roomAttendee.RoomId).SendMessage($"{Context.ConnectionId} / {roomAttendee.AppUser.Username} has joined {roomAttendee.RoomId.ToString()}");
        }

        public async Task LeaveRoom(RoomAttendeeDto roomAttendee)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomAttendee.RoomId);

            await Clients.Group(roomAttendee.RoomId).SendMessage($"{roomAttendee.AppUser.Username} has left {roomAttendee.RoomId}");
        }

        public async Task ReceiveGroups(ReceiveGroupsDto receiveGroups)
        {
            await Clients.Group(receiveGroups.RoomId).UpdateGroups(receiveGroups.Groups);
        }
    }
}