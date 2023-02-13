using data.model;
using Type = data.model.Type;

namespace logic.Service.Inreface;

public interface IShopService
{
    public Task<Page<Shop>> GetShops();
    public Task<Shop> GetShop(Guid id);
    public void DeleteShop(Guid id);
    public void CreateShop(Shop shop);
    public Task<Shop> EditShop(Shop shop, Guid userid, Role role);
    public Task<Page<Shop>> GetPublicShops();
    public Task<Page<Shop>> GetSellerShops(Guid id);
    public Task<Shop> ChangeBlockShop(Guid id, bool value);
}