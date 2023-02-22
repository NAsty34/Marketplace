

using data.model;

namespace logic.Service.Inreface;

public interface IProductService
{
    public Task<Page<ProductEntity>> GetProducts(int? page, int? size);

    public Task<ProductEntity> CreateProduct(ProductEntity productEntity);

    public Task<ProductEntity> EditProduct(ProductEntity productEntity);

    /*public Task<ProductEntity> ActiveProduct(Guid id, bool value);

    public Task DeletedProduct(Guid id);*/
}