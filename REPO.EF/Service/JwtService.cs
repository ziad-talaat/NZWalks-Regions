using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using REPO.Core.Contract;
using REPO.Core.DTO;
using REPO.Core.Models;
using REPO.EF.Service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Authorization_Refreshtoken.Service
{
    public class JWTService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWTOptions _JWToptions;
        public JWTService(IConfiguration configuration, IOptions<JWTOptions> jwtoption, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _userManager= userManager;
            _JWToptions = jwtoption.Value;
        }
        public async Task<AuthanticatedResponse> CreateJwtToken(ApplicationUser user)
        {
            DateTime expiration = DateTime.UtcNow.AddHours(_JWToptions.expiration);


            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

               new Claim(JwtRegisteredClaimNames.Iat,   new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(),ClaimValueTypes.Integer64),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.UserName),
            };

            var roles =  await _userManager.GetRolesAsync(user);

            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role)); 
            }




            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWToptions.secretKey));
            SigningCredentials signCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken tokenGenerator = new JwtSecurityToken(
               issuer: _configuration["jwt:issuer"],
               audience: _configuration["jwt:audience"],

                claims,
                expires: expiration,
                signingCredentials: signCred
                );

            JwtSecurityTokenHandler tokenHandler = new();
            string token = tokenHandler.WriteToken(tokenGenerator);

            return new AuthanticatedResponse
            {
                Token = token,
                ExpirationDate = expiration,
                Email = user.Email,
                Name = user.UserName,
                RefreshToken = CreateRefreshToken(),
                RF_Expiration = DateTime.UtcNow.AddDays(Convert.ToInt32(_configuration["RefreshToken:ExpirationDate"]))  
            };
        }

        public string CreateRefreshToken()
        {
            byte[] bytes = new byte[64];
            var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }



        public ClaimsPrincipal GetPrinciplesFromJWTToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
               
                ValidAudience = _configuration["jwt:audience"],

                ValidateIssuer = true,
                ValidIssuer = _configuration["jwt:issuer"],

                 ValidateLifetime = false,
                ValidateIssuerSigningKey = true,

                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWToptions.secretKey))

            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            ClaimsPrincipal principles = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validateToken);

            if (validateToken is not JwtSecurityToken jwtsecurityToken || !jwtsecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("InvalidToken");
            }
            return principles;
        }
    }
}





