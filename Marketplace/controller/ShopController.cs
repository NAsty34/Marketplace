using data.model;
using logic.Exceptions;
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

    public ShopController(IShopService ishopservice, IUserServer userServer, IFileInfoService fileInfoService, IConfiguration appConfig)
    {
        _ishopservice = ishopservice;
        _userServer = userServer;
        _fileInfoService = fileInfoService;
        _appConfig = appConfig;
    }

    [Route("/api/v1/shops")]
    [HttpGet]
    public async Task<ResponceDto<Page<ShopDto>>> Shops(FiltersShops filtersShops)
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
        Page<ShopDto> result = Page<ShopDto>.Create(shop, shop.Items.Select(a => new ShopDto(a, _appConfig)));
        return new(result);
    }

    [Route("/api/v1/shops/{id}")]
    [HttpGet]
    public async Task<ResponceDto<ShopDto>> GetShop(Guid id)
    {
        var selshop = await _ishopservice.GetShop(id);
        if (role.Equals(Role.Buyer) && !selshop.IsPublic)
        {

            throw new AccessDeniedException();
        }
        
        if (selshop.CreatorId != Userid && role.Equals(Role.Seller))
        {
            throw new AccessDeniedException();
        }
        return new(new ShopDto(selshop, _appConfig));
    }

    [Route("/api/v1/shops")]
    [HttpPost]
    public async Task<ResponceDto<ShopDto>> CreateShop([FromForm] ShopDto shopDto, IFormFile file)
    {
        
        if (!role.Equals(Role.Seller) && !role.Equals(Role.Admin))
        {
            throw new AccessDeniedException();
        }
        
        var user = await _userServer.GetUser(Userid.Value);
        Guid id = new Guid();
        /*logger.Log(LogLevel.Information, "==========="+shopDto.Payment.First());
        logger.Log(LogLevel.Information, "==========="+shopDto.Com.First());
        logger.Log(LogLevel.Information, "==========="+shopDto.Payment.Zip(shopDto.Com, (guid, d) => new {k=guid, v=d}).First().k);*/
        var shops = new Shop()
        {
            Name = shopDto.Name,
            Description = shopDto.Description,
            IsPublic = shopDto.IsPublic,
            Inn = shopDto.Inn,
            Creator = user,
            Id = id,
            ShopCategory = shopDto.Categories.Select(a=>new ShopCategory(id, a)).ToList(),
            ShopTypes = shopDto.Types.Select(a=>new ShopTypes(id, a)).ToList(),
            ShopDeliveries = shopDto.Deliveri.Zip(shopDto.MinPrice, (guid, d) => new {k=guid, v=d}).Select(a=>new ShopDelivery(id, a.k, a.v)).ToList(),
            ShopPayment = shopDto.Payment.Zip(shopDto.Com, (guid, d) => new {k=guid, v=d}).Select(a=>new ShopPayment(id, a.k, a.v)).ToList(),
        };

        
        if (file != null)
        {
            data.model.FileInfo fileIn = await _fileInfoService.Addfile(file, user.Id);
            
            shops.Logo = fileIn;
        }
        await _ishopservice.CreateShop(shops);
        return new(new ShopDto(shops, _appConfig));
    }

    [Route("/api/v1/shops/{shopid}")]
    [HttpPut]
    public async Task<ResponceDto<ShopDto>> EditShops([FromForm] ShopDto shopDto, IFormFile file, Guid shopid)
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
        Guid id = shopid;
        
        var shops = new Shop()
        {
            Name = shopDto.Name,
            Description = shopDto.Description,
            IsPublic = shopDto.IsPublic,
            Inn = shopDto.Inn,
            Logo = fi,
            Id = id,
            ShopCategory = shopDto.Categories.Select(a=>new ShopCategory(id, a)).ToList(),
            ShopDeliveries = shopDto.Deliveri.Zip(shopDto.MinPrice, (guid, d) => new {k=guid, v=d}).Select(a=>new ShopDelivery(id, a.k, a.v)).ToList(),
            ShopTypes = shopDto.Types.Select(a=>new ShopTypes(id, a)).ToList(),
            ShopPayment = shopDto.Payment.Zip(shopDto.Com, (guid, d) => new {k=guid, v=d}).Select(a=>new ShopPayment(id, a.k, a.v)).ToList()
            
        };
        var shope = await _ishopservice.EditShop(shops, Userid.Value, (Role)role);
        return new(new ShopDto(shope, _appConfig));
    }

    [Route("/api/v1/shops/block/{id}")]
    [HttpGet]
    public async Task<ResponceDto<ShopDto>> BlockShop(Guid id)
    {
        if (!role.Equals(Role.Admin))
        {
            throw new AccessDeniedException();
        }
        var blockshop = await _ishopservice.ChangeBlockShop(id, false);
        return new(new ShopDto(blockshop, _appConfig));
    }

    [Route("/api/v1/shops/unblock/{id}")]
    [HttpGet]
    public async Task<ResponceDto<ShopDto>> UnblockGetShops(Guid id)
    {
        if (!role.Equals(Role.Admin))
        {
            throw new AccessDeniedException();
        }
        var unblockshop = await _ishopservice.ChangeBlockShop(id, true);
        return new(new ShopDto(unblockshop, _appConfig));
    }

    [Route("/api/v1/shops/{id}")]
    [HttpDelete]
    public async Task<ResponceDto<string>> DeleteShop(Guid id)
    {
        await _ishopservice.DeleteShop(id);
        return new("Shop ok deleted");
    }

}