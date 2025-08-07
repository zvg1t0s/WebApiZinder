using Microsoft.IdentityModel.Tokens;
using RestApiTinderClone.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using RestApiTinderClone.Tools.Interfaces;
namespace RestApiTinderClone.Tools
{
    public class JwtProvider : IJWTProvider
    {
        public string Generate(User user)
        {
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("2131231jodjosaidjasodijsaodjasodiasjdsosja2")),SecurityAlgorithms.HmacSha256);
            Claim[] claims = [new("UserId", user.Id.ToString()), new("Login", user.Login)];
            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddDays(1)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
