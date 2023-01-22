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
    private IConfiguration appConfig;

    public FeedbackController(IFeedbackService _feedbackService, IConfiguration _appConfig)
    {
        this._feedbackService = _feedbackService;
        this.appConfig = _appConfig;
    }
    [Route("/api/v1/users/{id}/feedback")]
    [HttpGet]
    public ResponceDto<Page<FeedbackDTO>> UserFeedback(Guid id)
    {
        string role = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;
        Enum.TryParse(role, out Role qrole);
        var idfeedbyuser = _feedbackService.GetByUser(id, qrole.Equals(Role.Admin));
        Page<FeedbackDTO> findfeed = Page<FeedbackDTO>.Create(idfeedbyuser, idfeedbyuser.Items.Select(a => new FeedbackDTO(a, appConfig)));
        return new(findfeed);
    }

    [Route("/api/v1/shops/{id}/feedback")]
    [HttpGet]
    public ResponceDto<Page<FeedbackDTO>> ShopFeedback(Guid id)
    {
        string role = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;
        Enum.TryParse(role, out Role qrole);
        var idfeedbyshop = _feedbackService.GetByShop(id, qrole.Equals(Role.Admin));
        Page<FeedbackDTO> findfeed = Page<FeedbackDTO>.Create(idfeedbyshop, idfeedbyshop.Items.Select(a => new FeedbackDTO(a, appConfig)));
        return new(findfeed);
    }

    [Route("/api/v1/shops/{id}/feedback")]
    [HttpPost]
    public ResponceDto<FeedbackDTO> AddFeedback([FromBody]FeedbackDTO feedbackDto, Guid id)
    {
        var userid = User.Claims.First(a => a.Type == ClaimTypes.Actor).Value;
        
        var feed = new Feedback()
        {
            Content = feedbackDto.Content,
            CreateDate = DateTime.Now,
            Stars = feedbackDto.Stars,
            ShopId = id,
            CreatorId = Guid.Parse(userid)
        };
        var newfeed = _feedbackService.AddFeedback(feed);
        return new(new FeedbackDTO(newfeed, appConfig));
    }

    [Route("/api/v1/shops/feedback/{id}")]
    [HttpPut]
    public ResponceDto<FeedbackDTO> EditFeedback([FromBody]FeedbackDTO feedbackDto, Guid id)
    {
        string roleuser = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;
        var userid = User.Claims.First(a => a.Type == ClaimTypes.Actor).Value;
        Enum.TryParse(roleuser, out Role role);
        var feed = new Feedback()
        {
            Content = feedbackDto.Content,
            Stars = feedbackDto.Stars,
            Id = id
        };
        var upfeed = _feedbackService.EditFeedback(feed, Guid.Parse(userid), role);
        return new(new FeedbackDTO(upfeed, appConfig));
    }

    [Route("/api/v1/shops/feedback/{id}")]
    [HttpDelete]
    public ResponceDto<string> DeleteFeedback(Guid id)
    {
        string roleuser = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;
        var userid = User.Claims.First(a => a.Type == ClaimTypes.Actor).Value;
        Enum.TryParse(roleuser, out Role role);
        _feedbackService.DeleteFeedback(id, Guid.Parse(userid), role);
        return new("Успешно удалено!");
    }
    [Route("/api/v1/feedback/block/{id}")]
    [HttpGet]
    public ResponceDto<FeedbackDTO> BlockFeedback(Guid id)
    {
        var userrole = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;
        Enum.TryParse(userrole, out Role role);
        if (!role.Equals(Role.Admin))
        {
            throw new AccessDeniedException();
        }
        var blockfeed = _feedbackService.ChangeBlockFeedback(id, false);
        return new(new FeedbackDTO(blockfeed, appConfig));
    }

    [Route("/api/v1/feedback/unblock/{id}")]
    [HttpGet]
    public ResponceDto<FeedbackDTO> UnblockFeedback(Guid id)
    {
        var userrole = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;
        Enum.TryParse(userrole, out Role role);
        if (!role.Equals(Role.Admin))
        {
            throw new AccessDeniedException();
        }
        var unblockfeed = _feedbackService.ChangeBlockFeedback(id, true);
        return new(new FeedbackDTO(unblockfeed, appConfig));
    }
}