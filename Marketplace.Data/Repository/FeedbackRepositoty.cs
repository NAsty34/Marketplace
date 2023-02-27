using data.model;
using data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;

public class FeedbackRepositoty: BaseRepository<FeedbackEntity>, IFeedbackRepositiry
{
    public async Task<PageEntity<FeedbackEntity>> GetFeedbackbyUser(Guid user, bool active, int? page, int? size)
    {
        var findfeed = DbSet.Where(a => a.CreatorId == user && a.IsActive);
       
        return await GetPage(
            findfeed.Include(s => s.Creator).Include(s => s.Shop),
            page, size);
    }

    public async Task<PageEntity<FeedbackEntity>> GetFeedbackbyShop(Guid shop, bool active,int? page, int? size)
    {
        var findfeed = DbSet.Where(a => a.ShopId == shop && a.IsActive);
        
        return await GetPage(
            findfeed.Include(s => s.Creator)
                .Include(s => s.Shop), page, size);

    }

    public FeedbackRepositoty(MarketplaceContext marketplaceContext) : base(marketplaceContext)
    {
        
    }
}