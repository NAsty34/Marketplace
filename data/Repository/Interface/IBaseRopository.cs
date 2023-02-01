using data.model;
using Microsoft.EntityFrameworkCore;

namespace data.Repository.Interface;

public interface IBaseRopository<T> where T : class
{
    public T? GetById(Guid id);
    public IEnumerable<T> GetByIds(IEnumerable<Guid> ids);
    public Page<T> GetPage(IQueryable<T> _queryable, int page, int size);
    public Page<T> GetPage(int page, int size);
    public void Create(T t);
    public void Create(IEnumerable<T> _t);
    public void Save();
    public void Edit(T t);
    public void Edit(IEnumerable<T> _t);
    public void Delete(T t);
    public void Delete(Guid id);
    public void SetActivite(T t, bool value);
}