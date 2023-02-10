using AutoMapper;
using AutoMapper.QueryableExtensions;
using backend.DTOs;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class RoomRepository
    {
        private readonly XimDbContext _context;
        private readonly IMapper _mapper;

        public RoomRepository(XimDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ReadRoomDto>> GetRoomsDto()
        {
            var rooms = await _context.Rooms
                .ProjectTo<ReadRoomDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return rooms;
        }

        public async Task<ReadRoomDto> GetRoomDto(Guid id)
        {
            var room = await _context.Rooms
                .ProjectTo<ReadRoomDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(r => r.RoomId == id);

            return room;
        }

        public async Task<Room> GetRoom(Guid id)
        {
            var room = await _context.Rooms
                .Include(r => r.Attendees)
                .ThenInclude(u => u.AppUser)
                .FirstOrDefaultAsync(r => r.RoomId == id);

            return room;
        }

        public async Task<Room> GetRoom(string code)
        {
            var room = await _context.Rooms
                .Include(r => r.Attendees)
                .ThenInclude(u => u.AppUser)
                .FirstOrDefaultAsync(r => r.RoomCode == code);

            return room;
        }


        public async Task<Room> GetRoomByName(string name)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomName == name);

            return room;
        }

        public async Task CreateRoom(Room room)
        {
            await _context.Rooms.AddAsync(room);
        }

        public async Task DeleteRoom(Guid id)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomId == id);

            _context.Rooms.Remove(room);
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}