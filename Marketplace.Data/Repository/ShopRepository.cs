using data.model;
using data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;


public class ShopRepository:BaseRepository<ShopEntity>, IShopRepository
{
    public async Task<ShopEntity?> GetByInn(string inn)
    {
        return DbSet.Include(a=>a.Creator).FirstOrDefault(a => a.Inn == inn && a.IsActive);
        
    }

    public async Task<PageEntity<ShopEntity>> GetPage(FiltersShopsEntity filtersShopsEntity, int? page, int? size)
    {
        var q = (IQueryable<ShopEntity>)DbSet;
        if (filtersShopsEntity.IsPublic != null) q = q.Where(a => a.IsPublic && a.IsActive);
        if (filtersShopsEntity.Id != null) q = q.Where(a => filtersShopsEntity.Id.Contains(a.Id) && a.IsActive);
        if (filtersShopsEntity.User != null) q = q.Where(a => a.CreatorId == filtersShopsEntity.User.Value && a.IsActive);
        if (filtersShopsEntity.Name != null) q = q.Where(a => a.Name.Contains(filtersShopsEntity.Name) && a.IsActive);
        if (filtersShopsEntity.Description != null)
            q = q.Where(a => a.Description.Contains(filtersShopsEntity.Description) && a.IsActive);
        if (filtersShopsEntity.Category?.Any() ?? false) q = q.Where(a => a.ShopCategory.Select(sc => sc.CategoryId).Any(c => filtersShopsEntity.Category.Contains(c)) && a.IsActive);
        if (filtersShopsEntity.Deliveries?.Any() ?? false) q = q.Where(a => a.ShopDeliveries.Select(sc => sc.DeliveryId).Any(c => filtersShopsEntity.Deliveries.Contains(c)) && a.IsActive);
        if (filtersShopsEntity.Payment?.Any() ?? false) q = q.Where(a => a.ShopPayment.Select(sc => sc.PaymentId).Any(c => filtersShopsEntity.Payment.Contains(c)) && a.IsActive);
        if (filtersShopsEntity.Types?.Any() ?? false) q = q.Where(a => a.ShopTypes.Select(sc => sc.TypeId).Any(c => filtersShopsEntity.Types.Contains(c)) && a.IsActive);
            
        
        return await GetPage(
            q,
            filtersShopsEntity.Page ?? page,
            filtersShopsEntity.Size ?? size
        );
    }



    public ShopRepository(MarketplaceContext marketplaceContext) : base(marketplaceContext)
    {
    }
}