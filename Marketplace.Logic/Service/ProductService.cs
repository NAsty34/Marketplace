using data.model;
using data.Repository;
using data.Repository.Interface;
using logic.Exceptions;
using logic.Service.Inreface;

namespace logic.Service;

public class ProductService:IProductService
{
    private IProductRepository _productRepository;
    private IBaseRopository<CategoryEntity> _categoryRepository;

    public ProductService(IProductRepository productRepository, IBaseRopository<CategoryEntity> categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<PageEntity<ProductEntity>> GetProducts(FilterProductEntity filterProductEntity, int? page, int? size)
    {
        return await _productRepository.GetProducts(filterProductEntity, page, size);
    }
    
    public async Task<ProductEntity> CreateProduct(ProductEntity productEntity)
    {
        var category = await _categoryRepository.GetById(productEntity.CategoryId);
        if (category == null || !category.IsActive) throw new CategoryNotFoundException();
        
        await _productRepository.Create(productEntity);
        await _productRepository.Save();
        return productEntity;
    }
    
    public async Task<ProductEntity> EditProduct(ProductEntity productEntity)
    {
        var fromDb = await _productRepository.GetById(productEntity.Id);
        if (fromDb == null || !fromDb.IsActive)
        {
            throw new ProductNotFoundException();
        }
        
        var category = await _categoryRepository.GetById(productEntity.CategoryId);
        if (category == null || !category.IsActive) throw new CategoryNotFoundException();

        fromDb.Country = productEntity.Country;
        fromDb.Description = productEntity.Description;
        fromDb.Depth = productEntity.Depth;
        fromDb.Height = productEntity.Height;
        fromDb.Name = productEntity.Name;
        fromDb.Width = productEntity.Width;
        fromDb.Weight = productEntity.Weight;
        fromDb.CategoryId = productEntity.CategoryId;
        fromDb.PartNumber = productEntity.PartNumber;
        fromDb.Photo = productEntity.Photo;
        
        await _productRepository.Save();
        return fromDb;
    }
    
    public async Task<ProductEntity> IsActiveProduct(Guid id, bool value)
    {
        var product = await _productRepository.GetById(id);
        if (product is null)
        {
            throw new ProductNotFoundException();
        }

        product.IsActive = value;
        await _productRepository.Save();
        return product;
    }
    
    public async Task DeletedProduct(Guid id)
    {
        var product = await _productRepository.GetById(id);
        if (product == null || !product.IsActive) throw new ProductNotFoundException();
        await _productRepository.Delete(product);
        await _productRepository.Save();
    }
    
}