using data.model;
using data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;

public class BaseRepository<T> : IBaseRopository<T> where T:BaseEntity
{

    protected DBContext _dbContext;
    protected DbSet<T> _dbSet;

    public BaseRepository(DBContext _dbContext)
    {
        this._dbContext = _dbContext;
        this._dbSet = this._dbContext.Set<T>();
    }

    public async Task<T?> GetById(Guid id)
    {
        return _dbSet.Find(id);
    }

    public async Task<IEnumerable<T>> GetByIds(IEnumerable<Guid> ids)
    {
        return _dbSet.Where(a => ids.Contains(a.Id));
    }

    public async Task<Page<T>> GetPage(IQueryable<T> queryable, int page, int size)
    {
        return queryable.GetPage(page, size);
    }
    
    public async Task<Page<T>> GetPage(int page, int size)
    {
        return await GetPage(_dbSet, page, size);
    }

    public async void Create(T t)
    {
        _dbSet.Add(t);
    }

    public async void Create(IEnumerable<T> _t)
    {
         _dbSet.AddRange(_t);
        
    }

    public async void Save()
    {
        _dbContext.SaveChanges();
    }

    public async void Edit(T t)
    {
       _dbContext.Entry(t).State = EntityState.Modified;
    }
    public async void Edit(IEnumerable<T> _t)
    {
        foreach (var baseEntity in _t)
        {
            _dbContext.Entry(baseEntity).State = EntityState.Modified;
        }
    }

    public async void Delete(T t)
    {
        t.IsDeleted = true;
    }
    public async void Delete(Guid id)
    {
        var shopid = await GetById(id);
        shopid.IsDeleted = true;
    }
   

    public async void SetActivite(T t, bool value)
    {
        t.IsActive = value;
    }

    
}