using System.Security.Claims;
using data.model;
using logic.Service.Inreface;
using Marketplace.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.controller;

[Authorize]
public class ProfileController : UserBaseController
{
    private readonly IUserServer _userServer;
    public ProfileController(IUserServer userServer)
    {
        _userServer = userServer;
    }

    [Route("/api/v1/me")]
    [HttpGet]
    public async Task<ResponceDto<UserDto>> GetProfile()
    {
        var user = await _userServer.GetUser(Userid.Value);
        return new (new UserDto(user));
        
    }
    
    [Route("/api/v1/me")]
    [HttpPut]
    public async Task<ResponceDto<UserDto>> EditProfile([FromBody]UserDto userDto)
    {
        var userEntity = new UserEntity()
        {
            Name = userDto.Name,
            Surname = userDto.Surname,
            Patronymic = userDto.Patronymic,
            Id = Userid.Value
        };
        userEntity = await _userServer.EditUser(userEntity);
        return new(new UserDto(userEntity));
    }
    
}

