using System.Security.Claims;
using data.model;
using logic.Service;
using Marketplace.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.controller;

[Authorize]
public class ProfileController : Controller
{
    private readonly IUserServer _UserServer;

    public ProfileController(IUserServer userServer)
    {
        this._UserServer = userServer;
    }

    [Route("/api/v1/me")]
    [HttpGet]
    public ResponceDto<UserDto> GetProfile()
    {
        int usid = int.Parse(User.Claims.First(a => a.Type == ClaimTypes.Actor).Value);
        var user = _UserServer.GetUser(usid);
        return new (new UserDto(user));
    }
    
    [Route("/api/v1/me")]
    [HttpPut]
    public ResponceDto<UserDto> EditProfile([FromBody]UserDto userDto)
    {
        int usid = int.Parse(User.Claims.First(a => a.Type == ClaimTypes.Actor).Value);
        User user = new User()
        {
            Name = userDto.Name,
            Surname = userDto.Surname,
            Patronymic = userDto.Patronymic,
            Id = usid
        };
        user = _UserServer.EditUser(user);
        return new(new UserDto(user));
    }
}