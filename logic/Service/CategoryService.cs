using data.model;
using data.Repository;
using data.Repository.Interface;
using logic.Service.Inreface;

namespace logic.Service;

public class CategoryService:BaseService<Category>, ICategoryService
{
    private readonly IBaseRopository<Category> _repository;

    public CategoryService(IBaseRopository<Category> repository) : base(repository)
    {
        this._repository = repository;
    }

    public CategoryService(ICategoryRepository _base) : base(_base)
    {
        _repository = _base;
    }

    public override Category Edit(Category t)
    {
        var FromDB = _repository.GetById(t.Id);
        FromDB.Name = t.Name;
        _repository.Save();
        return FromDB;
    }
}