using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace logic;

public class AuthOptions
{
    public const string ISSUER = "Chief"; // издатель токена
    public const string AUDIENCE = "AuthClient"; // потребитель токена
    const string KEY = "veryveryveryveryveryveryveryveryveryveryveryveryverysecretkey!123";   // ключ для шифрации
    public static SymmetricSecurityKey GetSymmetricSecurityKey() => 
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}