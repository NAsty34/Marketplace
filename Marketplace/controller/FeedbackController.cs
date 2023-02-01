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
public class FeedbackController:UserBaseController
{
    private IFeedbackService _feedbackService;
    private IConfiguration appConfig;

    public FeedbackController(ILogger<UserBaseController> logger, IFeedbackService _feedbackService, IConfiguration _appConfig)
    {
        this._feedbackService = _feedbackService;
        this.appConfig = _appConfig;
    }
    [Route("/api/v1/users/{id}/feedback")]
    [HttpGet]
    public ResponceDto<Page<FeedbackDTO>> UserFeedback(Guid id)
    {
        var idfeedbyuser = _feedbackService.GetByUser(id, role.Equals(Role.Admin));
        Page<FeedbackDTO> findfeed = Page<FeedbackDTO>.Create(idfeedbyuser, idfeedbyuser.Items.Select(a => new FeedbackDTO(a, appConfig)));
        return new(findfeed);
    }

    [Route("/api/v1/shops/{id}/feedback")]
    [HttpGet]
    public ResponceDto<Page<FeedbackDTO>> ShopFeedback(Guid id)
    {
        var idfeedbyshop = _feedbackService.GetByShop(id, role.Equals(Role.Admin));
        Page<FeedbackDTO> findfeed = Page<FeedbackDTO>.Create(idfeedbyshop, idfeedbyshop.Items.Select(a => new FeedbackDTO(a, appConfig)));
        return new(findfeed);
    }

    [Route("/api/v1/shops/{id}/feedback")]
    [HttpPost]
    public ResponceDto<FeedbackDTO> AddFeedback([FromBody]FeedbackDTO feedbackDto, Guid id)
    {
        var feed = new Feedback()
        {
            Content = feedbackDto.Content,
            CreateDate = DateTime.Now,
            Stars = feedbackDto.Stars,
            ShopId = id,
            CreatorId = Userid

        };
        _feedbackService.AddFeedback(feed);
        return new(new FeedbackDTO(feed, appConfig));
    }

    [Route("/api/v1/shops/feedback/{id}")]
    [HttpPut]
    public ResponceDto<FeedbackDTO> EditFeedback([FromBody]FeedbackDTO feedbackDto, Guid id)
    {
        var feed = new Feedback()
        {
            Content = feedbackDto.Content,
            Stars = feedbackDto.Stars,
            Id = id
        };
        var upfeed = _feedbackService.EditFeedback(feed, (Guid)Userid, (Role)role);
        return new(new FeedbackDTO(upfeed, appConfig));
    }

    [Route("/api/v1/shops/feedback/{id}")]
    [HttpDelete]
    public ResponceDto<string> DeleteFeedback(Guid id)
    {
        _feedbackService.DeleteFeedback(id, (Guid)Userid, (Role)role);
        return new("Успешно удалено!");
    }
    [Route("/api/v1/feedback/block/{id}")]
    [HttpGet]
    public ResponceDto<FeedbackDTO> BlockFeedback(Guid id)
    {
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
        if (!role.Equals(Role.Admin))
        {
            throw new AccessDeniedException();
        }
        var unblockfeed = _feedbackService.ChangeBlockFeedback(id, true);
        return new(new FeedbackDTO(unblockfeed, appConfig));
    }
}