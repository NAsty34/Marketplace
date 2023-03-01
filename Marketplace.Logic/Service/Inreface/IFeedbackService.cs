using data.model;

namespace logic.Service;

public interface IFeedbackService
{
    public Task<PageEntity<FeedbackEntity>> GetByUser(Guid id, bool isAdmin, int? page, int? size);
    public Task<PageEntity<FeedbackEntity>> GetByShop(Guid id, bool isAdmin, int? page, int? size);
    public Task<FeedbackEntity> AddFeedback(FeedbackEntity feedbackEntity);
    
    public Task<FeedbackEntity> EditFeedback(FeedbackEntity feedbackEntity, Guid userid, RoleEntity roleEntity);
    public Task DeleteFeedback(Guid feedback, Guid userid, RoleEntity roleEntity);
    public Task<FeedbackEntity> ChangeBlockFeedback(Guid id, bool value);
}