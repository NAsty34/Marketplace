using data.model;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;

public class FeedbackRepositoty: BaseRepository<Feedback>, IFeedbackRepositiry
{
    public Page<Feedback> GetFeedbackbyUser(int user)
    {
        return GetPage(
            _dbSet.Where(a => a.CreatorId == user && a.IsDeleted == false).Include(s => s.Creator).Include(s => s.Shop),
            1, 20);
    }

    public Page<Feedback> GetFeedbackbyShop(int shop)
    {
        return GetPage(
            _dbSet.Where(a => a.ShopId == shop && a.IsDeleted == false).Include(s => s.Creator)
                .Include(s => s.Shop), 1, 20);

    }

    public FeedbackRepositoty(DBContext _dbContext) : base(_dbContext, _dbContext.Feedbacks)
    {
        
    }
}