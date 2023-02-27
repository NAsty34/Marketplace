using data.model;

namespace Marketplace.DTO;

public class CategoryDto:DictionaryDto<CategoryEntity>
{
    public CategoryDto()
    {
        
    }
    public CategoryDto(CategoryEntity t)
    {
        Id = t.Id;
        Name = t.Name;
        Parent = new CategoryDto(t.Parent);
    }
    public CategoryDto? Parent { get; set; }
}