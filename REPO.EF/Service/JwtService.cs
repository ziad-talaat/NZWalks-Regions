using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using REPO.Core.Contract;
using REPO.Core.DTO;
using REPO.Core.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace REPO.EF.Service
{
    public class JwtService : IJwtService
    {
        private readonly JWTOptions _jwtOptions;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        public JwtService(IOptions<JWTOptions>jwt, IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            _jwtOptions = jwt.Value;
            _configuration = config;
            _userManager = userManager;
        }

        public async Task<AuthanticatedResponse> CreateJwtToken(ApplicationUser user)
        {

            DateTime expire = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtOptions.Expiration));

            var roles = await _userManager.GetRolesAsync(user);

            List<Claim>claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Email.ToString()),
                new Claim(ClaimTypes.Name, user.UserName.ToString()),
            };

            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var secret = _configuration.GetSection("jwt")["secretKey"];
            if (string.IsNullOrEmpty(secret))
            {
                throw new InvalidOperationException("JWT SecretKey is not configured.");
            }

            var keyBytes = Encoding.UTF8.GetBytes(secret);
            var securityKey = new SymmetricSecurityKey(keyBytes);

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);





            JwtSecurityToken tokenGenerator = new JwtSecurityToken(
                      issuer: _jwtOptions.Issuer,
                      audience: _jwtOptions.Audience,
                      claims: claims,
                      expires: expire,
                      signingCredentials: signingCredentials
            );   
            
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            string token = handler.WriteToken(tokenGenerator);

            return new AuthanticatedResponse
            {
                Token = token,
                Email = user.Email,
                Name = user.UserName,
                ExpirationDate = expire
            };

        }
    }
}

    
        
               



