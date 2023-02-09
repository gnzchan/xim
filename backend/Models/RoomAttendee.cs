namespace backend.Models
{
    public class RoomAttendee
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid RoomId { get; set; }
        public Room Room { get; set; }
    }
}