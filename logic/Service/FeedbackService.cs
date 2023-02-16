using data;
using data.model;
using data.Repository;
using data.Repository.Interface;
using logic.Exceptions;
using logic.Service.Inreface;

namespace logic.Service;

public class FeedbackService:IFeedbackService
{
    private IFeedbackRepositiry _feedbackRepositiry;
    private IShopRepository _shopRepository;
    private IRepositoryUser _repositoryUser;

    public FeedbackService(IFeedbackRepositiry _feedbackRepositiry, IShopRepository _shopRepository, IRepositoryUser _repositoryUser)
    {
        this._feedbackRepositiry = _feedbackRepositiry;
        this._shopRepository = _shopRepository;
        this._repositoryUser = _repositoryUser;
    }
    public async Task<Page<Feedback>> GetByUser(Guid id, bool isAdmin)
    {
        return await _feedbackRepositiry.GetFeedbackbyUser(id, isAdmin);
    }

    public async Task<Page<Feedback>> GetByShop(Guid id, bool isAdmin)
    {
        return await _feedbackRepositiry.GetFeedbackbyShop(id, isAdmin);
    }

    public async void AddFeedback(Feedback feedback)
    {
        feedback.Creator = await _repositoryUser.GetById(feedback.CreatorId.Value);
        feedback.Shop = await _shopRepository.GetById(feedback.ShopId);
        _feedbackRepositiry.Create(feedback);
        _feedbackRepositiry.Save();
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
        _feedbackRepositiry.Save();
        return fromdb;
    }

    public async void DeleteFeedback(Guid feedback, Guid userid, Role role)
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
        _feedbackRepositiry.Save();
    }

    public async Task<Feedback> ChangeBlockFeedback(Guid id, bool value)
    {
        var feedid = await _feedbackRepositiry.GetById(id);
        if (feedid== null)
        {
            throw new FeedbackNotFoundException();
        }

        feedid.IsActive = value;
        _feedbackRepositiry.Save();
        return feedid;
    }
}