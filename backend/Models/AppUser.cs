using Microsoft.AspNetCore.Identity;

namespace backend.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<RoomAttendee> Rooms { get; set; }
    }
}