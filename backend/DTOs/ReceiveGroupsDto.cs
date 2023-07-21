using backend.Models;

namespace backend.DTOs
{
    public class ReceiveGroupsDto
    {
        public string RoomId { get; set; }
        public List<GroupDto> Groups { get; set; }
    }
}