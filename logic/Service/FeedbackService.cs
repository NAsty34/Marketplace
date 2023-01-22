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
    public Page<Feedback> GetByUser(Guid id, bool isAdmin)
    {
        return _feedbackRepositiry.GetFeedbackbyUser(id, isAdmin);
    }

    public Page<Feedback> GetByShop(Guid id, bool isAdmin)
    {
        return _feedbackRepositiry.GetFeedbackbyShop(id, isAdmin);
    }

    public Feedback AddFeedback(Feedback feedback)
    {
        feedback.Creator = _repositoryUser.GetById((Guid)feedback.CreatorId);
        feedback.Shop = _shopRepository.GetById(feedback.ShopId);
        _feedbackRepositiry.Create(feedback);
        _feedbackRepositiry.Save();
        return feedback;
    }

    public Feedback EditFeedback(Feedback feedback, Guid userid, Role role)
    {
        var fromdb = _feedbackRepositiry.GetById(feedback.Id);
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

    public void DeleteFeedback(Guid feedback, Guid userid, Role role)
    {
        var fromdb = _feedbackRepositiry.GetById(feedback);
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

    public Feedback ChangeBlockFeedback(Guid id, bool value)
    {
        var feedid = _feedbackRepositiry.GetById(id);
        if (feedid== null)
        {
            throw new FeedbackNotFoundException();
        }

        feedid.IsActive = value;
        _feedbackRepositiry.Save();
        return feedid;
    }
}