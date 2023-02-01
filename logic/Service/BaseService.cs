using data.model;
using data.Repository.Interface;
using logic.Service.Inreface;


namespace logic.Service;

public class BaseService<T> : IBaseService<T> where T : DictionaryBase
{
    private IBaseRopository<T> _baseRopository;

    public BaseService(IBaseRopository<T> _base)
    {
        this._baseRopository = _base;
    }
    public Page<T> Page(int page, int size)
    {
        return _baseRopository.GetPage(page, size);
    }

   

    public void Create(T t)
    {
        _baseRopository.Create(t);
        _baseRopository.Save();
    }

    public T Edit(T t)
    {
        var FromDB = _baseRopository.GetById(t.Id);
        FromDB.Name = t.Name;
        _baseRopository.Save();
        return FromDB;
    }

    public void Delete(Guid id)
    {
        _baseRopository.Delete(id);
        _baseRopository.Save();
    }
}