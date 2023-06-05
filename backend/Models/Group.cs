using backend.DTOs;

namespace backend.Models
{
    public class Group
    {
        // public string Name { get; set; }
        public int Id { get; set; }
        public List<UserDto> Members { get; set; } = new List<UserDto>();
    }
}