using System.Security.Claims;
using data.model;
using logic.Exceptions;
using logic.Service;
using logic.Service.Inreface;
using Marketplace.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.controller;

[Authorize]
public class ProfileController : UserBaseController
{
    private readonly IUserServer _UserServer;
    private readonly IShopService _shopService;
    private readonly IConfiguration appConfig;

    public ProfileController(ILogger<UserBaseController> logger, IUserServer userServer, IShopService shopService, IConfiguration _appConfig) 
    {
        this._UserServer = userServer;
        this._shopService = shopService;
        this.appConfig = _appConfig;
    }

    [Route("/api/v1/me")]
    [HttpGet]
    public ResponceDto<UserDto> GetProfile()
    {
        var user = _UserServer.GetUser((Guid)Userid);
        return new (new UserDto(user));
    }
    
    [Route("/api/v1/me")]
    [HttpPut]
    public ResponceDto<UserDto> EditProfile([FromBody]UserDto userDto)
    {
        User user = new User()
        {
            Name = userDto.Name,
            Surname = userDto.Surname,
            Patronymic = userDto.Patronymic,
            Id = (Guid)Userid
        };
        user = _UserServer.EditUser(user);
        return new(new UserDto(user));
    }

    [Route("/api/v1/me/shops")]
    [HttpGet]
    public ResponceDto<IEnumerable<ShopDTO>> FavoriteShops()
    {
        return new ResponceDto<IEnumerable<ShopDTO>>(_UserServer.GetFavoriteShops((Guid)Userid).Select(a=>new ShopDTO(a, appConfig)));
    }

    [Route("/api/v1/me/shops/{shopid}")]
    [HttpGet]
    public ResponceDto<ShopDTO> CreateFavoriteShops(Guid shopid)
    {
        var shop = _UserServer.CreateFavShop(shopid, (Guid)Userid);
        return new ResponceDto<ShopDTO>(new ShopDTO(shop, appConfig));
    }
    
    [Route("/api/v1/me/shops/{shopid}")]
    [HttpDelete]
    public ResponceDto<ShopDTO> DelFavoriteShops(Guid shopid)
    {
        var shop = _UserServer.DelFavShop(shopid, (Guid)Userid);
        return new ResponceDto<ShopDTO>(new ShopDTO(shop, appConfig));
    }

}