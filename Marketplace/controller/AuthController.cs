using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Marketplace.DTO;
using data.model;
using logic.Service.Inreface;

namespace Marketplace.controller;

public class AuthController: Controller
{
    private readonly IAuthService _authService;
    //private ILogger<User> _logger;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
        //_logger = logger;
    }
    
    [Route("/api/v1/auth/login")]
    [HttpPost]
    public async Task<ResponceDto<TokenDto>> Login([FromBody]LoginDto loginDto)
    {
        var a = await _authService.Login(loginDto.Email, loginDto.Password);
        TokenDto tokenDto = new TokenDto();
        tokenDto.Token = new JwtSecurityTokenHandler().WriteToken(a);
        tokenDto.Exp = DateTime.UtcNow.Add(TimeSpan.FromMinutes(60));
        return new ResponceDto<TokenDto>(tokenDto);
    }

    [Route("/api/v1/auth/register")]
    [HttpPost]
    public async Task<ResponceDto<string>> Register([FromBody]RegisterDto register)
    {
        
        User user = new User();
        user.Password = register.Password;
        user.Email = register.Email;
        user.Name = register.Name;
        user.Surname = register.Surname;
        user.Patronymic = register.Patronymic;
        user.Role = register.Role;
        user.CreateDate = DateTime.Now;
        await _authService.Register(user);
        return new("Вам на почту отправлен код");
    }
    [Route("/api/v1/auth/verify")]
    [HttpPost]
    public async Task<ResponceDto<string>> Verify([FromBody]LoginDto loginDto)
    {
         await _authService.EmailVerify(loginDto.Email, loginDto.Password);
         return new ResponceDto<string>("Почта подтверждена");
    }

    
}