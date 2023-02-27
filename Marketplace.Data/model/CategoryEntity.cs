

namespace data.model;

public class CategoryEntity:DictionaryBaseEntity
{
    public virtual CategoryEntity? Parent { get; set; }
    
}