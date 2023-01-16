using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using logic.Service.Inreface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace logic.Service;

public class JWTService:IJWTService
{
    private readonly IConfiguration appConfig;

    public JWTService(IConfiguration _appConfig)
    {
        this.appConfig = _appConfig;
    }
    public JwtSecurityToken GenerateJWT(int id, string role)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Role, role),
            new Claim(ClaimTypes.Actor, id.ToString())
        };
       
        var jwt = new JwtSecurityToken(
            issuer: appConfig["ISSUER"],
            audience: appConfig["AUDIENCE"],
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(60)), // время действия 2 минуты
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appConfig["KEY"])),SecurityAlgorithms.HmacSha256));
        
        return jwt;
    }
}