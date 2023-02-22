using data.model;

namespace logic.Service.Inreface;

public interface IFeedbackService
{
    public Task<Page<Feedback>> GetByUser(Guid id, bool isAdmin, int? page, int? size);
    public Task<Page<Feedback>> GetByShop(Guid id, bool isAdmin, int? page, int? size);
    public Task<Feedback> AddFeedback(Feedback feedback);
    
    public Task<Feedback> EditFeedback(Feedback feedback, Guid userid, Role role);
    public Task DeleteFeedback(Guid feedback, Guid userid, Role role);
    public Task<Feedback> ChangeBlockFeedback(Guid id, bool value);
}