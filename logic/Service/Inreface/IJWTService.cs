using System.IdentityModel.Tokens.Jwt;

namespace logic.Service.Inreface;

public interface IJwtService
{ 
    JwtSecurityToken GenerateJwt(Guid id, string role);
}