using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.RegularExpressions;
using data.model;
using data.Repository;
using logic.Exceptions;
using logic.Service.Inreface;

namespace logic.Service;

public class AuthServer:IAuthService
{
    private readonly IRepositoryUser _userRepository;
    private readonly ISendEmailService _sendEmailService;
    private readonly IJWTService _jwtService;
    public AuthServer(IRepositoryUser repositoryUser, IJWTService jwtService, ISendEmailService sendEmailService)
    {
        this._userRepository = repositoryUser;
        this._sendEmailService = sendEmailService;
        this._jwtService = jwtService;
    }
    
    public void Register(User user)
    {
        if (!user.Role.Equals(Role.Buyer) && !user.Role.Equals(Role.Seller))
            throw new RoleException();
        if (string.IsNullOrEmpty(user.Email) )
            throw new EmailException();
        if (!new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$").IsMatch(user.Password))
            throw new PasswordIncorrectException();
        var Emailuser = _userRepository.GetUser(user.Email);
        if (Emailuser != null)
        {
            if (Emailuser.EmailIsVerified) throw new EmailException();
            Emailuser.Password = user.Password;
            user = Emailuser;
        }

        var code = GeneratePassword();
        user.EmailCode = BCrypt.Net.BCrypt.HashPassword(code);
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        if (user.Id.Equals(Guid.Empty))
        {
            user.Id = new Guid();
            _userRepository.Create(user);
        }
        _userRepository.Save();
        _sendEmailService.Send(user.Email, "Код для подтверждения: "+code, "Admin sait");
        
    }

    public JwtSecurityToken Login(string email, string password)
    {
        var user = _userRepository.GetUser(email);
        
        if (user == null)
        {
            throw new UserNotFoundException();
        }

        if (!user.EmailIsVerified)
        {
            throw new EmailIsVerifiedException();
        }

        if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            throw new PasswordIncorrectException();
        }
        var jwt = _jwtService.GenerateJWT(user.Id, user.Role.ToString());
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
        var user = _userRepository.GetUser(email);
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
        _userRepository.Save();
    }
}