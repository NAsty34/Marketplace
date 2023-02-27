using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.RegularExpressions;
using data.model;
using data.Repository.Interface;
using logic.Exceptions;
using logic.Service.Inreface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace logic.Service;

public class AuthServer:IAuthService
{
    private readonly IRepositoryUser _userRepository;
    private readonly ISendEmailService _sendEmailService;
    private readonly IJwtService _jwtService;
    //private ILogger<User> _logger;
    private PasswordOprions _options;
    public AuthServer(IRepositoryUser repositoryUser, IJwtService jwtService, ISendEmailService sendEmailService, IOptions<PasswordOprions> options)
    {
        _userRepository = repositoryUser;
        _sendEmailService = sendEmailService;
        _options = options.Value;
        // _logger = logger;
        _jwtService = jwtService;
    }
    
    public async Task Register(UserEntity userEntity)
    {
        if (!userEntity.RoleEntity.Equals(RoleEntity.Buyer) && !userEntity.RoleEntity.Equals(RoleEntity.Seller))
            throw new RoleException();
        if (string.IsNullOrEmpty(userEntity.Email) )
            throw new EmailException();
        
        /*_logger.Log(LogLevel.Information,"======do========"+new Regex($"{regularOptions.RegexForPassword}"));
        _logger.Log(LogLevel.Information,"======pas========"+user.Password);*/
       
        if (!new Regex(_options.RegexForPassword).IsMatch(userEntity.Password))
            throw new PasswordIncorrectException();
        
        var emailuser = _userRepository.GetUser(userEntity.Email);
        userEntity.Id = new Guid();
        if (emailuser != null)
        {
            if (emailuser.EmailIsVerified) throw new EmailException();
            emailuser.Password = userEntity.Password;
            userEntity = emailuser;
        }

        var code = GeneratePassword();
        
        userEntity.EmailCode = BCrypt.Net.BCrypt.HashPassword(code);
        userEntity.Password = BCrypt.Net.BCrypt.HashPassword(userEntity.Password);
        if (emailuser == null) await _userRepository.Create(userEntity);
        
        await _userRepository.Save();
        if (userEntity.Email != null) await _sendEmailService.Send(userEntity.Email, "Код для подтверждения: " + code, "Admin sait");
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
        var jwt = _jwtService.GenerateJwt(user.Id, user.RoleEntity.ToString());
        
        return jwt;
    }

    public string GeneratePassword()
    {
        StringBuilder sb = new StringBuilder();
        Random rnd = new Random();
        for (int i = 0; i < 10; i++)
        {
            int index = rnd.Next(_options.CharsForPassword.Length);
            sb.Append(_options.CharsForPassword[index]);
            
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