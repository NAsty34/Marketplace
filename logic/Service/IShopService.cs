using data.model;

namespace logic.Service;

public interface IShopService
{
    public IEnumerable<Shop> GetShops();
    public IEnumerable<Shop> GetShop(int id);
    public IEnumerable<Shop> DeleteShop(int id);
    public Shop CreateShop(string name, string description, string logo, string inn, bool isPublic);
    public Shop EditShop(int id, string name, string description, string logo, string inn, bool isPublic);
    public IEnumerable<Shop> GetPublicShops();
    public IEnumerable<Shop> GetSellerShops(string email);
    public void BlockShops(string email);
    
}