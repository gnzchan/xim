using Microsoft.AspNetCore.Identity;

namespace backend.DTOs
{
    public class ReadRoomDto
    {
        public Guid RoomId { get; set; }
        public string RoomName { get; set; }
        public string RoomCode { get; set; }
        public int Capacity { get; set; }
        public List<IdentityUser> Users { get; set; }
    }
}