using AutoMapper;
using backend.DTOs;
using backend.Hubs;
using backend.Models;
using Microsoft.AspNetCore.SignalR;

namespace backend.Service
{
    public class GroupService
    {
        private readonly IHubContext<RoomHub> _hubContext;
        private readonly IMapper _mapper;

        public GroupService(IHubContext<RoomHub> hubContext, IMapper mapper)
        {
            _hubContext = hubContext;
            _mapper = mapper;
        }

        public async Task<List<Group>> GetGroups(Room room, int numberOfGroups)
        {
            var groups = Enumerable.Range(0, numberOfGroups).Select(i => new Group() { Id = i }).ToList();

            var groupIndex = 0;

            foreach (var attendee in room.Attendees)
            {
                var userDto = _mapper.Map<UserDto>(attendee.AppUser);

                groups.ElementAt(groupIndex).Members.Add(userDto);

                groupIndex++;
                if (groupIndex == numberOfGroups)
                {
                    groupIndex = 0;
                }
            }

            await _hubContext.Clients.Group(room.RoomId.ToString()).SendAsync("ReceiveGroups", groups);

            return groups;
        }
    }
}