using System.Drawing;
using data.model;
using data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;


public class ShopRepository:BaseRepository<Shop>, IShopRepository
{
    public async Task<Page<Shop>> GetPublicShops()
    { 
        return await GetPage(
            _dbSet.Include(s=>((Shop)s).Creator).Where(a => a.isPublic && a.IsActive),1,20
            );
        
    }

    public async Task<Page<Shop>> GetSellerShops(Guid id)
    {
        return await GetPage(
            _dbSet.Include(s=>((Shop)s).Creator).Where(a => a.CreatorId == id),1,20
            );
        
    }

    public async Task<Shop?> GetByInn(string inn)
    {
        return _dbSet.Include(a=>a.Creator).FirstOrDefault(a => a.Inn == inn && a.IsActive);
        
    }

    public async Task<Page<Shop>> GetPage(int page, int size)
    {
        return await GetPage(
            _dbSet.Where(a=>!a.IsDeleted),page,size
        );
    }

    public ShopRepository(DBContext _dbContext) : base(_dbContext)
    {
    }
}