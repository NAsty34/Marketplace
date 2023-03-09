using data.model;

namespace data.Repository.Interface;

public interface IProductRepository:IBaseRopository<ProductEntity>
{
    public Task<PageEntity<ProductEntity>> GetProducts(FilterProductEntity? filterProductEntity, int? page, int? size);
    public Task<List<ProductEntity>> GetByPartNumber(IEnumerable<int> set);
    public Task<ProductEntity?> GetByCodEdit(int cod);
}