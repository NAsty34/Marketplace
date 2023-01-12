using System.Security.Claims;
using data.model;
using logic.Exceptions;
using logic.Service;
using Marketplace.DTO;

namespace Marketplace.controller;

public class AuthMiddleware
{
    private readonly RequestDelegate next;

    public AuthMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context, IUserServer userServer)
    {
        if (!context.User.Identity.IsAuthenticated) return;
        var id = context.User.Claims.First(a => a.Type == ClaimTypes.Actor).Value;
        var user = userServer.GetUser(int.Parse(id));
        if (user == null || !user.IsActive)
        {
            throw new AccessDeniedException();
        }
    }

}