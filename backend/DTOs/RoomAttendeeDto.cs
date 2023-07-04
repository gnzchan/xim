namespace backend.DTOs
{
    public class RoomAttendeeDto
    {
        public string RoomId { get; set; }
        public UserDto AppUser { get; set; }
    }
}