using System.Security.Claims;
using data.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Marketplace.controller;


public class UserBaseController:Controller
{
    public UserBaseController(ILogger<UserBaseController> logger)
    {
        this.logger = logger;

    }

    public override void OnActionExecuting(ActionExecutingContext  context)
    {
        base.OnActionExecuting(context);
        logger.Log(LogLevel.Information, "USER CLAIMS" + HttpContext.User);
        userid = Guid.Parse(HttpContext.User.Claims.First(a => a.Type == ClaimTypes.Actor).Value);
        var role = HttpContext.User.Claims.First(a => a.Type == ClaimTypes.Role).Value;
        Enum.TryParse(role, out Role userrole);
        this.userrole = userrole;
    }

    public Guid userid { get; set; }
    public Role userrole { get; set; }
    public ILogger logger { get; set; }
}