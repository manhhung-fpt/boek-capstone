using Boek.Core.Constants;
using Boek.Core.Enums;
using Boek.Core.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Boek.Service.Commons
{
    public class AccessTokenManager
    {
        public static string GenerateJwtToken(string name, string[] roles, Guid? userId,
            IConfiguration configuration)
        {
            var tokenConfig = configuration.GetSection(MessageConstants.AUTH_CONFIG_TOKEN);
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenConfig[MessageConstants.AUTH_CONFIG_SECRET_KEY]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var permClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            };

            if (roles != null && roles.Length > 0)
            {
                foreach (string role in roles)
                {
                    permClaims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(permClaims),
                Expires = DateTime.Now.AddDays(7.0),
                SigningCredentials = credentials
            };
            var token = jwtSecurityTokenHandler.CreateToken(tokenDescription);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public static string GenerateGuestJwtToken(string name, string email, IConfiguration configuration)
        {
            var tokenConfig = configuration.GetSection(MessageConstants.AUTH_CONFIG_TOKEN);
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenConfig[MessageConstants.AUTH_CONFIG_SECRET_KEY]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var role = StatusExtension<BoekRole>.GetStatus((byte)BoekRole.Guest);

            var permClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(permClaims),
                Expires = DateTime.Now.AddDays(1.0),
                SigningCredentials = credentials
            };
            var token = jwtSecurityTokenHandler.CreateToken(tokenDescription);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
