using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using microStore.Services.AuthApi.Models;
using microStore.Services.AuthApi.Service.IService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace microStore.Services.AuthApi.Service
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly JwtOptions _jwtOptions;
        public JwtGenerator(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }
        public string GenerateJwt(ApplicationUser applicationUser, IList<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtOptions.Secret);
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, applicationUser.Id),
                new (JwtRegisteredClaimNames.Email, applicationUser.Email),
                new (JwtRegisteredClaimNames.Name, applicationUser.Name),
                new (ClaimTypes.Role, String.Join(",", roles)),

            };
            //claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtOptions.Audence,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
