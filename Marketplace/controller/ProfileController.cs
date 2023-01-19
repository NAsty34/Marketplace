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
public class ProfileController : Controller
{
    private readonly IUserServer _UserServer;
    private readonly IShopService _shopService;

    public ProfileController(IUserServer userServer, IShopService shopService)
    {
        this._UserServer = userServer;
        this._shopService = shopService;
    }

    [Route("/api/v1/me")]
    [HttpGet]
    public ResponceDto<UserDto> GetProfile()
    {
        int usid = int.Parse(User.Claims.First(a => a.Type == ClaimTypes.Actor).Value);
        var user = _UserServer.GetUser(usid);
        return new (new UserDto(user));
    }
    
    [Route("/api/v1/me")]
    [HttpPut]
    public ResponceDto<UserDto> EditProfile([FromBody]UserDto userDto)
    {
        int usid = int.Parse(User.Claims.First(a => a.Type == ClaimTypes.Actor).Value);
        User user = new User()
        {
            Name = userDto.Name,
            Surname = userDto.Surname,
            Patronymic = userDto.Patronymic,
            Id = usid
        };
        user = _UserServer.EditUser(user);
        return new(new UserDto(user));
    }

    [Route("/api/v1/me/shops")]
    [HttpGet]
    public ResponceDto<IEnumerable<ShopDTO>> FavoriteShops()
    {
        int usid = int.Parse(User.Claims.First(a => a.Type == ClaimTypes.Actor).Value);
        return new ResponceDto<IEnumerable<ShopDTO>>(_UserServer.GetFavoriteShops(usid).Select(a=>new ShopDTO(a)));
    }

    [Route("/api/v1/me/shops/{shopid}")]
    [HttpGet]
    public ResponceDto<ShopDTO> CreateFavoriteShops(int shopid)
    {
        int userid = int.Parse(User.Claims.First(a => a.Type == ClaimTypes.Actor).Value);
        return new ResponceDto<ShopDTO>(new ShopDTO(_UserServer.CreateFavShop(shopid, userid)));
    }
    
    [Route("/api/v1/me/shops/{shopid}")]
    [HttpDelete]
    public ResponceDto<ShopDTO> DelFavoriteShops(int shopid)
    {
        int userid = int.Parse(User.Claims.First(a => a.Type == ClaimTypes.Actor).Value);
        return new ResponceDto<ShopDTO>(new ShopDTO(_UserServer.DelFavShop(shopid, userid)));
    }

}