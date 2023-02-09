using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Models;
using Microsoft.IdentityModel.Tokens;

namespace backend.Service
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            //

            // var jwtTokenHandler = new JwtSecurityTokenHandler();

            // var key = Encoding.UTF8.GetBytes(_configuration["JwtConfig:Secret"]);

            // var tokenDescriptor = new SecurityTokenDescriptor()
            // {
            //     Subject = new ClaimsIdentity(new[]
            //     {
            //         new Claim(JwtRegisteredClaimNames.NameId, user.Id),
            //         new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            //         new Claim(JwtRegisteredClaimNames.Email, user.Email),
            //         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //         new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
            //     }),
            //     Expires = DateTime.Now.AddHours(1),
            //     SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512)
            // };

            // var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            // var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}