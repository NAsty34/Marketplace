using data.Repository.Interface;
using logic.Service.Inreface;
using Type = data.model.Type;

namespace logic.Service;

public class TypeService:BaseService<Type>,ITypeService 
{
    private readonly IBaseRopository<Type> _repository;
    public TypeService(IBaseRopository<Type> repository) : base(repository)
    {
        this._repository = repository;
    }
    public TypeService(ITypeRepository _base) : base(_base)
    {
        _repository = _base;
    }

    public override Type Edit(Type t)
    {
        var FromDB = _repository.GetById(t.Id);
        FromDB.Name = t.Name;
        _repository.Save();
        return FromDB;
    }
}