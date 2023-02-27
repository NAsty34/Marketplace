using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using data.model;
using logic.Service.Inreface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace logic.Service;

public class JwtService:IJwtService
{
    private  JwtTokenOptions _options;
    //private ILogger<UserEntity> _logger;

    public JwtService(IOptions<JwtTokenOptions> options)
    {
        _options = options.Value;
        //_logger = logger;
        //_logger.Log(LogLevel.Information, "===" + _options.Issuer+"===" + _options.Audience + "===" + _options.Key + "========" + _options.Time);
    }
    public JwtSecurityToken GenerateJwt(Guid id, string role)
    {
        //_logger.Log(LogLevel.Information, "===" + jwtoptin.Issuer+"===" + jwtoptin.Audience + "===" + jwtoptin.Key + "========" + jwtoptin.Time);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Role, role),
            new Claim(ClaimTypes.Actor, id.ToString())
        };
       
        var jwt = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(_options.Time)), // время действия
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key)),SecurityAlgorithms.HmacSha256));
        
        return jwt;
    }
}

