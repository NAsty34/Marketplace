using System.Data;
using Dapper;
using data.model;
using data.Repository;
using data.Repository.Interface;
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
            throw new SystemException("Category not found");
        }
        CheckParent(t);
        
        var children = categoryRepository.Children(t.Id);

        if (children.Contains(t.parent.Id))
        {
            throw new SystemException("Категория не может иметь своих потомков в роли родителя");
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
            throw new SystemException("потомок категории сама категория");
        }
        var parent = _baseRopository.GetById(t.parent.Id);
        if (parent == null)
        {
            throw new SystemException("Parent not found");
        }
        t.parent = parent;
    }
}

