using System.Drawing;
using data.model;
using data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;


public class ShopRepository:BaseRepository<Shop>, IShopRepository
{
    public async Task<Shop?> GetByInn(string inn)
    {
        return _dbSet.Include(a=>a.Creator).FirstOrDefault(a => a.Inn == inn && a.IsActive);
        
    }

    public async Task<Page<Shop>> GetPage(FiltersShops filtersShops)
    {
        var q = (IQueryable<Shop>)_dbSet;
        if (filtersShops.IsPublic != null) q = q.Where(a => a.isPublic && a.IsActive);
        if (filtersShops.Id != null) q = q.Where(a => filtersShops.Id.Contains(a.Id));
        if (filtersShops.User != null) q = q.Where(a => a.CreatorId == filtersShops.User.Value);
        if (filtersShops.Name != null) q = q.Where(a => a.Name != null && a.Name.Contains(filtersShops.Name));
        if (filtersShops.category != null) q = q.Where(a => a.ShopCategory.Select(sc => sc.CategoryId).Any(c => filtersShops.category.Contains(c)));
        if (filtersShops.deliveries != null) q = q.Where(a => a.ShopDeliveries.Select(sc => sc.DeliveryId).Any(c => filtersShops.deliveries.Contains(c)));
        if (filtersShops.payment != null) q = q.Where(a => a.ShopPayment.Select(sc => sc.Paymentid).Any(c => filtersShops.payment.Contains(c)));
        if (filtersShops.types != null) q = q.Where(a => a.ShopTypes.Select(sc => sc.TypeId).Any(c => filtersShops.types.Contains(c)));
            
        
        return await GetPage(
            q,
            filtersShops.page ?? 1,
            filtersShops.size ?? 20
        );
    }

    public async Task<Page<Shop>> GetByNameAnddescription(string name,string description)
    {
        return await GetPage(
            _dbSet.Include(a => a.Creator).Where(a => a.Name == name && a.Description == description && a.IsActive), 1,
            20
        );
    }


    public ShopRepository(DBContext _dbContext) : base(_dbContext)
    {
    }
}