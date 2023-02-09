using AutoMapper;
using AutoMapper.QueryableExtensions;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class RoomRepository
    {
        private readonly XimDbContext _context;

        public RoomRepository(XimDbContext context)
        {
            _context = context;
        }

        public async Task<List<Room>> GetRooms()
        {
            var rooms = await _context.Rooms
                .Include(r => r.Attendees)
                .ThenInclude(u => u.AppUser)
                .ToListAsync();

            return rooms;
        }

        public async Task<Room> GetRoomById(Guid id)
        {
            var room = await _context.Rooms
                .Include(r => r.Attendees)
                .ThenInclude(u => u.AppUser)
                .FirstOrDefaultAsync(r => r.RoomId == id);

            return room;
        }

        public async Task<Room> GetRoomByName(string name)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomName == name);

            return room;
        }

        public async void CreateRoom(Room room)
        {
            await _context.Rooms.AddAsync(room);
        }

        public void DeleteRoom(Room room)
        {
            _context.Rooms.Remove(room);
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }
}