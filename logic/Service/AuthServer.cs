using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.RegularExpressions;
using data.model;
using data.Repository;
using logic.Exceptions;
using logic.Service.Inreface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace logic.Service;

public class AuthServer:IAuthService
{
    private readonly IRepositoryUser _userRepository;
    private readonly ISendEmailService _sendEmailService;
    private readonly IJwtService _jwtService;
    private ILogger<User> _logger;
    private readonly IConfiguration _configuration;
    public AuthServer(IRepositoryUser repositoryUser, IJwtService jwtService, ISendEmailService sendEmailService, ILogger<User> logger, IConfiguration configuration)
    {
        _userRepository = repositoryUser;
        _sendEmailService = sendEmailService;
        _logger = logger;
        _configuration = configuration;
        _jwtService = jwtService;
    }
    
    public async Task Register(User user)
    {
        var regularOptions = new PasswordOprions();
        _configuration.GetSection(PasswordOprions.Password).Bind(regularOptions);
        
        if (!user.Role.Equals(Role.Buyer) && !user.Role.Equals(Role.Seller))
            throw new RoleException();
        if (string.IsNullOrEmpty(user.Email) )
            throw new EmailException();
        
        /*_logger.Log(LogLevel.Information,"======do========"+new Regex($"{regularOptions.RegexForPassword}"));
        _logger.Log(LogLevel.Information,"======pas========"+user.Password);*/
       
        if (!new Regex(regularOptions.RegexForPassword).IsMatch(user.Password))
            throw new PasswordIncorrectException();
        
        var emailuser = _userRepository.GetUser(user.Email);
        user.Id = new Guid();
        if (emailuser != null)
        {
            if (emailuser.EmailIsVerified) throw new EmailException();
            emailuser.Password = user.Password;
            user = emailuser;
        }

        var code = GeneratePassword();
        
        user.EmailCode = BCrypt.Net.BCrypt.HashPassword(code);
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        if (emailuser == null) await _userRepository.Create(user);
        
        await _userRepository.Save();
        if (user.Email != null) await _sendEmailService.Send(user.Email, "Код для подтверждения: " + code, "Admin sait");
    }

    public async Task<JwtSecurityToken> Login(string email, string password)
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
        var jwt = _jwtService.GenerateJwt(user.Id, user.Role.ToString());
        
        return jwt;
    }

    public string GeneratePassword()
    {
        var regularOptions = new PasswordOprions();
        _configuration.GetSection(PasswordOprions.Password).Bind(regularOptions);
        /*const string chars = ;*/
        StringBuilder sb = new StringBuilder();
        Random rnd = new Random();
        for (int i = 0; i < 10; i++)
        {
            int index = rnd.Next(regularOptions.CharsForPassword.Length);
            sb.Append(regularOptions.CharsForPassword[index]);
            
        }
        
        return sb.ToString();
    }

    public async Task EmailVerify(string email, string code)
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
        await _userRepository.Save();
    }
}