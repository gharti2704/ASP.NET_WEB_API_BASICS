using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;


namespace basic.Helpers;

public class AuthHelper
{
    public string CreateToken(int userId)
    {
        var claims = new Claim[]
        {
            new Claim("userId", userId.ToString())
        };

        var tokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_TOKEN_KEY")!));
        var credentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha512Signature);
        var descriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = credentials,
            Expires = DateTime.Now.AddDays(1)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken token = tokenHandler.CreateToken(descriptor);

        return tokenHandler.WriteToken(token);
    }
}