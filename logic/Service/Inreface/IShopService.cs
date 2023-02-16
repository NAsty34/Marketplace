using data.model;
using Type = data.model.Type;

namespace logic.Service.Inreface;

public interface IShopService
{
    public Task<Page<Shop>> GetShops(FiltersShops filtersShops);
    public Task<Shop> GetShop(Guid id);
    public void DeleteShop(Guid id);
    public Task<Shop> CreateShop(Shop shop);
    public Task<Shop> EditShop(Shop shop, Guid userid, Role role);
    public Task<Shop> ChangeBlockShop(Guid id, bool value);
    
}