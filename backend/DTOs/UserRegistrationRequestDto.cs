using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class UserRegistrationRequestDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}