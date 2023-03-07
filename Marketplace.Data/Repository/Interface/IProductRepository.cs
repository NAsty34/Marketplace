using data.model;

namespace data.Repository.Interface;

public interface IProductRepository:IBaseRopository<ProductEntity>
{
    public Task<PageEntity<ProductEntity>> GetProducts(FilterProductEntity? filterProductEntity, int? page, int? size);
    public Task<List<int>> GetByCodSet();
    Task<PageEntity<ProductEntity>> GetProducts(int page, int size);
    public Task<List<ProductEntity>> GetByCodEdit(int cod);
}