using data.model;
using Type = data.model.Type;

namespace logic.Service.Inreface;

public interface IShopService
{
    public Page<Shop> GetShops();
    public Shop? GetShop(Guid id);
    public void DeleteShop(Guid id);
    public void CreateShop(Shop shop);
    public Shop EditShop(Shop shop, Guid userid, Role role);
    public Page<Shop> GetPublicShops();
    public Page<Shop> GetSellerShops(Guid id);
    public Shop ChangeBlockShop(Guid id, bool value);
}