using data.model;
using data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;

public class BaseRepository<T> : IBaseRopository<T> where T:BaseEntity
{

    protected DBContext DbContext;
    protected DbSet<T> DbSet;

    public BaseRepository(DBContext dbContext)
    {
        DbContext = dbContext;
        DbSet = DbContext.Set<T>();
    }

    public async Task<T?> GetById(Guid id)
    {
       return await DbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetByIds(IEnumerable<Guid> ids)
    {
        return DbSet.Where(a => ids.Contains(a.Id));
    }

    public async Task<Page<T>> GetPage(IQueryable<T> queryable, int? page, int? size)
    {
        return queryable.GetPage(page, size);
    }
    
    public async Task<Page<T>> GetPage(int? page, int? size)
    {
        return await GetPage(DbSet, page, size);
    }

    public async Task Create(T t)
    {
         await DbSet.AddAsync(t);
    }

    public async Task Create(IEnumerable<T> t)
    {
         await DbSet.AddRangeAsync(t);
        
    }

    public async Task Save()
    {
        await DbContext.SaveChangesAsync();
    }

    public async Task Edit(T t)
    {
       DbContext.Entry(t).State = EntityState.Modified;
    }
    public async Task Edit(IEnumerable<T> t)
    {
        foreach (var baseEntity in t)
        {
           DbContext.Entry(baseEntity).State = EntityState.Modified;
        }
    }

    public async Task Delete(T t)
    {
        t.IsDeleted = true;
    }
    public async Task Delete(Guid id)
    {
        var shopId = await GetById(id);
        shopId!.IsDeleted = true;
    }
   

    public async Task SetActivite(T t, bool value)
    {
        t.IsActive = value;
    }

    
}