using System.Security.Claims;
using data.model;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.controller;

public class UserBaseController:Controller
{
    public UserBaseController()
    {
        userid = Guid.Parse(User.Claims.First(a => a.Type == ClaimTypes.Actor).Value);
        var role = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;
        Enum.TryParse(role, out Role userrole);
        this.userrole = userrole;
    }

    public Guid userid { get; set; }
    public Role userrole { get; set; }
}