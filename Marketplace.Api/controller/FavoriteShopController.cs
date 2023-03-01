using AutoMapper;
using data.model;
using logic.Service;
using logic.Service.Inreface;
using Marketplace.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Marketplace.controller;

[Authorize]


public class FavoriteShopController:UserBaseController
{
    private IFavoriteShopService _favoriteShopService;
    private IShopService _shopService;
    private IUserServer _userServer;


    public FavoriteShopController(IShopService shopService, IUserServer userServer, IFavoriteShopService favoriteShopService)
    {
        _shopService = shopService;
        _userServer = userServer;
        _favoriteShopService = favoriteShopService;
    }

    [Route("/api/v1/me/shops")]
    [HttpGet]
    public async Task<ResponceDto<PageEntity<FavoriteShopDto>>> FavoriteShops(int page, int size)
    {
        var pageFavShop = await _favoriteShopService.GetFavoriteShops(Userid.Value, page, size);
        var result =
            PageEntity<FavoriteShopDto>.Create(pageFavShop, pageFavShop.Items.Select(a => new FavoriteShopDto(a)));
        return new (result);
    }

    [Route("/api/v1/me/shops/{shopid}")]
    [HttpGet]
    public async Task<ResponceDto<FavoriteShopDto>> CreateFavoriteShops(Guid shopid)
    {
        var Shop = await _shopService.GetShop(shopid);
        var User = await _userServer.GetUser(Userid.Value);
        var favShop = new FavoriteShopsEntity()
        {
            ShopId = shopid,
            UserId = Userid.Value,
            Shop = Shop,
            User = User
        };
        await _favoriteShopService.CreateFavShop(favShop);
        
        return new (new FavoriteShopDto(favShop));
    }

    [Route("/api/v1/me/shops/{shopid}")]
    [HttpDelete]
    public async Task<ResponceDto<string>> DelFavoriteShops(Guid shopid)
    {
        await _favoriteShopService.DelFavShop(shopid, Userid.Value);
        return new("Магазин удален из избанных");
    }
   
}