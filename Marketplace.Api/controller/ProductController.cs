using data.model;
using data.Repository;
using data.Repository.Interface;
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

    public ProductController(IProductService productService, IUserServer userServer, IFileInfoService fileInfoService,
        IProductRepository productRepository)
    {
        _productService = productService;
        _userServer = userServer;
        _fileInfoService = fileInfoService;
        _productRepository = productRepository;
    }

    [Route("/api/v1/products")]
    [HttpGet]
    public async Task<ResponceDto<PageEntity<ProductDto>>> Products(FilterProductEntity filterProductEntity, int? page,
        int? size)
    {
        var products = await _productService.GetProducts(filterProductEntity, page, size);
        var result = PageEntity<ProductDto>.Create(products, products.Items.Select(a =>new ProductDto(a)));
        return new(result);
    }

    [Route("/api/v1/products")]
    [HttpPost]
    public async Task<ResponceDto<ProductDto>> CreateProduct([FromForm] ProductDto productDto, List<IFormFile?> photos)
    {
        var set = new HashSet<int>(await _productRepository.GetByCodSet());
        var user = await _userServer.GetUser(Userid.Value);

        var product = new ProductEntity()
        {
            Id = Guid.NewGuid(),
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
        foreach (var photo in photos)
        {
            if (photo != null)
            {
                product.Photo = await _fileInfoService.Addfile(photo,product.Id,user.Id);
                product.UrlPhotos = _fileInfoService.GetUrlProduct(product);
            }
        }
      

        if (set.Contains(productDto.PartNumber))
        {
            throw new SystemException("Продукт с таким кодом уже существует");
        }

        await _productService.CreateProduct(product);
        set.Add(product.PartNumber);

        

        return new(new ProductDto(product));
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
                product.Photo = await _fileInfoService.Addfile(photo,productid,Userid.Value);
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
    public async Task<ResponceDto<PageEntity<ProductDto>>> Upload(IFormFile? productFile)
    {
        if (productFile == null || productFile.Length <= 0) throw new SystemException("Загрузите файл");

        if (!Path.GetExtension(productFile.FileName).Equals(".xls", StringComparison.OrdinalIgnoreCase))
            throw new SystemException("Другое расширение");
        
        var listProduct = await _productService.Upload(productFile);
        
        var set = new HashSet<int>(await _productRepository.GetByCodSet());
        foreach (var product in listProduct)
        {
            product.Creator = await _userServer.GetUser(Userid.Value);


            if (!set.Contains(product.PartNumber))
            {
                await _productRepository.Create(product);
                set.Add(product.PartNumber);
            }

            var fromDb = await _productRepository.GetByCodEdit(product.PartNumber);

            foreach (var prod in fromDb)
            {
                prod.CategoryId = product.CategoryId;
                prod.Name = product.Name;
                prod.Description = product.Description;
                prod.Weight = product.Weight;
                prod.Width = product.Width;
                prod.Height = product.Height;
                prod.Depth = product.Depth;
                prod.Country = product.Country;
                prod.UrlPhotos = product.UrlPhotos;
                await _productRepository.Edit(prod);
            }

            await _productRepository.Save();
        }

        var pageProduct = await _productRepository.GetProducts(0, 0);
        var result = PageEntity<ProductDto>.Create(pageProduct, pageProduct.Items.Select(a => new ProductDto(a)));
        return new(result);
    }
}