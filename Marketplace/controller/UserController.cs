using data.model;
using logic.Service;
using Microsoft.AspNetCore.Mvc;
using Marketplace.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Marketplace.controller;
[Authorize(Roles = nameof(Role.Admin))]
public class UserController:Controller
{
    private readonly IUserServer _userServer;

    public UserController(IUserServer userServer)
    {
        this._userServer = userServer;
    }

    [Route("/api/v1/users")]

    public ResponceDto<Page<UserDto>> GetUsers()
    {
        var users = _userServer.GetUsers();
        Page<UserDto> Up = Page<UserDto>.Create(users, users.Items.Select(a => new UserDto(a)));
        
        return new(Up);
    }

    [Route("/api/v1/users/{id}")]
    public ResponceDto<UserDto> GetUser(int id)
    {
        var user = _userServer.GetUser(id);
        UserDto userD = new UserDto(user);
        return new(userD);
    }
}