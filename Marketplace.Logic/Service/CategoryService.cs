using data.model;
using data.Repository;
using data.Repository.Interface;
using logic.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace logic.Service;

public class CategoryService:BaseService<CategoryEntity>
{
    private ILogger<CategoryService> _logger;
    private CategoryRepository _categoryRepository;
    private CategoryOptions _options;
    public CategoryService(IBaseRopository<CategoryEntity> @base, IOptions<CategoryOptions> options, ILogger<CategoryService> logger) : base(@base)
    {
        _options = options.Value;
        _logger = logger;
        _categoryRepository = (CategoryRepository)@base;
        //_logger.Log(LogLevel.Information, "=========CategoryService==========" + _options.MaxLevel);
    }
    public override async Task Create(CategoryEntity t)
    {
        //_logger.Log(LogLevel.Information,"=========CategoryService==========");
       
        
        await CheckParent(t);
        await BaseRopository.Create(t);
        await BaseRopository.Save();
    }

    public override async Task<CategoryEntity> Edit(CategoryEntity t)
    {
        var fromDb = await BaseRopository.GetById(t.Id);
        if (fromDb == null)
        {
            throw new CategoryNotFoundException();
        }
        
        await CheckParent(t);
        
        var children = await _categoryRepository.Children(t.Id);
        var count = children.Count();
        _logger.Log(LogLevel.Information, "=========CategoryService==========" + count);
        if (t.Parent!= null && children.Contains(t.Parent.Id))
        {
            throw new CategoryParentException();
        }
        
        fromDb.Name = t.Name;
        fromDb.Parent = t.Parent;
        await BaseRopository.Save();
        return fromDb;
    }

    public async Task CheckParent(CategoryEntity t)
    {
        if (t.Parent == null) return;

        if (t.Parent.Id.Equals(t.Id))
        {
            throw new CategoryParentCategoryException();
        }

        var parent = await BaseRopository.GetById(t.Parent.Id);

        if (parent == null || parent.IsDeleted)
        {
            throw new ParentNotFoundException();
        }

        
        var children = await _categoryRepository.Parent(t.Parent.Id);
        var countchild = children.Count();
        _logger.Log(LogLevel.Information, "=========CategoryService==========" + countchild + "======" + _options.MaxLevel);
        
        if (_options.MaxLevel != null && countchild > _options.MaxLevel)
        {
            throw new CategoryMaxLevelException();
        }
        
        t.Parent = parent;

    }
}

