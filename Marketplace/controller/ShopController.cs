using System.Security.Claims;
using data.model;
using logic.Service;
using logic.Service.Inreface;
using Microsoft.AspNetCore.Mvc;
using Marketplace.DTO;
using Microsoft.AspNetCore.Authorization;
using Org.BouncyCastle.Tls;

namespace Marketplace.controller;
[Authorize]
public class ShopController:Controller
{
    private IShopService _ishopservice;
    private IUserServer _userServer;
    
    public ShopController(IShopService ishopservice, IUserServer _userServer)
    {
        this._ishopservice = ishopservice;
        this._userServer = _userServer;
    }

    [Route("/api/v1/shops")]
    [HttpGet]
    public ResponceDto<Page<ShopDTO>> Shops()
    {
        string roleuser = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;
        Enum.TryParse(roleuser, out Role userRole);
        Page<Shop> shop;
        if (userRole.Equals(Role.Admin))
        {
            shop = _ishopservice.GetShops();
        }
        else if (userRole.Equals(Role.Buyer))
        {
            shop = _ishopservice.GetPublicShops();
        }
        else
        {
            int usid = int.Parse(User.Claims.First(a => a.Type == ClaimTypes.Actor).Value);
            shop = _ishopservice.GetSellerShops(usid);
        }

        Page<ShopDTO> result = Page<ShopDTO>.Create(shop, shop.Items.Select(a => new ShopDTO(a)));
        return new(result);
    }

    [Route("/api/v1/shops/{id}")]
    [HttpGet]
    public ResponceDto<ShopDTO> GetShop(int id)
    {
        var selshop = _ishopservice.GetShop(id);
        string userrole = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;
        Enum.TryParse(userrole, out Role userRole);
        if (userRole.Equals(Role.Buyer) && !selshop.isPublic)
        {

            throw new SystemException("Access denied");
        }
        int usid = int.Parse(User.Claims.First(a => a.Type == ClaimTypes.Actor).Value);
        if (selshop.CreatorId != usid && userRole.Equals(Role.Seller))
        {
            throw new SystemException("Access denied");
        }
        return new(new ShopDTO(selshop));
    }

    [Route("/api/v1/shops")]
    [HttpPost]
    public async Task<ResponceDto<ShopDTO>> CreateShop([FromBody] ShopDTO shopDto)
    {
        var userrole = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;
        Enum.TryParse(userrole, out Role role);
        if (!role.Equals(Role.Seller) && !role.Equals(Role.Admin))
        {
            throw new SystemException("Access denied");
        }
        var iduser = User.Claims.First(a => a.Type == ClaimTypes.Actor).Value;
        
        var user = _userServer.GetUser(int.Parse(iduser));
        var shops = new Shop()
        {
            Name = shopDto.Name,
            Description = shopDto.Description,
            isPublic = shopDto.isPublic,
            Inn = shopDto.Inn,
            Logo = shopDto.Logo,
            Creator = user
        };
        
        await _ishopservice.CreateShop(shops);
        return new(new ShopDTO(shops));
    }

    [Route("/api/v1/shops/{id}")]
    [HttpPut]
    public ResponceDto<ShopDTO> EditGetShops([FromBody] ShopDTO shopDto, int id)
    {
        var userrole = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;
        Enum.TryParse(userrole, out Role role);
        var userid = int.Parse(User.Claims.First(a => a.Type == ClaimTypes.Actor).Value);
        if (!role.Equals(Role.Seller) && !role.Equals(Role.Admin))
        {
            throw new SystemException("Access denied");
        }

        if (role.Equals(Role.Admin)) userid = -1;
        
        var shops = new Shop()
        {
            Name = shopDto.Name,
            Description = shopDto.Description,
            isPublic = shopDto.isPublic,
            Inn = shopDto.Inn,
            Logo = shopDto.Logo,
            Id = id
        };
        
        var shope = _ishopservice.EditShop(shops, userid);
        return new(new ShopDTO(shope));
    }

    [Route("/api/v1/shops/block/{id}")]
    [HttpGet]
    public ResponceDto<ShopDTO> BlockShop(int id)
    {
        var userrole = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;
        Enum.TryParse(userrole, out Role role);
        if (!role.Equals(Role.Admin))
        {
            throw new SystemException("Access denied");
        }
        var blockshop = _ishopservice.ChangeBlockShop(id, false);
        return new(new ShopDTO(blockshop));
    }

    [Route("/api/v1/shops/unblock/{id}")]
    [HttpGet]
    public ResponceDto<ShopDTO> UnblockGetShops(int id)
    {
        var userrole = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;
        Enum.TryParse(userrole, out Role role);
        if (!role.Equals(Role.Admin))
        {
            throw new SystemException("Access denied");
        }
        var unblockshop = _ishopservice.ChangeBlockShop(id, true);
        return new(new ShopDTO(unblockshop));
    }

    [Route("/api/v1/shops/{id}")]
    [HttpDelete]
    public ResponceDto<string> DeleteShop(int id)
    {
        _ishopservice.DeleteShop(id);
        return new("Shop ok deleted");
    }
}