using System.Security.Claims;
using data.model;
using logic.Exceptions;
using logic.Service;
using Microsoft.AspNetCore.Mvc;
using Marketplace.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Marketplace.controller;
[Authorize(Roles = nameof(Role.Admin))]
public class UserController:UserBaseController
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
        Page<UserDto> pageuser = Page<UserDto>.Create(users, users.Items.Select(a => new UserDto(a)));
        
        return new(pageuser);
    }

    [Route("/api/v1/users/{id}")]
    public ResponceDto<UserDto> GetUser(Guid id)
    {
        var user = _userServer.GetUser(id);
        UserDto userD = new UserDto(user);
        return new(userD);
    }
    
    [Route("/api/v1/user/block/{id}")]
    public ResponceDto<UserDto> BlockUser(Guid id)
    {
        if (!userrole.Equals(Role.Admin))
        {
            throw new AccessDeniedException();
        }
        var blockuser = _userServer.ChangeBlockUser(id, false);
        return new(new UserDto(blockuser));
    }

    [Route("/api/v1/user/unblock/{id}")]
    public ResponceDto<UserDto> UnblockUser(Guid id)
    {
        if (!userrole.Equals(Role.Admin))
        {
            throw new AccessDeniedException();
        }
        var unblockuser = _userServer.ChangeBlockUser(id, true);
        return new(new UserDto(unblockuser));
    }

    [Route("/api/v1/user/admin")]
    [HttpPost]
    public ResponceDto<UserDto> CreateAdmin([FromBody] RegisterDTO userDto)
    {
        if (!userrole.Equals(Role.Admin))
        {
            //throw new AccessDeniedException();
        }
        var adminuser = new User()
        {
            Name = userDto.Name,
            Surname = userDto.Surname,
            Patronymic = userDto.Surname,
            Email = userDto.Email,
            Role = userDto.role,
            EmailIsVerified = true,
            CreateDate = DateTime.Now,
            CreatorId = userDto.Id,
            Password = userDto.Password,
            IsActive = true,
            IsDeleted = false
        };
        var newAdmin =  _userServer.CreateAdmin(adminuser);
        return new(new UserDto(newAdmin));
    }
}