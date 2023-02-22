using data.model;

namespace logic.Service.Inreface;

public interface IShopService
{
    public Task<Page<Shop>> GetShops(FiltersShops filtersShops, int? page, int? size);
    public Task<Shop> GetShop(Guid id);
    public Task DeleteShop(Guid id);
    public Task<Shop> CreateShop(Shop shop);
    public Task<Shop> EditShop(Shop shop, Guid userid, Role role);
    public Task<Shop> ChangeBlockShop(Guid id, bool value);
    
}