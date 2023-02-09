namespace backend.Models
{
    public class Room
    {
        public Guid RoomId { get; set; }
        public string RoomName { get; set; }
        public string RoomCode { get; set; }
        public int Capacity { get; set; }
        public ICollection<RoomAttendee> Attendees { get; set; }
    }
}