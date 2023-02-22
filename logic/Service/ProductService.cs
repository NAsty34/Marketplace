using data.model;
using data.Repository.Interface;
using logic.Exceptions;
using logic.Service.Inreface;

namespace logic.Service;

public class ProductService:IProductService
{
    private IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Page<ProductEntity>> GetProducts(int? page, int? size)
    {
        return await _productRepository.GetProducts(page, size);
    }
    
    public async Task<ProductEntity> CreateProduct(ProductEntity productEntity)
    {
        await _productRepository.Create(productEntity);
        await _productRepository.Save();
        return productEntity;
    }
    
    public async Task<ProductEntity> EditProduct(ProductEntity productEntity)
    {
        var fromDb = await _productRepository.GetById(productEntity.Id);
        if (fromDb == null) throw new ProductNotFoundException();
        fromDb.Country = productEntity.Country;
        fromDb.Description = productEntity.Description;
        fromDb.Depth = productEntity.Depth;
        fromDb.Height = productEntity.Height;
        fromDb.Name = productEntity.Name;
        fromDb.Width = productEntity.Width;
        fromDb.Weight = productEntity.Weight;
        fromDb.CategoryId = productEntity.CategoryId;
        fromDb.PartNumber = productEntity.PartNumber;
        fromDb.PhotoId = productEntity.PhotoId;
        await _productRepository.Save();
        return fromDb;
    }
    
    /*public async Task<ProductEntity> ActiveProduct(Guid id, bool value)
    {
        
    }
    
    public async Task DeletedProduct(Guid id)
    {
        
    }*/
    
}