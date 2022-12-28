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

    public ResponceDto<IEnumerable<UserDto>> GetUsers()
    {
        var users = _userServer.GetUsers().Select(a => new UserDto()
        {
            Name = a.Name,
            Surname = a.Surname,
            Patronymic = a.Patronymic,
            Email = a.Email,
            role = a.Role
        }).ToList();
        return new(users);
    }

    [Route("/api/v1/users/{id}")]
    public ResponceDto<UserDto> GetUser(int id)
    {
        var user = _userServer.GetUser(id);
        UserDto userD = new UserDto()
        {
            Name = user.Name,
            Surname = user.Surname,
            Patronymic = user.Patronymic,
            Email = user.Email,
            role = user.Role
        };
        return new(userD);
    }
}