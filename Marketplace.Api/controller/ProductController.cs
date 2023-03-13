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
    private readonly IProductService _productService;
    private readonly IUserServer _userServer;
    private readonly IFileInfoService _fileInfoService;
    private readonly IProductRepository _productRepository;
    private readonly CategoryRepository _categoryRepository;

    public ProductController(IProductService productService, IUserServer userServer, IFileInfoService fileInfoService,
        IProductRepository productRepository, CategoryRepository categoryRepository)
    {
        _productService = productService;
        _userServer = userServer;
        _fileInfoService = fileInfoService;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    [Route("/api/v1/products")]
    [HttpGet]
    public async Task<ResponceDto<PageEntity<ProductDto>>> Products(FilterProductEntity filterProductEntity, int? page,
        int? size)
    {
        var products = await _productService.GetProducts(filterProductEntity, page, size);
        var result = PageEntity<ProductDto>.Create(products, products.Items.Select(a =>
        {
            var pageDto = new ProductDto(a);
            pageDto.UrlPhotos ??= new List<string>();
            pageDto.UrlPhotos.AddRange(_fileInfoService.GetUrlProduct(a));
            return pageDto;
        }));
        return new(result);
    }

    [Route("/api/v1/products")]
    [HttpPost]
    public async Task<ResponceDto<ProductDto>> CreateProduct([FromForm] ProductDto productDto,
        [FromForm] List<IFormFile>? photos)
    {
        var user = await _userServer.GetUser(Userid!.Value);
        var category = await _categoryRepository.GetById(productDto.CategoryId);
        if (category == null || !category.IsActive) throw new CategoryNotFoundException();
        
        var product = new ProductEntity()
        {
            Id = Guid.NewGuid(),
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
            UrlPhotos = productDto.UrlPhotos,
        };
        
        var dtoProd = await _productService.CreateProduct(product);
        if (photos != null) dtoProd.Photo = await _fileInfoService.Addfiles(photos, dtoProd.Id, user.Id);

        dtoProd.PhotoId ??= new List<Guid>();
        if (dtoProd.Photo != null) dtoProd.PhotoId.AddRange(_fileInfoService.GetFilesId(dtoProd.Photo));
        await _productRepository.Save();
        
        var result = new ProductDto(dtoProd);
        result.UrlPhotos ??= new List<string>();
        result.UrlPhotos.AddRange(_fileInfoService.GetUrlProduct(dtoProd));
        return new(result);
    }


    [Route("/api/v1/products/{prodId}")]
    [HttpPut]
    public async Task<ResponceDto<ProductDto>> EditProduct([FromForm] ProductDto productDto, [FromForm]List<IFormFile>? photos,
        Guid prodId)
    {
        
        var user = await _userServer.GetUser(Userid!.Value);
        var product = new ProductEntity
        {
            Id = prodId,
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
            UrlPhotos = productDto.UrlPhotos
        };
        var editProduct = await _productService.EditProduct(product);
        if (photos != null) editProduct.Photo = await _fileInfoService.Addfiles(photos, prodId, user.Id);

        editProduct.PhotoId ??= new List<Guid>();
        if (editProduct.Photo != null) editProduct.PhotoId.AddRange(_fileInfoService.GetFilesId(editProduct.Photo));

        await _productRepository.Save();
        
        var result = new ProductDto(editProduct);
        result.UrlPhotos ??= new List<string>();
        result.UrlPhotos.AddRange(_fileInfoService.GetUrlProduct(editProduct));
        return new(result);

    }


    [Route("/api/v1/products/{id}")]
    [HttpDelete]
    public async Task<ResponceDto<string>> DeletedProduct(Guid id)
    {
        await _productService.DeletedProduct(id);
        return new("Product ok deleted");
    }

    [Route("/api/v1/products/{id}/unblock")]
    [HttpPatch]
    public async Task<ResponceDto<ProductDto>> IsActiveProduct(Guid id)
    {
        var activeProduct = await _productService.IsActiveProduct(id, true);
        var productDto = new ProductDto(activeProduct);
        if (activeProduct.Photo != null)
        {
            productDto.UrlPhotos = _fileInfoService.GetUrlProduct(activeProduct);
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
            productDto.UrlPhotos = _fileInfoService.GetUrlProduct(activeProduct);
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

        var listProduct = await _productService.Upload(productFile, Userid!.Value);
        var result = listProduct.Select(a => new ProductDto(a));
        return new(result);
    }
}