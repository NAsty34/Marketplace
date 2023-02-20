using data.model;
using data.Repository.Interface;
using logic.Service.Inreface;


namespace logic.Service;

public class BaseService<T> : IBaseService<T> where T : DictionaryBase
{
    protected IBaseRopository<T> BaseRopository;

    public BaseService(IBaseRopository<T> baseRopository)
    {
        BaseRopository = baseRopository;
    }
    public async Task<Page<T>> Page(int page, int size)
    {
        return await BaseRopository.GetPage(page, size);
    }


    public virtual async Task Create(T t)
    {
         t.CreateDate = DateTime.Now;
          
         await BaseRopository.Create(t);
         await BaseRopository.Save();
    }

    public virtual async Task<T> Edit(T t)
    {
        var fromDb = await BaseRopository.GetById(t.Id);
        fromDb.Name = t.Name;
        await BaseRopository.Save();
        return fromDb;
    }

    public async Task Delete(Guid id)
    {
        BaseRopository.Delete(id);
        await BaseRopository.Save();
    }
    
}