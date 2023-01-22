using System.IdentityModel.Tokens.Jwt;

namespace logic.Service.Inreface;

public interface IJWTService
{
    JwtSecurityToken GenerateJWT(Guid id, string role);
}