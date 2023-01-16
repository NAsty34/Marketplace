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
    public Page<Feedback> GetByUser(int id, bool isAdmin)
    {
        return _feedbackRepositiry.GetFeedbackbyUser(id, isAdmin);
    }

    public Page<Feedback> GetByShop(int id, bool isAdmin)
    {
        return _feedbackRepositiry.GetFeedbackbyShop(id, isAdmin);
    }

    public Feedback AddFeedback(Feedback feedback)
    {
        feedback.Creator = _repositoryUser.GetById(feedback.CreatorId);
        feedback.Shop = _shopRepository.GetById(feedback.ShopId);
        _feedbackRepositiry.Create(feedback);
        _feedbackRepositiry.Save();
        return feedback;
    }

    public Feedback EditFeedback(Feedback feedback, int userid, Role role)
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

    public void DeleteFeedback(int feedback, int userid, Role role)
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

    public Feedback ChangeBlockFeedback(int id, bool value)
    {
        var s = _feedbackRepositiry.GetById(id);
        if (s == null)
        {
            throw new FeedbackNotFoundException();
        }

        s.IsActive = value;
        _feedbackRepositiry.Save();
        return s;
    }
}