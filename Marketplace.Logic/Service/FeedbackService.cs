using data.model;
using data.Repository.Interface;
using logic.Exceptions;
using logic.Service.Inreface;

namespace logic.Service;

public class FeedbackService : IFeedbackService
{
    private IFeedbackRepositiry _feedbackRepositiry;
    private IShopRepository _shopRepository;
    private IRepositoryUser _repositoryUser;

    public FeedbackService(IFeedbackRepositiry feedbackRepositiry, IShopRepository shopRepository,
        IRepositoryUser repositoryUser)
    {
        _feedbackRepositiry = feedbackRepositiry;
        _shopRepository = shopRepository;
        _repositoryUser = repositoryUser;
    }

    public async Task<PageEntity<FeedbackEntity>> GetByUser(Guid id, bool isAdmin, int? page, int? size)
    {
        var user = await _repositoryUser.GetById(id);
        if (user == null || !user.IsActive) throw new UserNotFoundException();
        return await _feedbackRepositiry.GetFeedbackbyUser(id, isAdmin, page, size);
    }

    public async Task<PageEntity<FeedbackEntity>> GetByShop(Guid id, bool isAdmin, int? page, int? size)
    {
        var shop = await _shopRepository.GetById(id);
        if (shop == null || !shop.IsActive) throw new ShopNotFoundException();
        return await _feedbackRepositiry.GetFeedbackbyShop(id, isAdmin, page, size);
    }

    public async Task<FeedbackEntity> AddFeedback(FeedbackEntity feedbackEntity)
    {
        feedbackEntity.Creator = await _repositoryUser.GetById(feedbackEntity.CreatorId.Value);
        feedbackEntity.Shop = await _shopRepository.GetById(feedbackEntity.ShopId);
        if (feedbackEntity.Shop == null || !feedbackEntity.Shop.IsActive) throw new ShopNotFoundException();
        await _feedbackRepositiry.Create(feedbackEntity);
        await _feedbackRepositiry.Save();
        return feedbackEntity;
    }

    public async Task<FeedbackEntity> EditFeedback(FeedbackEntity feedbackEntity, Guid userid, RoleEntity roleEntity)
    {
        var fromdb = await _feedbackRepositiry.GetById(feedbackEntity.Id);
        if (fromdb == null || !fromdb.IsActive)
        {
            throw new FeedbackNotFoundException();
        }

        if (!roleEntity.Equals(RoleEntity.Admin) && fromdb.CreatorId != userid)
        {
            throw new AccessDeniedException();
        }

        fromdb.Stars = feedbackEntity.Stars;
        fromdb.Content = feedbackEntity.Content;
        fromdb.EditDate = DateTime.Now;
        fromdb.EditorId = userid;
        await _feedbackRepositiry.Save();
        return fromdb;
    }

    public async Task DeleteFeedback(Guid feedback, Guid userid, RoleEntity roleEntity)
    {
        var fromdb = await _feedbackRepositiry.GetById(feedback);
        if (fromdb == null || !fromdb.IsActive)
        {
            throw new FeedbackNotFoundException();
        }

        if (!roleEntity.Equals(RoleEntity.Admin) && fromdb.CreatorId != userid)
        {
            throw new AccessDeniedException();
        }

        fromdb.IsDeleted = true;
        fromdb.DeletorId = userid;
        fromdb.DeletedDate = DateTime.Now;
        await _feedbackRepositiry.Save();
    }

    public async Task<FeedbackEntity> ChangeBlockFeedback(Guid id, bool value)
    {
        var feedid = await _feedbackRepositiry.GetById(id);
        if (feedid == null)
        {
            throw new FeedbackNotFoundException();
        }

        feedid.IsActive = value;
        await _feedbackRepositiry.Save();
        return feedid;
    }
}