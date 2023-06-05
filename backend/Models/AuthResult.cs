using backend.DTOs;

namespace backend.Models
{
    public class AuthResult
    {
        public string Token { get; set; }
        public UserDto User { get; set; }
        public bool ResultIsSuccess { get; set; }
        public List<string> Errors { get; set; }
    }
}