using System.Security.Claims;
using data.model;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.controller;


public class UserBaseController:Controller
{
    
    protected Guid? Userid => 
        Guid.TryParse(HttpContext.User.Claims.First(a => a.Type == ClaimTypes.Actor).Value, out Guid userid)?userid:null;
    protected RoleEntity? Role =>
        Enum.TryParse(HttpContext.User.Claims.First(a => a.Type == ClaimTypes.Role).Value, out RoleEntity role) ? role : null;
    
    
}