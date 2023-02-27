using data.model;
using data.Repository.Interface;

namespace data.Repository;

public class ProductRepository:BaseRepository<ProductEntity>, IProductRepository
{
    public ProductRepository(MarketplaceContext marketplaceContext) : base(marketplaceContext)
    {
    }

    public async Task<PageEntity<ProductEntity>> GetProducts(int? page, int? size)
    {
        var allprod =  DbSet.Where(a => !a.IsDeleted && a.IsActive);
        return await GetPage(allprod, page, size);
    }
    
    
}