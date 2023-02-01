using System.Security.Claims;
using data.model;
using data.Repository.Interface;
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
    

    public ShopController(HttpContext context, ILogger<UserBaseController> logger, IShopService ishopservice, IUserServer _userServer, IFileInfoService fileInfoService, IConfiguration appConfig) 
    {
        logger.Log(LogLevel.Information, "=============== USER " + User);
        logger.Log(LogLevel.Information, "=============== context " + context);
        logger.Log(LogLevel.Information, "=============== USER ID " + Userid);
        this._ishopservice = ishopservice;
        this._userServer = _userServer;
        this._fileInfoService = fileInfoService;
        this._appConfig = appConfig;
        
    }

    [Route("/api/v1/shops")]
    [HttpGet]
    public ResponceDto<Page<ShopDTO>> Shops(ILogger<ShopController> logger)
    {
     logger.Log(LogLevel.Information, "============" + Userid);   
        Page<Shop> shop;
        if (role.Equals(Role.Admin))
        {
            shop = _ishopservice.GetShops();
        }
        else if (role.Equals(Role.Buyer))
        {
            shop = _ishopservice.GetPublicShops();
        }
        else
        {
            shop = _ishopservice.GetSellerShops((Guid)Userid);
        }

        Page<ShopDTO> result = Page<ShopDTO>.Create(shop, shop.Items.Select(a => new ShopDTO(a, _appConfig)));
        return new(result);
    }

    [Route("/api/v1/shops/{id}")]
    [HttpGet]
    public ResponceDto<ShopDTO> GetShop(Guid id)
    {
        var selshop = _ishopservice.GetShop(id);
        if (role.Equals(Role.Buyer) && !selshop.isPublic)
        {

            throw new SystemException("Access denied");
        }
        
        if (selshop.CreatorId != Userid && role.Equals(Role.Seller))
        {
            throw new SystemException("Access denied");
        }
        return new(new ShopDTO(selshop, _appConfig));
    }

    [Route("/api/v1/shops")]
    [HttpPost]
    public async Task<ResponceDto<ShopDTO>> CreateShop([FromForm] ShopDTO shopDto, IFormFile file)
    {
        
        if (!role.Equals(Role.Seller) && !role.Equals(Role.Admin))
        {
            throw new SystemException("Access denied");
        }
        
        var user = _userServer.GetUser((Guid)Userid);
        Guid Id = new Guid();
        var shops = new Shop()
        {
            Name = shopDto.Name,
            Description = shopDto.Description,
            isPublic = shopDto.isPublic,
            Inn = shopDto.Inn,
            Creator = user,
            Id = Id,
            ShopCategory = shopDto.Categories.Select(a=>new ShopCategory(Id, a)).ToList(),
            ShopDeliveries = shopDto.Deliveris.Select(a=>new ShopDelivery(Id, a)).ToList(),
            ShopPayment = shopDto.Payments.Select(a=>new ShopPayment(Id, a)).ToList(),
            ShopTypes = shopDto.Types.Select(a=>new ShopTypes(Id, a)).ToList()
        };
        
        if (file != null)
        {
            data.model.FileInfo fileIn = _fileInfoService.Addfile(file, shops.Id);
            shops.Logo = fileIn;
        }
        _ishopservice.CreateShop(shops);
        return new(new ShopDTO(shops, _appConfig));
    }

    [Route("/api/v1/shops/{shopid}")]
    [HttpPut]
    public ResponceDto<ShopDTO> EditShops([FromForm] ShopDTO shopDto, IFormFile file, Guid shopid)
    {
        
        
        if (!role.Equals(Role.Seller) && !role.Equals(Role.Admin))
        {
            throw new AccessDeniedException();
        }
        
        //if (role.Equals(Role.Admin)) Userid = Userid;
        data.model.FileInfo fi=null;
        if (file != null)
        {
            fi = _fileInfoService.Addfile(file, shopid);
        }
        Guid Id = shopid;
        var shops = new Shop()
        {
            Name = shopDto.Name,
            Description = shopDto.Description,
            isPublic = shopDto.isPublic,
            Inn = shopDto.Inn,
            Logo = fi,
            Id = Id,
            ShopCategory = shopDto.Categories.Select(a=>new ShopCategory(Id, a)).ToList(),
            ShopDeliveries = shopDto.Deliveris.Select(a=>new ShopDelivery(Id, a)).ToList(),
            ShopPayment = shopDto.Payments.Select(a=>new ShopPayment(Id, a)).ToList(),
            ShopTypes = shopDto.Types.Select(a=>new ShopTypes(Id, a)).ToList()
        };
        var shope = _ishopservice.EditShop(shops, (Guid)Userid, (Role)role);
        return new(new ShopDTO(shope, _appConfig));
    }

    [Route("/api/v1/shops/block/{id}")]
    [HttpGet]
    public ResponceDto<ShopDTO> BlockShop(Guid id)
    {
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