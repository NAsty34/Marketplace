using data.Repository.Interface;
using Type = data.model.Type;

namespace logic.Service;

public class TypeService:BaseService<Type>
{
    public TypeService(IBaseRopository<Type> _base) : base(_base)
    {
    }

    public override void Create(Type t)
    {
        if (t.discription.Length > 500)
        {
            throw new SystemException("Количество символом не должно превышать 500");
        }
        _baseRopository.Create(t);
        _baseRopository.Save();
    }
    public override Type Edit(Type t)
    {
        if (t.discription.Length > 500)
        {
            throw new SystemException("Количество символом не должно превышать 500");
        }
        var FromDB = _baseRopository.GetById(t.Id);
        FromDB.discription = t.discription;
        FromDB.Name = t.Name;
        _baseRopository.Save();
        return FromDB;
    }
}