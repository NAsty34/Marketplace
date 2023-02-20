using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using data.model;
using logic.Service.Inreface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace logic.Service;

public class JwtService:IJwtService
{
    private readonly IConfiguration _appConfig;
    private ILogger<User> _logger;

    public JwtService(IConfiguration appConfig, ILogger<User> logger)
    {
        _appConfig = appConfig;
        _logger = logger;
    }
    public JwtSecurityToken GenerateJwt(Guid id, string role)
    {
        var jwtTokenOptions = new JWTTokenOptions();
        _appConfig.GetSection(JWTTokenOptions.JWTToken).Bind(jwtTokenOptions);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Role, role),
            new Claim(ClaimTypes.Actor, id.ToString())
        };
       
        var jwt = new JwtSecurityToken(
            issuer: jwtTokenOptions.ISSUER,
            audience: jwtTokenOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(60)), // время действия
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenOptions.KEY)),SecurityAlgorithms.HmacSha256));
        //_logger.Log(LogLevel.Information, "===" + jwtTokenOptions.ISSUER+"===" + jwtTokenOptions.AUDIENCE + "===" + jwtTokenOptions.KEY);
        return jwt;
    }
}