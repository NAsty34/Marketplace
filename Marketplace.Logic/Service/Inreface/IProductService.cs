

using data.model;
using Microsoft.AspNetCore.Http;

namespace logic.Service.Inreface;

public interface IProductService
{
    public Task<PageEntity<ProductEntity>> GetProducts(FilterProductEntity filterProductEntity, int? page, int? size);

    public Task<ProductEntity> CreateProduct(ProductEntity productEntity);
    
    public Task<ProductEntity> EditProduct(ProductEntity productEntity);

    public Task<ProductEntity> IsActiveProduct(Guid id, bool value);

    public Task DeletedProduct(Guid id);
    public Task<IEnumerable<ProductEntity>> Upload(IFormFile productFile, Guid userId);
}