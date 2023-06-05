using AutoMapper;
using backend.DTOs;
using backend.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace backend.Service
{
    public class GroupService
    {
        private readonly IMapper _mapper;

        public GroupService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<Group> GetGroups(Room room, int numberOfGroups)
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

            return groups;
        }
    }
}