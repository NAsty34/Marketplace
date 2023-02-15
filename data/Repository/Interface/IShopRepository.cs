using data.model;

namespace data.Repository.Interface;

public interface IShopRepository:IBaseRopository<Shop>
{
    Task<Shop?> GetByInn(string inn);
    Task<Page<Shop>> GetPage(FiltersShops filtersShops);
    Task<Page<Shop>> GetByNameAnddescription(string name, string description);
}