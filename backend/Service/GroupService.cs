using AutoMapper;
using backend.Data;
using backend.DTOs;
using backend.Hubs;
using backend.Models;
using Microsoft.AspNetCore.SignalR;

namespace backend.Service
{
    public class GroupService
    {
        private readonly GroupRepository _repository;
        private readonly IHubContext<RoomHub> _hubContext;
        private readonly IMapper _mapper;

        public GroupService(GroupRepository repository, IHubContext<RoomHub> hubContext, IMapper mapper)
        {
            _repository = repository;
            _hubContext = hubContext;
            _mapper = mapper;
        }

        public async Task<List<GroupDto>> GetGroups(Guid id)
        {
            var groups = await _repository.GetGroupsDto(id);

            return groups;
        }

        public async Task<List<GroupDto>> CreateGroups(Room room, int numberOfGroups)
        {
            Random random = new Random();
            var groups = Enumerable.Range(0, numberOfGroups).Select(i => new Group() { GroupId = Guid.NewGuid(), RoomId = room.RoomId, Room = room }).ToList();

            var numberOfMembersPerGroup = (int)Math.Ceiling((double)room.Attendees.Count / numberOfGroups);

            foreach (var attendee in room.Attendees)
            {
                Group randomGroup;
                do
                {
                    randomGroup = groups[random.Next(groups.Count)];
                } while (randomGroup.Members.Count >= numberOfMembersPerGroup);

                var groupAttendee = new GroupAttendee
                {
                    AppUserId = attendee.AppUserId,
                    GroupId = randomGroup.GroupId,
                };
                randomGroup.Members.Add(groupAttendee);
            }

            var outdatedGroups = await _repository.GetGroupsDto(room.RoomId);
            if (outdatedGroups.Any())
            {
                await _repository.DeleteGroups(room.RoomId);
            }

            await _repository.CreateGroups(groups);

            return _mapper.Map<List<GroupDto>>(groups);
        }

        public async Task<bool> SaveChanges()
        {
            return await _repository.SaveChanges();
        }
    }
}