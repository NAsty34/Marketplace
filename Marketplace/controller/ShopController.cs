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
    private ILogger<UserBaseController> logger;
    

    public ShopController(ILogger<UserBaseController> logger, IShopService ishopservice, IUserServer _userServer, IFileInfoService fileInfoService, IConfiguration appConfig)
    {
        this.logger = logger;
        this._ishopservice = ishopservice;
        this._userServer = _userServer;
        this._fileInfoService = fileInfoService;
        this._appConfig = appConfig;
        
    }

    [Route("/api/v1/shops")]
    [HttpGet]
    public async Task<ResponceDto<Page<ShopDTO>>> Shops(FiltersShops filtersShops)
    {
     /*logger.Log(LogLevel.Information, "============" + filtersShops);*/
     filtersShops.User = null;
     filtersShops.IsPublic = null;
        
        if (role.Equals(Role.Buyer))
        {
            filtersShops.IsPublic = true;
        }
        else
        {
            filtersShops.User = Userid;
        }
        
        Page<Shop> shop = await _ishopservice.GetShops(filtersShops);
        Page<ShopDTO> result = Page<ShopDTO>.Create(shop, shop.Items.Select(a => new ShopDTO(a, _appConfig)));
        return new(result);
    }

    [Route("/api/v1/shops/{id}")]
    [HttpGet]
    public async Task<ResponceDto<ShopDTO>> GetShop(Guid id)
    {
        var selshop = await _ishopservice.GetShop(id);
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
        
        var user = await _userServer.GetUser((Guid)Userid);
        Guid Id = new Guid();
        /*logger.Log(LogLevel.Information, "==========="+shopDto.Payment.First());
        logger.Log(LogLevel.Information, "==========="+shopDto.Com.First());
        logger.Log(LogLevel.Information, "==========="+shopDto.Payment.Zip(shopDto.Com, (guid, d) => new {k=guid, v=d}).First().k);*/
        var shops = new Shop()
        {
            Name = shopDto.Name,
            Description = shopDto.Description,
            isPublic = shopDto.isPublic,
            Inn = shopDto.Inn,
            Creator = user,
            Id = Id,
            ShopCategory = shopDto.Categories.Select(a=>new ShopCategory(Id, a)).ToList(),
            ShopTypes = shopDto.Types.Select(a=>new ShopTypes(Id, a)).ToList(),
            ShopDeliveries = shopDto.Deliveri.Zip(shopDto.MinPrice, (guid, d) => new {k=guid, v=d}).Select(a=>new ShopDelivery(Id, a.k, a.v)).ToList(),
            ShopPayment = shopDto.Payment.Zip(shopDto.Com, (guid, d) => new {k=guid, v=d}).Select(a=>new ShopPayment(Id, a.k, a.v)).ToList(),
            
        };
        
        if (file != null)
        {
            data.model.FileInfo fileIn = await _fileInfoService.Addfile(file, shops.Id);
            shops.Logo = fileIn;
        }
        await _ishopservice.CreateShop(shops);
        return new(new ShopDTO(shops, _appConfig));
    }

    [Route("/api/v1/shops/{shopid}")]
    [HttpPut]
    public async Task<ResponceDto<ShopDTO>> EditShops([FromForm] ShopDTO shopDto, IFormFile file, Guid shopid)
    {
        if (!role.Equals(Role.Seller) && !role.Equals(Role.Admin))
        {
            throw new AccessDeniedException();
        }
        
        //if (role.Equals(Role.Admin)) Userid = Userid;
        data.model.FileInfo fi=null;
        if (file != null)
        {
            fi = await _fileInfoService.Addfile(file, shopid);
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
            ShopDeliveries = shopDto.Deliveri.Zip(shopDto.MinPrice, (guid, d) => new {k=guid, v=d}).Select(a=>new ShopDelivery(Id, a.k, a.v)).ToList(),
            ShopTypes = shopDto.Types.Select(a=>new ShopTypes(Id, a)).ToList(),
            ShopPayment = shopDto.Payment.Zip(shopDto.Com, (guid, d) => new {k=guid, v=d}).Select(a=>new ShopPayment(Id, a.k, a.v)).ToList()
            
        };
        var shope = await _ishopservice.EditShop(shops, (Guid)Userid, (Role)role);
        return new(new ShopDTO(shope, _appConfig));
    }

    [Route("/api/v1/shops/block/{id}")]
    [HttpGet]
    public async Task<ResponceDto<ShopDTO>> BlockShop(Guid id)
    {
        if (!role.Equals(Role.Admin))
        {
            throw new SystemException("Access denied");
        }
        var blockshop = await _ishopservice.ChangeBlockShop(id, false);
        return new(new ShopDTO(blockshop, _appConfig));
    }

    [Route("/api/v1/shops/unblock/{id}")]
    [HttpGet]
    public async Task<ResponceDto<ShopDTO>> UnblockGetShops(Guid id)
    {
        if (!role.Equals(Role.Admin))
        {
            throw new SystemException("Access denied");
        }
        var unblockshop = await _ishopservice.ChangeBlockShop(id, true);
        return new(new ShopDTO(unblockshop, _appConfig));
    }

    [Route("/api/v1/shops/{id}")]
    [HttpDelete]
    public async Task<ResponceDto<string>> DeleteShop(Guid id)
    {
        _ishopservice.DeleteShop(id);
        return new("Shop ok deleted");
    }

    [Route("/api/v1/shops/{name}/{description}")]
    [HttpGet]
    public async Task<ResponceDto<Page<ShopDTO>>> SearchShops(string name, string description)
    {
        Page<Shop> shop;
        shop = await _ishopservice.GetShopSerch(name, description);
        Page<ShopDTO> result = Page<ShopDTO>.Create(shop, shop.Items.Select(a => new ShopDTO(a, _appConfig)));
        return new(result);
    }
    

}