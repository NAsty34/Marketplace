using data.model;

namespace data.Repository.Interface;

public interface IProductRepository:IBaseRopository<ProductEntity>
{
    public Task<PageEntity<ProductEntity>> GetProducts(FilterProductEntity filterProductEntity, int? page, int? size);
   
}