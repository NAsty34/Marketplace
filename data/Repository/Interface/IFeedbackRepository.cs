using data.model;
using data.Repository.Interface;

namespace data.Repository;

public interface IFeedbackRepositiry:IBaseRopository<Feedback>
{
    Page<Feedback> GetFeedbackbyUser(Guid user, bool active); //получение по пользователю
    Page<Feedback> GetFeedbackbyShop(Guid shop, bool active); //получение по Магазину
   
}