using System.IdentityModel.Tokens.Jwt;
using data.model;
namespace logic.Service;

public interface IAuthService
{
    void Register(User user);
    JwtSecurityToken Login(string email, string password);
    string GeneratePassword();
    void EmailVerify(string email, string code);
}