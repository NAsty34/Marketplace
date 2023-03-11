using data.model;
using logic.Exceptions;
using logic.Service.Inreface;
using Microsoft.AspNetCore.Mvc;
using Marketplace.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;


namespace Marketplace.controller;

[Authorize]
public class ShopController : UserBaseController
{
    private IShopService _ishopservice;
    private IUserServer _userServer;
    private IFileInfoService _fileInfoService;
    private FileInfoOptions _options;
    private ILogger<ShopEntity> _logger;


    public ShopController(IShopService ishopservice, IUserServer userServer, IFileInfoService fileInfoService,
        IOptions<FileInfoOptions> options, ILogger<ShopEntity> logger)
    {
        _options = options.Value;
        _ishopservice = ishopservice;
        _userServer = userServer;
        _fileInfoService = fileInfoService;
        _logger = logger;
    }

    [Route("/api/v1/shops")]
    [HttpGet]
    public async Task<ResponceDto<PageEntity<ShopDto>>> Shops(FiltersShopsEntity filtersShopsEntity, int? page,
        int? size)
    {
        /*logger.Log(LogLevel.Information, "============" + filtersShops);*/
        filtersShopsEntity.User = null;
        filtersShopsEntity.IsPublic = null;

        if (Role.Equals(RoleEntity.Buyer))
        {
            filtersShopsEntity.IsPublic = true;
        }
        else
        {
            filtersShopsEntity.User = Userid;
        }

        var shop = await _ishopservice.GetShops(filtersShopsEntity, page, size);
        var result = PageEntity<ShopDto>.Create(shop, shop.Items.Select( a =>
        {
            var shopDto = new ShopDto(a);
            if (a.Logo != null)
            {
                shopDto.Logo = _fileInfoService.GetUrlShop(a);
            }
            return shopDto;
        }));

        return new(result);
    }

    [Route("/api/v1/shops/{id}")]
    [HttpGet]
    public async Task<ResponceDto<ShopDto>> GetShop(Guid id)
    {
        var selshop = await _ishopservice.GetShop(id);
        if (Role.Equals(RoleEntity.Buyer) && !selshop.IsPublic)
        {
            throw new AccessDeniedException();
        }

        if (selshop.CreatorId != Userid && Role.Equals(RoleEntity.Seller))
        {
            throw new AccessDeniedException();
        }

       
        var shop = new ShopDto(selshop);
        shop.Logo = _fileInfoService.GetUrlShop(selshop);
        return new(shop);
    }

    [Route("/api/v1/shops")]
    [HttpPost]
    public async Task<ResponceDto<ShopDto>> CreateShop([FromForm] ShopDto shopDto, IFormFile? file)
    {
        if (!Role.Equals(RoleEntity.Seller) && !Role.Equals(RoleEntity.Admin))
        {
            throw new AccessDeniedException();
        }

        var user = await _userServer.GetUser(Userid.Value);
        Guid id = new Guid();

        //_logger.Log(LogLevel.Information, "==========="+_pathOptions);
        /*logger.Log(LogLevel.Information, "==========="+shopDto.Payment.Zip(shopDto.Com, (guid, d) => new {k=guid, v=d}).First().k);*/
        var shops = new ShopEntity()
        {
            Name = shopDto.Name,
            Description = shopDto.Description,
            IsPublic = shopDto.IsPublic,
            Inn = shopDto.Inn,
            Creator = user,
            Id = id,
            ShopCategory = shopDto.Categories.Select(a => new ShopCategoryEntity(id, a)).ToList(),
            ShopTypes = shopDto.Types.Select(a => new ShopTypesEntity(id, a)).ToList(),
            ShopDeliveries = shopDto.Deliveri.Zip(shopDto.MinPrice, (guid, d) => new { k = guid, v = d })
                .Select(a => new ShopDeliveryEntity(id, a.k, a.v)).ToList(),
            ShopPayment = shopDto.Payment.Zip(shopDto.Com, (guid, d) => new { k = guid, v = d })
                .Select(a => new ShopPaymentEntity(id, a.k, a.v)).ToList(),
        };


        if (file != null)
        {
            FileInfoEntity? fileIn = await _fileInfoService.Addfile(file,shops.Id ,user.Id);
            shops.Logo = fileIn;
        }


        await _ishopservice.CreateShop(shops);

        var shop = new ShopDto(shops);
        if (shops.Logo != null)
        {
            shop.Logo =  _fileInfoService.GetUrlShop(shops);
        }
        
        //shopDto.Logo = $"{_options.BaseUrl}/{_options.RequestPath}/{shops.Creator.Id}/{shops.Logo.Id}{shops.Logo.Extension}";
        //_logger.Log(LogLevel.Information, "==========="+shopDto.Logo);
        return new(shop);
    }

    [Route("/api/v1/shops/{shopid}")]
    [HttpPut]
    public async Task<ResponceDto<ShopDto>> EditShops([FromForm] ShopDto shopDto, IFormFile? file, Guid shopid)
    {
        if (!Role.Equals(RoleEntity.Seller) && !Role.Equals(RoleEntity.Admin))
        {
            throw new AccessDeniedException();
        }
        var user = await _userServer.GetUser(Userid.Value);
        //if (role.Equals(Role.Admin)) Userid = Userid;
        FileInfoEntity? fi = null;
        if (file != null)
        {
            fi = await _fileInfoService.Addfile(file,shopid, user.Id);
        }

       
        Guid id = shopid;

        var shops = new ShopEntity
        {
            Name = shopDto.Name,
            Description = shopDto.Description,
            IsPublic = shopDto.IsPublic,
            Inn = shopDto.Inn,
            Logo = fi,
            Id = id,
            ShopCategory = shopDto.Categories.Select(a => new ShopCategoryEntity(id, a)).ToList(),
            ShopDeliveries = shopDto.Deliveri.Zip(shopDto.MinPrice, (guid, d) => new { k = guid, v = d })
                .Select(a => new ShopDeliveryEntity(id, a.k, a.v)).ToList(),
            ShopTypes = shopDto.Types.Select(a => new ShopTypesEntity(id, a)).ToList(),
            ShopPayment = shopDto.Payment.Zip(shopDto.Com, (guid, d) => new { k = guid, v = d })
                .Select(a => new ShopPaymentEntity(id, a.k, a.v)).ToList()
        };
       

        var shope = await _ishopservice.EditShop(shops, Userid.Value, (RoleEntity)Role);
        var shop = new ShopDto(shope);
        if (shope.Logo != null)
        {
            shop.Logo = _fileInfoService.GetUrlShop(shope);
        }
        return new (shop);
    }

    [Route("/api/v1/shops/{id}/block")]
    [HttpPatch]
    public async Task<ResponceDto<ShopDto>> BlockShop(Guid id)
    {
        if (!Role.Equals(RoleEntity.Admin))
        {
            throw new AccessDeniedException();
        }

        var blockshop = await _ishopservice.ChangeBlockShop(id, false);
        var shop = new ShopDto(blockshop);
        if (blockshop.Logo != null)
        {
            shop.Logo = _fileInfoService.GetUrlShop(blockshop);
        }
        return new (shop);
    }

    [Route("/api/v1/shops/{id}/unblock")]
    [HttpPatch]
    public async Task<ResponceDto<ShopDto>> UnblockGetShops(Guid id)
    {
        if (!Role.Equals(RoleEntity.Admin))
        {
            throw new AccessDeniedException();
        }

        var unblockshop = await _ishopservice.ChangeBlockShop(id, true);
        var shop = new ShopDto(unblockshop);
        if (unblockshop.Logo != null)
        {
            shop.Logo = _fileInfoService.GetUrlShop(unblockshop);
        }
        return new (shop);
    }

    [Route("/api/v1/shops/{id}")]
    [HttpDelete]
    public async Task<ResponceDto<string>> DeleteShop(Guid id)
    {
        await _ishopservice.DeleteShop(id);
        return new("Shop ok deleted");
    }
}