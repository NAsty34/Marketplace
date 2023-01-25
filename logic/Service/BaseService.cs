using data.model;
using data.Repository.Interface;
using logic.Service.Inreface;

namespace logic.Service;

public abstract class BaseService<T> : IBaseService<T> where T : DictionaryBase
{
    private IBaseRopository<T> _baseRopository;

    public BaseService(IBaseRopository<T> _base)
    {
        this._baseRopository = _base;
    }
    public Page<T> Page(int page, int size)
    {
        return _baseRopository.GetPage(_baseRopository.DbSet(), page, size);
    }

   

    public T Create(T t)
    {
        _baseRopository.Create(t);
        _baseRopository.Save();
        return t;
    }

    public abstract T Edit(T t);

    public void Delete(Guid id)
    {
        _baseRopository.Delete(id);
        _baseRopository.Save();
    }
}