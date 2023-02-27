using data.model;

namespace data.Repository.Interface;

public interface IFeedbackRepositiry:IBaseRopository<FeedbackEntity>
{
    Task<PageEntity<FeedbackEntity>> GetFeedbackbyUser(Guid user, bool active, int? page, int? size); //получение по пользователю
    Task<PageEntity<FeedbackEntity>> GetFeedbackbyShop(Guid shop, bool active, int? page, int? size); //получение по Магазину
   
}