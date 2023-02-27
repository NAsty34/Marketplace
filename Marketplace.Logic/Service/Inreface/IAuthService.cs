using System.IdentityModel.Tokens.Jwt;
using data.model;
using Microsoft.Extensions.Options;

namespace logic.Service.Inreface;

public interface IAuthService
{
    Task Register(UserEntity userEntity);
    Task<JwtSecurityToken> Login(string email, string password);
    string GeneratePassword();
    Task EmailVerify(string email, string code);
}