using data.model;
using data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;

public class ShopDictionaryRepository<T> : IShopDictionaryRepository<T> where T : ShopDictionaryBase
{
    protected DBContext _dbContext;
    protected DbSet<T> _dbSet;

    public ShopDictionaryRepository(DBContext _dbContext)
    {
        this._dbContext = _dbContext;
        this._dbSet = _dbContext.Set<T>();
    }

    

    public async void DeleteAllByShop(Guid shopid)
    {
        _dbSet.RemoveRange(_dbSet.Where(a => a.shopid == shopid));
        _dbContext.SaveChanges();
    }

    public async void CreateRange(IEnumerable<T> ids)
    {
        _dbSet.AddRange(ids);
        _dbContext.SaveChanges();
    }

}