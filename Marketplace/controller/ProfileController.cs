using data.model;
using logic.Service.Inreface;
using Marketplace.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.controller;

[Authorize]
public class ProfileController : UserBaseController
{
    private readonly IUserServer _userServer;
    private readonly IConfiguration _appConfig;

    public ProfileController(IUserServer userServer, IConfiguration appConfig) 
    {
        _userServer = userServer;
        _appConfig = appConfig;
    }

    [Route("/api/v1/me")]
    [HttpGet]
    public async Task<ResponceDto<UserDto>> GetProfile()
    {
        var user = await _userServer.GetUser(Userid.Value);
        return new (new UserDto(user));
        
    }
    
    [Route("/api/v1/me")]
    [HttpPut]
    public async Task<ResponceDto<UserDto>> EditProfile([FromBody]UserDto userDto)
    {
        User user = new User()
        {
            Name = userDto.Name,
            Surname = userDto.Surname,
            Patronymic = userDto.Patronymic,
            Id = Userid.Value
        };
        user = await _userServer.EditUser(user);
        return new(new UserDto(user));
    }

    [Route("/api/v1/me/shops")]
    [HttpGet]
    public async Task<ResponceDto<IEnumerable<ShopDto>>> FavoriteShops()
    {
        var list = await _userServer.GetFavoriteShops(Userid.Value);
        return new ResponceDto<IEnumerable<ShopDto>>( list.Select(a=>new ShopDto(a, _appConfig)));
    }

    [Route("/api/v1/me/shops/{shopid}")]
    [HttpGet]
    public async Task<ResponceDto<ShopDto>> CreateFavoriteShops(Guid shopid)
    {
        var shop = await _userServer.CreateFavShop(shopid, Userid.Value);
        return new ResponceDto<ShopDto>(new ShopDto(shop, _appConfig));
    }
    
    [Route("/api/v1/me/shops/{shopid}")]
    [HttpDelete]
    public async Task<ResponceDto<ShopDto>> DelFavoriteShops(Guid shopid)
    {
        var shop = await _userServer.DelFavShop(shopid, Userid.Value);
        return new ResponceDto<ShopDto>(new ShopDto(shop, _appConfig));
    }

}