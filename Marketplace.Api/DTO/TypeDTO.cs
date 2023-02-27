
using data.model;

namespace Marketplace.DTO;

public class TypeDto:DictionaryDto<TypeEntity>
{
    public TypeDto()
    {
        
    }
    public TypeDto(TypeEntity t)
    {
        t.Id = Id;
        t.Name = Name;
        Discription = t.Discription;
    }
    public string Discription { get; set; }
}