using data.model;
using data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;


public class ShopRepository:BaseRepository<Shop>, IShopRepository
{
    public async Task<Shop?> GetByInn(string inn)
    {
        return DbSet.Include(a=>a.Creator).FirstOrDefault(a => a.Inn == inn && a.IsActive);
        
    }

    public async Task<Page<Shop>> GetPage(FiltersShops filtersShops, int? page, int? size)
    {
        var q = (IQueryable<Shop>)DbSet;
        if (filtersShops.IsPublic != null) q = q.Where(a => a.IsPublic && a.IsActive);
        if (filtersShops.Id != null) q = q.Where(a => filtersShops.Id.Contains(a.Id));
        if (filtersShops.User != null) q = q.Where(a => a.CreatorId == filtersShops.User.Value);
        if (filtersShops.Name != null) q = q.Where(a => a.Name.Contains(filtersShops.Name));
        if (filtersShops.Description != null)
            q = q.Where(a => a.Description.Contains(filtersShops.Description));
        if (filtersShops.Category?.Any() ?? false) q = q.Where(a => a.ShopCategory.Select(sc => sc.CategoryId).Any(c => filtersShops.Category.Contains(c)));
        if (filtersShops.Deliveries?.Any() ?? false) q = q.Where(a => a.ShopDeliveries.Select(sc => sc.DeliveryId).Any(c => filtersShops.Deliveries.Contains(c)));
        if (filtersShops.Payment?.Any() ?? false) q = q.Where(a => a.ShopPayment.Select(sc => sc.PaymentId).Any(c => filtersShops.Payment.Contains(c)));
        if (filtersShops.Types?.Any() ?? false) q = q.Where(a => a.ShopTypes.Select(sc => sc.TypeId).Any(c => filtersShops.Types.Contains(c)));
            
        
        return await GetPage(
            q,
            filtersShops.Page ?? page,
            filtersShops.Size ?? size
        );
    }



    public ShopRepository(DBContext dbContext) : base(dbContext)
    {
    }
}