
namespace Marketplace.DTO;

public class DictionaryDto<T>
{
    protected DictionaryDto()
    {
    }
    public DictionaryDto(T t)
    {
        
    }

    public Guid Id { get; set; }
    public string? Name { get; set; }
}