using data.model;

namespace logic.Service.Inreface;

public interface IShopService
{
    public Page<Shop> GetShops();
    public Shop? GetShop(int id);
    public void DeleteShop(int id);
    public Task<Shop> CreateShop(Shop shop);
    public Shop EditShop(Shop shop, int userid);
    public Page<Shop> GetPublicShops();
    public Page<Shop> GetSellerShops(int id);
    public Shop ChangeBlockShop(int id, bool value);
}