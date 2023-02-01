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
        //if (t.parent != null) ParentCategories = new CategoryDto(t.parent);
    }
    public CategoryDto parent { get; set; }
}