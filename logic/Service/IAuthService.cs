using System.IdentityModel.Tokens.Jwt;
using data.model;
namespace logic.Service;

public interface IAuthService
{
    void Register(string email, Role role, string name, string surname, string lastname);
    JwtSecurityToken Login(string email, string password);
    string GeneratePassword();
}