namespace backend.Models
{
    public class GroupAttendee
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid GroupId { get; set; }
        public Group Group { get; set; }
    }
}