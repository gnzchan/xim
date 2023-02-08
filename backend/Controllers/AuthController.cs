using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.DTOs;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
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

            var newUser = new IdentityUser
            {
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

            var token = GenerateJwtToken(newUser);

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
                return BadRequest(new AuthResult
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
                return BadRequest(new AuthResult
                {
                    ResultIsSuccess = false,
                    Errors = new List<string>
                    {
                        "Invalid login credentials"
                    }
                });
            }

            var token = GenerateJwtToken(user);

            return Ok(new AuthResult
            {
                Token = token,
                ResultIsSuccess = true,
            });
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_configuration["JwtConfig:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
                }),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}