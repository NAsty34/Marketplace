using data.model;
using data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;

public class FeedbackRepositoty: BaseRepository<Feedback>, IFeedbackRepositiry
{
    public async Task<Page<Feedback>> GetFeedbackbyUser(Guid user, bool active)
    {
        var findfeed = DbSet.Where(a => a.CreatorId == user && !a.IsDeleted);
        if (active)
        {
            findfeed = findfeed.Where(a => a.IsActive);
        }
        return await GetPage(
            findfeed.Include(s => s.Creator).Include(s => s.Shop),
            1, 20);
    }

    public async Task<Page<Feedback>> GetFeedbackbyShop(Guid shop, bool active)
    {
        var findfeed = DbSet.Where(a => a.ShopId == shop && !a.IsDeleted);
        if (active)
        {
            findfeed = findfeed.Where(a=>a.IsActive);
        }
        return await GetPage(
            findfeed.Include(s => s.Creator)
                .Include(s => s.Shop), 1, 20);

    }

    public FeedbackRepositoty(DBContext dbContext) : base(dbContext)
    {
        
    }
}