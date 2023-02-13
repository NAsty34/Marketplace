using data.model;

namespace data.Repository.Interface;

public interface IShopRepository:IBaseRopository<Shop>
{
    Task<Page<Shop>> GetPublicShops();
    Task<Page<Shop>> GetSellerShops(Guid id);
    Task<Shop?> GetByInn(string inn);
    Task<Page<Shop>> GetPage(int page, int size);

}