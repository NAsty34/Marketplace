using data.model;

namespace logic.Service.Inreface;

public interface IBaseService<T> 
{
    public Task<Page<T>> Page(int? page, int? size);
    public Task Create(T t);
    public Task Edit(T t);
    public Task Delete (Guid id);
}