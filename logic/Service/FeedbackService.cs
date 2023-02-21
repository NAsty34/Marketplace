using data.model;
using data.Repository.Interface;
using logic.Exceptions;
using logic.Service.Inreface;

namespace logic.Service;

public class FeedbackService:IFeedbackService
{
    private IFeedbackRepositiry _feedbackRepositiry;
    private IShopRepository _shopRepository;
    private IRepositoryUser _repositoryUser;

    public FeedbackService(IFeedbackRepositiry feedbackRepositiry, IShopRepository shopRepository, IRepositoryUser repositoryUser)
    {
        _feedbackRepositiry = feedbackRepositiry;
        _shopRepository = shopRepository;
        _repositoryUser = repositoryUser;
    }
    public async Task<Page<Feedback>> GetByUser(Guid id, bool isAdmin)
    {
        return await _feedbackRepositiry.GetFeedbackbyUser(id, isAdmin);
    }

    public async Task<Page<Feedback>> GetByShop(Guid id, bool isAdmin)
    {
        return await _feedbackRepositiry.GetFeedbackbyShop(id, isAdmin);
    }

    public async Task AddFeedback(Feedback feedback)
    {
        feedback.Creator = await _repositoryUser.GetById(feedback.CreatorId);
        feedback.Shop = await _shopRepository.GetById(feedback.ShopId);
       await _feedbackRepositiry.Create(feedback);
        await _feedbackRepositiry.Save();
    }

    public async Task<Feedback> EditFeedback(Feedback feedback, Guid userid, Role role)
    {
        var fromdb = await _feedbackRepositiry.GetById(feedback.Id);
        if (fromdb == null)
        {
            throw new FeedbackNotFoundException();
        }

        if (!role.Equals(Role.Admin) && fromdb.CreatorId != userid)
        {
            throw new AccessDeniedException();
        }

        fromdb.Stars = feedback.Stars;
        fromdb.Content = feedback.Content;
        fromdb.EditDate = DateTime.Now;
        fromdb.EditorId = userid;
        await _feedbackRepositiry.Save();
        return fromdb;
    }

    public async Task DeleteFeedback(Guid feedback, Guid userid, Role role)
    {
        var fromdb = await _feedbackRepositiry.GetById(feedback);
        if (fromdb == null)
        {
            throw new FeedbackNotFoundException();
        }

        if (!role.Equals(Role.Admin) && fromdb.CreatorId != userid)
        {
            throw new AccessDeniedException();
        }
        fromdb.IsDeleted = true;
        fromdb.DeletorId = userid;
        fromdb.DeletedDate = DateTime.Now;
        await _feedbackRepositiry.Save();
    }

    public async Task<Feedback> ChangeBlockFeedback(Guid id, bool value)
    {
        var feedid = await _feedbackRepositiry.GetById(id);
        if (feedid== null)
        {
            throw new FeedbackNotFoundException();
        }

        feedid.IsActive = value;
        await _feedbackRepositiry.Save();
        return feedid;
    }
}