using data.model;
using data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;

public class ProductRepository:BaseRepository<ProductEntity>, IProductRepository
{
    public ProductRepository(MarketplaceContext marketplaceContext) : base(marketplaceContext)
    {
    }

    public async Task<PageEntity<ProductEntity>> GetProducts(FilterProductEntity? filterProductEntity, int? page, int? size)
    {
        
        var allprod =  (IQueryable<ProductEntity>)DbSet;
        
        if (filterProductEntity.Name != null )allprod = allprod.Where(a => a.Name.Contains(filterProductEntity.Name) && a.IsActive);
        if (filterProductEntity.Description != null )allprod = allprod.Where(a => a.Description.Contains(filterProductEntity.Description) && a.IsActive);
        if (filterProductEntity.CategoryName != null) allprod = allprod.Where(a => a.Category.Name.Contains(filterProductEntity.CategoryName) && a.IsActive);
        if (filterProductEntity.Depth != null) allprod = allprod.Where(a => a.Depth.Equals(filterProductEntity.Depth) && a.IsActive);
        
        if (filterProductEntity.MaxWeight != null)
            allprod = allprod.Where(a => a.Weight < (filterProductEntity.MaxWeight) && a.IsActive);

        if (filterProductEntity.MinWeight != null)
            allprod = allprod.Where(a => a.Weight > (filterProductEntity.MinWeight) && a.IsActive);
        
        if (filterProductEntity.Height != null)
            allprod = allprod.Where(a => a.Height.Equals(filterProductEntity.Height) && a.IsActive);
        if (filterProductEntity.Width != null)
            allprod = allprod.Where(a => a.Width.Equals(filterProductEntity.Width) && a.IsActive);

        if (filterProductEntity.Country != null)
            allprod = allprod.Where(a => EF.Functions.Like((string)(object)a.Country, "%" + filterProductEntity.Country + "%" ) && a.IsActive);
        
        return await GetPage(allprod, page, size);
    }
    public async Task<List<ProductEntity>> GetByPartNumber(IEnumerable<int> set)
    {
        return await DbSet.Where(a => set.Contains(a.PartNumber)).ToListAsync();
    }

   public async Task<ProductEntity?> GetByCodEdit(int cod)
    {
        return await DbSet.Where(a=>a.PartNumber == cod).FirstOrDefaultAsync();
    }
  
}