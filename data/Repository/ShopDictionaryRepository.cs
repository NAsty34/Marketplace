using data.model;
using data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;

public class ShopDictionaryRepository<T> : IShopDictionaryRepository<T> where T : ShopDictionaryBase
{
    protected DBContext DbContext;
    protected DbSet<T> DbSet;

    public ShopDictionaryRepository(DBContext dbContext)
    {
        DbContext = dbContext;
        DbSet = dbContext.Set<T>();
    }

    

    public async Task DeleteAllByShop(Guid shopid)
    {
        DbSet.RemoveRange(DbSet.Where(a => a.ShopId == shopid));
        await DbContext.SaveChangesAsync();
    }

    public async Task CreateRange(IEnumerable<T> ids)
    {
        await DbSet.AddRangeAsync(ids);
        await DbContext.SaveChangesAsync();
    }

}