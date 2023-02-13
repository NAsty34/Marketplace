using System.IdentityModel.Tokens.Jwt;
using logic.Service;
using Microsoft.AspNetCore.Mvc;
using Marketplace.DTO;
using data.model;
using Microsoft.AspNetCore.Authorization;

namespace Marketplace.controller;

public class AuthController: Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        this._authService = authService;
    }
    
    [Route("/api/v1/auth/login")]
    [HttpPost]
    public async Task<ResponceDto<TokenDTO>> Login([FromBody]LoginDTO loginDto)
    {
        var a = _authService.Login(loginDto.Email, loginDto.Password);
        TokenDTO tokenDto = new TokenDTO();
        tokenDto.token = new JwtSecurityTokenHandler().WriteToken(a);
        tokenDto.exp = DateTime.UtcNow.Add(TimeSpan.FromMinutes(60));
        return new ResponceDto<TokenDTO>(tokenDto);
    }

    [Route("/api/v1/auth/register")]
    [HttpPost]
    public ResponceDto<string> Register([FromBody]RegisterDTO register)
    {
        
        User user = new User();
        user.Password = register.Password;
        user.Email = register.Email;
        user.Name = register.Name;
        user.Surname = register.Surname;
        user.Patronymic = register.Patronymic;
        user.Role = register.role;
        user.CreateDate = DateTime.Now;
        _authService.Register(user);
        return new("Вам на почту отправлен код");
    }
    [Route("/api/v1/auth/verify")]
    [HttpPost]
    public ResponceDto<string> Verify([FromBody]LoginDTO loginDto)
    {
         _authService.EmailVerify(loginDto.Email, loginDto.Password);
         return new ResponceDto<string>("Почта подтверждена");
    }

    
}