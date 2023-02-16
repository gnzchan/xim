using backend.DTOs;
using backend.Models;
using backend.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenService _tokenService;

        public AuthController(UserManager<AppUser> userManager, TokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByEmailAsync(requestDto.Email);

            if (user != null)
            {
                return BadRequest(new AuthResult
                {
                    ResultIsSuccess = false,
                    Errors = new List<string>
                    {
                        "Email already exists"
                    }
                });
            }

            var newUser = new AppUser
            {
                FirstName = requestDto.FirstName,
                LastName = requestDto.LastName,
                Email = requestDto.Email,
                UserName = requestDto.UserName,
            };

            var isCreated = await _userManager.CreateAsync(newUser, requestDto.Password);

            if (!isCreated.Succeeded)
            {
                return BadRequest(new AuthResult
                {
                    ResultIsSuccess = false,
                    Errors = new List<string>
                    {
                        "Server Error"
                    }
                });
            }

            var token = _tokenService.GenerateJwtToken(newUser);

            return Ok(new AuthResult
            {
                Token = token,
                ResultIsSuccess = true
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByEmailAsync(requestDto.Email);

            if (user == null)
            {
                return Unauthorized(new AuthResult
                {
                    ResultIsSuccess = false,
                    Errors = new List<string>
                    {
                        "Invalid login credentials"
                    }
                });
            }

            var isLoginCredentialsValid = await _userManager.CheckPasswordAsync(user, requestDto.Password);

            if (!isLoginCredentialsValid)
            {
                return Unauthorized(new AuthResult
                {
                    ResultIsSuccess = false,
                    Errors = new List<string>
                    {
                        "Invalid login credentials"
                    }
                });
            }

            var token = _tokenService.GenerateJwtToken(user);

            return Ok(new AuthResult
            {
                Token = token,
                ResultIsSuccess = true,
            });
        }
    }
}