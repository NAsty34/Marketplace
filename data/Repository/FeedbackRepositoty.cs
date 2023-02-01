using data.model;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;

public class FeedbackRepositoty: BaseRepository<Feedback>, IFeedbackRepositiry
{
    public Page<Feedback> GetFeedbackbyUser(Guid user, bool active)
    {
        var findfeed = _dbSet.Where(a => a.CreatorId == user && !a.IsDeleted);
        if (active)
        {
            findfeed = findfeed.Where(a => a.IsActive);
        }
        return GetPage(
            findfeed.Include(s => s.Creator).Include(s => s.Shop),
            1, 20);
    }

    public Page<Feedback> GetFeedbackbyShop(Guid shop, bool active)
    {
        var findfeed = _dbSet.Where(a => a.ShopId == shop && !a.IsDeleted);
        if (active)
        {
            findfeed = findfeed.Where(a=>a.IsActive);
        }
        return GetPage(
            findfeed.Include(s => s.Creator)
                .Include(s => s.Shop), 1, 20);

    }

    public FeedbackRepositoty(DBContext _dbContext) : base(_dbContext)
    {
        
    }
}