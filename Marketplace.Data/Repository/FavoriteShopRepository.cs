using data.model;
using data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;

public class FavoriteShopRepository: IFavoriteShopRepository
{
    protected readonly MarketplaceContext MarketplaceContext;
    protected readonly DbSet<FavoriteShopsEntity> DbSet;
    
    public FavoriteShopRepository(MarketplaceContext marketplaceContext)
    {
        MarketplaceContext = marketplaceContext;
        DbSet = MarketplaceContext.Set<FavoriteShopsEntity>();
    }

    public async Task<FavoriteShopsEntity?> GetById(Guid shopid, Guid userid)
    {
        return await DbSet.FindAsync(shopid, userid);
    }
    /*public async Task<PageEntity<FavoriteShopsEntity>> GetPage(IQueryable<FavoriteShopsEntity> queryable, int? page, int? size)
    {
        return queryable.GetPage(page, size);
    }*/
    
    
    public async Task<PageEntity<FavoriteShopsEntity>> GetPageFav(Guid userid, int page, int size)
    {
        var favShop = DbSet.Where(a => a.UserId == userid);
        return favShop.GetPage(page, size);
    }

    public async Task<FavoriteShopsEntity> Create(FavoriteShopsEntity t)
    {
        await DbSet.AddAsync(t);
        return t;
    }

    public async Task DeleteAll(FavoriteShopsEntity favoriteShopsEntity)
    { 
        DbSet.RemoveRange(DbSet.Where(a => a.ShopId == favoriteShopsEntity.ShopId));
        
    }
    public async Task Save()
    {
        await MarketplaceContext.SaveChangesAsync(); 
    }
    
    
}