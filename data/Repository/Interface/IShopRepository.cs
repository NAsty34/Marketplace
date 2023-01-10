using data.model;

namespace data.Repository.Interface;

public interface IShopRepository:IBaseRopository<Shop>
{
    Page<Shop> GetPublicShops();
    Page<Shop> GetSellerShops(int id);
    Shop? GetByInn(string inn);
    Page<Shop> GetPage(int page, int size);

}