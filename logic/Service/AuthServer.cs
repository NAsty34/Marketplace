using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using data.model;
using data.Repository;
using MailKit.Net.Smtp;
using Microsoft.IdentityModel.Tokens;
using MimeKit;

namespace logic.Service;

public class AuthServer:IAuthService
{
    private readonly IRepositoryUser userRepository;

    public AuthServer(IRepositoryUser irepositoryuser)
    {
        this.userRepository = irepositoryuser;
    }
    
    public void Register(string email, Role role, string name, string surname, string lastname)
    {
        if (!role.Equals(Role.Buyer) && !role.Equals(Role.Seller))
            throw new ArgumentException("Only Buyer and Seller can be register");
        if (userRepository.GetUser(email)!=null)
            throw new ArgumentException("Email is already in use");
        User user = new User();
        user.Password = GeneratePassword();
        user.Email = email;
        user.Name = name;
        user.Surname = surname;
        user.Patronymic = lastname;
        user.Role = role;
        user.DataRegistration = DateTime.Now;
        userRepository.Create(user);
        userRepository.Save();
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("Admin sait", "nasty_mihailova16@mail.ru"));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = "Пароль для входа";
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = user.Password
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
        //throw new SystemException("Role is null, fuck u " + nameof(e.Role) + " " + e.Role.ToString());
        if (e == null)
        {
            throw new SystemException("User not found");
        }

        if (!e.Password.Equals(password))
        {
            throw new SystemException("Password incorrect");
        }
        var claims = new List<Claim> {new Claim(ClaimsIdentity.DefaultNameClaimType, email), new Claim(ClaimsIdentity.DefaultRoleClaimType, e.Role.ToString()) };
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "token", ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);
        
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claimsIdentity.Claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(60)), // время действия 2 минуты
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),SecurityAlgorithms.HmacSha256));
        
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
}