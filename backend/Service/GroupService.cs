using AutoMapper;
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
                groups.ElementAt(groupIndex).Members.Add(attendee.AppUser.UserName);

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