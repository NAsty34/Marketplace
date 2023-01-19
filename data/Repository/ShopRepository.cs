using System.Drawing;
using data.model;
using data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;


public class ShopRepository:BaseRepository<Shop>, IShopRepository
{
    public Page<Shop> GetPublicShops()
    { 
        return GetPage(
            _dbSet.Include(s=>((Shop)s).Creator).Where(a => a.isPublic && a.IsActive && !a.IsDeleted),1,20
            );
        
    }

    public Page<Shop> GetSellerShops(int id)
    {
        return GetPage(
            _dbSet.Include(s=>((Shop)s).Creator).Where(a => a.CreatorId == id && !a.IsDeleted),1,20
            );
        
    }

    public Shop? GetByInn(string inn)
    {
        return _dbSet.Include(a=>a.Creator).FirstOrDefault(a => a.Inn == inn && a.IsActive && !a.IsDeleted);
        
    }

    public Page<Shop> GetPage(int page, int size)
    {
        return GetPage(
            _dbSet.Where(a=>!a.IsDeleted),page,size
        );
    }

    public ShopRepository(DBContext _dbContext) : base(_dbContext, _dbContext.Shops)
    {
    }
}