using System.IdentityModel.Tokens.Jwt;
using data.model;
using Microsoft.Extensions.Options;

namespace logic.Service.Inreface;

public interface IJwtService
{ 
    JwtSecurityToken GenerateJwt(Guid id, string role);
}