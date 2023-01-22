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
public class ShopController:Controller
{
    private IShopService _ishopservice;
    private IUserServer _userServer;
    private IFileInfoService _fileInfoService;
    private IConfiguration _appConfig;
    
    
    public ShopController(IShopService ishopservice, IUserServer _userServer, IFileInfoService fileInfoService, IConfiguration appConfig)
    {
        this._ishopservice = ishopservice;
        this._userServer = _userServer;
        this._fileInfoService = fileInfoService;
        this._appConfig = appConfig;
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
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(usid).CopyTo(bytes, 0);
            shop = _ishopservice.GetSellerShops(new Guid(bytes));
        }

        Page<ShopDTO> result = Page<ShopDTO>.Create(shop, shop.Items.Select(a => new ShopDTO(a, _appConfig)));
        return new(result);
    }

    [Route("/api/v1/shops/{id}")]
    [HttpGet]
    public ResponceDto<ShopDTO> GetShop(Guid id)
    {
       var selshop = _ishopservice.GetShop(id);
        string userrole = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;
        Enum.TryParse(userrole, out Role userRole);
        if (userRole.Equals(Role.Buyer) && !selshop.isPublic)
        {

            throw new SystemException("Access denied");
        }
        var usid = User.Claims.First(a => a.Type == ClaimTypes.Actor).Value;
       
        if (selshop.CreatorId != Guid.Parse(usid) && userRole.Equals(Role.Seller))
        {
            throw new SystemException("Access denied");
        }
        return new(new ShopDTO(selshop, _appConfig));
    }

    [Route("/api/v1/shops")]
    [HttpPost]
    public async Task<ResponceDto<ShopDTO>> CreateShop([FromForm] ShopDTO shopDto, IFormFile file)
    {
        var userrole = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;
        Enum.TryParse(userrole, out Role role);
        if (!role.Equals(Role.Seller) && !role.Equals(Role.Admin))
        {
            throw new SystemException("Access denied");
        }
        var iduser = User.Claims.First(a => a.Type == ClaimTypes.Actor).Value;
        var user = _userServer.GetUser(Guid.Parse(iduser));
        var shops = new Shop()
        {
            Name = shopDto.Name,
            Description = shopDto.Description,
            isPublic = shopDto.isPublic,
            Inn = shopDto.Inn,
            Creator = user,
            Id = new Guid(),
        };
        await _ishopservice.CreateShop(shops);
        if (file != null)
        {
            data.model.FileInfo fileIn = _fileInfoService.Addfile(file, shops.Id);
            shops.Logo = fileIn;
            _ishopservice.EditShop(shops, Guid.Parse(iduser), role);
        }
        return new(new ShopDTO(shops, _appConfig));
    }

    [Route("/api/v1/shops/{shopid}")]
    [HttpPut]
    public ResponceDto<ShopDTO> EditGetShops([FromForm] ShopDTO shopDto, IFormFile file, Guid shopid)
    {
        var userrole = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;
        Enum.TryParse(userrole, out Role role);
        var userid = User.Claims.First(a => a.Type == ClaimTypes.Actor).Value;
        if (!role.Equals(Role.Seller) && !role.Equals(Role.Admin))
        {
            throw new AccessDeniedException();
        }
        
        if (role.Equals(Role.Admin)) userid = Guid.Empty.ToString();
        data.model.FileInfo fi=null;
        if (file != null)
        {
            fi = _fileInfoService.Addfile(file, shopid);
        }
        var shops = new Shop()
        {
            Name = shopDto.Name,
            Description = shopDto.Description,
            isPublic = shopDto.isPublic,
            Inn = shopDto.Inn,
            Logo = fi,
            Id = shopid
        };
        var shope = _ishopservice.EditShop(shops, Guid.Parse(userid), role);
        return new(new ShopDTO(shope, _appConfig));
    }

    [Route("/api/v1/shops/block/{id}")]
    [HttpGet]
    public ResponceDto<ShopDTO> BlockShop(Guid id)
    {
        var userrole = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;
        Enum.TryParse(userrole, out Role role);
        if (!role.Equals(Role.Admin))
        {
            throw new SystemException("Access denied");
        }
        var blockshop = _ishopservice.ChangeBlockShop(id, false);
        return new(new ShopDTO(blockshop, _appConfig));
    }

    [Route("/api/v1/shops/unblock/{id}")]
    [HttpGet]
    public ResponceDto<ShopDTO> UnblockGetShops(Guid id)
    {
        var userrole = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;
        Enum.TryParse(userrole, out Role role);
        if (!role.Equals(Role.Admin))
        {
            throw new SystemException("Access denied");
        }
        var unblockshop = _ishopservice.ChangeBlockShop(id, true);
        return new(new ShopDTO(unblockshop, _appConfig));
    }

    [Route("/api/v1/shops/{id}")]
    [HttpDelete]
    public ResponceDto<string> DeleteShop(Guid id)
    {
        _ishopservice.DeleteShop(id);
        return new("Shop ok deleted");
    }
}