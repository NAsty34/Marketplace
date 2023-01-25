using data.model;

namespace Marketplace.DTO;

public class CategoryDto:DictionaryDTO<Category>
{
    public CategoryDto()
    {
        
    }
    public CategoryDto(Category t)
    {
        Id = t.Id;
        Name = t.Name;
    }
}