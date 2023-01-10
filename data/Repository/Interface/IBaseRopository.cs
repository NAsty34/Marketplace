using data.model;
using Microsoft.EntityFrameworkCore;

namespace data.Repository.Interface;

public interface IBaseRopository<T> where T : class
{
    public T? GetById(int id);
    public IEnumerable<T> GetByIds(IEnumerable<int> ids);
    public Page<T> GetPage(IQueryable<T> _queryable, int page, int size);
    public void Create(T t);
    public void Create(IEnumerable<T> _t);
    public void Save();
    public void Edit(T t);
    public void Edit(IEnumerable<T> _t);
    public void Delete(T t);
    public void Delete(int id);
    public void Delete(IEnumerable<T> _t);
    public void Delete(IEnumerable<int> id);
    public void SetActivite(T t, bool value);
    public  DBContext DbContext();
    public DbSet<T> DbSet();
}