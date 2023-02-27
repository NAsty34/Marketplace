using data.model;
using logic.Exceptions;
using logic.Service.Inreface;
using Microsoft.AspNetCore.Mvc;
using Marketplace.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Marketplace.controller;
[Authorize(Roles = nameof(RoleEntity.Admin))]
public class UserController:UserBaseController
{
    private readonly IUserServer _userServer;

    public UserController(IUserServer userServer)
    {
        _userServer = userServer;
    }

    [Route("/api/v1/users")]
    [HttpGet]
    public async Task<ResponceDto<PageEntity<UserDto>>> GetUsers(int? page, int? size)
    {
        var users =await _userServer.GetUsers(page, size);
        var pageuser = PageEntity<UserDto>.Create(users, users.Items.Select(a => new UserDto(a)));
        
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
    
    [Route("/api/v1/user/{id}/block")]
    [HttpPatch]
    public async Task<ResponceDto<UserDto>> BlockUser(Guid id)
    {
        if (!Role.Equals(RoleEntity.Admin))
        {
            throw new AccessDeniedException();
        }
        var blockuser = await _userServer.ChangeBlockUser(id, false);
        return new(new UserDto(blockuser));
    }

    [Route("/api/v1/user/{id}/unblock")]
    [HttpPatch]
    public async Task<ResponceDto<UserDto>> UnblockUser(Guid id)
    {
        if (!Role.Equals(RoleEntity.Admin))
        {
            throw new AccessDeniedException();
        }
        var unblockuser =await _userServer.ChangeBlockUser(id, true);
        return new(new UserDto(unblockuser));
    }

    [Route("/api/v1/user/admin")]
    [HttpPost]
    public async Task<ResponceDto<UserDto>> CreateAdmin([FromBody] RegisterDto userDto)
    {
        if (!Role.Equals(RoleEntity.Admin))
        {
            throw new AccessDeniedException();
        }
        var adminuser = new UserEntity()
        {
            Id = new Guid(),
            Name = userDto.Name,
            Surname = userDto.Surname,
            Patronymic = userDto.Surname,
            Email = userDto.Email,
            RoleEntity = userDto.Role,
            EmailIsVerified = true,
            CreateDate = DateTime.Now,
            CreatorId = userDto.Id,
            Password = userDto.Password,
            IsActive = true,
            IsDeleted = false
        };
        await _userServer.CreateAdmin(adminuser);
        return new(new UserDto(adminuser));
    }
}