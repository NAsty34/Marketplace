using data.model;
using data.Repository.Interface;

namespace data.Repository;

public class ProductRepository:BaseRepository<ProductEntity>, IProductRepository
{
    public ProductRepository(DBContext dbContext) : base(dbContext)
    {
    }

    public async Task<Page<ProductEntity>> GetProducts(int? page, int? size)
    {
        var allprod =  DbSet.Where(a => !a.IsDeleted);
        return await GetPage(allprod, page, size);
    }
    
}