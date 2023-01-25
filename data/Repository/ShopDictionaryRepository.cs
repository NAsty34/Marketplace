using data.model;
using data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;

public class ShopDictionaryRepository<T>:BaseRepository<T>,IShopDictionaryRepository<T> where T : ShopDictionaryBase
{
    public void DeleteAllByShop(Guid shopid)
    {
         _dbSet.RemoveRange(_dbSet.Where(a=>a.shopid == shopid));
         Save();
    }

    public List<T> CreateRange(List<T> ids)
    {
        _dbSet.AddRange(ids);
        Save();
        return ids;
    }

    public ShopDictionaryRepository(DBContext _dbContext, DbSet<T> _dbSet) : base(_dbContext, _dbSet)
    {
    }
}