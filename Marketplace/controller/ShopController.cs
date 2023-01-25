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
public class ShopController:UserBaseController
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
        
        Page<Shop> shop;
        if (userrole.Equals(Role.Admin))
        {
            shop = _ishopservice.GetShops();
        }
        else if (userrole.Equals(Role.Buyer))
        {
            shop = _ishopservice.GetPublicShops();
        }
        else
        {
            shop = _ishopservice.GetSellerShops(userid);
        }

        Page<ShopDTO> result = Page<ShopDTO>.Create(shop, shop.Items.Select(a => new ShopDTO(a, _appConfig)));
        return new(result);
    }

    [Route("/api/v1/shops/{id}")]
    [HttpGet]
    public ResponceDto<ShopDTO> GetShop(Guid id)
    {
        var selshop = _ishopservice.GetShop(id);
        if (userrole.Equals(Role.Buyer) && !selshop.isPublic)
        {

            throw new SystemException("Access denied");
        }
        
        if (selshop.CreatorId != userid && userrole.Equals(Role.Seller))
        {
            throw new SystemException("Access denied");
        }
        return new(new ShopDTO(selshop, _appConfig));
    }

    [Route("/api/v1/shops")]
    [HttpPost]
    public async Task<ResponceDto<ShopDTO>> CreateShop([FromForm] ShopDTO shopDto, IFormFile file)
    {
        
        if (!userrole.Equals(Role.Seller) && !userrole.Equals(Role.Admin))
        {
            throw new SystemException("Access denied");
        }
        
        var user = _userServer.GetUser(userid);
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
            _ishopservice.EditShop(shops, userid, userrole);
        }
        return new(new ShopDTO(shops, _appConfig));
    }

    [Route("/api/v1/shops/{shopid}")]
    [HttpPut]
    public ResponceDto<ShopDTO> EditGetShops([FromForm] ShopDTO shopDto, IFormFile file, Guid shopid)
    {
        
        
        if (!userrole.Equals(Role.Seller) && !userrole.Equals(Role.Admin))
        {
            throw new AccessDeniedException();
        }
        
        if (userrole.Equals(Role.Admin)) userid = userid;
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
        var shope = _ishopservice.EditShop(shops, userid, userrole);
        return new(new ShopDTO(shope, _appConfig));
    }

    [Route("/api/v1/shops/block/{id}")]
    [HttpGet]
    public ResponceDto<ShopDTO> BlockShop(Guid id)
    {
        if (!userrole.Equals(Role.Admin))
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
        if (!userrole.Equals(Role.Admin))
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