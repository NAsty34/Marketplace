using data.model;

namespace Marketplace.DTO;

public class ShortCategoryDto:DictionaryDto<CategoryEntity>
{
    public ShortCategoryDto()
    {
        
    }
    public ShortCategoryDto(CategoryEntity? t)
    {
        Id = t.Id;
        Name = t.Name;
        Parent = t.Parent?.Id;
    }
    public Guid? Parent { get; set; }
}