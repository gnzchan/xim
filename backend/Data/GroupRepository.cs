using AutoMapper;
using AutoMapper.QueryableExtensions;
using backend.DTOs;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class GroupRepository
    {
        private readonly XimDbContext _context;
        private readonly IMapper _mapper;

        public GroupRepository(XimDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<GroupDto>> GetGroupsDto(Guid roomId)
        {
            var groups = await _context.Groups.Where(g => g.RoomId == roomId)
                .ProjectTo<GroupDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return groups;
        }

        public async Task CreateGroups(List<Group> groups)
        {
            await _context.Groups.AddRangeAsync(groups);
        }

        public async Task DeleteGroups(Guid roomId)
        {
            var outdatedGroups = await _context.Groups.Where(g => g.RoomId == roomId).ToListAsync();

            _context.Groups.RemoveRange(outdatedGroups);
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}