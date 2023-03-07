using data.model;
using data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;

public class BaseRepository<T> : IBaseRopository<T> where T:BaseEntity
{
    protected readonly MarketplaceContext MarketplaceContext;
    protected readonly DbSet<T> DbSet;
    
    public BaseRepository(MarketplaceContext marketplaceContext)
    {
        MarketplaceContext = marketplaceContext;
        DbSet = MarketplaceContext.Set<T>();
    }

    public async Task<T?> GetById(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetByIds(IEnumerable<Guid> ids)
    {
        return await DbSet.Where(a => ids.Contains(a.Id)).ToListAsync();
    }

    
    public async Task<PageEntity<T>> GetPage(IQueryable<T> queryable, int? page, int? size)
    {
        return queryable.GetPage(page, size);
    }
    
    public async Task<PageEntity<T>> GetPage(int? page, int? size)
    {
        return await GetPage(DbSet.Where(a=>!a.IsDeleted), page, size);
    }

    public async Task Create(T t)
    {
        t.CreateDate = DateTime.Now;
        await DbSet.AddAsync(t);
    }

    public async Task Create(IEnumerable<T> t)
    {
         await DbSet.AddRangeAsync(t);
        
    }

    public async Task Save()
    {
        await MarketplaceContext.SaveChangesAsync();
    }

    public async Task Edit(T t)
    {
       MarketplaceContext.Entry(t).State = EntityState.Modified;
    }
    public async Task Edit(IEnumerable<T> t)
    {
        foreach (var baseEntity in t)
        {
           MarketplaceContext.Entry(baseEntity).State = EntityState.Modified;
        }
    }

    public async Task Delete(T t)
    {
        t.IsDeleted = true;
    }
    public async Task Delete(Guid id)
    {
        var entity = await GetById(id);
        entity.IsDeleted = true;
    }
   

    public async Task SetActivite(T t, bool value)
    {
        t.IsActive = value;
    }

    
}