using data.model;
using data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;

public class ShopDictionaryRepository<T> : IShopDictionaryRepository<T> where T : ShopDictionaryBase
{
    protected MarketplaceContext MarketplaceContext;
    protected DbSet<T> DbSet;

    public ShopDictionaryRepository(MarketplaceContext marketplaceContext)
    {
        MarketplaceContext = marketplaceContext;
        DbSet = marketplaceContext.Set<T>();
    }

    

    public async Task DeleteAllByShop(Guid shopid)
    {
        DbSet.RemoveRange(DbSet.Where(a => a.ShopEntityId == shopid));
        await MarketplaceContext.SaveChangesAsync();
    }

    public async Task CreateRange(IEnumerable<T> ids)
    {
        await DbSet.AddRangeAsync(ids);
        await MarketplaceContext.SaveChangesAsync();
    }

}