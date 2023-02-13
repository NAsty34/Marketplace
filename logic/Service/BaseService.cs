using data.model;
using data.Repository.Interface;
using logic.Service.Inreface;


namespace logic.Service;

public class BaseService<T> : IBaseService<T> where T : DictionaryBase
{
    protected IBaseRopository<T> _baseRopository;

    public BaseService(IBaseRopository<T> _base)
    {
        this._baseRopository = _base;
    }
    public async Task<Page<T>> Page(int page, int size)
    {
        return await _baseRopository.GetPage(page, size);
    }

   

    public virtual async void Create(T t)
    {
         _baseRopository.Create(t);
         _baseRopository.Save();
    }

    public virtual async Task<T> Edit(T t)
    {
        var FromDB = await _baseRopository.GetById(t.Id);
        FromDB.Name = t.Name;
        _baseRopository.Save();
        return FromDB;
    }

    public async void Delete(Guid id)
    {
        _baseRopository.Delete(id);
        _baseRopository.Save();
    }
    
}