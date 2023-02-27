using data.model;
using logic.Exceptions;
using logic.Service.Inreface;
using Microsoft.AspNetCore.Mvc;
using Marketplace.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Marketplace.controller;

[Authorize]
public class FeedbackController : UserBaseController
{
    private IFeedbackService _feedbackService;
    private IFileInfoService _fileInfoService;

    public FeedbackController(IFeedbackService feedbackService, IFileInfoService fileInfoService)
    {
        _feedbackService = feedbackService;
        _fileInfoService = fileInfoService;
    }

    [Route("/api/v1/users/{id}/feedback")]
    [HttpGet]
    public async Task<ResponceDto<PageEntity<FeedbackDto>>> UserFeedback(Guid id, int? page, int? size)
    {
        var idfeedbyuser = await _feedbackService.GetByUser(id, Role.Equals(data.model.RoleEntity.Admin), page, size);
        PageEntity<FeedbackDto> findfeed = PageEntity<FeedbackDto>.Create(idfeedbyuser, idfeedbyuser.Items.Select(a =>
        {
            var feedbackDto = new FeedbackDto(a);
            if (a.Shop.Logo != null)
            {
                feedbackDto.Shop.Logo = _fileInfoService.GetUrlShop(a.Shop);
            }

            return feedbackDto;
        }));
        return new(findfeed);
    }

    [Route("/api/v1/shops/{id}/feedback")]
    [HttpGet]
    public async Task<ResponceDto<PageEntity<FeedbackDto>>> ShopFeedback(Guid id, int? page, int? size)
    {
        var idfeedbyshop = await _feedbackService.GetByShop(id, Role.Equals(RoleEntity.Admin), page, size);

        var findfeed = PageEntity<FeedbackDto>.Create(idfeedbyshop, idfeedbyshop.Items.Select(a =>
        {
            var feedbackShopLogo = new FeedbackDto(a);
            if (a.Shop.Logo != null)
            {
                feedbackShopLogo.Shop.Logo = _fileInfoService.GetUrlShop(a.Shop);
            }

            return feedbackShopLogo;
        }));
        return new(findfeed);
    }

    [Route("/api/v1/shops/{id}/feedback")]
    [HttpPost]
    public async Task<ResponceDto<FeedbackDto>> AddFeedback([FromBody] FeedbackDto feedbackDto, Guid id)
    {
        var feed = new FeedbackEntity()
        {
            Content = feedbackDto.Content,
            CreateDate = DateTime.Now,
            Stars = feedbackDto.Stars,
            ShopId = id,
            CreatorId = Userid.Value
        };
        await _feedbackService.AddFeedback(feed);

        var feedbackShopLogo = new FeedbackDto(feed);
        if (feed.Shop.Logo != null)
        {
            feedbackShopLogo.Shop.Logo = _fileInfoService.GetUrlShop(feed.Shop);
        }

        return new(feedbackShopLogo);
    }

    [Route("/api/v1/shops/feedback/{id}")]
    [HttpPut]
    public async Task<ResponceDto<FeedbackDto>> EditFeedback([FromBody] FeedbackDto feedbackDto, Guid id)
    {
        var feed = new FeedbackEntity()
        {
            Content = feedbackDto.Content,
            Stars = feedbackDto.Stars,
            Id = id
        };
        var upfeed = await _feedbackService.EditFeedback(feed, Userid.Value, (RoleEntity)Role);
        var feedbackShopLogo = new FeedbackDto(upfeed);
        if (upfeed.Shop.Logo != null)
        {
            feedbackShopLogo.Shop.Logo = _fileInfoService.GetUrlShop(upfeed.Shop);
        }

        return new(feedbackShopLogo);
    }

    [Route("/api/v1/shops/feedback/{id}")]
    [HttpDelete]
    public async Task<ResponceDto<string>> DeleteFeedback(Guid id)
    {
        await _feedbackService.DeleteFeedback(id, Userid.Value, (RoleEntity)Role);
        return new("Успешно удалено!");
    }

    [Route("/api/v1/feedback/{id}/block")]
    [HttpPatch]
    public async Task<ResponceDto<FeedbackDto>> BlockFeedback(Guid id)
    {
        if (!Role.Equals(RoleEntity.Admin))
        {
            throw new AccessDeniedException();
        }

        var blockfeed = await _feedbackService.ChangeBlockFeedback(id, false);
        var feedbackShopLogo = new FeedbackDto(blockfeed);
        if (blockfeed.Shop.Logo != null)
        {
            feedbackShopLogo.Shop.Logo = _fileInfoService.GetUrlShop(blockfeed.Shop);
        }

        return new(feedbackShopLogo);
    }

    [Route("/api/v1/feedback/{id}/unblock")]
    [HttpPatch]
    public async Task<ResponceDto<FeedbackDto>> UnblockFeedback(Guid id)
    {
        if (!Role.Equals(RoleEntity.Admin))
        {
            throw new AccessDeniedException();
        }

        var unblockfeed = await _feedbackService.ChangeBlockFeedback(id, true);
        var feedbackShopLogo = new FeedbackDto(unblockfeed);
        if (unblockfeed.Shop.Logo != null)
        {
            feedbackShopLogo.Shop.Logo = _fileInfoService.GetUrlShop(unblockfeed.Shop);
        }

        return new(feedbackShopLogo);
    }
}