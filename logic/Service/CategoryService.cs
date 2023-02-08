using System.Data;
using Dapper;
using data.model;
using data.Repository;
using data.Repository.Interface;
using logic.Exceptions;
using Microsoft.Extensions.Logging;

namespace logic.Service;

public class CategoryService:BaseService<Category>
{
    private ILogger<CategoryService> logger;
    private CategoryRepository categoryRepository;
    public CategoryService(ILogger<CategoryService> logger, IBaseRopository<Category> _base) : base(_base)
    {
        this.logger = logger;
        this.categoryRepository = (CategoryRepository?)_base;
    }
    public override void Create(Category t)
    {
        //logger.Log(LogLevel.Information,"=========CategoryService==========");
        CheckParent(t);
        _baseRopository.Create(t);
        _baseRopository.Save();
    }

    public override Category Edit(Category t)
    {
        var FromDB = _baseRopository.GetById(t.Id);
        if (FromDB == null)
        {
            throw new CategoryNotFoundException();
        }
        CheckParent(t);
        
        var children = categoryRepository.Children(t.Id);

        if (children.Contains(t.parent.Id))
        {
            throw new CategoryParentException();
        }
        
        
        FromDB.Name = t.Name;
        FromDB.parent = t.parent;
        _baseRopository.Save();
        return FromDB;
    }

    private void CheckParent(Category t)
    {
        if (t.parent == null) return;
        if (t.parent.Id.Equals(t.Id))
        {
            throw new CategoryParentCategoryException();
        }
        var parent = _baseRopository.GetById(t.parent.Id);
        if (parent == null)
        {
            throw new ParentNotFoundException();
        }
        t.parent = parent;
    }
}

