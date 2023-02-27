using data.model;
using logic.Service.Inreface;
using Marketplace.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.controller;

[Authorize (Roles = nameof(RoleEntity.Admin))]

public class ProductController:UserBaseController
{
    private IProductService _productService;
    private IUserServer _userServer;
    private IFileInfoService _fileInfoService;

    public ProductController(IProductService productService, IUserServer userServer, IFileInfoService fileInfoService)
    {
        _productService = productService;
        _userServer = userServer;
        _fileInfoService = fileInfoService;
    }
    [Route("/api/v1/products")]
    [HttpGet]
    public async Task<ResponceDto<PageEntity<ProductDto>>> Products(int? page, int? size)
    {
        var products = await _productService.GetProducts(page, size);
        var result = PageEntity<ProductDto>.Create(products, products.Items.Select(a =>
        {
            var pageProduct = new ProductDto(a);
            if (a.Photo != null)
            {
                pageProduct.Photo =  _fileInfoService.GetUrlProduct(a);
            }

            return pageProduct;
        }));
        return new (result);
    }

    [Route("/api/v1/products")]
    [HttpPost]
    public async Task<ResponceDto<ProductDto>> CreateProduct([FromForm] ProductDto productDto, IFormFile? photo)
    {
        var user = await _userServer.GetUser(Userid.Value);
        Guid id = new Guid();
        var product = new ProductEntity()
        {
            Id = id,
            Name = productDto.Name,
            Description = productDto.Description,
            PartNumber = productDto.PartNumber,
            CategoryId = productDto.CategoryId,
            Width = productDto.Width,
            Weight = productDto.Weight,
            Country = productDto.Country,
            Depth = productDto.Depth,
            Height = productDto.Height,
            Creator = user
        };
        if (photo != null)
        {
            FileInfoEntity? fileIn = await _fileInfoService.Addfile(photo, user.Id);
            product.Photo = fileIn;
        }
        //_logger.Log(LogLevel.Information, "====================" + photoId);
        await _productService.CreateProduct(product);
        
        var newProduct = new ProductDto(product);
        if (product.Photo != null)
        {
            newProduct.Photo =  _fileInfoService.GetUrlProduct(product);
        }
        return new (newProduct);
    }
    [Route("/api/v1/products/{id}")]
    [HttpPut]
    public async Task<ResponceDto<ProductDto>> EditProduct([FromForm] ProductDto productDto, IFormFile? photo, Guid id)
    {
        FileInfoEntity? fi=null;
        if (photo != null)
        {
            fi = await _fileInfoService.Addfile(photo, Userid.Value);
        }

        Guid Id = id;
        var user = await _userServer.GetUser(Userid.Value);
        var product = new ProductEntity()
        {
            Id = Id,
            CategoryId = productDto.CategoryId,
            Name = productDto.Name,
            Country = productDto.Country,
            Depth = productDto.Depth,
            Width = productDto.Width,
            Weight = productDto.Weight,
            Height = productDto.Height,
            PartNumber = productDto.PartNumber,
            Photo = fi,
            Description = productDto.Description,
            Creator = user
        };
        
        var editProduct = await _productService.EditProduct(product);
        
        var newProduct = new ProductDto(editProduct);
        if (editProduct.Photo != null)
        {
            newProduct.Photo =  _fileInfoService.GetUrlProduct(editProduct);
        }
        return new (newProduct);
    }
    
    
    
    [Route("/api/v1/products/{id}")]
    [HttpDelete]
    public async Task<ResponceDto<string>> DeletedProduct(Guid id)
    {
        await _productService.DeletedProduct(id);
        return new ("Product ok daleted");
    }
    
    [Route("/api/v1/products/{id}/unblock")]
    [HttpPatch]
    public async Task<ResponceDto<ProductDto>> IsActiveProduct(Guid id)
    {
        var activeProduct = await _productService.IsActiveProduct(id, true);
        var productDto = new ProductDto(activeProduct);
        if (activeProduct.Photo != null)
        {
            productDto.Photo = _fileInfoService.GetUrlProduct(activeProduct);
        }
        return new (productDto);
    }
    [Route("/api/v1/products/{id}/block")]
    [HttpPatch]
    public async Task<ResponceDto<ProductDto>> IsNotActiveProduct(Guid id)
    {
        var activeProduct = await _productService.IsActiveProduct(id, false);
        var productDto = new ProductDto(activeProduct);
        if (activeProduct.Photo != null)
        {
            productDto.Photo = _fileInfoService.GetUrlProduct(activeProduct);
        }
        return new (productDto);
    }
}