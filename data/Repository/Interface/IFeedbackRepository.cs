using data.model;

namespace data.Repository.Interface;

public interface IFeedbackRepositiry:IBaseRopository<Feedback>
{
    Task<Page<Feedback>> GetFeedbackbyUser(Guid user, bool active, int? page, int? size); //получение по пользователю
    Task<Page<Feedback>> GetFeedbackbyShop(Guid shop, bool active, int? page, int? size); //получение по Магазину
   
}