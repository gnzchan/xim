using Microsoft.AspNetCore.Identity;

namespace backend.Models
{
    public class Room
    {
        public Guid RoomId { get; set; }
        public string RoomName { get; set; }
        public string RoomCode { get; set; }
        public int Capacity { get; set; }
        public List<IdentityUser> Users { get; set; }
    }
}