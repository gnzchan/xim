using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace backend.DTOs
{
    public class CreateRoomDto
    {
        [Required]
        public string RoomName { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public List<IdentityUser> Users { get; set; }
    }
}