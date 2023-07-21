using Microsoft.AspNetCore.Identity;

namespace backend.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<RoomAttendee> Rooms { get; set; }
        public ICollection<GroupAttendee> Groups { get; set; }
    }
}