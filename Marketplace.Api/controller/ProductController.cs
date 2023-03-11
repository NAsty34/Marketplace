using data.model;
using data.Repository;
using data.Repository.Interface;
using logic.Exceptions;
using logic.Service.Inreface;
using Marketplace.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.controller;

[Authorize(Roles = nameof(RoleEntity.Admin))]
public class ProductController : UserBaseController
{
    private IProductService _productService;
    private IUserServer _userServer;
    private IFileInfoService _fileInfoService;
    private IProductRepository _productRepository;
    private ILogger<ProductEntity> _logger;
    private CategoryRepository _categoryRepository;

    public ProductController(IProductService productService, IUserServer userServer, IFileInfoService fileInfoService,
        IProductRepository productRepository, ILogger<ProductEntity> logger, CategoryRepository categoryRepository)
    {
        _productService = productService;
        _userServer = userServer;
        _fileInfoService = fileInfoService;
        _productRepository = productRepository;
        _logger = logger;
        _categoryRepository = categoryRepository;
    }

    [Route("/api/v1/products")]
    [HttpGet]
    public async Task<ResponceDto<PageEntity<ProductDto>>> Products(FilterProductEntity filterProductEntity, int? page,
        int? size)
    {
        var products = await _productService.GetProducts(filterProductEntity, page, size);
        var result = PageEntity<ProductDto>.Create(products, products.Items.Select(a => new ProductDto(a)));
        return new(result);
    }

    [Route("/api/v1/products")]
    [HttpPost]
    public async Task<ResponceDto<ProductDto>> CreateProduct([FromForm] ProductDto productDto,
        [FromForm] List<IFormFile>? photos)
    {
        var user = await _userServer.GetUser(Userid.Value);
        var category = await _categoryRepository.GetById(productDto.CategoryId);
        if (category == null || !category.IsActive) throw new CategoryNotFoundException();
        var id = Guid.NewGuid();
        var product = new ProductEntity()
        {
            Id = id,
            Name = productDto.Name,
            Description = productDto.Description,
            PartNumber = productDto.PartNumber,
            CategoryId = productDto.CategoryId,
            Category = category,
            Width = productDto.Width,
            Weight = productDto.Weight,
            Country = productDto.Country,
            Depth = productDto.Depth,
            Height = productDto.Height,
            Creator = user,
            UrlPhotos = productDto.UrlPhotos
        };
        
        product.Photo = await _fileInfoService.Addfiles(photos, product.Id, user.Id);
        var dtoprod = await _productService.CreateProduct(product);
        var result = new ProductDto(dtoprod);
        result.UrlPhotos ??= new List<string>();
        result.UrlPhotos.AddRange(_fileInfoService.GetUrlProduct(dtoprod));
        return new(result);
    }


    [Route("/api/v1/products/{productid}")]
    [HttpPut]
    public async Task<ResponceDto<ProductDto>> EditProduct([FromForm] ProductDto productDto, List<IFormFile?> photos,
        Guid productid)
    {
        Guid id = productid;
        var user = await _userServer.GetUser(Userid.Value);
        var product = new ProductEntity
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
            Creator = user,
        };
        FileInfoEntity? fi = null;
        foreach (var photo in photos)
        {
            if (photo != null)
            {
                //product.Photo = await _fileInfoService.Addfile(photo,productid,Userid.Value);
            }
        }

        var editProduct = await _productService.EditProduct(product);

        var newProduct = new ProductDto(editProduct);
        if (editProduct.Photo != null)
        {
            //newProduct.Photo = _fileInfoService.GetUrlProduct(editProduct);
        }

        return new(newProduct);
    }


    [Route("/api/v1/products/{id}")]
    [HttpDelete]
    public async Task<ResponceDto<string>> DeletedProduct(Guid id)
    {
        await _productService.DeletedProduct(id);
        return new("Product ok daleted");
    }

    [Route("/api/v1/products/{id}/unblock")]
    [HttpPatch]
    public async Task<ResponceDto<ProductDto>> IsActiveProduct(Guid id)
    {
        var activeProduct = await _productService.IsActiveProduct(id, true);
        var productDto = new ProductDto(activeProduct);
        if (activeProduct.Photo != null)
        {
            //productDto.Photo = _fileInfoService.GetUrlProduct(activeProduct);
        }

        return new(productDto);
    }

    [Route("/api/v1/products/{id}/block")]
    [HttpPatch]
    public async Task<ResponceDto<ProductDto>> IsNotActiveProduct(Guid id)
    {
        var activeProduct = await _productService.IsActiveProduct(id, false);
        var productDto = new ProductDto(activeProduct);
        if (activeProduct.Photo != null)
        {
            //productDto.Photo = _fileInfoService.GetUrlProduct(activeProduct);
        }

        return new(productDto);
    }

    [Route("/api/v1/products/upload")]
    [HttpPost]
    public async Task<ResponceDto<IEnumerable<ProductDto>>> Upload(IFormFile? productFile)
    {
        if (productFile == null || productFile.Length <= 0) throw new SystemException("Загрузите файл");

        if (!Path.GetExtension(productFile.FileName).Equals(".xls", StringComparison.OrdinalIgnoreCase))
            throw new SystemException("Другое расширение");

        var listProduct = await _productService.Upload(productFile, Userid.Value);
        var result = listProduct.Select(a => new ProductDto(a));
        return new(result);
    }
}