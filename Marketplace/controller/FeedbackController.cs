using data.model;
using logic.Exceptions;
using logic.Service.Inreface;
using Microsoft.AspNetCore.Mvc;
using Marketplace.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Marketplace.controller;
[Authorize]
public class FeedbackController:UserBaseController
{
    private IFeedbackService _feedbackService;
    private IConfiguration _appConfig;

    public FeedbackController(IFeedbackService feedbackService, IConfiguration appConfig)
    {
        _feedbackService = feedbackService;
        _appConfig = appConfig;
    }
    [Route("/api/v1/users/{id}/feedback")]
    [HttpGet]
    public async Task<ResponceDto<Page<FeedbackDto>>> UserFeedback(Guid id)
    {
        var idfeedbyuser = await _feedbackService.GetByUser(id, Role.Equals(data.model.Role.Admin));
        Page<FeedbackDto> findfeed = Page<FeedbackDto>.Create(idfeedbyuser, idfeedbyuser.Items.Select(a => new FeedbackDto(a, _appConfig)));
        return new(findfeed);
    }

    [Route("/api/v1/shops/{id}/feedback")]
    [HttpGet]
    public async Task<ResponceDto<Page<FeedbackDto>>> ShopFeedback(Guid id)
    {
        var idfeedbyshop = await _feedbackService.GetByShop(id, Role.Equals(data.model.Role.Admin));
        Page<FeedbackDto> findfeed = Page<FeedbackDto>.Create(idfeedbyshop, idfeedbyshop.Items.Select(a => new FeedbackDto(a, _appConfig)));
        return new(findfeed);
    }

    [Route("/api/v1/shops/{id}/feedback")]
    [HttpPost]
    public async Task<ResponceDto<FeedbackDto>> AddFeedback([FromBody]FeedbackDto feedbackDto, Guid id)
    {
        var feed = new Feedback()
        {
            Content = feedbackDto.Content,
            CreateDate = DateTime.Now,
            Stars = feedbackDto.Stars,
            ShopId = id,
            CreatorId = Userid.Value

        };
        await _feedbackService.AddFeedback(feed);
        return new(new FeedbackDto(feed, _appConfig));
    }

    [Route("/api/v1/shops/feedback/{id}")]
    [HttpPut]
    public async Task<ResponceDto<FeedbackDto>> EditFeedback([FromBody]FeedbackDto feedbackDto, Guid id)
    {
        var feed = new Feedback()
        {
            Content = feedbackDto.Content,
            Stars = feedbackDto.Stars,
            Id = id
        };
        var upfeed = await _feedbackService.EditFeedback(feed, Userid.Value, (Role)Role);
        return new(new FeedbackDto(upfeed, _appConfig));
    }

    [Route("/api/v1/shops/feedback/{id}")]
    [HttpDelete]
    public async Task<ResponceDto<string>> DeleteFeedback(Guid id)
    {
        await _feedbackService.DeleteFeedback(id, Userid.Value, (Role)Role);
        return new("Успешно удалено!");
    }
    [Route("/api/v1/feedback/block/{id}")]
    [HttpGet]
    public async Task<ResponceDto<FeedbackDto>> BlockFeedback(Guid id)
    {
        if (!Role.Equals(data.model.Role.Admin))
        {
            throw new AccessDeniedException();
        }
        var blockfeed = await _feedbackService.ChangeBlockFeedback(id, false);
        return new(new FeedbackDto(blockfeed, _appConfig));
    }

    [Route("/api/v1/feedback/unblock/{id}")]
    [HttpGet]
    public async Task<ResponceDto<FeedbackDto>> UnblockFeedback(Guid id)
    {
        if (!Role.Equals(data.model.Role.Admin))
        {
            throw new AccessDeniedException();
        }
        var unblockfeed = await _feedbackService.ChangeBlockFeedback(id, true);
        return new(new FeedbackDto(unblockfeed, _appConfig));
    }
}