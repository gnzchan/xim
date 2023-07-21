using backend.DTOs;

namespace backend.Models
{
    public class Group
    {
        // public string Name { get; set; }
        public Guid GroupId { get; set; }

        public Guid RoomId { get; set; }
        public Room Room { get; set; }

        public ICollection<GroupAttendee> Members { get; set; } = new List<GroupAttendee>();
    }
}