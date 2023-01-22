using data.model;
using data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;

public class BaseRepository<T> : IBaseRopository<T> where T : BaseEntity
{

    protected DBContext _dbContext;
    protected DbSet<T> _dbSet;

    public BaseRepository(DBContext _dbContext, DbSet<T> _dbSet)
    {
        this._dbContext = _dbContext;
        this._dbSet = _dbSet;
    }

    public T? GetById(Guid id)
    {
        return _dbSet.Find(id);
    }

    public IEnumerable<T> GetByIds(IEnumerable<Guid> ids)
    {
        return _dbSet.Where(a => ids.Contains(a.Id));
    }

    public Page<T> GetPage(IQueryable<T> _queryable, int page, int size)
    {
        IEnumerable<T> items = _queryable.Skip((page-1) * size).Take(size).ToList();
        Page<T> p = new Page<T>();
        p.Count = items.Count();
        p.CurrentPage = page;
        p.Size = size;
        p.Items = items;
        p.Total = _queryable.Count();
        p.TotalPages = (int)Math.Ceiling(p.Total / (double)size);
        return p;
    }

    public void Create(T t)
    {
        _dbSet.Add(t);
    }

    public void Create(IEnumerable<T> _t)
    {
        _dbSet.AddRange(_t);
        
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }

    public void Edit(T t)
    {
        _dbContext.Entry(t).State = EntityState.Modified;
    }
    public void Edit(IEnumerable<T> _t)
    {
        foreach (var baseEntity in _t)
        {
            _dbContext.Entry(baseEntity).State = EntityState.Modified;
        }
    }

    public void Delete(T t)
    {
        _dbSet.Remove(t);
    }
    public void Delete(Guid id)
    {
        var shopid = GetById(id);
        Delete(shopid);
    }
    public void Delete(IEnumerable<T> _t)
    {
        _dbSet.RemoveRange(_t);
    }
    public void Delete(IEnumerable<Guid> id)
    {
        var shopsid = GetByIds(id);
        Delete(shopsid);
    }

    public void SetActivite(T t, bool value)
    {
        t.IsActive = value;
    }

    public DBContext DbContext()
    {
        return _dbContext;
    }

    public DbSet<T> DbSet()
    {
        return _dbSet;
    }
}