using data.model;

namespace data.Repository.Interface;

public interface IShopRepository:IBaseRopository<ShopEntity>
{
    Task<ShopEntity?> GetByInn(string inn);
    Task<PageEntity<ShopEntity>> GetPage(FiltersShopsEntity filtersShopsEntity, int? page, int? size);
}