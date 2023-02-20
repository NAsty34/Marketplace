using data.model;

namespace Marketplace.DTO;

public class CategoryDto:DictionaryDto<Category>
{
    public CategoryDto()
    {
        
    }
    public CategoryDto(Category t)
    {
        Id = t.Id;
        Name = t.Name;
    }
    public CategoryDto? Parent { get; set; }
}