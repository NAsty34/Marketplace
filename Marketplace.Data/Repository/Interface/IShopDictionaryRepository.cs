

namespace data.Repository.Interface;

public interface IShopDictionaryRepository<T>
{
    
    public Task DeleteAllByShop(Guid shopid);
    public Task CreateRange(IEnumerable<T> ids);
    
}