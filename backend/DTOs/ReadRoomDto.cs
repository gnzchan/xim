using backend.Models;
using Microsoft.AspNetCore.Identity;

namespace backend.DTOs
{
    public class ReadRoomDto
    {
        public Guid RoomId { get; set; }
        public AppUser Creator { get; set; }
        public string RoomName { get; set; }
        public string RoomCode { get; set; }
        public int Capacity { get; set; }
        public List<AppUser> Users { get; set; }
    }
}