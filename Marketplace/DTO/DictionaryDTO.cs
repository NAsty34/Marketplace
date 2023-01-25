using data.model;

namespace Marketplace.DTO;

public class DictionaryDTO<T>
{
    protected DictionaryDTO()
    {
    }
    public DictionaryDTO(T t)
    {
        
    }

    

    public Guid Id { get; set; }
    public string Name { get; set; }
}