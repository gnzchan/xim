namespace backend.DTOs
{
    public class GroupDto
    {
        public Guid GroupId { get; set; }
        public List<UserDto> Members { get; set; } = new List<UserDto>();
    }
}