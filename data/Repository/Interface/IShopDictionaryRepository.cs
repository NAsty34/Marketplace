using data.model;

namespace data.Repository.Interface;

public interface IShopDictionaryRepository<T>:IBaseRopository<T> where T:ShopDictionaryBase 
{
    public void DeleteAllByShop(Guid shopid);
    public List<T> CreateRange(List<T> ids);
}