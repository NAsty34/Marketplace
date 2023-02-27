using data.model;

namespace data.Repository.Interface;

public interface IBaseRopository<T>
{
    public Task<T?> GetById(Guid id);
    public Task<IEnumerable<T>> GetByIds(IEnumerable<Guid> ids);
    public Task<PageEntity<T>> GetPage(IQueryable<T> queryable, int? page, int? size);
    public Task<PageEntity<T>> GetPage(int? page, int? size);
    public Task Create(T t);
    public Task Create(IEnumerable<T> t);
    public Task Save();
    public Task Edit(T t);
    public Task Edit(IEnumerable<T> t);
    public Task Delete(T t);
    //public Task Delete(Guid id);
    public Task SetActivite(T t, bool value);
}