using System.ComponentModel.DataAnnotations;
using backend.Models;

namespace backend.DTOs
{
    public class CreateRoomDto
    {
        [Required]
        public string RoomName { get; set; }

        [Required]
        public int Capacity { get; set; }
    }
}