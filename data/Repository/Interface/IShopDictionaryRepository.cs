using data.model;

namespace data.Repository.Interface;

public interface IShopDictionaryRepository<T>
{
    
    public void DeleteAllByShop(Guid shopid);
    public void CreateRange(IEnumerable<T> ids);
    
}