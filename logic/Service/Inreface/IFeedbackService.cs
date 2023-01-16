using data.model;

namespace logic.Service.Inreface;

public interface IFeedbackService
{
    public Page<Feedback> GetByUser(int id, bool isAdmin);
    public Page<Feedback> GetByShop(int id, bool isAdmin);
    public Feedback AddFeedback(Feedback feedback);
    
    public Feedback EditFeedback(Feedback feedback, int userid, Role role);
    public void DeleteFeedback(int feedback, int userid, Role role);
    public Feedback ChangeBlockFeedback(int id, bool value);
}