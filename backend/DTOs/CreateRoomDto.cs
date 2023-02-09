using System.ComponentModel.DataAnnotations;
using backend.Models;
using Microsoft.AspNetCore.Identity;

namespace backend.DTOs
{
    public class CreateRoomDto
    {
        [Required]
        public AppUser Creator { get; set; }

        [Required]
        public string RoomName { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public List<AppUser> Users { get; set; }
    }
}