using System.IdentityModel.Tokens.Jwt;
using data.model;

namespace logic.Service.Inreface;

public interface IAuthService
{
    Task Register(User user);
    Task<JwtSecurityToken> Login(string email, string password);
    string GeneratePassword();
    Task EmailVerify(string email, string code);
}