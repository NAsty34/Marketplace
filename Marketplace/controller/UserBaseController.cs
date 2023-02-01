using System.Security.Claims;
using data.model;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.controller;


public class UserBaseController:Controller
{
    public UserBaseController()
    {
    }

    protected Role? role =>
        Role.TryParse(HttpContext.User.Claims.First(a => a.Type == ClaimTypes.Role).Value, out Role role) ? role : null;
    protected Guid? Userid => 
        Guid.TryParse(HttpContext.User.Claims.First(a => a.Type == ClaimTypes.Actor).Value, out Guid userid)?userid:null;
    
}