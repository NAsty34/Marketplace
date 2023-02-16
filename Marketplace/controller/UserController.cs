using data.model;
using logic.Exceptions;
using logic.Service;
using Microsoft.AspNetCore.Mvc;
using Marketplace.DTO;
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
    [HttpGet]
    public async Task<ResponceDto<Page<UserDto>>> GetUsers()
    {
        var users =await _userServer.GetUsers();
        Page<UserDto> pageuser = Page<UserDto>.Create(users, users.Items.Select(a => new UserDto(a)));
        
        return new(pageuser);
    }

    [Route("/api/v1/users/{id}")]
    [HttpGet]
    public async Task<ResponceDto<UserDto>> GetUser(Guid id)
    {
        var user = await _userServer.GetUser(id);
        UserDto userD = new UserDto(user);
        return new(userD);
    }
    
    [Route("/api/v1/user/block/{id}")]
    [HttpGet]
    public async Task<ResponceDto<UserDto>> BlockUser(Guid id)
    {
        if (!role.Equals(Role.Admin))
        {
            throw new AccessDeniedException();
        }
        var blockuser = await _userServer.ChangeBlockUser(id, false);
        return new(new UserDto(blockuser));
    }

    [Route("/api/v1/user/unblock/{id}")]
    [HttpGet]
    public async Task<ResponceDto<UserDto>> UnblockUser(Guid id)
    {
        if (!role.Equals(Role.Admin))
        {
            throw new AccessDeniedException();
        }
        var unblockuser =await _userServer.ChangeBlockUser(id, true);
        return new(new UserDto(unblockuser));
    }

    [Route("/api/v1/user/admin")]
    [HttpPost]
    public Task<ResponceDto<UserDto>> CreateAdmin([FromBody] RegisterDTO userDto)
    {
        if (!role.Equals(Role.Admin))
        {
            throw new AccessDeniedException();
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
        _userServer.CreateAdmin(adminuser);
        return Task.FromResult<ResponceDto<UserDto>>(new(new UserDto(adminuser)));
    }
}