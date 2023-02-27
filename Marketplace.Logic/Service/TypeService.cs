using data.model;
using data.Repository.Interface;
using logic.Exceptions;

namespace logic.Service;

public class TypeService:BaseService<TypeEntity>
{
    public TypeService(IBaseRopository<TypeEntity> @base) : base(@base)
    {
    }

    public override async Task Create(TypeEntity t)
    {
        if (t.Discription.Length > 500)
        {
            throw new TypeDiscriptionExtencion();
        }
        await BaseRopository.Create(t);
        await BaseRopository.Save();
    }
    public override async Task<TypeEntity> Edit(TypeEntity t)
    {
        if (t.Discription.Length > 500)
        {
            throw new TypeDiscriptionExtencion();
        }
        var fromDb = await BaseRopository.GetById(t.Id);
        if (fromDb == null)
        {
            throw new TypeNotFoundException();
        }
        fromDb.Discription = t.Discription;
        fromDb.Name = t.Name; 
        await BaseRopository.Save();
        return fromDb;
    }

    
}