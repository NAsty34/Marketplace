using System.IdentityModel.Tokens.Jwt;
using logic.Service;
using Microsoft.AspNetCore.Mvc;
using Marketplace.DTO;
using data.model;
using Microsoft.AspNetCore.Authorization;

namespace Marketplace.controller;

public class AuthController:Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        this._authService = authService;
    }
    
    [Route("/api/v1/auth/login")]
    [HttpPost]
    public ResponceDto<TokenDTO> Login([FromBody]LoginDTO loginDto)
    {
        var a = _authService.Login(loginDto.Email, loginDto.Password);
        TokenDTO tokenDto = new TokenDTO();
        tokenDto.token = new JwtSecurityTokenHandler().WriteToken(a);
        tokenDto.exp = DateTime.UtcNow.Add(TimeSpan.FromMinutes(60));
        return new ResponceDto<TokenDTO>(tokenDto);
    }

    [Route("/api/v1/auth/register")]
    [HttpPost]
    public ResponceDto<string> Register([FromBody]UserDto userDto)
    {
        //UserDto st = await Request.ReadFromJsonAsync<UserDto>();
        //return new ResponceDto<UserDto>(userDto);
        _authService.Register(userDto.Email, userDto.role, userDto.Name, userDto.Surname, userDto.Patronymic);
        return new("Успешная регистрация, Вам на почту отправлен пароль");
    }
}