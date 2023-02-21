using data.model;
using data.Repository;
using data.Repository.Interface;
using logic.Exceptions;
using Microsoft.Extensions.Configuration;

namespace logic.Service;

public class CategoryService:BaseService<Category>
{
   // private ILogger<CategoryService> _logger;
    private CategoryRepository _categoryRepository;
    private readonly IConfiguration _configuration;
    public CategoryService(IBaseRopository<Category> @base, IConfiguration configuration) : base(@base)
    {
        //_logger = logger;
        _configuration = configuration;
        _categoryRepository = (CategoryRepository)@base;
    }
    public override async Task<Category> Create(Category t)
    {
        //_logger.Log(LogLevel.Information,"=========CategoryService==========");
       
        
        await CheckParent(t);
        await BaseRopository.Create(t);
        await BaseRopository.Save();
        return t;
    }

    public override async Task<Category> Edit(Category t)
    {
        var fromDb = await BaseRopository.GetById(t.Id);
        if (fromDb == null)
        {
            throw new CategoryNotFoundException();
        }
        
        await CheckParent(t);
        
        var children = await _categoryRepository.Children(t.Id);
        if (t.Parent!= null && children.Contains(t.Parent.Id))
        {
            throw new CategoryParentException();
        }
        
        fromDb.Name = t.Name;
        fromDb.Parent = t.Parent;
        await BaseRopository.Save();
        return fromDb;
    }

    private async Task CheckParent(Category t)
    {
        var categoryoptions = new CategoryOptions();
        _configuration.GetSection(CategoryOptions.Category).Bind(categoryoptions);

        if (t.Parent == null) return;

        if (t.Parent.Id.Equals(t.Id))
        {
            throw new CategoryParentCategoryException();
        }

        var parent = await BaseRopository.GetById(t.Parent.Id);

        if (parent == null)
        {
            throw new ParentNotFoundException();
        }

        
        var children = await _categoryRepository.Children(t.Parent.Id);
        var countchild = children.Count();
        //_logger.Log(LogLevel.Information, "=========CategoryService==========" + categoryoptions.MaxLevel);
        
        if (categoryoptions.MaxLevel != null && countchild >= categoryoptions.MaxLevel)
        {
            throw new CategoryMaxLevelException();
        }
        
        t.Parent = parent;

    }
}

