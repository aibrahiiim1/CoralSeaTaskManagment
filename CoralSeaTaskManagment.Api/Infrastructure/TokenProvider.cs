using CoralSeaTaskManagment.Api.Models.DTO;
using CoralSeaTaskManagment.Model.Models.Domain;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace CoralSeaTaskManagment.Api.Infrastructure
{
    public class TokenProvider
    {
        private readonly IConfiguration _configuration;
        public TokenProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Token GenerateToken(User user)
        {
            //RefreshTokenHelper
            var accessToken = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();
            return new Token {AccessToken=accessToken,
                RefreshToken=refreshToken};
        }
        private RefreshAccessToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshAccessToken
            {
                Token=Guid.NewGuid().ToString(),
                CreateDate = DateTime.Now,
                Enabled = true,
                Expires = DateTime.Now.AddMonths(1),
                 
            };
            return refreshToken;
        }
        private string GenerateAccessToken(User user)
        {
            var SecretKey = _configuration["Jwt:Key"];
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var Credentials = new SigningCredentials(SecurityKey,SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor 
            {
                Subject = new ClaimsIdentity([
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role) ,
                    new Claim("DepId", user.DepartmentId.ToString()) ,
                    new Claim("HotelId", user.HotelId.ToString()),
                    new Claim("FullName", user.FullName.ToString()),

                    ]),

                Expires = DateTime.Now.AddMinutes(60), 
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"], 
                SigningCredentials = Credentials ,
            };

            return new JsonWebTokenHandler().CreateToken(tokenDescriptor);
        }
    }
    public class Token
    {
        public string AccessToken { get; set; }
        public RefreshAccessToken RefreshToken { get; set; }
    }
}
