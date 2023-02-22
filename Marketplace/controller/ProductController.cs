using data.model;
using logic.Service.Inreface;
using Marketplace.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.controller;

[Authorize (Roles = nameof(data.model.Role.Admin))]

public class ProductController:UserBaseController
{
    private IProductService _productService;
    private IUserServer _userServer;
    private IFileInfoService _fileInfoService;
    private IConfiguration _configuration;
    private ILogger<ProductEntity> _logger;

    public ProductController(IProductService productService, IUserServer userServer, IFileInfoService fileInfoService, IConfiguration configuration, ILogger<ProductEntity> logger)
    {
        _productService = productService;
        _userServer = userServer;
        _fileInfoService = fileInfoService;
        _configuration = configuration;
        _logger = logger;
    }
    [Route("/api/v1/products")]
    [HttpGet]
    public async Task<ResponceDto<Page<ProductDto>>> Products(int? page, int? size)
    {
        var products = await _productService.GetProducts(page, size);
        var result = Page<ProductDto>.Create(products, products.Items.Select(a => new ProductDto(a, _configuration)));
        return new (result);
    }

    [Route("/api/v1/products")]
    [HttpPost]
    public async Task<ResponceDto<ProductDto>> CreateProduct([FromForm] ProductDto productDto, IFormFile photoId)
    {
        var user = await _userServer.GetUser(Userid.Value);
        var product = new ProductEntity()
        {
            Id = new Guid(),
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
        if (photoId != null)
        {
            FileInfoEntity fileIn = await _fileInfoService.Addfile(photoId, user.Id);
            product.PhotoId = fileIn;
        }
        //_logger.Log(LogLevel.Information, "====================" + photoId);
        await _productService.CreateProduct(product);
        return new (new ProductDto(product, _configuration));
    }
    [Route("/api/v1/products/{id}")]
    [HttpPut]
    public async Task<ResponceDto<ProductDto>> EditProduct([FromForm] ProductDto productDto, IFormFile photoId, Guid id)
    {
        FileInfoEntity fi=null;
        if (photoId != null)
        {
            fi = await _fileInfoService.Addfile(photoId, id);
        }

        var product = new ProductEntity()
        {
            CategoryId = productDto.CategoryId,
            Name = productDto.Name,
            Country = productDto.Country,
            Depth = productDto.Depth,
            Width = productDto.Width,
            Weight = productDto.Weight,
            Height = productDto.Height,
            PartNumber = productDto.PartNumber,
            PhotoId = fi,
            Description = productDto.Description
        };
        var updateProduct = await _productService.EditProduct(product);
        return new (new ProductDto(updateProduct, _configuration));
    }
    
    /*[HttpDelete]
    public async Task<ResponceDto<string>> DeletedProduct()
    {
        return new ResponceDto<string>();
    }
    
    [HttpPatch]
    public async Task<ResponceDto<ProductDto>> IsActiveProduct()
    {
        return new ResponceDto<Page<ProductDto>>();
    }*/
}