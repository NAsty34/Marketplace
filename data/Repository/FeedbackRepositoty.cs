using data.model;
using data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;

public class FeedbackRepositoty: BaseRepository<Feedback>, IFeedbackRepositiry
{
    public async Task<Page<Feedback>> GetFeedbackbyUser(Guid user, bool active, int? page, int? size)
    {
        var findfeed = DbSet.Where(a => a.CreatorId == user && !a.IsDeleted);
        if (active)
        {
            findfeed = findfeed.Where(a => a.IsActive);
        }
        return await GetPage(
            findfeed.Include(s => s.Creator).Include(s => s.Shop),
            page, size);
    }

    public async Task<Page<Feedback>> GetFeedbackbyShop(Guid shop, bool active,int? page, int? size)
    {
        var findfeed = DbSet.Where(a => a.ShopId == shop && !a.IsDeleted);
        if (active)
        {
            findfeed = findfeed.Where(a=>a.IsActive);
        }
        return await GetPage(
            findfeed.Include(s => s.Creator)
                .Include(s => s.Shop), page, size);

    }

    public FeedbackRepositoty(DBContext dbContext) : base(dbContext)
    {
        
    }
}