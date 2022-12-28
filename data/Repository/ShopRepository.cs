using data.model;
namespace data.Repository;

public class ShopRepository:IShopRepository
{
    public IEnumerable<Shop> GetShops()
    {
        return DBContext.GetContext().Shops.ToList();
    }

    public void Create(Shop shop)
    {
        DBContext.GetContext().Shops.Add(shop);
    }

    public void Deleted(Shop shop)
    {
        DBContext.GetContext().Shops.Remove(shop);
    }

    public void Save()
    {
        DBContext.GetContext().SaveChanges();
    }
}