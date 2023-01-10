using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using data.model;
using data.Repository;
using logic.Exceptions;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;

namespace logic.Service;

public class AuthServer:IAuthService
{
    private readonly IRepositoryUser userRepository;
    private readonly IConfiguration appConfig;
    
    public AuthServer(IRepositoryUser irepositoryuser, IConfiguration _appConfig)
    {
        this.userRepository = irepositoryuser;
        this.appConfig = _appConfig;
    }
    
    public void Register(User user)
    {
        if (!user.Role.Equals(Role.Buyer) && !user.Role.Equals(Role.Seller))
            throw new RoleException();
        if (userRepository.GetUser(user.Email)!=null)
            throw new EmailException();
        var code = GeneratePassword();
        user.EmailCode = BCrypt.Net.BCrypt.HashPassword(code);
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        userRepository.Create(user);
        userRepository.Save();
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("Admin sait", "nasty_mihailova16@mail.ru"));
        emailMessage.To.Add(new MailboxAddress("", user.Email));
        emailMessage.Subject = "Код для входа";
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = code
        };
        using (var client = new SmtpClient())
        {
            client.Connect("smtp.mail.ru", 465, true);
            client.Authenticate("nasty_mihailova16@mail.ru", "2zxWndJAV2FEsLh9z0ms");
            client.Send(emailMessage);
            client.Disconnect(true);
        }
    }

    public JwtSecurityToken Login(string email, string password)
    {
        var e = userRepository.GetUser(email);
        
        if (e == null)
        {
            throw new UserNotFoundException();
        }

        if (!e.EmailIsVerified)
        {
            throw new EmailIsVerifiedException();
        }

        if (!BCrypt.Net.BCrypt.Verify(password, e.Password))
        {
            throw new PasswordIncorrectException();
        }
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, email), 
            new Claim(ClaimTypes.Role, e.Role.ToString()),
            new Claim(ClaimTypes.Actor, e.Id.ToString())
        };
        // ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "token", ClaimsIdentity.DefaultNameClaimType,
        //     ClaimsIdentity.DefaultRoleClaimType);
        
        var jwt = new JwtSecurityToken(
            issuer: appConfig["ISSUER"],
            audience: appConfig["AUDIENCE"],
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(60)), // время действия 2 минуты
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appConfig["KEY"])),SecurityAlgorithms.HmacSha256));
        
        return jwt;
    }

    public string GeneratePassword()
    {
        const string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()-+_";
        StringBuilder sb = new StringBuilder();
        Random rnd = new Random();
        for (int i = 0; i < 10; i++)
        {
            int index = rnd.Next(chars.Length);
            sb.Append(chars[index]);
        }
        return sb.ToString();
    }

    public void EmailVerify(string email, string code)
    {
        var user = userRepository.GetUser(email);
        if (user == null)
        {
            throw new UserNotFoundException();
        }

        if (user.EmailIsVerified)
        {
            throw new EmailException();
        }
        
        if (!BCrypt.Net.BCrypt.Verify(code, user.EmailCode))
        {
            throw new CodeIncorrectException();
        }

        user.EmailIsVerified = true;
        userRepository.Save();
    }
}