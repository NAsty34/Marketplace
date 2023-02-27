using data.model;
using data.Repository.Interface;
using logic.Service.Inreface;


namespace logic.Service;

public class BaseService<T> : IBaseService<T> where T : DictionaryBaseEntity
{
    protected IBaseRopository<T> BaseRopository;

    public BaseService(IBaseRopository<T> baseRopository)
    {
        BaseRopository = baseRopository;
    }

    
    public async Task<PageEntity<T>> Page(int? page, int? size)
    {
        return await BaseRopository.GetPage(page, size);
    }


    public virtual async Task Create(T t)
    {
         t.CreateDate = DateTime.Now;
          
         await BaseRopository.Create(t);
         await BaseRopository.Save();
    }

    public virtual async Task Edit(T t)
    {
        var fromDb = await BaseRopository.GetById(t.Id);
        if (fromDb != null)
        {
            fromDb.Name = t.Name;
            
        }
        await BaseRopository.Save();
    }

    public async Task Delete(Guid id)
    {
        var entity = await BaseRopository.GetById(id);
        if (entity == null) throw new SystemException("Not Found");
        await BaseRopository.Delete(entity);
        
        await BaseRopository.Save();
    }
    
}