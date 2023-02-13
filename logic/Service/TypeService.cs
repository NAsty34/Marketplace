using data.Repository.Interface;
using logic.Exceptions;
using Type = data.model.Type;

namespace logic.Service;

public class TypeService:BaseService<Type>
{
    public TypeService(IBaseRopository<Type> _base) : base(_base)
    {
    }

    public override async void Create(Type t)
    {
        LengthField(t);
        _baseRopository.Create(t);
        _baseRopository.Save();
    }
    public override async Task<Type> Edit(Type t)
    {
        LengthField(t);
        var FromDB = await _baseRopository.GetById(t.Id);
        FromDB.discription = t.discription;
        FromDB.Name = t.Name;
        _baseRopository.Save();
        return FromDB;
    }

    private static async void LengthField(Type t)
    {
        if (t.discription.Length > 500)
        {
            throw new TypeDiscriptionExtencion();
        }
    }
}