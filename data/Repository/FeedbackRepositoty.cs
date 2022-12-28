using data.model;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;

public class FeedbackRepositoty:IFeedbackRepositiry
{
    public IEnumerable<Feedback> GetFeedbackbyUser(int user)
    {
        return DBContext.GetContext().Feedbacks.Where(a => a.IDUser.Id == user).Include(s=>s.IDUser).Include(s=>s.IDShop).ToList();
    }

    public IEnumerable<Feedback> GetFeedbackbyShop(Shop shop)
    {
        return DBContext.GetContext().Feedbacks.Where(a => a.IDShop.Id == shop.Id).Include(s => s.IDUser)
            .Include(s => s.IDShop).ToList();
    }

    public IEnumerable<Feedback> GetFeedbackbyUserandShop(User user, Shop shop)
    {
        return DBContext.GetContext().Feedbacks.Where(a => a.IDUser.Id == user.Id && a.IDShop.Id == shop.Id)
            .Include(s => s.IDUser).Include(s => s.IDShop).ToList();
    }

    public void Create(Feedback feedback)
    {
        DBContext.GetContext().Feedbacks.Add(feedback);
    }

    public void Deleted(Feedback feedback)
    {
        DBContext.GetContext().Feedbacks.Remove(feedback);
    }

    public void Save()
    {
        DBContext.GetContext().SaveChanges();
    }
}