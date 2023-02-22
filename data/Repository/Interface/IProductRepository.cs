using data.model;

namespace data.Repository.Interface;

public interface IProductRepository:IBaseRopository<ProductEntity>
{
    public Task<Page<ProductEntity>> GetProducts(int? page, int? size);
}