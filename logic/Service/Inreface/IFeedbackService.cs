using data.model;

namespace logic.Service.Inreface;

public interface IFeedbackService
{
    public Page<Feedback> GetByUser(Guid id, bool isAdmin);
    public Page<Feedback> GetByShop(Guid id, bool isAdmin);
    public Feedback AddFeedback(Feedback feedback);
    
    public Feedback EditFeedback(Feedback feedback, Guid userid, Role role);
    public void DeleteFeedback(Guid feedback, Guid userid, Role role);
    public Feedback ChangeBlockFeedback(Guid id, bool value);
}