using System.Security.Claims;
using data.model;
using logic.Exceptions;
using logic.Service;
using logic.Service.Inreface;
using Microsoft.AspNetCore.Mvc;
using Marketplace.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Marketplace.controller;
[Authorize]
public class FeedbackController:Controller
{
    private IFeedbackService _feedbackService;

    public FeedbackController(IFeedbackService _feedbackService)
    {
        this._feedbackService = _feedbackService;
    }
    [Route("/api/v1/users/{id}/feedback")]
    [HttpGet]
    public ResponceDto<Page<FeedbackDTO>> UserFeedback(int id)
    {
        string role = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;
        Enum.TryParse(role, out Role qrole);
        var q = _feedbackService.GetByUser(id, qrole.Equals(Role.Admin));
        Page<FeedbackDTO> fp = Page<FeedbackDTO>.Create(q, q.Items.Select(a => new FeedbackDTO(a)));
        return new(fp);
    }

    [Route("/api/v1/shops/{id}/feedback")]
    [HttpGet]
    public ResponceDto<Page<FeedbackDTO>> ShopFeedback(int id)
    {
        string role = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;
        Enum.TryParse(role, out Role qrole);
        var q = _feedbackService.GetByShop(id, qrole.Equals(Role.Admin));
        Page<FeedbackDTO> fp = Page<FeedbackDTO>.Create(q, q.Items.Select(a => new FeedbackDTO(a)));
        return new(fp);
    }

    [Route("/api/v1/shops/{id}/feedback")]
    [HttpPost]
    public ResponceDto<FeedbackDTO> AddFeedback([FromBody]FeedbackDTO feedbackDto, int id)
    {
        int userid = int.Parse(User.Claims.First(a => a.Type == ClaimTypes.Actor).Value);
        var feed = new Feedback()
        {
            Content = feedbackDto.Content,
            CreateDate = DateTime.Now,
            Stars = feedbackDto.Stars,
            ShopId = id,
            CreatorId = userid
        };
        var f = _feedbackService.AddFeedback(feed);
        return new(new FeedbackDTO(f));
    }

    [Route("/api/v1/shops/feedback/{id}")]
    [HttpPut]
    public ResponceDto<FeedbackDTO> EditFeedback([FromBody]FeedbackDTO feedbackDto, int id)
    {
        string g = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;
        int userid = int.Parse(User.Claims.First(a => a.Type == ClaimTypes.Actor).Value);
        Enum.TryParse(g, out Role role);
        var feed = new Feedback()
        {
            Content = feedbackDto.Content,
            Stars = feedbackDto.Stars,
            Id = id
        };
        var f = _feedbackService.EditFeedback(feed, userid, role);
        return new(new FeedbackDTO(f));
    }

    [Route("/api/v1/shops/feedback/{id}")]
    [HttpDelete]
    public ResponceDto<string> DeleteFeedback(int id)
    {
        string g = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;
        int userid = int.Parse(User.Claims.First(a => a.Type == ClaimTypes.Actor).Value);
        Enum.TryParse(g, out Role role);
        _feedbackService.DeleteFeedback(id, userid, role);
        return new("Успешно удалено!");
    }
    [Route("/api/v1/feedback/block/{id}")]
    [HttpGet]
    public ResponceDto<FeedbackDTO> BlockFeedback(int id)
    {
        var userrole = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;
        Enum.TryParse(userrole, out Role role);
        if (!role.Equals(Role.Admin))
        {
            throw new AccessDeniedException();
        }
        var f = _feedbackService.ChangeBlockFeedback(id, false);
        return new(new FeedbackDTO(f));
    }

    [Route("/api/v1/feedback/unblock/{id}")]
    [HttpGet]
    public ResponceDto<FeedbackDTO> UnblockFeedback(int id)
    {
        var userrole = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;
        Enum.TryParse(userrole, out Role role);
        if (!role.Equals(Role.Admin))
        {
            throw new AccessDeniedException();
        }
        var f = _feedbackService.ChangeBlockFeedback(id, true);
        return new(new FeedbackDTO(f));
    }
}