
using Type = data.model.Type;

namespace Marketplace.DTO;

public class TypeDTO:DictionaryDTO<Type>
{
    public TypeDTO()
    {
        
    }
    public TypeDTO(Type t)
    {
        t.Id = Id;
        t.Name = Name;
    }
}